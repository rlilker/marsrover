using Xunit;
using MarsRover_Library.Core.Domain;
using MarsRover_Library.Core.Enums;

namespace MarsRover_Library_Tests.Core.Domain;

public class RoverTests
{
    [Fact]
    public void EnsureRoverPropertiesAreSetAsExpected()
    {
        //Arrange
        int positionX = 1;
        int positionY = 1;
        var position = new Position(positionX,positionY);
        var orientation = Orientation.N;

        //Act
        var rover = new Rover("Rover 1");
        rover.Position = position;
        rover.Orientation = orientation;

        //Assert
        Assert.Equal(positionX, rover.Position.X);
        Assert.Equal(positionY, rover.Position.Y);
        Assert.Equal(orientation, rover.Orientation);
    }
}