namespace OopFactory.X12.Shared.Models.Typed
{
    using OopFactory.X12.Shared.Attributes;

    public enum DTPQualifier
    {
        [EdiFieldValue("001")]
        CancelAfter,

        [EdiFieldValue("002")]
        DeliveryRequested,

        [EdiFieldValue("003")]
        Invoice,

        [EdiFieldValue("004")]
        PurchaseOrder,

        [EdiFieldValue("005")]
        Sailing,

        [EdiFieldValue("006")]
        Sold,

        [EdiFieldValue("007")]
        Effective,

        [EdiFieldValue("008")]
        PurchaseOrderReceived,

        [EdiFieldValue("009")]
        Process,

        [EdiFieldValue("010")]
        RequestedShip,

        [EdiFieldValue("011")]
        Shipped,

        [EdiFieldValue("012")]
        TermsDiscountDue,

        [EdiFieldValue("013")]
        TermsNetDue,

        [EdiFieldValue("014")]
        DeferredPayment,

        [EdiFieldValue("015")]
        PromotionStart,

        [EdiFieldValue("016")]
        PromotionEnd,

        [EdiFieldValue("017")]
        EstimatedDelivery,

        [EdiFieldValue("018")]
        Available,

        [EdiFieldValue("019")]
        Unloaded,

        [EdiFieldValue("020")]
        Check,

        [EdiFieldValue("021")]
        ChargeBack,

        [EdiFieldValue("022")]
        FreightBill,

        [EdiFieldValue("023")]
        PromotionOrder_Start,

        [EdiFieldValue("024")]
        PromotionOrder_End,

        [EdiFieldValue("025")]
        PromotionShip_Start,

        [EdiFieldValue("026")]
        PromotionShip_End,

        [EdiFieldValue("027")]
        PromotionRequestedDelivery_Start,

        [EdiFieldValue("028")]
        PromotionRequestedDelivery_End,

        [EdiFieldValue("029")]
        PromotionPerformance_Start,

        [EdiFieldValue("030")]
        PromotionPerformance_End,

        [EdiFieldValue("031")]
        PromotionInvoicePerformance_Start,

        [EdiFieldValue("032")]
        PromotionInvoicePerformance_End,

        [EdiFieldValue("033")]
        PromotionFloorStockProtect_Start,

        [EdiFieldValue("034")]
        PromotionFloorStockProtect_End,

        [EdiFieldValue("035")]
        Delivered,

        [EdiFieldValue("036")]
        Expiration,

        [EdiFieldValue("037")]
        ShipNotBefore,

        [EdiFieldValue("038")]
        ShipNoLater,

        [EdiFieldValue("039")]
        ShipWeekOf,

        [EdiFieldValue("040")]
        Status_AfterandIncluding,

        [EdiFieldValue("041")]
        Status_PriorandIncluding,

        [EdiFieldValue("042")]
        Superseded,

        [EdiFieldValue("043")]
        Publication,

        [EdiFieldValue("044")]
        SettlementDateasSpecifiedbytheOriginator,

        [EdiFieldValue("045")]
        EndorsementDate,

        [EdiFieldValue("046")]
        FieldFailure,

        [EdiFieldValue("047")]
        FunctionalTest,

        [EdiFieldValue("048")]
        SystemTest,

        [EdiFieldValue("049")]
        PrototypeTest,

        [EdiFieldValue("050")]
        Received,

        [EdiFieldValue("051")]
        CumulativeQuantityStart,

        [EdiFieldValue("052")]
        CumulativeQuantityEnd,

        [EdiFieldValue("053")]
        BuyersLocal,

        [EdiFieldValue("054")]
        SellersLocal,

        [EdiFieldValue("055")]
        Confirmed,

        [EdiFieldValue("056")]
        EstimatedPortOfEntry,

        [EdiFieldValue("057")]
        ActualPortOfEntry,

        [EdiFieldValue("058")]
        CustomsClearance,

        [EdiFieldValue("059")]
        InlandShip,

        [EdiFieldValue("060")]
        EngineeringChangeLevel,

        [EdiFieldValue("061")]
        CancelifNotDeliveredby,

        [EdiFieldValue("062")]
        Blueprint,

        [EdiFieldValue("063")]
        DoNotDeliverAfter,

        [EdiFieldValue("064")]
        DoNotDeliverBefore,

        [EdiFieldValue("065")]
        FirstScheduleDelivery,

        [EdiFieldValue("066")]
        FirstScheduleShip,

        [EdiFieldValue("067")]
        CurrentScheduleDelivery,

        [EdiFieldValue("068")]
        CurrentScheduleShip,

        [EdiFieldValue("069")]
        PromisedforDelivery,

        [EdiFieldValue("070")]
        ScheduledforDelivery_AfterandIncluding,

        [EdiFieldValue("071")]
        RequestedforDelivery_AfterandIncluding,

        [EdiFieldValue("072")]
        PromisedforDelivery_AfterandIncluding,

        [EdiFieldValue("073")]
        ScheduledforDelivery_PriortoandIncluding,

        [EdiFieldValue("074")]
        RequestedforDelivery_PriortoandIncluding,

        [EdiFieldValue("075")]
        PromisedforDelivery_PriortoandIncluding,

        [EdiFieldValue("076")]
        ScheduledforDelivery_WeekOf,

        [EdiFieldValue("077")]
        RequestedforDelivery_WeekOf,

        [EdiFieldValue("078")]
        PromisedforDelivery_WeekOf,

        [EdiFieldValue("079")]
        PromisedforShipment,

        [EdiFieldValue("080")]
        ScheduledforShipment_AfterandIncluding,

        [EdiFieldValue("081")]
        RequestedforShipment_AfterandIncluding,

        [EdiFieldValue("082")]
        PromisedforShipment_AfterandIncluding,

        [EdiFieldValue("083")]
        ScheduledforShipment_PriortoandIncluding,

        [EdiFieldValue("084")]
        RequestedforShipment_PriortoandIncluding,

        [EdiFieldValue("085")]
        PromisedforShipment_PriortoandIncluding,

        [EdiFieldValue("086")]
        ScheduledforShipment_WeekOf,

        [EdiFieldValue("087")]
        RequestedforShipment_WeekOf,

        [EdiFieldValue("088")]
        PromisedforShipment_WeekOf,

        [EdiFieldValue("089")]
        Inquiry,

        [EdiFieldValue("090")]
        ReportStart,

        [EdiFieldValue("091")]
        ReportEnd,

        [EdiFieldValue("092")]
        ContractEffective,

        [EdiFieldValue("093")]
        ContractExpiration,

        [EdiFieldValue("094")]
        Manufacture,

        [EdiFieldValue("095")]
        BillOfLading,

        [EdiFieldValue("096")]
        Discharge,

        [EdiFieldValue("097")]
        TransactionCreation,

        [EdiFieldValue("098")]
        Bid_Effective,

        [EdiFieldValue("099")]
        BidOpen_DateBidsWillBeOpened,

        [EdiFieldValue("100")]
        NoShippingScheduleEstablishedasOf,

        [EdiFieldValue("101")]
        NoProductionScheduleEstablishedasOf,

        [EdiFieldValue("102")]
        Issue,

        [EdiFieldValue("103")]
        Award,

        [EdiFieldValue("104")]
        SystemSurvey,

        [EdiFieldValue("105")]
        QualityRating,

        [EdiFieldValue("106")]
        RequiredBy,

        [EdiFieldValue("107")]
        Deposit,

        [EdiFieldValue("108")]
        Postmark,

        [EdiFieldValue("109")]
        ReceivedatLockbox,

        [EdiFieldValue("110")]
        OriginallyScheduledShip,

        [EdiFieldValue("111")]
        Manifest_ShipNotice,

        [EdiFieldValue("112")]
        BuyersDock,

        [EdiFieldValue("113")]
        SampleRequired,

        [EdiFieldValue("114")]
        ToolingRequired,

        [EdiFieldValue("115")]
        SampleAvailable,

        [EdiFieldValue("116")]
        ScheduledInterchangeDelivery,

        [EdiFieldValue("118")]
        RequestedPick_up,

        [EdiFieldValue("119")]
        TestPerformed,

        [EdiFieldValue("120")]
        ControlPlan,

        [EdiFieldValue("121")]
        FeasibilitySignOff,

        [EdiFieldValue("122")]
        FailureModeEffective,

        [EdiFieldValue("124")]
        GroupContractEffective,

        [EdiFieldValue("125")]
        GroupContractExpiration,

        [EdiFieldValue("126")]
        WholesaleContractEffective,

        [EdiFieldValue("127")]
        WholesaleContractExpiration,

        [EdiFieldValue("128")]
        ReplacementEffective,

        [EdiFieldValue("129")]
        CustomerContractEffective,

        [EdiFieldValue("130")]
        CustomerContractExpiration,

        [EdiFieldValue("131")]
        ItemContractEffective,

        [EdiFieldValue("132")]
        ItemContractExpiration,

        [EdiFieldValue("133")]
        AccountsReceivable_StatementDate,

        [EdiFieldValue("134")]
        ReadyforInspection,

        [EdiFieldValue("135")]
        Booking,

        [EdiFieldValue("136")]
        TechnicalRating,

        [EdiFieldValue("137")]
        DeliveryRating,

        [EdiFieldValue("138")]
        CommercialRating,

        [EdiFieldValue("139")]
        Estimated,

