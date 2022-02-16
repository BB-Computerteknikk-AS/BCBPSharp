using System.Collections.Generic;

namespace no.bbc.BCBPSharp
{
    public class BCBPParser
    {
        public static BarcodedBoardingPass Parse(string barcodeString, int? referenceYear = null)
        {
            var bcbp = new BarcodedBoardingPass(barcodeString);
            var mainSection = new SectionDecoder(barcodeString);

            bcbp.data = new BoardingPassData();
            bcbp.meta = new BoardingPassMetaData();
            bcbp.meta.formatCode = mainSection.getNextString(LENGTHS.FORMAT_CODE);
            bcbp.meta.numberOfLegs = mainSection.getNextNumber(LENGTHS.NUMBER_OF_LEGS) ?? 0;
            bcbp.data.passengerName = mainSection.getNextString(LENGTHS.PASSENGER_NAME);
            bcbp.meta.electronicTicketIndicator = mainSection.getNextString(
                LENGTHS.ELECTRONIC_TICKET_INDICATOR
            );

            bcbp.data.Legs = new List<Leg>();

            var addedUniqueFields = false;

            for (var legIndex = 0; legIndex < bcbp.meta.numberOfLegs; legIndex++)
            {
                var leg = new Leg();
                leg.operatingCarrierPNR = mainSection.getNextString(
                    LENGTHS.OPERATING_CARRIER_PNR
                );
                leg.departureAirport = mainSection.getNextString(LENGTHS.DEPARTURE_AIRPORT);
                leg.arrivalAirport = mainSection.getNextString(LENGTHS.ARRIVAL_AIRPORT);
                leg.operatingCarrierDesignator = mainSection.getNextString(
                    LENGTHS.OPERATING_CARRIER_DESIGNATOR
                );
                leg.flightNumber = mainSection.getNextString(LENGTHS.FLIGHT_NUMBER);
                leg.flightDate = mainSection.getNextDate(
                    LENGTHS.FLIGHT_DATE,
                    false,
                    referenceYear
                );
                leg.compartmentCode = mainSection.getNextString(LENGTHS.COMPARTMENT_CODE);
                leg.seatNumber = mainSection.getNextString(LENGTHS.SEAT_NUMBER);
                leg.checkInSequenceNumber = mainSection.getNextString(
                    LENGTHS.CHECK_IN_SEQUENCE_NUMBER
                );
                leg.passengerStatus = mainSection.getNextString(LENGTHS.PASSENGER_STATUS);

                var conditionalSectionSize = mainSection.getNextSectionSize();
                var conditionalSection = new SectionDecoder(
                    mainSection.getNextString(conditionalSectionSize)
                );

                if (!addedUniqueFields)
                {
                    bcbp.meta.versionNumberIndicator = conditionalSection.getNextString(
                        LENGTHS.VERSION_NUMBER_INDICATOR
                    );
                    bcbp.meta.versionNumber = conditionalSection.getNextNumber(
                        LENGTHS.VERSION_NUMBER
                    );

                    var sectionASize = conditionalSection.getNextSectionSize();
                    var sectionA = new SectionDecoder(
                        conditionalSection.getNextString(sectionASize)
                    );
                    bcbp.data.passengerDescription = sectionA.getNextString(
                        LENGTHS.PASSENGER_DESCRIPTION
                    );
                    bcbp.data.checkInSource = sectionA.getNextString(LENGTHS.CHECK_IN_SOURCE);
                    bcbp.data.boardingPassIssuanceSource = sectionA.getNextString(
                        LENGTHS.BOARDING_PASS_ISSUANCE_SOURCE
                    );
                    bcbp.data.issuanceDate = sectionA.getNextDate(
                        LENGTHS.ISSUANCE_DATE,
                        true,
                        referenceYear
                    );
                    bcbp.data.documentType = sectionA.getNextString(LENGTHS.DOCUMENT_TYPE);
                    bcbp.data.boardingPassIssuerDesignator = sectionA.getNextString(
                        LENGTHS.BOARDING_PASS_ISSUER_DESIGNATOR
                    );
                    bcbp.data.baggageTagNumber = sectionA.getNextString(
                        LENGTHS.BAGGAGE_TAG_NUMBER
                    );
                    bcbp.data.firstBaggageTagNumber = sectionA.getNextString(
                        LENGTHS.FIRST_BAGGAGE_TAG_NUMBER
                    );
                    bcbp.data.secondBaggageTagNumber = sectionA.getNextString(
                        LENGTHS.SECOND_BAGGAGE_TAG_NUMBER
                    );

                    addedUniqueFields = true;
                }

                var sectionBSize = conditionalSection.getNextSectionSize();
                var sectionB = new SectionDecoder(
                    conditionalSection.getNextString(sectionBSize)
                );
                leg.airlineNumericCode = sectionB.getNextString(
                    LENGTHS.AIRLINE_NUMERIC_CODE
                );
                leg.serialNumber = sectionB.getNextString(LENGTHS.SERIAL_NUMBER);
                leg.selecteeIndicator = sectionB.getNextString(LENGTHS.SELECTEE_INDICATOR);
                leg.internationalDocumentationVerification = sectionB.getNextString(
                    LENGTHS.INTERNATIONAL_DOCUMENTATION_VERIFICATION
                );
                leg.marketingCarrierDesignator = sectionB.getNextString(
                    LENGTHS.MARKETING_CARRIER_DESIGNATOR
                );
                leg.frequentFlyerAirlineDesignator = sectionB.getNextString(
                    LENGTHS.FREQUENT_FLYER_AIRLINE_DESIGNATOR
                );
                leg.frequentFlyerNumber = sectionB.getNextString(
                    LENGTHS.FREQUENT_FLYER_NUMBER
                );
                leg.idIndicator = sectionB.getNextString(LENGTHS.ID_INDICATOR);
                leg.freeBaggageAllowance = sectionB.getNextString(
                    LENGTHS.FREE_BAGGAGE_ALLOWANCE
                );
                leg.fastTrack = sectionB.getNextBoolean();

                leg.airlineInfo = conditionalSection.getRemainingString();

                bcbp.data.Legs.Add(leg);
            }

            bcbp.meta.securityDataIndicator = mainSection.getNextString(
                LENGTHS.SECURITY_DATA_INDICATOR
            );
            bcbp.data.securityDataType = mainSection.getNextString(
                LENGTHS.SECURITY_DATA_TYPE
            );

            var securitySectionSize = mainSection.getNextSectionSize();
            var securitySection = new SectionDecoder(
                mainSection.getNextString(securitySectionSize)
            );
            bcbp.data.securityData = securitySection.getNextString(LENGTHS.SECURITY_DATA);

            if (bcbp.data.issuanceDate != null && referenceYear == null)
            {
                foreach (var leg in bcbp.data.Legs)
                {
                    if (leg.flightDate != null)
                    {
                        var dayOfYear = Utils.DateToDayOfYear(leg.flightDate.Value);
                        leg.flightDate = Utils.DayOfYearToDate(
                            dayOfYear,
                            false,
                            bcbp.data.issuanceDate?.Year
                        );
                    }
                }
            }

            return bcbp;
        }
    }
}