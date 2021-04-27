using System;

namespace no.bbc.BCBPSharp
{
    public class BCBPField
    {
        public string name { get; set; }
        public string type { get; set; }
        public bool unique { get; set; }
        public bool mandatory { get; set; }
        public bool meta { get; set; }
        public bool isSecurityField { get; set; }
        public int? length { get; set; }
        public string defaultValue { get; set; }
        public BCBPField[] fields { get; set; }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(type))
            {
                return string.Format("{0}:{1}", name, type);
            }

            return base.ToString();
        }
    }
}
