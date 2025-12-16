#!/opt/homebrew/bin/dotnet run
#:sdk Microsoft.NET.Sdk

if (args.Length == 0)
{
  Console.WriteLine("Usage: ./Scaffold.cs <day>");
  Console.WriteLine("Example: ./Scaffold.cs 1");
  return;
}

var day = int.Parse(args[0]);
var dayPadded = day.ToString("D2");
var dayClass = $"Day{dayPadded}";

var projectRoot = Directory.GetCurrentDirectory();
var dotNETProjectRoot = Path.Combine(projectRoot, "AdventOfCode");
var puzzleFile = Path.Combine(dotNETProjectRoot, "Puzzles", $"{dayClass}.cs");
var inputDir = Path.Combine(dotNETProjectRoot, "Inputs", $"day{dayPadded}");
var exampleFile = Path.Combine(inputDir, "example.txt");
var inputFile = Path.Combine(inputDir, "input.txt");

// Check if puzzle file already exists
if (File.Exists(puzzleFile))
{
  Console.WriteLine($"Error: {puzzleFile} already exists!");
  return;
}

// Check if input directory already exists
if (Directory.Exists(inputDir))
{
  Console.WriteLine($"Error: {inputDir}/ already exists!");
  return;
}

// Create input directory
Directory.CreateDirectory(inputDir);

// Create empty input files
File.WriteAllText(exampleFile, "");
File.WriteAllText(inputFile, "");

// Create puzzle file with scaffold
var puzzleContent =
  $@"using AdventOfCode.Services;

namespace AdventOfCode.Puzzles;

public class {dayClass}
{{
  public static string[] Part1(string filename = ""example"")
  {{
    var lines = InputParserService.GetInputLines(""{dayPadded}"", filename);

    // TODO: Implement Part 1

    return lines;
  }}

  public static string[] Part2(string filename = ""example"")
  {{
    var lines = InputParserService.GetInputLines(""{dayPadded}"", filename);

    // TODO: Implement Part 2

    return lines;
  }}
}}
";

File.WriteAllText(puzzleFile, puzzleContent);

Console.WriteLine($"Created {puzzleFile}");
Console.WriteLine($"Created {inputDir}/");
Console.WriteLine($"Created {exampleFile}");
Console.WriteLine($"Created {inputFile}");
