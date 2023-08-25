using CleanArchitecture.Application.Products.Queries.GetProductList;
using CleanArchitecture.Application.Products.ViewModels;
using CleanArchitecture.Presentation.Controllers.Products;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

#pragma warning disable CS8602 // Dereference of a possibly null reference.

namespace CleanArchitecture.Presentation.Tests.Products
{
    public class ProductsListControllerTests
    {
        [Fact(DisplayName = "Get All Three Products")]
        public void GettingThreeProducts()
        {
            // Arrange
            IList<ProductModel> expected = new List<ProductModel>
            {
                new ProductModel
                {
                    Id = 1,
                    Name = "Doombringer (Champion Sword)",
                    Price = 5999.99m
                },
                new ProductModel
                {
                    Id = 2,
                    Name = "Flamebellow (Balrog Blade)",
                    Price = 7459.55m
                },
                new ProductModel
                {
                    Id = 3,
                    Name = "The Grandfather (Colossus Blade)",
                    Price = 9999.98m
                }
            };

            var getProductsListQuery = new Mock<IGetProductsListQuery>();
            getProductsListQuery
                .Setup(x => x.ExecuteAsync())
                .Returns(Task.FromResult(expected));

            var controller = new ProductsListController(getProductsListQuery.Object);

            // Act
            var actionResult = controller
                .Index()
                .GetAwaiter()
                .GetResult();

            var viewResult = actionResult as ViewResult;

            var actual = viewResult.Model as IList<ProductModel>;

            // Assert
            getProductsListQuery.Verify(
                x => x.ExecuteAsync(),
                Times.Once());

            Assert.IsType<ViewResult>(actionResult);

            Assert.NotNull(actual);
            Assert.NotEmpty(actual);

            Assert.Equal(expected.Count, actual.Count);

            for (int index = 0; index < expected.Count; index++)
            {
                Assert.Equal(expected[index].Id, actual[index].Id);
                Assert.Equal(expected[index].Name, actual[index].Name);
            }
        }
    }
}

#pragma warning restore CS8602 // Dereference of a possibly null reference.