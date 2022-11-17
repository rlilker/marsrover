using MarsRover_Library.Core.Domain;

namespace MarsRover_Library.Core.Commands;

public abstract class Command
{

    public abstract IList<Result<IRover>> Execute();
}
