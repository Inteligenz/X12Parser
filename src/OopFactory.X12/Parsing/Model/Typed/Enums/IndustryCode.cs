using OopFactory.X12.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed.Enums
{
    public enum IndustryCode
    {
        [EDIFieldValue("11")]
        Office,
        [EDIFieldValue("12")]
        Home,
        [EDIFieldValue("21")]
        InpatientHospital,
        [EDIFieldValue("22")]
        OutpatientHospital,
        [EDIFieldValue("23")]
        EmergencyRoom_Hospital,
        [EDIFieldValue("24")]
        AmbulatorySurgicalCenter,
        [EDIFieldValue("25")]
        BirthingCenter,
        [EDIFieldValue("26")]
        MilitaryTreatmentFacility,
        [EDIFieldValue("31")]
        SkilledNursingFacility,
        [EDIFieldValue("32")]
        NursingFacility,
        [EDIFieldValue("33")]
        CustodialCareFacility,
        [EDIFieldValue("34")]
        Hospice,
        [EDIFieldValue("41")]
        Ambulance_Land,
        [EDIFieldValue("42")]
        Ambulance_AirorWater,
        [EDIFieldValue("50")]
        FederallyQualifiedHealthCenter,
        [EDIFieldValue("51")]
        InpatientPsychiatricFacility,
        [EDIFieldValue("52")]
        PsychiatricFacilityPartialHospitalization,
        [EDIFieldValue("53")]
        CommunityMentalHealthCenter,
        [EDIFieldValue("54")]
        IntermediateCareFacility_MentallyRetarded,
        [EDIFieldValue("55")]
        ResidentialSubstanceAbuseTreatmentFacility,
        [EDIFieldValue("56")]
        PsychiatricResidentialTreatmentCenter,
        [EDIFieldValue("60")]
        MassImmunizationCenter,
        [EDIFieldValue("61")]
        ComprehensiveInpatientRehabilitationFacility,
        [EDIFieldValue("62")]
        ComprehensiveOutpatientRehabilitationFacility,
        [EDIFieldValue("65")]
        End_StageRenalDiseaseTreatmentFacility,
        [EDIFieldValue("71")]
        StateorLocalPublicHealthClinic,
        [EDIFieldValue("72")]
        RuralHealthClinic,
        [EDIFieldValue("81")]
        IndependentLaboratory,
        [EDIFieldValue("99")]
        OtherUnlistedFacility,
    }
}
