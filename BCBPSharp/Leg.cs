using System;

namespace no.bbc.BCBPSharp;

public class Leg
{
    public string? operatingCarrierPNR { get; set; }
    public string? departureAirport { get; set; }
    public string? arrivalAirport { get; set; }
    public string? operatingCarrierDesignator { get; set; }
    public string? flightNumber { get; set; }
    public DateTime? flightDate { get; set; }
    public string? compartmentCode { get; set; }
    public string? seatNumber { get; set; }
    public string? checkInSequenceNumber { get; set; }
    public string? passengerStatus { get; set; }
    public string? airlineNumericCode { get; set; }
    public string? serialNumber { get; set; }
    public string? selecteeIndicator { get; set; }
    public string? internationalDocumentationVerification { get; set; }
    public string? marketingCarrierDesignator { get; set; }
    public string? frequentFlyerAirlineDesignator { get; set; }
    public string? frequentFlyerNumber { get; set; }
    public string? idIndicator { get; set; }
    public string? freeBaggageAllowance { get; set; }
    public bool? fastTrack { get; set; }
    public string? airlineInfo { get; set; }
}