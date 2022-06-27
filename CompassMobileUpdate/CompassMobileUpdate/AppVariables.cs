using CompassDataAccess;
using CompassMobileModels;
using CompassMobileUpdate.Models;
using CompassMobileUpdate.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace CompassMobileUpdate
{
    using VoltageRule = LocalVoltageRule;

    public static class AppVariables
    {
        private static bool isInitalized = false;
        private static int _defaultFadeMs;
        private static DateTimeOffset _startTime;
        private static List<VoltageRule> _voltageRules = new List<VoltageRule>();
        public static readonly ImageSource StatusErrorImage;
        public static readonly ImageSource StatusGoodImage;
        public static readonly ImageSource StatusUncertainImage;
        public static readonly ImageSource StatusBlankImage;
        public static readonly ImageSource UnderConstruction;
        public static readonly RestSharp.Parameter AuthorizationHeaderParameter;
        public static readonly RestSharp.Parameter ExtendedJWTHeaderParameter;
        public static readonly RestSharp.Parameter CompassMaintenanceParameter; // MAJ 12/18/2017
        private static bool _isLogging = false;
        public static bool IsLogging
        {
            get { return _isLogging; }
            set
            {
                _isLogging = value;
                ResetAppService();
            }
        }
        private static AppService _appService;
        private static AppEnvironment _appEnvironment;
        private static LocalSql _localSql;
        private static bool _isCachingMapPins = false;
        private static bool _isResolvingByIP;
        private static HashSet<string> _disallowedPQRSubTypeNames = new HashSet<string>();
        public static AppUser User { get; set; }
        public static readonly string AppVersion;
        public static bool IsResolvingByIP
        {
            get
            {
                return _isResolvingByIP;
            }
            set
            {
                _isResolvingByIP = value;

                if (isInitalized)
                {
                    ResetAppService();
                }
            }
        }
        public static string TimeZoneID { get; private set; }
        public static Company Company { get; private set; }
        public static bool CmoIsLive { get; set; }

        static AppVariables()
        {
            AppVersion = "1.5.0"; // "1.4.1";
            Company = Company.ComEd;
            ResetCompanyVariables(Company);
            StatusErrorImage = ImageSource.FromFile("status_error.png");
            StatusGoodImage = ImageSource.FromFile("status_good.png");
            StatusUncertainImage = ImageSource.FromFile("question.png");
            StatusBlankImage = ImageSource.FromFile("transparentx10.png");
            UnderConstruction = ImageSource.FromFile("under_construction.png");
            _isResolvingByIP = true;
            //See documentation: https://restsharp.dev/v107/#headers new way of adding header parameters to request.

            _localSql = new LocalSql();
            _defaultFadeMs = 3000;

            ResetDisallowedPQRSubTypes();

            ResetAppService();
            isInitalized = true;
        }
        public static bool IsCachingMapPins
        {
            get { return _isCachingMapPins; }
            set { _isCachingMapPins = value; }
        }

        public static LocalSql LocalAppSql
        {
            get
            {
                return _localSql;
            }
        }

        public static AppEnvironment AppEnvironment
        {
            get
            {
                return _appEnvironment;
            }
            set
            {
                _appEnvironment = value;

                if (isInitalized)
                {
                    ResetAppService();
                }
            }
        }
        public static int DefaultFadeMs
        {
            get { return _defaultFadeMs; }
        }

        public static App Application
        {
            get;
            set;
        }
        public static DateTimeOffset StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        public static List<VoltageRule> VoltageRules
        {
            get { return _voltageRules; }
            private set { _voltageRules = value; }
        }

        public static MapSpan LastMapPosition { get; set; }
        public static List<Pin> CachedMapPins { get; set; }
        public static List<BoundingCoordinates> CachedMapBoundingCoords { get; set; }

        public async static Task ResetVoltageRules(bool forceResync)
        {
            //var lastSyncTime = LocalAppSql.GetLastVoltageSyncTime();
        }

        private static void ResetAppService()
        {
            _appService = new AppService();
        }
        public static void ResetCompanyVariables(Company company)
        {
            //these Time zones are "Olson Time Zones" and not .NET TimzeZones which is why they aren't
            //"Central Standard Time" and "Eastern Standard Time"
            if (company == Company.ComEd)
            {
                TimeZoneID = "America/Chicago";
            }
            else
            {
                TimeZoneID = "America/New_York";
            }
        }
        public static void ResetDisallowedPQRSubTypes()
        {
            _disallowedPQRSubTypeNames = GetDisallowedPQRSubTypeNames();
        }

        /// <summary>
        /// List of Lowercase subtypes
        /// If you add any in the future add them in lowercase
        /// </summary>
        /// <returns></returns>
        private static HashSet<string> GetDisallowedPQRSubTypeNames()
        {
            HashSet<string> subType = new HashSet<string>();
            subType.Add("did_subtype_generic_dlms");
            subType.Add("did_subtype_generic_gmi");
            subType.Add("did_subtype_generic_gmi_han");
            subType.Add("did_subtype_imu_gas");
            subType.Add("did_subtype_imu_water");
            subType.Add("did_subtype_i210dt");
            subType.Add("did_subtype_i210eo");
            subType.Add("did_subtype_i210eo_han");
            subType.Add("did_subtype_i210rd");
            subType.Add("did_subtype_i210rd_han");
            subType.Add("did_subtype_kv2");
            subType.Add("did_subtype_lg_ax");
            subType.Add("did_subtype_lg_ax_han");
            subType.Add("did_subtype_lg_axsd");
            subType.Add("did_subtype_lg_axsd_han");
            subType.Add("did_subtype_lg_g370_gas");
            subType.Add("did_subtype_lg_s4e");
            subType.Add("did_subtype_lg_s4e_han");
            return subType;
        }
    }
}
