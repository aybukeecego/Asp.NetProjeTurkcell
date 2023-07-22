using Asp.NetProjeTurkcell.Models;
using Asp.NetProjeTurkcell.ViewModels;
using AutoMapper;

namespace Asp.NetProjeTurkcell.Mapping
{
	public class ViewModelMapping : Profile
	{
		public ViewModelMapping()
		{
			CreateMap<Product, ProductViewModel>().ReverseMap();
			CreateMap<Visitor, VisitorViewModel>().ReverseMap();

		}
	}
}
