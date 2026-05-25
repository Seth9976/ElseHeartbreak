using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Mono.Data.Tds.Protocol
{
	// Token: 0x02000013 RID: 19
	internal sealed class TdsComm
	{
		// Token: 0x060000FE RID: 254 RVA: 0x0000BA3C File Offset: 0x00009C3C
		public TdsComm(string dataSource, int port, int packetSize, int timeout, TdsVersion tdsVersion)
		{
			this.packetSize = packetSize;
			this.tdsVersion = tdsVersion;
			this.dataSource = dataSource;
			this.outBuffer = new byte[packetSize];
			this.inBuffer = new byte[packetSize];
			this.outBufferLength = packetSize;
			this.inBufferLength = packetSize;
			this.lsb = true;
			bool flag = false;
			IPEndPoint ipendPoint;
			try
			{
				IPAddress ipaddress;
				if (IPAddress.TryParse(this.dataSource, out ipaddress))
				{
					ipendPoint = new IPEndPoint(ipaddress, port);
				}
				else
				{
					IPHostEntry hostEntry = Dns.GetHostEntry(this.dataSource);
					ipendPoint = new IPEndPoint(hostEntry.AddressList[0], port);
				}
			}
			catch (SocketException ex)
			{
				throw new TdsInternalException("Server does not exist or connection refused.", ex);
			}
			try
			{
				this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				IAsyncResult asyncResult = this.socket.BeginConnect(ipendPoint, null, null);
				int num = timeout * 1000;
				if (timeout > 0 && !asyncResult.IsCompleted && !asyncResult.AsyncWaitHandle.WaitOne(num, false))
				{
					throw Tds.CreateTimeoutException(dataSource, "Open()");
				}
				this.socket.EndConnect(asyncResult);
				try
				{
					this.socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, 1);
				}
				catch (SocketException)
				{
				}
				try
				{
					this.socket.NoDelay = true;
					this.socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, num);
					this.socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, num);
				}
				catch
				{
				}
				this.stream = new NetworkStream(this.socket, true);
			}
			catch (SocketException ex2)
			{
				flag = true;
				throw new TdsInternalException("Server does not exist or connection refused.", ex2);
			}
			catch (Exception)
			{
				flag = true;
				throw;
			}
			finally
			{
				if (flag && this.socket != null)
				{
					try
					{
						Socket socket = this.socket;
						this.socket = null;
						socket.Close();
					}
					catch
					{
					}
				}
			}
			if (!this.socket.Connected)
			{
				throw new TdsInternalException("Server does not exist or connection refused.", null);
			}
			this.packetsSent = 1;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000100 RID: 256 RVA: 0x0000BD10 File Offset: 0x00009F10
		// (set) Token: 0x06000101 RID: 257 RVA: 0x0000BD18 File Offset: 0x00009F18
		public int CommandTimeout
		{
			get
			{
				return this.commandTimeout;
			}
			set
			{
				this.commandTimeout = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000102 RID: 258 RVA: 0x0000BD24 File Offset: 0x00009F24
		// (set) Token: 0x06000103 RID: 259 RVA: 0x0000BD2C File Offset: 0x00009F2C
		internal Encoding Encoder
		{
			get
			{
				return this.encoder;
			}
			set
			{
				this.encoder = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000104 RID: 260 RVA: 0x0000BD38 File Offset: 0x00009F38
		// (set) Token: 0x06000105 RID: 261 RVA: 0x0000BD40 File Offset: 0x00009F40
		public int PacketSize
		{
			get
			{
				return this.packetSize;
			}
			set
			{
				this.packetSize = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000106 RID: 262 RVA: 0x0000BD4C File Offset: 0x00009F4C
		// (set) Token: 0x06000107 RID: 263 RVA: 0x0000BD58 File Offset: 0x00009F58
		public bool TdsByteOrder
		{
			get
			{
				return !this.lsb;
			}
			set
			{
				this.lsb = !value;
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000BD64 File Offset: 0x00009F64
		public byte[] Swap(byte[] toswap)
		{
			byte[] array = new byte[toswap.Length];
			for (int i = 0; i < toswap.Length; i++)
			{
				array[toswap.Length - i - 1] = toswap[i];
			}
			return array;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000BD9C File Offset: 0x00009F9C
		public void SendIfFull()
		{
			if (this.nextOutBufferIndex == this.outBufferLength)
			{
				this.SendPhysicalPacket(false);
				this.nextOutBufferIndex = TdsComm.headerLength;
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000BDC4 File Offset: 0x00009FC4
		public void SendIfFull(int reserve)
		{
			if (this.nextOutBufferIndex + reserve > this.outBufferLength)
			{
				this.SendPhysicalPacket(false);
				this.nextOutBufferIndex = TdsComm.headerLength;
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000BDEC File Offset: 0x00009FEC
		public void Append(object o)
		{
			if (o == null || o == DBNull.Value)
			{
				this.Append(0);
				return;
			}
			switch (Type.GetTypeCode(o.GetType()))
			{
			case TypeCode.Object:
				if (o is byte[])
				{
					this.Append((byte[])o);
				}
				return;
			case TypeCode.Boolean:
				if ((bool)o)
				{
					this.Append(1);
				}
				else
				{
					this.Append(0);
				}
				return;
			case TypeCode.Byte:
				this.Append((byte)o);
				return;
			case TypeCode.Int16:
				this.Append((short)o);
				return;
			case TypeCode.Int32:
				this.Append((int)o);
				return;
			case TypeCode.Int64:
				this.Append((long)o);
				return;
			case TypeCode.Single:
				this.Append((float)o);
				return;
			case TypeCode.Double:
				this.Append((double)o);
				return;
			case TypeCode.Decimal:
				this.Append((decimal)o, 17);
				return;
			case TypeCode.DateTime:
				this.Append((DateTime)o, 8);
				return;
			case TypeCode.String:
				this.Append((string)o);
				return;
			}
			throw new InvalidOperationException(string.Format("Object Type :{0} , not being appended", o.GetType()));
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000BF38 File Offset: 0x0000A138
		public void Append(byte b)
		{
			this.SendIfFull();
			this.Store(this.nextOutBufferIndex, b);
			this.nextOutBufferIndex++;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000BF5C File Offset: 0x0000A15C
		public void Append(DateTime t, int bytes)
		{
			DateTime dateTime = new DateTime(1900, 1, 1);
			TimeSpan timeSpan = t - dateTime;
			int days = timeSpan.Days;
			this.SendIfFull(bytes);
			if (bytes == 8)
			{
				long num = (long)(timeSpan.Hours * 3600 + timeSpan.Minutes * 60 + timeSpan.Seconds) * 1000L + (long)timeSpan.Milliseconds;
				int num2 = (int)(num * 300L / 1000L);
				this.AppendInternal(days);
				this.AppendInternal(num2);
			}
			else
			{
				if (bytes != 4)
				{
					throw new Exception("Invalid No of bytes");
				}
				int num2 = timeSpan.Hours * 60 + timeSpan.Minutes;
				this.AppendInternal((short)days);
				this.AppendInternal((short)num2);
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000C02C File Offset: 0x0000A22C
		public void Append(byte[] b)
		{
			this.Append(b, b.Length, 0);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000C03C File Offset: 0x0000A23C
		public void Append(byte[] b, int len, byte pad)
		{
			int i = Math.Min(b.Length, len);
			int j = len - i;
			int num = 0;
			while (i > 0)
			{
				this.SendIfFull();
				int num2 = this.outBufferLength - this.nextOutBufferIndex;
				int num3 = Math.Min(num2, i);
				Buffer.BlockCopy(b, num, this.outBuffer, this.nextOutBufferIndex, num3);
				this.nextOutBufferIndex += num3;
				i -= num3;
				num += num3;
			}
			while (j > 0)
			{
				this.SendIfFull();
				int num4 = this.outBufferLength - this.nextOutBufferIndex;
				int num5 = Math.Min(num4, j);
				for (int k = 0; k < num5; k++)
				{
					this.outBuffer[this.nextOutBufferIndex++] = pad;
				}
				j -= num5;
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000C114 File Offset: 0x0000A314
		private void AppendInternal(short s)
		{
			if (!this.lsb)
			{
				this.outBuffer[this.nextOutBufferIndex++] = (byte)(s >> 8) & byte.MaxValue;
				this.outBuffer[this.nextOutBufferIndex++] = (byte)(s & 255);
			}
			else
			{
				this.outBuffer[this.nextOutBufferIndex++] = (byte)(s & 255);
				this.outBuffer[this.nextOutBufferIndex++] = (byte)(s >> 8) & byte.MaxValue;
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000C1B8 File Offset: 0x0000A3B8
		public void Append(short s)
		{
			this.SendIfFull(2);
			this.AppendInternal(s);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000C1C8 File Offset: 0x0000A3C8
		public void Append(ushort s)
		{
			this.SendIfFull(2);
			this.AppendInternal((short)s);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000C1DC File Offset: 0x0000A3DC
		private void AppendInternal(int i)
		{
			if (!this.lsb)
			{
				this.AppendInternal((short)((int)((short)(i >> 16)) & 65535));
				this.AppendInternal((short)(i & 65535));
			}
			else
			{
				this.AppendInternal((short)(i & 65535));
				this.AppendInternal((short)((int)((short)(i >> 16)) & 65535));
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000C23C File Offset: 0x0000A43C
		public void Append(int i)
		{
			this.SendIfFull(4);
			this.AppendInternal(i);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000C24C File Offset: 0x0000A44C
		public void Append(string s)
		{
			if (this.tdsVersion < TdsVersion.tds70)
			{
				this.Append(this.encoder.GetBytes(s));
			}
			else
			{
				int num = s.Length * 2;
				int num2 = num / this.outBufferLength;
				int num3 = 0;
				if (num % this.outBufferLength > 0)
				{
					num2++;
				}
				int num4 = this.outBufferLength - this.nextOutBufferIndex;
				for (int i = 0; i < num2; i++)
				{
					int num5 = Math.Min(num4, num);
					int j = 0;
					while (j < num5)
					{
						this.AppendInternal((short)s[num3]);
						j += 2;
						num3++;
					}
					num -= Math.Min(num4, num);
					this.SendIfFull(num + 2);
				}
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000C310 File Offset: 0x0000A510
		public byte[] Append(string s, int len, byte pad)
		{
			if (s == null)
			{
				return new byte[0];
			}
			byte[] bytes = this.encoder.GetBytes(s);
			this.Append(bytes, len, pad);
			return bytes;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000C344 File Offset: 0x0000A544
		public void Append(double value)
		{
			if (!this.lsb)
			{
				this.Append(this.Swap(BitConverter.GetBytes(value)), 8, 0);
			}
			else
			{
				this.Append(BitConverter.GetBytes(value), 8, 0);
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000C384 File Offset: 0x0000A584
		public void Append(float value)
		{
			if (!this.lsb)
			{
				this.Append(this.Swap(BitConverter.GetBytes(value)), 4, 0);
			}
			else
			{
				this.Append(BitConverter.GetBytes(value), 4, 0);
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000C3C4 File Offset: 0x0000A5C4
		public void Append(long l)
		{
			this.SendIfFull(8);
			if (!this.lsb)
			{
				this.AppendInternal((int)((long)((int)(l >> 32)) & (long)((ulong)(-1))));
				this.AppendInternal((int)(l & (long)((ulong)(-1))));
			}
			else
			{
				this.AppendInternal((int)(l & (long)((ulong)(-1))));
				this.AppendInternal((int)((long)((int)(l >> 32)) & (long)((ulong)(-1))));
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000C420 File Offset: 0x0000A620
		public void Append(decimal d, int bytes)
		{
			int[] bits = decimal.GetBits(d);
			byte b = ((!(d > 0m)) ? 0 : 1);
			this.SendIfFull(bytes);
			this.Append(b);
			this.AppendInternal(bits[0]);
			this.AppendInternal(bits[1]);
			this.AppendInternal(bits[2]);
			this.AppendInternal(0);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000C480 File Offset: 0x0000A680
		public void Close()
		{
			if (this.stream == null)
			{
				return;
			}
			this.connReset = false;
			this.socket = null;
			try
			{
				this.stream.Close();
			}
			catch
			{
			}
			this.stream = null;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000C4E0 File Offset: 0x0000A6E0
		public bool IsConnected()
		{
			return this.socket != null && this.socket.Connected && (!this.socket.Poll(0, SelectMode.SelectRead) || this.socket.Available != 0);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000C534 File Offset: 0x0000A734
		public byte GetByte()
		{
			if (this.inBufferIndex >= this.inBufferLength)
			{
				this.GetPhysicalPacket();
			}
			return this.inBuffer[this.inBufferIndex++];
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000C574 File Offset: 0x0000A774
		public byte[] GetBytes(int len, bool exclusiveBuffer)
		{
			byte[] array;
			if (exclusiveBuffer || len > 16384)
			{
				array = new byte[len];
			}
			else
			{
				if (this.resBuffer.Length < len)
				{
					this.resBuffer = new byte[len];
				}
				array = this.resBuffer;
			}
			int i = 0;
			while (i < len)
			{
				if (this.inBufferIndex >= this.inBufferLength)
				{
					this.GetPhysicalPacket();
				}
				int num = this.inBufferLength - this.inBufferIndex;
				num = ((num <= len - i) ? num : (len - i));
				Buffer.BlockCopy(this.inBuffer, this.inBufferIndex, array, i, num);
				i += num;
				this.inBufferIndex += num;
			}
			return array;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000C630 File Offset: 0x0000A830
		public string GetString(int len, Encoding enc)
		{
			if (this.tdsVersion >= TdsVersion.tds70)
			{
				return this.GetString(len, true, null);
			}
			return this.GetString(len, false, null);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000C660 File Offset: 0x0000A860
		public string GetString(int len)
		{
			if (this.tdsVersion >= TdsVersion.tds70)
			{
				return this.GetString(len, true);
			}
			return this.GetString(len, false);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000C680 File Offset: 0x0000A880
		public string GetString(int len, bool wide, Encoding enc)
		{
			if (wide)
			{
				char[] array = new char[len];
				for (int i = 0; i < len; i++)
				{
					int num = (int)(this.GetByte() & byte.MaxValue);
					int num2 = (int)(this.GetByte() & byte.MaxValue);
					array[i] = (char)(num | (num2 << 8));
				}
				return new string(array);
			}
			byte[] array2 = new byte[len];
			Array.Copy(this.GetBytes(len, false), array2, len);
			if (enc != null)
			{
				return enc.GetString(array2);
			}
			return this.encoder.GetString(array2);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000C70C File Offset: 0x0000A90C
		public string GetString(int len, bool wide)
		{
			return this.GetString(len, wide, null);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000C718 File Offset: 0x0000A918
		public int GetNetShort()
		{
			return TdsComm.Ntohs(new byte[]
			{
				this.GetByte(),
				this.GetByte()
			}, 0);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000C748 File Offset: 0x0000A948
		public short GetTdsShort()
		{
			byte[] array = new byte[2];
			for (int i = 0; i < 2; i++)
			{
				array[i] = this.GetByte();
			}
			if (!BitConverter.IsLittleEndian)
			{
				return BitConverter.ToInt16(this.Swap(array), 0);
			}
			return BitConverter.ToInt16(array, 0);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000C798 File Offset: 0x0000A998
		public int GetTdsInt()
		{
			byte[] array = new byte[4];
			for (int i = 0; i < 4; i++)
			{
				array[i] = this.GetByte();
			}
			if (!BitConverter.IsLittleEndian)
			{
				return BitConverter.ToInt32(this.Swap(array), 0);
			}
			return BitConverter.ToInt32(array, 0);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000C7E8 File Offset: 0x0000A9E8
		public long GetTdsInt64()
		{
			byte[] array = new byte[8];
			for (int i = 0; i < 8; i++)
			{
				array[i] = this.GetByte();
			}
			if (!BitConverter.IsLittleEndian)
			{
				return BitConverter.ToInt64(this.Swap(array), 0);
			}
			return BitConverter.ToInt64(array, 0);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000C838 File Offset: 0x0000AA38
		private void GetPhysicalPacket()
		{
			int physicalPacketHeader = this.GetPhysicalPacketHeader();
			this.GetPhysicalPacketData(physicalPacketHeader);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000C854 File Offset: 0x0000AA54
		private int Read(byte[] buffer, int offset, int count)
		{
			int num;
			try
			{
				num = this.stream.Read(buffer, offset, count);
			}
			catch
			{
				this.socket = null;
				this.stream.Close();
				throw;
			}
			return num;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000C8B4 File Offset: 0x0000AAB4
		private int GetPhysicalPacketHeader()
		{
			int num;
			for (int i = 0; i < 8; i += num)
			{
				num = this.Read(this.tmpBuf, i, 8 - i);
				if (num <= 0)
				{
					this.socket = null;
					this.stream.Close();
					throw new IOException((num != 0) ? "Connection error" : "Connection lost");
				}
			}
			TdsPacketType tdsPacketType = (TdsPacketType)this.tmpBuf[0];
			if (tdsPacketType != TdsPacketType.Logon && tdsPacketType != TdsPacketType.Query && tdsPacketType != TdsPacketType.Reply)
			{
				throw new Exception(string.Format("Unknown packet type {0}", this.tmpBuf[0]));
			}
			int num2 = TdsComm.Ntohs(this.tmpBuf, 2) - 8;
			if (num2 >= this.inBuffer.Length)
			{
				this.inBuffer = new byte[num2];
			}
			if (num2 < 0)
			{
				throw new Exception(string.Format("Confused by a length of {0}", num2));
			}
			return num2;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000C99C File Offset: 0x0000AB9C
		private void GetPhysicalPacketData(int length)
		{
			int num;
			for (int i = 0; i < length; i += num)
			{
				num = this.Read(this.inBuffer, i, length - i);
				if (num <= 0)
				{
					this.socket = null;
					this.stream.Close();
					throw new IOException((num != 0) ? "Connection error" : "Connection lost");
				}
			}
			this.packetsReceived++;
			this.inBufferLength = length;
			this.inBufferIndex = 0;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000CA1C File Offset: 0x0000AC1C
		private static int Ntohs(byte[] buf, int offset)
		{
			int num = (int)(buf[offset + 1] & byte.MaxValue);
			int num2 = (int)(buf[offset] & byte.MaxValue) << 8;
			return num2 | num;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000CA44 File Offset: 0x0000AC44
		public byte Peek()
		{
			if (this.inBufferIndex >= this.inBufferLength)
			{
				this.GetPhysicalPacket();
			}
			return this.inBuffer[this.inBufferIndex];
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000CA78 File Offset: 0x0000AC78
		public bool Poll(int seconds, SelectMode selectMode)
		{
			return this.Poll(this.socket, seconds, selectMode);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000CA88 File Offset: 0x0000AC88
		private bool Poll(Socket s, int seconds, SelectMode selectMode)
		{
			long num;
			for (num = (long)(seconds * 1000000); num > 2147483647L; num -= 2147483647L)
			{
				bool flag = s.Poll(int.MaxValue, selectMode);
				if (flag)
				{
					return true;
				}
			}
			return s.Poll((int)num, selectMode);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000CAD8 File Offset: 0x0000ACD8
		internal void ResizeOutBuf(int newSize)
		{
			if (newSize != this.outBufferLength)
			{
				byte[] array = new byte[newSize];
				Buffer.BlockCopy(this.outBuffer, 0, array, 0, newSize);
				this.outBufferLength = newSize;
				this.outBuffer = array;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000130 RID: 304 RVA: 0x0000CB18 File Offset: 0x0000AD18
		// (set) Token: 0x06000131 RID: 305 RVA: 0x0000CB20 File Offset: 0x0000AD20
		public bool ResetConnection
		{
			get
			{
				return this.connReset;
			}
			set
			{
				this.connReset = value;
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000CB2C File Offset: 0x0000AD2C
		public void SendPacket()
		{
			if (this.packetType != TdsPacketType.Query && this.packetType != TdsPacketType.Proc)
			{
				this.connReset = false;
			}
			this.SendPhysicalPacket(true);
			this.nextOutBufferIndex = 0;
			this.packetType = TdsPacketType.None;
			this.connReset = false;
			this.packetsSent = 1;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000CB7C File Offset: 0x0000AD7C
		private void SendPhysicalPacket(bool isLastSegment)
		{
			if (this.nextOutBufferIndex > TdsComm.headerLength || this.packetType == TdsPacketType.Cancel)
			{
				byte b = (byte)(((!isLastSegment) ? 0 : 1) | ((!this.connReset) ? 0 : 8));
				this.Store(0, (byte)this.packetType);
				this.Store(1, b);
				this.Store(2, (short)this.nextOutBufferIndex);
				this.Store(4, 0);
				this.Store(5, 0);
				if (this.tdsVersion >= TdsVersion.tds70)
				{
					this.Store(6, (byte)this.packetsSent);
				}
				else
				{
					this.Store(6, 0);
				}
				this.Store(7, 0);
				this.stream.Write(this.outBuffer, 0, this.nextOutBufferIndex);
				this.stream.Flush();
				this.packetsSent++;
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000CC5C File Offset: 0x0000AE5C
		public void Skip(long i)
		{
			while (i > 0L)
			{
				this.GetByte();
				i -= 1L;
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000CC78 File Offset: 0x0000AE78
		public void StartPacket(TdsPacketType type)
		{
			if (type != TdsPacketType.Cancel && this.inBufferIndex != this.inBufferLength)
			{
				this.inBufferIndex = this.inBufferLength;
			}
			this.packetType = type;
			this.nextOutBufferIndex = TdsComm.headerLength;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000CCBC File Offset: 0x0000AEBC
		private void Store(int index, byte value)
		{
			this.outBuffer[index] = value;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000CCC8 File Offset: 0x0000AEC8
		private void Store(int index, short value)
		{
			this.outBuffer[index] = (byte)(value >> 8) & byte.MaxValue;
			this.outBuffer[index + 1] = (byte)value & byte.MaxValue;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000CCFC File Offset: 0x0000AEFC
		public IAsyncResult BeginReadPacket(AsyncCallback callback, object stateObject)
		{
			TdsAsyncResult tdsAsyncResult = new TdsAsyncResult(callback, stateObject);
			this.stream.BeginRead(this.tmpBuf, 0, 8, new AsyncCallback(this.OnReadPacketCallback), tdsAsyncResult);
			return tdsAsyncResult;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000CD34 File Offset: 0x0000AF34
		public int EndReadPacket(IAsyncResult ar)
		{
			if (!ar.IsCompleted)
			{
				ar.AsyncWaitHandle.WaitOne();
			}
			return (int)((TdsAsyncResult)ar).ReturnValue;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000CD68 File Offset: 0x0000AF68
		public void OnReadPacketCallback(IAsyncResult socketAsyncResult)
		{
			TdsAsyncResult tdsAsyncResult = (TdsAsyncResult)socketAsyncResult.AsyncState;
			int num;
			for (int i = this.stream.EndRead(socketAsyncResult); i < 8; i += num)
			{
				num = this.Read(this.tmpBuf, i, 8 - i);
				if (num <= 0)
				{
					this.socket = null;
					this.stream.Close();
					throw new IOException((num != 0) ? "Connection error" : "Connection lost");
				}
			}
			TdsPacketType tdsPacketType = (TdsPacketType)this.tmpBuf[0];
			if (tdsPacketType != TdsPacketType.Logon && tdsPacketType != TdsPacketType.Query && tdsPacketType != TdsPacketType.Reply)
			{
				throw new Exception(string.Format("Unknown packet type {0}", this.tmpBuf[0]));
			}
			int num2 = TdsComm.Ntohs(this.tmpBuf, 2) - 8;
			if (num2 >= this.inBuffer.Length)
			{
				this.inBuffer = new byte[num2];
			}
			if (num2 < 0)
			{
				throw new Exception(string.Format("Confused by a length of {0}", num2));
			}
			this.GetPhysicalPacketData(num2);
			int num3 = num2 + 8;
			tdsAsyncResult.ReturnValue = num3;
			tdsAsyncResult.MarkComplete();
		}

		// Token: 0x040000AA RID: 170
		private NetworkStream stream;

		// Token: 0x040000AB RID: 171
		private int packetSize;

		// Token: 0x040000AC RID: 172
		private TdsPacketType packetType;

		// Token: 0x040000AD RID: 173
		private bool connReset;

		// Token: 0x040000AE RID: 174
		private Encoding encoder;

		// Token: 0x040000AF RID: 175
		private string dataSource;

		// Token: 0x040000B0 RID: 176
		private int commandTimeout;

		// Token: 0x040000B1 RID: 177
		private byte[] outBuffer;

		// Token: 0x040000B2 RID: 178
		private int outBufferLength;

		// Token: 0x040000B3 RID: 179
		private int nextOutBufferIndex;

		// Token: 0x040000B4 RID: 180
		private bool lsb;

		// Token: 0x040000B5 RID: 181
		private byte[] inBuffer;

		// Token: 0x040000B6 RID: 182
		private int inBufferLength;

		// Token: 0x040000B7 RID: 183
		private int inBufferIndex;

		// Token: 0x040000B8 RID: 184
		private static int headerLength = 8;

		// Token: 0x040000B9 RID: 185
		private byte[] tmpBuf = new byte[8];

		// Token: 0x040000BA RID: 186
		private byte[] resBuffer = new byte[256];

		// Token: 0x040000BB RID: 187
		private int packetsSent;

		// Token: 0x040000BC RID: 188
		private int packetsReceived;

		// Token: 0x040000BD RID: 189
		private Socket socket;

		// Token: 0x040000BE RID: 190
		private TdsVersion tdsVersion;
	}
}
