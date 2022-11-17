using Microsoft.AspNetCore.Http;
using MarsRover_Library.Core.Commands;
using MarsRover_Library.Extensions;
using MarsRover_Library.Core.Domain;
using MarsRover_Library.Core.Enums;

namespace MarsRover_Library.UseCases.GetMovements;

public class MovementsCSVParser : IMovementsParser<IFormFile>
{
    private const string ROVERNAME = "Rover";

    private readonly IPlateau _plateau;

    public MovementsCSVParser(IPlateau plateau)
    {
        _plateau = plateau;
    }

    public async Task<IList<RoverCommand>> Parse(IFormFile movementsFile) {
        var roverCommands = new List<RoverCommand>();

        int roverCount = 1;
        var movements = await movementsFile.ReadAsListAsync();

        foreach(string line in movements) {
            var roverConfigAndDirections = line.ToUpper().Split('|');
            var roverConfig = roverConfigAndDirections[0].Split(' ');
            var roverDirections = roverConfigAndDirections[1].ToCharArray();

            IRover rover = new Rover($"{ROVERNAME} {roverCount}");
            rover.Orientation = GetOrientation(roverConfig);
            rover.Position = GetPosition(roverConfig);

            IList<Direction> directions = GetDirections(roverDirections);

            RoverCommand roverCommand = new RoverCommand(_plateau, rover, directions);
            roverCommands.Add(roverCommand);

            roverCount++;
        }

        return roverCommands;
    }

    private Position GetPosition(string[] roverConfig) {
        return new Position(int.Parse(roverConfig[0]), int.Parse(roverConfig[1]));
    }

    private Orientation GetOrientation(string[] roverConfig) {
        switch(roverConfig[2]) {
            case "N":
            default:
                return Orientation.N;
            case "E":
                return Orientation.E;
            case "S":
                return Orientation.S;
            case "W":
                return Orientation.W;
        }
    }

    private IList<Direction> GetDirections(char[] roverDirections) {
        IList<Direction> directions = new List<Direction>();
        foreach(var roverDirection in roverDirections) {
            switch(roverDirection) {
                case 'M':
                    directions.Add(Direction.M);
                    break;
                case 'L':
                    directions.Add(Direction.L);
                    break;
                case 'R':
                    directions.Add(Direction.R);
                    break;
            }
        }

        return directions;
    }
}
