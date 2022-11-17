using Xunit;
using MarsRover_Library.Core.Domain;
using MarsRover_Library.Core.Commands;
using MarsRover_Library.Core.Enums;
using System.Collections.Generic;
using Moq;

namespace MarsRover_Library_Tests.Core.Domain;

public class RoverCommandTests
{
    [Fact]
    public void EnsureCommandsArePassedToThePlateauForExecution()
    {
        //Arrange
        var rover = new Rover("Rover 1") {
            Orientation = Orientation.N,
            Position = new Position(1,2)
        };
        var directions = new List<Direction> {
            Direction.L,
            Direction.M,
            Direction.L,
            Direction.M,
            Direction.L,
            Direction.M,
            Direction.L,
            Direction.M,
            Direction.M
        };

        var plateau = new Mock<IPlateau>();
        
        plateau
            .Setup(p => p.MoveRover(It.IsAny<IRover>(), It.IsAny<IList<Direction>>()))
            .Returns(new List<Result<IRover>>());

        var roverCommand = new RoverCommand(plateau.Object, 
                rover,
                directions);
        

        //Act
        roverCommand.Execute();

        //Assert
        plateau.Verify(s => s.MoveRover(rover, directions), Times.Once());
    }
}