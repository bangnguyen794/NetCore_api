using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ApiCore_facebook.Library
{
    /// <summary>
    /// Thư viện gọi api HttpClient -get - post - put - delete;
    /// </summary>
    public class httpClientCall
    {
        public static async Task<string>  Get(string url)
        {
            using (var httpClient = new HttpClient(new WinHttpHandler() { WindowsProxyUsePolicy = WindowsProxyUsePolicy.UseWinInetProxy }))
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await httpClient.GetAsync(url);
                string jsonResult = await response.Content.ReadAsStringAsync();
                return jsonResult;
            }
        }
    }
}
