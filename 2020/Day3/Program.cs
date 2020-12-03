using System;
using System.IO;

string[] map = File.ReadAllLines("./map.txt");
const char tree = '#';

Puzzle1();
Puzzle2();

void Puzzle1()
{
    Console.WriteLine($"Encountered {CountEncounteredTrees(3, 1)} trees");
}

void Puzzle2()
{
    long product = 1;
    foreach (var slope in new[] { (1, 1), (3, 1), (5, 1), (7, 1), (1, 2) })
    {
        product *= CountEncounteredTrees(slope.Item1, slope.Item2);
    }
    Console.WriteLine($"Product of encountered trees for each slope: {product}");
}

long CountEncounteredTrees(int rightShift, int downShift)
{
    long treesEncountered = 0;

    int x = 0;
    int y = 0;

    for (int i = 0; i < map.Length; i++)
    {
        x += rightShift;
        y += downShift;

        if (y >= map.Length) break;

        treesEncountered += GetCoordinate(x, y) == tree ? 1 : 0;
    }

    return treesEncountered;
}

char GetCoordinate(int x, int y)
{
    var line = map[y];

    while ((line.Length - 1) < x)
        line += map[y];

    return line[x];
}