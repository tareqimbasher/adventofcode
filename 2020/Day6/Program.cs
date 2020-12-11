using System;
using System.IO;
using System.Linq;

var groups = File.ReadAllText("./declaration-forms.txt")
    .Split(Environment.NewLine + Environment.NewLine);

Puzzle1();
Puzzle2();

void Puzzle1()
{
    int yesAnswers = groups.Sum(g => g.Replace(Environment.NewLine, string.Empty).ToCharArray().Distinct().Count());
    Console.WriteLine($"Yes answers by anyone: {yesAnswers}");
}

void Puzzle2()
{
    int yesAnswers = groups.Sum(group => 
    {
        int numberOfPeopleInGroup = group.Split(Environment.NewLine).Length;
        return group
            .Replace(Environment.NewLine, string.Empty)
            .ToCharArray()
            .GroupBy(c => c)
            .Where(c => c.Count() == numberOfPeopleInGroup)
            .Count();
    });
    Console.WriteLine($"Yes answers by everyone {yesAnswers}");
}