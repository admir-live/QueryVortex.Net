QueryVortex.Net
===============

QueryVortex.Net is a robust .NET library designed to transform complex API queries into actionable backend logic. This powerful tool converts URL parameters, including intricate filters, sorting instructions, and field selectors, into executable commands.

Features
--------

*   Parses complex API request queries
*   Handles diverse filter operators ('greater than', 'less than', 'equals to') and logical expressions (AND/OR)
*   Supports sorting data in ascending or descending order
*   Facilitates the selection of specific fields from the API responses
*   Unit test coverage of 99%

Getting Started
---------------

### Installation

// TODO: Add installation instructions

### Usage

    using QueryVortex.Core.Builders;
    using QueryVortex.Core.Parsers;
    
    var queryString =
        @"?sort=price:desc&fields=name&fields=price&fields=description&fields=category&fields=brand&filters[category][$eq]=Electronics[$AND]filters[brand][$eq]=Samsung[$OR]filters[brand][$eq]=Apple[$AND]filters[price][$gte]=500[$AND]filters[price][$lte]=2000[$AND](filters[condition][$eq]=New[$OR]filters[condition][$eq]=Refurbished)&page=1&limit=20";
    
    var parser = new DefaultSqlQueryBuilder();
    var parserObject = new DefaultQueryVortexParser();
    
    var result = parserObject.Parse(queryString, "products");
    
    var sqlQuery = parser.CreateSqlQuery(result);
    
    Console.WriteLine(sqlQuery.QueryText);
    

Documentation
-------------

Please note that the documentation for QueryVortex.Net is currently a work in progress. We are actively working on providing comprehensive documentation to assist you in using the library effectively.

However, rest assured that QueryVortex.Net has undergone thorough testing, and it boasts an impressive unit test coverage of 99%. We have put significant effort into ensuring the reliability and correctness of the library.

We appreciate your patience as we continue to work on completing the documentation. In the meantime, if you have any questions or need assistance, please feel free to reach out to us:

*   Email: [admir.m@penzle.com](mailto:admir.m@penzle.com)
*   LinkedIn: [Admir Live](https://www.linkedin.com/in/admir-live/)

Contributing
------------

Contributions are welcome! If you have any ideas, improvements, or bug fixes, please open an issue or submit a pull request. We value and appreciate community contributions to make QueryVortex.Net even better.

License
-------

QueryVortex.Net is released under the MIT License.

\[!\[License: MIT\](https://img.shields.io/badge/License-MIT-yellow.svg)\](https://opensource.org/licenses/MIT)
