using QueryVortex.Core.Builders;
using QueryVortex.Core.Parsers;

var queryString =
    @"?sort=price:desc&fields=name&fields=price&fields=description&fields=category&fields=brand&filters[category][$eq]=Electronics[$AND]filters[brand][$eq]=Samsung[$OR]filters[brand][$eq]=Apple[$AND]filters[price][$gte]=500[$AND]filters[price][$lte]=2000[$AND](filters[condition][$eq]=New[$OR]filters[condition][$eq]=Refurbished)&page=1&limit=20";

var parser = new DefaultSqlQueryBuilder();
var parserObject = new DefaultQueryVortexParser();

var result = parserObject.Parse(queryString, "products");

var sqlQuery = parser.CreateSqlQuery(result);

Console.WriteLine(sqlQuery.QueryText);
