using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Parsing;
using OopFactory.X12.Repositories;

namespace OopFactory.X12.Hipaa.Claims.Repositories
{
    public class SqlClaimRepository<T> : SqlTransactionRepository<T> where T : struct
    {
        public SqlClaimRepository(string dsn, string schema)
            : base(dsn, new SpecificationFinder(), "AMT,BHT,CAS,CL1,CLM,CN1,CR1,CR2,CR3,CR4,CR5,CR6,CR7,CR8,CRC,CTP,CUR,DMG,DN1,DN2,DSB,DTP,HCP,HI,HL,HSD,IMM,K3,LIN,LX,MEA,MIA,MOA,N2,N3,N4,NM1,NTE,OI,PAT,PER,PRV,PS1,PWK,QTY,REF,SBR,SE,ST,SV1,SV2,SV3,SV4,SV5,SV6,SV7,SVD,TOO,UR".Split(','), schema)
        {
        }
    }
}
