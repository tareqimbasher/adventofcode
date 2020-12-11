using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var boardingPasses = File.ReadAllLines("./boarding-passes.txt");
var seatIds = new List<int>();

foreach (var pass in boardingPasses)
{
    var row = Decode(pass[..7], 'F', 0, 127);
    var col = Decode(pass[7..], 'L', 0, 7);

    int seatId = (row * 8) + col;
    seatIds.Add(seatId);
}

Puzzle1();
Puzzle2();

void Puzzle1()
{
    Console.WriteLine($"Highest seat ID: {seatIds.Max()}");
}

void Puzzle2()
{
    Console.WriteLine($"My seat ID: {Enumerable.Range(100, seatIds.Max() - 100).Except(seatIds).Single()}");
}

int Decode(string instructions, char lowerHalfIdentifier, int min, int max)
{
    foreach (var c in instructions)
    {
        int numberOfItems = ((max + 1 - min) / 2) - 1;

        if (c == lowerHalfIdentifier) // Lower half
        {
            max = min + numberOfItems;
        }
        else // Upper half
        {
            min += numberOfItems + 1;
        }
    }
    return max;
}