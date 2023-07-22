using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Asp.NetProjeTurkcell.Filters
{
	public class LogFilters : ActionFilterAttribute
	{
		//actiondan önce
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			Debug.WriteLine("Action metod çalışmadan önce");
		}
		//actiondan sonra
		public override void OnActionExecuted(ActionExecutedContext context)
		{
			Debug.WriteLine("Action metod çalıştıktan sonra");

		}
		//sonuçtan önce
		public override void OnResultExecuting(ResultExecutingContext context)
		{
			Debug.WriteLine("Sonuçtan önce");

		}
		//sonuçtan sonra
		public override void OnResultExecuted(ResultExecutedContext context)
		{
			Debug.WriteLine("Sonuçtan Sonra");

		}


	}
}
