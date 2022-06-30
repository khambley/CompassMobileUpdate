using CompassMobileModels;
using CompassMobileUpdate.Helpers;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompassMobileUpdate.Models
{
    public class LocalMeter
    {
        public const string PrimaryKeyPropertyName = "DeviceUtilityID";
        [PrimaryKey, Collation("NOCASE")]
        public string DeviceUtilityID { get; set; }
        public string DeviceUtilityIDWithLocation { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerContactNumber { get; set; }
        public double? Distance { get; set; }
        [Indexed]
        public bool IsFavorite { get; set; }

        public DateTime CreatedTime { get; set; }
        [Indexed]
        public DateTime LastAccessedTime { get; set; }
        [Indexed]
        public DateTime LastUpdatedTime { get; set; }
        [Ignore] //don't create a column in the Database for this Property
        public string DeviceUtilityIDAndCustomerName
        {
            get
            {
                return MeterHelper.GetDeviceUtilityIDAndCustomerName(this);
            }
        }
        [Ignore] //don't create a column in the Database for this Property
        public string CustomerNameAndDeviceUtilityID
        {
            get
            {
                return MeterHelper.GetCustomerNameAndDeviceUtilityID(this);
            }
        }
        [Ignore] //don't create a column in the Database for this Property
        public string DeviceUtilityIDAndDistance
        {
            get
            {
                return MeterHelper.GetDeviceUtilityIDAndDistance(this);
            }
        }
        [Ignore] //don't create a column in the Database for this Property
        public string DistanceAndCustomerAddress
        {
            get
            {
                return MeterHelper.GetDistanceAndCustomerAddress(this);
            }
        }
        [Ignore] //don't create a column in the Database for this Property
        public double? DistanceInFeet { get { return MeterHelper.GetDistanceInFeet(this); } }
        [Ignore] //don't create a column in the Database for this Property
        public string DistanceInFeetFormatted { get { return MeterHelper.GetDistanceInFeetFormatted(this); } }
        public void ConvertCompassMeterToLocalMeter(Meter meter)
        {
            this.DeviceUtilityID = meter.DeviceUtilityID;
            this.CustomerName = meter.CustomerName;
            this.CustomerAddress = meter.CustomerAddress;
            this.CustomerContactNumber = meter.CustomerContactNumber;
            this.DeviceUtilityIDWithLocation = meter.DeviceUtilityIDWithLocation;
            this.Distance = meter.Distance;
        }

        public static LocalMeter GetLocalMeterFromMeter(Meter meter)
        {
            LocalMeter local = new LocalMeter();
            local.ConvertCompassMeterToLocalMeter(meter);

            return local;
        }
        public static List<LocalMeter> GetListOfLocalMetersFromMeters(List<Meter> meters)
        {
            List<LocalMeter> localMeters = new List<LocalMeter>();
            for (int i = 0; i < meters.Count; i++)
            {
                localMeters.Add(LocalMeter.GetLocalMeterFromMeter(meters[i]));
            }

            return localMeters;
        }


    }
}
