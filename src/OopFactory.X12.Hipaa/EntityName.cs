using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Hipaa
{
    public class EntityName
    {
        public string Identifier { get; set; }
        public string Qualifier { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }

    }
}
