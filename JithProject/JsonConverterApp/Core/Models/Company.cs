using System.Xml.Serialization;

namespace JsonConverterApp.Core.Models
{
    [Serializable, XmlRoot("Data")]
    public class Company
    {
        [XmlElement(ElementName = "id")]
        public int Id { get; set; }

        [XmlElement(ElementName = "name")]
        public string? Name { get; set; }

        [XmlElement(ElementName = "description")]
        public string? Description { get; set; }
    }
}
