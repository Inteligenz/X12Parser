namespace X12.Hipaa.Claims.Services
{
    using System.Collections.Generic;

    using X12.Hipaa.Claims.Forms;

    /// <summary>
    /// Provides interface for transformations between claims and claim forms
    /// </summary>
    public interface IClaimToClaimFormTransfomation
    {
        /// <summary>
        /// Transform claim to claim form
        /// </summary>
        /// <param name="claim">Object to be transformed</param>
        /// <returns>Collection of <see cref="FormPage"/> objects</returns>
        List<FormPage> TransformClaimToClaimFormFoXml(Claim claim);
    }
}
