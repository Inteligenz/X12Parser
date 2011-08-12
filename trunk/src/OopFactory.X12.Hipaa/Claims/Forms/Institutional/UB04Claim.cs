using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Hipaa.Claims.Forms.Institutional
{
    public class UB04Claim : IComparable
    {

        public string Field1_ProviderName { get; set; }

        List<UB04ServiceLine> ServiceLines { get; set; }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
