using System;
using System.Diagnostics;

namespace no.bbc.BCBPSharp
{
    public class BCBPLeg
    {
        public string operatingCarrierPNR { get; private set; }
        public string departureAirport { get; private set; }
        public string arrivalAirport { get; private set; }
        public string operatingCarrierDesignator { get; set; }
        public string flightNumber { get; set; }
        public DateTime? flightDate { get; set; }
        public string compartmentCode { get; set; }
        public string seatNumber { get; set; }
        public string checkInSequenceNumber { get; set; }
        public string passengerStatus { get; set; }
        public string serialNumber { get; set; }
        public string internationalDocumentationVerification { get; set; }
        public string marketingCarrierDesignator { get; set; }
        public string frequentFlyerAirlineDesignator { get; set; }
        public string frequentFlyerNumber { get; set; }
        public string freeBaggageAllowance { get; set; }
        public bool? fastTrack { get; set; }
        public string airlineNumericCode { get; set; }
        public string airlineInfo { get; set; }
        public string selecteeIndicator { get; set; }

        public bool HasValues()
        {
            var hasValues = false;
            try
            {
                var properties = GetType().GetProperties();

                foreach (var prop in properties)
                {
                    var getterParams = prop.GetMethod.GetParameters();
                    if (getterParams.Length > 0)
                    {
                        // ignore the indexer
                        continue;
                    }

                    var value = prop.GetValue(this, null);

                    if (value != null)
                    {
                        var propType = value.GetType();

                        if (propType.IsValueType)
                        {
                            throw new Exception("value type checking is unhandled");
                        }

                        hasValues = true;
                    }
                    else
                    {
                        hasValues = false;
                    }

                    if (hasValues)
                    {
                        break;
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return hasValues;
        }

        /// <summary>
        /// Allows to assign the properties dynamically
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public object this[string propertyName]
        {
            get
            {
                return GetType().GetProperty(propertyName).GetValue(this, null);
            }
            set
            {
                var prop = GetType().GetProperty(propertyName);

                if (prop == null)
                {
                    if (Debugger.IsAttached)
                    {
                        Debugger.Break(); // TODO Implement Property
                    }

                    Console.WriteLine(propertyName + " was not found!");
                }
                else
                {
                    prop.SetValue(this, value, null);
                }
            }
        }
    }
}
