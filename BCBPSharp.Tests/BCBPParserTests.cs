using System;
using Xunit;

namespace no.bbc.BCBPSharp.Tests
{
    /// <summary>
    /// NOTE: User-manual https://xunit.net/docs/getting-started/netcore/cmdline
    /// </summary>
    public class BCBPParserTests
    {
        [Fact(DisplayName = "Basic BCBP Parsing")]
        public void Basic()
        {
            var parseResult = BCBPParser.Parse("M1DESMARAIS/LUC       EABC123 YULFRAAC 0834 226F001A0025 106>60000");

            var expectedFlightDate = DateTime.SpecifyKind(new DateTime(DateTime.Now.Year, 1, 1).AddDays(226 - 1), DateTimeKind.Utc);

            Assert.NotNull(parseResult.Legs);

            Assert.True(parseResult.Legs.Count == 1);

            Assert.Equal("DESMARAIS/LUC", parseResult.passengerName);

            var firstLeg = parseResult.Legs[0];

            Assert.Equal("ABC123", firstLeg.operatingCarrierPNR);
            Assert.Equal("YUL", firstLeg.departureAirport);
            Assert.Equal("FRA", firstLeg.arrivalAirport);
            Assert.Equal("AC", firstLeg.operatingCarrierDesignator);
            Assert.Equal("0834", firstLeg.flightNumber);
            Assert.Equal(expectedFlightDate, firstLeg.flightDate);
            Assert.Equal("F", firstLeg.compartmentCode);
            Assert.Equal("001A", firstLeg.seatNumber);
            Assert.Equal("0025", firstLeg.checkInSequenceNumber);
            Assert.Equal("1", firstLeg.passengerStatus);
        }

        [Fact(DisplayName = "Basic BCBP Parsing 2")]
        public void Basic2()
        {
            var parseResult = BCBPParser.Parse("M1DOE/JOHN            EXYZ123 ZRHSFOBA 1234 099F035A0001 100");

            var expectedFlightDate = new DateTime(DateTime.Now.Year, 04, 09);

            Assert.Equal("DOE/JOHN", parseResult.passengerName);

            Assert.NotNull(parseResult.Legs);

            Assert.True(parseResult.Legs.Count == 1);

            var firstLeg = parseResult.Legs[0];

            Assert.Equal("XYZ123", firstLeg.operatingCarrierPNR);
            Assert.Equal("SFO", firstLeg.arrivalAirport);
            Assert.Equal("ZRH", firstLeg.departureAirport);
            Assert.Equal("BA", firstLeg.operatingCarrierDesignator);
            Assert.Equal("1234", firstLeg.flightNumber);
            Assert.Equal("0001", firstLeg.checkInSequenceNumber);
            Assert.Equal("XYZ123", firstLeg.operatingCarrierPNR);
            Assert.Equal("1", firstLeg.passengerStatus);
            Assert.Equal(expectedFlightDate.ToShortDateString(), firstLeg.flightDate.Value.ToShortDateString());
            Assert.Equal("F", firstLeg.compartmentCode);
            Assert.Equal("035A", firstLeg.seatNumber);
        }

        [Fact(DisplayName = "Complex BCBP Parsing")]
        public void Complex()
        {
            var parseResult = BCBPParser.Parse("M1DESMARAIS/LUC       EABC123    FRAAC      226F001A      3B>60B1W 6225BAC 2A   1234567890 1AC AC 1234567890123    20KY^164GIWVC5EH7JNT684FVNJ91W2QA4DVN5J8K4F0L0GEQ3DF5TGBN8709HKT5D3DW3GBHFCVHMY7J5T6HFR41W2QA4DVN5J8K4F0L0GE");

            var expectedIssuanceDate = new DateTime(2016, 08, 12);
            var expectedFlightDate = new DateTime(2016, 08, 13);

            Assert.NotNull(parseResult.Legs);

            Assert.True(parseResult.Legs.Count == 1);

            Assert.Equal("DESMARAIS/LUC", parseResult.passengerName);
            Assert.Equal("1", parseResult.passengerDescription);
            Assert.Equal("W", parseResult.checkInSource);
            Assert.Equal(expectedIssuanceDate.ToShortDateString(), parseResult.issuanceDate.Value.ToShortDateString());
            Assert.Equal("B", parseResult.documentType);
            Assert.Equal("AC", parseResult.boardingPassIssuerDesignator);
            Assert.Equal("1", parseResult.securityDataType);
            Assert.Equal("GIWVC5EH7JNT684FVNJ91W2QA4DVN5J8K4F0L0GEQ3DF5TGBN8709HKT5D3DW3GBHFCVHMY7J5T6HFR41W2QA4DVN5J8K4F0L0GE", parseResult.securityData);

            var firstLeg = parseResult.Legs[0];

            Assert.Equal("ABC123", firstLeg.operatingCarrierPNR);
            Assert.Equal("FRA", firstLeg.arrivalAirport);
            Assert.Equal("AC", firstLeg.operatingCarrierDesignator);
            Assert.Equal(expectedFlightDate.ToShortDateString(), firstLeg.flightDate.Value.ToShortDateString());
            Assert.Equal("F", firstLeg.compartmentCode);
            Assert.Equal("001A", firstLeg.seatNumber);
            Assert.Equal("1", firstLeg.internationalDocumentationVerification);
            Assert.Equal("AC", firstLeg.marketingCarrierDesignator);
            Assert.Equal("AC", firstLeg.frequentFlyerAirlineDesignator);
            Assert.Equal("1234567890123", firstLeg.frequentFlyerNumber);
            Assert.Equal("20K", firstLeg.freeBaggageAllowance);
            Assert.True(firstLeg.fastTrack);
        }

