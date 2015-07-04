using OopFactory.X12.Attributes;

namespace OopFactory.X12.Parsing.Model.Typed.Enums
{
    public enum ProductOrServiceIdQualifiers
    {
        [EDIFieldValue("ER")]
        JurisdictionSpecificProcedureAndSupplyCodes,
        [EDIFieldValue("HC")]
        HealthCareFinancingAdministrationCommonProceduralCodingSystem,
        [EDIFieldValue("IV")]
        HomeInfusionEDI_CoalitionProduct_ServiceCode,
        [EDIFieldValue("WK")]
        AdvancedBillingConceptsCodes,
    }
}
