using AdventOfCode.Services;

namespace AdventOfCode.Puzzles;

public class Day05
{
  public static int Part1(string filename = "example")
  {
    var lines = InputParserService.GetInputLines("05", filename);

    List<(ulong start, ulong end)> freshRanges = [];
    List<ulong> ingredientIDs = [];

    var reachedBoundary = false;
    foreach (var line in lines)
    {
      if (line == "")
      {
        reachedBoundary = true;
        continue;
      }

      if (!reachedBoundary)
      {
        var range = line.Split("-").Select(ulong.Parse).ToArray();
        freshRanges.Add((range[0], range[1]));
      }
      else
      {
        ingredientIDs.Add(ulong.Parse(line));
      }
    }

    int freshIngredientsCount = 0;

    foreach (var ingredientID in ingredientIDs)
    {
      var isFresh = false;

      foreach (var range in freshRanges)
      {
        var (start, end) = range;

        if (ingredientID >= start && ingredientID <= end)
        {
          isFresh = true;
          break;
        }
      }

      if (isFresh)
        freshIngredientsCount++;
    }

    return freshIngredientsCount;
  }

  public static ulong Part2(string filename = "example")
  {
    var lines = InputParserService.GetInputLines("05", filename);

    List<(ulong start, ulong end)> freshRanges = [];

    foreach (var line in lines)
    {
      if (line == "")
        break;

      var range = line.Split("-").Select(ulong.Parse).ToArray();
      freshRanges.Add((range[0], range[1]));
    }

    List<(ulong start, ulong end)> collapsedRanges = [];
    ulong freshIDCount = 0L;

    foreach (var range in freshRanges)
    {
      var (start, end) = range;

      if (collapsedRanges.Count == 0)
      {
        collapsedRanges.Add(range);
        continue;
      }

      List<(ulong start, ulong end)> overlappingRanges = [];

      var isContained = false;
      for (var i = 0; i < collapsedRanges.Count; i++)
      {
        var collapsedRange = collapsedRanges[i];

        if (
          (start >= collapsedRange.start && start <= collapsedRange.end)
          || (end >= collapsedRange.start && end <= collapsedRange.end)
          || (start < collapsedRange.start && end > collapsedRange.end)
        )
        {
          if (start >= collapsedRange.start && end <= collapsedRange.end)
          {
            isContained = true;
            break;
          }

          overlappingRanges.Add(collapsedRange);
        }
      }

      if (isContained)
        continue;

      if (overlappingRanges.Count == 0)
      {
        collapsedRanges.Add(range);
        continue;
      }

      overlappingRanges.Add(range);

      List<ulong> overlappingStarts = [];
      List<ulong> overlappingEnds = [];

      foreach (var overlappingRange in overlappingRanges)
      {
        var (overlapStart, overlapEnd) = overlappingRange;

        overlappingStarts.Add(overlapStart);
        overlappingEnds.Add(overlapEnd);
      }

      var newRange = (overlappingStarts.Min(), overlappingEnds.Max());

      var filteredRanges = collapsedRanges
        .Where((rangeToBeFiltered) => !overlappingRanges.Contains(rangeToBeFiltered))
        .ToList();
      filteredRanges.Add(newRange);

      collapsedRanges = [.. filteredRanges.Select(x => x)];
    }

    foreach (var collapsedRange in collapsedRanges)
    {
      var (start, end) = collapsedRange;

      freshIDCount += end - start + 1;
    }

    return freshIDCount;
  }
}
