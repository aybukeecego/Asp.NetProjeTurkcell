
using System.Net;

namespace Asp.NetProjeTurkcell.Middleware
{
	public class WhiteIpAdressControlMiddleware
	{
		private readonly RequestDelegate _requestDelegate;
		private const string WhiteIpAdress = "::1";

		public WhiteIpAdressControlMiddleware(RequestDelegate requestDelegate)
		{
			_requestDelegate = requestDelegate;
		}

		public async Task InvokeAsync(HttpContext context)
		{

			var requestIpAdress = context.Connection.RemoteIpAddress;

			bool anyWhiteAdress=IPAddress.Parse(WhiteIpAdress).Equals(requestIpAdress);

			if (anyWhiteAdress==true)
			{
				await _requestDelegate(context);
			}
			else
			{
				context.Response.StatusCode = HttpStatusCode.Forbidden.GetHashCode();
				await context.Response.WriteAsync("Forbidden");
			}

		}
	}
}
