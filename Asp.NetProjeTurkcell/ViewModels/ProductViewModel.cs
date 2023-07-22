

using Asp.NetProjeTurkcell.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Asp.NetProjeTurkcell.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "İsim Kısmı Boş olamaz")]
        [Remote(action: "HasProductName", controller: "Products")]
        public string? Name { get; set; }

        public int Stock { get; set; }
        public decimal Price { get; set; }
        public bool IsPublish { get; set; }
        public int Expire { get; set; }
        [Required(ErrorMessage = "Açıklama Alanı Boş olamaz")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Açıklama alanı minimum 10 maksimum 50 karakter olmalı")]
        public string? Description { get; set; }
        public string? Color { get; set; }
        public DateTime PublishDate { get; set; }

		[ValidateNever]
		public IFormFile? Image { get; set; }

        [ValidateNever]
        public string ImagePath { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }    

	}
}
