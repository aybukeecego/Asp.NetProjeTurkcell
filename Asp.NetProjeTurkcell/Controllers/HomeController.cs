using Asp.NetProjeTurkcell.Models;
using Asp.NetProjeTurkcell.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Asp.NetProjeTurkcell.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, AppDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [Route("/")]
        [Route("/Home")]
        [Route("/Home/Index")]

        public IActionResult Index()
        {
            var product = _context.Products.OrderByDescending(x => x.Id).Select(x => new ProductPartialViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Stock = x.Stock,


            }).ToList();

            ViewBag.productListPartialView = new ProductListPartialViewModel()
            {
                Products = product
            };
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(ErrorViewModel errorViewModel)
        {

            errorViewModel.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;


            return View(errorViewModel);
        }

        public IActionResult Visitor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveVisitorComment(VisitorViewModel visitorviewmodel)
        {
            try
            {
                var visitor = _mapper.Map<Visitor>(visitorviewmodel);
                visitor.Created = DateTime.Now;
                _context.Visitors.Add(visitor);
                _context.SaveChanges();

                TempData["result"] = "Yorumunuz kaydolmuştur";

                return RedirectToAction(nameof(HomeController.Visitor));

            }
            catch (Exception)
            {

                TempData["result"] = "Yorumunuz kaydedilirken bir hata meydana geldi";
                return RedirectToAction(nameof(HomeController.Visitor));

            }

        }
    }
}