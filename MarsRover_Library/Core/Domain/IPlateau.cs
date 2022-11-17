using MarsRover_Library.Core.Enums;

namespace MarsRover_Library.Core.Domain;

public interface IPlateau
{
    public int Height { get; }
    public int Width { get; }

    public IList<IRover> Rovers { get; }

    IList<Result<IRover>> MoveRover(IRover rover, IList<Direction> directions);
}
