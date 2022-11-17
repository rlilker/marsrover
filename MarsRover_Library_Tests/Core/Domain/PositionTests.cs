using Xunit;
using MarsRover_Library.Core.Domain;
using MarsRover_Library.Core.Enums;

namespace MarsRover_Library_Tests.Core.Domain;

public class PositionTests
{
    [Fact]
    public void EnsurePositionPropertiesAreSetInConstructor()
    {
        //Arrange
        int positionX = 1;
        int positionY = 1;
        
        //Act
        var position = new Position(positionX,positionY);

        //Assert
        Assert.Equal(positionX, position.X);
        Assert.Equal(positionY, position.Y);
    }

    [Fact]
    public void EnsurePositionToStringReturnsCorrectOutput()
    {
        //Arrange
        int positionX = 5;
        int positionY = 6;
        
        //Act
        var positionCoordinates = new Position(positionX,positionY).ToString();

        //Assert
        Assert.Equal(positionCoordinates, $"({positionX},{positionY})");
    }
}