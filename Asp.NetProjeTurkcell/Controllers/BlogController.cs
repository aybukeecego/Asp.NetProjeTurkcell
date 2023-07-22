using Microsoft.AspNetCore.Mvc;

namespace Asp.NetProjeTurkcell.Controllers
{
	public class BlogController : Controller
	{
		public IActionResult Article(string name, int id)
		{
			//var routes = Request.RouteValues["article"];
			return View();
		}
	}
}
