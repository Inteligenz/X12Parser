namespace OopFactory.X12.Hipaa.Claims
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// Represents information associated with a patient's tooth
    /// </summary>
    public class ToothInformation
    {
        /// <summary>
        /// Gets or sets the code identifier for the tooth
        /// </summary>
        [XmlAttribute]
        public string ToothCode { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Common.Lookup"/> collection for the tooth surface
        /// </summary>
        [XmlElement(ElementName = Enums.DentalElements.ToothSurface)]
        public List<Common.Lookup> ToothSurfaces { get; set; }
    }
}