        [Fact(DisplayName = "Full BCBP Parsing")]
        public void Full()
        {
            var parseResult = BCBPParser.Parse("M2DESMARAIS/LUC       EABC123 YULFRAAC 0834 226F001A0025 14D>6181WW6225BAC 00141234560032A0141234567890 1AC AC 1234567890123    20KYLX58ZDEF456 FRAGVALH 3664 227C012C0002 12E2A0140987654321 1AC AC 1234567890123    2PCNWQ^164GIWVC5EH7JNT684FVNJ91W2QA4DVN5J8K4F0L0GEQ3DF5TGBN8709HKT5D3DW3GBHFCVHMY7J5T6HFR41W2QA4DVN5J8K4F0L0GE");

            var expectedIssuanceDate = new DateTime(2016, 08, 12);
            var expectedFlightDate = new DateTime(2016, 08, 13);
            var expectedFlightDate2 = new DateTime(2016, 08, 14);

            Assert.NotNull(parseResult.Legs);

            Assert.True(parseResult.Legs.Count == 2);

            var firstLeg = parseResult.Legs[0];
            var secondLeg = parseResult.Legs[1];

            #region First Leg

            Assert.Equal("ABC123", firstLeg.operatingCarrierPNR);
            Assert.Equal("YUL", firstLeg.departureAirport);
            Assert.Equal("FRA", firstLeg.arrivalAirport);
            Assert.Equal("AC", firstLeg.operatingCarrierDesignator);
            Assert.Equal("0834", firstLeg.flightNumber);
            Assert.Equal(expectedFlightDate, firstLeg.flightDate);
            Assert.Equal("F", firstLeg.compartmentCode);
            Assert.Equal("001A", firstLeg.seatNumber);
            Assert.Equal("0025", firstLeg.checkInSequenceNumber);
            Assert.Equal("1", firstLeg.passengerStatus);
            Assert.Equal("014", firstLeg.airlineNumericCode);
            Assert.Equal("1234567890", firstLeg.serialNumber);
            Assert.Equal("1", firstLeg.internationalDocumentationVerification);
            Assert.Equal("AC", firstLeg.marketingCarrierDesignator);
            Assert.Equal("AC", firstLeg.frequentFlyerAirlineDesignator);
            Assert.Equal("1234567890123", firstLeg.frequentFlyerNumber);
            Assert.Equal("20K", firstLeg.freeBaggageAllowance);
            Assert.True(firstLeg.fastTrack);
            Assert.Equal("LX58Z", firstLeg.airlineInfo);

            #endregion

            #region Second Leg

            Assert.Equal("DEF456", secondLeg.operatingCarrierPNR);
            Assert.Equal("FRA", secondLeg.departureAirport);
            Assert.Equal("GVA", secondLeg.arrivalAirport);
            Assert.Equal("LH", secondLeg.operatingCarrierDesignator);
            Assert.Equal("3664", secondLeg.flightNumber);
            Assert.Equal(expectedFlightDate2, secondLeg.flightDate);
            Assert.Equal("C", secondLeg.compartmentCode);
            Assert.Equal("012C", secondLeg.seatNumber);
            Assert.Equal("0002", secondLeg.checkInSequenceNumber);
            Assert.Equal("1", secondLeg.passengerStatus);
            Assert.Equal("014", secondLeg.airlineNumericCode);
            Assert.Equal("0987654321", secondLeg.serialNumber);
            Assert.Equal("1", secondLeg.internationalDocumentationVerification);
            Assert.Equal("AC", secondLeg.marketingCarrierDesignator);
            Assert.Equal("AC", secondLeg.frequentFlyerAirlineDesignator);
            Assert.Equal("1234567890123", secondLeg.frequentFlyerNumber);
            Assert.Equal("2PC", secondLeg.freeBaggageAllowance);
            Assert.False(secondLeg.fastTrack);
            Assert.Equal("WQ", secondLeg.airlineInfo);

            #endregion

            Assert.Equal("DESMARAIS/LUC", parseResult.passengerName);
            Assert.Equal("1", parseResult.passengerDescription);
            Assert.Equal("W", parseResult.checkInSource);
            Assert.Equal("W", parseResult.boardingPassIssuanceSource);
            Assert.Equal(expectedIssuanceDate, parseResult.issuanceDate);
            Assert.Equal("B", parseResult.documentType);
            Assert.Equal("0014123456003", parseResult.baggageTagNumber);
            Assert.Equal("1", parseResult.securityDataType);
            Assert.Equal("GIWVC5EH7JNT684FVNJ91W2QA4DVN5J8K4F0L0GEQ3DF5TGBN8709HKT5D3DW3GBHFCVHMY7J5T6HFR41W2QA4DVN5J8K4F0L0GE", parseResult.securityData);
        }

