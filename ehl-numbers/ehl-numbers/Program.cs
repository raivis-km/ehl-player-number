// See https://aka.ms/new-console-template for more information
using ehl_numbers;

Console.WriteLine("ehl-numbers");

var teamUrls = Utilities.GetTeamUrls();
var tmp = new List<Number>();
var playerNumbers = new List<Number>();

foreach (var teamUrl in teamUrls)
{

    Console.WriteLine(teamUrl);
    
    var numbers = Utilities.GetTeamNumbers(teamUrl);

    tmp.AddRange(numbers);
}

foreach (var player in tmp)
{
    if (!playerNumbers.Where(x => x.ToString() == player.ToString()).Any())
        playerNumbers.Add(player);
}

var x = playerNumbers.Distinct()
    .GroupBy(x => x.number)
    .Select(group => new {key = group.Key, value = group.Count() })
    .OrderByDescending(m => m.value);

Console.ReadLine();


