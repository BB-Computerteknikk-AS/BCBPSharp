using System;

namespace no.bbc.BCBPSharp;

public class SectionDecoder
{
    public readonly string? barcodeString;
    public int currentIndex;

    public SectionDecoder(string? barcodeString)
    {
        this.barcodeString = barcodeString;
    }

    private string? getNextField(int? length = null)
    {
        if (barcodeString == null)
        {
            return null;
        }

        var barcodeLength = barcodeString.Length;

        if (currentIndex >= barcodeLength)
        {
            return null;
        }

        string value;

        if (length != null)
        {
            if (barcodeLength - currentIndex - length < 0)
            {
                value = barcodeString.Substring(currentIndex);
            }
            else
            {
                value = barcodeString.Substring(currentIndex, length ?? 0);
            }
        }
        else
        {
            value = barcodeString.Substring(currentIndex);
        }

        currentIndex += length ?? barcodeLength;

        var trimmedValue = value.TrimEnd();
        if (trimmedValue == "")
        {
            return null;
        }

        return trimmedValue;
    }

    public string? getNextString(int length)
    {
        return getNextField(length);
    }

    public int? getNextNumber(int length)
    {
        var value = this.getNextField(length);
        if (value == null)
        {
            return null;
        }

        return int.Parse(value);
    }

    public DateTime? getNextDate(int length, bool hasYearPrefix, int? referenceYear)
    {
        var value = this.getNextField(length);
        if (value == null)
        {
            return null;
        }

        return Utils.DayOfYearToDate(value, hasYearPrefix, referenceYear);
    }

    public bool? getNextBoolean()
    {
        var value = this.getNextField(1);
        if (value == null)
        {
            return null;
        }

        return value == "Y";
    }

    public int getNextSectionSize()
    {
        return Utils.HexToNumber(this.getNextField(2) ?? "00");
    }

    public string? getRemainingString()
    {
        return getNextField();
    }
}