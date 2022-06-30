using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CompassMobileModels
{
    public class Meter
    {
        public string MacID { get; set; }
        public string DeviceSSNID { get; set; }
        public string DeviceUtilityID { get; set; }
        public string DeviceUtilityIDWithLocation { get; set; }
        public string ManufacturerName { get; set; }
        public Decimal? Latitude { get; set; }
        public Decimal? Longitude { get; set; }
        public double? Distance { get; set; }
        public CustomerClassEnum CustomerClassType { get; set; }
        public string CustomerContactNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerClass { get; set; }
        public string Form { get; set; }
        public string Type { get; set; }
        public string TypeName { get; set; }
        public string Version { get; set; }
        public string Model { get; set; }
        public string Status { get; set; }
        public DateTimeOffset StatusDate { get; set; }
        public string NICSoftwareVersion { get; set; }
        public string NICSoftwareRevision { get; set; }
        public string NICSoftwarePatch { get; set; }
        public Meter()
        {
        }

        public static Meter GetObjectFromDataTable(DataTable dt)
        {
            List<Meter> meters = GetListFromDataTable(dt);
            if (meters.Count > 0)
            {
                return meters[0];
            }
            else
                return null;
        }
        public static List<Meter> GetListFromDataTable(DataTable dt)
        {
            List<Meter> items = new List<Meter>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                Meter item = new Meter();

                item.MacID = dr["Mac_ID"].ToString();
                item.DeviceSSNID = dr["Device_SSN_ID"].ToString();
                item.DeviceUtilityID = dr["Device_Utility_ID"].ToString();
                item.ManufacturerName = dr["Manufacturer"].ToString();
                if (dr["Latitude"] != DBNull.Value)
                {
                    item.Latitude = Convert.ToDecimal(dr["Latitude"]);
                }
                if (dr["Longitude"] != DBNull.Value)
                {
                    item.Longitude = Convert.ToDecimal(dr["Longitude"]);
                }

                #region PossibleColumnsToHandle
                if (dt.Columns.Contains("Distance"))
                {
                    if (dr["Distance"] != DBNull.Value)
                    {
                        item.Distance = Convert.ToDouble(dr["Distance"]);
                    }
                }
                if (dt.Columns.Contains("Device_Utility_ID_W_Location"))
                {
                    item.DeviceUtilityIDWithLocation = dr["Device_Utility_ID_W_Location"].ToString();
                }
                if (dt.Columns.Contains("Customer_Contact_Number"))
                {
                    item.CustomerContactNumber = dr["Customer_Contact_Number"].ToString();
                }
                if (dt.Columns.Contains("Customer_Name"))
                {
                    item.CustomerName = dr["Customer_Name"].ToString();
                }
                if (dt.Columns.Contains("Customer_Address"))
                {
                    item.CustomerAddress = dr["Customer_Address"].ToString();
                }
                if (dt.Columns.Contains("Customer_Class"))
                {
                    item.CustomerClass = dr["Customer_Class"].ToString();
                }
                #endregion
                items.Add(item);
            }

            return items;
        }


    }
}
