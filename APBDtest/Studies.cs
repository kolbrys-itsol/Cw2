using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace APBDtest
{
    public class Studies
    {
        [XmlElement(ElementName = "name")]
        [JsonPropertyName("name")]
        public string studName { get; set; }
        [XmlElement(ElementName = "mode")]
        [JsonPropertyName("mode")]
        public string mode { get; set; }
        
        [XmlElement(ElementName = "numberOfStudents")]
        [JsonPropertyName("numberOfStudents")]
        public string numberOfStudents { get; set; }
    }
}