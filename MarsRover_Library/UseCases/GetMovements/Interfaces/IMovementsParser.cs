using MarsRover_Library.Core.Commands;

namespace MarsRover_Library.UseCases.GetMovements;

public interface IMovementsParser<T>
{
    Task<IList<RoverCommand>> Parse(T input);
}
