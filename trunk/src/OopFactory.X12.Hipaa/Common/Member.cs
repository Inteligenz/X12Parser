using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Hipaa.Common
{
    public enum GenderEnum
    {
        Male,
        Female,
        Unknown
    }

    public class Member : Entity
    {

        public GenderEnum Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