        [EdiFieldValue("140")]
        Actual,

        [EdiFieldValue("141")]
        Assigned,

        [EdiFieldValue("142")]
        Loss,

        [EdiFieldValue("143")]
        DueDateOfFirstPaymenttoPrincipalandInterest,

        [EdiFieldValue("144")]
        EstimatedAcceptance,

        [EdiFieldValue("145")]
        OpeningDate,

        [EdiFieldValue("146")]
        ClosingDate,

        [EdiFieldValue("147")]
        DueDateLastCompleteInstallmentPaid,

        [EdiFieldValue("148")]
        DateOfLocalOfficeApprovalOfConveyanceOfDamagedRealEstateProperty,

        [EdiFieldValue("149")]
        DateDeedFiledforRecord,

        [EdiFieldValue("150")]
        ServicePeriodStart,

        [EdiFieldValue("151")]
        ServicePeriodEnd,

        [EdiFieldValue("152")]
        EffectiveDateOfChange,

        [EdiFieldValue("153")]
        ServiceInterruption,

        [EdiFieldValue("154")]
        AdjustmentPeriodStart,

        [EdiFieldValue("155")]
        AdjustmentPeriodEnd,

        [EdiFieldValue("156")]
        AllotmentPeriodStart,

        [EdiFieldValue("157")]
        TestPeriodStart,

        [EdiFieldValue("158")]
        TestPeriodEnding,

        [EdiFieldValue("159")]
        BidPriceException,

        [EdiFieldValue("160")]
        SamplestobeReturnedBy,

        [EdiFieldValue("161")]
        LoadedonVessel,

        [EdiFieldValue("162")]
        PendingArchive,

        [EdiFieldValue("163")]
        ActualArchive,

        [EdiFieldValue("164")]
        FirstIssue,

        [EdiFieldValue("165")]
        FinalIssue,

        [EdiFieldValue("166")]
        Message,

        [EdiFieldValue("167")]
        MostRecentRevision_OrInitialVersion,

        [EdiFieldValue("168")]
        Release,

        [EdiFieldValue("169")]
        ProductAvailabilityDate,

        [EdiFieldValue("170")]
        SupplementalIssue,

        [EdiFieldValue("171")]
        Revision,

        [EdiFieldValue("172")]
        Correction,

        [EdiFieldValue("173")]
        WeekEnding,

        [EdiFieldValue("174")]
        MonthEnding,

        [EdiFieldValue("175")]
        Cancelifnotshippedby,

        [EdiFieldValue("176")]
        Expeditedon,

        [EdiFieldValue("177")]
        Cancellation,

        [EdiFieldValue("178")]
        Hold_AsOf,

        [EdiFieldValue("179")]
        HoldasStock_AsOf,

        [EdiFieldValue("180")]
        NoPromise_AsOf,

        [EdiFieldValue("181")]
        StopWork_AsOf,

        [EdiFieldValue("182")]
        WillAdvise_AsOf,

        [EdiFieldValue("183")]
        Connection,

        [EdiFieldValue("184")]
        Inventory,

        [EdiFieldValue("185")]
        VesselRegistry,

        [EdiFieldValue("186")]
        InvoicePeriodStart,

        [EdiFieldValue("187")]
        InvoicePeriodEnd,

        [EdiFieldValue("188")]
        CreditAdvice,

        [EdiFieldValue("189")]
        DebitAdvice,

        [EdiFieldValue("190")]
        ReleasedtoVessel,

        [EdiFieldValue("191")]
        MaterialSpecification,

        [EdiFieldValue("192")]
        DeliveryTicket,

        [EdiFieldValue("193")]
        PeriodStart,

        [EdiFieldValue("194")]
        PeriodEnd,

        [EdiFieldValue("195")]
        ContractRe_Open,

        [EdiFieldValue("196")]
        Start,

        [EdiFieldValue("197")]
        End,

        [EdiFieldValue("198")]
        Completion,

        [EdiFieldValue("199")]
        Seal,

        [EdiFieldValue("200")]
        AssemblyStart,

        [EdiFieldValue("201")]
        Acceptance,

        [EdiFieldValue("202")]
        MasterLeaseAgreement,

        [EdiFieldValue("203")]
        FirstProduced,

        [EdiFieldValue("204")]
        OfficialRailCarInterchange_EitherActualorAgreedUpon,

        [EdiFieldValue("205")]
        Transmitted,

        [EdiFieldValue("206")]
        Status_OutsideProcessor,

        [EdiFieldValue("207")]
        Status_Commercial,

        [EdiFieldValue("208")]
        LotNumberExpiration,

        [EdiFieldValue("209")]
        ContractPerformanceStart,

        [EdiFieldValue("210")]
        ContractPerformanceDelivery,

        [EdiFieldValue("211")]
        ServiceRequested,

        [EdiFieldValue("212")]
        ReturnedtoCustomer,

        [EdiFieldValue("213")]
        AdjustmenttoBillDated,

        [EdiFieldValue("214")]
        DateOfRepair_Service,

        [EdiFieldValue("215")]
        InterruptionStart,

        [EdiFieldValue("216")]
        InterruptionEnd,

        [EdiFieldValue("217")]
        Spud,

        [EdiFieldValue("218")]
        InitialCompletion,

        [EdiFieldValue("219")]
        PluggedandAbandoned,

        [EdiFieldValue("220")]
        Penalty,

        [EdiFieldValue("221")]
        PenaltyBegin,

        [EdiFieldValue("222")]
        Birth,

        [EdiFieldValue("223")]
        BirthCertificate,

        [EdiFieldValue("224")]
        Adoption,

        [EdiFieldValue("225")]
        Christening,

        [EdiFieldValue("226")]
        LeaseCommencement,

        [EdiFieldValue("227")]
        LeaseTermStart,

        [EdiFieldValue("228")]
        LeaseTermEnd,

        [EdiFieldValue("229")]
        RentStart,

        [EdiFieldValue("230")]
        Installation,

        [EdiFieldValue("231")]
        ProgressPayment,

        [EdiFieldValue("232")]
        ClaimStatementPeriodStart,

        [EdiFieldValue("233")]
        ClaimStatementPeriodEnd,

        [EdiFieldValue("234")]
        SettlementDate,

        [EdiFieldValue("235")]
        DelayedBilling_NotDelayedPayment,

        [EdiFieldValue("236")]
        LenderCreditCheck,

        [EdiFieldValue("237")]
        StudentSigned,

        [EdiFieldValue("238")]
        ScheduleRelease,

        [EdiFieldValue("239")]
        Baseline,

        [EdiFieldValue("240")]
        BaselineStart,

        [EdiFieldValue("241")]
        BaselineComplete,

        [EdiFieldValue("242")]
        ActualStart,

        [EdiFieldValue("243")]
        ActualComplete,

        [EdiFieldValue("244")]
        EstimatedStart,

        [EdiFieldValue("245")]
        EstimatedCompletion,

        [EdiFieldValue("246")]
        Startnoearlierthan,

        [EdiFieldValue("247")]
        Startnolaterthan,

        [EdiFieldValue("248")]
        Finishnolaterthan,

        [EdiFieldValue("249")]
        Finishnoearlierthan,

        [EdiFieldValue("250")]
        Mandatory_orTarget_Start,

        [EdiFieldValue("251")]
        Mandatory_orTarget_Finish,

        [EdiFieldValue("252")]
        EarlyStart,

        [EdiFieldValue("253")]
        EarlyFinish,

        [EdiFieldValue("254")]
        LateStart,

        [EdiFieldValue("255")]
        LateFinish,

        [EdiFieldValue("256")]
        ScheduledStart,

        [EdiFieldValue("257")]
        ScheduledFinish,

        [EdiFieldValue("258")]
        OriginalEarlyStart,

        [EdiFieldValue("259")]
        OriginalEarlyFinish,

        [EdiFieldValue("260")]
        RestDay,

        [EdiFieldValue("261")]
        RestStart,

        [EdiFieldValue("262")]
        RestFinish,

        [EdiFieldValue("263")]
        Holiday,

        [EdiFieldValue("264")]
        HolidayStart,

        [EdiFieldValue("265")]
        HolidayFinish,

        [EdiFieldValue("266")]
        Base,

        [EdiFieldValue("267")]
        Timenow,

        [EdiFieldValue("268")]
        EndDateOfSupport,

        [EdiFieldValue("269")]
        DateAccountMatures,

        [EdiFieldValue("270")]
        DateFiled,

        [EdiFieldValue("271")]
        PenaltyEnd,

        [EdiFieldValue("272")]
        ExitPlantDate,

        [EdiFieldValue("273")]
        LatestOnBoardCarrierDate,

        [EdiFieldValue("274")]
        RequestedDepartureDate,

        [EdiFieldValue("275")]
        Approved,

        [EdiFieldValue("276")]
        ContractStart,

        [EdiFieldValue("277")]
        ContractDefinition,

        [EdiFieldValue("278")]
        LastItemDelivery,

        [EdiFieldValue("279")]
        ContractCompletion,

        [EdiFieldValue("280")]
        DateCourseOfOrthodonticsTreatmentBeganorisExpectedtoBegin,

        [EdiFieldValue("281")]
        OverTargetBaselineMonth,

        [EdiFieldValue("282")]
        PreviousReport,

        [EdiFieldValue("283")]
        FundsAppropriation_Start,

