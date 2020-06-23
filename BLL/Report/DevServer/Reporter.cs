using DotNet4.Utilities.UtilCode;
using DotNet4.Utilities.UtilReg;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace DevServer
{
	public class Reporter : IDisposable
	{
		private readonly HttpClient http;

		public static string Host { get; set; } = "https://serfend.top";
		public static string LogPath { get; set; } = "/log/report";
		public static string UserName { get; set; } = "PC";
		private string Uid { get; set; } = new Reg().In("Setting").GetInfo("uid", HttpUtil.UfUID);

		public Reporter()
		{
			http = new HttpClient();
		}

		public void Report(string host = null, string logPath = null, Report report = null)
		{
			if (host == null) host = Host;
			if (!host.StartsWith("http")) host = $"http://{host}";
			if (logPath == null) logPath = LogPath;
			ReportRaw item = report ?? new DevServer.Report();
			if (report.Device == null || report.Device.Length == 0) report.Device = Uid;
			if (report.UserName == null || report.UserName.Length == 0) report.UserName = UserName;
			var str = JsonConvert.SerializeObject(item);

			using var http = new HttpClient();
			HttpContent content = new StringContent(str);
			content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
			content.Headers.Add("Device", report.Device);
			var res = http.PostAsync($"{host}{logPath}", content).Result;
			Console.WriteLine(res.Content.ReadAsStringAsync().Result);
		}

		public void Dispose()
		{
			((IDisposable)http).Dispose();
		}
	}
}