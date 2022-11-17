
using Microsoft.AspNetCore.Http;

namespace MarsRover_Library.Extensions;

public static class IFormFileExtensions
{
    public static async Task<List<string>> ReadAsListAsync(this IFormFile file)
    {
        var result = new List<String>();
        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            while (reader.Peek() >= 0) {
                string? line = await reader.ReadLineAsync();
                if (!string.IsNullOrWhiteSpace(line)) {
                    result.Add(line); 
                }
            }
        }
        return result;
    }
}