        [EdiFieldValue("284")]
        FundsAppropriation_End,

        [EdiFieldValue("285")]
        EmploymentorHire,

        [EdiFieldValue("286")]
        Retirement,

        [EdiFieldValue("287")]
        Medicare,

        [EdiFieldValue("288")]
        ConsolidatedOmnibusBudgetReconciliationAct_COBRA_288,

        [EdiFieldValue("289")]
        PremiumPaidtoDate,

        [EdiFieldValue("290")]
        CoordinationOfBenefits,

        [EdiFieldValue("291")]
        Plan,

        [EdiFieldValue("292")]
        Benefit,

        [EdiFieldValue("293")]
        Education,

        [EdiFieldValue("294")]
        EarningsEffectiveDate,

        [EdiFieldValue("295")]
        PrimaryCareProvider,

        [EdiFieldValue("296")]
        ReturntoWork,

        [EdiFieldValue("297")]
        DateLastWorked,

        [EdiFieldValue("298")]
        LatestAbsence,

        [EdiFieldValue("299")]
        Illness,

        [EdiFieldValue("300")]
        EnrollmentSignatureDate,

        [EdiFieldValue("301")]
        ConsolidatedOmnibusBudgetReconciliationAct_COBRA_QualifyingEvent,

        [EdiFieldValue("302")]
        Maintenance,

        [EdiFieldValue("303")]
        MaintenanceEffective,

        [EdiFieldValue("304")]
        LatestVisitorConsultation,

        [EdiFieldValue("305")]
        NetCreditServiceDate,

        [EdiFieldValue("306")]
        AdjustmentEffectiveDate,

        [EdiFieldValue("307")]
        Eligibility,

        [EdiFieldValue("308")]
        Pre_AwardSurvey,

        [EdiFieldValue("309")]
        PlanTermination,

        [EdiFieldValue("310")]
        DateOfClosing,

        [EdiFieldValue("311")]
        LatestReceivingDate_CutoffDate,

        [EdiFieldValue("312")]
        SalaryDeferral,

        [EdiFieldValue("313")]
        Cycle,

        [EdiFieldValue("314")]
        Disability,

        [EdiFieldValue("315")]
        Offset,

        [EdiFieldValue("316")]
        PriorIncorrectDateOfBirth,

        [EdiFieldValue("317")]
        CorrectedDateOfBirth,

        [EdiFieldValue("318")]
        Added,

        [EdiFieldValue("319")]
        Failed,

        [EdiFieldValue("320")]
        DateForeclosureProceedingsInstituted,

        [EdiFieldValue("321")]
        Purchased,

        [EdiFieldValue("322")]
        PutintoService,

        [EdiFieldValue("323")]
        Replaced,

        [EdiFieldValue("324")]
        Returned,

        [EdiFieldValue("325")]
        DisbursementDate,

        [EdiFieldValue("326")]
        GuaranteeDate,

        [EdiFieldValue("327")]
        QuarterEnding,

        [EdiFieldValue("328")]
        Changed,

        [EdiFieldValue("329")]
        Terminated,

        [EdiFieldValue("330")]
        ReferralDate,

        [EdiFieldValue("331")]
        EvaluationDate,

        [EdiFieldValue("332")]
        PlacementDate,

        [EdiFieldValue("333")]
        IndividualEducationPlan_IEP,

        [EdiFieldValue("334")]
        Re_evaluationDate,

        [EdiFieldValue("335")]
        DismissalDate,

        [EdiFieldValue("336")]
        EmploymentBegin,

        [EdiFieldValue("337")]
        EmploymentEnd,

        [EdiFieldValue("338")]
        MedicareBegin,

        [EdiFieldValue("339")]
        MedicareEnd,

        [EdiFieldValue("340")]
        ConsolidatedOmnibusBudgetReconciliationAct_COBRA_Begin_340,

        [EdiFieldValue("341")]
        ConsolidatedOmnibusBudgetReconciliationAct_COBRA_End_341,

        [EdiFieldValue("342")]
        PremiumPaidToDateBegin,

        [EdiFieldValue("343")]
        PremiumPaidToDateEnd,

        [EdiFieldValue("344")]
        CoordinationOfBenefitsBegin,

        [EdiFieldValue("345")]
        CoordinationOfBenefitsEnd,

        [EdiFieldValue("346")]
        PlanBegin,

        [EdiFieldValue("347")]
        PlanEnd,

        [EdiFieldValue("348")]
        BenefitBegin,

        [EdiFieldValue("349")]
        BenefitEnd,

        [EdiFieldValue("350")]
        EducationBegin,

        [EdiFieldValue("351")]
        EducationEnd,

        [EdiFieldValue("352")]
        PrimaryCareProviderBegin,

        [EdiFieldValue("353")]
        PrimaryCareProviderEnd,

        [EdiFieldValue("354")]
        IllnessBegin,

        [EdiFieldValue("355")]
        IllnessEnd,

        [EdiFieldValue("356")]
        EligibilityBegin,

        [EdiFieldValue("357")]
        EligibilityEnd,

        [EdiFieldValue("358")]
        CycleBegin,

        [EdiFieldValue("359")]
        CycleEnd,

        [EdiFieldValue("360")]
        DisabilityBegin,

        [EdiFieldValue("361")]
        DisabilityEnd,

        [EdiFieldValue("362")]
        OffsetBegin,

        [EdiFieldValue("363")]
        OffsetEnd,

        [EdiFieldValue("364")]
        PlanPeriodElectionBegin,

        [EdiFieldValue("365")]
        PlanPeriodElectionEnd,

        [EdiFieldValue("366")]
        PlanPeriodElection,

        [EdiFieldValue("367")]
        DuetoCustomer,

        [EdiFieldValue("368")]
        Submittal,

        [EdiFieldValue("369")]
        EstimatedDepartureDate,

        [EdiFieldValue("370")]
        ActualDepartureDate,

        [EdiFieldValue("371")]
        EstimatedArrivalDate,

        [EdiFieldValue("372")]
        ActualArrivalDate,

        [EdiFieldValue("373")]
        OrderStart,

        [EdiFieldValue("374")]
        OrderEnd,

        [EdiFieldValue("375")]
        DeliveryStart,

        [EdiFieldValue("376")]
        DeliveryEnd,

        [EdiFieldValue("377")]
        ContractCostsThrough,

        [EdiFieldValue("378")]
        FinancialInformationSubmission,

        [EdiFieldValue("379")]
        BusinessTermination,

        [EdiFieldValue("380")]
        ApplicantSigned,

        [EdiFieldValue("381")]
        CosignerSigned,

        [EdiFieldValue("382")]
        Enrollment,

        [EdiFieldValue("383")]
        AdjustedHire,

        [EdiFieldValue("384")]
        CreditedService,

        [EdiFieldValue("385")]
        CreditedServiceBegin,

        [EdiFieldValue("386")]
        CreditedServiceEnd,

        [EdiFieldValue("387")]
        DeferredDistribution,

        [EdiFieldValue("388")]
        PaymentCommencement,

        [EdiFieldValue("389")]
        PayrollPeriod,

        [EdiFieldValue("390")]
        PayrollPeriodBegin,

        [EdiFieldValue("391")]
        PayrollPeriodEnd,

        [EdiFieldValue("392")]
        PlanEntry,

        [EdiFieldValue("393")]
        PlanParticipationSuspension,

        [EdiFieldValue("394")]
        Rehire,

        [EdiFieldValue("395")]
        Retermination,

        [EdiFieldValue("396")]
        Termination,

        [EdiFieldValue("397")]
        Valuation,

        [EdiFieldValue("398")]
        VestingService,

        [EdiFieldValue("399")]
        VestingServiceBegin,

        [EdiFieldValue("400")]
        VestingServiceEnd,

        [EdiFieldValue("401")]
        DuplicateBill,

        [EdiFieldValue("402")]
        AdjustmentPromised,

        [EdiFieldValue("403")]
        AdjustmentProcessed,

        [EdiFieldValue("404")]
        YearEnding,

        [EdiFieldValue("405")]
        Production,

        [EdiFieldValue("406")]
        MaterialClassification,

        [EdiFieldValue("408")]
        Weighed,

        [EdiFieldValue("409")]
        DateOfDeedinLieu,

        [EdiFieldValue("410")]
        DateOfFirmCommitment,

        [EdiFieldValue("411")]
        ExpirationDateOfExtensionToForeclose,

        [EdiFieldValue("412")]
        DateOfNoticetoConvey,

        [EdiFieldValue("413")]
        DateOfReleaseOfBankruptcy,

        [EdiFieldValue("414")]
        OptimisticEarlyStart,

        [EdiFieldValue("415")]
        OptimisticEarlyFinish,

        [EdiFieldValue("416")]
        OptimisticLateStart,

        [EdiFieldValue("417")]
        OptimisticLateFinish,

        [EdiFieldValue("418")]
        MostLikelyEarlyStart,

        [EdiFieldValue("419")]
        MostLikelyEarlyFinish,

        [EdiFieldValue("420")]
        MostLikelyLateStart,

        [EdiFieldValue("421")]
        MostLikelyLateFinish,

        [EdiFieldValue("422")]
        PessimisticEarlyStart,

        [EdiFieldValue("423")]
        PessimisticEarlyFinish,

        [EdiFieldValue("424")]
        PessimisticLateStart,

