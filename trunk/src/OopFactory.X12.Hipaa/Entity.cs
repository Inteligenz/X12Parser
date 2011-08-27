using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Hipaa
{
    public class Entity
    {
        public Entity()
        {
            if (Name == null) Name = new EntityName();
            if (Address == null) Address = new PostalAddress();
            if (Identifications == null) Identifications = new List<Identification>();
            if (Contacts == null) Contacts = new List<Contact>();
        }

        public EntityName Name { get; set; }
        public PostalAddress Address { get; set; }
        public List<Identification> Identifications { get; set; }
        public List<Contact> Contacts { get; set; }

    }
}
