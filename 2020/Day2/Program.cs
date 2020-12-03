using System;
using System.IO;
using System.Linq;

var passwordEntries = File.ReadAllLines("./password-db.txt")
    .Select(l => l.Trim())
    .Select(l => l.Split(":").Select(p => p.Trim()).ToArray())
    .Select(data =>
    {
        string policy = data[0];
        var policyParts = policy.Split(' ');
        var minMaxParts = policyParts[0].Split('-');

        return new
        {
            Character = Convert.ToChar(policyParts[1]),
            LeftNumber = Convert.ToInt32(minMaxParts[0]),
            RightNumber = Convert.ToInt32(minMaxParts[1]),
            Password = data[1]
        };
    });

int Interpretation1()
{
    return passwordEntries.Count(entry =>
    {
        int charCount = entry.Password.Count(c => c == entry.Character);
        return charCount >= entry.LeftNumber && charCount <= entry.RightNumber;
    });
}

int Interpretation2()
{
    return passwordEntries.Count(entry =>
    {
        bool index1Valid = entry.Password[entry.LeftNumber - 1] == entry.Character;
        bool index2Valid = entry.Password[entry.RightNumber - 1] == entry.Character;
        return index1Valid ^ index2Valid;
    });
}

Console.WriteLine($"Number of valid passwords (Interpretation 1): {Interpretation1()}");
Console.WriteLine($"Number of valid passwords (Interpretation 2): {Interpretation2()}");