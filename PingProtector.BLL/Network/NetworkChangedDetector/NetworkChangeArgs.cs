﻿using System;

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