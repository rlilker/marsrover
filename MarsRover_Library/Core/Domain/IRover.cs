using MarsRover_Library.Core.Enums;

namespace MarsRover_Library.Core.Domain;

public interface IRover
{
    string Name { get; }
    Position Position { get; set; }
    Orientation Orientation { get; set; }

    Rover DeepCopy();
}
