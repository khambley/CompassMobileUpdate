using CompassMobileModels;
using CompassMobileUpdate.DataAccess;
using CompassMobileUpdate.Helpers.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace CompassMobileUpdate.Helpers
{
    class AppHelper
    {
        public static BoundingCoordinates GetBoundingCoords(MapSpan visibleRegion)
        {

            var center = visibleRegion.Center;
            var halfHeightDegrees = visibleRegion.LatitudeDegrees / 2;
            var halfWidthDegrees = visibleRegion.LongitudeDegrees / 2;

            var left = center.Longitude - halfWidthDegrees;
            var right = center.Longitude + halfWidthDegrees;
            var top = center.Latitude + halfHeightDegrees;
            var bottom = center.Latitude - halfHeightDegrees;

            // Adjust for Internation Date Line (+/- 180 degrees longitude)
            if (left < -180) left = 180 + (180 + left);
            if (right > 180) right = (right - 180) - 180;

            return new BoundingCoordinates(left, top, right, bottom);
        }
        public static async Task FadeOutLabelByEmptyString(Label lbl, int delayInMs)
        {

            if (delayInMs <= 0)
            {
                lbl.Text = string.Empty;
            }
            else
            {
                int fadeLength = 750;

                if (delayInMs > fadeLength)
                {
                    int diff = delayInMs - fadeLength;

                    await Task.Delay(diff);
                    await lbl.FadeTo(0, Convert.ToUInt32(fadeLength), null);
                }
                else
                {
                    await lbl.FadeTo(0, Convert.ToUInt32(delayInMs), null);
                }
                lbl.Text = string.Empty;
                lbl.Opacity = 1;
            }
        }


        public static async void FadeOutLabelByEmptyString(Label lbl)
        {
            int delayInMs = AppVariables.DefaultFadeMs;

            if (delayInMs <= 0)
            {
                lbl.Text = string.Empty;
            }
            else
            {
                int fadeLength = 750;

                if (delayInMs > fadeLength)
                {
                    int diff = delayInMs - fadeLength;

                    await Task.Delay(diff);
                    await lbl.FadeTo(0, Convert.ToUInt32(fadeLength), null);
                }
                else
                {
                    await lbl.FadeTo(0, Convert.ToUInt32(delayInMs), null);
                }
                lbl.Text = string.Empty;
                lbl.Opacity = 1;
            }
        }

        public static async void FadeOutLabelByNotVisible(Label lbl, int delayInMs)
        {

            if (delayInMs <= 0)
            {
                lbl.IsVisible = false;
            }
            else
            {
                int fadeLength = 750;

                if (delayInMs > fadeLength)
                {
                    int diff = delayInMs - fadeLength;

                    await Task.Delay(diff);
                    await lbl.FadeTo(0, Convert.ToUInt32(fadeLength), null);
                }
                else
                {
                    await lbl.FadeTo(0, Convert.ToUInt32(delayInMs), null);
                }

                lbl.IsVisible = false;
                lbl.Opacity = 1;
            }
        }

        public static async void FadeOutLabelByNotVisible(Label lbl)
        {
            int delayInMs = AppVariables.DefaultFadeMs;

            if (delayInMs <= 0)
            {
                lbl.IsVisible = false;
            }
            else
            {
                int fadeLength = 750;

                if (delayInMs > fadeLength)
                {
                    int diff = delayInMs - fadeLength;

                    await Task.Delay(diff);
                    await lbl.FadeTo(0, Convert.ToUInt32(fadeLength), null);
                }
                else
                {
                    await lbl.FadeTo(0, Convert.ToUInt32(delayInMs), null);
                }

                lbl.IsVisible = false;
                lbl.Opacity = 1;
            }
        }

        public static async void FadeInLabel(Label lbl, int delayInMs)
        {
            lbl.Opacity = 0;
            lbl.IsVisible = true;

            if (delayInMs <= 0)
            {
                lbl.Opacity = 1;
            }
            else
            {
                int fadeLength = 750;

                if (delayInMs > fadeLength)
                {
                    int diff = delayInMs - fadeLength;

                    await Task.Delay(diff);
                    await lbl.FadeTo(1, Convert.ToUInt32(fadeLength), null);
                }
                else
                {
                    await lbl.FadeTo(1, Convert.ToUInt32(delayInMs), null);
                }


            }
        }

        public static void FadeInLabel(Label lbl)
        {
            FadeInLabel(lbl, AppVariables.DefaultFadeMs);
        }

        public static string stripNonDigits(string textToStrip)
        {
            if (string.IsNullOrWhiteSpace(textToStrip))
            {
                return textToStrip;
            }
            else
            {
                for (int i = 0; i < textToStrip.Length; i++)
                {
                    int temp;
                    string character = textToStrip.Substring(i, 1);
                    if (!int.TryParse(character, out temp))
                    {
                        textToStrip = textToStrip.Remove(i, 1);
                        i--;
                    }
                }

                return textToStrip;
            }
        }

        /// <summary>
        /// Expects a list of digits only.
        /// It will then Format the number like xxx-xxxx or x-xxx-xxxx
        /// where the delimeter is by default a hyphen ("-") or you can pass your own
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryFormatPhoneNumberFromDigits(string phoneNumber, out string result, char delimiter = '-')
        {
            long number;

            //if the phone number is all numbers
            if (long.TryParse(phoneNumber, out number))
            {
                if (phoneNumber.Length == 10 || phoneNumber.Length == 11)
                {
                    //lets add some "-" for readability
                    if (phoneNumber.Length == 10)
                    {
                        result = phoneNumber.Insert(3, delimiter.ToString());
                        result = result.Insert(7, delimiter.ToString());
                    }
                    else if (phoneNumber.Length == 11)
                    {
                        result = phoneNumber.Insert(1, delimiter.ToString());
                        result = result.Insert(5, delimiter.ToString());
                        result = result.Insert(9, delimiter.ToString());
                    }
                    else
                        result = null;
                }
                else
                    result = null;

                return true;
            }

            result = null;
            return false;
        }

        public static Exception GetExceptionFromResponseStatusCode(string source, RestSharp.RestResponse response)
        {
            return new Exception(source + ": " + response.StatusDescription, response.ErrorException);
        }



        public static bool ContainsNullResponseException(Exception ex)
        {
            Exception e = ex;

            while (e != null)
            {
                if (e.GetType() == typeof(NullResponseException))
                {
                    return true;
                }

                e = e.InnerException;
            }

            return false;
        }

        public static bool ContainsApplicationMaintenance(Exception ex)
        {
            Exception e = ex;

            while (e != null)
            {
                if (e.GetType() == typeof(ApplicationMaintenanceException))
                {
                    return true;
                }

                e = e.InnerException;
            }

            return false;
        }
        public static bool ContainsAuthenticationRequiredException(Exception ex)
        {
            Exception e = ex;

            while (e != null)
            {
                if (e.GetType() == typeof(AuthenticationRequiredException))
                {
                    return true;
                }

                e = e.InnerException;
            }

            return false;

        }

        public static string GetInnerMostExceptionMessage(Exception ex)
        {
            string message = string.Empty;
            while (ex != null)
            {
                message = ex.Message;
                ex = ex.InnerException;
            }

            return message;
        }

        public static string GetAmalgamatedExceptionMessage(Exception ex)
        {
            string message = string.Empty;
            bool firstMessage = true;
            while (ex != null)
            {
                if (firstMessage)
                {
                    message = ex.Message;
                    firstMessage = false;
                }
                else
                {
                    message += Environment.NewLine + ex.Message;
                }

                ex = ex.InnerException;

            }

            return message;
        }

        public static DateTimeOffset GetConfiguredTimeZone(DateTimeOffset dto)
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dto, AppVariables.TimeZoneID);
        }
    }
}