        [EdiFieldValue("425")]
        PessimisticLateFinish,

        [EdiFieldValue("426")]
        FirstPaymentDue,

        [EdiFieldValue("427")]
        FirstInterestPaymentDue,

        [EdiFieldValue("428")]
        SubsequentInterestPaymentDue,

        [EdiFieldValue("429")]
        IrregularInterestPaymentDue,

        [EdiFieldValue("430")]
        GuarantorReceived,

        [EdiFieldValue("431")]
        OnsetOfCurrentSymptomsorIllness,

        [EdiFieldValue("432")]
        Submission,

        [EdiFieldValue("433")]
        Removed,

        [EdiFieldValue("434")]
        Statement,

        [EdiFieldValue("435")]
        Admission,

        [EdiFieldValue("436")]
        InsuranceCard,

        [EdiFieldValue("437")]
        SpouseRetirement,

        [EdiFieldValue("438")]
        OnsetOfSimilarSymptomsorIllness,

        [EdiFieldValue("439")]
        Accident,

        [EdiFieldValue("440")]
        ReleaseOfInformation,

        [EdiFieldValue("441")]
        PriorPlacement,

        [EdiFieldValue("442")]
        DateOfDeath,

        [EdiFieldValue("443")]
        PeerReviewOrganization_PRO_ApprovedStay,

        [EdiFieldValue("444")]
        FirstVisitorConsultation,

        [EdiFieldValue("445")]
        InitialPlacement,

        [EdiFieldValue("446")]
        Replacement,

        [EdiFieldValue("447")]
        Occurrence,

        [EdiFieldValue("448")]
        OccurrenceSpan,

        [EdiFieldValue("449")]
        OccurrenceSpanFrom,

        [EdiFieldValue("450")]
        OccurrenceSpanTo,

        [EdiFieldValue("451")]
        InitialFeeDue,

        [EdiFieldValue("452")]
        AppliancePlacement,

        [EdiFieldValue("453")]
        AcuteManifestationOfAChronicCondition,

        [EdiFieldValue("454")]
        InitialTreatment,

        [EdiFieldValue("455")]
        LastX_Ray,

        [EdiFieldValue("456")]
        Surgery,

        [EdiFieldValue("457")]
        ContinuousPassiveMotion_CPM,

        [EdiFieldValue("458")]
        Certification,

        [EdiFieldValue("459")]
        NursingHomeFrom,

        [EdiFieldValue("460")]
        NursingHomeTo,

        [EdiFieldValue("461")]
        LastCertification,

        [EdiFieldValue("462")]
        DateOfLocalOfficeApprovalOfConveyanceOfOccupiedRealEstateProperty,

        [EdiFieldValue("463")]
        BeginTherapy,

        [EdiFieldValue("464")]
        OxygenTherapyFrom,

        [EdiFieldValue("465")]
        OxygenTherapyTo,

        [EdiFieldValue("466")]
        OxygenTherapy,

        [EdiFieldValue("467")]
        Signature,

        [EdiFieldValue("468")]
        PrescriptionFill,

        [EdiFieldValue("469")]
        ProviderSignature,

        [EdiFieldValue("470")]
        DateOfLocalOfficeCertificationOfConveyanceOfDamagedRealEstateProperty,

        [EdiFieldValue("471")]
        Prescription,

        [EdiFieldValue("472")]
        Service,

        [EdiFieldValue("473")]
        MedicaidBegin,

        [EdiFieldValue("474")]
        MedicaidEnd,

        [EdiFieldValue("475")]
        Medicaid,

        [EdiFieldValue("476")]
        PeerReviewOrganization_PRO_ApprovedStayFrom,

        [EdiFieldValue("477")]
        PeerReviewOrganization_PRO_ApprovedStayTo,

        [EdiFieldValue("478")]
        PrescriptionFrom,

        [EdiFieldValue("479")]
        PrescriptionTo,

        [EdiFieldValue("480")]
        ArterialBloodGasTest,

        [EdiFieldValue("481")]
        OxygenSaturationTest,

        [EdiFieldValue("482")]
        PregnancyBegin,

        [EdiFieldValue("483")]
        PregnancyEnd,

        [EdiFieldValue("484")]
        LastMenstrualPeriod,

        [EdiFieldValue("485")]
        InjuryBegin,

        [EdiFieldValue("486")]
        InjuryEnd,

        [EdiFieldValue("487")]
        NursingHome,

        [EdiFieldValue("488")]
        CollateralDependent,

        [EdiFieldValue("489")]
        CollateralDependentBegin,

        [EdiFieldValue("490")]
        CollateralDependentEnd,

        [EdiFieldValue("491")]
        SponsoredDependent,

        [EdiFieldValue("492")]
        SponsoredDependentBegin,

        [EdiFieldValue("493")]
        SponsoredDependentEnd,

        [EdiFieldValue("494")]
        Deductible,

        [EdiFieldValue("495")]
        OutOfPocket,

        [EdiFieldValue("496")]
        ContractAuditDate,

        [EdiFieldValue("497")]
        LatestDeliveryDateatPier,

        [EdiFieldValue("498")]
        MortgageeReportedCurtailmentDate,

        [EdiFieldValue("499")]
        MortgageeOfficialSignatureDate,

        [EdiFieldValue("500")]
        Resubmission,

        [EdiFieldValue("501")]
        ExpectedReply,

        [EdiFieldValue("502")]
        DroppedtoLessthanHalfTime,

        [EdiFieldValue("503")]
        RepaymentBegin,

        [EdiFieldValue("504")]
        LoanServicingTransfer,

        [EdiFieldValue("505")]
        LoanPurchase,

        [EdiFieldValue("506")]
        LastNotification,

        [EdiFieldValue("507")]
        Extract,

        [EdiFieldValue("508")]
        Extended,

        [EdiFieldValue("509")]
        ServicerSignatureDate,

        [EdiFieldValue("510")]
        DatePacked,

        [EdiFieldValue("511")]
        ShelfLifeExpiration,

        [EdiFieldValue("512")]
        WarrantyExpiration,

        [EdiFieldValue("513")]
        Overhauled,

        [EdiFieldValue("514")]
        Transferred,

        [EdiFieldValue("515")]
        Notified,

        [EdiFieldValue("516")]
        Discovered,

        [EdiFieldValue("517")]
        Inspected,

        [EdiFieldValue("518")]
        Voucher_DateOf,

        [EdiFieldValue("519")]
        DateBankruptcyFiled,

        [EdiFieldValue("520")]
        DateOfDamage,

        [EdiFieldValue("521")]
        DateHazardInsurancePolicyCancelled,

        [EdiFieldValue("522")]
        ExpirationDatetoSubmitTitleEvidence,

        [EdiFieldValue("523")]
        DateOfClaim,

        [EdiFieldValue("524")]
        DateOfNoticeOfReferralforAssignment,

        [EdiFieldValue("525")]
        DateOfNoticeOfProbableIneligibilityforAssignment,

        [EdiFieldValue("526")]
        DateOfForeclosureNotice,

        [EdiFieldValue("527")]
        ExpirationOfForeclosureTimeframe,

        [EdiFieldValue("528")]
        DatePossessoryActionInitiated,

        [EdiFieldValue("529")]
        DateOfPossession,

        [EdiFieldValue("530")]
        DateOfLastInstallmentReceived,

        [EdiFieldValue("531")]
        DateOfAcquisitionOfTitle,

        [EdiFieldValue("532")]
        ExpirationOfExtensiontoConvey,

        [EdiFieldValue("533")]
        DateOfAssignmentApproval,

        [EdiFieldValue("534")]
        DateOfAssignmentRejection,

        [EdiFieldValue("535")]
        CurtailmentDatefromAdviceOfPayment,

        [EdiFieldValue("536")]
        ExpirationOfExtensiontoSubmitFiscalData,

        [EdiFieldValue("537")]
        DateDocumentation,

        orPaperwork,

        orBothWasSent,

        [EdiFieldValue("538")]
        MakegoodCommercialDate,

        [EdiFieldValue("539")]
        PolicyEffective,

        [EdiFieldValue("540")]
        PolicyExpiration,

        [EdiFieldValue("541")]
        EmployeeEffectiveDateOfCoverage,

        [EdiFieldValue("542")]
        DateOfRepresentation,

        [EdiFieldValue("543")]
        LastPremiumPaidDate,

        [EdiFieldValue("544")]
        DateReportedtoEmployer,

        [EdiFieldValue("545")]
        DateReportedtoClaimAdministrator,

        [EdiFieldValue("546")]
        DateOfMaximumMedicalImprovement,

        [EdiFieldValue("547")]
        DateOfLoan,

        [EdiFieldValue("548")]
        DateOfAdvance,

        [EdiFieldValue("549")]
        BeginningLayDate,

        [EdiFieldValue("550")]
        CertificateEffective,

        [EdiFieldValue("551")]
        BenefitApplicationDate,

        [EdiFieldValue("552")]
        ActualReturntoWork,

        [EdiFieldValue("553")]
        ReleasedReturntoWork,

        [EdiFieldValue("554")]
        EndingLayDate,

        [EdiFieldValue("555")]
        EmployeeWagesCeased,

        [EdiFieldValue("556")]
        LastSalaryIncrease,

        [EdiFieldValue("557")]
        EmployeeLaidOff,

        [EdiFieldValue("558")]
        InjuryorIllness,

