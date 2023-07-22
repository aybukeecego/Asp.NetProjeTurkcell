using Asp.NetProjeTurkcell.Models;
using Asp.NetProjeTurkcell.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Asp.NetProjeTurkcell.Views.Shared.ViewCompanents
{
    public class ProductListViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public ProductListViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int type = 1)
        {
            var component = _context.Products.Select(x => new ProductListComponentViewModel()
            {
                Name = x.Name,
                Description = x.Description
            }).ToList();


            if (type == 1)
            {
                return View("Default", component);
            }
            else
            {
                return View("Type2", component);
            }
        }
    }
}
