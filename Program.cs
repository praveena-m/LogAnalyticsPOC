using LogAnalytics.Client;
using System;
using System.Threading.Tasks;

namespace LogAnalyticsPOC
{
	class Program
	{
		 static async Task Main(string[] args)
		{
			LogAnalyticsClient logger = new LogAnalyticsClient(
				workspaceId: "v66652c9-39ae-487c-8099-917bb5a97df0",
				sharedKey: "APbh42OKxuqwmtmDMkwYKLtyUkC3SdNmt9Y83Niduf/NjuIeJ53LBh4dHtxZUfw+j8qGug13r7Y6y74gfOLMPAQ==");

			await logger.SendLogEntry(new LogEntity
			{
				Category = "Cheggout",
				UserId = "Admin",
				Request = "{\"request\":\"testing\"}",
				Response = "{\"response\":\"testing\"}",
				StatusCode = 200,

			}, "CheggoutAPILogs").ConfigureAwait(false);
		}
	}

	public class LogEntity
	{
		public string Category { get; set; }
		public string UserId { get; set; }
		public string Request { get; set; }
		public string Response { get; set; }
		public int StatusCode { get; set; }

	}
}
