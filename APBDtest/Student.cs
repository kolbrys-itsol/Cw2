using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace APBDtest
{
    public class Student
    {
        [XmlElement(ElementName = "fname")]
        [JsonPropertyName("fname")]
        public string Name { get; set; }
        [XmlElement(ElementName = "lname")]
        [JsonPropertyName("lname")]
        public string Surname { get; set; }
        [XmlAttribute(AttributeName = "indexNumber")]
        [JsonPropertyName("indexNumber")]
        public string IndexNum { get; set; }
        [XmlElement(ElementName = "birthdate")]
        [JsonPropertyName("birthdate")]
        public string Date { get; set; }
        [XmlElement(ElementName = "email")]
        [JsonPropertyName("email")]
        public string Mail { get; set; }
        [XmlElement(ElementName = "mothersname")]
        [JsonPropertyName("mothersname")]
        public string MothersName { get; set; }
        [XmlElement(ElementName = "fathersname")]
        [JsonPropertyName("fathersname")]
        public string FathersName { get; set; }
        [XmlElement(ElementName = "studies")]
        [JsonPropertyName("studies")]
        public Studies Studies { get; set; }


        public override bool Equals(object? obj)
        {
            var obj2 = obj as Student;
            if (obj2 == null)
            {
                return false;
            }
            return IndexNum.Equals(obj2.IndexNum);
        }
        
        public override int GetHashCode()
        {
            return (Name+Surname+IndexNum).GetHashCode();
        }
    }

    
}