using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Asp.NetProjeTurkcell.TagHelpers
{
    public class ImageThumbnailTagHelper : TagHelper
    {
        public string ImageSrc { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // <img src=""/>
            output.TagName = "img";
            // Aşağıdaki iki şey formatı ve ismi bölmek içindi. Birincisi cat ismine atandı diğeri ise formatı olan jpg e atandı
            var fileName = ImageSrc.Split(".")[0];
            var fileExtensions = Path.GetExtension(ImageSrc);

            output.Attributes.SetAttribute("src", $"{fileName}-427x640{fileExtensions}");

        }
    }
}
