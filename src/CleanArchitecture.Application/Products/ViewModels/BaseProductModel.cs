using System.ComponentModel.DataAnnotations;
using CleanArchitecture.Common.Dimensions;

namespace CleanArchitecture.Application.Products.ViewModels
{
    public class BaseProductModel
    {
        [StringLength(
            ProductsDimensions.LengthMax,
            MinimumLength = ProductsDimensions.LengthMin)]
        public string Name { get; set; } = string.Empty;

        [Range(0d, double.MaxValue)]
        public decimal Price { get; set; }
    }
}

