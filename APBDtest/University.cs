using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace APBDtest
{
    public class University
    {
        public University()
        {
            Students = new HashSet<Student>();
            DateEst = DateTime.Now.ToString("yyyy-mm-dd");
            ActiveStudies = new HashSet<Studies>();
        }
        [XmlAttribute(AttributeName = "Author")]
        [JsonPropertyName("Author")]
        public String Author { get; set; }
        [XmlAttribute(AttributeName = "CreatedAt")]
        [JsonPropertyName("CreatedAt")]
        public string DateEst { get; set; }
        public HashSet<Student> Students { get; set; }
        public HashSet<Studies> ActiveStudies { get; set; }
    }
}