        [EdiFieldValue("559")]
        OldestUnpaidInstallment,

        [EdiFieldValue("560")]
        PreforeclosureAcceptanceDate,

        [EdiFieldValue("561")]
        PreforeclosureSaleClosingDate,

        [EdiFieldValue("562")]
        DateOfFirstUncuredDefault,

        [EdiFieldValue("563")]
        DateDefaultWasCured,

        [EdiFieldValue("564")]
        DateOfFirstMortgagePayment,

        [EdiFieldValue("565")]
        DateOfPropertyInspection,

        [EdiFieldValue("566")]
        DateTotalAmountOfDelinquencyReported,

        [EdiFieldValue("567")]
        DateOutstandingLoanBalanceReported,

        [EdiFieldValue("568")]
        DateForeclosureSaleScheduled,

        [EdiFieldValue("569")]
        DateForeclosureHeld,

        [EdiFieldValue("570")]
        DateRedemptionPeriodEnds,

        [EdiFieldValue("571")]
        DateVoluntaryConveyanceAccepted,

        [EdiFieldValue("572")]
        DatePropertySold,

        [EdiFieldValue("573")]
        DateClaimPaid,

        [EdiFieldValue("574")]
        ActionBeginDate,

        [EdiFieldValue("575")]
        ProjectedActionEndDate,

        [EdiFieldValue("576")]
        ActionEndDate,

        [EdiFieldValue("577")]
        OriginalMaturityDate,

        [EdiFieldValue("578")]
        DateReferredtoAttorneyforForeclosure,

        [EdiFieldValue("579")]
        PlannedRelease,

        [EdiFieldValue("580")]
        ActualRelease,

        [EdiFieldValue("581")]
        ContractPeriod,

        [EdiFieldValue("582")]
        ReportPeriod,

        [EdiFieldValue("583")]
        Suspension,

        [EdiFieldValue("584")]
        Reinstatement_584,

        [EdiFieldValue("585")]
        Report,

        [EdiFieldValue("586")]
        FirstContact,

        [EdiFieldValue("587")]
        ProjectedForeclosureSaleDate,

        [EdiFieldValue("589")]
        DateAssignmentFiledforRecord,

        [EdiFieldValue("590")]
        DateOfAppraisal,

        [EdiFieldValue("591")]
        ExpirationDateOfExtensiontoAssign,

        [EdiFieldValue("592")]
        DateOfExtensiontoConvey,

        [EdiFieldValue("593")]
        DateHazardInsurancePolicyRefused,

        [EdiFieldValue("594")]
        HighFabricationReleaseAuthorization,

        [EdiFieldValue("595")]
        HighRawMaterialAuthorization,

        [EdiFieldValue("596")]
        MaterialChangeNotice,

        [EdiFieldValue("597")]
        LatestDeliveryDateatRailRamp,

        [EdiFieldValue("598")]
        Rejected,

        [EdiFieldValue("599")]
        RepaymentScheduleSent,

        [EdiFieldValue("600")]
        AsOf,

        [EdiFieldValue("601")]
        FirstSubmission,

        [EdiFieldValue("602")]
        SubsequentSubmission,

        [EdiFieldValue("603")]
        Renewal,

        [EdiFieldValue("604")]
        Withdrawn,

        [EdiFieldValue("606")]
        CertificationPeriodStart,

        [EdiFieldValue("607")]
        CertificationRevision,

        [EdiFieldValue("608")]
        ContinuousCoverageDates,

        [EdiFieldValue("609")]
        PrearrangedDealMatch,

        [EdiFieldValue("610")]
        ContingencyEnd,

        [EdiFieldValue("611")]
        OxygenTherapyEvaluation,

        [EdiFieldValue("612")]
        ShutIn,

        [EdiFieldValue("613")]
        AllowableEffective,

        [EdiFieldValue("614")]
        FirstSales,

        [EdiFieldValue("615")]
        DateAcquired,

        [EdiFieldValue("616")]
        InterviewerSigned,

        [EdiFieldValue("617")]
        ApplicationLoggedDate,

        [EdiFieldValue("618")]
        ReviewDate,

        [EdiFieldValue("619")]
        DecisionDate,

        [EdiFieldValue("620")]
        PreviouslyResided,

        [EdiFieldValue("621")]
        Reported,

        [EdiFieldValue("622")]
        Checked,

        [EdiFieldValue("623")]
        Settled,

        [EdiFieldValue("624")]
        PresentlyResiding,

        [EdiFieldValue("625")]
        EmployedinthisPosition,

        [EdiFieldValue("626")]
        Verified,

        [EdiFieldValue("627")]
        SecondAdmissionDate,

        [EdiFieldValue("629")]
        AccountOpened,

        [EdiFieldValue("630")]
        AccountClosed,

        [EdiFieldValue("631")]
        PropertyAcquired,

        [EdiFieldValue("632")]
        PropertyBuilt,

        [EdiFieldValue("633")]
        EmployedinthisProfession,

        [EdiFieldValue("634")]
        NextReviewDate,

        [EdiFieldValue("635")]
        InitialContactDate,

        [EdiFieldValue("636")]
        DateOfLastUpdate,

        [EdiFieldValue("637")]
        SecondDischargeDate,

        [EdiFieldValue("638")]
        DateOfLastDraw,

        [EdiFieldValue("640")]
        Complaint,

        [EdiFieldValue("641")]
        Option,

        [EdiFieldValue("642")]
        Solicitation,

        [EdiFieldValue("643")]
        Clause,

        [EdiFieldValue("644")]
        Meeting,

        [EdiFieldValue("646")]
        RentalPeriod,

        [EdiFieldValue("647")]
        NextPayIncrease,

        [EdiFieldValue("648")]
        PeriodCoveredbySourceDocuments,

        [EdiFieldValue("649")]
        DocumentDue,

        [EdiFieldValue("650")]
        CourtNotice,

        [EdiFieldValue("651")]
        ExpectedFundingDate,

        [EdiFieldValue("652")]
        AssignmentRecorded,

        [EdiFieldValue("653")]
        CaseReopened,

        [EdiFieldValue("655")]
        PreviousCourtEvent,

        [EdiFieldValue("656")]
        LastDatetoObject,

        [EdiFieldValue("657")]
        CourtEvent,

        [EdiFieldValue("658")]
        LastDateToFileAClaim,

        [EdiFieldValue("659")]
        CaseConverted,

        [EdiFieldValue("660")]
        DebtIncurred,

        [EdiFieldValue("661")]
        Judgment,

        [EdiFieldValue("662")]
        WagesStart,

        [EdiFieldValue("663")]
        WagesEnd,

        [EdiFieldValue("664")]
        DateThroughWhichPropertyTaxesHaveBeenPaid,

        [EdiFieldValue("665")]
        PaidThroughDate,

        [EdiFieldValue("666")]
        DatePaid,

        [EdiFieldValue("667")]
        AnesthesiaAdministration,

        [EdiFieldValue("668")]
        PriceProtection,

        [EdiFieldValue("669")]
        ClaimIncurred,

        [EdiFieldValue("670")]
        BookEntryDelivery,

        [EdiFieldValue("671")]
        RateAdjustment,

        [EdiFieldValue("672")]
        NextInstallmentDueDate,

        [EdiFieldValue("673")]
        DaylightOverdraftTime,

        [EdiFieldValue("674")]
        PresentmentDate,

        [EdiFieldValue("675")]
        NegotiatedExtensionDate,

        [EdiFieldValue("681")]
        Remittance,

        [EdiFieldValue("682")]
        SecurityRateAdjustment,

        [EdiFieldValue("683")]
        FilingPeriod,

        [EdiFieldValue("684")]
        ReviewPeriodEnd,

        [EdiFieldValue("685")]
        RequestedSettlement,

        [EdiFieldValue("686")]
        LastScreening,

        [EdiFieldValue("687")]
        Confinement,

        [EdiFieldValue("688")]
        Arrested,

        [EdiFieldValue("689")]
        Convicted,

        [EdiFieldValue("690")]
        Interviewed,

        [EdiFieldValue("691")]
        LastVisit,

        [EdiFieldValue("692")]
        Recovery,

        [EdiFieldValue("693")]
        TimeInUS,

        [EdiFieldValue("694")]
        FuturePeriod,

        [EdiFieldValue("695")]
        PreviousPeriod,

        [EdiFieldValue("696")]
        InterestPaidTo,

        [EdiFieldValue("697")]
        DateOfSeizure,

        [EdiFieldValue("699")]
        SetOff,

        [EdiFieldValue("700")]
        OverrideDateforSettlement,

        [EdiFieldValue("701")]
        SettlementDate_FromInterlineSettlementSystem_ISS_only,

        [EdiFieldValue("702")]
        SendingRoadTimeStamp,

        [EdiFieldValue("703")]
        RetransmissionTimeStamp,

        [EdiFieldValue("704")]
        DeliveryAppointmentDateandTime,

        [EdiFieldValue("705")]
        InterestPaidThrough,

        [EdiFieldValue("706")]
        DateMaterialUsageSuspended,

        [EdiFieldValue("707")]
        LastPaymentMade,

        [EdiFieldValue("708")]
        PastDue,

        [EdiFieldValue("709")]
        AnalysisMonthEnding,

        [EdiFieldValue("710")]
        DateOfSpecification,

        [EdiFieldValue("711")]
        DateOfStandard,

