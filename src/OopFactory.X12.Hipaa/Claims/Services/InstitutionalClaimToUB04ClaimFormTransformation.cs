using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Hipaa.Claims.Forms;
using OopFactory.X12.Hipaa.Claims.Forms.Institutional;

namespace OopFactory.X12.Hipaa.Claims.Services
{
    public class InstitutionalClaimToUB04ClaimFormTransformation : IClaimToClaimFormTransfomation
    {
        private string _formImagePath;

        public InstitutionalClaimToUB04ClaimFormTransformation(string formImagePath)
        {
            _formImagePath = formImagePath;
        }

        public virtual UB04Claim TransformClaimToUB04(Claim claim)
        {
            return new UB04Claim();
        }

        private FormBlock AddBlock(FormPage page, decimal x, decimal y, decimal width, string text)
        {
            return AddBlock(page, x, y, width, text, TextAlignEnum.left);
        }

        private FormBlock AddBlock(FormPage page, decimal x, decimal y, decimal width, string text, TextAlignEnum textAlign)
        {
            decimal xScale = 0.1m;
            decimal yScale = 0.1685m;
            var block = new FormBlock
            {
                TextAlign = textAlign,
                Left = -0.21m + xScale * x,
                Top = 0.1m + yScale * y,
                Width = xScale * width,
                Height = yScale * 1.1m,
                Text = text
            };
            page.Blocks.Add(block);
            return block;
        }
        
        public virtual List<FormPage> TransformUB04ToFormPages(UB04Claim ub04)
        {
            List<FormPage> pages = new List<FormPage>();

            return pages;
        }

        public List<FormPage> TransformClaimToClaimFormFoXml(Claim claim)
        {
            UB04Claim ub04 = TransformClaimToUB04(claim);

            return TransformUB04ToFormPages(ub04);
        }
    }
}