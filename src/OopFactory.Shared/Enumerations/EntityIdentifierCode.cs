namespace OopFactory.X12.Shared.Enumerations
{
    using OopFactory.X12.Shared.Attributes;

    public enum EntityIdentifierCode
    {
        [EdiFieldValue("01")]
        LoanApplicant,

        [EdiFieldValue("02")]
        LoanBroker,

        [EdiFieldValue("03")]
        Dependent,

        [EdiFieldValue("04")]
        AssetAccountHolder,

        [EdiFieldValue("05")]
        Tenant,

        [EdiFieldValue("06")]
        RecipientOfCivilOrLegalLiabilityPayment,

        [EdiFieldValue("07")]
        Titleholder,

        [EdiFieldValue("08")]
        NonMortgageLiabilityAccountHolder,

        [EdiFieldValue("09")]
        NoteCo_Signer,

        [EdiFieldValue("0A")]
        ComparableRentals,

        [EdiFieldValue("0B")]
        InterimFundingOrganization,

        [EdiFieldValue("0D")]
        NonOccupantCoBorrower,

        [EdiFieldValue("0E")]
        ListOwner,

        [EdiFieldValue("0F")]
        ListMailer,

        [EdiFieldValue("0H")]
        StateDivision,

        [EdiFieldValue("10")]
        Conduit,

        [EdiFieldValue("11")]
        PartyToBeBilled_AARAccountingRule11,

        [EdiFieldValue("12")]
        RegionalOffice,

        [EdiFieldValue("13")]
        ContractedServiceProvider,

        [EdiFieldValue("14")]
        WhollyOwnedSubsidiary,

        [EdiFieldValue("15")]
        AccountsPayableOffice,

        [EdiFieldValue("16")]
        Plant,

        [EdiFieldValue("17")]
        ConsultantsOffice,

        [EdiFieldValue("18")]
        Production,

        [EdiFieldValue("19")]
        NonProductionSupplier,

        [EdiFieldValue("1A")]
        Subgroup,

        [EdiFieldValue("1B")]
        Applicant,

        [EdiFieldValue("1C")]
        GroupPurchasingOrganization_GPO,

        [EdiFieldValue("1D")]
        CoOperative,

        [EdiFieldValue("1E")]
        HealthMaintenanceOrganization_HMO,

        [EdiFieldValue("1F")]
        Alliance,

        [EdiFieldValue("1G")]
        OncologyCenter,

        [EdiFieldValue("1H")]
        KidneyDialysisUnit,

        [EdiFieldValue("1I")]
        PreferredProviderOrganization_PPO,

        [EdiFieldValue("1J")]
        Connection,

        [EdiFieldValue("1K")]
        Franchisor,

        [EdiFieldValue("1L")]
        Franchisee,

        [EdiFieldValue("1M")]
        PreviousGroup,

        [EdiFieldValue("1N")]
        Shareholder,

        [EdiFieldValue("1O")]
        AcuteCareHospital,

        [EdiFieldValue("1P")]
        Provider,

        [EdiFieldValue("1Q")]
        MilitaryFacility,

        [EdiFieldValue("1R")]
        University_CollegeOrSchool,

        [EdiFieldValue("1S")]
        OutpatientSurgicenter,

        [EdiFieldValue("1T")]
        Physician_ClinicOrGroupPractice,

        [EdiFieldValue("1U")]
        LongTermCareFacility,

        [EdiFieldValue("1V")]
        ExtendedCareFacility,

        [EdiFieldValue("1W")]
        PsychiatricHealthFacility,

        [EdiFieldValue("1X")]
        Laboratory,

        [EdiFieldValue("1Y")]
        RetailPharmacy,

        [EdiFieldValue("1Z")]
        HomeHealthCare,

        [EdiFieldValue("20")]
        ForeignSupplier,

        [EdiFieldValue("21")]
        SmallBusiness,

        [EdiFieldValue("22")]
        MinorityOwnedBusiness_Small,

        [EdiFieldValue("23")]
        MinorityOwnedBusiness_Large,

        [EdiFieldValue("24")]
        WomanOwnedBusiness_Small,

        [EdiFieldValue("25")]
        WomanOwnedBusiness_Large,

        [EdiFieldValue("26")]
        SociallyDisadvantagedBusiness,

        [EdiFieldValue("27")]
        SmallDisadvantagedBusiness,

        [EdiFieldValue("28")]
        Subcontractor,

        [EdiFieldValue("29")]
        PrototypeSupplier,

        [EdiFieldValue("2A")]
        FederalStateCountyOrCityFacility,

        [EdiFieldValue("2B")]
        ThirdPartyAdministrator,

        [EdiFieldValue("2C")]
        CoParticipant,

        [EdiFieldValue("2D")]
        MiscellaneousHealthCareFacility,

        [EdiFieldValue("2E")]
        NonHealthCareMiscellaneousFacility,

        [EdiFieldValue("2F")]
        State,

        [EdiFieldValue("2G")]
        Assigner,

        [EdiFieldValue("2H")]
        HospitalDistrictOrAuthority,

        [EdiFieldValue("2I")]
        ChurchOperatedFacility,

        [EdiFieldValue("2J")]
        Individual,

        [EdiFieldValue("2K")]
        Partnership,

        [EdiFieldValue("2L")]
        Corporation,

        [EdiFieldValue("2M")]
        AirForceFacility,

        [EdiFieldValue("2N")]
        ArmyFacility,

        [EdiFieldValue("2O")]
        NavyFacility,

        [EdiFieldValue("2P")]
        PublicHealthServiceFacility,

        [EdiFieldValue("2Q")]
        VeteransAdministrationFacility,

        [EdiFieldValue("2R")]
        FederalFacility,

        [EdiFieldValue("2S")]
        PublicHealthServiceIndianServiceFacility,

        [EdiFieldValue("2T")]
        DepartmentOfJusticeFacility,

        [EdiFieldValue("2U")]
        OtherNotForProfitFacility,

        [EdiFieldValue("2V")]
        IndividualForProfitFacility,

        [EdiFieldValue("2W")]
        PartnershipForProfitFacility,

        [EdiFieldValue("2X")]
        CorporationForProfitFacility,

        [EdiFieldValue("2Y")]
        GeneralMedicalAndSurgicalFacility,

        [EdiFieldValue("2Z")]
        HospitalUnitOfAnInstitution_PrisonHospital_CollegeInfirmary_Etc,

        [EdiFieldValue("30")]
        ServiceSupplier,

        [EdiFieldValue("31")]
        PostalMailingAddress,

        [EdiFieldValue("32")]
        PartyToReceiveMaterialRelease,

        [EdiFieldValue("33")]
        InquiryAddress,

        [EdiFieldValue("34")]
        MaterialChangeNoticeAddress,

        [EdiFieldValue("35")]
        ElectronicDataInterchange_EDI_CoordinatorPointAddress,

        [EdiFieldValue("36")]
        Employer,

        [EdiFieldValue("37")]
        PreviousDebtHolder,

        [EdiFieldValue("38")]
        MortgageLiabilityAccountHolder,

        [EdiFieldValue("39")]
        AppraisalCompany,

        [EdiFieldValue("3A")]
        HospitalUnitWithinanInstitutionfortheMentallyRetarded,

        [EdiFieldValue("3B")]
        PsychiatricFacility,

        [EdiFieldValue("3C")]
        TuberculosisAndOtherRespiratoryDiseasesFacility,

        [EdiFieldValue("3D")]
        ObstetricsAndGynecologyFacility,

        [EdiFieldValue("3E")]
        Eye_Ear_NoseAndThroatFacility,

        [EdiFieldValue("3F")]
        RehabilitationFacility,

        [EdiFieldValue("3G")]
        OrthopedicFacility,

        [EdiFieldValue("3H")]
        ChronicDiseaseFacility,

        [EdiFieldValue("3I")]
        OtherSpecialtyFacility,

        [EdiFieldValue("3J")]
        ChildrensGeneralFacility,

        [EdiFieldValue("3K")]
        ChildrensHospitalUnitOfanInstitution,

        [EdiFieldValue("3L")]
        ChildrensPsychiatricFacility,

        [EdiFieldValue("3M")]
        ChildrensTuberculosisAndOtherRespiratoryDiseasesFacility,

        [EdiFieldValue("3N")]
        ChildrensEyeEarNoseAndThroatFacility,

        [EdiFieldValue("3O")]
        ChildrensRehabilitationFacility,

        [EdiFieldValue("3P")]
        ChildrensOrthopedicFacility,

        [EdiFieldValue("3Q")]
        ChildrensChronicDiseaseFacility,

        [EdiFieldValue("3R")]
        ChildrensOtherSpecialtyFacility,

        [EdiFieldValue("3S")]
        InstitutionforMentalRetardation,

        [EdiFieldValue("3T")]
        AlcoholismAndOtherChemicalDependencyFacility,

        [EdiFieldValue("3U")]
        GeneralInpatientCareforAIDSARCFacility,

        [EdiFieldValue("3V")]
        AIDSARCUnit,

        [EdiFieldValue("3W")]
        SpecializedOutpatientProgramforAIDSARC,

        [EdiFieldValue("3X")]
        AlcoholDrugAbuseOrDependencyInpatientUnit,

        [EdiFieldValue("3Y")]
        AlcoholDrugAbuseOrDependencyOutpatientServices,

        [EdiFieldValue("3Z")]
        ArthritisTreatmentCenter,

        [EdiFieldValue("40")]
        Receiver,

        [EdiFieldValue("41")]
        Submitter,

        [EdiFieldValue("42")]
        ComponentManufacturer,

        [EdiFieldValue("43")]
        ClaimantAuthorizedRepresentative,

        [EdiFieldValue("44")]
        DataProcessingServiceBureau,

        [EdiFieldValue("45")]
        DropOffLocation,

        [EdiFieldValue("46")]
        InvoicingDealer,

        [EdiFieldValue("47")]
        Estimator,

        [EdiFieldValue("48")]
        InServiceSource,

        [EdiFieldValue("49")]
        InitialDealer,

        [EdiFieldValue("4A")]
        BirthingRoomLDRPRoom,

        [EdiFieldValue("4B")]
        BurnCareUnit,

        [EdiFieldValue("4C")]
        CardiacCatherizationLaboratory,

        [EdiFieldValue("4D")]
        OpenHeartSurgeryFacility,

        [EdiFieldValue("4E")]
        CardiacIntensiveCareUnit,

        [EdiFieldValue("4F")]
        AngioplastyFacility,

        [EdiFieldValue("4G")]
        ChronicObstructivePulmonaryDiseaseServiceFacility,

        [EdiFieldValue("4H")]
        EmergencyDepartment,

        [EdiFieldValue("4I")]
        TraumaCenter_Certified,

        [EdiFieldValue("4J")]
        ExtracorporealShock_WaveLithotripter_ESWL_Unit,

        [EdiFieldValue("4K")]
        FitnessCenter,

        [EdiFieldValue("4L")]
        GeneticCounselingScreeningServices,

        [EdiFieldValue("4M")]
        AdultDayCareProgramFacility,

        [EdiFieldValue("4N")]
        AlzheimersDiagnosticAssessmentServices,

        [EdiFieldValue("4O")]
        ComprehensiveGeriatricAssessmentFacility,

        [EdiFieldValue("4P")]
        EmergencyResponse_Geriatric_Unit,

        [EdiFieldValue("4Q")]
        GeriatricAcuteCareUnit,

        [EdiFieldValue("4R")]
        GeriatricClinics,

        [EdiFieldValue("4S")]
        RespiteCareFacility,

        [EdiFieldValue("4T")]
        SeniorMembershipProgram,

        [EdiFieldValue("4U")]
        PatientEducationUnit,

        [EdiFieldValue("4V")]
        CommunityHealthPromotionFacility,

        [EdiFieldValue("4W")]
        WorksiteHealthPromotionFacility,

        [EdiFieldValue("4X")]
        HemodialysisFacility,

        [EdiFieldValue("4Y")]
        HomeHealthServices,

        [EdiFieldValue("4Z")]
        Hospice,

        [EdiFieldValue("50")]
        ManufacturersRepresentative,

        [EdiFieldValue("51")]
        PartsDistributor,

        [EdiFieldValue("52")]
        PartRemanufacturer,

        [EdiFieldValue("53")]
        RegisteredOwner,

        [EdiFieldValue("54")]
        OrderWriter,

        [EdiFieldValue("55")]
        ServiceManager,

        [EdiFieldValue("56")]
        ServicingDealer,

        [EdiFieldValue("57")]
        ServicingOrganization,

        [EdiFieldValue("58")]
        StoreManager,

        [EdiFieldValue("59")]
        PartyToApproveSpecification,

        [EdiFieldValue("5A")]
        MedicalSurgicalOrOtherIntensiveCareUnit,

        [EdiFieldValue("5B")]
        HisopathologyLaboratory,

        [EdiFieldValue("5C")]
        BloodBank,

        [EdiFieldValue("5D")]
        NeonatalIntensiveCareUnit,

        [EdiFieldValue("5E")]
        ObstetricsUnit,

        [EdiFieldValue("5F")]
        OccupationalHealthServices,

        [EdiFieldValue("5G")]
        OrganizedOutpatientServices,

        [EdiFieldValue("5H")]
        PediatricAcuteInpatientUnit,

        [EdiFieldValue("5I")]
        PsychiatricChildAdolescentServices,

        [EdiFieldValue("5J")]
        PsychiatricConsultation_LiaisonServices,

        [EdiFieldValue("5K")]
        PsychiatricEducationServices,

        [EdiFieldValue("5L")]
        PsychiatricEmergencyServices,

        [EdiFieldValue("5M")]
        PsychiatricGeriatricServices,

        [EdiFieldValue("5N")]
        PsychiatricInpatientUnit,

        [EdiFieldValue("5O")]
        PsychiatricOutpatientServices,

        [EdiFieldValue("5P")]
        PsychiatricPartialHospitalizationProgram,

        [EdiFieldValue("5Q")]
        MegavoltageRadiationTherapyUnit,

        [EdiFieldValue("5R")]
        RadioactiveImplantsUnit,

        [EdiFieldValue("5S")]
        TherapeuticRadioisotopeFacility,

        [EdiFieldValue("5T")]
        X_RayRadiationTherapyUnit,

        [EdiFieldValue("5U")]
        CTScannerUnit,

        [EdiFieldValue("5V")]
        DiagnosticRadioisotopeFacility,

        [EdiFieldValue("5W")]
        MagneticResonanceImaging_MRI_Facility,

        [EdiFieldValue("5X")]
        UltrasoundUnit,

        [EdiFieldValue("5Y")]
        RehabilitationInpatientUnit,

        [EdiFieldValue("5Z")]
        RehabilitationOutpatientServices,

        [EdiFieldValue("60")]
        Salesperson,

        [EdiFieldValue("61")]
        PerformedAt,

        [EdiFieldValue("62")]
        ApplicantsEmployer,

        [EdiFieldValue("63")]
        ReferencesEmployer,

        [EdiFieldValue("64")]
        CosignersEmployer,

        [EdiFieldValue("65")]
        ApplicantsReference,

        [EdiFieldValue("66")]
        ApplicantsCosigner,

        [EdiFieldValue("67")]
        ApplicantsComaker,

        [EdiFieldValue("68")]
        OwnersRepresentative,

        [EdiFieldValue("69")]
        RepairingOutlet,

        [EdiFieldValue("6A")]
        ReproductiveHealthServices,

        [EdiFieldValue("6B")]
        SkilledNursingOrOtherLong_TermCareUnit,

        [EdiFieldValue("6C")]
        SinglePhotonEmissionComputerizedTomography_SPECT_Unit,

        [EdiFieldValue("6D")]
        OrganizedSocialWorkServiceFacility,

        [EdiFieldValue("6E")]
        OutpatientSocialWorkServices,

        [EdiFieldValue("6F")]
        EmergencyDepartmentSocialWorkServices,

        [EdiFieldValue("6G")]
        SportsMedicineClinicServices,

        [EdiFieldValue("6H")]
        HospitalAuxiliaryUnit,

        [EdiFieldValue("6I")]
        PatientRepresentativeServices,

        [EdiFieldValue("6J")]
        VolunteerServicesDepartment,

        [EdiFieldValue("6K")]
        OutpatientSurgeryServices,

        [EdiFieldValue("6L")]
        OrganTissueTransplantUnit,

        [EdiFieldValue("6M")]
        OrthopedicSurgeryFacility,

        [EdiFieldValue("6N")]
        OccupationalTherapyServices,

        [EdiFieldValue("6O")]
        PhysicalTherapyServices,

        [EdiFieldValue("6P")]
        RecreationalTherapyServices,

        [EdiFieldValue("6Q")]
        RespiratoryTherapyServices,

        [EdiFieldValue("6R")]
        SpeechTherapyServices,

        [EdiFieldValue("6S")]
        WomensHealthCenterServices,

        [EdiFieldValue("6T")]
        HealthSciencesLibrary,

        [EdiFieldValue("6U")]
        CardiacRehabilitationProgramFacility,

        [EdiFieldValue("6V")]
        Non_InvasiveCardiacAssessmentServices,

        [EdiFieldValue("6W")]
        EmergencyMedicalTechnician,

        [EdiFieldValue("6X")]
        DisciplinaryContact,

        [EdiFieldValue("6Y")]
        CaseManager,

        [EdiFieldValue("6Z")]
        Advisor,

        [EdiFieldValue("70")]
        PriorIncorrectInsured,

        [EdiFieldValue("71")]
        AttendingPhysician,

        [EdiFieldValue("72")]
        OperatingPhysician,

        [EdiFieldValue("73")]
        OtherPhysician,

        [EdiFieldValue("74")]
        CorrectedInsured,

        [EdiFieldValue("75")]
        Participant,

        [EdiFieldValue("76")]
        SecondaryWarranter,

        [EdiFieldValue("77")]
        ServiceLocation,

        [EdiFieldValue("78")]
        ServiceRequester,

        [EdiFieldValue("79")]
        Warranter,

        [EdiFieldValue("7A")]
        Premises,

        [EdiFieldValue("7B")]
        Bottler,

        [EdiFieldValue("7C")]
        PlaceOfOccurrence,

        [EdiFieldValue("7D")]
        ContractingOfficerRepresentative,

        [EdiFieldValue("7E")]
        PartyAuthorizedToDefinitizeContractAction,

        [EdiFieldValue("7F")]
        FilingAddress,

        [EdiFieldValue("7G")]
        HazardousMaterialOffice,

        [EdiFieldValue("7H")]
        GovernmentFurnishedPropertyFOBPoint,

        [EdiFieldValue("7I")]
        ProjectName,

        [EdiFieldValue("7J")]
        Codefendant,

        [EdiFieldValue("7K")]
        Co_occupant,

        [EdiFieldValue("7L")]
        PreliminaryInspectionLocation,

        [EdiFieldValue("7M")]
        InspectionAndAcceptanceLocation,

        [EdiFieldValue("7N")]
        PartyToReceiveProposal,

        [EdiFieldValue("7O")]
        FederallyCharteredFacility,

        [EdiFieldValue("7P")]
        TransportationOffice,

        [EdiFieldValue("7Q")]
        PartyToWhomProtestSubmitted,

        [EdiFieldValue("7R")]
        Birthplace,

        [EdiFieldValue("7S")]
        PipelineSegment,

        [EdiFieldValue("7T")]
        HomeStateName,

        [EdiFieldValue("7U")]
        Liquidator,

        [EdiFieldValue("7V")]
        PetitioningCreditorsAttorney,

        [EdiFieldValue("7W")]
        MergedName,

        [EdiFieldValue("7X")]
        PartyRepresented,

        [EdiFieldValue("7Y")]
        ProfessionalOrganization,

        [EdiFieldValue("7Z")]
        Referee,

        [EdiFieldValue("80")]
        Hospital,

        [EdiFieldValue("81")]
        PartSource,

        [EdiFieldValue("82")]
        RenderingProvider,

        [EdiFieldValue("83")]
        SubscribersSchool,

        [EdiFieldValue("84")]
        SubscribersEmployer,

        [EdiFieldValue("85")]
        BillingProvider,

        [EdiFieldValue("86")]
        Conductor,

        [EdiFieldValue("87")]
        Pay_toProvider,

        [EdiFieldValue("88")]
        Approver,

        [EdiFieldValue("89")]
        Investor,

        [EdiFieldValue("8A")]
        VacationHome,

        [EdiFieldValue("8B")]
        PrimaryResidence,

        [EdiFieldValue("8C")]
        SecondHome,

        [EdiFieldValue("8D")]
        PermitHolder,

        [EdiFieldValue("8E")]
        MinorityInstitution,

        [EdiFieldValue("8F")]
        BailmentWarehouse,

        [EdiFieldValue("8G")]
        FirstAppraiser,

        [EdiFieldValue("8H")]
        TaxExemptOrganization,

        [EdiFieldValue("8I")]
        ServiceOrganization,

        [EdiFieldValue("8J")]
        EmergingSmallBusiness,

        [EdiFieldValue("8K")]
        SurplusDealer,

        [EdiFieldValue("8L")]
        PollingSite,

        [EdiFieldValue("8M")]
        SociallyDisadvantagedIndividual,

        [EdiFieldValue("8N")]
        EconomicallyDisadvantagedIndividual,

        [EdiFieldValue("8O")]
        DisabledIndividual,

        [EdiFieldValue("8P")]
        Producer,

        [EdiFieldValue("8Q")]
        PublicOrPrivateOrganizationfortheDisabled,

        [EdiFieldValue("8R")]
        ConsumerServiceProvider_CSP_Customer,

        [EdiFieldValue("8S")]
        ConsumerServiceProvider_CSP,

        [EdiFieldValue("8T")]
        Voter,

        [EdiFieldValue("8U")]
        NativeHawaiianOrganization,

        [EdiFieldValue("8V")]
        PrimaryIntra_LATA_LocalAccessTransportArea_Carrier,

        [EdiFieldValue("8W")]
        PaymentAddress,

        [EdiFieldValue("8X")]
        OilAndGasCustodian,

        [EdiFieldValue("8Y")]
        RegisteredOffice,

        [EdiFieldValue("8Z")]
        JointDebtorAttorney_8Z,

        [EdiFieldValue("90")]
        PreviousBusinessPartner,

        [EdiFieldValue("91")]
        ActionParty,

        [EdiFieldValue("92")]
        SupportParty,

        [EdiFieldValue("93")]
        InsuranceInstitute,

        [EdiFieldValue("94")]
        NewSupplySource,

        [EdiFieldValue("95")]
        ResearchInstitute,

        [EdiFieldValue("96")]
        DebtorCompany,

        [EdiFieldValue("97")]
        PartyWaivingRequirements,

        [EdiFieldValue("98")]
        FreightManagementFacilitator,

        [EdiFieldValue("99")]
        OuterContinentalShelf_OCS_AreaLocation,

        [EdiFieldValue("9A")]
        DebtorIndividual,

        [EdiFieldValue("9B")]
        CountryOfExport,

        [EdiFieldValue("9C")]
        CountryOfDestination,

        [EdiFieldValue("9D")]
        NewServiceProvider,

        [EdiFieldValue("9E")]
        Sub_servicer,

        [EdiFieldValue("9F")]
        LossPayee,

        [EdiFieldValue("9G")]
        Nickname,

        [EdiFieldValue("9H")]
        Assignee,

        [EdiFieldValue("9I")]
        RegisteredPrincipal,

        [EdiFieldValue("9J")]
        AdditionalDebtor,

        [EdiFieldValue("9K")]
        KeyPerson,

        [EdiFieldValue("9L")]
        IncorporatedBy,

        [EdiFieldValue("9N")]
        PartyToLease,

        [EdiFieldValue("9O")]
        PartyToContract,

        [EdiFieldValue("9P")]
        Investigator,

        [EdiFieldValue("9Q")]
        LastSupplier,

        [EdiFieldValue("9R")]
        DownstreamFirstSupplier,

        [EdiFieldValue("9S")]
        Co_Investigator,

        [EdiFieldValue("9T")]
        TelephoneAnsweringServiceBureau,

        [EdiFieldValue("9U")]
        Author,

        [EdiFieldValue("9V")]
        FirstSupplier,

        [EdiFieldValue("9W")]
        UltimateParentCompany,

        [EdiFieldValue("9X")]
        ContractualReceiptMeter,

        [EdiFieldValue("9Y")]
        ContractualDeliveryMeter,

        [EdiFieldValue("9Z")]
        Co_debtor,

        [EdiFieldValue("A1")]
        Adjuster,

        [EdiFieldValue("A2")]
        Woman_OwnedBusiness,

        [EdiFieldValue("A3")]
        LaborSurplusAreaFirm,

        [EdiFieldValue("A4")]
        OtherDisadvantagedBusiness,

        [EdiFieldValue("A5")]
        Veteran_OwnedBusiness,

        [EdiFieldValue("A6")]
        Section8a_ProgramParticipantFirm,

        [EdiFieldValue("A7")]
        ShelteredWorkshop,

        [EdiFieldValue("A8")]
        NonprofitInstitution,

        [EdiFieldValue("A9")]
        SalesOffice,

        [EdiFieldValue("AA")]
        AuthorityForShipment,

        [EdiFieldValue("AB")]
        AdditionalPickUpAddress,

        [EdiFieldValue("AC")]
        AirCargoCompany,

        [EdiFieldValue("AD")]
        PartyToBeadvised_Writtenorders,

        [EdiFieldValue("AE")]
        AdditionalDeliveryAddress,

        [EdiFieldValue("AF")]
        AuthorizedAcceptingOfficial,

        [EdiFieldValue("AG")]
        AgentAgency,

        [EdiFieldValue("AH")]
        Advertiser,

        [EdiFieldValue("AI")]
        Airline,

        [EdiFieldValue("AJ")]
        AllegedDebtor,

        [EdiFieldValue("AK")]
        PartyToWhomAcknowledgmentShouldBeSent,

        [EdiFieldValue("AL")]
        AllotmentCustomer,

        [EdiFieldValue("AM")]
        AssistantUSTrustee,

        [EdiFieldValue("AN")]
        AuthorizedFrom,

        [EdiFieldValue("AO")]
        AccountOf,

        [EdiFieldValue("AP")]
        AccountOf_OriginParty,

        [EdiFieldValue("AQ")]
        AccountOf_DestinationParty,

        [EdiFieldValue("AR")]
        ArmedServicesLocationDesignation,

        [EdiFieldValue("AS")]
        PostsecondaryEducationSender,

        [EdiFieldValue("AT")]
        PostsecondaryEducationRecipient,

        [EdiFieldValue("AU")]
        PartyAuthorizingDisposition,

        [EdiFieldValue("AV")]
        AuthorizedTo,

        [EdiFieldValue("AW")]
        Accountant,

        [EdiFieldValue("AX")]
        Plaintiff,

        [EdiFieldValue("AY")]
        Clearinghouse,

        [EdiFieldValue("AZ")]
        PreviousName,

        [EdiFieldValue("B1")]
        ConstructionFirm,

        [EdiFieldValue("B2")]
        OtherUnlistedTypeOfOrganizationalEntity,

        [EdiFieldValue("B3")]
        PreviousNameOfFirm,

        [EdiFieldValue("B4")]
        ParentCompany,

        [EdiFieldValue("B5")]
        AffiliatedCompany,

        [EdiFieldValue("B6")]
        RegisteringParentParty,

        [EdiFieldValue("B7")]
        RegisteringNonparentParty,

        [EdiFieldValue("B8")]
        RegularDealer,

        [EdiFieldValue("B9")]
        LargeBusiness,

        [EdiFieldValue("BA")]
        Battery,

        [EdiFieldValue("BB")]
        BusinessPartner,

        [EdiFieldValue("BC")]
        Broadcaster,

        [EdiFieldValue("BD")]
        Bill_toPartyforDiversionCharges,

        [EdiFieldValue("BE")]
        Beneficiary,

        [EdiFieldValue("BF")]
        BilledFrom,

        [EdiFieldValue("BG")]
        BuyingGroup,

        [EdiFieldValue("BH")]
        InterimTrustee,

        [EdiFieldValue("BI")]
        TrusteesAttorney,

        [EdiFieldValue("BJ")]
        CoCounsel,

        [EdiFieldValue("BK")]
        Bank,

        [EdiFieldValue("BL")]
        PartyToReceiveBillOfLading,

        [EdiFieldValue("BM")]
        Brakeman,

        [EdiFieldValue("BN")]
        BeneficialOwner,

        [EdiFieldValue("BO")]
        BrokerOrSalesOffice,

        [EdiFieldValue("BP")]
        SpecialCounsel,

        [EdiFieldValue("BQ")]
        AttorneyforDefendantPrivate,

        [EdiFieldValue("BR")]
        Broker,

        [EdiFieldValue("BS")]
        BillAndShipTo,

        [EdiFieldValue("BT")]
        BillToParty,

        [EdiFieldValue("BU")]
        PlaceOfBusiness,

        [EdiFieldValue("BV")]
        BillingService,

        [EdiFieldValue("BW")]
        Borrower,

        [EdiFieldValue("BX")]
        AttorneyforPlaintiff,

        [EdiFieldValue("BY")]
        BuyingParty_Purchaser,

        [EdiFieldValue("BZ")]
        BusinessAssociate,

        [EdiFieldValue("C1")]
        InCareOfPartyno1,

        [EdiFieldValue("C2")]
        InCareOfPartyno2,

        [EdiFieldValue("C3")]
        CircuitLocationIdentifier,

        [EdiFieldValue("C4")]
        ContractAdministrationOffice,

        [EdiFieldValue("C5")]
        PartySubmittingQuote,

        [EdiFieldValue("C6")]
        Municipality,

        [EdiFieldValue("C7")]
        County,

        [EdiFieldValue("C8")]
        City,

        [EdiFieldValue("C9")]
        ContractHolder,

        [EdiFieldValue("CA")]
        Carrier,

        [EdiFieldValue("CB")]
        CustomsBroker,

        [EdiFieldValue("CC")]
        Claimant,

        [EdiFieldValue("CD")]
        Consignee_ToReceiveMailAndSmallParcels,

        [EdiFieldValue("CE")]
        Consignee_ToreceivelargeparcelsAndfreight,

        [EdiFieldValue("CF")]
        SubsidiaryDivision,

        [EdiFieldValue("CG")]
        CarnetIssuer,

        [EdiFieldValue("CH")]
        ChassisProvider,

        [EdiFieldValue("CI")]
        Consignor,

        [EdiFieldValue("CJ")]
        AutomatedDataProcessing_ADP_Point,

        [EdiFieldValue("CK")]
        Pharmacist,

        [EdiFieldValue("CL")]
        ContainerLocation,

        [EdiFieldValue("CM")]
        Customs,

        [EdiFieldValue("CN")]
        Consignee,

        [EdiFieldValue("CO")]
        OceanTariffConference,

        [EdiFieldValue("CP")]
        PartyToReceiveCertOfCompliance,

        [EdiFieldValue("CQ")]
        CorporateOffice,

        [EdiFieldValue("CR")]
        ContainerReturnCompany,

        [EdiFieldValue("CS")]
        Consolidator,

        [EdiFieldValue("CT")]
        CountryOfOrigin,

        [EdiFieldValue("CU")]
        CoatingOrPaintSupplier,

        [EdiFieldValue("CV")]
        Converter,

        [EdiFieldValue("CW")]
        AccountingStation,

        [EdiFieldValue("CX")]
        ClaimAdministrator,

        [EdiFieldValue("CY")]
        Country,

        [EdiFieldValue("CZ")]
        AdmittingSurgeon,

        [EdiFieldValue("D1")]
        Driver,

        [EdiFieldValue("D2")]
        CommercialInsurer,

        [EdiFieldValue("D3")]
        Defendant,

        [EdiFieldValue("D4")]
        Debtor,

        [EdiFieldValue("D5")]
        DebtorInPossession,

        [EdiFieldValue("D6")]
        ConsolidatedDebtor,

        [EdiFieldValue("D7")]
        PetitioningCreditor,

        [EdiFieldValue("D8")]
        Dispatcher,

        [EdiFieldValue("D9")]
        CreditorsAttorney,

        [EdiFieldValue("DA")]
        DeliveryAddress,

        [EdiFieldValue("DB")]
        DistributorBranch,

        [EdiFieldValue("DC")]
        DestinationCarrier,

        [EdiFieldValue("DD")]
        AssistantSurgeon,

        [EdiFieldValue("DE")]
        Depositor,

        [EdiFieldValue("DF")]
        MaterialDispositionAuthorizationLocation,

        [EdiFieldValue("DG")]
        DesignEngineering,

        [EdiFieldValue("DH")]
        DoingBusinessAs,

        [EdiFieldValue("DI")]
        DifferentPremiseAddress_DPA,

        [EdiFieldValue("DJ")]
        ConsultingPhysician,

        [EdiFieldValue("DK")]
        OrderingPhysician,

        [EdiFieldValue("DL")]
        Dealer,

        [EdiFieldValue("DM")]
        DestinationMailFacility,

        [EdiFieldValue("DN")]
        ReferringProvider,

        [EdiFieldValue("DO")]
        DependentName,

        [EdiFieldValue("DP")]
        PartyToProvideDiscount,

        [EdiFieldValue("DQ")]
        SupervisingPhysician,

        [EdiFieldValue("DR")]
        DestinationDrayman,

        [EdiFieldValue("DS")]
        Distributor,

        [EdiFieldValue("DT")]
        DestinationTerminal,

        [EdiFieldValue("DU")]
        ResaleDealer,

        [EdiFieldValue("DV")]
        Division,

        [EdiFieldValue("DW")]
        DownstreamParty,

        [EdiFieldValue("DX")]
        Distiller,

        [EdiFieldValue("DY")]
        DefaultForeclosureSpecialist,

        [EdiFieldValue("DZ")]
        DeliveryZone,

        [EdiFieldValue("E1")]
        PersonOrOtherEntityLegallyResponsibleforaChild,

        [EdiFieldValue("E2")]
        PersonOrOtherEntityWithWhomaChildResides,

        [EdiFieldValue("E3")]
        PersonOrOtherEntityLegallyResponsibleforAndWithWhomaChildResides,

        [EdiFieldValue("E4")]
        OtherPersonOrEntityAssociatedwithStudent,

        [EdiFieldValue("E5")]
        Examiner,

        [EdiFieldValue("E6")]
        Engineering,

        [EdiFieldValue("E7")]
        PreviousEmployer,

        [EdiFieldValue("E8")]
        InquiringParty,

        [EdiFieldValue("E9")]
        ParticipatingLaboratory,

        [EdiFieldValue("EA")]
        StudySubmitter,

        [EdiFieldValue("EB")]
        EligiblePartyToTheContract,

        [EdiFieldValue("EC")]
        Exchanger,

        [EdiFieldValue("ED")]
        ExcludedParty,

        [EdiFieldValue("EE")]
        LocationOfGoodsforCustomsExaminationBeforeClearance,

        [EdiFieldValue("EF")]
        ElectronicFiler,

        [EdiFieldValue("EG")]
        Engineer,

        [EdiFieldValue("EH")]
        Exhibitor,

        [EdiFieldValue("EI")]
        ExecutorOfEstate,

        [EdiFieldValue("EJ")]
        PrincipalPerson,

        [EdiFieldValue("EK")]
        AnimalSource,

        [EdiFieldValue("EL")]
        EstablishedLocation,

        [EdiFieldValue("EM")]
        PartyToReceiveElectronicMemoOfInvoice,

        [EdiFieldValue("EN")]
        EndUser,

        [EdiFieldValue("EO")]
        LimitedLiabilityPartnership,

        [EdiFieldValue("EP")]
        EligiblePartyTotheRate,

        [EdiFieldValue("EQ")]
        OldDebtor,

        [EdiFieldValue("ER")]
        NewDebtor,

        [EdiFieldValue("ES")]
        EmployerName,

        [EdiFieldValue("ET")]
        PlanAdministrator,

        [EdiFieldValue("EU")]
        OldSecuredParty,

        [EdiFieldValue("EV")]
        SellingAgent,

        [EdiFieldValue("EW")]
        ServicingBroker,

        [EdiFieldValue("EX")]
        Exporter,

        [EdiFieldValue("EY")]
        EmployeeName,

        [EdiFieldValue("EZ")]
        NewSecuredParty,

        [EdiFieldValue("F1")]
        Company_OwnedOilField,

        [EdiFieldValue("F2")]
        EnergyInformationAdministration_DepartmentOfEnergy__OwnedOilField,

        [EdiFieldValue("F3")]
        SpecializedMobileRadioService_SMRS_Licensee,

        [EdiFieldValue("F4")]
        FormerResidence,

        [EdiFieldValue("F5")]
        RadioControlStationLocation,

        [EdiFieldValue("F6")]
        SmallControlStationLocation,

        [EdiFieldValue("F7")]
        SmallBaseStationLocation,

        [EdiFieldValue("F8")]
        AntennaSite,

        [EdiFieldValue("F9")]
        AreaOfOperation,

        [EdiFieldValue("FA")]
        Facility,

        [EdiFieldValue("FB")]
        FirstBreakTerminal,

        [EdiFieldValue("FC")]
        CustomerIdentificationFile_CIF_CustomerIdentifier,

        [EdiFieldValue("FD")]
        PhysicalAddress,

        [EdiFieldValue("FE")]
        MailAddress,

        [EdiFieldValue("FF")]
        ForeignLanguageSynonym,

        [EdiFieldValue("FG")]
        TradeNameSynonym,

        [EdiFieldValue("FH")]
        PartyToReceiveLimitationsOfHeavyElementsReport,

        [EdiFieldValue("FI")]
        NameVariationSynonym,

        [EdiFieldValue("FJ")]
        FirstContact,

        [EdiFieldValue("FL")]
        PrimaryControlPointLocation,

        [EdiFieldValue("FM")]
        Fireman,

        [EdiFieldValue("FN")]
        FilerName,

        [EdiFieldValue("FO")]
        FieldOrBranchOffice,

        [EdiFieldValue("FP")]
        NameonCreditCard,

        [EdiFieldValue("FQ")]
        PierName,

        [EdiFieldValue("FR")]
        MessageFrom,

        [EdiFieldValue("FS")]
        FinalScheduledDestination,

        [EdiFieldValue("FT")]
        NewAssignee,

        [EdiFieldValue("FU")]
        OldAssignee,

        [EdiFieldValue("FV")]
        VesselName,

        [EdiFieldValue("FW")]
        Forwarder,

        [EdiFieldValue("FX")]
        ClosedDoorPharmacy,

        [EdiFieldValue("FY")]
        VeterinaryHospital,

        [EdiFieldValue("FZ")]
        ChildrensDayCareCenter,

        [EdiFieldValue("G0")]
        DependentInsured,

        [EdiFieldValue("G1")]
        BankruptcyTrustee,

        [EdiFieldValue("G2")]
        Annuitant,

        [EdiFieldValue("G3")]
        Clinic,

        [EdiFieldValue("G5")]
        ContingentBeneficiary,

        [EdiFieldValue("G6")]
        EntityHoldingtheInformation,

        [EdiFieldValue("G7")]
        EntityProvidingtheService,

        [EdiFieldValue("G8")]
        EntityResponsibleforFollow_up,

        [EdiFieldValue("G9")]
        FamilyMember,

        [EdiFieldValue("GA")]
        GasPlant,

        [EdiFieldValue("GB")]
        OtherInsured,

        [EdiFieldValue("GC")]
        PreviousCreditGrantor,

        [EdiFieldValue("GD")]
        Guardian,

        [EdiFieldValue("GE")]
        GeneralAgency,

        [EdiFieldValue("GF")]
        InspectionCompany,

        [EdiFieldValue("GG")]
        Intermediary,

        [EdiFieldValue("GH")]
        MotorVehicleReportProviderCompany,

        [EdiFieldValue("GI")]
        Paramedic,

        [EdiFieldValue("GJ")]
        ParamedicalCompany,

        [EdiFieldValue("GK")]
        PreviousInsured,

        [EdiFieldValue("GL")]
        PreviousResidence,

        [EdiFieldValue("GM")]
        SpouseInsured,

        [EdiFieldValue("GN")]
        Garnishee,

        [EdiFieldValue("GO")]
        PrimaryBeneficiary,

        [EdiFieldValue("GP")]
        GatewayProvider,

        [EdiFieldValue("GQ")]
        ProposedInsured,

        [EdiFieldValue("GR")]
        Reinsurer,

        [EdiFieldValue("GS")]
        GaragedLocation,

        [EdiFieldValue("GT")]
        CreditGrantor,

        [EdiFieldValue("GU")]
        GuaranteeAgency,

        [EdiFieldValue("GV")]
        GasTransactionEndingPoint,

        [EdiFieldValue("GW")]
        Group,

        [EdiFieldValue("GX")]
        Retrocessionaire,

        [EdiFieldValue("GY")]
        TreatmentFacility,

        [EdiFieldValue("GZ")]
        Grandparent,

        [EdiFieldValue("H1")]
        Representative,

        [EdiFieldValue("H2")]
        Sub_Office,

        [EdiFieldValue("H3")]
        District,

        [EdiFieldValue("H5")]
        PayingAgent,

        [EdiFieldValue("H6")]
        SchoolDistrict,

        [EdiFieldValue("H7")]
        GroupAffiliate,

        [EdiFieldValue("H8")]
        ServicingAgent_H8,

        [EdiFieldValue("H9")]
        Designer,

        [EdiFieldValue("HA")]
        Owner_HA,

        [EdiFieldValue("HB")]
        HistoricallyBlackCollegeOrUniversity,

        [EdiFieldValue("HC")]
        JointAnnuitant,

        [EdiFieldValue("HD")]
        ContingentAnnuitant,

        [EdiFieldValue("HE")]
        ContingentOwner,

        [EdiFieldValue("HF")]
        HealthcareProfessionalShortageArea_HPSA_Facility,

        [EdiFieldValue("HG")]
        BrokerOpinionOrAnalysisRequester,

        [EdiFieldValue("HH")]
        HomeHealthAgency,

        [EdiFieldValue("HI")]
        ListingCompany,

        [EdiFieldValue("HJ")]
        AutomatedUnderwritingSystem,

        [EdiFieldValue("HK")]
        Subscriber,

        [EdiFieldValue("HL")]
        DocumentCustodian,

        [EdiFieldValue("HM")]
        CompetitivePropertyListing,

        [EdiFieldValue("HN")]
        CompetingProperty,

        [EdiFieldValue("HO")]
        ComparablePropertyListing,

        [EdiFieldValue("HP")]
        ClosedSale,

        [EdiFieldValue("HQ")]
        SourcePartyOfInformation,

        [EdiFieldValue("HR")]
        SubjectOfInquiry,

        [EdiFieldValue("HS")]
        HighSchool,

        [EdiFieldValue("HT")]
        StateCharteredFacility,

        [EdiFieldValue("HU")]
        Subsidiary,

        [EdiFieldValue("HV")]
        TaxAddress,

        [EdiFieldValue("HW")]
        DesignatedHazardousWasteFacility,

        [EdiFieldValue("HX")]
        TransporterOfHazardousWaste,

        [EdiFieldValue("HY")]
        Charity,

        [EdiFieldValue("HZ")]
        HazardousWasteGenerator,

        [EdiFieldValue("I1")]
        InterestedParty,

        [EdiFieldValue("I3")]
        IndependentPhysiciansAssociation_IPA,

        [EdiFieldValue("I4")]
        IntellectualPropertyOwner,

        [EdiFieldValue("I9")]
        Interviewer,

        [EdiFieldValue("IA")]
        InstalledAt,

        [EdiFieldValue("IB")]
        IndustryBureau,

        [EdiFieldValue("IC")]
        IntermediateConsignee,

        [EdiFieldValue("ID")]
        IssuerOfDebitOrCreditMemo,

        [EdiFieldValue("IE")]
        OtherIndividualDisabilityCarrier,

        [EdiFieldValue("IF")]
        InternationalFreightForwarder,

        [EdiFieldValue("II")]
        IssuerOfInvoice,

        [EdiFieldValue("IJ")]
        InjectionPoint,

        [EdiFieldValue("IK")]
        IntermediateCarrier,

        [EdiFieldValue("IL")]
        InsuredOrSubscriber,

        [EdiFieldValue("IM")]
        Importer,

        [EdiFieldValue("IN")]
        Insurer,

        [EdiFieldValue("IO")]
        Inspector,

        [EdiFieldValue("IP")]
        IndependentAdjuster,

        [EdiFieldValue("IQ")]
        In_patientPharmacy,

        [EdiFieldValue("IR")]
        SelfInsured,

        [EdiFieldValue("IS")]
        PartyToReceiveCertifiedInspectionReport,

        [EdiFieldValue("IT")]
        InstallationonSite,

        [EdiFieldValue("IU")]
        Issuer,

        [EdiFieldValue("IV")]
        Renter,

        [EdiFieldValue("J1")]
        AssociateGeneralAgent,

        [EdiFieldValue("J2")]
        AuthorizedEntity,

        [EdiFieldValue("J3")]
        BrokersAssistant,

        [EdiFieldValue("J4")]
        Custodian,

        [EdiFieldValue("J5")]
        IrrevocableBeneficiary,

        [EdiFieldValue("J6")]
        PowerOfAttorney,

        [EdiFieldValue("J7")]
        TrustOfficer,

        [EdiFieldValue("J8")]
        BrokerDealer,

        [EdiFieldValue("J9")]
        CommunityAgent,

        [EdiFieldValue("JA")]
        DairyDepartment,

        [EdiFieldValue("JB")]
        DelicatessenDepartment,

        [EdiFieldValue("JC")]
        DryGroceryDepartment,

        [EdiFieldValue("JD")]
        Judge,

        [EdiFieldValue("JE")]
        FrozenDepartment,

        [EdiFieldValue("JF")]
        GeneralMerchandiseDepartment,

        [EdiFieldValue("JG")]
        HealthAndBeautyDepartment,

        [EdiFieldValue("JH")]
        AlcoholBeverageDepartment,

        [EdiFieldValue("JI")]
        MeatDepartment,

        [EdiFieldValue("JJ")]
        ProduceDepartment,

        [EdiFieldValue("JK")]
        BakeryDepartment,

        [EdiFieldValue("JL")]
        VideoDepartment,

        [EdiFieldValue("JM")]
        CandyAndConfectionsDepartment,

        [EdiFieldValue("JN")]
        CigarettesAndTobaccoDepartment,

        [EdiFieldValue("JO")]
        In_StoreBakeryDepartment,

        [EdiFieldValue("JP")]
        FloralDepartment,

        [EdiFieldValue("JQ")]
        PharmacyDepartment,

        [EdiFieldValue("JR")]
        Bidder,

        [EdiFieldValue("JS")]
        JointDebtorAttorney_JS,

        [EdiFieldValue("JT")]
        JointDebtor,

        [EdiFieldValue("JU")]
        Jurisdiction,

        [EdiFieldValue("JV")]
        JointOwner,

        [EdiFieldValue("JW")]
        JointVenture,

        [EdiFieldValue("JX")]
        ClosingAgent,

        [EdiFieldValue("JY")]
        FinancialPlanner,

        [EdiFieldValue("JZ")]
        ManagingGeneralAgent,

        [EdiFieldValue("K1")]
        ContractorCognizantSecurityOffice,

        [EdiFieldValue("K2")]
        SubcontractorCognizantSecurityOffice,

        [EdiFieldValue("K3")]
        PlaceOfPerformanceCognizantSecurityOffice,

        [EdiFieldValue("K4")]
        PartyAuthorizingReleaseOfSecurityInformation,

        [EdiFieldValue("K5")]
        PartyToReceiveContractSecurityClassificationSpecification,

        [EdiFieldValue("K6")]
        PolicyWritingAgent,

        [EdiFieldValue("K7")]
        RadioStation,

        [EdiFieldValue("K8")]
        FilingLocation,

        [EdiFieldValue("K9")]
        PreviousDistributor,

        [EdiFieldValue("KA")]
        ItemManager,

        [EdiFieldValue("KB")]
        CustomerforWhomSameOrSimilarWorkWasPerformed,

        [EdiFieldValue("KC")]
        PartyThatReceivedDisclosureStatement,

        [EdiFieldValue("KD")]
        Proposer,

        [EdiFieldValue("KE")]
        ContactOffice,

        [EdiFieldValue("KF")]
        AuditOffice,

        [EdiFieldValue("KG")]
        ProjectManager,

        [EdiFieldValue("KH")]
        OrganizationHavingSourceControl,

        [EdiFieldValue("KI")]
        UnitedStatesOverseasSecurityAdministrationOffice,

        [EdiFieldValue("KJ")]
        QualifyingOfficer,

        [EdiFieldValue("KK")]
        RegisteringParty,

        [EdiFieldValue("KL")]
        ClerkOfCourt,

        [EdiFieldValue("KM")]
        Coordinator,

        [EdiFieldValue("KN")]
        FormerAddress,

        [EdiFieldValue("KO")]
        PlantClearanceOfficer,

        [EdiFieldValue("KP")]
        NameUnderWhichFiled,

        [EdiFieldValue("KQ")]
        Licensee,

        [EdiFieldValue("KR")]
        Pre_kindergartenToGrade12Recipient,

        [EdiFieldValue("KS")]
        Pre_kindergartenToGrade12Sender,

        [EdiFieldValue("KT")]
        Court,

        [EdiFieldValue("KU")]
        ReceiverSite,

        [EdiFieldValue("KV")]
        DisbursingOfficer,

        [EdiFieldValue("KW")]
        BidOpeningLocation,

        [EdiFieldValue("KX")]
        FreeonBoardPoint,

        [EdiFieldValue("KY")]
        TechnicalOffice,

        [EdiFieldValue("KZ")]
        AcceptanceLocation,

        [EdiFieldValue("L1")]
        InspectionLocation,

        [EdiFieldValue("L2")]
        LocationOfPrincipalAssets,

        [EdiFieldValue("L3")]
        LoanCorrespondent,

        [EdiFieldValue("L5")]
        Contact,

        [EdiFieldValue("L8")]
        HeadOffice,

        [EdiFieldValue("L9")]
        InformationProvider,

        [EdiFieldValue("LA")]
        Attorney,

        [EdiFieldValue("LB")]
        LastBreakTerminal,

        [EdiFieldValue("LC")]
        LocationOfSpotforStorage,

        [EdiFieldValue("LD")]
        LiabilityHolder,

        [EdiFieldValue("LE")]
        Lessor,

        [EdiFieldValue("LF")]
        LimitedPartner,

        [EdiFieldValue("LG")]
        LocationOfGoods,

        [EdiFieldValue("LH")]
        Pipeline,

        [EdiFieldValue("LI")]
        IndependentLab,

        [EdiFieldValue("LJ")]
        LimitedLiabilityCompany,

        [EdiFieldValue("LK")]
        JuvenileOwner,

        [EdiFieldValue("LL")]
        LocationOfLoadExchange_Export,

        [EdiFieldValue("LM")]
        LendingInstitution,

        [EdiFieldValue("LN")]
        Lender,

        [EdiFieldValue("LO")]
        LoanOriginator,

        [EdiFieldValue("LP")]
        LoadingParty,

        [EdiFieldValue("LQ")]
        LawFirm,

        [EdiFieldValue("LR")]
        LegalRepresentative,

        [EdiFieldValue("LS")]
        Lessee,

        [EdiFieldValue("LT")]
        Long_termDisabilityCarrier,

        [EdiFieldValue("LU")]
        MasterAgent,

        [EdiFieldValue("LV")]
        LoanServicer,

        [EdiFieldValue("LW")]
        Customer,

        [EdiFieldValue("LY")]
        Labeler,

        [EdiFieldValue("LZ")]
        LocalChain,

        [EdiFieldValue("M1")]
        SourceMeterLocation,

        [EdiFieldValue("M2")]
        ReceiptMeterLocation,

        [EdiFieldValue("M3")]
        UpstreamMeterLocation,

        [EdiFieldValue("M4")]
        DownstreamMeterLocation,

        [EdiFieldValue("M5")]
        MigrantHealthClinic,

        [EdiFieldValue("M6")]
        Landlord,

        [EdiFieldValue("M7")]
        ForeclosingLender,

        [EdiFieldValue("M8")]
        EducationalInstitution,

        [EdiFieldValue("M9")]
        Manufacturing,

        [EdiFieldValue("MA")]
        PartyforwhomItemisUltimatelyIntended,

        [EdiFieldValue("MB")]
        CompanyInterviewerWorksFor,

        [EdiFieldValue("MC")]
        MotorCarrier,

        [EdiFieldValue("MD")]
        VeteransAdministrationLoanGuarantyAuthority,

        [EdiFieldValue("ME")]
        VeteransAdministrationLoanAuthorizedSupplier,

        [EdiFieldValue("MF")]
        ManufacturerOfGoods,

        [EdiFieldValue("MG")]
        GovernmentLoanAgencySponsorOrAgent,

        [EdiFieldValue("MH")]
        MortgageInsurer,

        [EdiFieldValue("MI")]
        PlanningScheduleMaterialReleaseIssuer,

        [EdiFieldValue("MJ")]
        FinancialInstitution,

        [EdiFieldValue("MK")]
        LoanHolderforRealEstateAsset,

        [EdiFieldValue("ML")]
        ConsumerCreditAccountCompany,

        [EdiFieldValue("MM")]
        MortgageCompany,

        [EdiFieldValue("MN")]
        AuthorizedMarketer,

        [EdiFieldValue("MO")]
        ReleaseDrayman,

        [EdiFieldValue("MP")]
        ManufacturingPlant,

        [EdiFieldValue("MQ")]
        MeteringLocation,

        [EdiFieldValue("MR")]
        MedicalInsuranceCarrier,

        [EdiFieldValue("MS")]
        BureauOfLandManagement_MineralsManagementService_PropertyUnit,

        [EdiFieldValue("MT")]
        Material,

        [EdiFieldValue("MU")]
        MeetingLocation,

        [EdiFieldValue("MV")]
        Mainline,

        [EdiFieldValue("MW")]
        MarineSurveyor,

        [EdiFieldValue("MX")]
        JuvenileWitness,

        [EdiFieldValue("MY")]
        MasterGeneralAgent,

        [EdiFieldValue("MZ")]
        Minister,

        [EdiFieldValue("N1")]
        NotifyPartyNo1,

        [EdiFieldValue("N2")]
        NotifyPartyNo2,

        [EdiFieldValue("N3")]
        IneligibleParty,

        [EdiFieldValue("N4")]
        PriceAdministration,

        [EdiFieldValue("N5")]
        PartyWhoSignedtheDeliveryReceipt,

        [EdiFieldValue("N6")]
        NonemploymentIncomeSource,

        [EdiFieldValue("N7")]
        PreviousNeighbor,

        [EdiFieldValue("N8")]
        Relative,

        [EdiFieldValue("N9")]
        Neighborhood,

        [EdiFieldValue("NB")]
        Neighbor,

        [EdiFieldValue("NC")]
        Cross_TownSwitch,

        [EdiFieldValue("ND")]
        NextDestination,

        [EdiFieldValue("NE")]
        Newspaper,

        [EdiFieldValue("NF")]
        OwnerAnnuitant,

        [EdiFieldValue("NG")]
        Administrator,

        [EdiFieldValue("NH")]
        Association,

        [EdiFieldValue("NI")]
        Non_insured,

        [EdiFieldValue("NJ")]
        TrustOrEstate,

        [EdiFieldValue("NK")]
        NationalChain,

        [EdiFieldValue("NL")]
        Non_railroadEntity,

        [EdiFieldValue("NM")]
        Physician_Specialists,

        [EdiFieldValue("NN")]
        NetworkName,

        [EdiFieldValue("NP")]
        NotifyPartyforShippersOrder,

        [EdiFieldValue("NQ")]
        PipelineSegmentBoundary,

        [EdiFieldValue("NR")]
        GasTransactionStartingPoint,

        [EdiFieldValue("NS")]
        Non_TemporaryStorageFacility,

        [EdiFieldValue("NT")]
        MagistrateJudge,

        [EdiFieldValue("NU")]
        FormerlyKnownAs,

        [EdiFieldValue("NV")]
        FormerlyDoingBusinessAs,

        [EdiFieldValue("NW")]
        MaidenName,

        [EdiFieldValue("NX")]
        PrimaryOwner,

        [EdiFieldValue("NY")]
        BirthName,

        [EdiFieldValue("NZ")]
        PrimaryPhysician,

        [EdiFieldValue("O1")]
        OriginatingBank,

        [EdiFieldValue("O2")]
        OriginatingCompany,

        [EdiFieldValue("O3")]
        ReceivingCompany,

        [EdiFieldValue("O4")]
        Factor,

        [EdiFieldValue("O5")]
        MerchantBanker,

        [EdiFieldValue("O6")]
        NonRegisteredBusinessName,

        [EdiFieldValue("O7")]
        RegisteredBusinessName,

        [EdiFieldValue("O8")]
        Registrar,

        [EdiFieldValue("OA")]
        ElectronicReturnOriginator,

        [EdiFieldValue("OB")]
        OrderedBy,

        [EdiFieldValue("OC")]
        OriginCarrier,

        [EdiFieldValue("OD")]
        DoctorOfOptometry,

        [EdiFieldValue("OE")]
        BookingOffice,

        [EdiFieldValue("OF")]
        OffsetOperator,

        [EdiFieldValue("OG")]
        CoOwner,

        [EdiFieldValue("OH")]
        OtherDepartments,

        [EdiFieldValue("OI")]
        OutsideInspectionAgency,

        [EdiFieldValue("OK")]
        Owner_OK,

        [EdiFieldValue("OL")]
        Officer,

        [EdiFieldValue("OM")]
        OriginMailFacility,

        [EdiFieldValue("ON")]
        ProductPositionHolder,

        [EdiFieldValue("OO")]
        OrderOf_ShippersOrders_Transportation,

        [EdiFieldValue("OP")]
        OperatorOfpropertyOrunit,

        [EdiFieldValue("OR")]
        OriginDrayman,

        [EdiFieldValue("OS")]
        OverrideInstitution,

        [EdiFieldValue("OT")]
        OriginTerminal,

        [EdiFieldValue("OU")]
        OutsideProcessor,

        [EdiFieldValue("OV")]
        OwnerOfVessel,

        [EdiFieldValue("OW")]
        OwnerOfPropertyOrUnit,

        [EdiFieldValue("OX")]
        OxygenTherapyFacility,

        [EdiFieldValue("OY")]
        OwnerOfVehicle,

        [EdiFieldValue("OZ")]
        OutsideTestingAgency,

        [EdiFieldValue("P0")]
        PatientFacility,

        [EdiFieldValue("P1")]
        Preparer,

        [EdiFieldValue("P2")]
        PrimaryInsuredOrSubscriber,

        [EdiFieldValue("P3")]
        PrimaryCareProvider,

        [EdiFieldValue("P4")]
        PriorInsuranceCarrier,

        [EdiFieldValue("P5")]
        PlanSponsor,

        [EdiFieldValue("P6")]
        ThirdPartyReviewingPreferredProviderOrganization_PPO,

        [EdiFieldValue("P7")]
        ThirdPartyRepricingPreferredProviderOrganization_PPO,

        [EdiFieldValue("P8")]
        PersonnelOffice,

        [EdiFieldValue("P9")]
        PrimaryInterexchangeCarrier_PIC,

        [EdiFieldValue("PA")]
        PartyToReceiveInspectionReport,

        [EdiFieldValue("PB")]
        PayingBank,

        [EdiFieldValue("PC")]
        PartyToReceiveCertOfConformance_CAA,

        [EdiFieldValue("PD")]
        PurchasersDepartmentBuyer,

        [EdiFieldValue("PE")]
        Payee,

        [EdiFieldValue("PF")]
        PartyToReceiveFreightBill,

        [EdiFieldValue("PG")]
        PrimeContractor,

        [EdiFieldValue("PH")]
        Printer,

        [EdiFieldValue("PI")]
        Publisher,

        [EdiFieldValue("PJ")]
        PartyToReceiveCorrespondence,

        [EdiFieldValue("PK")]
        PartyToReceiveCopy,

        [EdiFieldValue("PL")]
        PartyToReceivePurchaseOrder,

        [EdiFieldValue("PM")]
        PartyToreceivepaperMemoOfInvoice,

        [EdiFieldValue("PN")]
        PartyToReceiveShippingNotice,

        [EdiFieldValue("PO")]
        PartyToReceiveInvoiceforGoodsOrServices,

        [EdiFieldValue("PP")]
        Property,

        [EdiFieldValue("PQ")]
        PartyToReceiveInvoiceforLeasePayments,

        [EdiFieldValue("PR")]
        Payer,

        [EdiFieldValue("PS")]
        PreviousStation,

        [EdiFieldValue("PT")]
        PartyToReceiveTestReport,

        [EdiFieldValue("PU")]
        PartyatPick_upLocation,

        [EdiFieldValue("PV")]
        Partyperformingcertification,

        [EdiFieldValue("PW")]
        PickUpAddress,

        [EdiFieldValue("PX")]
        PartyPerformingCount,

        [EdiFieldValue("PY")]
        PartyToFilePersonalPropertyTax,

        [EdiFieldValue("PZ")]
        PartyToReceiveEquipment,

        [EdiFieldValue("Q1")]
        ConductorPilot,

        [EdiFieldValue("Q2")]
        EngineerPilot,

        [EdiFieldValue("Q3")]
        RetailAccount,

        [EdiFieldValue("Q4")]
        CooperativeBuyingGroup,

        [EdiFieldValue("Q5")]
        AdvertisingGroup,

        [EdiFieldValue("Q6")]
        Interpreter,

        [EdiFieldValue("Q7")]
        Partner,

        [EdiFieldValue("Q8")]
        BasePeriodEmployer,

        [EdiFieldValue("Q9")]
        LastEmployer,

        [EdiFieldValue("QA")]
        Pharmacy,

        [EdiFieldValue("QB")]
        PurchaseServiceProvider,

        [EdiFieldValue("QC")]
        Patient,

        [EdiFieldValue("QD")]
        ResponsibleParty,

        [EdiFieldValue("QE")]
        Policyholder,

        [EdiFieldValue("QF")]
        Passenger,

        [EdiFieldValue("QG")]
        Pedestrian,

        [EdiFieldValue("QH")]
        Physician,

        [EdiFieldValue("QI")]
        PartyinPossession,

        [EdiFieldValue("QJ")]
        MostRecentEmployer_Chargeable,

        [EdiFieldValue("QK")]
        ManagedCare,

        [EdiFieldValue("QL")]
        Chiropractor,

        [EdiFieldValue("QM")]
        DialysisCenters,

        [EdiFieldValue("QN")]
        Dentist,

        [EdiFieldValue("QO")]
        DoctorOfOsteopathy,

        [EdiFieldValue("QP")]
        PrincipalBorrower,

        [EdiFieldValue("QQ")]
        QualityControl,

        [EdiFieldValue("QR")]
        BuyersQualityReviewBoard,

        [EdiFieldValue("QS")]
        Podiatrist,

        [EdiFieldValue("QT")]
        Psychiatrist,

        [EdiFieldValue("QU")]
        Veterinarian,

        [EdiFieldValue("QV")]
        GroupPractice,

        [EdiFieldValue("QW")]
        Government,

        [EdiFieldValue("QX")]
        HomeHealthCorporation,

        [EdiFieldValue("QY")]
        MedicalDoctor,

        [EdiFieldValue("QZ")]
        Co_borrower,

        [EdiFieldValue("R0")]
        RoyaltyOwner,

        [EdiFieldValue("R1")]
        PartyToReceiveScaleTicket,

        [EdiFieldValue("R2")]
        ReportingOfficer,

        [EdiFieldValue("R3")]
        NextScheduledDestination,

        [EdiFieldValue("R4")]
        Regulatory_State_District,

        [EdiFieldValue("R5")]
        Regulatory_State_Entity,

        [EdiFieldValue("R6")]
        Requester,

        [EdiFieldValue("R7")]
        ConsumerReferralContact,

        [EdiFieldValue("R8")]
        CreditReportingAgency,

        [EdiFieldValue("R9")]
        RequestedLender,

        [EdiFieldValue("RA")]
        AlternateReturnAddress,

        [EdiFieldValue("RB")]
        ReceivingBank,

        [EdiFieldValue("RC")]
        ReceivingLocation,

        [EdiFieldValue("RD")]
        DestinationIntermodalRamp,

        [EdiFieldValue("RE")]
        PartyToReceiveCommercialInvoiceRemittance,

        [EdiFieldValue("RF")]
        Refinery,

        [EdiFieldValue("RG")]
        ResponsibleInstallation_Origin,

        [EdiFieldValue("RH")]
        ResponsibleInstallation_Destination,

        [EdiFieldValue("RI")]
        RemitTo,

        [EdiFieldValue("RJ")]
        ResidenceOrDomicile,

        [EdiFieldValue("RK")]
        RefineryOperator,

        [EdiFieldValue("RL")]
        ReportingLocation,

        [EdiFieldValue("RM")]
        Partythatremitspayment,

        [EdiFieldValue("RN")]
        RepairOrRefurbishLocation,

        [EdiFieldValue("RO")]
        OriginalIntermodalRamp,

        [EdiFieldValue("RP")]
        ReceivingPointforCustomerSamples,

        [EdiFieldValue("RQ")]
        ResaleCustomer,

        [EdiFieldValue("RR")]
        Railroad,

        [EdiFieldValue("RS")]
        ReceivingFacilityScheduler,

        [EdiFieldValue("RT")]
        Returnedto,

        [EdiFieldValue("RU")]
        ReceivingSub_Location,

        [EdiFieldValue("RV")]
        Reservoir,

        [EdiFieldValue("RW")]
        RuralHealthClinic,

        [EdiFieldValue("RX")]
        ResponsibleExhibitor,

        [EdiFieldValue("RY")]
        SpecifiedRepository,

        [EdiFieldValue("RZ")]
        ReceiptZone,

        [EdiFieldValue("S0")]
        SoleProprietor,

        [EdiFieldValue("S1")]
        Parent,

        [EdiFieldValue("S2")]
        Student,

        [EdiFieldValue("S3")]
        CustodialParent,

        [EdiFieldValue("S4")]
        SkilledNursingFacility,

        [EdiFieldValue("S5")]
        SecuredParty,

        [EdiFieldValue("S6")]
        AgencyGrantingSecurityClearance,

        [EdiFieldValue("S7")]
        SecuredPartyCompany,

        [EdiFieldValue("S8")]
        SecuredPartyIndividual,

        [EdiFieldValue("S9")]
        Sibling,

        [EdiFieldValue("SA")]
        SalvageCarrier,

        [EdiFieldValue("SB")]
        StorageArea,

        [EdiFieldValue("SC")]
        StoreClass,

        [EdiFieldValue("SD")]
        SoldToAndShipTo,

        [EdiFieldValue("SE")]
        SellingParty,

        [EdiFieldValue("SF")]
        ShipFrom,

        [EdiFieldValue("SG")]
        StoreGroup,

        [EdiFieldValue("SH")]
        Shipper,

        [EdiFieldValue("SI")]
        ShippingScheduleIssuer,

        [EdiFieldValue("SJ")]
        ServiceProvider,

        [EdiFieldValue("SK")]
        SecondaryLocationAddress_SLA,

        [EdiFieldValue("SL")]
        OriginSublocation,

        [EdiFieldValue("SM")]
        PartyToReceiveShippingManifest,

        [EdiFieldValue("SN")]
        Store,

        [EdiFieldValue("SO")]
        SoldToIfDifferentFromBillTo,

        [EdiFieldValue("SP")]
        PartyfillingShippersOrder,

        [EdiFieldValue("SQ")]
        ServiceBureau,

        [EdiFieldValue("SR")]
        SamplesToBeReturnedTo,

        [EdiFieldValue("SS")]
        SteamshipCompany,

        [EdiFieldValue("ST")]
        ShipTo,

        [EdiFieldValue("SU")]
        SupplierManufacturer,

        [EdiFieldValue("SV")]
        ServicePerformanceSite,

        [EdiFieldValue("SW")]
        SealingCompany,

        [EdiFieldValue("SX")]
        School_basedServiceProvider,

        [EdiFieldValue("SY")]
        SecondaryTaxpayer,

        [EdiFieldValue("SZ")]
        Supervisor,

        [EdiFieldValue("T1")]
        OperatorOftheTransferPoint,

        [EdiFieldValue("T2")]
        OperatorOftheSourceTransferPoint,

        [EdiFieldValue("T3")]
        TerminalLocation,

        [EdiFieldValue("T4")]
        TransferPoint,

        [EdiFieldValue("T6")]
        TerminalOperator,

        [EdiFieldValue("T8")]
        PreviousTitleCompany,

        [EdiFieldValue("T9")]
        PriorTitleEvidenceHolder,

        [EdiFieldValue("TA")]
        TitleInsuranceServicesProvider,

        [EdiFieldValue("TB")]
        Tooling,

        [EdiFieldValue("TC")]
        ToolSource,

        [EdiFieldValue("TD")]
        ToolingDesign,

        [EdiFieldValue("TE")]
        Theatre,

        [EdiFieldValue("TF")]
        TankFarm,

        [EdiFieldValue("TG")]
        ToolingFabrication,

        [EdiFieldValue("TH")]
        TheaterCircuit,

        [EdiFieldValue("TI")]
        TariffIssuer,

        [EdiFieldValue("TJ")]
        Cosigner,

        [EdiFieldValue("TK")]
        TestSponsor,

        [EdiFieldValue("TL")]
        TestingLaboratory,

        [EdiFieldValue("TM")]
        Transmitter,

        [EdiFieldValue("TN")]
        Tradename,

        [EdiFieldValue("TO")]
        MessageTo,

        [EdiFieldValue("TP")]
        PrimaryTaxpayer,

        [EdiFieldValue("TQ")]
        ThirdPartyReviewingOrganization_TPO,

        [EdiFieldValue("TR")]
        Terminal,

        [EdiFieldValue("TS")]
        PartyToReceiveCertifiedTestResults,

        [EdiFieldValue("TT")]
        TransferTo,

        [EdiFieldValue("TU")]
        ThirdPartyRepricingOrganization_TPO,

        [EdiFieldValue("TV")]
        ThirdPartyAdministrator_TPA,

        [EdiFieldValue("TW")]
        TransitAuthority,

        [EdiFieldValue("TX")]
        TaxAuthority,

        [EdiFieldValue("TY")]
        Trustee,

        [EdiFieldValue("TZ")]
        SignificantOther,

        [EdiFieldValue("U1")]
        GasTransactionPoint1,

        [EdiFieldValue("U2")]
        GasTransactionPoint2,

        [EdiFieldValue("U3")]
        ServicingAgent_U3,

        [EdiFieldValue("U4")]
        Team,

        [EdiFieldValue("U5")]
        Underwriter,

        [EdiFieldValue("U6")]
        TitleUnderwriter,

        [EdiFieldValue("U7")]
        Psychologist,

        [EdiFieldValue("U8")]
        Reference,

        [EdiFieldValue("U9")]
        Non_RegisteredInvestmentAdvisor,

        [EdiFieldValue("UA")]
        PlaceOfBottling,

        [EdiFieldValue("UB")]
        PlaceOfDistilling,

        [EdiFieldValue("UC")]
        UltimateConsignee,

        [EdiFieldValue("UD")]
        Region,

        [EdiFieldValue("UE")]
        TestingService,

        [EdiFieldValue("UF")]
        HealthMiscellaneous,

        [EdiFieldValue("UG")]
        NursingHomeChain,

        [EdiFieldValue("UH")]
        NursingHome,

        [EdiFieldValue("UI")]
        RegisteredInvestmentAdvisor,

        [EdiFieldValue("UJ")]
        SalesAssistant,

        [EdiFieldValue("UK")]
        System,

        [EdiFieldValue("UL")]
        SpecialAccount,

        [EdiFieldValue("UM")]
        CurrentEmployer_Primary,

        [EdiFieldValue("UN")]
        Union,

        [EdiFieldValue("UO")]
        CurrentEmployer_Secondary,

        [EdiFieldValue("UP")]
        UnloadingParty,

        [EdiFieldValue("UQ")]
        SubsequentOwner,

        [EdiFieldValue("UR")]
        Surgeon,

        [EdiFieldValue("US")]
        UpstreamParty,

        [EdiFieldValue("UT")]
        USTrustee,

        [EdiFieldValue("UU")]
        AnnuitantPayor,

        [EdiFieldValue("UW")]
        UnassignedAgent,

        [EdiFieldValue("UX")]
        BaseJurisdiction,

        [EdiFieldValue("UY")]
        Vehicle,

        [EdiFieldValue("UZ")]
        Signer,

        [EdiFieldValue("V1")]
        Surety,

        [EdiFieldValue("V2")]
        Grantor,

        [EdiFieldValue("V3")]
        WellPadConstructionContractor,

        [EdiFieldValue("V4")]
        OilAndGasRegulatoryAgency,

        [EdiFieldValue("V5")]
        SurfaceDischargeAgency,

        [EdiFieldValue("V6")]
        WellCasingDepthAuthority,

        [EdiFieldValue("V8")]
        MarketTimer,

        [EdiFieldValue("V9")]
        OwnerAnnuitantPayor,

        [EdiFieldValue("VA")]
        SecondContact,

        [EdiFieldValue("VB")]
        Candidate,

        [EdiFieldValue("VC")]
        VehicleCustodian,

        [EdiFieldValue("VD")]
        MultipleListingService,

        [EdiFieldValue("VE")]
        BoardOfRealtors,

        [EdiFieldValue("VF")]
        SellingOffice,

        [EdiFieldValue("VG")]
        ListingAgent,

        [EdiFieldValue("VH")]
        ShowingAgent,

        [EdiFieldValue("VI")]
        ContactPerson,

        [EdiFieldValue("VJ")]
        OwnerJointAnnuitantPayor,

        [EdiFieldValue("VK")]
        PropertyOrBuildingManager,

        [EdiFieldValue("VL")]
        BuilderName,

        [EdiFieldValue("VM")]
        Occupant,

        [EdiFieldValue("VN")]
        Vendor,

        [EdiFieldValue("VO")]
        ElementarySchool,

        [EdiFieldValue("VP")]
        PartywithPowerToVoteSecurities,

        [EdiFieldValue("VQ")]
        MiddleSchool,

        [EdiFieldValue("VR")]
        JuniorHighSchool,

        [EdiFieldValue("VS")]
        VehicleSalvageAssignment,

        [EdiFieldValue("VT")]
        ListingOffice,

        [EdiFieldValue("VU")]
        SecondContactOrganization,

        [EdiFieldValue("VV")]
        OwnerPayor,

        [EdiFieldValue("VW")]
        Winner,

        [EdiFieldValue("VX")]
        ProductionManager_VX,

        [EdiFieldValue("VY")]
        OrganizationCompletingConfigurationChange,

        [EdiFieldValue("VZ")]
        ProductionManager_VZ,

        [EdiFieldValue("W1")]
        WorkTeam,

        [EdiFieldValue("W2")]
        SupplierWorkTeam,

        [EdiFieldValue("W3")]
        ThirdPartyInvestmentAdvisor,

        [EdiFieldValue("W4")]
        Trust,

        [EdiFieldValue("W8")]
        InterlineServiceCommitmentCustomer,

        [EdiFieldValue("W9")]
        SamplingLocation,

        [EdiFieldValue("WA")]
        WritingAgent,

        [EdiFieldValue("WB")]
        AppraiserName,

        [EdiFieldValue("WC")]
        ComparableProperty,

        [EdiFieldValue("WD")]
        StorageFacilityatDestination,

        [EdiFieldValue("WE")]
        SubjectProperty,

        [EdiFieldValue("WF")]
        TankFarmOwner,

        [EdiFieldValue("WG")]
        WageEarner,

        [EdiFieldValue("WH")]
        Warehouse,

        [EdiFieldValue("WI")]
        Witness,

        [EdiFieldValue("WJ")]
        SupervisoryAppraiserName,

        [EdiFieldValue("WL")]
        Wholesaler,

        [EdiFieldValue("WN")]
        CompanyAssignedWell,

        [EdiFieldValue("WO")]
        StorageFacilityatOrigin,

        [EdiFieldValue("WP")]
        WitnessforPlaintiff,

        [EdiFieldValue("WR")]
        WithdrawalPoint,

        [EdiFieldValue("WS")]
        WaterSystem,

        [EdiFieldValue("WT")]
        WitnessforDefendant,

        [EdiFieldValue("WU")]
        PrimarySupportOrganization,

        [EdiFieldValue("WV")]
        PreliminaryMaintenancePeriodDesignatingOrganization,

        [EdiFieldValue("WW")]
        PreliminaryMaintenanceOrganization,

        [EdiFieldValue("WX")]
        PreliminaryReferredToOrganization,

        [EdiFieldValue("WY")]
        FinalMaintenancePeriodDesignatingOrganization,

        [EdiFieldValue("WZ")]
        FinalMaintenanceOrganization,

        [EdiFieldValue("X1")]
        Mailto,

        [EdiFieldValue("X2")]
        PartyToPerformPackaging,

        [EdiFieldValue("X3")]
        UtilizationManagementOrganization,

        [EdiFieldValue("X4")]
        Spouse,

        [EdiFieldValue("X5")]
        DurableMedicalEquipmentSupplier,

        [EdiFieldValue("X6")]
        InternationalOrganization,

        [EdiFieldValue("X7")]
        Inventor,

        [EdiFieldValue("X8")]
        HispanicServiceInstitute,

        [EdiFieldValue("XA")]
        Creditor,

        [EdiFieldValue("XC")]
        DebtorsAttorney,

        [EdiFieldValue("XD")]
        Alias,

        [EdiFieldValue("XE")]
        ClaimRecipient,

        [EdiFieldValue("XF")]
        Auctioneer,

        [EdiFieldValue("XG")]
        EventLocation,

        [EdiFieldValue("XH")]
        FinalReferredToOrganization,

        [EdiFieldValue("XI")]
        OriginalClaimant,

        [EdiFieldValue("XJ")]
        ActualReferredByOrganization,

        [EdiFieldValue("XK")]
        ActualReferredToOrganization,

        [EdiFieldValue("XL")]
        BorrowersEmployer,

        [EdiFieldValue("XM")]
        MaintenanceOrganizationUsedforEstimate,

        [EdiFieldValue("XN")]
        PlanningMaintenanceOrganization,

        [EdiFieldValue("XO")]
        PreliminaryCustomerOrganization,

        [EdiFieldValue("XP")]
        PartyToReceiveSolicitation,

        [EdiFieldValue("XQ")]
        CanadianCustomsBroker,

        [EdiFieldValue("XR")]
        MexicanCustomsBroker,

        [EdiFieldValue("XS")]
        SCorporation,

        [EdiFieldValue("XT")]
        FinalCustomerOrganization,

        [EdiFieldValue("XU")]
        UnitedStatesCustomsBroker,

        [EdiFieldValue("XV")]
        CrossClaimant,

        [EdiFieldValue("XW")]
        CounterClaimant,

        [EdiFieldValue("XX")]
        BusinessArea,

        [EdiFieldValue("XY")]
        TribalGovernment,

        [EdiFieldValue("XZ")]
        AmericanIndian_OwnedBusiness,

        [EdiFieldValue("Y2")]
        ManagedCareOrganization,

        [EdiFieldValue("YA")]
        Affiant,

        [EdiFieldValue("YB")]
        Arbitrator,

        [EdiFieldValue("YC")]
        BailPayor,

        [EdiFieldValue("YD")]
        DistrictJustice,

        [EdiFieldValue("YE")]
        ThirdParty,

        [EdiFieldValue("YF")]
        WitnessforProsecution,

        [EdiFieldValue("YG")]
        ExpertWitness,

        [EdiFieldValue("YH")]
        CrimeVictim,

        [EdiFieldValue("YI")]
        JuvenileVictim,

        [EdiFieldValue("YJ")]
        JuvenileDefendant,

        [EdiFieldValue("YK")]
        Bondsman,

        [EdiFieldValue("YL")]
        CourtAppointedAttorney,

        [EdiFieldValue("YM")]
        ComplainantsAttorney,

        [EdiFieldValue("YN")]
        DistrictAttorney,

        [EdiFieldValue("YO")]
        AttorneyforDefendant_Public,

        [EdiFieldValue("YP")]
        ProBonoAttorney,

        [EdiFieldValue("YQ")]
        ProSeCounsel,

        [EdiFieldValue("YR")]
        PartyToAppearBefore,

        [EdiFieldValue("YS")]
        Appellant,

        [EdiFieldValue("YT")]
        Appellee,

        [EdiFieldValue("YU")]
        ArrestingOfficer,

        [EdiFieldValue("YV")]
        HostileWitness,

        [EdiFieldValue("YW")]
        DischargePoint,

        [EdiFieldValue("YX")]
        FloodCertifier,

        [EdiFieldValue("YY")]
        FloodDeterminationProvider,

        [EdiFieldValue("YZ")]
        ElectronicRegistrationUtility,

        [EdiFieldValue("Z1")]
        PartyToReceiveStatus,

        [EdiFieldValue("Z2")]
        UnserviceableMaterialConsignee,

        [EdiFieldValue("Z3")]
        PotentialSourceOfSupply,

        [EdiFieldValue("Z4")]
        OwningInventoryControlPoint,

        [EdiFieldValue("Z5")]
        ManagementControlActivity,

        [EdiFieldValue("Z6")]
        TransferringParty,

        [EdiFieldValue("Z7")]
        Mark_forParty,

        [EdiFieldValue("Z8")]
        LastKnownSourceOfSupply,

        [EdiFieldValue("Z9")]
        Banker,

        [EdiFieldValue("ZA")]
        CorrectedAddress,

        [EdiFieldValue("ZB")]
        PartyToReceiveCredit,

        [EdiFieldValue("ZC")]
        RentPayor,

        [EdiFieldValue("ZD")]
        PartyToReceiveReports,

        [EdiFieldValue("ZE")]
        EndItemManufacturer,

        [EdiFieldValue("ZF")]
        BreakBulkPoint,

        [EdiFieldValue("ZG")]
        PresentAddress,

        [EdiFieldValue("ZH")]
        Child,

        [EdiFieldValue("ZJ")]
        Branch,

        [EdiFieldValue("ZK")]
        Reporter,

        [EdiFieldValue("ZL")]
        PartyPassingtheTransaction,

        [EdiFieldValue("ZM")]
        LeaseLocation,

        [EdiFieldValue("ZN")]
        LosingInventoryManager,

        [EdiFieldValue("ZO")]
        MinimumRoyaltyPayor,

        [EdiFieldValue("ZP")]
        GainingInventoryManager,

        [EdiFieldValue("ZQ")]
        ScreeningPoint,

        [EdiFieldValue("ZR")]
        ValidatingParty,

        [EdiFieldValue("ZS")]
        MonitoringParty,

        [EdiFieldValue("ZT")]
        ParticipatingArea,

        [EdiFieldValue("ZU")]
        Formation,

        [EdiFieldValue("ZV")]
        AllowableRecipient,

        [EdiFieldValue("ZW")]
        Field,

        [EdiFieldValue("ZX")]
        AttorneyOfRecord,

        [EdiFieldValue("ZY")]
        AmicusCuriae,

        [EdiFieldValue("ZZ")]
        MutuallyDefined,

        [EdiFieldValue("001")]
        Pumper,

        [EdiFieldValue("002")]
        SurfaceManagementEntity,

        [EdiFieldValue("003")]
        ApplicationParty,

        [EdiFieldValue("004")]
        SiteOperator,

        [EdiFieldValue("005")]
        ConstructionContractor,

        [EdiFieldValue("006")]
        DrillingContractor,

        [EdiFieldValue("007")]
        SpudContractor,

        [EdiFieldValue("AAA")]
        Sub_account,

        [EdiFieldValue("AAB")]
        ManagementNon_Officer,

        [EdiFieldValue("AAC")]
        IncorporatedLocation,

        [EdiFieldValue("AAD")]
        NamenotToBeConfusedwith,

        [EdiFieldValue("AAE")]
        Lot,

        [EdiFieldValue("AAF")]
        PreviousOccupant,

        [EdiFieldValue("AAG")]
        GroundAmbulanceServices,

        [EdiFieldValue("AAH")]
        AirAmbulanceServices,

        [EdiFieldValue("AAI")]
        WaterAmbulanceServices,

        [EdiFieldValue("AAJ")]
        AdmittingServices,

        [EdiFieldValue("AAK")]
        PrimarySurgeon,

        [EdiFieldValue("AAL")]
        MedicalNurse,

        [EdiFieldValue("AAM")]
        CardiacRehabilitationServices,

        [EdiFieldValue("AAN")]
        SkilledNursingServices,

        [EdiFieldValue("AAO")]
        ObservationRoomServices,

        [EdiFieldValue("AAP")]
        Employee,

        [EdiFieldValue("AAQ")]
        AnesthesiologyServices,

        [EdiFieldValue("AAS")]
        PriorBaseJurisdiction,

        [EdiFieldValue("AAT")]
        IncorporationJurisdiction,

        [EdiFieldValue("AAU")]
        MarkerOwner,

        [EdiFieldValue("AAV")]
        ReclamationCenter,

        [EdiFieldValue("ABB")]
        MasterProperty,

        [EdiFieldValue("ABC")]
        ProjectProperty,

        [EdiFieldValue("ABD")]
        UnitProperty,

        [EdiFieldValue("ABE")]
        AdditionalAddress,

        [EdiFieldValue("ABF")]
        SocietyOfPropertyInformationCompilersAndAnalysts,

        [EdiFieldValue("ABG")]
        Organization,

        [EdiFieldValue("ABH")]
        JointOwnerAnnuitant,

        [EdiFieldValue("ABI")]
        JointAnnuitantOwner,

        [EdiFieldValue("ABJ")]
        JointOwnerAnnuitantPayor,

        [EdiFieldValue("ABK")]
        JointOwnerJointAnnuitant,

        [EdiFieldValue("ABL")]
        JointOwnerJointAnnuitantPayor,

        [EdiFieldValue("ABM")]
        JointOwnerPayor,

        [EdiFieldValue("ALA")]
        AlternativeAddressee,

        [EdiFieldValue("BAL")]
        Bailiff,

        [EdiFieldValue("BKR")]
        Bookkeeper,

        [EdiFieldValue("BRN")]
        BrandName,

        [EdiFieldValue("BUS")]
        Business,

        [EdiFieldValue("CMW")]
        CompanyMergedWith,

        [EdiFieldValue("COL")]
        CollateralAssignee,

        [EdiFieldValue("COR")]
        CorrectedName,

        [EdiFieldValue("DCC")]
        ChiefDeputyClerkOfCourt,

        [EdiFieldValue("DIR")]
        DistributionRecipient,

        [EdiFieldValue("ENR")]
        Enroller,

        [EdiFieldValue("EXS")]
        Ex_spouse,

        [EdiFieldValue("FRL")]
        ForeignRegistrationLocation,

        [EdiFieldValue("FSR")]
        FinancialStatementRecipient,

        [EdiFieldValue("GIR")]
        GiftRecipient,

        [EdiFieldValue("HMI")]
        MaterialSafetyDataSheet_MSDS_Recipient,

        [EdiFieldValue("HOM")]
        HomeOffice,

        [EdiFieldValue("IAA")]
        BusinessEntity,

        [EdiFieldValue("IAC")]
        PrincipalExecutiveOffice,

        [EdiFieldValue("IAD")]
        ForeignOffice,

        [EdiFieldValue("IAE")]
        Member,

        [EdiFieldValue("IAF")]
        ExecutiveCommitteeMember,

        [EdiFieldValue("IAG")]
        Director,

        [EdiFieldValue("IAH")]
        Clerk,

        [EdiFieldValue("IAI")]
        PartywithKnowledgeOfAffairsOftheCompany,

        [EdiFieldValue("IAK")]
        PartyToReceiveStatementOfFeesDue,

        [EdiFieldValue("IAL")]
        CompanyinwhichInterestHeld,

        [EdiFieldValue("IAM")]
        CompanywhichHoldsInterest,

        [EdiFieldValue("IAN")]
        Notary,

        [EdiFieldValue("IAO")]
        Manager,

        [EdiFieldValue("IAP")]
        AlienAffiliate,

        [EdiFieldValue("IAQ")]
        IncorporationStatePrincipalOffice,

        [EdiFieldValue("IAR")]
        IncorporationStatePlaceOfBusiness,

        [EdiFieldValue("IAS")]
        Out_of_StatePrincipalOffice,

        [EdiFieldValue("IAT")]
        PartyExecutingAndVerifying,

        [EdiFieldValue("IAU")]
        Felon,

        [EdiFieldValue("IAV")]
        OtherRelatedParty,

        [EdiFieldValue("IAW")]
        Record_KeepingAddress,

        [EdiFieldValue("IAY")]
        InitialSubscriber,

        [EdiFieldValue("IAZ")]
        OriginalJurisdiction,

        [EdiFieldValue("INV")]
        InvestmentAdvisor,

        [EdiFieldValue("LGS")]
        LocalGovernmentSponsor,

        [EdiFieldValue("LYM")]
        AmendedName,

        [EdiFieldValue("LYN")]
        Stockholder,

        [EdiFieldValue("LYO")]
        ManagingAgent,

        [EdiFieldValue("LYP")]
        Organizer,

        [EdiFieldValue("MSC")]
        MammographyScreeningCenter,

        [EdiFieldValue("NCT")]
        NameChangedTo,

        [EdiFieldValue("NPC")]
        NotaryPublic,

        [EdiFieldValue("ORI")]
        OriginalName,

        [EdiFieldValue("PLR")]
        PayerOfLastResort,

        [EdiFieldValue("PMF")]
        PartyManufacturedFor,

        [EdiFieldValue("PPS")]
        PersonforWhoseBenefitPropertywasSeized,

        [EdiFieldValue("PRE")]
        PreviousOwner,

        [EdiFieldValue("PRP")]
        PrimaryPayer,

        [EdiFieldValue("PUR")]
        PurchasedCompany,

        [EdiFieldValue("RCR")]
        RecoveryRoom,

        [EdiFieldValue("REC")]
        ReceiverManager,

        [EdiFieldValue("RGA")]
        ResponsibleGovernmentAgency,

        [EdiFieldValue("SEP")]
        SecondaryPayer,

        [EdiFieldValue("TPM")]
        ThirdPartyMarketer,

        [EdiFieldValue("TSE")]
        ConsigneeCourierTransferStation,

        [EdiFieldValue("TSR")]
        ConsignorCourierTransferStation,

        [EdiFieldValue("TTP")]
        TertiaryPayer,
    }
}