        [EdiFieldValue("712")]
        ReturntoWorkPartTime,

        [EdiFieldValue("713")]
        Paid_throughDateforSalaryContinuation,

        [EdiFieldValue("714")]
        Paid_throughDateforVacationPay,

        [EdiFieldValue("715")]
        Paid_throughDateforAccruedSickPay,

        [EdiFieldValue("716")]
        AppraisalOrdered,

        [EdiFieldValue("717")]
        DateOfOperation,

        [EdiFieldValue("718")]
        BestTimetoCall,

        [EdiFieldValue("719")]
        VerbalReportNeeded,

        [EdiFieldValue("720")]
        EstimatedEscrowClosing,

        [EdiFieldValue("721")]
        PermitYear,

        [EdiFieldValue("722")]
        RemodelingCompleted,

        [EdiFieldValue("723")]
        CurrentMonthEnding,

        [EdiFieldValue("724")]
        PreviousMonthEnding,

        [EdiFieldValue("725")]
        CycletoDate,

        [EdiFieldValue("726")]
        YeartoDate,

        [EdiFieldValue("727")]
        OnHold,

        [EdiFieldValue("728")]
        OffHold,

        [EdiFieldValue("729")]
        FacsimileDueBy,

        [EdiFieldValue("730")]
        ReportingCycleDate,

        [EdiFieldValue("731")]
        LastPaidInstallmentDate,

        [EdiFieldValue("732")]
        ClaimsMade,

        [EdiFieldValue("733")]
        DateOfLastPaymentReceived,

        [EdiFieldValue("734")]
        CurtailmentDate,

        [EdiFieldValue("736")]
        PoolSettlement,

        [EdiFieldValue("737")]
        NextInterestChangeDate,

        [EdiFieldValue("738")]
        MostRecentHemoglobinorHematocritorBoth,

        [EdiFieldValue("739")]
        MostRecentSerumCreatine,

        [EdiFieldValue("740")]
        Closed,

        [EdiFieldValue("741")]
        Therapy,

        [EdiFieldValue("742")]
        Implantation,

        [EdiFieldValue("743")]
        Explantation,

        [EdiFieldValue("744")]
        DateBecameAware,

        [EdiFieldValue("745")]
        FirstMarketed,

        [EdiFieldValue("746")]
        LastMarketed,

        [EdiFieldValue("750")]
        ExpectedProblemResolution,

        [EdiFieldValue("751")]
        AlternateProblemResolution,

        [EdiFieldValue("752")]
        FeeCapitalization,

        [EdiFieldValue("753")]
        InterestCapitalization,

        [EdiFieldValue("754")]
        NextPaymentDue,

        [EdiFieldValue("755")]
        ConversiontoRepayment,

        [EdiFieldValue("756")]
        EndOfGrace,

        [EdiFieldValue("757")]
        SchoolRefund,

        [EdiFieldValue("758")]
        SimpleInterestDue,

        [EdiFieldValue("760")]
        Printed,

        [EdiFieldValue("770")]
        BackonMarket,

        [EdiFieldValue("771")]
        Status,

        [EdiFieldValue("773")]
        Off_Market,

        [EdiFieldValue("774")]
        Tour,

        [EdiFieldValue("776")]
        ListingReceived,

        [EdiFieldValue("778")]
        AnticipatedClosing,

        [EdiFieldValue("779")]
        LastPublication,

        [EdiFieldValue("780")]
        SoldBookPublication,

        [EdiFieldValue("781")]
        Occupancy,

        [EdiFieldValue("782")]
        Contingency,

        [EdiFieldValue("783")]
        PercolationTest,

        [EdiFieldValue("784")]
        SepticApproval,

        [EdiFieldValue("785")]
        TitleTransfer,

        [EdiFieldValue("786")]
        OpenHouse,

        [EdiFieldValue("789")]
        Homestead,

        [EdiFieldValue("800")]
        MidpointOfPerformance,

        [EdiFieldValue("801")]
        AcquisitionDate,

        [EdiFieldValue("802")]
        DateOfAction,

        [EdiFieldValue("803")]
        PaidinFull,

        [EdiFieldValue("804")]
        Refinance,

        [EdiFieldValue("805")]
        VoluntaryTermination,

        [EdiFieldValue("806")]
        CustomerOrder,

        [EdiFieldValue("807")]
        Stored,

        [EdiFieldValue("808")]
        Selected,

        [EdiFieldValue("809")]
        Posted,

        [EdiFieldValue("810")]
        DocumentReceived,

        [EdiFieldValue("811")]
        Rebuilt,

        [EdiFieldValue("812")]
        Marriage,

        [EdiFieldValue("813")]
        CustomsEntryDate,

        [EdiFieldValue("814")]
        PaymentDueDate,

        [EdiFieldValue("815")]
        MaturityDate,

        [EdiFieldValue("816")]
        TradeDate,

        [EdiFieldValue("817")]
        GallonsPerMinute_GPM_TestPerformed,

        [EdiFieldValue("818")]
        BritishThermalUnit_BTU_TestPerformed,

        [EdiFieldValue("820")]
        RealEstateTaxYear,

        [EdiFieldValue("821")]
        FinalReconciliationValueEstimateAsOf,

        [EdiFieldValue("822")]
        Map,

        [EdiFieldValue("823")]
        Opinion,

        [EdiFieldValue("824")]
        Version,

        [EdiFieldValue("825")]
        OriginalDueDate,

        [EdiFieldValue("826")]
        IncumbencyPeriod,

        [EdiFieldValue("827")]
        AudienceDeficiencyPeriod,

        [EdiFieldValue("828")]
        AiredDate,

        [EdiFieldValue("830")]
        Schedule,

        [EdiFieldValue("831")]
        PaidThroughDateforMinimumPayment,

        [EdiFieldValue("832")]
        PaidThroughDateforTotalPayment,

        [EdiFieldValue("840")]
        Election,

        [EdiFieldValue("841")]
        EngineeringDataList,

        [EdiFieldValue("842")]
        LastProduction,

        [EdiFieldValue("843")]
        NotBefore,

        [EdiFieldValue("844")]
        NotAfter,

        [EdiFieldValue("845")]
        InitialClaim,

        [EdiFieldValue("846")]
        BenefitsPaid,

        [EdiFieldValue("847")]
        WagesEarned,

        [EdiFieldValue("848")]
        AdjustedStart,

        [EdiFieldValue("849")]
        AdjustedEnd,

        [EdiFieldValue("850")]
        RevisedAdjustedStart,

        [EdiFieldValue("851")]
        RevisedAdjustedEnd,

        [EdiFieldValue("853")]
        FieldTest,

        [EdiFieldValue("854")]
        MortgageNoteDate,

        [EdiFieldValue("855")]
        AlternativeDueDate,

        [EdiFieldValue("856")]
        FirstPaymentChange,

        [EdiFieldValue("857")]
        FirstRateAdjustment,

        [EdiFieldValue("858")]
        AlternateBasePeriod,

        [EdiFieldValue("859")]
        PriorNotice,

        [EdiFieldValue("860")]
        AppointmentEffective,

        [EdiFieldValue("861")]
        AppointmentExpiration,

        [EdiFieldValue("862")]
        CompanyTermination,

        [EdiFieldValue("863")]
        ContinuingEducationRequirement,

        [EdiFieldValue("864")]
        DistributorEffective,

        [EdiFieldValue("865")]
        DistributorTermination,

        [EdiFieldValue("866")]
        Examination,

        [EdiFieldValue("867")]
        IncorporationDissolution,

        [EdiFieldValue("868")]
        LastFollow_up,

        [EdiFieldValue("869")]
        LicenseEffective,

        [EdiFieldValue("870")]
        LicenseExpiration,

        [EdiFieldValue("871")]
        LicenseRenewal,

        [EdiFieldValue("872")]
        LicenseRequested,

        [EdiFieldValue("873")]
        Mailed,

        [EdiFieldValue("874")]
        PaperworkMailed,

        [EdiFieldValue("875")]
        PreviousEmployment,

        [EdiFieldValue("876")]
        PreviousEmploymentEnd,

        [EdiFieldValue("877")]
        PreviousEmploymentStart,

        [EdiFieldValue("878")]
        PreviousResidence,

        [EdiFieldValue("879")]
        PreviousResidenceEnd,

        [EdiFieldValue("880")]
        PreviousResidenceStart,

        [EdiFieldValue("881")]
        Request,

        [EdiFieldValue("882")]
        ResidentLicenseEffective,

        [EdiFieldValue("883")]
        ResidentLicenseExpiration,

        [EdiFieldValue("884")]
        StateTermination,

        [EdiFieldValue("885")]
        TexasLineTermination,

        [EdiFieldValue("900")]
        Acceleration,

        [EdiFieldValue("901")]
        AdjustedContestability,

        [EdiFieldValue("903")]
        ApplicationEntry,

        [EdiFieldValue("904")]
        ApprovalOffer,

        [EdiFieldValue("905")]
        AutomaticPremiumLoan,

        [EdiFieldValue("906")]
        Collection,

        [EdiFieldValue("907")]
        ConfinementEnd,

        [EdiFieldValue("908")]
        ConfinementStart,

        [EdiFieldValue("909")]
        Contestability,

        [EdiFieldValue("910")]
        FlatExtraEnd,

        [EdiFieldValue("911")]
        LastActivity,

