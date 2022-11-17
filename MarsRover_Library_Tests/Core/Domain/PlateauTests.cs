using Xunit;
using MarsRover_Library.Core.Domain;
using MarsRover_Library.Core.Enums;

namespace MarsRover_Library_Tests.Core.Domain;

public class PlateauTests
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
}