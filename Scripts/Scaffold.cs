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
var puzzleFile = Path.Combine(projectRoot, "Puzzles", $"{dayClass}.cs");
var inputDir = Path.Combine(projectRoot, "Inputs", $"day{dayPadded}");
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
  $@"#:sdk Microsoft.NET.Sdk

namespace {dayClass};

public static class InputParser
{{
  public static string[] GetInputLines(string day, string filename)
  {{
    var projectRootPath = Directory.GetCurrentDirectory();
    var inputFilePath = Path.Combine(projectRootPath, $""Inputs/day{{day}}"", $""{{filename}}.txt"");

    return File.ReadAllLines(inputFilePath);
  }}
}}

public class {dayClass}
{{
  private static void Part1(string filename = ""example"")
  {{
    var lines = InputParser.GetInputLines(""{dayPadded}"", filename);

    // TODO: Implement Part 1
  }}

  private static void Part2(string filename = ""example"")
  {{
    var lines = InputParser.GetInputLines(""{dayPadded}"", filename);

    // TODO: Implement Part 2
  }}

  public static void Main()
  {{
    Console.WriteLine(""[[Part 1]]"");
    Part1();
    Console.WriteLine(""Example Answer: "");
    Part1(""input"");
    Console.WriteLine(""Problem Answer: "");

    Console.WriteLine(""\n...---...\n"");

    Console.WriteLine(""[[Part 2]]"");
    Part2();
    Console.WriteLine(""Example Answer: "");
    Part2(""input"");
    Console.WriteLine(""Problem Answer: "");
  }}
}}
";

File.WriteAllText(puzzleFile, puzzleContent);

Console.WriteLine($"Created {puzzleFile}");
Console.WriteLine($"Created {inputDir}/");
Console.WriteLine($"Created {exampleFile}");
Console.WriteLine($"Created {inputFile}");
