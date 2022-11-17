using Xunit;
using MarsRover_Library.Core.Commands;
using MarsRover_Library.UseCases.GetMovements;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using MarsRover_Library.Core.Domain;
using MarsRover_Library.Core.Enums;
using System.Threading.Tasks;

namespace MarsRover_Library_Tests.UseCases;

public class MovementsCSVParserTests
{
    [Fact]
    public async Task EnsureParsingOfInputIntoExpectedOutput()
    {
        //Arrange
        Plateau plateau = new Plateau();
        //create FormFile with desired data
        IFormFile file = MovementsCSVHelper.GenerateFormFile("movements.csv", "1 2 N|LMLMLMLMM\n3 3 E|MMRMMRMRRM\n");
        
        var movementsCsvParser = new MovementsCSVParser(plateau);

        var expected = new List<RoverCommand>()
        {
            new RoverCommand(plateau, 
                new Rover("Rover 1"),
                new List<Direction>()),
            new RoverCommand(plateau,     
                new Rover("Rover 2"),
                new List<Direction>())
        };

        //Act
        var result = await movementsCsvParser.Parse(file);

        //Assert
        Assert.Equal(expected.Count, result.Count);
    }
}