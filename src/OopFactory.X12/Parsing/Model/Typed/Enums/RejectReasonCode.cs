using OopFactory.X12.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed.Enums
{
    public enum RejectReasonCode
    {
        [EDIFieldValue("15")]
        RequiredApplicationDataMissing,
        [EDIFieldValue("04")]
        AuthorizedQuantityExceeded,
        [EDIFieldValue("41")]
        AuthorizationAccessRestrictions,
        [EDIFieldValue("42")]
        UnableToRespondAtCurrentTime,
        [EDIFieldValue("43")]
        InvalidMissingProviderIdentification,
        [EDIFieldValue("45")]
        InvalidMissingProviderSpecialty,
        [EDIFieldValue("47")]
        InvalidMissingProviderState,
        [EDIFieldValue("48")]
        InvalidMissingReferringProviderIdentificationNumber,
        [EDIFieldValue("49")]
        ProviderisNotPrimaryCarePhysician,
        [EDIFieldValue("51")]
        ProviderNotonFile,
        [EDIFieldValue("52")]
        ServiceDatesNotWithinProviderPlanEnrollment,
        [EDIFieldValue("56")]
        InappropriateDate,
        [EDIFieldValue("57")]
        InvalidMissingDatesOfService,
        [EDIFieldValue("58")]
        InvalidMissingDatOfBirth,
        [EDIFieldValue("60")]
        DateOfBirthFollowsDatesOfService,
        [EDIFieldValue("61")]
        DateOfDeathPrecedesDatesOfService,
        [EDIFieldValue("62")]
        DateOfServiceNotWithinAllowableInquiryPeriod,
        [EDIFieldValue("63")]
        DateOfServiceinFuture,
        [EDIFieldValue("64")]
        InvalidMissingPatientID,
        [EDIFieldValue("65")]
        InvalidMissingPatientName,
        [EDIFieldValue("66")]
        InvalidMissingPatientGenderCode,
        [EDIFieldValue("67")]
        PatientNotFound,
        [EDIFieldValue("68")]
        DuplicatePatientIDNumber,
        [EDIFieldValue("71")]
        PatientBirthDateDoesNotMatchThatforthePatientontheDatabase,
        [EDIFieldValue("72")]
        InvalidMissingSubscriberInsuredID,
        [EDIFieldValue("73")]
        InvalidMissingSubscriberInsuredName,
        [EDIFieldValue("74")]
        InvalidMissingSubscriberInsuredGenderCode,
        [EDIFieldValue("75")]
        SubscriberInsuredNotFound,
        [EDIFieldValue("76")]
        DuplicateSubscriberInsuredIDNumber,
        [EDIFieldValue("77")]
        SubscriberFound_PatientNotFound,
        [EDIFieldValue("78")]
        SubscriberInsuredNotinGroupPlanIdentified,
        [EDIFieldValue("79")]
        InvalidParticipantIdentification,
    }
}