        [EdiFieldValue("912")]
        LastChange,

        [EdiFieldValue("913")]
        LastEpisode,

        [EdiFieldValue("914")]
        LastMeal,

        [EdiFieldValue("915")]
        Loan,

        [EdiFieldValue("916")]
        ApplicationStatus,

        [EdiFieldValue("917")]
        Maturity,

        [EdiFieldValue("918")]
        MedicalInformationSignature,

        [EdiFieldValue("919")]
        MedicalInformationSystem,

        [EdiFieldValue("920")]
        Note,

        [EdiFieldValue("921")]
        OfferExpiration,

        [EdiFieldValue("922")]
        OriginalReceipt,

        [EdiFieldValue("923")]
        Placement,

        [EdiFieldValue("924")]
        PlacementPeriodExpiration,

        [EdiFieldValue("925")]
        Processing,

        [EdiFieldValue("926")]
        Recapture,

        [EdiFieldValue("927")]
        Re_entry,

        [EdiFieldValue("928")]
        Reissue,

        [EdiFieldValue("929")]
        Reinstatement_929,

        [EdiFieldValue("930")]
        Requalification,

        [EdiFieldValue("931")]
        ReinsuranceEffective,

        [EdiFieldValue("932")]
        ReservationOfFacility,

        [EdiFieldValue("933")]
        SettlementStatus,

        [EdiFieldValue("934")]
        TableRatingEnd,

        [EdiFieldValue("935")]
        TerminationOfFacility,

        [EdiFieldValue("936")]
        Treatment,

        [EdiFieldValue("937")]
        DepartmentOfLaborWageDeterminationDate,

        [EdiFieldValue("938")]
        Order,

        [EdiFieldValue("939")]
        Resolved,

        [EdiFieldValue("940")]
        ExecutionDate,

        [EdiFieldValue("941")]
        CapitationPeriodStart,

        [EdiFieldValue("942")]
        CapitationPeriodEnd,

        [EdiFieldValue("943")]
        LastDateforaGovernmentAgencyToFileAClaim,

        [EdiFieldValue("944")]
        AdjustmentPeriod,

        [EdiFieldValue("945")]
        Activity,

        [EdiFieldValue("946")]
        MailBy,

        [EdiFieldValue("947")]
        Preparation,

        [EdiFieldValue("948")]
        PaymentInitiated,

        [EdiFieldValue("949")]
        PaymentEffective,

        [EdiFieldValue("950")]
        Application,

        [EdiFieldValue("951")]
        Reclassification,

        [EdiFieldValue("952")]
        Reclassification_ExitDate,

        [EdiFieldValue("953")]
        Post_Reclassification,

        [EdiFieldValue("954")]
        Post_Reclassification_FirstReportCard,

        [EdiFieldValue("955")]
        Post_Reclassification_FirstSemi_annual,

        [EdiFieldValue("956")]
        Post_Reclassification_SecondSemi_annual,

        [EdiFieldValue("957")]
        Post_Reclassification_EndOfSecondYear,

        [EdiFieldValue("960")]
        AdjustedDeathBenefit,

        [EdiFieldValue("961")]
        Anniversary,

        [EdiFieldValue("962")]
        Annuitization,

        [EdiFieldValue("963")]
        AnnuityCommencementDate,

        [EdiFieldValue("964")]
        Bill,

        [EdiFieldValue("965")]
        CalendarAnniversary,

        [EdiFieldValue("966")]
        ContractMailed,

        [EdiFieldValue("967")]
        EarlyWithdrawal,

        [EdiFieldValue("968")]
        FiscalAnniversary,

        [EdiFieldValue("969")]
        Income,

        [EdiFieldValue("970")]
        InitialPremium,

        [EdiFieldValue("971")]
        InitialPremiumEffective,

        [EdiFieldValue("972")]
        LastPremiumEffective,

        [EdiFieldValue("973")]
        MinimumRequiredDistribution,

        [EdiFieldValue("974")]
        NextAnniversary,

        [EdiFieldValue("975")]
        Notice,

        [EdiFieldValue("976")]
        NotificationOfDeath,

        [EdiFieldValue("977")]
        PartialAnnuitization,

        [EdiFieldValue("978")]
        PlanAnniversary,

        [EdiFieldValue("979")]
        PolicySurrender,

        [EdiFieldValue("980")]
        PriorContractAnniversary,

        [EdiFieldValue("981")]
        PriorContractIssue,

        [EdiFieldValue("982")]
        SignatureReceived,

        [EdiFieldValue("983")]
        Tax,

        [EdiFieldValue("984")]
        BenefitPeriod,

        [EdiFieldValue("985")]
        MonthtoDate,

        [EdiFieldValue("986")]
        SemiannualEnding,

        [EdiFieldValue("987")]
        Surrender,

        [EdiFieldValue("988")]
        PlanOfTreatmentPeriod,

        [EdiFieldValue("989")]
        PriorHospitalizationDates_RelatedtoCurrentServices,

        [EdiFieldValue("992")]
        DateRequested,

        [EdiFieldValue("993")]
        RequestforQuotation,

        [EdiFieldValue("994")]
        Quote,

        [EdiFieldValue("995")]
        RecordedDate,

        [EdiFieldValue("996")]
        RequiredDelivery,

        [EdiFieldValue("997")]
        QuotetobeReceivedBy,

        [EdiFieldValue("998")]
        ContinuationOfPayStartDate,

        [EdiFieldValue("999")]
        DocumentDate,

        [EdiFieldValue("AA1")]
        EstimatedPointOfArrival,

        [EdiFieldValue("AA2")]
        EstimatedPointOfDischarge,

        [EdiFieldValue("AA3")]
        CancelAfter_ExCountry,

        [EdiFieldValue("AA4")]
        CancelAfter_ExFactory,

        [EdiFieldValue("AA5")]
        DoNotShipBefore_ExCountry,

        [EdiFieldValue("AA6")]
        DoNotShipBefore_ExFactory,

        [EdiFieldValue("AA7")]
        FinalScheduledPayment,

        [EdiFieldValue("AA8")]
        ActualDischarge,

        [EdiFieldValue("AA9")]
        AddressPeriod,

        [EdiFieldValue("AAA")]
        ArrivalinCountry,

        [EdiFieldValue("AAB")]
        Citation,

        [EdiFieldValue("AAD")]
        Crime,

        [EdiFieldValue("AAE")]
        Discharge_Planned,

        [EdiFieldValue("AAF")]
        Draft,

        [EdiFieldValue("AAG")]
        DueDate,

        [EdiFieldValue("AAH")]
        Event,

        [EdiFieldValue("AAI")]
        FirstInvolvement,

        [EdiFieldValue("AAJ")]
        GuaranteePeriod,

        [EdiFieldValue("AAK")]
        IncomeIncreasePeriod,

        [EdiFieldValue("AAL")]
        InstallmentDate,

        [EdiFieldValue("AAM")]
        LastCivilianFlight,

        [EdiFieldValue("AAN")]
        LastFlight,

        [EdiFieldValue("AAO")]
        LastInsuranceMedical,

        [EdiFieldValue("AAP")]
        LastMilitaryFlight,

        [EdiFieldValue("AAQ")]
        LastPhysical,

        [EdiFieldValue("AAR")]
        License,

        [EdiFieldValue("AAS")]
        MedicalCertificate,

        [EdiFieldValue("AAT")]
        Medication,

        [EdiFieldValue("AAU")]
        NetWorthDate,

        [EdiFieldValue("AAV")]
        NextActivity,

        [EdiFieldValue("AAW")]
        OwnershipChange,

        [EdiFieldValue("AAX")]
        OwnershipPeriod,

        [EdiFieldValue("AAY")]
        RateDate,

        [EdiFieldValue("AAZ")]
        RequestedContract,

        [EdiFieldValue("AB1")]
        RequestedOffer,

        [EdiFieldValue("AB2")]
        SalesPeriod,

        [EdiFieldValue("AB3")]
        TaxYear,

        [EdiFieldValue("AB4")]
        TimePeriod,

        [EdiFieldValue("AB5")]
        Travel,

        [EdiFieldValue("AB6")]
        TreatmentEnd,

        [EdiFieldValue("AB7")]
        TreatmentStart,

        [EdiFieldValue("AB8")]
        Trust,

        [EdiFieldValue("AB9")]
        WorstTimetoCall,

        [EdiFieldValue("ABA")]
        Registration,

        [EdiFieldValue("ABB")]
        Revoked,

        [EdiFieldValue("ABC")]
        EstimatedDateOfBirth,

        [EdiFieldValue("ABD")]
        LastAnnualReport,

        [EdiFieldValue("ABE")]
        LegalActionStarted,

        [EdiFieldValue("ABG")]
        PaymentPeriod,

        [EdiFieldValue("ABH")]
        ProfitPeriod,

        [EdiFieldValue("ABI")]
        Registered,

        [EdiFieldValue("ABK")]
        Consolidated,

        [EdiFieldValue("ABL")]
        BoardOfDirectorsNotAuthorizedAsOf,

        [EdiFieldValue("ABM")]
        BoardOfDirectorsIncompleteAsOf,

        [EdiFieldValue("ABN")]
        ManagerNotRegisteredAsOf,

        [EdiFieldValue("ABO")]
        CitizenshipChange,

        [EdiFieldValue("ABP")]
        Participation,

