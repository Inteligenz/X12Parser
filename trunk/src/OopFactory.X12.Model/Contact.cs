using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Model
{
    public class Contact
    {
        public string Name { get; set; }

        public string Fax { get; set; }
        public string Email { get; set; }
        public Phone Phone { get; set; }
    }
}
