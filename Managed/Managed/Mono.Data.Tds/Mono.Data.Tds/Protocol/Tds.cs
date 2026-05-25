using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Text;
using Mono.Security.Protocol.Ntlm;

namespace Mono.Data.Tds.Protocol
{
	// Token: 0x0200000A RID: 10
	public abstract class Tds
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00004728 File Offset: 0x00002928
		public Tds(string dataSource, int port, int packetSize, int timeout, TdsVersion tdsVersion)
		{
			this.tdsVersion = tdsVersion;
			this.packetSize = packetSize;
			this.dataSource = dataSource;
			this.columns = new TdsDataColumnCollection();
			this.comm = new TdsComm(dataSource, port, packetSize, timeout, tdsVersion);
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000040 RID: 64 RVA: 0x000047CC File Offset: 0x000029CC
		// (remove) Token: 0x06000041 RID: 65 RVA: 0x000047E8 File Offset: 0x000029E8
		public event TdsInternalErrorMessageEventHandler TdsErrorMessage;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000042 RID: 66 RVA: 0x00004804 File Offset: 0x00002A04
		// (remove) Token: 0x06000043 RID: 67 RVA: 0x00004820 File Offset: 0x00002A20
		public event TdsInternalInfoMessageEventHandler TdsInfoMessage;

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000044 RID: 68 RVA: 0x0000483C File Offset: 0x00002A3C
		protected string Charset
		{
			get
			{
				return this.charset;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00004844 File Offset: 0x00002A44
		protected CultureInfo Locale
		{
			get
			{
				return this.locale;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000046 RID: 70 RVA: 0x0000484C File Offset: 0x00002A4C
		public bool DoneProc
		{
			get
			{
				return this.doneProc;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00004854 File Offset: 0x00002A54
		protected string Language
		{
			get
			{
				return this.language;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000048 RID: 72 RVA: 0x0000485C File Offset: 0x00002A5C
		protected ArrayList ColumnNames
		{
			get
			{
				return this.columnNames;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00004864 File Offset: 0x00002A64
		public TdsDataRow ColumnValues
		{
			get
			{
				return this.currentRow;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600004A RID: 74 RVA: 0x0000486C File Offset: 0x00002A6C
		internal TdsComm Comm
		{
			get
			{
				return this.comm;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00004874 File Offset: 0x00002A74
		public string Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004C RID: 76 RVA: 0x0000487C File Offset: 0x00002A7C
		public string DataSource
		{
			get
			{
				return this.dataSource;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00004884 File Offset: 0x00002A84
		// (set) Token: 0x0600004E RID: 78 RVA: 0x000048B8 File Offset: 0x00002AB8
		public bool IsConnected
		{
			get
			{
				return this.connected && this.comm != null && this.comm.IsConnected();
			}
			set
			{
				this.connected = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000048C4 File Offset: 0x00002AC4
		// (set) Token: 0x06000050 RID: 80 RVA: 0x000048CC File Offset: 0x00002ACC
		public bool Pooling
		{
			get
			{
				return this.pooling;
			}
			set
			{
				this.pooling = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000048D8 File Offset: 0x00002AD8
		// (set) Token: 0x06000052 RID: 82 RVA: 0x000048E0 File Offset: 0x00002AE0
		public bool MoreResults
		{
			get
			{
				return this.moreResults;
			}
			set
			{
				this.moreResults = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000048EC File Offset: 0x00002AEC
		public int PacketSize
		{
			get
			{
				return this.packetSize;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000048F4 File Offset: 0x00002AF4
		// (set) Token: 0x06000055 RID: 85 RVA: 0x000048FC File Offset: 0x00002AFC
		public int RecordsAffected
		{
			get
			{
				return this.recordsAffected;
			}
			set
			{
				this.recordsAffected = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00004908 File Offset: 0x00002B08
		public string ServerVersion
		{
			get
			{
				return this.databaseProductVersion;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00004910 File Offset: 0x00002B10
		public TdsDataColumnCollection Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00004918 File Offset: 0x00002B18
		public TdsVersion TdsVersion
		{
			get
			{
				return this.tdsVersion;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00004920 File Offset: 0x00002B20
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00004928 File Offset: 0x00002B28
		public ArrayList OutputParameters
		{
			get
			{
				return this.outputParameters;
			}
			set
			{
				this.outputParameters = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00004934 File Offset: 0x00002B34
		// (set) Token: 0x0600005C RID: 92 RVA: 0x0000493C File Offset: 0x00002B3C
		protected TdsMetaParameterCollection Parameters
		{
			get
			{
				return this.parameters;
			}
			set
			{
				this.parameters = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00004948 File Offset: 0x00002B48
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00004950 File Offset: 0x00002B50
		public bool SequentialAccess
		{
			get
			{
				return this.sequentialAccess;
			}
			set
			{
				this.sequentialAccess = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600005F RID: 95 RVA: 0x0000495C File Offset: 0x00002B5C
		public byte[] Collation
		{
			get
			{
				return this.collation;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00004964 File Offset: 0x00002B64
		public TdsVersion ServerTdsVersion
		{
			get
			{
				switch (this.databaseMajorVersion)
				{
				case 4:
					return TdsVersion.tds42;
				case 5:
					return TdsVersion.tds50;
				case 7:
					return TdsVersion.tds70;
				case 8:
					return TdsVersion.tds80;
				case 9:
					return TdsVersion.tds90;
				case 10:
					return TdsVersion.tds100;
				}
				return this.tdsVersion;
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000049BC File Offset: 0x00002BBC
		private void SkipRow()
		{
			this.SkipToColumnIndex(this.Columns.Count);
			this.StreamLength = 0L;
			this.StreamColumnIndex = 0;
			this.StreamIndex = 0L;
			this.LoadInProgress = false;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000049F8 File Offset: 0x00002BF8
		private void SkipToColumnIndex(int colIndex)
		{
			if (this.LoadInProgress)
			{
				this.EndLoad();
			}
			if (colIndex < this.StreamColumnIndex)
			{
				throw new Exception("Cannot Skip to a colindex less than the curr index");
			}
			while (colIndex != this.StreamColumnIndex)
			{
				TdsColumnType? columnType = this.Columns[this.StreamColumnIndex].ColumnType;
				if (columnType == null)
				{
					throw new Exception("Column type unset.");
				}
				if (!(columnType == TdsColumnType.Image) && !(columnType == TdsColumnType.Text) && !(columnType == TdsColumnType.NText))
				{
					this.GetColumnValue(columnType, false, this.StreamColumnIndex);
					this.StreamColumnIndex++;
				}
				else
				{
					this.BeginLoad(columnType);
					this.Comm.Skip(this.StreamLength);
					this.StreamLength = 0L;
					this.EndLoad();
				}
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004B08 File Offset: 0x00002D08
		public object GetSequentialColumnValue(int colIndex)
		{
			if (colIndex < this.StreamColumnIndex)
			{
				throw new InvalidOperationException("Invalid attempt tp read from column ordinal" + colIndex);
			}
			if (this.LoadInProgress)
			{
				this.EndLoad();
			}
			if (colIndex != this.StreamColumnIndex)
			{
				this.SkipToColumnIndex(colIndex);
			}
			object columnValue = this.GetColumnValue(this.Columns[colIndex].ColumnType, false, colIndex);
			this.StreamColumnIndex++;
			return columnValue;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004B84 File Offset: 0x00002D84
		public long GetSequentialColumnValue(int colIndex, long fieldIndex, byte[] buffer, int bufferIndex, int size)
		{
			if (colIndex < this.StreamColumnIndex)
			{
				throw new InvalidOperationException("Invalid attempt to read from column ordinal" + colIndex);
			}
			long num;
			try
			{
				if (colIndex != this.StreamColumnIndex)
				{
					this.SkipToColumnIndex(colIndex);
				}
				if (!this.LoadInProgress)
				{
					this.BeginLoad(this.Columns[colIndex].ColumnType);
				}
				if (buffer == null)
				{
					num = this.StreamLength;
				}
				else
				{
					num = this.LoadData(fieldIndex, buffer, bufferIndex, size);
				}
			}
			catch (IOException ex)
			{
				this.connected = false;
				throw new TdsInternalException("Server closed the connection.", ex);
			}
			return num;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004C48 File Offset: 0x00002E48
		private void BeginLoad(TdsColumnType? colType)
		{
			if (this.LoadInProgress)
			{
				this.EndLoad();
			}
			this.StreamLength = 0L;
			if (colType == null)
			{
				throw new ArgumentNullException("colType");
			}
			if (colType != null)
			{
				TdsColumnType value = colType.Value;
				switch (value)
				{
				case TdsColumnType.Image:
				case TdsColumnType.Text:
					break;
				default:
					switch (value)
					{
					case TdsColumnType.Binary:
					case TdsColumnType.Char:
						goto IL_0133;
					default:
						switch (value)
						{
						case TdsColumnType.BigVarBinary:
						case TdsColumnType.BigVarChar:
							break;
						default:
							switch (value)
							{
							case TdsColumnType.BigBinary:
							case TdsColumnType.BigChar:
								break;
							default:
								if (value == TdsColumnType.NText)
								{
									goto IL_00CD;
								}
								if (value != TdsColumnType.NVarChar && value != TdsColumnType.NChar)
								{
									goto IL_014A;
								}
								goto IL_0133;
							}
							break;
						}
						this.Comm.GetTdsShort();
						this.StreamLength = (long)this.Comm.GetTdsShort();
						goto IL_0157;
					}
					break;
				case TdsColumnType.VarBinary:
				case TdsColumnType.VarChar:
					goto IL_0133;
				}
				IL_00CD:
				if (this.Comm.GetByte() != 0)
				{
					this.Comm.Skip(24L);
					this.StreamLength = (long)this.Comm.GetTdsInt();
				}
				else
				{
					this.StreamLength = -2L;
				}
				goto IL_0157;
				IL_0133:
				this.StreamLength = (long)this.Comm.GetTdsShort();
				goto IL_0157;
			}
			IL_014A:
			this.StreamLength = -1L;
			IL_0157:
			this.StreamIndex = 0L;
			this.LoadInProgress = true;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004DBC File Offset: 0x00002FBC
		private void EndLoad()
		{
			if (this.StreamLength > 0L)
			{
				this.Comm.Skip(this.StreamLength);
			}
			this.StreamLength = 0L;
			this.StreamIndex = 0L;
			this.StreamColumnIndex++;
			this.LoadInProgress = false;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00004E0C File Offset: 0x0000300C
		private long LoadData(long fieldIndex, byte[] buffer, int bufferIndex, int size)
		{
			if (this.StreamLength <= 0L)
			{
				return this.StreamLength;
			}
			if (fieldIndex < this.StreamIndex)
			{
				throw new InvalidOperationException(string.Format("Attempting to read at dataIndex '{0}' is not allowed as this is less than the current position. You must read from dataIndex '{1}' or greater.", fieldIndex, this.StreamIndex));
			}
			if (fieldIndex >= this.StreamLength + this.StreamIndex)
			{
				return 0L;
			}
			int num = (int)(fieldIndex - this.StreamIndex);
			this.Comm.Skip((long)num);
			this.StreamIndex += fieldIndex - this.StreamIndex;
			this.StreamLength -= (long)num;
			int num2 = (int)(((long)size <= this.StreamLength) ? ((long)size) : this.StreamLength);
			byte[] bytes = this.Comm.GetBytes(num2, true);
			this.StreamIndex += (long)num2 + (fieldIndex - this.StreamIndex);
			this.StreamLength -= (long)num2;
			bytes.CopyTo(buffer, bufferIndex);
			return (long)bytes.Length;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004F0C File Offset: 0x0000310C
		protected internal void InitExec()
		{
			this.moreResults = true;
			this.doneProc = false;
			this.isResultRead = false;
			this.isRowRead = false;
			this.StreamLength = 0L;
			this.StreamIndex = 0L;
			this.StreamColumnIndex = 0;
			this.LoadInProgress = false;
			this.queryInProgress = false;
			this.cancelsRequested = 0;
			this.cancelsProcessed = 0;
			this.recordsAffected = -1;
			this.messages.Clear();
			this.outputParameters.Clear();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00004F88 File Offset: 0x00003188
		public void Cancel()
		{
			if (this.queryInProgress && this.cancelsRequested == this.cancelsProcessed)
			{
				this.comm.StartPacket(TdsPacketType.Cancel);
				try
				{
					this.Comm.SendPacket();
				}
				catch (IOException ex)
				{
					this.connected = false;
					throw new TdsInternalException("Server closed the connection.", ex);
				}
				this.cancelsRequested++;
			}
		}

		// Token: 0x0600006A RID: 106
		public abstract bool Connect(TdsConnectionParameters connectionParameters);

		// Token: 0x0600006B RID: 107 RVA: 0x00005010 File Offset: 0x00003210
		public static TdsTimeoutException CreateTimeoutException(string dataSource, string method)
		{
			string text = "Timeout expired. The timeout period elapsed prior to completion of the operation or the server is not responding.";
			return new TdsTimeoutException(0, 0, text, -2, method, dataSource, "Mono TdsClient Data Provider", 0);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00005038 File Offset: 0x00003238
		public void Disconnect()
		{
			try
			{
				this.comm.StartPacket(TdsPacketType.Logoff);
				this.comm.Append(0);
				this.comm.SendPacket();
			}
			catch
			{
			}
			this.connected = false;
			this.comm.Close();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000050A4 File Offset: 0x000032A4
		public virtual bool Reset()
		{
			this.database = this.originalDatabase;
			return true;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000050B4 File Offset: 0x000032B4
		protected virtual bool IsValidRowCount(byte status, byte op)
		{
			return (status & 16) != 0;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000050C0 File Offset: 0x000032C0
		public void Execute(string sql)
		{
			this.Execute(sql, null, 0, false);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000050CC File Offset: 0x000032CC
		public void ExecProc(string sql)
		{
			this.ExecProc(sql, null, 0, false);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000050D8 File Offset: 0x000032D8
		public virtual void Execute(string sql, TdsMetaParameterCollection parameters, int timeout, bool wantResults)
		{
			this.ExecuteQuery(sql, timeout, wantResults);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000050E4 File Offset: 0x000032E4
		public virtual void ExecProc(string sql, TdsMetaParameterCollection parameters, int timeout, bool wantResults)
		{
			this.ExecuteQuery(string.Format("exec {0}", sql), timeout, wantResults);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000050FC File Offset: 0x000032FC
		public virtual void ExecPrepared(string sql, TdsMetaParameterCollection parameters, int timeout, bool wantResults)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00005104 File Offset: 0x00003304
		internal void ExecBulkCopyMetaData(int timeout, bool wantResults)
		{
			this.moreResults = true;
			try
			{
				this.Comm.SendPacket();
				this.CheckForData(timeout);
				if (!wantResults)
				{
					this.SkipToEnd();
				}
			}
			catch (IOException ex)
			{
				this.connected = false;
				throw new TdsInternalException("Server closed the connection.", ex);
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00005170 File Offset: 0x00003370
		internal void ExecBulkCopy(int timeout, bool wantResults)
		{
			this.moreResults = true;
			try
			{
				this.Comm.SendPacket();
				this.CheckForData(timeout);
				if (!wantResults)
				{
					this.SkipToEnd();
				}
			}
			catch (IOException ex)
			{
				this.connected = false;
				throw new TdsInternalException("Server closed the connection.", ex);
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000051DC File Offset: 0x000033DC
		protected void ExecuteQuery(string sql, int timeout, bool wantResults)
		{
			this.InitExec();
			this.Comm.StartPacket(TdsPacketType.Query);
			this.Comm.Append(sql);
			try
			{
				this.Comm.SendPacket();
				this.CheckForData(timeout);
				if (!wantResults)
				{
					this.SkipToEnd();
				}
			}
			catch (IOException ex)
			{
				this.connected = false;
				throw new TdsInternalException("Server closed the connection.", ex);
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00005260 File Offset: 0x00003460
		protected virtual void ExecRPC(string rpcName, TdsMetaParameterCollection parameters, int timeout, bool wantResults)
		{
			this.Comm.StartPacket(TdsPacketType.DBRPC);
			byte[] bytes = this.Comm.Encoder.GetBytes(rpcName);
			byte b = (byte)bytes.Length;
			ushort num = 0;
			ushort num2 = (ushort)(1 + b + 2);
			this.Comm.Append(num2);
			this.Comm.Append(b);
			this.Comm.Append(bytes);
			this.Comm.Append(num);
			try
			{
				this.Comm.SendPacket();
				this.CheckForData(timeout);
				if (!wantResults)
				{
					this.SkipToEnd();
				}
			}
			catch (IOException ex)
			{
				this.connected = false;
				throw new TdsInternalException("Server closed the connection.", ex);
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00005328 File Offset: 0x00003528
		public bool NextResult()
		{
			if (this.SequentialAccess && this.isResultRead)
			{
				while (this.NextRow())
				{
				}
				this.isRowRead = false;
				this.isResultRead = false;
			}
			if (!this.moreResults)
			{
				return false;
			}
			bool flag = false;
			bool flag2 = false;
			while (!flag)
			{
				TdsPacketSubType tdsPacketSubType = this.ProcessSubPacket();
				if (flag2)
				{
					this.moreResults = false;
					break;
				}
				TdsPacketSubType tdsPacketSubType2 = tdsPacketSubType;
				byte b;
				switch (tdsPacketSubType2)
				{
				case TdsPacketSubType.ColumnInfo:
					break;
				default:
					if (tdsPacketSubType2 != TdsPacketSubType.ColumnMetadata && tdsPacketSubType2 != TdsPacketSubType.RowFormat)
					{
						flag = !this.moreResults;
						continue;
					}
					break;
				case TdsPacketSubType.TableName:
					b = this.Comm.Peek();
					flag = b != 165;
					continue;
				case TdsPacketSubType.ColumnDetail:
					flag = true;
					continue;
				}
				b = this.Comm.Peek();
				flag = b != 164;
				if (flag && this.doneProc && b == 209)
				{
					flag2 = true;
					flag = false;
				}
			}
			return this.moreResults;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00005458 File Offset: 0x00003658
		public bool NextRow()
		{
			if (this.SequentialAccess && this.isRowRead)
			{
				this.SkipRow();
				this.isRowRead = false;
			}
			bool flag = false;
			bool flag2 = false;
			do
			{
				TdsPacketSubType tdsPacketSubType = this.ProcessSubPacket();
				TdsPacketSubType tdsPacketSubType2 = tdsPacketSubType;
				switch (tdsPacketSubType2)
				{
				case TdsPacketSubType.Done:
				case TdsPacketSubType.DoneProc:
				case TdsPacketSubType.DoneInProc:
					flag2 = false;
					flag = true;
					break;
				default:
					if (tdsPacketSubType2 == TdsPacketSubType.Row)
					{
						flag2 = true;
						flag = true;
					}
					break;
				}
			}
			while (!flag);
			return flag2;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000054D8 File Offset: 0x000036D8
		public virtual string Prepare(string sql, TdsMetaParameterCollection parameters)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000054E0 File Offset: 0x000036E0
		public void SkipToEnd()
		{
			try
			{
				while (this.NextResult())
				{
				}
			}
			catch (IOException ex)
			{
				this.connected = false;
				throw new TdsInternalException("Server closed the connection.", ex);
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00005538 File Offset: 0x00003738
		public virtual void Unprepare(string statementId)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00005540 File Offset: 0x00003740
		[MonoTODO("Is cancel enough, or do we need to drop the connection?")]
		protected void CheckForData(int timeout)
		{
			if (timeout > 0 && !this.comm.Poll(timeout, SelectMode.SelectRead))
			{
				this.Cancel();
				throw Tds.CreateTimeoutException(this.dataSource, "CheckForData()");
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00005580 File Offset: 0x00003780
		protected TdsInternalInfoMessageEventArgs CreateTdsInfoMessageEvent(TdsInternalErrorCollection errors)
		{
			return new TdsInternalInfoMessageEventArgs(errors);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00005588 File Offset: 0x00003788
		protected TdsInternalErrorMessageEventArgs CreateTdsErrorMessageEvent(byte theClass, int lineNumber, string message, int number, string procedure, string server, string source, byte state)
		{
			return new TdsInternalErrorMessageEventArgs(new TdsInternalError(theClass, lineNumber, message, number, procedure, server, source, state));
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000055AC File Offset: 0x000037AC
		private Encoding GetEncodingFromColumnCollation(int lcid, int sortId)
		{
			if (sortId != 0)
			{
				return TdsCharset.GetEncodingFromSortOrder(sortId);
			}
			return TdsCharset.GetEncodingFromLCID(lcid);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000055C4 File Offset: 0x000037C4
		protected object GetColumnValue(TdsColumnType? colType, bool outParam)
		{
			return this.GetColumnValue(colType, outParam, -1);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000055D0 File Offset: 0x000037D0
		private object GetColumnValue(TdsColumnType? colType, bool outParam, int ordinal)
		{
			object obj = null;
			int num = 0;
			int num2 = 0;
			if (colType == null)
			{
				throw new ArgumentNullException("colType");
			}
			if (ordinal > -1 && this.tdsVersion > TdsVersion.tds70)
			{
				num = this.columns[ordinal].LCID.Value;
				num2 = this.columns[ordinal].SortOrder.Value;
			}
			if (colType != null)
			{
				TdsColumnType value = colType.Value;
				switch (value)
				{
				case TdsColumnType.Image:
					if (outParam)
					{
						this.comm.Skip(1L);
					}
					return this.GetImageValue();
				case TdsColumnType.Text:
				{
					Encoding encoding = this.GetEncodingFromColumnCollation(num, num2);
					if (outParam)
					{
						this.comm.Skip(1L);
					}
					return this.GetTextValue(false, encoding);
				}
				case TdsColumnType.UniqueIdentifier:
				{
					if (this.comm.Peek() != 16)
					{
						this.comm.GetByte();
						return DBNull.Value;
					}
					if (outParam)
					{
						this.comm.Skip(1L);
					}
					int num3 = (int)(this.comm.GetByte() & byte.MaxValue);
					if (num3 > 0)
					{
						byte[] bytes = this.comm.GetBytes(num3, true);
						if (!BitConverter.IsLittleEndian)
						{
							byte[] array = new byte[num3];
							for (int i = 0; i < 4; i++)
							{
								array[i] = bytes[4 - i - 1];
							}
							for (int j = 4; j < 6; j++)
							{
								array[j] = bytes[6 - (j - 4) - 1];
							}
							for (int k = 6; k < 8; k++)
							{
								array[k] = bytes[8 - (k - 6) - 1];
							}
							for (int l = 8; l < 16; l++)
							{
								array[l] = bytes[l];
							}
							Array.Copy(array, 0, bytes, 0, num3);
						}
						obj = new Guid(bytes);
					}
					return obj;
				}
				case TdsColumnType.VarBinary:
				case TdsColumnType.Binary:
					if (outParam)
					{
						this.comm.Skip(1L);
					}
					return this.GetBinaryValue();
				case TdsColumnType.IntN:
					if (outParam)
					{
						this.comm.Skip(1L);
					}
					return this.GetIntValue(colType);
				case TdsColumnType.VarChar:
				case TdsColumnType.Char:
				{
					Encoding encoding = this.GetEncodingFromColumnCollation(num, num2);
					if (outParam)
					{
						this.comm.Skip(1L);
					}
					return this.GetStringValue(colType, false, outParam, encoding);
				}
				default:
					switch (value)
					{
					case TdsColumnType.NText:
					{
						Encoding encoding = this.GetEncodingFromColumnCollation(num, num2);
						if (outParam)
						{
							this.comm.Skip(1L);
						}
						return this.GetTextValue(true, encoding);
					}
					default:
					{
						Encoding encoding;
						switch (value)
						{
						case TdsColumnType.BigVarBinary:
						{
							if (outParam)
							{
								this.comm.Skip(1L);
							}
							int num3 = (int)this.comm.GetTdsShort();
							return this.comm.GetBytes(num3, true);
						}
						default:
							switch (value)
							{
							case TdsColumnType.BigBinary:
								if (outParam)
								{
									this.comm.Skip(2L);
								}
								return this.GetBinaryValue();
							default:
								if (value == TdsColumnType.SmallMoney)
								{
									goto IL_0374;
								}
								if (value == TdsColumnType.BigInt)
								{
									goto IL_01C0;
								}
								if (value != TdsColumnType.BigNVarChar && value != TdsColumnType.NChar)
								{
									goto IL_062C;
								}
								encoding = this.GetEncodingFromColumnCollation(num, num2);
								if (outParam)
								{
									this.comm.Skip(2L);
								}
								return this.GetStringValue(colType, true, outParam, encoding);
							case TdsColumnType.BigChar:
								break;
							}
							break;
						case TdsColumnType.BigVarChar:
							break;
						}
						encoding = this.GetEncodingFromColumnCollation(num, num2);
						if (outParam)
						{
							this.comm.Skip(2L);
						}
						return this.GetStringValue(colType, false, outParam, encoding);
					}
					case TdsColumnType.NVarChar:
					{
						Encoding encoding = this.GetEncodingFromColumnCollation(num, num2);
						if (outParam)
						{
							this.comm.Skip(1L);
						}
						return this.GetStringValue(colType, true, outParam, encoding);
					}
					case TdsColumnType.BitN:
						if (outParam)
						{
							this.comm.Skip(1L);
						}
						if (this.comm.GetByte() == 0)
						{
							obj = DBNull.Value;
						}
						else
						{
							obj = this.comm.GetByte() != 0;
						}
						return obj;
					case TdsColumnType.Decimal:
					case TdsColumnType.Numeric:
					{
						byte b;
						byte b2;
						if (outParam)
						{
							this.comm.Skip(1L);
							b = this.comm.GetByte();
							b2 = this.comm.GetByte();
						}
						else
						{
							b = (byte)this.columns[ordinal].NumericPrecision.Value;
							b2 = (byte)this.columns[ordinal].NumericScale.Value;
						}
						obj = this.GetDecimalValue(b, b2);
						if (b2 == 0 && b <= 19 && this.tdsVersion == TdsVersion.tds70 && !(obj is DBNull))
						{
							obj = Convert.ToInt64(obj);
						}
						return obj;
					}
					case TdsColumnType.FloatN:
						if (outParam)
						{
							this.comm.Skip(1L);
						}
						return this.GetFloatValue(colType);
					case TdsColumnType.MoneyN:
						if (outParam)
						{
							this.comm.Skip(1L);
						}
						return this.GetMoneyValue(colType);
					case TdsColumnType.DateTimeN:
						if (outParam)
						{
							this.comm.Skip(1L);
						}
						return this.GetDateTimeValue(colType);
					}
					break;
				case TdsColumnType.Int1:
				case TdsColumnType.Int2:
				case TdsColumnType.Int4:
					break;
				case TdsColumnType.Bit:
				{
					int @byte = (int)this.comm.GetByte();
					return @byte != 0;
				}
				case TdsColumnType.DateTime4:
				case TdsColumnType.DateTime:
					return this.GetDateTimeValue(colType);
				case TdsColumnType.Real:
				case TdsColumnType.Float8:
					return this.GetFloatValue(colType);
				case TdsColumnType.Money:
					goto IL_0374;
				}
				IL_01C0:
				return this.GetIntValue(colType);
				IL_0374:
				obj = this.GetMoneyValue(colType);
				return obj;
			}
			IL_062C:
			return DBNull.Value;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00005C10 File Offset: 0x00003E10
		private object GetBinaryValue()
		{
			object obj = DBNull.Value;
			if (this.tdsVersion >= TdsVersion.tds70)
			{
				int num = (int)this.comm.GetTdsShort();
				if (num != 65535 && num >= 0)
				{
					obj = this.comm.GetBytes(num, true);
				}
			}
			else
			{
				int num = (int)(this.comm.GetByte() & byte.MaxValue);
				if (num != 0)
				{
					obj = this.comm.GetBytes(num, true);
				}
			}
			return obj;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00005C88 File Offset: 0x00003E88
		private object GetDateTimeValue(TdsColumnType? type)
		{
			int num = 0;
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type != null)
			{
				TdsColumnType value = type.Value;
				switch (value)
				{
				case TdsColumnType.DateTime4:
					num = 4;
					break;
				default:
					if (value == TdsColumnType.DateTimeN)
					{
						byte b = this.comm.Peek();
						if (b == 0 || b == 4 || b == 8)
						{
							num = (int)this.comm.GetByte();
						}
					}
					break;
				case TdsColumnType.DateTime:
					num = 8;
					break;
				}
			}
			DateTime dateTime = new DateTime(1900, 1, 1);
			int num2 = num;
			object obj;
			if (num2 != 4)
			{
				if (num2 != 8)
				{
					obj = DBNull.Value;
				}
				else
				{
					obj = dateTime.AddDays((double)this.comm.GetTdsInt());
					int tdsInt = this.comm.GetTdsInt();
					long num3 = (long)Math.Round((double)((float)((long)tdsInt % 300L * 1000L) / 300f));
					if (tdsInt != 0 || num3 != 0L)
					{
						obj = ((DateTime)obj).AddSeconds((double)(tdsInt / 300));
						obj = ((DateTime)obj).AddMilliseconds((double)num3);
					}
				}
			}
			else
			{
				obj = dateTime.AddDays((double)((ushort)this.comm.GetTdsShort()));
				short tdsShort = this.comm.GetTdsShort();
				if (tdsShort != 0)
				{
					obj = ((DateTime)obj).AddMinutes((double)tdsShort);
				}
			}
			return obj;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00005E40 File Offset: 0x00004040
		private object GetDecimalValue(byte precision, byte scale)
		{
			if (this.tdsVersion < TdsVersion.tds70)
			{
				return this.GetDecimalValueTds50(precision, scale);
			}
			return this.GetDecimalValueTds70(precision, scale);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00005E60 File Offset: 0x00004060
		private object GetDecimalValueTds70(byte precision, byte scale)
		{
			int[] array = new int[4];
			int num = (int)((this.comm.GetByte() & byte.MaxValue) - 1);
			if (num < 0)
			{
				return DBNull.Value;
			}
			bool flag = this.comm.GetByte() == 1;
			if (num > 16)
			{
				throw new OverflowException();
			}
			int num2 = 0;
			int num3 = 0;
			while (num2 < num && num2 < 16)
			{
				array[num3] = this.comm.GetTdsInt();
				num2 += 4;
				num3++;
			}
			if (array[3] != 0)
			{
				return new TdsBigDecimal(precision, scale, !flag, array);
			}
			return new decimal(array[0], array[1], array[2], !flag, scale);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00005F14 File Offset: 0x00004114
		private object GetDecimalValueTds50(byte precision, byte scale)
		{
			int[] array = new int[4];
			int num = (int)(this.comm.GetByte() & byte.MaxValue);
			if (num == 0)
			{
				return DBNull.Value;
			}
			byte[] bytes = this.comm.GetBytes(num, false);
			byte[] array2 = new byte[4];
			bool flag = bytes[0] == 1;
			if (num > 17)
			{
				throw new OverflowException();
			}
			int num2 = 1;
			int num3 = 0;
			while (num2 < num && num2 < 16)
			{
				for (int i = 0; i < 4; i++)
				{
					if (num2 + i < num)
					{
						array2[i] = bytes[num - (num2 + i)];
					}
					else
					{
						array2[i] = 0;
					}
				}
				if (!BitConverter.IsLittleEndian)
				{
					array2 = this.comm.Swap(array2);
				}
				array[num3] = BitConverter.ToInt32(array2, 0);
				num2 += 4;
				num3++;
			}
			if (array[3] != 0)
			{
				return new TdsBigDecimal(precision, scale, flag, array);
			}
			return new decimal(array[0], array[1], array[2], flag, scale);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000601C File Offset: 0x0000421C
		private object GetFloatValue(TdsColumnType? columnType)
		{
			if (columnType == null)
			{
				throw new ArgumentNullException("columnType");
			}
			int num = 0;
			if (columnType != null)
			{
				TdsColumnType value = columnType.Value;
				switch (value)
				{
				case TdsColumnType.Real:
					num = 4;
					break;
				default:
					if (value == TdsColumnType.FloatN)
					{
						num = (int)this.comm.GetByte();
					}
					break;
				case TdsColumnType.Float8:
					num = 8;
					break;
				}
			}
			int num2 = num;
			if (num2 == 4)
			{
				return BitConverter.ToSingle(BitConverter.GetBytes(this.comm.GetTdsInt()), 0);
			}
			if (num2 != 8)
			{
				return DBNull.Value;
			}
			return BitConverter.Int64BitsToDouble(this.comm.GetTdsInt64());
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000060EC File Offset: 0x000042EC
		private object GetImageValue()
		{
			if (this.comm.GetByte() == 0)
			{
				return DBNull.Value;
			}
			this.comm.Skip(24L);
			int tdsInt = this.comm.GetTdsInt();
			if (tdsInt < 0)
			{
				return DBNull.Value;
			}
			return this.comm.GetBytes(tdsInt, true);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00006148 File Offset: 0x00004348
		private object GetIntValue(TdsColumnType? type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type != null)
			{
				TdsColumnType value = type.Value;
				int num;
				if (value != TdsColumnType.IntN)
				{
					if (value != TdsColumnType.Int1)
					{
						if (value != TdsColumnType.Int2)
						{
							if (value != TdsColumnType.Int4)
							{
								if (value != TdsColumnType.BigInt)
								{
									goto IL_0088;
								}
								num = 8;
							}
							else
							{
								num = 4;
							}
						}
						else
						{
							num = 2;
						}
					}
					else
					{
						num = 1;
					}
				}
				else
				{
					num = (int)this.comm.GetByte();
				}
				switch (num)
				{
				case 1:
					return this.comm.GetByte();
				case 2:
					return this.comm.GetTdsShort();
				case 4:
					return this.comm.GetTdsInt();
				case 8:
					return this.comm.GetTdsInt64();
				}
				return DBNull.Value;
			}
			IL_0088:
			return DBNull.Value;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000625C File Offset: 0x0000445C
		private object GetMoneyValue(TdsColumnType? type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type != null)
			{
				TdsColumnType value = type.Value;
				int num;
				switch (value)
				{
				case TdsColumnType.MoneyN:
					num = (int)this.comm.GetByte();
					goto IL_0081;
				default:
					if (value == TdsColumnType.Money)
					{
						num = 8;
						goto IL_0081;
					}
					if (value != TdsColumnType.SmallMoney)
					{
						goto IL_007B;
					}
					break;
				case TdsColumnType.Money4:
					break;
				}
				num = 4;
				IL_0081:
				int num2 = num;
				if (num2 == 4)
				{
					int num3 = this.Comm.GetTdsInt();
					bool flag = num3 < 0;
					if (flag)
					{
						num3 = ~(num3 - 1);
					}
					return new decimal(num3, 0, 0, flag, 4);
				}
				if (num2 != 8)
				{
					return DBNull.Value;
				}
				int num4 = this.Comm.GetTdsInt();
				int num5 = this.Comm.GetTdsInt();
				bool flag2 = num4 < 0;
				if (flag2)
				{
					num4 = ~num4;
					num5 = ~(num5 - 1);
				}
				return new decimal(num5, num4, 0, flag2, 4);
			}
			IL_007B:
			return DBNull.Value;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00006378 File Offset: 0x00004578
		protected object GetStringValue(TdsColumnType? colType, bool wideChars, bool outputParam, Encoding encoder)
		{
			Encoding encoding = encoder;
			bool flag;
			if (this.tdsVersion > TdsVersion.tds70 && outputParam && (colType == TdsColumnType.BigChar || colType == TdsColumnType.BigNVarChar || colType == TdsColumnType.BigVarChar || colType == TdsColumnType.NChar || colType == TdsColumnType.NVarChar))
			{
				byte[] bytes = this.Comm.GetBytes(5, true);
				encoding = TdsCharset.GetEncoding(bytes);
				flag = true;
			}
			else
			{
				flag = this.tdsVersion >= TdsVersion.tds70 && (wideChars || !outputParam);
			}
			int num = (int)((!flag) ? ((short)(this.comm.GetByte() & byte.MaxValue)) : this.comm.GetTdsShort());
			return this.GetStringValue(wideChars, num, encoding);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00006498 File Offset: 0x00004698
		protected object GetStringValue(bool wideChars, int len, Encoding enc)
		{
			if (this.tdsVersion < TdsVersion.tds70 && len == 0)
			{
				return DBNull.Value;
			}
			if (len >= 0)
			{
				object obj;
				if (wideChars)
				{
					obj = this.comm.GetString(len / 2, enc);
				}
				else
				{
					obj = this.comm.GetString(len, false, enc);
				}
				if (this.tdsVersion < TdsVersion.tds70 && ((string)obj).Equals(" "))
				{
					obj = string.Empty;
				}
				return obj;
			}
			return DBNull.Value;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00006520 File Offset: 0x00004720
		protected int GetSubPacketLength()
		{
			return (int)this.comm.GetTdsShort();
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00006530 File Offset: 0x00004730
		private object GetTextValue(bool wideChars, Encoding encoder)
		{
			byte @byte = this.comm.GetByte();
			if (@byte != 16)
			{
				return DBNull.Value;
			}
			this.comm.Skip(24L);
			int num = this.comm.GetTdsInt();
			if (num == 0)
			{
				return string.Empty;
			}
			string text;
			if (wideChars)
			{
				text = this.comm.GetString(num / 2, encoder);
			}
			else
			{
				text = this.comm.GetString(num, false, encoder);
			}
			num /= 2;
			if ((byte)this.tdsVersion < 70 && text == " ")
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000065D4 File Offset: 0x000047D4
		internal bool IsBlobType(TdsColumnType columnType)
		{
			return columnType == TdsColumnType.Text || columnType == TdsColumnType.Image || columnType == TdsColumnType.NText;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000065F0 File Offset: 0x000047F0
		internal bool IsLargeType(TdsColumnType columnType)
		{
			return (byte)columnType > 128;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000065FC File Offset: 0x000047FC
		protected bool IsWideType(TdsColumnType columnType)
		{
			return columnType == TdsColumnType.NText || columnType == TdsColumnType.NVarChar || columnType == TdsColumnType.NChar;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00006630 File Offset: 0x00004830
		internal static bool IsFixedSizeColumn(TdsColumnType columnType)
		{
			switch (columnType)
			{
			case TdsColumnType.Int1:
			case TdsColumnType.Bit:
			case TdsColumnType.Int2:
			case TdsColumnType.Int4:
			case TdsColumnType.DateTime4:
			case TdsColumnType.Real:
			case TdsColumnType.Money:
			case TdsColumnType.DateTime:
			case TdsColumnType.Float8:
				break;
			default:
				if (columnType != TdsColumnType.Money4 && columnType != TdsColumnType.SmallMoney && columnType != TdsColumnType.BigInt)
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000066A4 File Offset: 0x000048A4
		protected void LoadRow()
		{
			if (this.SequentialAccess)
			{
				if (this.isRowRead)
				{
					this.SkipRow();
				}
				this.isRowRead = true;
				this.isResultRead = true;
				return;
			}
			this.currentRow = new TdsDataRow();
			int num = 0;
			foreach (object obj in this.columns)
			{
				TdsDataColumn tdsDataColumn = (TdsDataColumn)obj;
				object columnValue = this.GetColumnValue(tdsDataColumn.ColumnType, false, num);
				this.currentRow.Add(columnValue);
				if (this.doneProc)
				{
					this.outputParameters.Add(columnValue);
				}
				if (columnValue is TdsBigDecimal && this.currentRow.BigDecimalIndex < 0)
				{
					this.currentRow.BigDecimalIndex = num;
				}
				num++;
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000067A8 File Offset: 0x000049A8
		internal static int LookupBufferSize(TdsColumnType columnType)
		{
			switch (columnType)
			{
			case TdsColumnType.Int1:
			case TdsColumnType.Bit:
				return 1;
			default:
				if (columnType != TdsColumnType.Money4 && columnType != TdsColumnType.SmallMoney)
				{
					if (columnType != TdsColumnType.BigInt)
					{
						return 0;
					}
					return 8;
				}
				break;
			case TdsColumnType.Int2:
				return 2;
			case TdsColumnType.Int4:
			case TdsColumnType.DateTime4:
			case TdsColumnType.Real:
				break;
			case TdsColumnType.Money:
			case TdsColumnType.DateTime:
			case TdsColumnType.Float8:
				return 8;
			}
			return 4;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00006824 File Offset: 0x00004A24
		protected internal int ProcessAuthentication()
		{
			int tdsShort = (int)this.Comm.GetTdsShort();
			byte[] bytes = this.Comm.GetBytes(tdsShort, true);
			Type2Message type2Message = new Type2Message(bytes);
			Type3Message type3Message = new Type3Message();
			type3Message.Challenge = type2Message.Nonce;
			type3Message.Domain = this.connectionParms.DefaultDomain;
			type3Message.Host = this.connectionParms.Hostname;
			type3Message.Username = this.connectionParms.User;
			type3Message.Password = this.connectionParms.Password;
			this.Comm.StartPacket(TdsPacketType.SspAuth);
			this.Comm.Append(type3Message.GetBytes());
			try
			{
				this.Comm.SendPacket();
			}
			catch (IOException ex)
			{
				this.connected = false;
				throw new TdsInternalException("Server closed the connection.", ex);
			}
			return 1;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00006910 File Offset: 0x00004B10
		protected void ProcessColumnDetail()
		{
			int subPacketLength = this.GetSubPacketLength();
			byte[] array = new byte[3];
			string text = string.Empty;
			int i = 0;
			while (i < subPacketLength)
			{
				for (int j = 0; j < 3; j++)
				{
					array[j] = this.comm.GetByte();
				}
				i += 3;
				bool flag = (array[2] & 32) != 0;
				if (flag)
				{
					int num;
					if (this.tdsVersion >= TdsVersion.tds70)
					{
						num = (int)this.comm.GetByte();
						i += 2 * num + 1;
					}
					else
					{
						num = (int)this.comm.GetByte();
						i += num + 1;
					}
					text = this.comm.GetString(num);
				}
				byte b = array[0] - 1;
				byte b2 = array[1] - 1;
				bool flag2 = (array[2] & 4) != 0;
				TdsDataColumn tdsDataColumn = this.columns[(int)b];
				tdsDataColumn.IsHidden = new bool?((array[2] & 16) != 0);
				tdsDataColumn.IsExpression = new bool?(flag2);
				tdsDataColumn.IsKey = new bool?((array[2] & 8) != 0);
				tdsDataColumn.IsAliased = new bool?(flag);
				tdsDataColumn.BaseColumnName = ((!flag) ? null : text);
				tdsDataColumn.BaseTableName = (flag2 ? null : ((string)this.tableNames[(int)b2]));
			}
		}

		// Token: 0x06000098 RID: 152
		protected abstract void ProcessColumnInfo();

		// Token: 0x06000099 RID: 153 RVA: 0x00006A7C File Offset: 0x00004C7C
		protected void ProcessColumnNames()
		{
			this.columnNames = new ArrayList();
			int tdsShort = (int)this.comm.GetTdsShort();
			int i = 0;
			int num = 0;
			while (i < tdsShort)
			{
				int @byte = (int)this.comm.GetByte();
				string @string = this.comm.GetString(@byte);
				i = i + 1 + @byte;
				this.columnNames.Add(@string);
				num++;
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00006AE4 File Offset: 0x00004CE4
		[MonoTODO("Make sure counting works right, especially with multiple resultsets.")]
		protected void ProcessEndToken(TdsPacketSubType type)
		{
			byte @byte = this.Comm.GetByte();
			this.Comm.Skip(1L);
			byte byte2 = this.comm.GetByte();
			this.Comm.Skip(1L);
			int tdsInt = this.comm.GetTdsInt();
			bool flag = this.IsValidRowCount(@byte, byte2);
			this.moreResults = (@byte & 1) != 0;
			bool flag2 = (@byte & 32) != 0;
			switch (type)
			{
			case TdsPacketSubType.Done:
			case TdsPacketSubType.DoneInProc:
				break;
			case TdsPacketSubType.DoneProc:
				this.doneProc = true;
				break;
			default:
				goto IL_00C0;
			}
			if (flag)
			{
				if (this.recordsAffected == -1)
				{
					this.recordsAffected = tdsInt;
				}
				else
				{
					this.recordsAffected += tdsInt;
				}
			}
			IL_00C0:
			if (this.moreResults)
			{
				this.queryInProgress = false;
			}
			if (flag2)
			{
				this.cancelsProcessed++;
			}
			if (this.messages.Count > 0 && !this.moreResults)
			{
				this.OnTdsInfoMessage(this.CreateTdsInfoMessageEvent(this.messages));
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00006C08 File Offset: 0x00004E08
		protected void ProcessEnvironmentChange()
		{
			int subPacketLength = this.GetSubPacketLength();
			switch (this.comm.GetByte())
			{
			case 1:
			{
				int num = (int)this.comm.GetByte();
				string @string = this.comm.GetString(num);
				num = (int)(this.comm.GetByte() & byte.MaxValue);
				this.comm.GetString(num);
				if (this.originalDatabase == string.Empty)
				{
					this.originalDatabase = @string;
				}
				this.database = @string;
				return;
			}
			case 3:
			{
				int num = (int)this.comm.GetByte();
				if (this.tdsVersion == TdsVersion.tds70)
				{
					this.SetCharset(this.comm.GetString(num));
					this.comm.Skip((long)(subPacketLength - 2 - num * 2));
				}
				else
				{
					this.SetCharset(this.comm.GetString(num));
					this.comm.Skip((long)(subPacketLength - 2 - num));
				}
				return;
			}
			case 4:
			{
				int num = (int)this.comm.GetByte();
				string string2 = this.comm.GetString(num);
				if (this.tdsVersion >= TdsVersion.tds70)
				{
					this.comm.Skip((long)(subPacketLength - 2 - num * 2));
				}
				else
				{
					this.comm.Skip((long)(subPacketLength - 2 - num));
				}
				this.packetSize = int.Parse(string2);
				this.comm.ResizeOutBuf(this.packetSize);
				return;
			}
			case 5:
			{
				int num = (int)this.comm.GetByte();
				int num2;
				if (this.tdsVersion >= TdsVersion.tds70)
				{
					num2 = (int)Convert.ChangeType(this.comm.GetString(num), typeof(int));
					this.comm.Skip((long)(subPacketLength - 2 - num * 2));
				}
				else
				{
					num2 = (int)Convert.ChangeType(this.comm.GetString(num), typeof(int));
					this.comm.Skip((long)(subPacketLength - 2 - num));
				}
				this.locale = new CultureInfo(num2);
				return;
			}
			case 7:
			{
				int num = (int)this.comm.GetByte();
				this.collation = this.comm.GetBytes(num, true);
				int num2 = TdsCollation.LCID(this.collation);
				this.locale = new CultureInfo(num2);
				this.SetCharset(TdsCharset.GetEncoding(this.collation));
				return;
			}
			}
			this.comm.Skip((long)(subPacketLength - 1));
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00006E94 File Offset: 0x00005094
		protected void ProcessLoginAck()
		{
			this.GetSubPacketLength();
			if (this.tdsVersion >= TdsVersion.tds70)
			{
				this.comm.Skip(1L);
				uint tdsInt = (uint)this.comm.GetTdsInt();
				uint num = tdsInt;
				if (num != 117440512U)
				{
					if (num != 117506048U)
					{
						if (num != 1895825409U)
						{
							if (num == 1913192450U)
							{
								this.tdsVersion = TdsVersion.tds90;
							}
						}
						else
						{
							this.tdsVersion = TdsVersion.tds81;
						}
					}
					else
					{
						this.tdsVersion = TdsVersion.tds80;
					}
				}
				else
				{
					this.tdsVersion = TdsVersion.tds70;
				}
			}
			if (this.tdsVersion >= TdsVersion.tds70)
			{
				int @byte = (int)this.comm.GetByte();
				this.databaseProductName = this.comm.GetString(@byte);
				this.databaseMajorVersion = (int)this.comm.GetByte();
				this.databaseProductVersion = string.Format("{0}.{1}.{2}", this.databaseMajorVersion.ToString("00"), this.comm.GetByte().ToString("00"), (256 * (int)this.comm.GetByte() + (int)this.comm.GetByte()).ToString("0000"));
			}
			else
			{
				this.comm.Skip(5L);
				short byte2 = (short)this.comm.GetByte();
				this.databaseProductName = this.comm.GetString((int)byte2);
				this.comm.Skip(1L);
				this.databaseMajorVersion = (int)this.comm.GetByte();
				this.databaseProductVersion = string.Format("{0}.{1}", this.databaseMajorVersion, this.comm.GetByte());
				this.comm.Skip(1L);
			}
			if (this.databaseProductName.Length > 1 && this.databaseProductName.IndexOf('\0') != -1)
			{
				int num2 = this.databaseProductName.IndexOf('\0');
				this.databaseProductName = this.databaseProductName.Substring(0, num2);
			}
			this.connected = true;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000070AC File Offset: 0x000052AC
		protected void OnTdsErrorMessage(TdsInternalErrorMessageEventArgs e)
		{
			if (this.TdsErrorMessage != null)
			{
				this.TdsErrorMessage(this, e);
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000070C8 File Offset: 0x000052C8
		protected void OnTdsInfoMessage(TdsInternalInfoMessageEventArgs e)
		{
			if (this.TdsInfoMessage != null)
			{
				this.TdsInfoMessage(this, e);
			}
			this.messages.Clear();
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000070F0 File Offset: 0x000052F0
		protected void ProcessMessage(TdsPacketSubType subType)
		{
			this.GetSubPacketLength();
			int tdsInt = this.comm.GetTdsInt();
			byte @byte = this.comm.GetByte();
			byte byte2 = this.comm.GetByte();
			bool flag;
			if (subType == TdsPacketSubType.EED)
			{
				flag = byte2 > 10;
				this.comm.Skip((long)this.comm.GetByte());
				this.comm.Skip(1L);
				this.comm.Skip(2L);
			}
			else
			{
				flag = subType == TdsPacketSubType.Error;
			}
			string @string = this.comm.GetString((int)this.comm.GetTdsShort());
			string string2 = this.comm.GetString((int)this.comm.GetByte());
			string string3 = this.comm.GetString((int)this.comm.GetByte());
			byte byte3 = this.comm.GetByte();
			this.comm.Skip(1L);
			string empty = string.Empty;
			if (flag)
			{
				this.OnTdsErrorMessage(this.CreateTdsErrorMessageEvent(byte2, (int)byte3, @string, tdsInt, string3, string2, empty, @byte));
			}
			else
			{
				this.messages.Add(new TdsInternalError(byte2, (int)byte3, @string, tdsInt, string3, string2, empty, @byte));
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00007228 File Offset: 0x00005428
		protected virtual void ProcessOutputParam()
		{
			this.GetSubPacketLength();
			this.comm.GetString((int)(this.comm.GetByte() & byte.MaxValue));
			this.comm.Skip(5L);
			TdsColumnType @byte = (TdsColumnType)this.comm.GetByte();
			object columnValue = this.GetColumnValue(new TdsColumnType?(@byte), true);
			this.outputParameters.Add(columnValue);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00007290 File Offset: 0x00005490
		protected void ProcessDynamic()
		{
			this.Comm.Skip(2L);
			this.Comm.GetByte();
			this.Comm.GetByte();
			this.Comm.GetString((int)this.Comm.GetByte());
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000072DC File Offset: 0x000054DC
		protected virtual TdsPacketSubType ProcessSubPacket()
		{
			TdsPacketSubType @byte = (TdsPacketSubType)this.comm.GetByte();
			TdsPacketSubType tdsPacketSubType = @byte;
			switch (tdsPacketSubType)
			{
			case TdsPacketSubType.ColumnName:
				this.Comm.Skip(8L);
				this.ProcessColumnNames();
				return @byte;
			case TdsPacketSubType.ColumnInfo:
				goto IL_019F;
			default:
				switch (tdsPacketSubType)
				{
				case TdsPacketSubType.Capability:
				case TdsPacketSubType.ParamFormat:
					break;
				case TdsPacketSubType.EnvironmentChange:
					this.ProcessEnvironmentChange();
					return @byte;
				default:
					switch (tdsPacketSubType)
					{
					case TdsPacketSubType.ReturnStatus:
						this.ProcessReturnStatus();
						return @byte;
					default:
						switch (tdsPacketSubType)
						{
						case TdsPacketSubType.Done:
						case TdsPacketSubType.DoneProc:
						case TdsPacketSubType.DoneInProc:
							this.ProcessEndToken(@byte);
							return @byte;
						default:
							if (tdsPacketSubType == TdsPacketSubType.ColumnMetadata)
							{
								goto IL_019F;
							}
							if (tdsPacketSubType != TdsPacketSubType.Row)
							{
								return @byte;
							}
							this.LoadRow();
							return @byte;
						}
						break;
					case TdsPacketSubType.ProcId:
						this.Comm.Skip(8L);
						return @byte;
					}
					break;
				case TdsPacketSubType.EED:
					goto IL_0130;
				case TdsPacketSubType.Dynamic:
					this.ProcessDynamic();
					return @byte;
				case TdsPacketSubType.Authentication:
					this.ProcessAuthentication();
					return @byte;
				case TdsPacketSubType.RowFormat:
					goto IL_019F;
				}
				break;
			case TdsPacketSubType.Dynamic2:
				this.comm.Skip((long)this.comm.GetTdsInt());
				return @byte;
			case TdsPacketSubType.TableName:
				this.ProcessTableName();
				return @byte;
			case TdsPacketSubType.ColumnDetail:
				this.ProcessColumnDetail();
				return @byte;
			case TdsPacketSubType.AltName:
			case TdsPacketSubType.AltFormat:
				break;
			case TdsPacketSubType.ColumnOrder:
				this.comm.Skip((long)this.comm.GetTdsShort());
				return @byte;
			case TdsPacketSubType.Error:
			case TdsPacketSubType.Info:
				goto IL_0130;
			case TdsPacketSubType.Param:
				this.ProcessOutputParam();
				return @byte;
			case TdsPacketSubType.LoginAck:
				this.ProcessLoginAck();
				return @byte;
			case TdsPacketSubType.Control:
				this.comm.Skip((long)this.comm.GetTdsShort());
				return @byte;
			}
			this.comm.Skip((long)this.comm.GetTdsShort());
			return @byte;
			IL_0130:
			this.ProcessMessage(@byte);
			return @byte;
			IL_019F:
			this.Columns.Clear();
			this.ProcessColumnInfo();
			return @byte;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000074F8 File Offset: 0x000056F8
		protected void ProcessTableName()
		{
			this.tableNames = new ArrayList();
			int tdsShort = (int)this.comm.GetTdsShort();
			int i = 0;
			while (i < tdsShort)
			{
				int num;
				if (this.tdsVersion >= TdsVersion.tds70)
				{
					num = (int)this.comm.GetTdsShort();
					i += 2 * (num + 1);
				}
				else
				{
					num = (int)this.comm.GetByte();
					i += num + 1;
				}
				this.tableNames.Add(this.comm.GetString(num));
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000757C File Offset: 0x0000577C
		protected void SetCharset(Encoding encoder)
		{
			this.comm.Encoder = encoder;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000758C File Offset: 0x0000578C
		protected void SetCharset(string charset)
		{
			if (charset == null || charset.Length > 30)
			{
				charset = "iso_1";
			}
			if (this.charset != null && this.charset == charset)
			{
				return;
			}
			if (charset.StartsWith("cp"))
			{
				this.encoder = Encoding.GetEncoding(int.Parse(charset.Substring(2)));
				this.charset = charset;
			}
			else
			{
				this.encoder = Encoding.GetEncoding("iso-8859-1");
				this.charset = "iso_1";
			}
			this.SetCharset(this.encoder);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000762C File Offset: 0x0000582C
		protected void SetLanguage(string language)
		{
			if (language == null || language.Length > 30)
			{
				language = "us_english";
			}
			this.language = language;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00007650 File Offset: 0x00005850
		protected virtual void ProcessReturnStatus()
		{
			this.comm.Skip(4L);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00007660 File Offset: 0x00005860
		protected IAsyncResult BeginExecuteQueryInternal(string sql, bool wantResults, AsyncCallback callback, object state)
		{
			this.InitExec();
			TdsAsyncResult tdsAsyncResult = new TdsAsyncResult(callback, state);
			tdsAsyncResult.TdsAsyncState.WantResults = wantResults;
			this.Comm.StartPacket(TdsPacketType.Query);
			this.Comm.Append(sql);
			try
			{
				this.Comm.SendPacket();
				this.Comm.BeginReadPacket(new AsyncCallback(this.OnBeginExecuteQueryCallback), tdsAsyncResult);
			}
			catch (IOException ex)
			{
				this.connected = false;
				throw new TdsInternalException("Server closed the connection.", ex);
			}
			return tdsAsyncResult;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00007700 File Offset: 0x00005900
		protected void EndExecuteQueryInternal(IAsyncResult ar)
		{
			if (!ar.IsCompleted)
			{
				ar.AsyncWaitHandle.WaitOne();
			}
			TdsAsyncResult tdsAsyncResult = (TdsAsyncResult)ar;
			if (tdsAsyncResult.IsCompletedWithException)
			{
				throw tdsAsyncResult.Exception;
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00007740 File Offset: 0x00005940
		protected void OnBeginExecuteQueryCallback(IAsyncResult ar)
		{
			TdsAsyncResult tdsAsyncResult = (TdsAsyncResult)ar.AsyncState;
			TdsAsyncState tdsAsyncState = tdsAsyncResult.TdsAsyncState;
			try
			{
				this.Comm.EndReadPacket(ar);
				if (!tdsAsyncState.WantResults)
				{
					this.SkipToEnd();
				}
			}
			catch (Exception ex)
			{
				tdsAsyncResult.MarkComplete(ex);
				return;
			}
			tdsAsyncResult.MarkComplete();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000077B8 File Offset: 0x000059B8
		public virtual IAsyncResult BeginExecuteNonQuery(string sql, TdsMetaParameterCollection parameters, AsyncCallback callback, object state)
		{
			throw new NotImplementedException("should not be called!");
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000077C4 File Offset: 0x000059C4
		public virtual void EndExecuteNonQuery(IAsyncResult ar)
		{
			throw new NotImplementedException("should not be called!");
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000077D0 File Offset: 0x000059D0
		public virtual IAsyncResult BeginExecuteQuery(string sql, TdsMetaParameterCollection parameters, AsyncCallback callback, object state)
		{
			throw new NotImplementedException("should not be called!");
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000077DC File Offset: 0x000059DC
		public virtual void EndExecuteQuery(IAsyncResult ar)
		{
			throw new NotImplementedException("should not be called!");
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000077E8 File Offset: 0x000059E8
		public virtual IAsyncResult BeginExecuteProcedure(string prolog, string epilog, string cmdText, bool IsNonQuery, TdsMetaParameterCollection parameters, AsyncCallback callback, object state)
		{
			throw new NotImplementedException("should not be called!");
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000077F4 File Offset: 0x000059F4
		public virtual void EndExecuteProcedure(IAsyncResult ar)
		{
			throw new NotImplementedException("should not be called!");
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00007800 File Offset: 0x00005A00
		public void WaitFor(IAsyncResult ar)
		{
			if (!ar.IsCompleted)
			{
				ar.AsyncWaitHandle.WaitOne();
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000781C File Offset: 0x00005A1C
		public void CheckAndThrowException(IAsyncResult ar)
		{
			TdsAsyncResult tdsAsyncResult = (TdsAsyncResult)ar;
			if (tdsAsyncResult.IsCompleted && tdsAsyncResult.IsCompletedWithException)
			{
				throw tdsAsyncResult.Exception;
			}
		}

		// Token: 0x04000048 RID: 72
		private TdsComm comm;

		// Token: 0x04000049 RID: 73
		private TdsVersion tdsVersion;

		// Token: 0x0400004A RID: 74
		protected internal TdsConnectionParameters connectionParms;

		// Token: 0x0400004B RID: 75
		protected readonly byte[] NTLMSSP_ID = new byte[] { 78, 84, 76, 77, 83, 83, 80, 0 };

		// Token: 0x0400004C RID: 76
		private int packetSize;

		// Token: 0x0400004D RID: 77
		private string dataSource;

		// Token: 0x0400004E RID: 78
		private string database;

		// Token: 0x0400004F RID: 79
		private string originalDatabase = string.Empty;

		// Token: 0x04000050 RID: 80
		private string databaseProductName;

		// Token: 0x04000051 RID: 81
		private string databaseProductVersion;

		// Token: 0x04000052 RID: 82
		private int databaseMajorVersion;

		// Token: 0x04000053 RID: 83
		private CultureInfo locale = CultureInfo.InvariantCulture;

		// Token: 0x04000054 RID: 84
		private string charset;

		// Token: 0x04000055 RID: 85
		private string language;

		// Token: 0x04000056 RID: 86
		private bool connected;

		// Token: 0x04000057 RID: 87
		private bool moreResults;

		// Token: 0x04000058 RID: 88
		private Encoding encoder;

		// Token: 0x04000059 RID: 89
		private bool doneProc;

		// Token: 0x0400005A RID: 90
		private bool pooling = true;

		// Token: 0x0400005B RID: 91
		private TdsDataRow currentRow;

		// Token: 0x0400005C RID: 92
		private TdsDataColumnCollection columns;

		// Token: 0x0400005D RID: 93
		private ArrayList tableNames;

		// Token: 0x0400005E RID: 94
		private ArrayList columnNames;

		// Token: 0x0400005F RID: 95
		private TdsMetaParameterCollection parameters = new TdsMetaParameterCollection();

		// Token: 0x04000060 RID: 96
		private bool queryInProgress;

		// Token: 0x04000061 RID: 97
		private int cancelsRequested;

		// Token: 0x04000062 RID: 98
		private int cancelsProcessed;

		// Token: 0x04000063 RID: 99
		private ArrayList outputParameters = new ArrayList();

		// Token: 0x04000064 RID: 100
		protected TdsInternalErrorCollection messages = new TdsInternalErrorCollection();

		// Token: 0x04000065 RID: 101
		private int recordsAffected = -1;

		// Token: 0x04000066 RID: 102
		private long StreamLength;

		// Token: 0x04000067 RID: 103
		private long StreamIndex;

		// Token: 0x04000068 RID: 104
		private int StreamColumnIndex;

		// Token: 0x04000069 RID: 105
		private bool sequentialAccess;

		// Token: 0x0400006A RID: 106
		private bool isRowRead;

		// Token: 0x0400006B RID: 107
		private bool isResultRead;

		// Token: 0x0400006C RID: 108
		private bool LoadInProgress;

		// Token: 0x0400006D RID: 109
		private byte[] collation;

		// Token: 0x0400006E RID: 110
		internal int poolStatus;
	}
}
