using CompassMobileModels;
using CompassMobileUpdate.Helpers.Exceptions;
using CompassMobileUpdate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CompassMobileUpdate.Helpers
{
    public static class MeterHelper
    {
        public static string GetDeviceUtilityIDAndCustomerName(Meter meter)
        {

            string returnString = meter.DeviceUtilityID;
            if (!string.IsNullOrWhiteSpace(meter.CustomerName))
            {
                returnString += " - " + meter.CustomerName;
            }

            return returnString;

        }
        public static string GetDeviceUtilityIDAndCustomerName(LocalMeter meter)
        {

            return GetDeviceUtilityIDAndCustomerName(GetMeterFromLocalMeter(meter));

        }

        public static string GetCustomerNameAndDeviceUtilityID(Meter meter)
        {

            string returnString = meter.DeviceUtilityID;
            if (!string.IsNullOrWhiteSpace(meter.CustomerName))
            {
                returnString = meter.CustomerName + " - " + meter.DeviceUtilityID;
            }

            return returnString;

        }
        public static string GetCustomerNameAndDeviceUtilityID(LocalMeter meter)
        {

            return GetCustomerNameAndDeviceUtilityID(GetMeterFromLocalMeter(meter));

        }

        public static string GetDeviceUtilityIDAndDistance(Meter meter)
        {
            string returnValue = string.Empty;
            if (!string.IsNullOrWhiteSpace(meter.DeviceUtilityID))
            {
                returnValue = meter.DeviceUtilityID;
                if (meter.Distance.HasValue)
                {
                    returnValue += " - " + GetDistanceInFeetFormatted(meter);
                }
            }

            return returnValue;
        }
        public static string GetDeviceUtilityIDAndDistance(LocalMeter meter)
        {
            return GetDeviceUtilityIDAndDistance(GetMeterFromLocalMeter(meter));
        }

        public static double? GetDistanceInFeet(Meter meter)
        {
            return Convert.ToDouble(Math.Round(Convert.ToDecimal(5280 * meter.Distance)));
        }
        public static double? GetDistanceInFeet(LocalMeter meter)
        {
            return GetDistanceInFeet(GetMeterFromLocalMeter(meter));
        }

        public static string GetDistanceInFeetFormatted(Meter meter)
        {
            return GetDistanceInFeet(meter).ToString() + " ft";
        }

        public static string GetDistanceInFeetFormatted(LocalMeter meter)
        {
            return GetDistanceInFeetFormatted(GetMeterFromLocalMeter(meter));
        }

        public static string GetDistanceAndCustomerAddress(Meter meter)
        {
            string returnValue = string.Empty;
            if (meter.Distance.HasValue)
            {
                returnValue += GetDistanceInFeetFormatted(meter);
                if (!string.IsNullOrWhiteSpace(meter.CustomerAddress))
                {
                    returnValue += " - " + meter.CustomerAddress;
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(meter.CustomerAddress))
                {
                    returnValue += meter.CustomerAddress;
                }
            }

            return returnValue;
        }
        public static string GetDistanceAndCustomerAddress(LocalMeter meter)
        {
            return GetDistanceAndCustomerAddress(GetMeterFromLocalMeter(meter));
        }

        public static string GetDeviceUtilityIDAndAddress(Meter meter)
        {
            return meter.DeviceUtilityID + " - " + meter.CustomerAddress;
        }
        public static string GetDeviceUtilityIDAndAddress(LocalMeter meter)
        {
            return GetDeviceUtilityIDAndAddress(GetMeterFromLocalMeter(meter));
        }

        public static string GetCustomerContactNumberFormatted(Meter meter)
        {
            string temp;
            if (AppHelper.TryFormatPhoneNumberFromDigits(meter.CustomerContactNumber, out temp))
            {
                return temp;
            }
            else
            {
                return meter.CustomerContactNumber;
            }
        }
        public static string GetCustomerContactNumberFormatted(LocalMeter meter)
        {
            return GetCustomerContactNumberFormatted(GetMeterFromLocalMeter(meter));
        }

        public static Meter GetMeterFromLocalMeter(LocalMeter localMeter)
        {
            Meter meter = new Meter();
            meter.DeviceUtilityID = localMeter.DeviceUtilityID;
            meter.CustomerName = localMeter.CustomerName;
            meter.CustomerAddress = localMeter.CustomerAddress;
            meter.CustomerContactNumber = localMeter.CustomerContactNumber;
            meter.DeviceUtilityIDWithLocation = localMeter.DeviceUtilityIDWithLocation;
            meter.Distance = localMeter.Distance;

            return meter;
        }

        public static bool IsPingable(Meter meter)
        {
            if (string.IsNullOrWhiteSpace(meter.Status))
                return false;
            else
            {
                string status = meter.Status.ToLower();
                if (status == "installed"
                   || status == "unreachable"
                   || status == "service disconnect"
                   || status == "new"
                   || status == "maintenance"
                   || status == "inactive"
                   || status == "investigate"
                   || status == "retired")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }//End we have a value for Status
        }

        public static bool AllowPQRs(Meter meter)
        {
            if (!string.IsNullOrWhiteSpace(meter.TypeName))
            {
                if (AppVariables.DisallowedPQRSubTypeNames.Contains(meter.TypeName.ToLower()))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        public static bool IsValidSerialNumber(string meterSerialNumber, out MeterSerialNumberFormatException exception)
        {
            int temp;
            exception = null;

            if (meterSerialNumber.Length != 10)
            {
                exception = new MeterSerialNumberFormatException("Meter ID should have the Manufacturer Letter followed by 9 digits");
                return false;
            }

            if (int.TryParse(meterSerialNumber, out temp))
            {
                exception = new MeterSerialNumberFormatException("Meter number needs to be preceded with the Manufacturer letter");
            }
            else
            {
                Regex regEx = new Regex("[a-zA-Z]"); //any character a to z or A to Z
                if (regEx.IsMatch(meterSerialNumber.Substring(0, 1)))
                {
                    string manufacturerLetter = meterSerialNumber[0].ToString();
                    string tempMeterNumber = meterSerialNumber.Substring(1, meterSerialNumber.Length - 1);
                    if (!int.TryParse(tempMeterNumber, out temp))
                    {
                        exception = new MeterSerialNumberFormatException("Meter ID should have the Manufacturer Letter followed by 9 digits");
                    }
                    else
                    {
                        exception = null;
                    }
                }
                else
                {
                    exception = new MeterSerialNumberFormatException("Meter number needs to be preceded with the Manufacturer letter");
                }
            }

            if (exception == null)
                return true;
            else
                return false;
        }

        public static bool? HasOverlappingVoltage(Meter meter)
        {
            List<LocalVoltageRule> possibleRanges = (from voltageRule
                                                    in AppVariables.VoltageRules
                                                     where (
                                                            voltageRule.MeterForm.Equals(meter.Form, StringComparison.InvariantCultureIgnoreCase)
                                                            && voltageRule.MeterType.Equals(meter.Type, StringComparison.InvariantCultureIgnoreCase)
                                                            && voltageRule.OverlappingVoltages == true
                                                            )
                                                     select voltageRule).ToList();

            if (possibleRanges.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool? IsVoltageInRange(Meter meter, decimal? voltage)
        {
            if (!voltage.HasValue)
                return null;

            // we currently don't have targetVoltage or customerClass within the app at the right time,
            // so we're ignoring it for now, but leaving it in the function signature
            bool result = false;

            List<LocalVoltageRule> possibleRanges = (from voltageRule
                                                    in AppVariables.VoltageRules
                                                     where (
                                                            voltageRule.MeterForm.Equals(meter.Form, StringComparison.InvariantCultureIgnoreCase)
                                                            && voltageRule.MeterType.Equals(meter.Type, StringComparison.InvariantCultureIgnoreCase)
                                                            )
                                                     select voltageRule).ToList();

            if (possibleRanges.Count > 0)
            {
                foreach (LocalVoltageRule currentRange in possibleRanges)
                {
                    decimal residentialLowRange = currentRange.ResidentialVoltageLow;
                    decimal residentialHighRange = currentRange.ResidentialVoltageHigh;
                    decimal commercialLowRange = currentRange.CommercialVoltageLow;
                    decimal commercialHighRange = currentRange.CommercialVoltageHigh;

                    if (meter.CustomerClassType == CustomerClassEnum.Unknown)
                    {
                        // check both ranges
                        if (((residentialLowRange < voltage) && (voltage < residentialHighRange)) || ((commercialLowRange < voltage) && (voltage < commercialHighRange)))
                        {
                            result = true;
                            break;
                        }
                    }
                    else if (meter.CustomerClassType == CustomerClassEnum.Residential)
                    {
                        // uh....check residential only
                        if ((residentialLowRange < voltage) && (voltage < residentialHighRange))
                        {
                            result = true;
                            break;
                        }
                    }
                    else if (meter.CustomerClassType == CustomerClassEnum.Commercial)
                    {
                        // uh....you guessed it, check check commercial only
                        if ((commercialLowRange < voltage) && (voltage < commercialHighRange))
                        {
                            result = true;
                            break;
                        }
                    }
                    else
                    {
                        // if we get here, a programmer added a value to the CustomerClass enum and didn't handle it here
                        throw new Exception("Unexpected CustomerClass enum value '" + meter.CustomerClassType.ToString() + "'.  Programmer Error.");
                    }
                }
            }
            else
            {
                int count = AppVariables.LocalAppSql.GetVoltageRules().Result.Count;
                string message = string.Format("No Voltage Rules found for Meter_Type = {0} And Meter_Form = {1}. There are currently {2} rows of Voltage Rules in this mobile device's memory", meter.Type, meter.Form, count);
                AppVariables.AppService.LogApplicationError("MeterHelper.IsVoltageInRange", new Exception(message));
                return null;
            }

            return result;
        }


    }
}
