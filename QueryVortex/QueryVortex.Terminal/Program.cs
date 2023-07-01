using System.Text.RegularExpressions;

var queryString =
    @"?sort=price:desc&fields=name&fields=price&fields=description&fields=category&fields=brand&filters[category][$eq]=Electronics[$AND]filters[brand][$eq]=Samsung[$OR]filters[brand][$eq]=Apple[$AND]filters[price][$gte]=500[$AND]filters[price][$lte]=2000[$AND](filters[condition][$eq]=New[$OR]filters[condition][$eq]=Refurbished)&page=1&limit=20";

var combinedPattern = @"page=(?<page>[^&]*)&limit=(?<limit>[^&]*)";

var combinedRegex = new Regex(combinedPattern);
var match = combinedRegex.Match(queryString);

if (match.Success)
{
    var pageNumber = match.Groups["page"].Value;
    var limitNumber = match.Groups["limit"].Value;

    Console.WriteLine($"Page Number: {pageNumber}, Limit Number: {limitNumber}");
}
