using Newtonsoft.Json;
using System.ComponentModel;

namespace Domain
{
    public class Person
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "firstName")]
        [DisplayName("Fornavn")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        [DisplayName("Efternavn")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "cprNo")]
        [DisplayName("Cpr Nummer")]
        public string CprNo { get; set; }

        [JsonProperty(PropertyName = "birthday")]
        [DisplayName("Fødselsdag")]
        public string Birthday { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
