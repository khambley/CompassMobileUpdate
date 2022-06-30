using Acr.UserDialogs;
using CompassMobileModels;
using CompassMobileUpdate.DataAccess;
using CompassMobileUpdate.Helpers;
using CompassMobileUpdate.Models;
using RestSharp;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CompassMobileUpdate.Services
{
    using VoltageRule = LocalVoltageRule;
    public class AppService
    {
        private string _devBaseUrl = "https://compassmobiledev.azurewebsites.net/api/";
        private string _compassMobileAPIBaseURI;
        public AppService()
        {
            //TODO: may need to add validation for SSL callback ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            string uri = AppVariables.URI_COMPASS_Mobile_API_BaseURI;

            if (Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                _compassMobileAPIBaseURI = uri;
            else
                throw new UriFormatException("COMPASS_Mobile_GetMetersWithinXRadius URI is badly formed");
        }
        public async Task<AuthResponse> Authenticate(AuthRequest currentUser, CancellationToken token)
        {
            var client = new RestClient(_compassMobileAPIBaseURI);
            var request = new CompassMobileRestRequest("Authentication/", Method.Post);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(currentUser);
            var result = await CompassMobileRestClientExtensions.CompassExecuteTaskAsync<AuthResponse>(client, request, token);
            return result;
        }



        //TODO: From AppVariables L77: may need to add headers here. The old way is below, new way is here, https://restsharp.dev/v107/#headers
        //AuthorizationHeaderParameter = new RestSharp.Parameter() 
        //    {
        //        Type = RestSharp.ParameterType.HttpHeader,
        //        Name = "WWW-Authenticate",
        //        Value = "Required"
        //    };
        //ExtendedJWTHeaderParameter = new RestSharp.Parameter()
        //    {
        //        Type = RestSharp.ParameterType.HttpHeader,
        //        Name = "X-EXTENDED-JWT"
        //    };
        //// MAJ 12/18/2017 BEGIN
        //CompassMaintenanceParameter = new RestSharp.Parameter()
        //{
        //    Type = RestSharp.ParameterType.HttpHeader,
        //    Name = "compass-maintenance"
        //};
        public async Task ShowMessage(string header, string message)
        {
            var config = new AlertConfig()
            {
                Title = header,
                Message = message,
                OkText = "OK"
            };

            await UserDialogs.Instance.AlertAsync(config);
        }
        public async void LogApplicationError(string source, Exception ex)
        {
            try
            {
                string userID = "Unknown";
                if (AppVariables.User != null)
                {
                    userID = AppVariables.User.UserID;
                }

                string appendedMessage = string.Empty;
                try
                {
                    //Xamarin.Insights.Report(ex);
                    //TODO: Implement App Center for Insights (Analytics and Crash Reports) Xamarin.Insights no longer supported.
                    
                }
                catch (Exception ex1)
                {
                    appendedMessage = " Failed to write log to Xamarin insights: " + AppHelper.GetAmalgamatedExceptionMessage(ex1);
                }

                Exception current = ex;
                int count = 0;

                //Record all Exceptions and InnerExceptions
                while (current != null)
                {
                    string message = string.Empty;
                    if (count != 0)
                    {
                        message = "Inner Exception #" + count.ToString() + ": ";
                    }

                    message += current.Message;
                    message += appendedMessage;

                    string stackTrace = current.StackTrace;

                    var client = new RestClient(_compassMobileAPIBaseURI);
                    var request = new CompassMobileRestRequest("Error/", Method.Post);
                    request.RequestFormat = DataFormat.Json;
                    request.AddBody(new ApplicationError { ErrorType = ApplicationErrorType.Mobile, Source = source, Message = message, StackTrace = stackTrace, UserID = userID });

                    var taskCompletionSource = new TaskCompletionSource<int>();

                    try
                    {
                        await CompassMobileRestClientExtensions.CompassExecuteAsync<int>(client, request);
                    }
                    catch (Exception e)
                    {
                        //Xamarin.Insights.Report(e);
                        //TODO: Implement App Center for Insights (Analytics and Crash Reports) Xamarin.Insights no longer supported.
                    }
                    finally
                    {
                        current = current.InnerException;
                        count++;
                    }
                }
            }
            catch (Exception ex1)
            {
                AppLogger.LogErrorLocallyOnly(ex1);
            }

        }
    }
}
