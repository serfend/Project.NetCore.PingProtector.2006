using DotNet4.Utilities.UtilReg;
using Newtonsoft.Json;
using System;

namespace Project.Core.Protector.BLL.Record
{
	/// <summary>
	/// while success ping,record on reg
	/// </summary>
	public class PingSuccessRecord : IDisposable
	{
		/// <summary>
		/// whether do record to reg
		/// </summary>
		public bool Enabled { get; set; } = true;

		public static Reg Record { get; } = new Reg().In("Record");
		public static int Length { get; set; } = -1;

		public PingSuccessRecord()
		{
			if (Length == -1) Length = Convert.ToInt32(Record.GetInfo("length", "0"));
		}

		public void SaveRecord(DAL.Record.Record record)
		{
			var str = JsonConvert.SerializeObject(record);
			Console.WriteLine(str);
			if (!Enabled) return;
			Record.SetInfo(Length++.ToString(), str);
		}

		public DAL.Record.Record GetRecord(int index)
		{
			var str = Record.GetInfo(index.ToString());
			return str == null ? null : JsonConvert.DeserializeObject<DAL.Record.Record>(str);
		}

		public void Dispose()
		{
			Record.SetInfo("length", Length.ToString());
		}
	}
}