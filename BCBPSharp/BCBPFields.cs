namespace no.bbc.BCBPSharp
{
    public class BCBPFields
    {
        private BCBPField[] _allFields;
        private static BCBPFields _instance;

        public static BCBPField[] AllFields
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new BCBPFields();
                }

                return _instance._allFields;
            }
        }

        public BCBPFields()
        {
            _allFields = new BCBPField[] {
                new BCBPField()
                {
                    name = "formatCode",
                    type = "string",
                    unique = true,
                    meta = true,
                    mandatory = true,
                    length = 1,
                    defaultValue = "M",
                },
                 new BCBPField()
                {
                    name = "numberOfLegs",
                    type = "string",
                    unique = true,
                    meta = true,
                    mandatory = true,
                    length = 1,
                    defaultValue = "",
                },
                 new BCBPField()
                {
                    name = "passengerName",
                    type = "string",
                    unique = true,
                    mandatory = true,
                    length = 20,
                    defaultValue = "",
                },
                 new BCBPField()
                {
                    name = "electronicTicketIndicator",
                    type = "string",
                    unique = true,
                    meta = true,
                    mandatory = true,
                    length = 1,
                    defaultValue = "E",
                },
                 new BCBPField()
                {
                    name = "operatingCarrierPNR",
                    type = "string",
                    unique = false,
                    mandatory = true,
                    length = 7,
                    defaultValue = "",
                },
                 new BCBPField()
                {
                    name = "departureAirport",
                    type = "string",
                    unique = false,
                    mandatory = true,
                    length = 3,
                    defaultValue = "",
                },
                 new BCBPField()
                {
                    name = "arrivalAirport",
                    type = "string",
                    unique = false,
                    mandatory = true,
                    length = 3,
                    defaultValue = "",
                },
                 new BCBPField()
                {
                    name = "operatingCarrierDesignator",
                    type = "string",
                    unique = false,
                    mandatory = true,
                    length = 3,
                    defaultValue = "",
                },
                 new BCBPField()
                {
                    name = "flightNumber",
                    type = "string",
                    unique = false,
                    mandatory = true,
                    length = 5,
                    defaultValue = "",
                },
                 new BCBPField()
                {
                    name = "flightDate",
                    type = "date",
                    unique = false,
                    mandatory = true,
                    length = 3,
                    defaultValue = "",
                },
                 new BCBPField()
                {
                    name = "compartmentCode",
                    type = "string",
                    unique = false,
                    mandatory = true,
                    length = 1,
                    defaultValue = "",
                },
                 new BCBPField()
                {
                    name = "seatNumber",
                    type = "string",
                    unique = false,
                    mandatory = true,
                    length = 4,
                    defaultValue = "",
                },
                 new BCBPField()
                {
                    name = "checkInSequenceNumber",
                    type = "string",
                    unique = false,
                    mandatory = true,
                    length = 5,
                    defaultValue = "",
                },
                 new BCBPField()
                {
                    name = "passengerStatus",
                    type = "string",
                    unique = false,
                    mandatory = true,
                    length = 1,
                    defaultValue = "",
                },
                 new BCBPField()
                {
                    name = "fieldSizeConditional",
                    type = "string",
                    unique = false,
                    meta = true,
                    mandatory = true,
                    length = 2,
                    defaultValue = "",
                    fields = new BCBPField[] {
                         new BCBPField()
                        {
                            name = "versionNumberIndicator",
                            type = "string",
                            unique = true,
                            meta = true,
                            length = 1,
                            defaultValue = ">",
                        },
                         new BCBPField()
                        {
                            name = "versionNumber",
                            type = "string",
                            unique = true,
                            meta = true,
                            length = 1,
                            defaultValue = "6",
                        },
                         new BCBPField()
                        {
                            name = "fieldSizeA",
                            type = "string",
                            unique = true,
                            meta = true,
                            length = 2,
                            defaultValue = "",
                            fields = new BCBPField[] {
                                 new BCBPField()
                                {
                                    name = "passengerDescription",
                                    type = "string",
                                    unique = true,
                                    length = 1,
                                    defaultValue = "",
                                },
                                 new BCBPField()
                                {
                                    name = "checkInSource",
                                    type = "string",
                                    unique = true,
                                    length = 1,
                                    defaultValue = "",
                                },
                                 new BCBPField()
                                {
                                    name = "boardingPassIssuanceSource",
                                    type = "string",
                                    unique = true,
                                    length = 1,
                                    defaultValue = "",
                                },
                                 new BCBPField()
                                {
                                    name = "issuanceDate",
                                    type = "dateWithYear",
                                    unique = true,
                                    length = 4,
                                    defaultValue = "",
                                },
                                 new BCBPField()
                                {
                                    name = "documentType",
                                    type = "string",
                                    unique = true,
                                    length = 1,
                                    defaultValue = "",
                                },
                                 new BCBPField()
                                {
                                    name = "boardingPassIssuerDesignator",
                                    type = "string",
                                    unique = true,
                                    length = 3,
                                    defaultValue = "",
                                },
                                 new BCBPField()
                                {
                                    name = "baggageTagNumber",
                                    type = "string",
                                    unique = true,
                                    length = 13,
                                    defaultValue = "",
                                },
                                 new BCBPField()
                                {
                                    name = "firstBaggageTagNumber",
                                    type = "string",
                                    unique = true,
                                    length = 13,
                                    defaultValue = "",
                                },
                                 new BCBPField()
                                {
                                    name = "secondBaggageTagNumber",
                                    type = "string",
                                    unique = true,
                                    length = 13,
                                    defaultValue = "",
                                },
                            },
                        },
                         new BCBPField()
                        {
                            name = "fieldSizeB",
                            type = "string",
                            unique = false,
                            meta = true,
                            length = 2,
                            defaultValue = "",
                            fields = new BCBPField[] {
                                 new BCBPField()
                                {
                                    name = "airlineNumericCode",
                                    type = "string",
                                    unique = false,
                                    length = 3,
                                    defaultValue = "",
                                },
                                 new BCBPField()
                                {
                                    name = "serialNumber",
                                    type = "string",
                                    unique = false,
                                    length = 10,
                                    defaultValue = "",
                                },
                                 new BCBPField()
                                {
                                    name = "selecteeIndicator",
                                    type = "string",
                                    unique = false,
                                    length = 1,
                                    defaultValue = "",
                                },
                                 new BCBPField()
                                {
                                    name = "internationalDocumentationVerification",
                                    type = "string",
                                    unique = false,
                                    length = 1,
                                    defaultValue = "",
                                },
                                 new BCBPField()
                                {
                                    name = "marketingCarrierDesignator",
                                    type = "string",
                                    unique = false,
                                    length = 3,
                                    defaultValue = "",
                                },
                                 new BCBPField()
                                {
                                    name = "frequentFlyerAirlineDesignator",
                                    type = "string",
                                    unique = false,
                                    length = 3,
                                    defaultValue = "",
                                },
                                 new BCBPField()
                                {
                                    name = "frequentFlyerNumber",
                                    type = "string",
                                    unique = false,
                                    length = 16,
                                    defaultValue = "",
                                },
                                 new BCBPField()
                                {
                                    name = "idIndicator",
                                    type = "string",
                                    unique = false,
                                    length = 1,
                                    defaultValue = "",
                                },
                                 new BCBPField()
                                {
                                    name = "freeBaggageAllowance",
                                    type = "string",
                                    unique = false,
                                    length = 3,
                                    defaultValue = "",
                                },
                                 new BCBPField()
                                {
                                    name = "fastTrack",
                                    type = "boolean",
                                    unique = false,
                                    length = 1,
                                    defaultValue = "",
                                },
                            },
                        },
                         new BCBPField()
                        {
                            name = "airlineInfo",
                            type = "string",
                            unique = false,
                            defaultValue = "",
                        },
                    },
                },
                 new BCBPField()
                {
                    name = "securityDataIndicator",
                    type = "string",
                    unique = true,
                    meta = true,
                    isSecurityField = true,
                    length = 1,
                    defaultValue = "^",
                },
                 new BCBPField()
                {
                    name = "securityDataType",
                    type = "string",
                    unique = true,
                    isSecurityField = true,
                    length = 1,
                    defaultValue = "1",
                },
                 new BCBPField()
                {
                    name = "securityDataSize",
                    type = "string",
                    unique = true,
                    meta = true,
                    isSecurityField = true,
                    length = 2,
                    defaultValue = "",
                    fields = new BCBPField[] {
                         new BCBPField()
                        {
                            name = "securityData",
                            type = "string",
                            unique = true,
                            isSecurityField = true,
                            length = 100,
                            defaultValue = "",
                        }
                    }
                }
            };
        }
    }
}
