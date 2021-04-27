using System;
using System.Linq;

namespace no.bbc.BCBPSharp
{
    public class BCBPParser
    {
        private static object GetFieldValue(BCBPField field, string value, int? referenceYear)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            var year = referenceYear.HasValue ? referenceYear.Value : DateTime.Now.Year;

            DateTime estimatedDate;
            double difference;

            switch (field.type)
            {
                case "date":
                    {
                        estimatedDate = DateTime.SpecifyKind(new DateTime(year, 1, 1).AddDays(int.Parse(value) - 1), DateTimeKind.Utc);
                        difference = ((DateTime.Now.Year - estimatedDate.Year) * 12) + DateTime.Now.Month - estimatedDate.Month;

                        if (!referenceYear.HasValue)
                        {
                            // Estimate the year for this date.
                            // If the estimated date is too far in the past, add a year.
                            if (difference > 10)
                            {
                                estimatedDate = DateTime.Parse(estimatedDate.AddYears(1).Year + value + "Z");
                                //estimatedDate = parse(format(add(estimatedDate, { years: 1 }), "y") +value + "Z","yDDDX",Date.now());
                            }
                        }

                        return estimatedDate;
                    }
                case "dateWithYear":
                    {
                        var yearLastDigit = value.Substring(0, 1);
                        var dayOfYear = value.Substring(1);

                        var targetYear = year.ToString().Substring(0, year.ToString().Length - 1) + yearLastDigit;
                        estimatedDate = DateTime.SpecifyKind(new DateTime(int.Parse(targetYear), 1, 1).AddDays(int.Parse(dayOfYear) - 1), DateTimeKind.Utc);

                        difference = (estimatedDate.Year - new DateTime(year, 1,1).AddDays(1).Year);

                        if (difference > 2)
                        {
                            estimatedDate = DateTime.SpecifyKind(new DateTime(estimatedDate.Year - 10, 1, 1).AddDays(int.Parse(dayOfYear) - 1), DateTimeKind.Utc);
                        }

                        return estimatedDate;
                    }
                case "boolean":
                    {
                        return (value == "Y");
                    }
                default:
                    return value;
            }
        }

        private static int ParseField(string barcodeString, BCBPOutput output, BCBPField field, int? referenceYear, int legIndex)
        {
            var fieldLength = field.length.HasValue ? field.length.Value : barcodeString.Length;
            var value = barcodeString.Substring(0, fieldLength).Trim();

            if (value != "" && !field.meta)
            {
                if (field.unique)
                {
                    output[field.name] = GetFieldValue(field, value, referenceYear);
                }
                else
                {
                    var leg = output.Legs[legIndex];

                    if (leg == null)
                    {
                        leg = new BCBPLeg();
                        output.Legs.Add(leg);
                    }

                    leg[field.name] = GetFieldValue(field, value, referenceYear);
                }
            }

            barcodeString = barcodeString.Substring(fieldLength);

            if (field.fields != null)
            {
                // This is a field size so get the next X characters where X is the field size value
                var sectionLength = int.Parse(value, System.Globalization.NumberStyles.HexNumber);
                var sectionString = barcodeString.Substring(0, sectionLength);

                var targetFields = field.fields.Where(f => legIndex == 0 || !f.unique);

                foreach (var subField in targetFields)
                {
                    if (sectionString == string.Empty)
                    {
                        break;
                    }

                    var subFieldLength = ParseField(sectionString, output, subField, referenceYear, legIndex);
                    fieldLength += subFieldLength;
                    sectionString = sectionString.Substring(subFieldLength);
                }
            }

            return fieldLength;
        }

        public static BCBPOutput Parse(string barcodeString, int? referenceYear = null)
        {
            var legs = int.Parse(barcodeString.Substring(1, 1));

            var output = new BCBPOutput();

            for (var i = 0; i < legs; i++)
            {
                // Start the leg with an empty object
                output.Legs.Add(new BCBPLeg());

                var fields = BCBPFields.AllFields.Where((f) =>
                {
                    return !f.isSecurityField && (i == 0 || !f.unique);
                }).ToArray();

                foreach (var field in fields)
                {
                    var fieldLength = ParseField(barcodeString, output, field, referenceYear, i);

                    barcodeString = barcodeString.Substring(fieldLength);
                }
            }

            // Security data needs to be decoded last
            if (barcodeString.StartsWith("^"))
            {
                var fields = BCBPFields.AllFields.Where(f => f.isSecurityField);

                foreach (var field in fields)
                {
                    var fieldLength = ParseField(barcodeString, output, field, referenceYear, 0);
                    barcodeString = barcodeString.Substring(fieldLength);
                }
            }

            // Special case for using the issuance year as the source of truth for other dates without a year
            if (!referenceYear.HasValue && output.issuanceDate.HasValue)
            {
                var issuanceYear = output.issuanceDate.Value.ToString("yy");

                foreach (var leg in output.Legs)
                {
                    var originalFlightDate = leg.flightDate.Value.DayOfYear;

                    var estimatedDate = DateTime.SpecifyKind(new DateTime(output.issuanceDate.Value.Year, 1, 1).AddDays(originalFlightDate - 1), DateTimeKind.Utc);

                    if (estimatedDate.CompareTo(leg.flightDate.Value) < 0)
                    {
                        leg.flightDate = estimatedDate;
                    }
                }
            }

            return output;
        }
    }
}
