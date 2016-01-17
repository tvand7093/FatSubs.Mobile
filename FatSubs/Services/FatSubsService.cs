using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FatSubs.Data;
using System.Net.Http.Headers;
using System.IO;

namespace FatSubs.Services
{
	public class FatSubsService
	{
		const string ApiUrl = "https://fat-subs.azurewebsites.net/api";

		public async Task<BusinessViewModel> GetDetailsAsync() {
			const string businessUrl = "Details";

			using (var client = new HttpClient ()) {
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var apiEndpoint = Path.Combine (ApiUrl, businessUrl);

				var json = await client.GetStringAsync(apiEndpoint);
				return JsonConvert.DeserializeObject<BusinessViewModel> (json);
			}
		}
	}
}

