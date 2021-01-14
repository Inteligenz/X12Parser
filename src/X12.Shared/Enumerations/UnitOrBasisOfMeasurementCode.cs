﻿namespace X12.Shared.Enumerations
{
    using X12.Shared.Attributes;

    public enum UnitOrBasisOfMeasurementCode
    {
        [EdiFieldValue("01")]
        ActualPounds,

        [EdiFieldValue("02")]
        StatuteMile,

        [EdiFieldValue("03")]
        Seconds,

        [EdiFieldValue("04")]
        SmallSpray,

        [EdiFieldValue("05")]
        Lifts,

        [EdiFieldValue("06")]
        Digits,

        [EdiFieldValue("07")]
        Strand,

        [EdiFieldValue("08")]
        HeatLots,

        [EdiFieldValue("09")]
        Tire,

        [EdiFieldValue("10")]
        Group,

        [EdiFieldValue("11")]
        Outfit,

        [EdiFieldValue("12")]
        Packet,

        [EdiFieldValue("13")]
        Ration,

        [EdiFieldValue("14")]
        Shot,

        [EdiFieldValue("15")]
        Stick,

        [EdiFieldValue("16")]
        _115KilogramDrum,

        [EdiFieldValue("17")]
        _100PoundDrum,

        [EdiFieldValue("18")]
        _55GallonDrum,

        [EdiFieldValue("19")]
        TankTruck,

        [EdiFieldValue("1A")]
        CarMile,

        [EdiFieldValue("1B")]
        CarCount,

        [EdiFieldValue("1C")]
        LocomotiveCount,

        [EdiFieldValue("1D")]
        CabooseCount,

        [EdiFieldValue("1E")]
        EmptyCar,

        [EdiFieldValue("1F")]
        TrainMile,

        [EdiFieldValue("1G")]
        FuelUsage_Gallons,

        [EdiFieldValue("1H")]
        CabooseMile,

        [EdiFieldValue("1I")]
        FixedRate,

        [EdiFieldValue("1J")]
        TonMiles,

        [EdiFieldValue("1K")]
        LocomotiveMile,

        [EdiFieldValue("1L")]
        TotalCarCount,

        [EdiFieldValue("1M")]
        TotalCarMile,

        [EdiFieldValue("1N")]
        Count,

        [EdiFieldValue("1O")]
        Season,

        [EdiFieldValue("1P")]
        TankCar,

        [EdiFieldValue("1Q")]
        Frames,

        [EdiFieldValue("1R")]
        Transactions,

        [EdiFieldValue("1X")]
        QuarterMile,

        [EdiFieldValue("20")]
        _20FootContainer,

        [EdiFieldValue("21")]
        _40FootContainer,

        [EdiFieldValue("22")]
        DeciliterperGram,

        [EdiFieldValue("23")]
        GramsPerCubicCentimeter,

        [EdiFieldValue("24")]
        TheoreticalPounds,

        [EdiFieldValue("25")]
        GramsPerSquareCentimeter,

        [EdiFieldValue("26")]
        ActualTons,

        [EdiFieldValue("27")]
        TheoreticalTons,

        [EdiFieldValue("28")]
        KilogramsPerSquareMeter,

        [EdiFieldValue("29")]
        PoundsPer1000SquareFeet,

        [EdiFieldValue("2A")]
        RadiansPerSecond,

        [EdiFieldValue("2B")]
        RadiansPerSecondSquared,

        [EdiFieldValue("2C")]
        Roentgen,

        [EdiFieldValue("2F")]
        VoltsPerMeter,

        [EdiFieldValue("2G")]
        Volts_AlternatingCurrent,

        [EdiFieldValue("2H")]
        Volts_DirectCurrent,

        [EdiFieldValue("2I")]
        BritishThermalUnitsPerHour,

        [EdiFieldValue("2J")]
        CubicCentimetersPerSecond,

        [EdiFieldValue("2K")]
        CubicFeetPerHour,

        [EdiFieldValue("2L")]
        CubicFeetPerMinute,

        [EdiFieldValue("2M")]
        CentimetersPerSecond,

        [EdiFieldValue("2N")]
        Decibels,

        [EdiFieldValue("2P")]
        Kilobyte,

        [EdiFieldValue("2Q")]
        Kilobecquerel,

        [EdiFieldValue("2R")]
        Kilocurie,

        [EdiFieldValue("2U")]
        Megagram,

        [EdiFieldValue("2V")]
        MegagramsPerHour,

        [EdiFieldValue("2W")]
        Bin,

        [EdiFieldValue("2X")]
        MetersPerMinute,

        [EdiFieldValue("2Y")]
        Milliroentgen,

        [EdiFieldValue("2Z")]
        Millivolts,

        [EdiFieldValue("30")]
        HorsepowerDaysPerAirDryMetricTons,

        [EdiFieldValue("31")]
        Catchweight,

        [EdiFieldValue("32")]
        KilogramsPerAirDryMetricTons,

        [EdiFieldValue("33")]
        KilopascalSquareMetersPerGram,

        [EdiFieldValue("34")]
        KilopascalsPerMillimeter,

        [EdiFieldValue("35")]
        MillilitersPerSquareCentimeterSecond,

        [EdiFieldValue("36")]
        CubicFeetPerMinutePerSquareFoot,

        [EdiFieldValue("37")]
        OuncesPerSquareFoot,

        [EdiFieldValue("38")]
        OuncesPerSquareFootPerOneHundredthOfAnInch,

        [EdiFieldValue("39")]
        BasisPoints,

        [EdiFieldValue("3B")]
        Megajoule,

        [EdiFieldValue("3C")]
        Manmonth,

        [EdiFieldValue("3E")]
        PoundsPerPoundofProduct,

        [EdiFieldValue("3F")]
        KilogramsPerLiterOfProduct,

        [EdiFieldValue("3G")]
        PoundsPerPieceOfProduct,

        [EdiFieldValue("3H")]
        KilogramsPerKilogramOfProduct,

        [EdiFieldValue("3I")]
        KilogramsPerPieceOfProduct,

        [EdiFieldValue("40")]
        MilliliterPerSecond,

        [EdiFieldValue("41")]
        MilliliterPerMinute,

        [EdiFieldValue("43")]
        SuperBulkBag,

        [EdiFieldValue("44")]
        _500KilogramBulkBag,

        [EdiFieldValue("45")]
        _300KilogramBulkBag,

        [EdiFieldValue("46")]
        _25KilogramBulkBag,

        [EdiFieldValue("47")]
        _50PoundBag,

        [EdiFieldValue("48")]
        BulkCarLoad,

        [EdiFieldValue("4A")]
        Bobbin,

        [EdiFieldValue("4B")]
        Cap,

        [EdiFieldValue("4C")]
        Centistokes,

        [EdiFieldValue("4D")]
        Curie,

        [EdiFieldValue("4E")]
        _20Pack,

        [EdiFieldValue("4F")]
        _100Pack,

        [EdiFieldValue("4G")]
        Microliter,

        [EdiFieldValue("4H")]
        Micrometer,

        [EdiFieldValue("4I")]
        MetersPerSecond,

        [EdiFieldValue("4J")]
        MetersPerSecondPerSecond,

        [EdiFieldValue("4K")]
        Milliamperes,

        [EdiFieldValue("4L")]
        Megabyte,

        [EdiFieldValue("4M")]
        MilligramsPerHour,

        [EdiFieldValue("4N")]
        Megabecquerel,

        [EdiFieldValue("4O")]
        Microfarad,

        [EdiFieldValue("4P")]
        NewtonsPerMeter,

        [EdiFieldValue("4Q")]
        OunceInch,

        [EdiFieldValue("4R")]
        OunceFoot,

        [EdiFieldValue("4S")]
        Pascal,

        [EdiFieldValue("4T")]
        Picofarad,

        [EdiFieldValue("4U")]
        PoundsPerHour,

        [EdiFieldValue("4V")]
        CubicMeterPerHour,

        [EdiFieldValue("4W")]
        TonPerHour,

        [EdiFieldValue("4X")]
        KiloliterPerHour,

        [EdiFieldValue("50")]
        ActualKilograms,

        [EdiFieldValue("51")]
        ActualTonnes,

        [EdiFieldValue("52")]
        Credits,

        [EdiFieldValue("53")]
        TheoreticalKilograms,

        [EdiFieldValue("54")]
        TheoreticalTonnes,

        [EdiFieldValue("56")]
        Sitas,

        [EdiFieldValue("57")]
        Mesh,

        [EdiFieldValue("58")]
        NetKilograms,

        [EdiFieldValue("59")]
        PartsPerMillion,

        [EdiFieldValue("5A")]
        BarrelsPerMinute,

        [EdiFieldValue("5B")]
        Batch,

        [EdiFieldValue("5C")]
        GallonsPerThousand,

        [EdiFieldValue("5E")]
        MMSCFPerDay,

        [EdiFieldValue("5F")]
        PoundsPerThousand,

        [EdiFieldValue("5G")]
        Pump,

        [EdiFieldValue("5H")]
        Stage,

        [EdiFieldValue("5I")]
        StandardCubicFoot,

        [EdiFieldValue("5J")]
        HydraulicHorsePower,

        [EdiFieldValue("5K")]
        CountPerMinute,

        [EdiFieldValue("5P")]
        SeismicLevel,

        [EdiFieldValue("5Q")]
        SeismicLine,

        [EdiFieldValue("60")]
        PercentWeight,

        [EdiFieldValue("61")]
        PartsPerBillion,

        [EdiFieldValue("62")]
        PercentPer1000Hours,

        [EdiFieldValue("63")]
        FailureRateInTime,

        [EdiFieldValue("64")]
        PoundsPerSquareInchGauge,

        [EdiFieldValue("65")]
        Coulomb,

        [EdiFieldValue("66")]
        Oersteds,

        [EdiFieldValue("67")]
        Siemens,

        [EdiFieldValue("68")]
        Ampere,

        [EdiFieldValue("69")]
        TestSpecificScale,

        [EdiFieldValue("70")]
        Volt,

        [EdiFieldValue("71")]
        VoltAmperePerPound,

        [EdiFieldValue("72")]
        WattsPerPound,

        [EdiFieldValue("73")]
        AmpereTurnPerCentimeter,

        [EdiFieldValue("74")]
        MilliPascals,

        [EdiFieldValue("76")]
        Gauss,

        [EdiFieldValue("77")]
        Mil,

        [EdiFieldValue("78")]
        Kilogauss,

        [EdiFieldValue("79")]
        ElectronVolt,

        [EdiFieldValue("80")]
        PoundsPerSquareInchAbsolute,

        [EdiFieldValue("81")]
        Henry,

        [EdiFieldValue("82")]
        Ohm,

        [EdiFieldValue("83")]
        Farad,

        [EdiFieldValue("84")]
        KiloPoundsPerSquareInch_KSI,

        [EdiFieldValue("85")]
        FootPounds,

        [EdiFieldValue("86")]
        Joules,

        [EdiFieldValue("87")]
        PoundsPerCubicFoot,

        [EdiFieldValue("89")]
        Poise,

        [EdiFieldValue("8C")]
        Cord,

        [EdiFieldValue("8D")]
        Duty,

        [EdiFieldValue("8P")]
        Project,

        [EdiFieldValue("8R")]
        Program,

        [EdiFieldValue("8S")]
        Session,

        [EdiFieldValue("8U")]
        SquareKilometer,

        [EdiFieldValue("90")]
        SayboldUniversalSecond,

        [EdiFieldValue("91")]
        Stokes,

        [EdiFieldValue("92")]
        CaloriesPerCubicCentimeter,

        [EdiFieldValue("93")]
        CaloriesPerGram,

        [EdiFieldValue("94")]
        CurlUnits,

        [EdiFieldValue("95")]
        _20kGallonTankcar,

        [EdiFieldValue("96")]
        _10kGallonTankcar,

        [EdiFieldValue("97")]
        _10KilogramDrum,

        [EdiFieldValue("98")]
        _15KilogramDrum,

        [EdiFieldValue("99")]
        Watt,

        [EdiFieldValue("A8")]
        DollarsPerHours,

        [EdiFieldValue("AA")]
        Ball,

        [EdiFieldValue("AB")]
        BulkPack,

        [EdiFieldValue("AC")]
        Acre,

        [EdiFieldValue("AD")]
        Bytes,

        [EdiFieldValue("AE")]
        AmperesPerMeter,

        [EdiFieldValue("AF")]
        Centigram,

        [EdiFieldValue("AG")]
        Angstrom,

        [EdiFieldValue("AH")]
        AdditionalMinutes,

        [EdiFieldValue("AI")]
        AverageMinutesPerCall,

        [EdiFieldValue("AJ")]
        Cop,

        [EdiFieldValue("AK")]
        Fathom,

        [EdiFieldValue("AL")]
        AccessLines,

        [EdiFieldValue("AM")]
        Ampoule,

        [EdiFieldValue("AN")]
        MinutesOrMessages,

        [EdiFieldValue("AO")]
        Ampereturn,

        [EdiFieldValue("AP")]
        AluminumPoundsOnly,

        [EdiFieldValue("AQ")]
        AntihemophilicFactorUnits,

        [EdiFieldValue("AR")]
        Suppository,

        [EdiFieldValue("AS")]
        Assortment,

        [EdiFieldValue("AT")]
        Atmosphere,

        [EdiFieldValue("AU")]
        OcularInsertSystem,

        [EdiFieldValue("AV")]
        Capsule,

        [EdiFieldValue("AW")]
        PowderFilledVials,

        [EdiFieldValue("AX")]
        Twenty,

        [EdiFieldValue("AY")]
        Assembly,

        [EdiFieldValue("AZ")]
        BritishThermalUnitsPerPound,

        [EdiFieldValue("B0")]
        BritishThermalUnitsPerCubicFoot,

        [EdiFieldValue("B1")]
        BarrelsPerDay,

        [EdiFieldValue("B2")]
        Bunks,

        [EdiFieldValue("B3")]
        BattingPound,

        [EdiFieldValue("B4")]
        BarrelImperial,

        [EdiFieldValue("B5")]
        Billet,

        [EdiFieldValue("B6")]
        Bun,

        [EdiFieldValue("B7")]
        Cycles,

        [EdiFieldValue("B8")]
        Board,

        [EdiFieldValue("B9")]
        Batt,

        [EdiFieldValue("BA")]
        Bale,

        [EdiFieldValue("BB")]
        BaseBox,

        [EdiFieldValue("BC")]
        Bucket,

        [EdiFieldValue("BD")]
        Bundle,

        [EdiFieldValue("BE")]
        Beam,

        [EdiFieldValue("BF")]
        BoardFeet,

        [EdiFieldValue("BG")]
        Bag,

        [EdiFieldValue("BH")]
        Brush,

        [EdiFieldValue("BI")]
        Bar,

        [EdiFieldValue("BJ")]
        Band,

        [EdiFieldValue("BK")]
        Book,

        [EdiFieldValue("BL")]
        Block,

        [EdiFieldValue("BM")]
        Bolt,

        [EdiFieldValue("BN")]
        Bulk,

        [EdiFieldValue("BO")]
        Bottle,

        [EdiFieldValue("BP")]
        _100BoardFeet,

        [EdiFieldValue("BQ")]
        Brakehorsepower,

        [EdiFieldValue("BR")]
        Barrel,

        [EdiFieldValue("BS")]
        Basket,

        [EdiFieldValue("BT")]
        Belt,

        [EdiFieldValue("BU")]
        Bushel,

        [EdiFieldValue("BV")]
        BushelDryImperial,

        [EdiFieldValue("BW")]
        BaseWeight,

        [EdiFieldValue("BX")]
        Box,

        [EdiFieldValue("BY")]
        BritishThermalUnit,

        [EdiFieldValue("BZ")]
        MillionBTUs,

        [EdiFieldValue("C0")]
        Calls,

        [EdiFieldValue("C1")]
        CompositeProductPounds_TotalWeight,

        [EdiFieldValue("C2")]
        Carset,

        [EdiFieldValue("C3")]
        Centiliter,

        [EdiFieldValue("C4")]
        Carload,

        [EdiFieldValue("C5")]
        Cost,

        [EdiFieldValue("C6")]
        Cell,

        [EdiFieldValue("C7")]
        Centipoise_CPS,

        [EdiFieldValue("C8")]
        CubicDecimeter,

        [EdiFieldValue("C9")]
        CoilGroup,

        [EdiFieldValue("CA")]
        Case,

        [EdiFieldValue("CB")]
        Carboy,

        [EdiFieldValue("CC")]
        CubicCentimeter,

        [EdiFieldValue("CD")]
        Carat,

        [EdiFieldValue("CE")]
        CentigradeCelsius,

        [EdiFieldValue("CF")]
        CubicFeet,

        [EdiFieldValue("CG")]
        Card,

        [EdiFieldValue("CH")]
        Container,

        [EdiFieldValue("CI")]
        CubicInches,

        [EdiFieldValue("CJ")]
        Cone,

        [EdiFieldValue("CK")]
        Connector,

        [EdiFieldValue("CL")]
        Cylinder,

        [EdiFieldValue("CM")]
        Centimeter,

        [EdiFieldValue("CN")]
        Can,

        [EdiFieldValue("CO")]
        CubicMeters_Net,

        [EdiFieldValue("CP")]
        Crate,

        [EdiFieldValue("CQ")]
        Cartridge,

        [EdiFieldValue("CR")]
        CubicMeter,

        [EdiFieldValue("CS")]
        Cassette,

        [EdiFieldValue("CT")]
        Carton,

        [EdiFieldValue("CU")]
        Cup,

        [EdiFieldValue("CV")]
        Cover,

        [EdiFieldValue("CW")]
        HundredPounds_CWT,

        [EdiFieldValue("CX")]
        Coil,

        [EdiFieldValue("CY")]
        CubicYard,

        [EdiFieldValue("CZ")]
        Combo,

        [EdiFieldValue("D2")]
        Shares,

        [EdiFieldValue("D3")]
        SquareDecimeter,

        [EdiFieldValue("D5")]
        KilogramPerSquareCentimeter,

        [EdiFieldValue("D8")]
        DraizeScore,

        [EdiFieldValue("D9")]
        DynePerSquareCentimeter,

        [EdiFieldValue("DA")]
        Days,

        [EdiFieldValue("DB")]
        DryPounds,

        [EdiFieldValue("DC")]
        Disk,

        [EdiFieldValue("DD")]
        Degree,

        [EdiFieldValue("DE")]
        Deal,

        [EdiFieldValue("DF")]
        Dram,

        [EdiFieldValue("DG")]
        Decigram,

        [EdiFieldValue("DH")]
        Miles,

        [EdiFieldValue("DI")]
        Dispenser,

        [EdiFieldValue("DJ")]
        Decagram,

        [EdiFieldValue("DK")]
        Kilometers,

        [EdiFieldValue("DL")]
        Deciliter,

        [EdiFieldValue("DM")]
        Decimeter,

        [EdiFieldValue("DN")]
        DeciNewtonMeter,

        [EdiFieldValue("DO")]
        DollarsUS,

        [EdiFieldValue("DP")]
        DozenPair,

        [EdiFieldValue("DQ")]
        DataRecords,

        [EdiFieldValue("DR")]
        Drum,

        [EdiFieldValue("DS")]
        Display,

        [EdiFieldValue("DT")]
        DryTon,

        [EdiFieldValue("DU")]
        Dyne,

        [EdiFieldValue("DW")]
        CalendarDays,

        [EdiFieldValue("DX")]
        DynesPerCentimeter,

        [EdiFieldValue("DY")]
        DirectoryBooks,

        [EdiFieldValue("DZ")]
        Dozen,

        [EdiFieldValue("E1")]
        Hectometer,

        [EdiFieldValue("E3")]
        Inches_FractionAverage,

        [EdiFieldValue("E4")]
        Inches_FractionMinimum,

        [EdiFieldValue("E5")]
        Inches_FractionActual,

        [EdiFieldValue("E7")]
        Inches_DecimalAverage,

        [EdiFieldValue("E8")]
        Inches_DecimalActual,

        [EdiFieldValue("E9")]
        English_FeetInches,

        [EdiFieldValue("EA")]
        Each,

        [EdiFieldValue("EB")]
        ElectronicMailBoxes,

        [EdiFieldValue("EC")]
        EachPerMonth,

        [EdiFieldValue("ED")]
        Inches_DecimalNominal,

        [EdiFieldValue("EE")]
        Employees,

        [EdiFieldValue("EF")]
        Inches_FractionNominal,

        [EdiFieldValue("EG")]
        DoubletimeHours,

        [EdiFieldValue("EH")]
        Knots,

        [EdiFieldValue("EJ")]
        Locations,

        [EdiFieldValue("EM")]
        Inches_DecimalMinimum,

        [EdiFieldValue("EP")]
        ElevenPack,

        [EdiFieldValue("EQ")]
        EquivalentGallons,

        [EdiFieldValue("EV")]
        Envelope,

        [EdiFieldValue("EX")]
        Feet_InchesAndFraction,

        [EdiFieldValue("EY")]
        Feet_InchesAndDecimal,

        [EdiFieldValue("EZ")]
        FeetAndDecimal,

        [EdiFieldValue("F1")]
        ThousandCubicFeetPerDay,

        [EdiFieldValue("F2")]
        InternationalUnit,

        [EdiFieldValue("F3")]
        Equivalent,

        [EdiFieldValue("F4")]
        Minim,

        [EdiFieldValue("F5")]
        MOL,

        [EdiFieldValue("F6")]
        PricePerShare,

        [EdiFieldValue("F9")]
        FibersPerCubicCentimeterOfAir,

        [EdiFieldValue("FA")]
        Fahrenheit,

        [EdiFieldValue("FB")]
        Fields,

        [EdiFieldValue("FC")]
        _1000CubicFeet,

        [EdiFieldValue("FD")]
        MillionParticlesPerCubicFoot,

        [EdiFieldValue("FE")]
        TrackFoot,

        [EdiFieldValue("FF")]
        HundredCubicMeters,

        [EdiFieldValue("FG")]
        TransdermalPatch,

        [EdiFieldValue("FH")]
        Micromolar,

        [EdiFieldValue("FJ")]
        SizingFactor,

        [EdiFieldValue("FK")]
        Fibers,

        [EdiFieldValue("FL")]
        FlakeTon,

        [EdiFieldValue("FM")]
        MillionCubicFeet,

        [EdiFieldValue("FO")]
        FluidOunce,

        [EdiFieldValue("FP")]
        PoundsPerSqFt,

        [EdiFieldValue("FR")]
        FeetPerMinute,

        [EdiFieldValue("FS")]
        FeetPerSecond,

        [EdiFieldValue("FT")]
        Foot,

        [EdiFieldValue("FZ")]
        FluidOunce_Imperial,

        [EdiFieldValue("G2")]
        USGallonsPerMinute,

        [EdiFieldValue("G3")]
        ImperialGallonsPerMinute,

        [EdiFieldValue("G4")]
        Gigabecquerel,

        [EdiFieldValue("G5")]
        Gill_Imperial,

        [EdiFieldValue("G7")]
        MicroficheSheet,

        [EdiFieldValue("GA")]
        Gallon,

        [EdiFieldValue("GB")]
        GallonsPerDay,

        [EdiFieldValue("GC")]
        GramsPer100Grams,

        [EdiFieldValue("GD")]
        GrossBarrels,

        [EdiFieldValue("GE")]
        PoundsPerGallon,

        [EdiFieldValue("GF")]
        GramsPer100Centimeters,

        [EdiFieldValue("GG")]
        GreatGross_DozenGross,

        [EdiFieldValue("GH")]
        HalfGallon,

        [EdiFieldValue("GI")]
        ImperialGallons,

        [EdiFieldValue("GJ")]
        GramsPerMilliliter,

        [EdiFieldValue("GK")]
        GramsPerKilogram,

        [EdiFieldValue("GL")]
        GramsPerLiter,

        [EdiFieldValue("GM")]
        GramsPerSqMeter,

        [EdiFieldValue("GN")]
        GrossGallons,

        [EdiFieldValue("GO")]
        MilligramsPerSquareMeter,

        [EdiFieldValue("GP")]
        MilligramsPerCubicMeter,

        [EdiFieldValue("GQ")]
        MicrogramsPerCubicMeter,

        [EdiFieldValue("GR")]
        Gram,

        [EdiFieldValue("GS")]
        Gross,

        [EdiFieldValue("GT")]
        GrossKilogram,

        [EdiFieldValue("GU")]
        GaussPerOersteds,

        [EdiFieldValue("GV")]
        Gigajoules,

        [EdiFieldValue("GW")]
        GallonsPerThousandCubicFeet,

        [EdiFieldValue("GX")]
        Grain,

        [EdiFieldValue("GY")]
        GrossYard,

        [EdiFieldValue("GZ")]
        GageSystems,

        [EdiFieldValue("H1")]
        HalfPages_Electronic,

        [EdiFieldValue("H2")]
        HalfLiter,

        [EdiFieldValue("H4")]
        Hectoliter,

        [EdiFieldValue("HA")]
        Hank,

        [EdiFieldValue("HB")]
        HundredBoxes,

        [EdiFieldValue("HC")]
        HundredCount,

        [EdiFieldValue("HD")]
        HalfDozen,

        [EdiFieldValue("HE")]
        HundredthOfACarat,

        [EdiFieldValue("HF")]
        HundredFeet,

        [EdiFieldValue("HG")]
        Hectogram,

        [EdiFieldValue("HH")]
        HundredCubicFeet,

        [EdiFieldValue("HI")]
        HundredSheets,

        [EdiFieldValue("HJ")]
        Horsepower,

        [EdiFieldValue("HK")]
        HundredKilograms,

        [EdiFieldValue("HL")]
        HundredFeet_Linear,

        [EdiFieldValue("HM")]
        MilesPerHour,

        [EdiFieldValue("HN")]
        MillimetersOfMercury,

        [EdiFieldValue("HO")]
        HundredTroyOunces,

        [EdiFieldValue("HP")]
        MillimeterH20,

        [EdiFieldValue("HQ")]
        Hectare,

        [EdiFieldValue("HR")]
        Hours,

        [EdiFieldValue("HS")]
        HundredSquareFeet,

        [EdiFieldValue("HT")]
        HalfHour,

        [EdiFieldValue("HU")]
        Hundred,

        [EdiFieldValue("HV")]
        HundredWeight_Short,

        [EdiFieldValue("HW")]
        HundredWeight_Long,

        [EdiFieldValue("HY")]
        HundredYards,

        [EdiFieldValue("HZ")]
        Hertz,

        [EdiFieldValue("IA")]
        InchPound,

        [EdiFieldValue("IB")]
        InchesPerSecond_VibrationVelocity,

        [EdiFieldValue("IC")]
        CountsPerInch,

        [EdiFieldValue("IE")]
        Person,

        [EdiFieldValue("IF")]
        InchesOfWater,

        [EdiFieldValue("IH")]
        Inhaler,

        [EdiFieldValue("II")]
        ColumnInches,

        [EdiFieldValue("IK")]
        PeaksPerInch_PPI,

        [EdiFieldValue("IL")]
        InchesPerMinute,

        [EdiFieldValue("IM")]
        Impressions,

        [EdiFieldValue("IN")]
        Inch,

        [EdiFieldValue("IP")]
        InsurancePolicy,

        [EdiFieldValue("IT")]
        CountsPerCentimeter,

        [EdiFieldValue("IU")]
        InchesPerSecond_LinearSpeed,

        [EdiFieldValue("IV")]
        InchesPerSecondPerSecond_Acceleration,

        [EdiFieldValue("IW")]
        InchesPerSecondPerSecond_VibrationAcceleration,

        [EdiFieldValue("J2")]
        JoulePerKilogram,

        [EdiFieldValue("JA")]
        Job,

        [EdiFieldValue("JB")]
        Jumbo,

        [EdiFieldValue("JE")]
        JoulePerKelvin,

        [EdiFieldValue("JG")]
        JoulePerGram,

        [EdiFieldValue("JK")]
        MegaJoulePerKilogram,

        [EdiFieldValue("JM")]
        MegajoulePerCubicMeter,

        [EdiFieldValue("JO")]
        Joint,

        [EdiFieldValue("JR")]
        Jar,

        [EdiFieldValue("JU")]
        Jug,

        [EdiFieldValue("K1")]
        KilowattDemand,

        [EdiFieldValue("K2")]
        KilovoltAmperesReactiveDemand,

        [EdiFieldValue("K3")]
        KilovoltAmperesReactiveHour,

        [EdiFieldValue("K4")]
        KilovoltAmperes,

        [EdiFieldValue("K5")]
        KilovoltAmperesReactive,

        [EdiFieldValue("K6")]
        Kiloliter,

        [EdiFieldValue("K7")]
        Kilowatt,

        [EdiFieldValue("K9")]
        KilogramsPerMillimeterSquared_KGPerMM2,

        [EdiFieldValue("KA")]
        Cake,

        [EdiFieldValue("KB")]
        Kilocharacters,

        [EdiFieldValue("KC")]
        KilogramsPerCubicMeter,

        [EdiFieldValue("KD")]
        KilogramsDecimal,

        [EdiFieldValue("KE")]
        Keg,

        [EdiFieldValue("KF")]
        Kilopackets,

        [EdiFieldValue("KG")]
        Kilogram,

        [EdiFieldValue("KH")]
        KilowattHour,

        [EdiFieldValue("KI")]
        KilogramsPerMillimeterWidth,

        [EdiFieldValue("KJ")]
        Kilosegments,

        [EdiFieldValue("KK")]
        _100Kilograms,

        [EdiFieldValue("KL")]
        KilogramsPerMeter,

        [EdiFieldValue("KM")]
        KilogramsPerSquareMeter_Kilograms_Decimal,

        [EdiFieldValue("KO")]
        MillequivalenceCausticPotashPerGramOfProduct,

        [EdiFieldValue("KP")]
        KilometersPerHour,

        [EdiFieldValue("KQ")]
        Kilopascal,

        [EdiFieldValue("KR")]
        Kiloroentgen,

        [EdiFieldValue("KS")]
        _1000PoundsPerSquareInch,

        [EdiFieldValue("KT")]
        Kit,

        [EdiFieldValue("KU")]
        _Task,

        [EdiFieldValue("KV")]
        Kelvin,

        [EdiFieldValue("KW")]
        KilogramsPerMillimeter,

        [EdiFieldValue("KX")]
        MillilitersPerKilogram,

        [EdiFieldValue("L2")]
        LitersPerMinute,

        [EdiFieldValue("LA")]
        PoundsPerCubicInch,

        [EdiFieldValue("LB")]
        Pound,

        [EdiFieldValue("LC")]
        LinearCentimeter,

        [EdiFieldValue("LE")]
        Lite,

        [EdiFieldValue("LF")]
        LinearFoot,

        [EdiFieldValue("LG")]
        LongTon,

        [EdiFieldValue("LH")]
        LaborHours,

        [EdiFieldValue("LI")]
        LinearInch,

        [EdiFieldValue("LJ")]
        LargeSpray,

        [EdiFieldValue("LK")]
        Link,

        [EdiFieldValue("LL")]
        Lifetime,

        [EdiFieldValue("LM")]
        LinearMeter,

        [EdiFieldValue("LN")]
        Length,

        [EdiFieldValue("LO")]
        Lot,

        [EdiFieldValue("LP")]
        LiquidPounds,

        [EdiFieldValue("LQ")]
        LitersPerDay,

        [EdiFieldValue("LR")]
        Layers,

        [EdiFieldValue("LS")]
        LumpSum,

        [EdiFieldValue("LT")]
        Liter,

        [EdiFieldValue("LX")]
        LinearYardsPerPound,

        [EdiFieldValue("LY")]
        LinearYard,

        [EdiFieldValue("M0")]
        MagneticTapes,

        [EdiFieldValue("M1")]
        MilligramsperLiter,

        [EdiFieldValue("M2")]
        MillimeterActual,

        [EdiFieldValue("M3")]
        Mat,

        [EdiFieldValue("M4")]
        MonetaryValue,

        [EdiFieldValue("M5")]
        Microcurie,

        [EdiFieldValue("M6")]
        Millibar,

        [EdiFieldValue("M7")]
        MicroInch,

        [EdiFieldValue("M8")]
        MegaPascals,

        [EdiFieldValue("M9")]
        MillionBritishThermalUnitsperOneThousandCubicFeet,

        [EdiFieldValue("MA")]
        MachinePerUnit,

        [EdiFieldValue("MB")]
        MillimeterNominal,

        [EdiFieldValue("MC")]
        Microgram,

        [EdiFieldValue("MD")]
        AirDryMetricTon,

        [EdiFieldValue("ME")]
        Milligram,

        [EdiFieldValue("MF")]
        MilligramPerSqFtperSide,

        [EdiFieldValue("MG")]
        MetricGrossTon,

        [EdiFieldValue("MH")]
        Microns_Micrometers,

        [EdiFieldValue("MI")]
        Metric,

        [EdiFieldValue("MJ")]
        Minutes,

        [EdiFieldValue("MK")]
        MilligramsPerSquareInch,

        [EdiFieldValue("ML")]
        Milliliter,

        [EdiFieldValue("MM")]
        Millimeter,

        [EdiFieldValue("MN")]
        MetricNetTon,

        [EdiFieldValue("MO")]
        Months,

        [EdiFieldValue("MP")]
        MetricTon,

        [EdiFieldValue("MQ")]
        _1000Meters,

        [EdiFieldValue("MR")]
        Meter,

        [EdiFieldValue("MS")]
        SquareMillimeter,

        [EdiFieldValue("MT")]
        MetricLongTon,

        [EdiFieldValue("MU")]
        Millicurie,

        [EdiFieldValue("MV")]
        NumberOfMults,

        [EdiFieldValue("MW")]
        MetricTonKilograms,

        [EdiFieldValue("MX")]
        Mixed,

        [EdiFieldValue("MY")]
        MillimeterAverage,

        [EdiFieldValue("MZ")]
        MillimeterMinimum,

        [EdiFieldValue("N1")]
        PenCalories,

        [EdiFieldValue("N2")]
        NumberOfLines,

        [EdiFieldValue("N3")]
        PrintPoint,

        [EdiFieldValue("N4")]
        PenGrams_Protein,

        [EdiFieldValue("N6")]
        Megahertz,

        [EdiFieldValue("N7")]
        Parts,

        [EdiFieldValue("N9")]
        CartridgeNeedle,

        [EdiFieldValue("NA")]
        MilligramsPerKilogram,

        [EdiFieldValue("NB")]
        Barge,

        [EdiFieldValue("NC")]
        Car,

        [EdiFieldValue("ND")]
        NetBarrels,

        [EdiFieldValue("NE")]
        NetLiters,

        [EdiFieldValue("NF")]
        Messages,

        [EdiFieldValue("NG")]
        NetGallons,

        [EdiFieldValue("NH")]
        MessageHours,

        [EdiFieldValue("NI")]
        NetImperialGallons,

        [EdiFieldValue("NJ")]
        NumberOfScreens,

        [EdiFieldValue("NL")]
        Load,

        [EdiFieldValue("NM")]
        NauticalMile,

        [EdiFieldValue("NN")]
        Train,

        [EdiFieldValue("NQ")]
        Mho,

        [EdiFieldValue("NR")]
        MicroMho,

        [EdiFieldValue("NS")]
        ShortTon,

        [EdiFieldValue("NT")]
        Trailer,

        [EdiFieldValue("NU")]
        NewtonMeter,

        [EdiFieldValue("NV")]
        Vehicle,

        [EdiFieldValue("NW")]
        Newton,

        [EdiFieldValue("NX")]
        PartsPerThousand,

        [EdiFieldValue("NY")]
        PoundsPerAirDryMetricTon,

        [EdiFieldValue("OA")]
        Panel,

        [EdiFieldValue("OC")]
        Billboard,

        [EdiFieldValue("ON")]
        OuncesPerSquareYard,

        [EdiFieldValue("OP")]
        TwoPack,

        [EdiFieldValue("OT")]
        OvertimeHours,

        [EdiFieldValue("OZ")]
        Ounce_Av,

        [EdiFieldValue("P0")]
        Pages_Electronic,

        [EdiFieldValue("P1")]
        Percent,

        [EdiFieldValue("P2")]
        Pounds_PerFoot,

        [EdiFieldValue("P3")]
        ThreePack,

        [EdiFieldValue("P4")]
        FourPack,

        [EdiFieldValue("P5")]
        FivePack,

        [EdiFieldValue("P6")]
        SixPack,

        [EdiFieldValue("P7")]
        SevenPack,

        [EdiFieldValue("P8")]
        EightPack,

        [EdiFieldValue("P9")]
        NinePack,

        [EdiFieldValue("PA")]
        Pail,

        [EdiFieldValue("PB")]
        PairInches,

        [EdiFieldValue("PC")]
        Piece,

        [EdiFieldValue("PD")]
        Pad,

        [EdiFieldValue("PE")]
        PoundsEquivalent,

        [EdiFieldValue("PF")]
        Pallet_Lift,

        [EdiFieldValue("PG")]
        PoundsGross,

        [EdiFieldValue("PH")]
        Pack,

        [EdiFieldValue("PI")]
        Pitch,

        [EdiFieldValue("PJ")]
        Pounds_Decimal_PoundsPerSquareFoot_PoundGage,

        [EdiFieldValue("PK")]
        Package,

        [EdiFieldValue("PL")]
        Pallet_UnitLoad,

        [EdiFieldValue("PM")]
        PoundsPercentage,

        [EdiFieldValue("PN")]
        PoundsNet,

        [EdiFieldValue("PO")]
        PoundsPerInchOfLength,

        [EdiFieldValue("PP")]
        Plate,

        [EdiFieldValue("PQ")]
        PagesPerInch,

        [EdiFieldValue("PR")]
        Pair,

        [EdiFieldValue("PS")]
        PoundsPerSqInch,

        [EdiFieldValue("PT")]
        Pint,

        [EdiFieldValue("PU")]
        MassPounds,

        [EdiFieldValue("PV")]
        HalfPint,

        [EdiFieldValue("PW")]
        PoundsPerInchOfWidth,

        [EdiFieldValue("PX")]
        Pint_Imperial,

        [EdiFieldValue("PY")]
        Peck_DryUS,

        [EdiFieldValue("PZ")]
        Peck_DryImperial,

        [EdiFieldValue("Q1")]
        Quarter_Time,

        [EdiFieldValue("Q2")]
        Pint_USDry,

        [EdiFieldValue("Q3")]
        Meal,

        [EdiFieldValue("Q4")]
        Fifty,

        [EdiFieldValue("Q5")]
        TwentyFive,

        [EdiFieldValue("Q6")]
        ThirtySix,

        [EdiFieldValue("Q7")]
        TwentyFour,

        [EdiFieldValue("QA")]
        Pages_Facsimile,

        [EdiFieldValue("QB")]
        Pages_Hardcopy,

        [EdiFieldValue("QC")]
        Channel,

        [EdiFieldValue("QD")]
        QuarterDozen,

        [EdiFieldValue("QE")]
        Photographs,

        [EdiFieldValue("QH")]
        QuarterHours,

        [EdiFieldValue("QK")]
        QuarterKilogram,

        [EdiFieldValue("QR")]
        Quire,

        [EdiFieldValue("QS")]
        Quart_DryUS,

        [EdiFieldValue("QT")]
        Quart,

        [EdiFieldValue("QU")]
        Quart_Imperial,

        [EdiFieldValue("R1")]
        Pica,

        [EdiFieldValue("R2")]
        Becquerel,

        [EdiFieldValue("R3")]
        RevolutionsPerMinute,

        [EdiFieldValue("R4")]
        Calorie,

        [EdiFieldValue("R5")]
        ThousandsOfDollars,

        [EdiFieldValue("R6")]
        MillionsOfDollars,

        [EdiFieldValue("R7")]
        BillionsOfDollars,

        [EdiFieldValue("R8")]
        RoentgenEquivalentInMan_REM,

        [EdiFieldValue("R9")]
        ThousandCubicMeters,

        [EdiFieldValue("RA")]
        Rack,

        [EdiFieldValue("RB")]
        Radian,

        [EdiFieldValue("RC")]
        Rod_area_16Pt25SquareYards,

        [EdiFieldValue("RD")]
        Rod_length_5Pt5Yards,

        [EdiFieldValue("RE")]
        Reel,

        [EdiFieldValue("RG")]
        Ring,

        [EdiFieldValue("RH")]
        RunningOrOperatingHours,

        [EdiFieldValue("RK")]
        RollMetricMeasure,

        [EdiFieldValue("RL")]
        Roll,

        [EdiFieldValue("RM")]
        Ream,

        [EdiFieldValue("RN")]
        ReamMetricMeasure,

        [EdiFieldValue("RO")]
        Round,

        [EdiFieldValue("RP")]
        PoundsPerReam,

        [EdiFieldValue("RS")]
        Resets,

        [EdiFieldValue("RT")]
        RevenueTonMiles,

        [EdiFieldValue("RU")]
        Run,

        [EdiFieldValue("S1")]
        Semester,

        [EdiFieldValue("S2")]
        Trimester,

        [EdiFieldValue("S3")]
        SquareFeetPerSecond,

        [EdiFieldValue("S4")]
        SquareMetersPerSecond,

        [EdiFieldValue("S5")]
        SixtyFourthsOfAnInch,

        [EdiFieldValue("S6")]
        Sessions,

        [EdiFieldValue("S7")]
        StorageUnits,

        [EdiFieldValue("S8")]
        StandardAdvertisingUnits_SAUs,

        [EdiFieldValue("S9")]
        SlipSheet,

        [EdiFieldValue("SA")]
        Sandwich,

        [EdiFieldValue("SB")]
        SquareMile,

        [EdiFieldValue("SC")]
        SquareCentimeter,

        [EdiFieldValue("SD")]
        SolidPounds,

        [EdiFieldValue("SE")]
        Section,

        [EdiFieldValue("SF")]
        SquareFoot,

        [EdiFieldValue("SG")]
        Segment,

        [EdiFieldValue("SH")]
        Sheet,

        [EdiFieldValue("SI")]
        SquareInch,

        [EdiFieldValue("SJ")]
        Sack,

        [EdiFieldValue("SK")]
        SplitTanktruck,

        [EdiFieldValue("SL")]
        Sleeve,

        [EdiFieldValue("SM")]
        SquareMeter,

        [EdiFieldValue("SN")]
        SquareRod,

        [EdiFieldValue("SO")]
        Spool,

        [EdiFieldValue("SP")]
        ShelfPackage,

        [EdiFieldValue("SQ")]
        Square,

        [EdiFieldValue("SR")]
        Strip,

        [EdiFieldValue("SS")]
        SheetMetricMeasure,

        [EdiFieldValue("ST")]
        Set,

        [EdiFieldValue("SV")]
        Skid,

        [EdiFieldValue("SW")]
        Skein,

        [EdiFieldValue("SX")]
        Shipment,

        [EdiFieldValue("SY")]
        SquareYard,

        [EdiFieldValue("SZ")]
        Syringe,

        [EdiFieldValue("T0")]
        TelecommunicationsLinesInService,

        [EdiFieldValue("T1")]
        ThousandPoundsGross,

        [EdiFieldValue("T2")]
        ThousandthsOfAnInch,

        [EdiFieldValue("T3")]
        ThousandPieces,

        [EdiFieldValue("T4")]
        ThousandBags,

        [EdiFieldValue("T5")]
        ThousandCasings,

        [EdiFieldValue("T6")]
        ThousandGallons,

        [EdiFieldValue("T7")]
        ThousandImpressions,

        [EdiFieldValue("T8")]
        ThousandLinearInches,

        [EdiFieldValue("T9")]
        ThousandKilowattHours,

        [EdiFieldValue("TA")]
        TenthCubicFoot,

        [EdiFieldValue("TB")]
        Tube,

        [EdiFieldValue("TC")]
        Truckload,

        [EdiFieldValue("TD")]
        Therms,

        [EdiFieldValue("TE")]
        Tote,

        [EdiFieldValue("TF")]
        TenSquareYards,

        [EdiFieldValue("TG")]
        GrossTon,

        [EdiFieldValue("TH")]
        Thousand,

        [EdiFieldValue("TI")]
        ThousandSquareInches,

        [EdiFieldValue("TJ")]
        ThousandSqCentimeters,

        [EdiFieldValue("TK")]
        Tank,

        [EdiFieldValue("TL")]
        ThousandFeet_Linear,

        [EdiFieldValue("TM")]
        ThousandFeet_Board,

        [EdiFieldValue("TN")]
        NetTon,

        [EdiFieldValue("TO")]
        TroyOunce,

        [EdiFieldValue("TP")]
        TenPack,

        [EdiFieldValue("TQ")]
        ThousandFeet,

        [EdiFieldValue("TR")]
        TenSquareFeet,

        [EdiFieldValue("TS")]
        ThousandSquareFeet,

        [EdiFieldValue("TT")]
        ThousandLinearMeters,

        [EdiFieldValue("TU")]
        ThousandLinearYards,

        [EdiFieldValue("TV")]
        ThousandKilograms,

        [EdiFieldValue("TW")]
        ThousandSheets,

        [EdiFieldValue("TX")]
        TroyPound,

        [EdiFieldValue("TY")]
        Tray,

        [EdiFieldValue("TZ")]
        ThousandCubicFeet,

        [EdiFieldValue("U1")]
        Treatments,

        [EdiFieldValue("U2")]
        Tablet,

        [EdiFieldValue("U3")]
        Ten,

        [EdiFieldValue("U5")]
        TwoHundredFifty,

        [EdiFieldValue("UA")]
        Torr,

        [EdiFieldValue("UB")]
        TelecommunicationsLinesInService_Average,

        [EdiFieldValue("UC")]
        TelecommunicationsPorts,

        [EdiFieldValue("UD")]
        TenthMinutes,

        [EdiFieldValue("UE")]
        TenthHours,

        [EdiFieldValue("UF")]
        UsagePerTelecommunicationsLine_Average,

        [EdiFieldValue("UH")]
        TenThousandYards,

        [EdiFieldValue("UL")]
        Unitless,

        [EdiFieldValue("UM")]
        MillionUnits,

        [EdiFieldValue("UN")]
        Unit,

        [EdiFieldValue("UP")]
        Troche,

        [EdiFieldValue("UQ")]
        Wafer,

        [EdiFieldValue("UR")]
        Application,

        [EdiFieldValue("US")]
        DosageForm,

        [EdiFieldValue("UT")]
        Inhalation,

        [EdiFieldValue("UU")]
        Lozenge,

        [EdiFieldValue("UV")]
        PercentTopicalOnly,

        [EdiFieldValue("UW")]
        Milliequivalent,

        [EdiFieldValue("UX")]
        Dram_Minim,

        [EdiFieldValue("UY")]
        FiftySquareFeet,

        [EdiFieldValue("UZ")]
        FiftyCount,

        [EdiFieldValue("V1")]
        Flat,

        [EdiFieldValue("V2")]
        Pouch,

        [EdiFieldValue("VA")]
        VoltAmperePerKilogram,

        [EdiFieldValue("VC")]
        FiveHundred,

        [EdiFieldValue("VI")]
        Vial,

        [EdiFieldValue("VP")]
        PercentVolume,

        [EdiFieldValue("VR")]
        VoltAmpereReactive,

        [EdiFieldValue("VS")]
        Visit,

        [EdiFieldValue("W2")]
        WetKilo,

        [EdiFieldValue("WA")]
        WattsPerKilogram,

        [EdiFieldValue("WB")]
        WetPound,

        [EdiFieldValue("WD")]
        WorkDays,

        [EdiFieldValue("WE")]
        WetTon,

        [EdiFieldValue("WG")]
        WineGallon,

        [EdiFieldValue("WH")]
        Wheel,

        [EdiFieldValue("WI")]
        WeightPerSquareInch,

        [EdiFieldValue("WK")]
        Week,

        [EdiFieldValue("WM")]
        WorkingMonths,

        [EdiFieldValue("WP")]
        Pennyweight,

        [EdiFieldValue("WR")]
        Wrap,

        [EdiFieldValue("WW")]
        MillilitersOfWater,

        [EdiFieldValue("X1")]
        Chains_LandSurvey,

        [EdiFieldValue("X2")]
        Bunch,

        [EdiFieldValue("X3")]
        Clove,

        [EdiFieldValue("X4")]
        Drop,

        [EdiFieldValue("X5")]
        Head,

        [EdiFieldValue("X6")]
        Heart,

        [EdiFieldValue("X7")]
        Leaf,

        [EdiFieldValue("X8")]
        Loaf,

        [EdiFieldValue("X9")]
        Portion,

        [EdiFieldValue("XP")]
        BaseBoxPerPound,

        [EdiFieldValue("Y1")]
        Slice,

        [EdiFieldValue("Y2")]
        Tablespoon,

        [EdiFieldValue("Y3")]
        Teaspoon,

        [EdiFieldValue("Y4")]
        Tub,

        [EdiFieldValue("YD")]
        Yard,

        [EdiFieldValue("YL")]
        _100LinealYards,

        [EdiFieldValue("YR")]
        Years,

        [EdiFieldValue("YT")]
        TenYards,

        [EdiFieldValue("Z1")]
        LiftVan,

        [EdiFieldValue("Z2")]
        Chest,

        [EdiFieldValue("Z3")]
        Cask,

        [EdiFieldValue("Z4")]
        Hogshead,

        [EdiFieldValue("Z5")]
        Lug,

        [EdiFieldValue("Z6")]
        ConferencePoints,

        [EdiFieldValue("Z8")]
        NewspaperAgateLine,

        [EdiFieldValue("ZA")]
        Bimonthly,

        [EdiFieldValue("ZB")]
        Biweekly,

        [EdiFieldValue("ZC")]
        Semiannual,

        [EdiFieldValue("ZP")]
        Page,

        [EdiFieldValue("ZZ")]
        MutuallyDefined
    }
}
