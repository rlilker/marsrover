using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using MarsRover_Web.Models;
using MarsRover_Library.UseCases.GetMovements;
using MarsRover_Library.Core.Domain;

namespace MarsRover_Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPlateau _plateau;
    private readonly IMovementsParser<IFormFile> _movementsParser;
    private readonly IMovementsValidator<IFormFile> _movementsValidator;
    
    public HomeController(ILogger<HomeController> logger,
        IMovementsValidator<IFormFile> movementsValidator,
        IMovementsParser<IFormFile> movementsParser,
        IPlateau plateau)
    {
        _logger = logger;
        _plateau = plateau;
        _movementsValidator = movementsValidator;
        _movementsParser = movementsParser;
    }

    public IActionResult Index()
    {
        PlateauViewModel vm = new PlateauViewModel {
            Height = _plateau.Height,
            Width = _plateau.Width
        };

        return View(vm);
    }

    [HttpPost]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> UploadFile(IFormFile formFile)
    {
        var validationResult = await _movementsValidator.Validate(formFile);
        if (validationResult.Success) {
            var roverCommands = await _movementsParser.Parse(formFile);
            List<Result<IRover>> roverCommandResults = new List<Result<IRover>>();
            foreach(var roverCommand in roverCommands) {
                roverCommandResults.AddRange(roverCommand.Execute());
            }
            return Json(new Result<List<Result<IRover>>>(roverCommandResults, true));
        } else {
            //return BadRequest(validationResult);
            return Json(validationResult);
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
