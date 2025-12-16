namespace AdventOfCode.Services;

public static class InputParserService
{
  public static string[] GetInputLines(string day, string filename)
  {
    var projectRootPath = AppContext.BaseDirectory;
    var inputFilePath = Path.Combine(projectRootPath, $"Inputs/day{day}", $"{filename}.txt");

    return File.ReadAllLines(inputFilePath);
  }
}