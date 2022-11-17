using Xunit;
using MarsRover_Library.UseCases.GetMovements;
using Microsoft.AspNetCore.Http;
using MarsRover_Library.Core.Domain;
using System.Threading.Tasks;

namespace MarsRover_Library_Tests.UseCases.MovementsFile;

public class MovementsCSVValidatorTests
{

    [Fact]
    public async Task EnsureFailureOnParsingOfInvalidFilename()
    {
        //Arrange
        
        //create FormFile with desired data
        IFormFile file = MovementsCSVHelper.GenerateFormFile("incorrect.csv", "1 2 N|LMLMLMLMM\n3 3 E|MMRMMRMRRM\n");

        var movementsCsvValidator = new MovementsCSVValidator();

        var expected = new Result<IFormFile>(file, false, string.Format("A file with the name movements.csv is expected\n"));

        //Act
        var result = await movementsCsvValidator.Validate(file);

        //Assert
        Assert.Equal(expected.Success, result.Success);
        Assert.Equal(expected.Message, result.Message);
    }

    [Fact]
    public async Task EnsureFailureOnParsingOfInvalidInput()
    {
        //Arrange

        //create FormFile with desired data
        IFormFile file = MovementsCSVHelper.GenerateFormFile("movements.csv", "1 2 N|LMLMLMLMM\nEnd of the file");
        var movementsCsvValidator = new MovementsCSVValidator();

        string message = "Incorrect Rover instructions on line 2. \"End of the file\" Please correct this and try again.\n";
        var expected = new Result<IFormFile>(file, false, message);

        //Act
        var result = await movementsCsvValidator.Validate(file);

        //Assert
        Assert.Equal(expected.Success, result.Success);
        Assert.Equal(expected.Message, result.Message);
    }

    [Fact]
    public async Task EnsureValidInputReturnsSuccessResult()
    {
        //Arrange
        
        //create FormFile with desired data
        IFormFile file = MovementsCSVHelper.GenerateFormFile("movements.csv", "1 2 N|LMLMLMLMM\n3 3 E|MMRMMRMRRM\n");

        var movementsCsvValidator = new MovementsCSVValidator();

        var expected = new Result<IFormFile>(file, true);

        //Act
        var result = await movementsCsvValidator.Validate(file);

        //Assert
        Assert.Equal(expected.Success, result.Success);
    }
}