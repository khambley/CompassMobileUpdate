using CompassDataAccess;
using CompassMobileModels;
using CompassMobileUpdate.Models;
using CompassMobileUpdate.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

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

        private static void ResetAppService()
        {
            _appService = new AppService();
        }
    }
}
