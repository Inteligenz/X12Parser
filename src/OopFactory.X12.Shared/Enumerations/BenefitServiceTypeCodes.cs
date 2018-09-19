namespace OopFactory.X12.Shared.Enumerations
{
    using OopFactory.X12.Shared.Attributes;

    public enum BenefitServiceTypeCodes
    {
        [EdiFieldValue("1")]
        MedicalCare,

        [EdiFieldValue("2")]
        Surgical,

        [EdiFieldValue("3")]
        Consultation,

        [EdiFieldValue("4")]
        DiagnosticXRay,

        [EdiFieldValue("5")]
        DiagnosticLab,

        [EdiFieldValue("6")]
        RadiationTherapy,

        [EdiFieldValue("7")]
        Anesthesia,

        [EdiFieldValue("8")]
        SurgicalAssistance,

        [EdiFieldValue("10")]
        Blood,

        [EdiFieldValue("11")]
        DurableMedicalEquipmentUsed,

        [EdiFieldValue("12")]
        DurableMedicalEquipmentPurchased,

        [EdiFieldValue("14")]
        RenalSupplies,

        [EdiFieldValue("17")]
        PreAdmissionTesting,

        [EdiFieldValue("18")]
        DurableMedicalEquipmentRental,

        [EdiFieldValue("19")]
        PneumoniaVaccine,

        [EdiFieldValue("20")]
        SecondSurgicalOpinion,

        [EdiFieldValue("21")]
        ThirdSurgicalOpinion,

        [EdiFieldValue("22")]
        SocialWork,

        [EdiFieldValue("23")]
        DiagnosticDental,

        [EdiFieldValue("24")]
        Periodontics,

        [EdiFieldValue("25")]
        Restorative,

        [EdiFieldValue("26")]
        Endodontics,

        [EdiFieldValue("27")]
        MaxillofacialProsthetics,

        [EdiFieldValue("28")]
        AdjunctiveDentalServices,

        [EdiFieldValue("30")]
        HealthBenefitPlanCoverage,

        [EdiFieldValue("32")]
        PlanWaitingPeriod,

        [EdiFieldValue("33")]
        Chiropractic,

        [EdiFieldValue("34")]
        ChiropracticModality,

        [EdiFieldValue("35")]
        DentalCare,

        [EdiFieldValue("36")]
        DentalCrowns,

        [EdiFieldValue("37")]
        DentalAccident,

        [EdiFieldValue("38")]
        Orthodontics,

        [EdiFieldValue("39")]
        Prosthodontics,

        [EdiFieldValue("40")]
        OralSurgery,

        [EdiFieldValue("41")]
        PreventiveDental,

        [EdiFieldValue("42")]
        HomeHealthCare,

        [EdiFieldValue("43")]
        HomeHealthPrescriptions,

        [EdiFieldValue("45")]
        Hospice,

        [EdiFieldValue("46")]
        RespiteCare,

        [EdiFieldValue("47")]
        Hospitalization,

        [EdiFieldValue("49")]
        HospitalRoomandBoard,

        [EdiFieldValue("54")]
        LongTermCare,

        [EdiFieldValue("55")]
        MajorMedical,

        [EdiFieldValue("56")]
        MedicallyRelatedTransportation,

        [EdiFieldValue("60")]
        GeneralBenefits,

        [EdiFieldValue("61")]
        InvitroFertilization,

        [EdiFieldValue("62")]
        MRIScan,

        [EdiFieldValue("63")]
        DonorProcedures,

        [EdiFieldValue("64")]
        Acupuncture,

        [EdiFieldValue("65")]
        NewbornCare,

        [EdiFieldValue("66")]
        Pathology,

        [EdiFieldValue("67")]
        SmokingCessation,

        [EdiFieldValue("68")]
        WellBabyCare,

        [EdiFieldValue("69")]
        Maternity,

        [EdiFieldValue("70")]
        Transplants,

        [EdiFieldValue("71")]
        Audiology,

        [EdiFieldValue("72")]
        InhalationTherapy,

        [EdiFieldValue("73")]
        DiagnosticMedical,

        [EdiFieldValue("74")]
        PrivateDutyNursing,

        [EdiFieldValue("75")]
        ProstheticDevice,

        [EdiFieldValue("76")]
        Dialysis,

        [EdiFieldValue("77")]
        Otology,

        [EdiFieldValue("78")]
        Chemotherapy,

        [EdiFieldValue("79")]
        AllergyTesting,

        [EdiFieldValue("80")]
        Immunizations,

        [EdiFieldValue("81")]
        RoutinePhysical,

        [EdiFieldValue("82")]
        FamilyPlanning,

        [EdiFieldValue("83")]
        Infertility,

        [EdiFieldValue("84")]
        Abortion,

        [EdiFieldValue("85")]
        HIVAIDSTreatment,

        [EdiFieldValue("86")]
        EmergencyServices,

        [EdiFieldValue("87")]
        CancerTreatment,

        [EdiFieldValue("88")]
        Pharmacy,

        [EdiFieldValue("89")]
        FreeStandingPrescriptionDrug,

        [EdiFieldValue("90")]
        MailOrderPrescriptionDrug,

        [EdiFieldValue("91")]
        BrandNamePrescriptionDrug,

        [EdiFieldValue("92")]
        GenericPrescriptionDrug,

        [EdiFieldValue("93")]
        Podiatry,

        [EdiFieldValue("A4")]
        Psychiatric,

        [EdiFieldValue("A6")]
        Psychotherapy,

        [EdiFieldValue("A7")]
        PsychiatricInpatient,

        [EdiFieldValue("A8")]
        PsychiatricOutpatient,

        [EdiFieldValue("A9")]
        Rehabilitation,

        [EdiFieldValue("AB")]
        RehabilitationInpatient,

        [EdiFieldValue("AC")]
        RehabilitationOutpatient,

        [EdiFieldValue("AD")]
        OccupationalTherapy,

        [EdiFieldValue("AE")]
        PhysicalMedicine,

        [EdiFieldValue("AF")]
        SpeechTherapy,

        [EdiFieldValue("AG")]
        SkilledNursingCare,

        [EdiFieldValue("AI")]
        SubstanceAbuse,

        [EdiFieldValue("AJ")]
        AlcoholismTreatment,

        [EdiFieldValue("AK")]
        DrugAddiction,

        [EdiFieldValue("AL")]
        Optometry,

        [EdiFieldValue("AM")]
        Frames,

        [EdiFieldValue("AO")]
        Lenses,

        [EdiFieldValue("AP")]
        RoutineEyeExam,

        [EdiFieldValue("AQ")]
        NonmedicallyNecessaryPhysical,

        [EdiFieldValue("AR")]
        ExperimentalDrugTherapy,

        [EdiFieldValue("B1")]
        BurnCare,

        [EdiFieldValue("B2")]
        BrandNamePrescriptionDrugFormulary,

        [EdiFieldValue("B3")]
        BrandNamePrescriptionDrugNonFormulary,

        [EdiFieldValue("BA")]
        IndependentMedicalEvaluation,

        [EdiFieldValue("BB")]
        PsychiatricTreatmentPartialHospitalization,

        [EdiFieldValue("BC")]
        DayCarePsychiatric,

        [EdiFieldValue("BD")]
        CognitiveTherapy,

        [EdiFieldValue("BE")]
        MassageTherapy,

        [EdiFieldValue("BF")]
        PulmonaryRehabilitation,

        [EdiFieldValue("BG")]
        CardiacRehabilitation,

        [EdiFieldValue("BH")]
        Pediatric,

        [EdiFieldValue("BI")]
        NurseryRoomandBoard,

        [EdiFieldValue("BK")]
        Orthopedic,

        [EdiFieldValue("BL")]
        Cardiac,

        [EdiFieldValue("BM")]
        Lymphatic,

        [EdiFieldValue("BN")]
        Gastrointestinal,

        [EdiFieldValue("BP")]
        Endocrine,

        [EdiFieldValue("BQ")]
        Neurology,

        [EdiFieldValue("BT")]
        Gynecological,

        [EdiFieldValue("BU")]
        Obstetrical,

        [EdiFieldValue("BV")]
        ObstetricalGynecological,

        [EdiFieldValue("BW")]
        MailOrderPrescriptionDrugBrandName,

        [EdiFieldValue("BX")]
        MailOrderPrescriptionDrugGeneric,

        [EdiFieldValue("BY")]
        PhysicianVisitSick,

        [EdiFieldValue("BZ")]
        PhysicianVisitWell,

        [EdiFieldValue("C1")]
        CoronaryCare,

        [EdiFieldValue("CK")]
        ScreeningXray,

        [EdiFieldValue("CL")]
        Screeninglaboratory,

        [EdiFieldValue("CM")]
        MammogramHighRiskPatient,

        [EdiFieldValue("CN")]
        MammogramLowRiskPatient,

        [EdiFieldValue("CO")]
        FluVaccination,

        [EdiFieldValue("CP")]
        EyewearAccessories,

        [EdiFieldValue("CQ")]
        CaseManagement,

        [EdiFieldValue("DG")]
        Dermatology,

        [EdiFieldValue("DM")]
        DurableMedicalEquipment,

        [EdiFieldValue("DS")]
        DiabeticSupplies,

        [EdiFieldValue("E0")]
        AlliedBehavioralAnalysisTherapy,

        [EdiFieldValue("E1")]
        NonMedicalEquipmentnonDME,

        [EdiFieldValue("E2")]
        PsychiatricEmergency,

        [EdiFieldValue("E3")]
        StepDownUnit,

        [EdiFieldValue("E4")]
        SkilledNursingFacilityHeadLevelofCare,

        [EdiFieldValue("E5")]
        SkilledNursingFacilityVentilatorLevelofCare,

        [EdiFieldValue("E6")]
        LevelofCare1,

        [EdiFieldValue("E7")]
        LevelofCare2,

        [EdiFieldValue("E8")]
        LevelofCare3,

        [EdiFieldValue("E9")]
        LevelofCare4,

        [EdiFieldValue("E10")]
        Radiographs,

        [EdiFieldValue("E11")]
        DiagnosticImaging,

        [EdiFieldValue("E12")]
        BasicRestorativeDental,

        [EdiFieldValue("E13")]
        MajorRestorativeDental,

        [EdiFieldValue("E14")]
        FixedProsthodontics,

        [EdiFieldValue("E15")]
        RemovableProsthodontics,

        [EdiFieldValue("E16")]
        IntraoralImagesCompleteSeries,

        [EdiFieldValue("E17")]
        OralEvaluation,

        [EdiFieldValue("E18")]
        DentalProphylaxis,

        [EdiFieldValue("E19")]
        PanoramicImages,

        [EdiFieldValue("E20")]
        Sealants,

        [EdiFieldValue("E21")]
        FlourideTreatments,

        [EdiFieldValue("E22")]
        DentalImplants,

        [EdiFieldValue("E23")]
        TemporomandibularJointDysfunction,

        [EdiFieldValue("E24")]
        RetailPharmacyPrescriptionDrug,

        [EdiFieldValue("E25")]
        LongTermCarePharmacy,

        [EdiFieldValue("E26")]
        ComprehensiveMedicationTherapyManagementReview,

        [EdiFieldValue("E27")]
        TargetedMedicationTherapyManagementReview,

        [EdiFieldValue("E28")]
        DietaryNutritionalServices,

        [EdiFieldValue("E29")]
        TechnicalCardiacRehabilitationServicesComponent,

        [EdiFieldValue("E30")]
        ProfessionalCardiacRehabilitationServicesComponent,

        [EdiFieldValue("E31")]
        ProfessionalIntensiveCardiacRehabilitationServicesComponent,

        [EdiFieldValue("E32")]
        IntensiveCardiacRehabilitationTechnicalComponent,

        [EdiFieldValue("E33")]
        IntensiveCardiacRehabilitation,

        [EdiFieldValue("E34")]
        PulmonaryRehabilitationTechnicalComponent,

        [EdiFieldValue("E35")]
        PulmonaryRehabilitationProfessionalComponent,

        [EdiFieldValue("E36")]
        ConvenienceCare,

        [EdiFieldValue("EA")]
        PreventiveServices,

        [EdiFieldValue("EB")]
        SpecialtyPharmacy,

        [EdiFieldValue("EC")]
        DurableMedicalEquipmentNew,

        [EdiFieldValue("ED")]
        CATScan,

        [EdiFieldValue("EE")]
        Ophthalmology,

        [EdiFieldValue("EF")]
        ContactLenses,

        [EdiFieldValue("GF")]
        GenericPrescriptionDrugFormulary,

        [EdiFieldValue("GN")]
        GenericPrescriptionDrugNonFormulary,

        [EdiFieldValue("GY")]
        Allergy,

        [EdiFieldValue("IC")]
        IntensiveCare,

        [EdiFieldValue("MH")]
        MentalHealth,

        [EdiFieldValue("NI")]
        NeonatalIntensiveCare,

        [EdiFieldValue("ON")]
        Oncology,

        [EdiFieldValue("PE")]
        PositronEmissionTomographyPETScan,

        [EdiFieldValue("PT")]
        PhysicalTherapy,

        [EdiFieldValue("PU")]
        Pulmonary,

        [EdiFieldValue("RN")]
        Renal,

        [EdiFieldValue("RT")]
        ResidentialPsychiatricTreatment,

        [EdiFieldValue("SMH")]
        SeriousMentalHealth,

        [EdiFieldValue("TC")]
        TransitionalCare,

        [EdiFieldValue("TN")]
        TransitionalNurseryCare,

        [EdiFieldValue("UC")]
        UrgentCare,
    }
}
