using Asp.NetProjeTurkcell.Models;
using Asp.NetProjeTurkcell.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Asp.NetProjeTurkcell.Controllers
{
	public class VisitorAjaxController : Controller
	{

		private readonly IMapper _mapper;
		private readonly AppDbContext _context;

		public VisitorAjaxController(IMapper mapper, AppDbContext context)
		{
			_mapper = mapper;
			_context = context;
		}
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult SaveVisitorComment(VisitorViewModel visitorViewModel)
		{
			var visitor = _mapper.Map<Visitor>(visitorViewModel);

			visitor.Created=DateTime.Now;

			_context.Visitors.Add(visitor);
			_context.SaveChanges();

			return Json(new {IsSuccsess="true"});
		}

		[HttpGet]
		public IActionResult VisitorCommentList()
		{
			var visitors = _context.Visitors.ToList();
			var visitorViewModels = _mapper.Map<List<VisitorViewModel>>(visitors);


			return Json(visitorViewModels);
		}
	}
}
