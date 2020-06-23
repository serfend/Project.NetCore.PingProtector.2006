using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Core.Protector.BLL.Network
{
	public class PingDetector : IDisposable
	{
		private readonly PingOptions options;
		private readonly byte[] buffer = Encoding.ASCII.GetBytes("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");

		/// <summary>
		/// instance of <see cref="System.Net.NetworkInformation.Ping"/>
		/// </summary>
		public Ping Ping { get; set; }

		/// <summary>
		/// host to ping
		/// </summary>
		public string Host { get; set; }

		/// <summary>
		/// if set , it would raise event while ping success
		/// </summary>
		public int CheckInterval
		{
			set
			{
				if (value <= 0)
				{
					timer.Enabled = false;
					return;
				}
				timer.Interval = value;
				timer.Enabled = true;
			}
		}

		public event PingSuccess OnPingReply;

		public delegate void PingSuccess(PingReply status);

		public PingDetector(PingOptions options = null, string host = null)
		{
			Ping = new Ping();
			if (options == null) options = new PingOptions
			{
				// Use the default Ttl value which is 128,
				// but change the fragmentation behavior.
				DontFragment = true
			};
			this.options = options;
			Host = host;
			timer = new System.Windows.Forms.Timer()
			{
				Enabled = false,
			};
			timer.Tick += Timer_Tick;
		}

		private void Timer_Tick(object sender, EventArgs e)
		{
			Check();
		}

		public System.Windows.Forms.Timer timer;

		public PingReply Check(string host = null, int timeout = 3000)
		{
			if (host == null) host = Host;
			var result = Ping.Send(host, timeout, buffer, options);
			if (result.Status == IPStatus.Success) OnPingReply(result);
			return result;
		}

		public void Dispose()
		{
			((IDisposable)Ping).Dispose();
		}
	}
}