using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

var passports = File.ReadAllText("./passports.txt")
    .Split(Environment.NewLine + Environment.NewLine)
    .Select(data => new Passport(data))
    .ToArray();

Puzzle1();
Puzzle2();

void Puzzle1()
{
    Console.WriteLine($"Passports with required fields: {passports.Count(p => p.IsValid(checkValueValidity: false))}");
}

void Puzzle2()
{
    Console.WriteLine($"Passports with required and valid fields: {passports.Count(p => p.IsValid(checkValueValidity: true))}");
}

class Passport
{
    static string[] _requiredFields = new[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };

    public Passport(string data)
    {
        Fields = data.Replace(Environment.NewLine, " ")
            .Split(' ')
            .Select(s => s.Split(':'))
            .ToDictionary(k => k[0], v => v[1]);
    }

    public Dictionary<string, string> Fields { get; set; }

    public bool IsValid(bool checkValueValidity)
    {
        if (!_requiredFields.All(x => Fields.ContainsKey(x)))
            return false;

        if (!checkValueValidity) return true;

        foreach (var field in Fields)
        {
            string key = field.Key;
            string val = field.Value;

            bool valid = key switch
            {
                "byr" => val.Length == 4 && IsBetween(Convert.ToInt32(val), 1920, 2002),
                "iyr" => val.Length == 4 && IsBetween(Convert.ToInt32(val), 2010, 2020),
                "eyr" => val.Length == 4 && IsBetween(Convert.ToInt32(val), 2020, 2030),
                "hgt" => (val.Contains("cm") && (IsBetween(Convert.ToInt32(val.Replace("cm", "")), 150, 193))) ||
                         (val.Contains("in") && (IsBetween(Convert.ToInt32(val.Replace("in", "")), 59, 76))),
                "hcl" => new Regex("^#[a-zA-Z0-9]{6}$").IsMatch(val),
                "ecl" => new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }.Contains(val),
                "pid" => new Regex("^[0-9]{9}$").IsMatch(val),
                "cid" => true,
                _ => throw new Exception($"Unknown field: {key}")
            };

            if (!valid) return false;
        }

        return true;
    }

    private bool IsBetween(int target, int min, int max) => target >= min && target <= max;
}