using Microsoft.AspNetCore.Http;
using MarsRover_Library.Core.Domain;

namespace MarsRover_Library.UseCases.GetMovements;

public interface IMovementsValidator<T>
{
    Task<Result<T>> Validate(T input);
}
