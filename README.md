
# BCBPSharp
A BCBP (bar-coded boarding pass) parser written in C#

[![.NET](https://github.com/BB-Computerteknikk-AS/BCBPSharp/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/BB-Computerteknikk-AS/BCBPSharp/actions/workflows/dotnet.yml)

The package is available on NuGet
https://www.nuget.org/packages/BCBPSharp/ 

## Example
```csharp
var parseResult = BCBPParser.Parse("M1DESMARAIS/LUC       EABC123    FRAAC      226F001A      3B>60B1W 6225BAC 2A   1234567890 1AC AC 1234567890123    20KY^164GIWVC5EH7JNT684FVNJ91W2QA4DVN5J8K4F0L0GEQ3DF5TGBN8709HKT5D3DW3GBHFCVHMY7J5T6HFR41W2QA4DVN5J8K4F0L0GE");

Console.WriteLine(parseResult.passengerName); // "DESMARAIS/LUC"

Console.WriteLine(parseResult.issuanceDate.Value.ToShortDateString()); // (2016, 08, 12);

var firstLeg = parseResult.Legs[0];
Console.WriteLine(firstLeg.seatNumber); // "001A"
```

*Based on https://github.com/georgesmith46/bcbp*
