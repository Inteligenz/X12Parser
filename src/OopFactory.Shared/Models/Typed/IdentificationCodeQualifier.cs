namespace OopFactory.X12.Shared.Models.Typed
{
    using OopFactory.X12.Shared.Attributes;

    public enum IdentificationCodeQualifier
    {
        [EdiFieldValue("1")]
        DunsNumber_DunAndBradstreet,

        [EdiFieldValue("2")]
        StandardCarrierAlphaCode_SCAC,

        [EdiFieldValue("3")]
        FederalMaritimeCommission_Ocean__FMC,

        [EdiFieldValue("4")]
        InternationalAirTransportAssociation_IATA,

        [EdiFieldValue("5")]
        SIRET,

        [EdiFieldValue("6")]
        PlantCode,

        [EdiFieldValue("7")]
        LoadingDock,

        [EdiFieldValue("8")]
        UCC_EANGlobalProductIdentificationPrefix,

        [EdiFieldValue("9")]
        DunsPlus4, DunsNumberwithFourCharacterSuffix,

        [EdiFieldValue("A")]
        UsCustomsCarrierIdentification,

        [EdiFieldValue("C")]
        InsuredsChangedUniqueIdentificationNumber,

        [EdiFieldValue("D")]
        CensusScheduleD,

        [EdiFieldValue("E")]
        HazardInsurancePolicyNumber,

        [EdiFieldValue("F")]
        DocumentCustodianIdentificationNumber,

        [EdiFieldValue("G")]
        PayeeIdentificationNumber,

        [EdiFieldValue("I")]
        SecondaryMarketingInvestorAssignedNumber,

        [EdiFieldValue("J")]
        MortgageElectronicRegistrationSystemOrganizationIdentifier,

        [EdiFieldValue("K")]
        CensusScheduleK,

        [EdiFieldValue("L")]
        InvestorAssignedIdentificationNumber,

        [EdiFieldValue("N")]
        InsuredsUniqueIdentificationNumber,

        [EdiFieldValue("S")]
        TitleInsurancePolicyNumber,

        [EdiFieldValue("10")]
        DepartmentofDefenseActivityAddressCode_DODAAC,

        [EdiFieldValue("11")]
        DrugEnforcementAdministration_DEA,

        [EdiFieldValue("12")]
        TelephoneNumber_Phone,

        [EdiFieldValue("13")]
        FederalReserveRoutingCode_FRRC,

        [EdiFieldValue("14")]
        UCC_EANLocationCodePrefix,

        [EdiFieldValue("15")]
        StandardAddressNumber_SAN,

        [EdiFieldValue("16")]
        ZIPCode,

        [EdiFieldValue("17")]
        AutomatedBrokerInterface_ABI_RoutingCode,

        [EdiFieldValue("18")]
        AutomotiveIndustryActionGroup_AIAG,

        [EdiFieldValue("19")]
        FIPS_55_NamedPopulatedPlaces,

        [EdiFieldValue("20")]
        StandardPointLocationCode_SPLC,

        [EdiFieldValue("21")]
        HealthIndustryNumber_HIN,

        [EdiFieldValue("22")]
        CouncilofPetroleumAccountingSocietiescode_COPAS,

        [EdiFieldValue("23")]
        JournalofCommerce_JOC,

        [EdiFieldValue("24")]
        EmployersIdentificationNumber,

        [EdiFieldValue("25")]
        CarriersCustomerCode,

        [EdiFieldValue("26")]
        PetroleumAccountantsSocietyofCanadaCompanyCode,

        [EdiFieldValue("27")]
        GovernmentBillOfLadingOfficeCode_GBLOC,

        [EdiFieldValue("28")]
        AmericanPaperInstitute,

        [EdiFieldValue("29")]
        GridLocationandFacilityCode,

        [EdiFieldValue("30")]
        AmericanPetroleumInstituteLocationCode,

        [EdiFieldValue("31")]
        BankIdentificationCode,

        [EdiFieldValue("32")]
        AssignedbyPropertyOperator,

        [EdiFieldValue("33")]
        CommercialandGovernmentEntity_CAGE,

        [EdiFieldValue("34")]
        SocialSecurityNumber,

        [EdiFieldValue("35")]
        ElectronicMailInternalSystemAddressCode,

        [EdiFieldValue("36")]
        CustomsHouseBrokerLicenseNumber,

        [EdiFieldValue("37")]
        UnitedNationsVendorCode,

        [EdiFieldValue("38")]
        CountryCode,

        [EdiFieldValue("39")]
        LocalUnionNumber,

        [EdiFieldValue("40")]
        ElectronicMailUserCode,

        [EdiFieldValue("41")]
        TelecommunicationsCarrierIdentificationCode,

        [EdiFieldValue("42")]
        TelecommunicationsPseudoCarrierIdentificationCode,

        [EdiFieldValue("43")]
        AlternateSocialSecurityNumber,

        [EdiFieldValue("44")]
        ReturnSequenceNumber,

        [EdiFieldValue("45")]
        DeclarationControlNumber,

        [EdiFieldValue("46")]
        ElectronicTransmitterIdentificationNumber_ETIN,

        [EdiFieldValue("47")]
        TaxAuthorityIdentification,

        [EdiFieldValue("48")]
        ElectronicFilerIdentificationNumber_EFIN,

        [EdiFieldValue("49")]
        StateIdentificationNumber,

        [EdiFieldValue("50")]
        BusinessLicenseNumber,

        [EdiFieldValue("53")]
        Building,

        [EdiFieldValue("54")]
        Warehouse,

        [EdiFieldValue("55")]
        PostOfficeBox,

        [EdiFieldValue("56")]
        Division,

        [EdiFieldValue("57")]
        Department,

        [EdiFieldValue("58")]
        OriginatingCompanyNumber,

        [EdiFieldValue("59")]
        ReceivingCompanyNumber,

        [EdiFieldValue("61")]
        HoldingMortgageeNumber,

        [EdiFieldValue("62")]
        ServicingMortgageeNumber,

        [EdiFieldValue("63")]
        Servicer_holderMortgageeNumber,

        [EdiFieldValue("64")]
        OneCallAgency,

        [EdiFieldValue("71")]
        IntegratedPostsecondaryEducationDataSystem_IPEDS,

        [EdiFieldValue("72")]
        TheCollegeBoardsAdmissionTestingProgram_ATP,

        [EdiFieldValue("73")]
        FederalInteragencyCommissiononEducation_FICE,

        [EdiFieldValue("74")]
        AmericanCollegeTesting_ACT_listofpostsecondaryeducationalinstitutions,

        [EdiFieldValue("75")]
        StateorProvinceAssignedNumber,

        [EdiFieldValue("76")]
        LocalSchoolDistrictorJurisdictionNumber,

        [EdiFieldValue("77")]
        NationalCenterforEducationStatistics_NCES_CommonCoreofData_CCD_number,

        [EdiFieldValue("78")]
        TheCollegeBoardandACT6digitcodelistofsecondaryeducationalinstitutions,

        [EdiFieldValue("81")]
        ClassificationofInstructionalPrograms_CIP_codingstructuremaintainedbytheUsDepartme,

        [EdiFieldValue("82")]
        HigherEducationGeneralInformationSurvey_HEGIS_maintainedbytheUsDepartmentofEducat,

        [EdiFieldValue("90")]
        CaliforniaEthnicSubgroupsCodeTable,

        [EdiFieldValue("91")]
        AssignedbySellerorSellersAgent,

        [EdiFieldValue("92")]
        AssignedbyBuyerorBuyersAgent,

        [EdiFieldValue("93")]
        Codeassignedbytheorganizationoriginatingthetransactionset,

        [EdiFieldValue("94")]
        Codeassignedbytheorganizationthatistheultimatedestinationofthetransactionset,

        [EdiFieldValue("95")]
        AssignedByTransporter,

        [EdiFieldValue("96")]
        AssignedByPipelineOperator,

        [EdiFieldValue("97")]
        ReceiversCode,

        [EdiFieldValue("98")]
        PurchasingOffice,

        [EdiFieldValue("99")]
        OfficeofWorkersCompensationPrograms_OWCP_AgencyCode,

        [EdiFieldValue("A1")]
        ApproverID,

        [EdiFieldValue("A2")]
        MilitaryAssistanceProgramAddressCode_MAPAC,

        [EdiFieldValue("A3")]
        AssignedbyThirdParty,

        [EdiFieldValue("A4")]
        AssignedbyClearinghouse,

        [EdiFieldValue("A5")]
        CommitteeforUniformSecurityIdentificationProcedures_CUSIP_Number,

        [EdiFieldValue("A6")]
        FinancialIdentificationNumberingSystem_FINS_Number,

        [EdiFieldValue("AA")]
        PostalServiceCode,

        [EdiFieldValue("AB")]
        USEnvironmentalProtectionAgency_EPA_IdentificationNumber,

        [EdiFieldValue("AC")]
        AttachmentControlNumber,

        [EdiFieldValue("AD")]
        BlueCrossBlueShieldAssociationPlanCode,

        [EdiFieldValue("AE")]
        AlbertaEnergyResourcesConservationBoard,

        [EdiFieldValue("AL")]
        AnesthesiaLicenseNumber,

        [EdiFieldValue("AP")]
        AlbertaPetroleumMarketingCommission,

        [EdiFieldValue("BC")]
        BritishColumbiaMinistryofEnergyMinesandPetroleumResources,

        [EdiFieldValue("BD")]
        BlueCrossProviderNumber,

        [EdiFieldValue("BE")]
        CommonLanguageLocationIdentification_CLLI,

        [EdiFieldValue("BG")]
        BadgeNumber,

        [EdiFieldValue("BP")]
        BenefitPlan,

        [EdiFieldValue("BS")]
        BlueShieldProviderNumber,

        [EdiFieldValue("C1")]
        InsuredorSubscriber,

        [EdiFieldValue("C2")]
        HealthMaintenanceOrganization_HMO_ProviderNumber,

        [EdiFieldValue("C5")]
        CustomerIdentificationFile,

        [EdiFieldValue("CA")]
        StatisticsCanadaCanadianCollegeStudentInformationSystemCourseCodes,

        [EdiFieldValue("CB")]
        StatisticsCanadaCanadianCollegeStudentInformationSystemInstitutionCodes,

        [EdiFieldValue("CC")]
        StatisticsCanadaUniversityStudentInformationSystemCurriculumCodes,

        [EdiFieldValue("CD")]
        ContractDivision,

        [EdiFieldValue("CE")]
        BureauoftheCensusFilerIdentificationCode,

        [EdiFieldValue("CF")]
        CanadianFinancialInstitutionRoutingNumber,

        [EdiFieldValue("CI")]
        CHAMPUS_CivilianHealthandMedicalProgramoftheUniformedServices_IdentificationNumber,

        [EdiFieldValue("CL")]
        CorrectedLoanNumber,

        [EdiFieldValue("CM")]
        UsCustomsService_USCS_ManufacturerIdentifier_MID,

        [EdiFieldValue("CP")]
        CanadianPetroleumAssociation,

        [EdiFieldValue("CR")]
        CreditRepository,

        [EdiFieldValue("CS")]
        StatisticsCanadaUniversityStudentInformationSystemUniversityCodes,

        [EdiFieldValue("CT")]
        CourtIdentificationCode,

        [EdiFieldValue("DG")]
        UnitedStatesDepartmentofEducationGuarantorIdentificationCode,

        [EdiFieldValue("DL")]
        UnitedStatesDepartmentofEducationLenderIdentificationCode,

        [EdiFieldValue("DN")]
        DentistLicenseNumber,

        [EdiFieldValue("DP")]
        DataProcessingPoint,

        [EdiFieldValue("DS")]
        UnitedStatesDepartmentofEducationSchoolIdentificationCode,

        [EdiFieldValue("EC")]
        ARIElectronicCommerceLocationIDCode,

        [EdiFieldValue("EH")]
        TheatreNumber,

        [EdiFieldValue("EI")]
        EmployeeIdentificationNumber,

        [EdiFieldValue("EP")]
        UsEnvironmentalProtectionAgency_EPA_,

        [EdiFieldValue("EQ")]
        InsuranceCompanyAssignedIdentificationNumber,

        [EdiFieldValue("ER")]
        MortgageeAssignedIdentificationNumber,

        [EdiFieldValue("ES")]
        AutomatedExportSystem_AES_FilerIdentificationCode,

        [EdiFieldValue("FA")]
        FacilityIdentification,

        [EdiFieldValue("FB")]
        FieldCode,

        [EdiFieldValue("FC")]
        FederalCourtJurisdictionIdentifier,

        [EdiFieldValue("FD")]
        FederalCourtDivisionalOfficeNumber,

        [EdiFieldValue("FI")]
        FederalTaxpayersIdentificationNumber,

        [EdiFieldValue("FJ")]
        FederalJurisdiction,

        [EdiFieldValue("FN")]
        UsEnvironmentalProtectionAgency_EPA_LaboratoryCertificationIdentification,

        [EdiFieldValue("GA")]
        PrimaryAgentIdentification,

        [EdiFieldValue("GC")]
        GasCode,

        [EdiFieldValue("HC")]
        HealthCareFinancingAdministration,

        [EdiFieldValue("HN")]
        HealthInsuranceClaim_HIC_Number,

        [EdiFieldValue("LC")]
        AgencyLocationCode_UsGovernment,

        [EdiFieldValue("LD")]
        NISOZ39_53LanguageCodes,

        [EdiFieldValue("LE")]
        ISO639LanguageCodes,

        [EdiFieldValue("LI")]
        LabelerIdentificationCode_LIC,

        [EdiFieldValue("LN")]
        LoanNumber,

        [EdiFieldValue("M3")]
        DisbursingStation,

        [EdiFieldValue("M4")]
        DepartmentofDefenseRoutingIdentifierCode_RIC,

        [EdiFieldValue("M5")]
        JurisdictionCode,

        [EdiFieldValue("M6")]
        DivisionOfficeCode,

        [EdiFieldValue("MA")]
        MailStop,

        [EdiFieldValue("MB")]
        MedicalInformationBureau,

        [EdiFieldValue("MC")]
        MedicaidProviderNumber,

        [EdiFieldValue("MD")]
        ManitobaDepartmentofMinesandResources,

        [EdiFieldValue("MI")]
        MemberIdentificationNumber,

        [EdiFieldValue("MK")]
        Market,

        [EdiFieldValue("ML")]
        MultipleListingServiceVendor_MultipleListingServiceIdentification,

        [EdiFieldValue("MN")]
        MortgageIdentificationNumber,

        [EdiFieldValue("MP")]
        MedicareProviderNumber,

        [EdiFieldValue("MR")]
        MedicaidRecipientIdentificationNumber,

        [EdiFieldValue("NA")]
        NationalAssociationofRealtors_MultipleListingServiceIdentification,

        [EdiFieldValue("ND")]
        ModeDesignator,

        [EdiFieldValue("NI")]
        NationalAssociationofInsuranceCommissioners_NAIC_Identification,

        [EdiFieldValue("NO")]
        NationalCriminalInformationCenterOriginatingAgency,

        [EdiFieldValue("OC")]
        OccupationCode,

        [EdiFieldValue("OP")]
        On_linePaymentandCollection,

        [EdiFieldValue("PA")]
        SecondaryAgentIdentification,

        [EdiFieldValue("PB")]
        PublicIdentification,

        [EdiFieldValue("PC")]
        ProviderCommercialNumber,

        [EdiFieldValue("PI")]
        PayorIdentification,

        [EdiFieldValue("PP")]
        PharmacyProcessorNumber,

        [EdiFieldValue("PR")]
        Pier,

        [EdiFieldValue("RA")]
        RegulatoryAgencyNumber,

        [EdiFieldValue("RB")]
        RealEstateAgent,

        [EdiFieldValue("RC")]
        RealEstateCompany,

        [EdiFieldValue("RD")]
        RealEstateBrokerIdentification,

        [EdiFieldValue("RE")]
        RealEstateLicenseNumber,

        [EdiFieldValue("RT")]
        RailroadTrack,

        [EdiFieldValue("SA")]
        TertiaryAgentIdentification,

        [EdiFieldValue("SB")]
        SocialInsuranceNumber,

        [EdiFieldValue("SD")]
        SaskatchewanDepartmentofEnergyMinesandResources,

        [EdiFieldValue("SF")]
        SuffixCode,

        [EdiFieldValue("SI")]
        StandardIndustryCode_SIC,

        [EdiFieldValue("SJ")]
        StateJurisdiction,

        [EdiFieldValue("SL")]
        StateLicenseNumber,

        [EdiFieldValue("SP")]
        SpecialtyLicenseNumber,

        [EdiFieldValue("ST")]
        State_ProvinceLicenseTag,

        [EdiFieldValue("SV")]
        ServiceProviderNumber,

        [EdiFieldValue("SW")]
        SocietyforWorldwideInterbankFinancialTelecommunications_SWIFT_Address,

        [EdiFieldValue("TA")]
        TaxpayerIDNumber,

        [EdiFieldValue("TC")]
        InternalRevenueServiceTerminalCode,

        [EdiFieldValue("TZ")]
        DepartmentCode,

        [EdiFieldValue("UC")]
        ConsumerCreditIdentificationNumber,

        [EdiFieldValue("UL")]
        UCC_EANLocationCode,

        [EdiFieldValue("UM")]
        UCC_EANLocationCodeSuffix,

        [EdiFieldValue("UP")]
        UniquePhysicianIdentificationNumber_UPIN,

        [EdiFieldValue("UR")]
        UniformResourceLocator_URL,

        [EdiFieldValue("US")]
        UniqueSupplierIdentificationNumber_USIN,

        [EdiFieldValue("WR")]
        WineRegionCode,

        [EdiFieldValue("XV")]
        HealthCareFinancingAdministrationNationalPayerIdentificationNumber_PAYERID,

        [EdiFieldValue("XX")]
        HealthCareFinancingAdministrationNationalProviderIdentifier,

        [EdiFieldValue("ZC")]
        ContractorEstablishmentCode,

        [EdiFieldValue("ZN")]
        Zone,

        [EdiFieldValue("ZY")]
        TemporaryIdentificationNumber,

        [EdiFieldValue("ZZ")]
        MutuallyDefined,
    }
}
