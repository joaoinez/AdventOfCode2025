using AdventOfCode.Services;

namespace AdventOfCode.Puzzles;

public class Day04
{
  public static readonly string Part1ExpectedValue = "13";
  public static readonly string Part2ExpectedValue = "43";

  private static readonly char PAPER_ROLL = '@';

  private static bool IsPaperRollAccessible((int x, int y) position, char[][] grid)
  {
    var numAdjacentRolls = 0;

    var (x, y) = position;

    // check top left
    if (y - 1 >= 0 && x - 1 >= 0)
    {
      var isPaperRoll = grid[y - 1][x - 1] == PAPER_ROLL;
      if (isPaperRoll)
      {
        numAdjacentRolls++;
        if (numAdjacentRolls >= 4)
          return false;
      }
    }
    // check top
    if (y - 1 >= 0)
    {
      var isPaperRoll = grid[y - 1][x] == PAPER_ROLL;
      if (isPaperRoll)
      {
        numAdjacentRolls++;
        if (numAdjacentRolls >= 4)
          return false;
      }
    }
    // check top right
    if (y - 1 >= 0 && x + 1 < grid[y].Length)
    {
      var isPaperRoll = grid[y - 1][x + 1] == PAPER_ROLL;
      if (isPaperRoll)
      {
        numAdjacentRolls++;
        if (numAdjacentRolls >= 4)
          return false;
      }
    }
    // check right
    if (x + 1 < grid[y].Length)
    {
      var isPaperRoll = grid[y][x + 1] == PAPER_ROLL;
      if (isPaperRoll)
      {
        numAdjacentRolls++;
        if (numAdjacentRolls >= 4)
          return false;
      }
    }
    // check bottom right
    if (y + 1 < grid.Length && x + 1 < grid[y].Length)
    {
      var isPaperRoll = grid[y + 1][x + 1] == PAPER_ROLL;
      if (isPaperRoll)
      {
        numAdjacentRolls++;
        if (numAdjacentRolls >= 4)
          return false;
      }
    }
    // check bottom
    if (y + 1 < grid.Length)
    {
      var isPaperRoll = grid[y + 1][x] == PAPER_ROLL;
      if (isPaperRoll)
      {
        numAdjacentRolls++;
        if (numAdjacentRolls >= 4)
          return false;
      }
    }
    // check bottom left
    if (y + 1 < grid.Length && x - 1 >= 0)
    {
      var isPaperRoll = grid[y + 1][x - 1] == PAPER_ROLL;
      if (isPaperRoll)
      {
        numAdjacentRolls++;
        if (numAdjacentRolls >= 4)
          return false;
      }
    }
    // check left
    if (x - 1 >= 0)
    {
      var isPaperRoll = grid[y][x - 1] == PAPER_ROLL;
      if (isPaperRoll)
      {
        numAdjacentRolls++;
        if (numAdjacentRolls >= 4)
          return false;
      }
    }

    return true;
  }

  public static int Part1(string filename = "example")
  {
    var grid = InputParserService
      .GetInputLines("04", filename)
      .Select(row => row.ToCharArray())
      .ToArray();

    var numAccessiblePaperRolls = 0;

    for (var y = 0; y < grid.Length; y++)
    {
      var row = grid[y];

      for (var x = 0; x < row.Length; x++)
      {
        var curPos = grid[y][x];

        if (curPos == PAPER_ROLL)
        {
          var isAccessible = IsPaperRollAccessible((x, y), grid);

          if (isAccessible)
            numAccessiblePaperRolls++;
        }
      }
    }

    return numAccessiblePaperRolls;
  }

  private static int CheckAndUpdateGrid(char[][] grid, int prevCount)
  {
    var numAccessiblePaperRolls = 0;
    var rollsToBeRemoved = new List<(int x, int y)>();

    for (var y = 0; y < grid.Length; y++)
    {
      var row = grid[y];

      for (var x = 0; x < row.Length; x++)
      {
        var curPos = grid[y][x];

        if (curPos == PAPER_ROLL)
        {
          var isAccessible = IsPaperRollAccessible((x, y), grid);

          if (isAccessible)
          {
            numAccessiblePaperRolls++;
            rollsToBeRemoved.Add((x, y));
          }
        }
      }
    }

    if (numAccessiblePaperRolls == 0)
      return prevCount;

    foreach (var position in rollsToBeRemoved)
    {
      var (x, y) = position;

      grid[y][x] = 'x';
    }

    return CheckAndUpdateGrid(grid, prevCount + numAccessiblePaperRolls);
  }

  public static int Part2(string filename = "example")
  {
    var grid = InputParserService
      .GetInputLines("04", filename)
      .Select(row => row.ToCharArray())
      .ToArray();

    var numAccessiblePaperRolls = CheckAndUpdateGrid(grid, 0);

    return numAccessiblePaperRolls;
  }
}
