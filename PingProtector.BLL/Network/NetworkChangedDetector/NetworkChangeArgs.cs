using NETworkManager.Models.Network;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace PingProtector.BLL.Network.NetworkChangedDetector
{
	public class NetworkChangeArgs : EventArgs
	{
		public NetworkChangeArgs(NetStatus status)
		{
			Status = status;
		}

		public NetStatus Status { get; private set; }
	}
}