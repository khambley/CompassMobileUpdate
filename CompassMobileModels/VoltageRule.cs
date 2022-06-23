using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CompassMobileModels
{
    public class VoltageRule
    {
        public int ID { get; set; }
        public string MeterForm { get; set; }
        public string MeterType { get; set; }
        public int TargetVoltage { get; set; }
        public int ResidentialVoltageLow { get; set; }
        public int ResidentialVoltageHigh { get; set; }
        public int CommercialVoltageLow { get; set; }
        public int CommercialVoltageHigh { get; set; }
        public string PhaseType { get; set; }
        public bool OverlappingVoltages { get; set; }

        public VoltageRule()
        {

        }

        #region static Methods
        #region DataBinding
        public static VoltageRule GetObjectFromDataTable(DataTable dt)
        {
            List<VoltageRule> items = GetListFromDataTable(dt);
            if (items.Count > 0)
            {
                return items[0];
            }
            else
                return null;
        }
        public static List<VoltageRule> GetListFromDataTable(DataTable dt)
        {
            List<VoltageRule> items = new List<VoltageRule>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                VoltageRule item = new VoltageRule();

                item.ID = Convert.ToInt32(dr["ID"]);
                item.MeterForm = dr["Meter_Form"].ToString();
                item.MeterType = dr["Meter_Type"].ToString();
                item.TargetVoltage = Convert.ToInt32(dr["Target_Voltage"]);
                item.ResidentialVoltageLow = Convert.ToInt32(dr["Res_Volt_Low"]);
                item.ResidentialVoltageHigh = Convert.ToInt32(dr["Res_Volt_High"]);
                item.CommercialVoltageLow = Convert.ToInt32(dr["Comm_Volt_Low"]);
                item.CommercialVoltageHigh = Convert.ToInt32(dr["Comm_Volt_High"]);
                item.PhaseType = dr["Phase_Type"].ToString();
                item.OverlappingVoltages = Convert.ToBoolean(dr["Overlapping_Voltages"].ToString().ToLower());

                items.Add(item);
            }

            return items;
        }
        #endregion //DataBinding
        #endregion //Static Methods.
    }
}
