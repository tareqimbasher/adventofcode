using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var bags = File.ReadAllLines("./bag-rules.txt")
    .Select(r => new Bag(r))
    .ToArray();

Puzzle1();
Puzzle2();

void Puzzle1()
{
    Console.WriteLine(GetBagsThatCanContainColor("shiny gold").Count());
}

void Puzzle2()
{
    Console.WriteLine(CountTotalContainedBags("shiny gold"));
}

IEnumerable<Bag> GetBagsThatCanContainColor(string color)
{
    var bagsContaingColor = bags.Where(x => x.ContainedBags.Any(cb => cb.color == color)).ToList();
    foreach (var bag in bagsContaingColor.ToArray())
    {
        bagsContaingColor.AddRange(GetBagsThatCanContainColor(bag.Color));
    }
    return bagsContaingColor.GroupBy(b => b.Color).Select(x => x.First());
}

int CountTotalContainedBags(string color)
{
    var bag = bags.Single(b => b.Color == color);
    int total = bag.ContainedBags.Sum(b => b.count);

    foreach (var containedBag in bag.ContainedBags)
    {
        total += containedBag.count * CountTotalContainedBags(containedBag.color);
    }

    return total;
}

class Bag
{
    public Bag(string rule)
    {
        ContainedBags = new List<(string color, int count)>();

        var parts = rule.Split(" ");
        Color = string.Join(" ", parts.Take(2));

        var otherBags = rule.Split("contain")[1].Split(",").Select(x => x.Trim());
        foreach (string otherBag in otherBags)
        {
            if (otherBag.StartsWith("no"))
                break;
            else
            {
                var otherBagParts = otherBag.Split(" ");
                string color = string.Join(" ", otherBagParts.Skip(1).Take(2));
                int count = Convert.ToInt32(otherBagParts.First());
                ContainedBags.Add((color, count));
            };
        }
    }

    public string Color { get; private set; }
    public List<(string color, int count)> ContainedBags { get; set; }
}