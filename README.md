QueryVortex.Net
===============

QueryVortex.Net is a robust .NET library designed to transform complex API queries into actionable backend logic. This powerful tool converts URL parameters, including intricate filters, sorting instructions, and field selectors, into executable commands.

[![codecov](https://codecov.io/gh/admir-live/QueryVortex.Net/branch/develop/graph/badge.svg?token=XXZ9G84BMK)](https://codecov.io/gh/admir-live/QueryVortex.Net)
![example workflow](https://github.com/admir-live/QueryVortex.Net/actions/workflows/dotnet.yml/badge.svg?branch=develop)


Features
--------

**Parsing of Complex URL Query Parameters**: Designed to convert URL parameters, including filters, sorting rules, and field selectors, into executable backend logic.

**Handling Diverse Filter Operators**: Supports various operators including:

| Operator     | Description                       |
|--------------|-----------------------------------|
| `$eq`        | Equals to                         |
| `$ne`        | Not equals to                     |
| `$gte`       | Greater than or equals to         |
| `$lte`       | Less than or equals to            |
| `$gt`        | Greater than                      |
| `$lt`        | Less than                         |
| `$like`      | Pattern match                     |
| `$in`        | In operator                       |
| `$nin`       | Not in operator                   |
| `$nlike`     | Not like operator                 |
| `$startswith`| Starts with operator              |
| `$endswith`  | Ends with operator                |


**Field Selection**: With the `fields` parameter, you can specify which fields to include in the response.

**Sorting**: You can sort data in ascending or descending order using the `sort` parameter. The default sort order is ascending.

**Pagination**: The `page` and `limit` parameters allow you to implement pagination in your API. You can specify the number of items per page and which page of results to retrieve.

**Logical Expressions**: Supports logical nested expressions like AND/OR.

**Test Coverage**: The unit test coverage is more than **95%**.

Additional capabilities include parsing complex API request queries and facilitating the selection of specific fields from the API responses, handling filters with different operators and logical expressions, and streamlining API interactions for efficient response management.

**Note**: The specific operators and logical expressions are used as part of the URL parameters in your API queries. For more specific details or advanced use cases, you may need to consult the official documentation or contact the maintainers of the QueryVortex.Net package directly.


Getting Started
---------------

### Installation

There are several ways you can install the `QueryVortex.Net` package:

**Using .NET CLI**:
    Run the following command in your terminal:
    
```
dotnet add package QueryVortex.Net
 ```

**Using NuGet Package Manager in Visual Studio**:
Run the following command in the Package Manager Console:
 ```
Install-Package QueryVortex.Net
 ```

**Using PackageReference**:
For projects that support PackageReference, copy the following XML node into your project file to reference the package:
 ```
 <PackageReference Include="QueryVortex.Net" />
 ```

**Using Paket CLI**:
Run the following command in your terminal:
 ```
 paket add QueryVortex.Net
 ```

**Using F# Interactive or Polyglot Notebooks**:
    Copy this into the interactive tool or source code of the script to reference the package:
```
 #r "nuget: QueryVortex.Net"
```

**Using Cake**:
    To install QueryVortex.Net as a Cake Addin or Cake Tool, use the following commands respectively:
```
// Install QueryVortex.Net as a Cake Addin
#addin nuget:?package=QueryVortex.Net

// Install QueryVortex.Net as a Cake Tool
#tool nuget:?package=QueryVortex.Net
```

Please choose the method that best suits your development environment and workflow.


### Usage

```csharp
using QueryVortex.Core.Builders;
using QueryVortex.Core.Parsers;
using System;

class Program
{
    static void Main()
    {
        var queryString =
            @"?sort=price:desc&fields=name&fields=price&fields=description&fields=category&fields=brand&filters[category][$eq]=Electronics[$AND]filters[brand][$eq]=Samsung[$OR]filters[brand][$eq]=Apple[$AND]filters[price][$gte]=500[$AND]filters[price][$lte]=2000[$AND](filters[condition][$eq]=New[$OR]filters[condition][$eq]=Refurbished)&page=1&limit=20";

        var parser = new DefaultSqlQueryBuilder();
        var parserObject = new DefaultQueryVortexParser();

        var result = parserObject.Parse(queryString, "products");

        var sqlQuery = parser.CreateSqlQuery(result);

        Console.WriteLine(sqlQuery.QueryText);
    }
}
```

#### Output

```tsql
SELECT [name], [price], [description], [category], [brand]
FROM [products]
WHERE [category] = 'Electronics'
  AND ([brand] = 'Samsung' OR [brand] = 'Apple')
  AND [price] >= 500 AND [price] <= 2000
  AND ([condition] = 'New' OR [condition] = 'Refurbished')
ORDER BY [price] DESC
OFFSET 0 ROWS FETCH NEXT 20 ROWS ONLY;
```

**Example 1: Filtering by Category and Brand with Sorting and Pagination**

```
?filters[category][$eq]=Electronics[$AND]filters[brand][$eq]=Samsung&sort=price:asc&page=1&limit=10
```

This query string filters products where the category is "Electronics" and the brand is "Samsung". It sorts the results by price in ascending order and retrieves the first page with 10 items.

**Example 2: Filtering by Price Range and Condition with Field Selection**

```
?filters[price][$gte]=1000&filters[price][$lte]=2000&filters[condition][$eq]=New&fields=name&fields=price&fields=description
```

This query string filters products with prices between 1000 and 2000. It also filters by products that are labeled as "New". The response will only include the name, price, and description fields.

**Example 3: Filtering with Multiple Brand Options using the "$OR" operator**

```
?filters[brand][$eq]=Samsung[$OR]filters[brand][$eq]=Apple[$OR]filters[brand][$eq]=Sony
```

This query string filters products where the brand is either "Samsung", "Apple", or "Sony".

**Example 4: Filtering by Category and Price Range using the "$AND" operator**

```
?filters[category][$eq]=Electronics[$AND]filters[price][$gte]=500[$AND]filters[price][$lte]=1000
```

This query string filters products in the "Electronics" category with prices between 500 and 1000.

**Example 5: Filtering by Name using the "Like" operator**

```
?filters[name][$like]=iphone
```

This query string filters products with names containing "iphone".

**Example 6: Filtering by Price using the "Less Than" operator**

```
?filters[price][$lt]=500
```

This query string filters products with prices less than 500.

**Example 7: Sorting by Name in Descending Order**

```
?sort=name:desc
```

This query string sorts the products by name in descending order.

**Example 8: Selecting Only the Name and Category Fields**

```
?fields=name&fields=category
```

This query string includes only the name and category fields in the response.

**Example 9: Pagination - Retrieving the Second Page with 20 Items**

```
?page=2&limit=20
```

This query string retrieves the second page of results with 20 items per page.

**Example 10: Combining Filters with Different Operators**

```
?filters[category][$eq]=Electronics[$AND]filters[brand][$eq]=Samsung[$OR]filters[brand][$eq]=Apple[$AND]filters[price][$gte]=500[$AND]filters[price][$lte]=2000[$AND](filters[condition][$eq]=New[$OR]filters[condition][$eq]=Refurbished)
```

This query string combines multiple filters using different operators to filter products in the "Electronics" category, with the brand "Samsung" or "Apple", price between 500 and 2000, and the condition "New" or "Refurbished".

## Documentation

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

Roadmap
-------

Our future plans and high-priority features are:

**Extend Library to Support Dependency Injection (DI)**: Our library will be enhanced to seamlessly integrate with dependency injection frameworks. This will enable easier testing, configuration and overall manageability of the services that use QueryVortex.Net.

**Extend Library to Support EF Core**: We are planning to extend the library's functionality to support Entity Framework Core. This will enable the library to leverage the performance, scalability, and flexibility of EF Core for database operations.

**Improve Resilient and Error Handling**: We aim to improve the resilience of the library by implementing advanced error handling and recovery techniques. This will ensure the robustness of applications built using QueryVortex.Net.

**Implement Ability to Support NoSQL**: The library will be extended to support NoSQL databases. This will provide developers with more flexibility in choosing the right database technology for their application's specific needs.

**Implement More Operators**: We plan to expand the library's operator set, providing developers with more options for creating complex queries. This will enhance the flexibility and expressiveness of the API interactions facilitated by QueryVortex.Net.

Please note that this roadmap may change based on user feedback and other factors. We welcome contributions and suggestions from the community!


Attention Engineers
-------

**Please note that the work on this library is currently still in progress and is not yet ready for production use. Please exercise caution and refrain from deploying or integrating it into live environments. Your understanding is appreciated.**
