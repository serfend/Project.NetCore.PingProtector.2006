using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace Project.Core.Protector.BLL.Network.PingDetector
{
	public class PingSuccessEventArgs : EventArgs
	{
		public PingSuccessEventArgs(PingReply reply)
		{
			Reply = reply;
		}

		public PingReply Reply { get; }
	}
}