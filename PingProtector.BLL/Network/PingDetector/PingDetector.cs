using System;
using System.Net.NetworkInformation;
using System.Text;

namespace Project.Core.Protector.BLL.Network.PingDetector
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
		public double CheckInterval
		{
			get => timer.Interval;
			set
			{
				timer.Interval = value;
				if (!timer.Enabled && value >= 0) timer.Start();
				else if (value <= 0) timer.Stop();
			}
		}

		public event EventHandler<PingSuccessEventArgs> OnPingReply;

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
			timer = new System.Timers.Timer()
			{
				Enabled = false,
			};
			timer.Elapsed += Timer_Elapsed; ;
		}

		private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			Check();
		}

		public System.Timers.Timer timer;

		public PingReply Check(string host = null, int timeout = 3000)
		{
			if (host == null) host = Host;
			var result = Ping.Send(host, timeout, buffer, options);
			if (result.Status == IPStatus.Success) OnPingReply?.Invoke(this, new PingSuccessEventArgs(result));
			return result;
		}

		public void Dispose()
		{
			((IDisposable)Ping).Dispose();
		}
	}
}