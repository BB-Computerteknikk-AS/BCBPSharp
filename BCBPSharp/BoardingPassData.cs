using System;
using System.Collections.Generic;

namespace no.bbc.BCBPSharp;

public class BoardingPassData
{
    public List<Leg>? Legs { get; set; }
    public string? passengerName { get; set; }
    public string? passengerDescription { get; set; }
    public string? checkInSource { get; set; }
    public string? boardingPassIssuanceSource { get; set; }
    public DateTime? issuanceDate { get; set; }
    public string? documentType { get; set; }
    public string? boardingPassIssuerDesignator { get; set; }
    public string? baggageTagNumber { get; set; }
    public string? firstBaggageTagNumber { get; set; }
    public string? secondBaggageTagNumber { get; set; }
    public string? securityDataType { get; set; }
    public string? securityData { get; set; }
}