        [Fact(DisplayName = "Empty First Leg")]
        public void EmptyFirstLeg()
        {
            var parseResult = BCBPParser.Parse("M2DESMARAIS/LUC       E                                   06>60000ABC123 YULFRAAC 0834 226F001A0025 10200");

            var expectedFlightDate = DateTime.SpecifyKind(new DateTime(DateTime.Now.Year, 1, 1).AddDays(226 - 1), DateTimeKind.Utc);

            Assert.NotNull(parseResult.Legs);

            Assert.True(parseResult.Legs.Count == 2);

            Assert.Equal("DESMARAIS/LUC", parseResult.passengerName);

            var firstLeg = parseResult.Legs[0];

            Assert.False(firstLeg.HasValues());

            var secondLeg = parseResult.Legs[1];

            Assert.NotNull(secondLeg);

            Assert.Equal("ABC123", secondLeg.operatingCarrierPNR);
            Assert.Equal("YUL", secondLeg.departureAirport);
            Assert.Equal("FRA", secondLeg.arrivalAirport);
            Assert.Equal("AC", secondLeg.operatingCarrierDesignator);
            Assert.Equal("0834", secondLeg.flightNumber);
            Assert.Equal(expectedFlightDate, secondLeg.flightDate);
            Assert.Equal("F", secondLeg.compartmentCode);
            Assert.Equal("001A", secondLeg.seatNumber);
            Assert.Equal("0025", secondLeg.checkInSequenceNumber);
            Assert.Equal("1", secondLeg.passengerStatus);
        }

        [Fact(DisplayName = "Reference Year 2010")]
        public void ReferenceYear2010()
        {
            var parseResult = BCBPParser.Parse("M1DESMARAIS/LUC       EABC123    FRAAC      226F001A      3B>60B1W 6225BAC 2A   1234567890 1AC AC 1234567890123    20KY^164GIWVC5EH7JNT684FVNJ91W2QA4DVN5J8K4F0L0GEQ3DF5TGBN8709HKT5D3DW3GBHFCVHMY7J5T6HFR41W2QA4DVN5J8K4F0L0GE", 2010);

            var expectedFlightDate = new DateTime(2010, 08, 14);
            var expectedIssuanceDate = new DateTime(2006, 08, 13);

            Assert.Equal("DESMARAIS/LUC", parseResult.passengerName);
            Assert.Equal("1", parseResult.passengerDescription);
            Assert.Equal("W", parseResult.checkInSource);
            Assert.Equal(expectedIssuanceDate.ToShortDateString(), parseResult.issuanceDate.Value.ToShortDateString());
            Assert.Equal("B", parseResult.documentType);
            Assert.Equal("AC", parseResult.boardingPassIssuerDesignator);
            Assert.Equal("1", parseResult.securityDataType);
            Assert.Equal("GIWVC5EH7JNT684FVNJ91W2QA4DVN5J8K4F0L0GEQ3DF5TGBN8709HKT5D3DW3GBHFCVHMY7J5T6HFR41W2QA4DVN5J8K4F0L0GE", parseResult.securityData);

            Assert.NotNull(parseResult.Legs);

            Assert.True(parseResult.Legs.Count == 1);

            var firstLeg = parseResult.Legs[0];

            Assert.Equal("ABC123", firstLeg.operatingCarrierPNR);
            Assert.Equal("FRA", firstLeg.arrivalAirport);
            Assert.Equal("AC", firstLeg.operatingCarrierDesignator);
            Assert.Equal(expectedFlightDate.ToShortDateString(), firstLeg.flightDate.Value.ToShortDateString());
            Assert.Equal("F", firstLeg.compartmentCode);
            Assert.Equal("001A", firstLeg.seatNumber);
            Assert.Equal("1234567890", firstLeg.serialNumber);
            Assert.Equal("1", firstLeg.internationalDocumentationVerification);
            Assert.Equal("AC", firstLeg.marketingCarrierDesignator);
            Assert.Equal("AC", firstLeg.frequentFlyerAirlineDesignator);
            Assert.Equal("1234567890123", firstLeg.frequentFlyerNumber);
            Assert.Equal("20K", firstLeg.freeBaggageAllowance);
            Assert.True(firstLeg.fastTrack);
        }

