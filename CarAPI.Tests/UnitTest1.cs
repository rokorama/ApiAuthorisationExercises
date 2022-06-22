using AutoFixture.Xunit2;
using CarAPI.Models;
using CarAPI.Services;
using Moq;

namespace CarAPI.Tests;

public class UnitTest1
{
    [Theory, AutoData]
    public void Test1(Car car)
    {
        // Arrange
        var mockRepo = new Mock<CarRepository>();
        
    }
}