using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace no.bbc.BCBPSharp
{
    public class BCBPOutput
    {
        public BCBPOutput(string rawData)
        {
            this.rawData = rawData;
        }
        
        public DateTime? issuanceDate { get; set; }
        public string passengerDescription { get; set; }
        public List<BCBPLeg> Legs { get; set; } = new List<BCBPLeg>();

        public string passengerName { get; set; }
        public string checkInSource { get; set; }
        public string documentType { get; set; }
        public string boardingPassIssuerDesignator { get; set; }
        public string securityDataType { get; set; }
        public string securityData { get; set; }
        public string boardingPassIssuanceSource { get; set; }
        public string baggageTagNumber { get; set; }

        public string rawData { get; private set; }

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
