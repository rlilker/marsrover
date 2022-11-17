using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using MarsRover_Library.Extensions;
using MarsRover_Library.Core.Domain;

namespace MarsRover_Library.UseCases.GetMovements;

public class MovementsCSVValidator : IMovementsValidator<IFormFile>
{
    private const string FILENAME = "movements.csv";
    
    public async Task<Result<IFormFile>> Validate(IFormFile movementsFile) {
        
        bool success = true;
        StringBuilder message = new StringBuilder();

        //Expected name is movements.csv, allow case insensitivity
        if (movementsFile.FileName.ToLower() != FILENAME) {
            success = false;
            message.AppendLine($"A file with the name {FILENAME} is expected");
        }

        //Ensure the file isn't empty
        if (movementsFile.Length == 0) {
            success = false;
            message.AppendLine("No Rover instructions found in file");
        } else {
            Regex rgx = new Regex(@"[1-5] [1-5] (N|E|S|W)\|(L|R|M)+", RegexOptions.IgnoreCase);

            int lineNumber = 1;
            var movements = await movementsFile.ReadAsListAsync();

            foreach(string line in movements) {
                MatchCollection matches = rgx.Matches(line);
                if (matches.Count == 0)
                {
                    success = false;
                    message.AppendLine($"Incorrect Rover instructions on line {lineNumber}. \"{line}\" Please correct this and try again.");
                }
                lineNumber++;
            }
        }

        return new Result<IFormFile>(movementsFile, success, message.ToString());
    }
}
