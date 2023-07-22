using Asp.NetProjeTurkcell.Filters;
using Asp.NetProjeTurkcell.Models;
using Asp.NetProjeTurkcell.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace Asp.NetProjeTurkcell.Controllers
{
    [Route("[controller]/[action]")]
    public class ProductsController : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileProvider _fileProvider;
		public ProductsController(AppDbContext context, IMapper mapper, IFileProvider fileProvider)
		{
			_productRepository = new ProductRepository();
			_context = context;
			_mapper = mapper;
			_fileProvider = fileProvider;
		}

        public IActionResult Index()
        {
            List<ProductViewModel> products = _context.Products.Include(x => x.Category).Select(x => new ProductViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Stock = x.Stock,
                Color = x.Color,
                Expire = x.Expire,
                IsPublish = x.IsPublish,
                Price = x.Price,
                PublishDate = x.PublishDate,
                ImagePath = x.ImagePath,
                CategoryName = x.Category.Name

            }).ToList();


            return View(products);
        }

        [HttpGet("{id}")]
        [ServiceFilter(typeof(NotFoundFilter))]
        public IActionResult Remove(int id)
        {
            var product = _context.Products.Find(id);
            _ = _context.Products.Remove(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Add()
        {

            ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1 ay",1 },
                {"3 ay",3 },
                {"6 ay",6 },
                {"12 ay",12 }

            };
            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>() {
                new(){Data="Mavi", Value="Mavi"},

                new(){Data="Yeşil", Value="Yeşil"},

                new(){Data="Pembe", Value="Pembe"}



            }, "Value", "Data");

            var categories=_context.Category.ToList();

            ViewBag.CategorySelect = new SelectList(categories,"Id","Name");



            return View();
        }
        [AcceptVerbs("POST", "GET")]
        public IActionResult HasProductName(string name)
        {
            var anyProduct = _context.Products.Any(x => x.Name.ToLower() == name.ToLower());
            if (anyProduct)
                return Json("Kaydetmek istediğiniz ürün Veritabında zaten var");
            else
                return Json(true);

        }

        [HttpPost]
        public IActionResult Add(ProductViewModel newProduct)
        {
            IActionResult result = null;



            if (ModelState.IsValid)
            {
                try 
                {
					var product = _mapper.Map<Product>(newProduct);

					if (newProduct.Image!=null && newProduct.Image.Length > 0)
                    {
						var root = _fileProvider.GetDirectoryContents("wwroot");
						var images = root.First(x => x.Name == "images");

						var randomImageName = Guid.NewGuid() + Path.GetExtension(newProduct.Image.FileName);

						var path = Path.Combine(images.PhysicalPath, randomImageName);

						using var stream = new FileStream(path, FileMode.Create);

						newProduct.Image.CopyTo(stream);

						product.ImagePath = randomImageName;

					}


					_context.Products.Add(product);
					_context.SaveChanges();


					TempData["status"] = "Ürün Başarıyla Eklendi";
					return RedirectToAction("Index");
				}
                catch (Exception)
                {
					result = View();
				}
            }
            else
            {
                result = View();
            }

			ViewBag.Expire = new Dictionary<string, int>()
			{
				{"1 ay",1 },
				{"3 ay",3 },
				{"6 ay",6 },
				{"12 ay",12 }

			};
			ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>() {
				new(){Data="Mavi", Value="Mavi"},

				new(){Data="Yeşil", Value="Yeşil"},

				new(){Data="Pembe", Value="Pembe"}



			}, "Value", "Data");

            return result;
		}

        [HttpGet("{id}")]
        [ServiceFilter(typeof(NotFoundFilter))]
        public IActionResult Update(int id)
        {
            var product = _context.Products.Find(id);
            ViewBag.ExpireValue = product.Expire;
            ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1 ay",1 },
                {"3 ay",3 },
                {"6 ay",6 },
                {"12 ay",12 }

            };
            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>() {
                new(){Data="Mavi", Value="Mavi"},

                new(){Data="Yeşil", Value="Yeşil"},

                new(){Data="Pembe", Value="Pembe"}



            }, "Value", "Data", product.Color);

            return View(product);
        }
        [HttpPost]
        
        public IActionResult Update(Product updateProduct)
        {
            _context.Products.Update(updateProduct);
            _context.SaveChanges();
            TempData["status"] = "Ürün Başarıyla Güncellendi";
            return RedirectToAction("Index");
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        [Route("urunler/urun/{productid}", Name = "product")]
        public IActionResult GetById(int productid)
        {
            var product = _context.Products.Find(productid);
            return View(_mapper.Map<ProductViewModel>(product));
        }

        [Route("[controller]/[action]/{page}/{pageSize}", Name = "productPage")]
        public IActionResult Pages(int page, int pageSize)
        {
            var product = _context.Products.Skip((page - 1) * (pageSize)).Take(pageSize).ToList();
            ViewBag.pageSize = pageSize;
            ViewBag.page = page;
            return View(_mapper.Map<List<ProductViewModel>>(product));

        }




    }
}
