using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System.IO;

namespace MarsRover_Library_Tests.UseCases;

public class MovementsCSVHelper
{
    public static IFormFile GenerateFormFile(string fileName, string content) {
        //Setup mock file using a memory stream
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(content);
        writer.Flush();
        stream.Position = 0;

        //create FormFile with desired data
        return new FormFile(stream, 0, stream.Length, "id_from_form", fileName);
    }
}