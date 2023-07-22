using Asp.NetProjeTurkcell.Models;
using Asp.NetProjeTurkcell.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Asp.NetProjeTurkcell.Views.Shared.ViewComponents
{
	public class VisitorViewComponent : ViewComponent
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;

		public VisitorViewComponent(IMapper mapper, AppDbContext context)
		{
			_mapper = mapper;
			_context = context;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var visitor = _context.Visitors.ToList();
			var visitorViewModel = _mapper.Map<List<VisitorViewModel>>(visitor);
			ViewBag.visitorViewModel = visitorViewModel;
			return View();
		}
	}
}