        [EdiFieldValue("ABQ")]
        Capitalization,

        [EdiFieldValue("ABR")]
        RegistrationOfBoardOfDirectors,

        [EdiFieldValue("ABS")]
        CeasedOperations,

        [EdiFieldValue("ABT")]
        Satisfied,

        [EdiFieldValue("ABU")]
        TermsMet,

        [EdiFieldValue("ABV")]
        AssetDocumentationExpiration,

        [EdiFieldValue("ABW")]
        CreditDocumentationExpiration,

        [EdiFieldValue("ABX")]
        IncomeDocumentationExpiration,

        [EdiFieldValue("ABY")]
        ProductHeldUntil,

        [EdiFieldValue("ACA")]
        ImmigrationDate,

        [EdiFieldValue("ACB")]
        EstimatedImmigrationDate,

        [EdiFieldValue("ACK")]
        Acknowledgment,

        [EdiFieldValue("ADB")]
        BusinessControlChange,

        [EdiFieldValue("ADC")]
        CourtRegistration,

        [EdiFieldValue("ADD")]
        AnnualReportDue,

        [EdiFieldValue("ADL")]
        AssetandLiabilitySchedule,

        [EdiFieldValue("ADM")]
        AnnualReportMailed,

        [EdiFieldValue("ADR")]
        AnnualReportFiled,

        [EdiFieldValue("ARD")]
        AnnualReportDelinquency,

        [EdiFieldValue("CAD")]
        ChangedAccountingDate,

        [EdiFieldValue("CCR")]
        CustomsCargoRelease,

        [EdiFieldValue("CDT")]
        MaintenanceComment,

        [EdiFieldValue("CEA")]
        Formation,

        [EdiFieldValue("CEB")]
        Continuance,

        [EdiFieldValue("CEC")]
        Merger,

        [EdiFieldValue("CED")]
        YearDue,

        [EdiFieldValue("CEE")]
        NextAnnualMeeting,

        [EdiFieldValue("CEF")]
        EndOfLastFiscalYear,

        [EdiFieldValue("CEH")]
        YearBeginning,

        [EdiFieldValue("CEJ")]
        StartedDoingBusiness,

        [EdiFieldValue("CEK")]
        SwornandSubscribed,

        [EdiFieldValue("CEL")]
        CalendarYear,

        [EdiFieldValue("CEM")]
        Asset,

        [EdiFieldValue("CEN")]
        Inactivity,

        [EdiFieldValue("CEO")]
        HighCapitalYear,

        [EdiFieldValue("CLO")]
        ClosingDateOfFirstBalanceSheet,

        [EdiFieldValue("CLU")]
        ClosedUntil,

        [EdiFieldValue("COM")]
        Compliance,

        [EdiFieldValue("CON")]
        ConvertedintoHoldingCompany,

        [EdiFieldValue("CUR")]
        CurrentList,

        [EdiFieldValue("DDO")]
        Declaration,

        [EdiFieldValue("DEE")]
        DeedNotAvailable,

        [EdiFieldValue("DET")]
        DetrimentalInformationReceived,

        [EdiFieldValue("DFF")]
        Deferral,

        [EdiFieldValue("DFS")]
        DepartureFromSpecification,

        [EdiFieldValue("DIS")]
        Disposition,

        [EdiFieldValue("DOI")]
        DeliveryOrderIssued,

        [EdiFieldValue("DSP")]
        Disposal,

        [EdiFieldValue("ECD")]
        EstimatedConstructionDate,

        [EdiFieldValue("ECF")]
        EstimatedCompletion_FirstPriorMonth,

        [EdiFieldValue("ECS")]
        EstimatedCompletion_SecondPriorMonth,

        [EdiFieldValue("ECT")]
        EstimatedCompletion_ThirdPriorMonth,

        [EdiFieldValue("EPP")]
        EstimatePreparation,

        [EdiFieldValue("ESC")]
        EstimateComment,

        [EdiFieldValue("ESF")]
        EstimatedStart_FirstPriorMonth,

        [EdiFieldValue("ESS")]
        EstimatedStart_SecondPriorMonth,

        [EdiFieldValue("EST")]
        EstimatedStart_ThirdPriorMonth,

        [EdiFieldValue("ETP")]
        EarliestFilingPeriod,

        [EdiFieldValue("EXO")]
        Exposure,

        [EdiFieldValue("EXP")]
        Export,

        [EdiFieldValue("FFI")]
        FinancialInformation,

        [EdiFieldValue("GRD")]
        Graduated,

        [EdiFieldValue("ICF")]
        ConvertedtoElectronicDate,

        [EdiFieldValue("IDG")]
        InsolvencyDischargeGranted,

        [EdiFieldValue("III")]
        Incorporation,

        [EdiFieldValue("IMP")]
        Import,

        [EdiFieldValue("INC")]
        Incident,

        [EdiFieldValue("INT")]
        InactiveUntil,

        [EdiFieldValue("KEV")]
        KeyEventFiscalYear,

        [EdiFieldValue("KEW")]
        KeyEventCalendarYear,

        [EdiFieldValue("LAS")]
        LastCheckforBalanceSheetUpdate,

        [EdiFieldValue("LCC")]
        LastCapitalChange,

        [EdiFieldValue("LEA")]
        LetterOfAgreement,

        [EdiFieldValue("LEL")]
        LetterOfLiability,

        [EdiFieldValue("LIQ")]
        Liquidation,

        [EdiFieldValue("LLP")]
        LowPeriod,

        [EdiFieldValue("LOG")]
        EquipmentLogEntry,

        [EdiFieldValue("LPC")]
        ListPriceChange,

        [EdiFieldValue("LSC")]
        LegalStructureChange,

        [EdiFieldValue("LTP")]
        LatestFilingPeriod,

        [EdiFieldValue("MRR")]
        MeterReading,

        [EdiFieldValue("MSD")]
        LatestMaterialSafetyDataSheetDate,

        [EdiFieldValue("NAM")]
        PresentName,

        [EdiFieldValue("NFD")]
        NegotiatedFinish,

        [EdiFieldValue("NRG")]
        NotRegistered,

        [EdiFieldValue("NSD")]
        NegotiatedStart,

        [EdiFieldValue("ORG")]
        OriginalList,

        [EdiFieldValue("PBC")]
        PresentControl,

        [EdiFieldValue("PDV")]
        PrivilegeDetailsVerification,

        [EdiFieldValue("PLS")]
        PresentLegalStructure,

        [EdiFieldValue("PPP")]
        PeakPeriod,

        [EdiFieldValue("PRD")]
        PreviouslyReportedDateOfBirth,

        [EdiFieldValue("PRR")]
        PresentedtoReceivers,

        [EdiFieldValue("PTD")]
        PaidToDate,

        [EdiFieldValue("RAP")]
        ReceiverAppointed,

        [EdiFieldValue("RES")]
        Resigned,

        [EdiFieldValue("RFD")]
        RequestedFinish,

        [EdiFieldValue("RFF")]
        RecoveryFinish,

        [EdiFieldValue("RFO")]
        ReferredFrom,

        [EdiFieldValue("RNT")]
        RentSurvey,

        [EdiFieldValue("RRM")]
        ReceivedintheMail,

        [EdiFieldValue("RRT")]
        Revocation,

        [EdiFieldValue("RSD")]
        RequestedStart,

        [EdiFieldValue("RSS")]
        RecoveryStart,

        [EdiFieldValue("RTO")]
        ReferredTo,

        [EdiFieldValue("SCV")]
        SocialSecurityClaimsVerification,

        [EdiFieldValue("SDD")]
        SoleDirectorshipDate,

        [EdiFieldValue("STN")]
        Transition,

        [EdiFieldValue("TSR")]
        TradeStyleRegistered,

        [EdiFieldValue("TSS")]
        TrialStarted,

        [EdiFieldValue("TST")]
        TrialSet,

        [EdiFieldValue("VAT")]
        ValueAddedTax_VAT_ClaimsVerification,

        [EdiFieldValue("VLU")]
        ValidUntil,

        [EdiFieldValue("W01")]
        SampleCollected,

        [EdiFieldValue("W02")]
        StatusChange,

        [EdiFieldValue("W03")]
        ConstructionStart,

        [EdiFieldValue("W05")]
        Recompletion,

        [EdiFieldValue("W06")]
        LastLogged,

        [EdiFieldValue("W07")]
        WellLogRun,

        [EdiFieldValue("W08")]
        SurfaceCasingAuthorityApproval,

        [EdiFieldValue("W09")]
        ReachedTotalDepth,

        [EdiFieldValue("W10")]
        SpacingOrderUnitAssigned,

        [EdiFieldValue("W11")]
        RigArrival,

        [EdiFieldValue("W12")]
        LocationExceptionOrderNumberAssigned,

        [EdiFieldValue("W13")]
        SidetrackedWellbore,

        [EdiFieldValue("WAY")]
        Waybill,

        [EdiFieldValue("YXX")]
        ProgrammedFiscalYear,

        [EdiFieldValue("YXY")]
        ProgrammedCalendarYear,

        [EdiFieldValue("ZZZ")]
        MutuallyDefined,

        [EdiFieldValue("340")]
        ConsolidatedOmnibusBudgetReconciliationAct,

        [EdiFieldValue("341")]
        ConsolidatedOmnibusBudgetReconciliationAct_COBRA,
    }
}
