using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var expenses = File.ReadAllLines("./expense-report.txt").Select(l => Convert.ToInt32(l.Trim())).ToList();

Puzzle1();
Puzzle2();

void Puzzle1()
{
	foreach (var expense1 in expenses)
	{
		foreach (var expense2 in expenses.Where(e => e != expense1))
		{
			if (expense1 + expense2 == 2020)
			{
				Console.WriteLine($"Puzzle 1 answer: {expense1 * expense2}");
				return;
			}
		}
	}
}

// Puzzle 2
void Puzzle2()
{
	foreach (var expense1 in expenses)
	{
		foreach (var expense2 in expenses.Where(e => e != expense1))
		{
			foreach (var expense3 in expenses.Where(e => e != expense1 && e != expense2))
			{
				if (expense1 + expense2 + expense3 == 2020)
				{
					Console.WriteLine($"Puzzle 2 answer: {expense1 * expense2 * expense3}");
					return;
				}
			}
		}
	}
}
