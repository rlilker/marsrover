using MarsRover_Library.Core.Commands;
using MarsRover_Library.Core.Enums;

namespace MarsRover_Library.Core.Domain;

public class Plateau : IPlateau
{
    private const int HEIGHT = 5;
    private const int WIDTH = 5;

    public int Height { get { return HEIGHT; } }
    public int Width { get { return WIDTH; } }

    public IList<IRover> Rovers { get; private set; } = new List<IRover>();


    public IList<Result<IRover>> MoveRover(IRover rover, IList<Direction> directions) {
        
        IList<Result<IRover>> output = new List<Result<IRover>>();

        string message = string.Empty;
        var isValid = IsPositionValid(rover.Position);

        if (!isValid) {
            message = string.Empty;
        } else {
            var facing = rover.Orientation;
            var positionX = rover.Position.X;
            var positionY = rover.Position.Y;

            message = $"{rover.Name} added to plateau at Position {rover.Position.ToString()}, facing {rover.Orientation.ToString()}";

            output.Add(new Result<IRover>(rover.DeepCopy(), isValid, message));

            foreach(Direction direction in directions) {
                isValid = true;
                switch(direction) {
                    case Direction.L:
                    case Direction.R:
                        rover.Orientation = SetOrientation(rover.Orientation, direction);
                        message = $"{rover.Name} turned to face {rover.Orientation.ToString()}";
                        break;
                    case Direction.M:
                        var newPosition = SetPosition(rover.Position, rover.Orientation);
                        isValid = IsPositionValid(newPosition);
                        if (isValid) {
                            rover.Position = newPosition;
                            message = $"{rover.Name} moved to Position {newPosition.ToString()}";
                        } else {
                            message = $"Cannot move {rover.Name} to Position {newPosition.ToString()}, it will fall off the plateau!";
                        }
                        break;
                }

                output.Add(new Result<IRover>(rover.DeepCopy(), isValid, message));
            }
        }
        return output;
    }

    private Orientation SetOrientation(Orientation orientation, Direction direction) {
        switch(direction) {
            case Direction.L:
                if (orientation == Orientation.N) {
                    orientation = Orientation.W;
                } else {
                    orientation--;
                }
                break;
            case Direction.R:
                if (orientation == Orientation.N) {
                    orientation = Orientation.W;
                } else {
                    orientation++;
                }
                break;
        }
        return orientation;
    }

    private Position SetPosition(Position position, Orientation orientation) {
        switch(orientation) {
            case Orientation.N:
            default:
                return new Position(position.X, position.Y + 1);
            case Orientation.E:
                return new Position(position.X + 1, position.Y);
            case Orientation.S:
                return new Position(position.X, position.Y - 1);
            case Orientation.W:
                return new Position(position.X - 1, position.Y);
        }
    }

    private bool IsPositionValid(Position position) {
        return (position.X > 0 && position.X <= WIDTH
            && position.Y > 0 && position.X <= HEIGHT);
    }
}
