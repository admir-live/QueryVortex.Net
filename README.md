QueryVortex.Net
===============

QueryVortex.Net is a robust .NET library designed to transform complex API queries into actionable backend logic. This powerful tool converts URL parameters, including intricate filters, sorting instructions, and field selectors, into executable commands.

Features
--------

*   Parses complex API request queries
*   Handles diverse filter operators ('greater than', 'less than', 'equals to') and logical expressions (AND/OR)
*   Supports sorting data in ascending or descending order
*   Facilitates the selection of specific fields from the API responses
*   Unit test coverage > 95%

Getting Started
---------------

### Installation

// TODO: Add installation instructions

### Usage

```csharp
    using QueryVortex.Core.Builders;
    using QueryVortex.Core.Parsers;
    
    var queryString =
        @"?sort=price:desc&fields=name&fields=price&fields=description&fields=category&fields=brand&filters[category][$eq]=Electronics[$AND]filters[brand][$eq]=Samsung[$OR]filters[brand][$eq]=Apple[$AND]filters[price][$gte]=500[$AND]filters[price][$lte]=2000[$AND](filters[condition][$eq]=New[$OR]filters[condition][$eq]=Refurbished)&page=1&limit=20";
    
    var parser = new DefaultSqlQueryBuilder();
    var parserObject = new DefaultQueryVortexParser();
    
    var result = parserObject.Parse(queryString, "products");
    
    var sqlQuery = parser.CreateSqlQuery(result);
    
    Console.WriteLine(sqlQuery.QueryText);
    
```

Example 1: Filtering by Category and Brand with Sorting and Pagination

`?filters[category][$eq]=Electronics[$AND]filters[brand][$eq]=Samsung&sort=price:asc&page=1&limit=10`

This query string filters products where the category is "Electronics" and the brand is "Samsung". It sorts the results by price in ascending order and retrieves the first page with 10 items.

Example 2: Filtering by Price Range and Condition with Field Selection

`?filters[price][$gte]=1000&filters[price][$lte]=2000&filters[condition][$eq]=New&fields=name&fields=price&fields=description`

This query string filters products with prices between 1000 and 2000. It also filters by products that are labeled as "New". The response will only include the name, price, and description fields.

Example 3: Filtering with Multiple Brand Options using the "$OR" operator

`?filters[brand][$eq]=Samsung[$OR]filters[brand][$eq]=Apple[$OR]filters[brand][$eq]=Sony`

This query string filters products where the brand is either "Samsung", "Apple", or "Sony".

Example 4: Filtering by Category and Price Range using the "$AND" operator

`?filters[category][$eq]=Electronics[$AND]filters[price][$gte]=500[$AND]filters[price][$lte]=1000`

This query string filters products in the "Electronics" category with prices between 500 and 1000.

Example 5: Filtering by Name using the "Like" operator

`?filters[name][$like]=iphone`

This query string filters products with names containing "iphone".

Example 6: Filtering by Price using the "Less Than" operator

`?filters[price][$lt]=500`

This query string filters products with prices less than 500.

Example 7: Sorting by Name in Descending Order

`?sort=name:desc`

This query string sorts the products by name in descending order.

Example 8: Selecting Only the Name and Category Fields

`?fields=name&fields=category`

This query string includes only the name and category fields in the response.

Example 9: Pagination - Retrieving the Second Page with 20 Items

`?page=2&limit=20`

This query string retrieves the second page of results with 20 items per page.

Example 10: Combining Filters with Different Operators

`?filters[category][$eq]=Electronics[$AND]filters[brand][$eq]=Samsung[$OR]filters[brand][$eq]=Apple[$AND]filters[price][$gte]=500[$AND]filters[price][$lte]=2000[$AND](filters[condition][$eq]=New[$OR]filters[condition][$eq]=Refurbished)`

This query string combines multiple filters using different operators to filter products in the "Electronics" category, with the brand "Samsung" or "Apple", price between 500 and 2000, and the condition "New" or "Refurbished".

Documentation

Please note that the documentation for QueryVortex.Net is currently a work in progress. We are actively working on providing comprehensive documentation to assist you in using the library effectively.

However, rest assured that QueryVortex.Net has undergone thorough testing, and it boasts an impressive unit test coverage of 99%. We have put significant effort into ensuring the reliability and correctness of the library.

We appreciate your patience as we continue to work on completing the documentation. In the meantime, if you have any questions or need assistance, please feel free to reach out to us:

*   Email: [admir.m@penzle.com](mailto:admir.m@penzle.com)
*   LinkedIn: [Admir Mujkic](https://www.linkedin.com/in/admir-live/)

Contributing
------------

Contributions are welcome! If you have any ideas, improvements, or bug fixes, please open an issue or submit a pull request. We value and appreciate community contributions to make QueryVortex.Net even better.

License
-------

QueryVortex.Net is released under the MIT License.

![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)
