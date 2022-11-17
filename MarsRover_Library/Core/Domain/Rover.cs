using MarsRover_Library.Core.Enums;

namespace MarsRover_Library.Core.Domain;

public class Rover : IRover
{
    public string Name { get; private set; }
    public Position Position { get; set; } = new Position(1,1);
    public Orientation Orientation { get; set; }

    public Rover(string name)
    {
        Name = name;
    }

    public Rover DeepCopy() {
        Rover copy = (Rover)this.MemberwiseClone();
        copy.Position = new Position(this.Position.X, this.Position.Y);
        copy.Orientation = this.Orientation;
        return copy;
    }
}
