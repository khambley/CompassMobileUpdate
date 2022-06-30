using CompassMobileUpdate.Helpers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompassMobileUpdate.DataAccess
{
    public static class CompassMobileRestClientExtensions
    {
        public static async Task<T> CompassExecuteAsync<T>(RestClient client, RestRequest request, [CallerMemberName] string callingMethod = "")
        {
            bool isNullResponseException = false;
            var tcs = new TaskCompletionSource<T>();

            var response = await client.ExecuteAsync<T>(request);

            //TODO: Add HandleResponseHeaders(response.Headers) method using new RestSharp library.

            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (response.Data == null)
                {
                    tcs.TrySetException(new NullResponseException());
                    isNullResponseException = true;
                }
                else
                {
                    tcs.SetResult(response.Data);
                }
            }
            else
            {
                Exception ex = AppHelper.GetExceptionFromResponseStatusCode(callingMethod, response);

                tcs.TrySetException(ex);
            }

            if (tcs.Task.Exception != null)
            {
                if (!isNullResponseException)
                {
                    AppLogger.LogErrorLocallyOnly(tcs.Task.Exception);
                }
                throw tcs.Task.Exception;
            }
            else
            {
                return await tcs.Task;
            }

        }
        public static async Task<T> CompassExecuteTaskAsync<T>(RestClient client, RestRequest request, CancellationToken token, [CallerMemberName] string callingMethod = "")
        {
            bool isNullResponseException = false;
            var tcs = new TaskCompletionSource<T>();

            var response = await client.ExecuteAsync<T>(request, token);

            //TODO: Add HandleResponseHeaders(response.Headers) method using new RestSharp library.

            if(response.StatusCode == HttpStatusCode.OK)
            {
                if(response.Data == null)
                {
                    tcs.TrySetException(new NullResponseException());
                }
                else
                {
                    tcs.SetResult(response.Data);
                }
            }
            else
            {
                Exception ex = AppHelper.GetExceptionFromResponseStatusCode(callingMethod, response);

                tcs.TrySetException(ex);
            }

            if(tcs.Task.Exception != null)
            {
                if (!isNullResponseException)
                {
                    AppLogger.LogErrorLocallyOnly(tcs.Task.Exception);
                }

                throw tcs.Task.Exception;
            }
            else
            {
                return tcs.Task.Result;
            }
        }
    }
}