        [Fact(DisplayName = "Reference Year 2006")]
        public void ReferenceYear2006()
        {
            var parseResult = BCBPParser.Parse("M1DESMARAIS/LUC       EABC123    FRAAC      226F001A      3B>60B1W 6225BAC 2A   1234567890 1AC AC 1234567890123    20KY^164GIWVC5EH7JNT684FVNJ91W2QA4DVN5J8K4F0L0GEQ3DF5TGBN8709HKT5D3DW3GBHFCVHMY7J5T6HFR41W2QA4DVN5J8K4F0L0GE", 2006);

            var expectedFlightDate = new DateTime(2006, 08, 14);
            var expectedIssuanceDate = new DateTime(2006, 08, 13);

            Assert.Equal("DESMARAIS/LUC", parseResult.passengerName);
            Assert.Equal("1", parseResult.passengerDescription);
            Assert.Equal("W", parseResult.checkInSource);
            Assert.Equal(expectedIssuanceDate.ToShortDateString(), parseResult.issuanceDate.Value.ToShortDateString());
            Assert.Equal("B", parseResult.documentType);
            Assert.Equal("AC", parseResult.boardingPassIssuerDesignator);
            Assert.Equal("1", parseResult.securityDataType);
            Assert.Equal("GIWVC5EH7JNT684FVNJ91W2QA4DVN5J8K4F0L0GEQ3DF5TGBN8709HKT5D3DW3GBHFCVHMY7J5T6HFR41W2QA4DVN5J8K4F0L0GE", parseResult.securityData);

            Assert.NotNull(parseResult.Legs);

            Assert.True(parseResult.Legs.Count == 1);

            var firstLeg = parseResult.Legs[0];

            Assert.Equal("ABC123", firstLeg.operatingCarrierPNR);
            Assert.Equal("FRA", firstLeg.arrivalAirport);
            Assert.Equal("AC", firstLeg.operatingCarrierDesignator);
            Assert.Equal(expectedFlightDate.ToShortDateString(), firstLeg.flightDate.Value.ToShortDateString());
            Assert.Equal("F", firstLeg.compartmentCode);
            Assert.Equal("001A", firstLeg.seatNumber);
            Assert.Equal("1234567890", firstLeg.serialNumber);
            Assert.Equal("1", firstLeg.internationalDocumentationVerification);
            Assert.Equal("AC", firstLeg.marketingCarrierDesignator);
            Assert.Equal("AC", firstLeg.frequentFlyerAirlineDesignator);
            Assert.Equal("1234567890123", firstLeg.frequentFlyerNumber);
            Assert.Equal("20K", firstLeg.freeBaggageAllowance);
            Assert.True(firstLeg.fastTrack);
        }

