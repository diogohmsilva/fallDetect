using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RMSF.Models
{
    public class Patient
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("age")]

        public long Age { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("risk")]
        public string Risk { get; set; }

        [JsonProperty("isOK")]

        public long IsOk { get; set; }

        [JsonProperty("responsible")]

        public long Responsible { get; set; }

        [JsonProperty("camIP")]
        public string CamIp { get; set; }

        public string AgeText 
        {
            get
            {
                return string.Format("{0} years old", Age);
            }
        }

        public string RiskText
        {
            get
            {
                if (Risk.Equals("L"))
                {
                    return "Risk is Low";
                }
                else if (Risk.Equals("M"))
                {
                    return "Risk is Medium";
                }
                else {
                    return "Risk is High";
                }
            }
        }

        public Patient(string name, string surname, string age, string address, string risk)
        {
            Name = name;
            Surname = surname;
            Age = Convert.ToInt64(age);
            Address = address;
            Risk = risk;
        }

        public string fullName {
            get
            {
                return string.Format("{0} {1}", Name, Surname);
            }
        }


        public string backColor {
            get
            {
                if (IsOk == 0)
                {
                    return "#f08181";
                }
                else
                {
                    return "#9de0ed";
                }
            }
        }


        
    }
}
