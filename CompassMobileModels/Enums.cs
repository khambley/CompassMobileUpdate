using System;
using System.Collections.Generic;
using System.Text;

namespace CompassMobileModels
{
    public enum MeterAvailabilityEventsEventType
    {
        Restorations = 100007,
        Outages = 12007
    }
    public enum AppEnvironment
    {
        Development,
        Test,
        Production
    }
    public enum CustomerClassEnum
    {
        Unknown,
        Residential,
        Commercial
    }
    public enum GetMeterForDeviceEndDeviceType
    {
        Meter
    }
    public enum Manufacturers
    {
        Elster,
        GE,
        LandG,
        Unknown
    }
    public enum Company
    {
        ComEd = 1,
        PECO = 2,
        BGE = 3,
        PHI = 4
    }
    public enum MeterPowerEventID
    {
        PingOn = 1691471514,
        PingOff = 1691471566,
        PowerRestore = 100007,
        LastGasp = 12007
    }
    public enum MeterPowerEventCategory
    {
        AvailabilityEvent = 1007,
        PingResult = 169147
    }

    /// <summary>
    /// Disposition IDs must match their counterpars in the COVR_DISPOSITION table
    /// </summary>
    public enum COVRDispositionEnum
    {
        AwaitingKillForMissingWorker = -3,
        AwaitingKillForMissingRecordLogs = -2,
        NULL = -1,
        AwaitingKillForAnytimePurge = 0,
        AwaitingKillForDNPPurge = 1,
        AwaitingKillForStale = 2,
        AwaitingKillForCOVRKillSwitch = 3,
        AwaitingKillForCORESurgeMode = 4,
        AwaitingKillForEOCOpconLevel = 5,
        AwaitingKillForeChannelsAutoShutoff = 6,
        AwaitingKillFor3PhaseMeter = 7,
        AwaitingKillForMissingOMSAccountID = 8,
        AwaitingKillForMissingPhaseType = 9,
        AwaitingKillForMissingOffice = 10,
        AwaitingNPDQCheck = 11,
        AwaitingLargeCommercialAccount = 12,
        AwaitingAdoption = 13,
        AwaitingDNPHoursDesignation = 14,
        AwaitingSendToAgent511 = 15,
        AwaitingKillForNotSendingToAgent511 = 16,
        AwaitingPing = 17,
        AwaitingDNPHoursToEnd = 18,
        AwaitingRecordCooldown = 19,
        AwaitingKillForOfficeTurnedOff = 21,
        AwaitingSendToAgent511RespectDNPThrottle = 23,
        AwaitingMultiMeteredAccountStatus = 27,
        AwaitingKillForMultipleMeters = 28,
        Processing = 99,
        SentToAgent511 = 100,
        AnyTimePurge = 700,
        DNPPurge = 701,
        LastGaspIsStale = 702,
        COVRKillSwitch = 703,
        CORESurgeMode = 704,
        EOCOpconLevel = 705,
        eChannelsAutoShutoff = 706,
        ThreePhaseMeter = 707,
        MissingOMSAccountID = 708,
        MissingPhaseType = 709,
        MissingOffice = 710,
        NotSendingToAgent511 = 711,
        ErrorMissingRecordLogs = 712,
        ErrorMissingWorker = 713,
        ErrorAdoptionRecordMissingAbandonedProcessLogID = 714,
        ErrorDispositionNotFoundInResolveRecordsWorker = 715,
        ErrorMissingLargeCommercialAccountStatus = 716,
        OfficeIsTurnedOff = 717,
        MeterNotPingable = 718,
        MeterStateNotAllowed = 719,
        MultiMeteredAccount = 720,
        PowerRestore = 900,
        PingOn = 901,
        OpenOutage = 902,
        ClosedOutage = 903,
        OpenTroubleTicket = 904
    }

    public enum PcadJobStatus
    {
        AC,
        RC,
        DP,
        OS,
        RP,
        CL
    }


    public enum CmoProcessingStatusEnum
    {
        Unknown,
        Open,
        Processing,
        Processed_EMAIL,
        Processed_PCAD,
        Processed_NO_ACTION,
        Processed_EMAIL_NO_RPT2,
        Deleted,
        Fault,
        Unsynced,
        Done
    }

    public static class CmoProcessingStatus
    {
        public static Dictionary<string, CmoProcessingStatusEnum> MappingDict
            = new Dictionary<string, CmoProcessingStatusEnum>
            {
                { "Unknown",                    CmoProcessingStatusEnum.Unknown},
                { "Open",                       CmoProcessingStatusEnum.Open},
                { "Processing",                 CmoProcessingStatusEnum.Processing},
                { "Processed_email",            CmoProcessingStatusEnum.Processed_EMAIL},
                { "Processed_pcad",             CmoProcessingStatusEnum.Processed_PCAD},
                { "Processed_no_action",        CmoProcessingStatusEnum.Processed_NO_ACTION},
                { "Processed_email_no_rpt2",    CmoProcessingStatusEnum.Processed_EMAIL_NO_RPT2},
                { "Deleted",                    CmoProcessingStatusEnum.Deleted},
                { "Fault",                      CmoProcessingStatusEnum.Fault},
                { "Unsynced",                   CmoProcessingStatusEnum.Unsynced},
                { "Done",                       CmoProcessingStatusEnum.Done},
            };

        public static CmoProcessingStatusEnum GetEnumVal(string enumKey, CmoProcessingStatusEnum defaultVal)
        {
            enumKey = enumKey.ToLower().FirstCharToUpper();

            if (MappingDict.ContainsKey(enumKey))
            {
                return MappingDict[enumKey];
            }

            return defaultVal;
        }
    }
}