        [Fact(DisplayName = "Real Data With Reference Year 2019")]
        public void RealData()
        {
            var parseResult = BCBPParser.Parse("M1ALIFANOV/ROMAN       WOTGK8 OSLCPHD8 3222 057Y007E0016 156>518                        2A3287295525751                            N328-7295525751", 2019);

            var expectedFlightDate = new DateTime(2019, 02, 26);

            Assert.Equal("ALIFANOV/ROMAN", parseResult.passengerName);

            Assert.NotNull(parseResult.Legs);

            Assert.True(parseResult.Legs.Count == 1);

            var firstLeg = parseResult.Legs[0];

            Assert.Equal("328-7295525751", firstLeg.airlineInfo);
            Assert.Equal("328", firstLeg.airlineNumericCode);
            Assert.Equal("CPH", firstLeg.arrivalAirport);
            Assert.Equal("0016", firstLeg.checkInSequenceNumber);
            Assert.Equal("Y", firstLeg.compartmentCode);
            Assert.Equal("OSL", firstLeg.departureAirport);
            Assert.False(firstLeg.fastTrack);
            Assert.Equal(expectedFlightDate.ToShortDateString(), firstLeg.flightDate.Value.ToShortDateString());
            Assert.Equal("3222", firstLeg.flightNumber);
            Assert.Equal("D8", firstLeg.operatingCarrierDesignator);
            Assert.Equal("WOTGK8", firstLeg.operatingCarrierPNR);
            Assert.Equal("1", firstLeg.passengerStatus);
            Assert.Equal("007E", firstLeg.seatNumber);
            Assert.Equal("7295525751", firstLeg.serialNumber);
        }

        
        [Fact(DisplayName = "Real Data 2 Legs With Reference Year 2018")]
        public void RealData2Legs()
        {
            var parseResult = BCBPParser.Parse("M2ALIFANOV/ROMAN      ESQCOLF CUNCDGAF 0651 179M036J0063 336>60B  W     KL 2505714128112530    AF 1174701495      SQCOLF CDGOSLAF 1374 180Y016D0041 3272505714128112530    AF 1174701495      ", 2018);

            var expectedFlightDate = new DateTime(2018, 06, 28);
            var expectedFlightDate2 = new DateTime(2018, 06, 29);

            Assert.Equal("ALIFANOV/ROMAN", parseResult.passengerName);
            Assert.Equal("W", parseResult.boardingPassIssuanceSource);
            Assert.Equal("KL", parseResult.boardingPassIssuerDesignator);

            Assert.NotNull(parseResult.Legs);

            Assert.True(parseResult.Legs.Count == 2);

            var firstLeg = parseResult.Legs[0];

            Assert.Equal("057", firstLeg.airlineNumericCode);
            Assert.Equal("CDG", firstLeg.arrivalAirport);
            Assert.Equal("0063", firstLeg.checkInSequenceNumber);
            Assert.Equal("M", firstLeg.compartmentCode);
            Assert.Equal("CUN", firstLeg.departureAirport);
            Assert.Equal("0651", firstLeg.flightNumber);
            Assert.Equal("AF", firstLeg.operatingCarrierDesignator);
            Assert.Equal("SQCOLF", firstLeg.operatingCarrierPNR);
            Assert.Equal("1174701495", firstLeg.frequentFlyerNumber);
            Assert.Equal("3", firstLeg.passengerStatus);
            Assert.Equal("036J", firstLeg.seatNumber);
            Assert.Equal("1412811253", firstLeg.serialNumber);
            Assert.Equal("0", firstLeg.selecteeIndicator);
            Assert.Equal(expectedFlightDate.ToShortDateString(), firstLeg.flightDate.Value.ToShortDateString());

            var secondLeg = parseResult.Legs[1];

            Assert.Equal("057", secondLeg.airlineNumericCode);
            Assert.Equal("OSL", secondLeg.arrivalAirport);
            Assert.Equal("0041", secondLeg.checkInSequenceNumber);
            Assert.Equal("Y", secondLeg.compartmentCode);
            Assert.Equal("CDG", secondLeg.departureAirport);
            Assert.Equal(expectedFlightDate2.ToShortDateString(), secondLeg.flightDate.Value.ToShortDateString());
            Assert.Equal("1374", secondLeg.flightNumber);
            Assert.Equal("AF", secondLeg.frequentFlyerAirlineDesignator);
            Assert.Equal("1174701495", secondLeg.frequentFlyerNumber);
            Assert.Equal("AF", secondLeg.operatingCarrierDesignator);
            Assert.Equal("SQCOLF", secondLeg.operatingCarrierPNR);
            Assert.Equal("3", secondLeg.passengerStatus);
            Assert.Equal("016D", secondLeg.seatNumber);
            Assert.Equal("1412811253", secondLeg.serialNumber);
            Assert.Equal("0", secondLeg.selecteeIndicator);
        }

        [Fact(DisplayName = "Broken BCBP")]
        public void BrokenBCBP()
        {
            try
            {
                var parseResult = BCBPParser.Parse("ASFASDJAGHSDKASJLDK?LASDKJASDASDASLDJKLASDJKL");
            }
            catch (Exception ex)
            {
                Assert.NotNull(ex);
            }
        }
    }
}