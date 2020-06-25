using DevServer;
using DotNet4.Utilities.UtilReg;
using Newtonsoft.Json;
using Project.Core.Protector.BLL.Network;
using Project.Core.Protector.BLL.Network.NetworkChangedDetector;
using Project.Core.Protector.BLL.Network.PingDetector;
using Project.Core.Protector.BLL.Record;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project.Core.Protector
{
	public partial class FrmMain : Form
	{
		private static readonly string outerHost = "serfend.top";
		private static readonly string outerIp = "39.97.229.104";
		private static readonly string innerHost = "192.168.8.8";
		private readonly Reporter reporter = new Reporter();
		private readonly BLL.Record.PingSuccessRecord pingSuccessRecord = new BLL.Record.PingSuccessRecord();
		private readonly NetworkChangeDetector networkChangeDetector = new NetworkChangeDetector(outerIp, innerHost);
		public Reg Setting = new Reg().In("setting");
		private bool isOuterConnected = false;

		private bool IsOuterConnected
		{
			get => isOuterConnected; set
			{
				isOuterConnected = value;
				pingSuccessRecord.Enabled = value;
			}
		}

		protected override void OnClosed(EventArgs e)
		{
			reporter.Dispose();
			pingSuccessRecord.Dispose();
			base.OnClosed(e);
		}

		public FrmMain()
		{
			InitializeComponent();
			networkChangeDetector.OnNetWorkChange += NetworkChangeDetector_OnNetWorkChange;
		}

		private void NetworkChangeDetector_OnNetWorkChange(object sender, PingProtector.BLL.Network.NetworkChangedDetector.NetworkChangeArgs e)
		{
			var s = e.Status;
			Console.WriteLine(JsonConvert.SerializeObject(s));
			if (s.Type != PingProtector.BLL.Network.NetworkChangedDetector.NetType.Internet || s.Log <= 0) return;
			this.Invoke((EventHandler)delegate
			{
				var r = new DAL.Record.Record()
				{
					Create = DateTime.Now,
					TargetIp = s.IPAddress?.ToString()
				};
				var info = $"{r.TargetIp}@{s.Log}ms";
				this.OpInfo.Items.Insert(0, new ListViewItem(new string[] { r.Create.ToString(), info }));
				var successOuter = s.IPAddress.ToString() == outerIp;
				if (successOuter != IsOuterConnected)
				{
					pingSuccessRecord.SaveRecord(r);
					reporter.Report(r.TargetIp, null, new Report()
					{
						UserName = "#SafeChecker#",
						Message = GetComputerInfo(),
						Rank = ActionRank.Disaster
					});
				}
				if (successOuter && IsOuterConnected) return;
				IsOuterConnected = successOuter; // if connect to outer,begin record

				MessageBox.Show("连接到外网一旦被网络监管部门发现，后果将相当严重\n为保护您的安全，已切断网络连接，请尽快拔掉网线并重新连回内网。", "连接外网警告", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
			});
		}

		private string GetComputerInfo()
		{
			string r = Environment.MachineName;
			try
			{
				r = JsonConvert.SerializeObject(new
				{
					MachineName = Environment.MachineName,
					UserName = Environment.UserName,
					OsVersion = Environment.OSVersion.VersionString,
					Version = Environment.Version.ToString(),
					TicketCount = Environment.TickCount64
				});
			}
			catch (Exception)
			{
			}
			return r;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
		}
	}
}