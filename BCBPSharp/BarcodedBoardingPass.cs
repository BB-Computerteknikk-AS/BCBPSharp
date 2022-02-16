namespace no.bbc.BCBPSharp;

public class BarcodedBoardingPass
{
    public BarcodedBoardingPass(string barcodeString)
    {
        BarcodeString = barcodeString;
    }

    public string BarcodeString { get; private set; }

    public BoardingPassData? data { get; set; }
    public BoardingPassMetaData? meta { get; set; }
}