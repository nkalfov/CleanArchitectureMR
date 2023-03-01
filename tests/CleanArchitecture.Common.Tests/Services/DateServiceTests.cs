using CleanArchitecture.Common.Services;
using CleanArchitecture.Common.Services.Contracts;
using Xunit;

namespace CleanArchitecture.Common.Tests.Services;

public class DateServiceTests
{
    private readonly IDateService _dateService;

    public DateServiceTests()
    {
        _dateService = new DateService();
    }

    [Fact(DisplayName = "GetDate Returns Today's Date")]
    public void GetDateReturnsToday()
    {
        // Arrange
        var expected = DateTime.Now.Date;

        // Act
        var actual = _dateService.GetDate();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "GetDate does not Return Tomorrow")]
    public void GetDateDoesNotReturnTomorrow()
    {
        // Arrange
        var tomorrow = DateTime.Now.AddDays(1).Date;

        // Act
        var actual = _dateService.GetDate();

        // Assert
        Assert.NotEqual(tomorrow, actual);
    }
}
