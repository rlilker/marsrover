using MarsRover_Library.Core.Domain;
using MarsRover_Library.Core.Enums;

namespace MarsRover_Library.Core.Commands;

public class RoverCommand : Command
{

    private readonly IPlateau _plateau;
    private readonly IRover _rover;
    private readonly IList<Direction> _directions ;

    public RoverCommand(IPlateau plateau, IRover rover, IList<Direction> directions)
    {
        _plateau = plateau;
        _rover = rover;
        _directions = directions;
    }

    public override IList<Result<IRover>> Execute()
    {
        return _plateau.MoveRover(_rover, _directions);
    }
}
