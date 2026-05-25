using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Mono.Security.Protocol.Tls;

namespace System.Net
{
	// Token: 0x02000311 RID: 785
	internal sealed class HttpConnection
	{
		// Token: 0x06001B55 RID: 6997 RVA: 0x0004DAC4 File Offset: 0x0004BCC4
		public HttpConnection(global::System.Net.Sockets.Socket sock, EndPointListener epl, bool secure, global::System.Security.Cryptography.X509Certificates.X509Certificate2 cert, AsymmetricAlgorithm key)
		{
			this.sock = sock;
			this.epl = epl;
			this.secure = secure;
			this.key = key;
			if (!secure)
			{
				this.stream = new global::System.Net.Sockets.NetworkStream(sock, false);
			}
			else
			{
				SslServerStream sslServerStream = new SslServerStream(new global::System.Net.Sockets.NetworkStream(sock, false), cert, false, false);
				SslServerStream sslServerStream2 = sslServerStream;
				sslServerStream2.PrivateKeyCertSelectionDelegate = (PrivateKeySelectionCallback)Delegate.Combine(sslServerStream2.PrivateKeyCertSelectionDelegate, new PrivateKeySelectionCallback(this.OnPVKSelection));
				this.stream = sslServerStream;
			}
			this.Init();
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x0004DB4C File Offset: 0x0004BD4C
		private AsymmetricAlgorithm OnPVKSelection(X509Certificate certificate, string targetHost)
		{
			return this.key;
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x0004DB54 File Offset: 0x0004BD54
		private void Init()
		{
			this.context_bound = false;
			this.i_stream = null;
			this.o_stream = null;
			this.prefix = null;
			this.chunked = false;
			this.ms = new MemoryStream();
			this.position = 0;
			this.input_state = HttpConnection.InputState.RequestLine;
			this.line_state = HttpConnection.LineState.None;
			this.context = new HttpListenerContext(this);
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001B58 RID: 7000 RVA: 0x0004DBB0 File Offset: 0x0004BDB0
		public int ChunkedUses
		{
			get
			{
				return this.chunked_uses;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001B59 RID: 7001 RVA: 0x0004DBB8 File Offset: 0x0004BDB8
		public IPEndPoint LocalEndPoint
		{
			get
			{
				return (IPEndPoint)this.sock.LocalEndPoint;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001B5A RID: 7002 RVA: 0x0004DBCC File Offset: 0x0004BDCC
		public IPEndPoint RemoteEndPoint
		{
			get
			{
				return (IPEndPoint)this.sock.RemoteEndPoint;
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001B5B RID: 7003 RVA: 0x0004DBE0 File Offset: 0x0004BDE0
		public bool IsSecure
		{
			get
			{
				return this.secure;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001B5C RID: 7004 RVA: 0x0004DBE8 File Offset: 0x0004BDE8
		// (set) Token: 0x06001B5D RID: 7005 RVA: 0x0004DBF0 File Offset: 0x0004BDF0
		public ListenerPrefix Prefix
		{
			get
			{
				return this.prefix;
			}
			set
			{
				this.prefix = value;
			}
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x0004DBFC File Offset: 0x0004BDFC
		public void BeginReadRequest()
		{
			if (this.buffer == null)
			{
				this.buffer = new byte[8192];
			}
			try
			{
				this.stream.BeginRead(this.buffer, 0, 8192, new AsyncCallback(this.OnRead), this);
			}
			catch
			{
				this.CloseSocket();
			}
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x0004DC78 File Offset: 0x0004BE78
		public RequestStream GetRequestStream(bool chunked, long contentlength)
		{
			if (this.i_stream == null)
			{
				byte[] array = this.ms.GetBuffer();
				int num = (int)this.ms.Length;
				this.ms = null;
				if (chunked)
				{
					this.chunked = true;
					this.context.Response.SendChunked = true;
					this.i_stream = new ChunkedInputStream(this.context, this.stream, array, this.position, num - this.position);
				}
				else
				{
					this.i_stream = new RequestStream(this.stream, array, this.position, num - this.position, contentlength);
				}
			}
			return this.i_stream;
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x0004DD20 File Offset: 0x0004BF20
		public ResponseStream GetResponseStream()
		{
			if (this.o_stream == null)
			{
				HttpListener listener = this.context.Listener;
				bool flag = listener == null || listener.IgnoreWriteExceptions;
				this.o_stream = new ResponseStream(this.stream, this.context.Response, flag);
			}
			return this.o_stream;
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x0004DD7C File Offset: 0x0004BF7C
		private void OnRead(IAsyncResult ares)
		{
			HttpConnection httpConnection = (HttpConnection)ares.AsyncState;
			int num = -1;
			try
			{
				num = this.stream.EndRead(ares);
				this.ms.Write(this.buffer, 0, num);
				if (this.ms.Length > 32768L)
				{
					this.SendError("Bad request", 400);
					this.Close(true);
					return;
				}
			}
			catch
			{
				if (this.ms != null && this.ms.Length > 0L)
				{
					this.SendError();
				}
				if (this.sock != null)
				{
					this.CloseSocket();
				}
				return;
			}
			if (num == 0)
			{
				this.CloseSocket();
				return;
			}
			if (this.ProcessInput(this.ms))
			{
				if (!this.context.HaveError)
				{
					this.context.Request.FinishInitialization();
				}
				if (this.context.HaveError)
				{
					this.SendError();
					this.Close(true);
					return;
				}
				if (!this.epl.BindContext(this.context))
				{
					this.SendError("Invalid host", 400);
					this.Close(true);
				}
				this.context_bound = true;
				return;
			}
			else
			{
				this.stream.BeginRead(this.buffer, 0, 8192, new AsyncCallback(this.OnRead), httpConnection);
			}
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x0004DF00 File Offset: 0x0004C100
		private bool ProcessInput(MemoryStream ms)
		{
			byte[] array = ms.GetBuffer();
			int num = (int)ms.Length;
			int num2 = 0;
			string text;
			try
			{
				text = this.ReadLine(array, this.position, num - this.position, ref num2);
				this.position += num2;
			}
			catch (Exception ex)
			{
				this.context.ErrorMessage = "Bad request";
				this.context.ErrorStatus = 400;
				return true;
			}
			while (text != null)
			{
				if (text == string.Empty)
				{
					if (this.input_state != HttpConnection.InputState.RequestLine)
					{
						this.current_line = null;
						ms = null;
						return true;
					}
				}
				else
				{
					if (this.input_state == HttpConnection.InputState.RequestLine)
					{
						this.context.Request.SetRequestLine(text);
						this.input_state = HttpConnection.InputState.Headers;
					}
					else
					{
						try
						{
							this.context.Request.AddHeader(text);
						}
						catch (Exception ex2)
						{
							this.context.ErrorMessage = ex2.Message;
							this.context.ErrorStatus = 400;
							return true;
						}
					}
					if (this.context.HaveError)
					{
						return true;
					}
					if (this.position >= num)
					{
						break;
					}
					try
					{
						text = this.ReadLine(array, this.position, num - this.position, ref num2);
						this.position += num2;
					}
					catch (Exception ex3)
					{
						this.context.ErrorMessage = "Bad request";
						this.context.ErrorStatus = 400;
						return true;
					}
				}
				if (text != null)
				{
					continue;
				}
				IL_0194:
				if (num2 == num)
				{
					ms.SetLength(0L);
					this.position = 0;
				}
				return false;
			}
			goto IL_0194;
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x0004E108 File Offset: 0x0004C308
		private string ReadLine(byte[] buffer, int offset, int len, ref int used)
		{
			if (this.current_line == null)
			{
				this.current_line = new StringBuilder();
			}
			int num = offset + len;
			used = 0;
			int num2 = offset;
			while (num2 < num && this.line_state != HttpConnection.LineState.LF)
			{
				used++;
				byte b = buffer[num2];
				if (b == 13)
				{
					this.line_state = HttpConnection.LineState.CR;
				}
				else if (b == 10)
				{
					this.line_state = HttpConnection.LineState.LF;
				}
				else
				{
					this.current_line.Append((char)b);
				}
				num2++;
			}
			string text = null;
			if (this.line_state == HttpConnection.LineState.LF)
			{
				this.line_state = HttpConnection.LineState.None;
				text = this.current_line.ToString();
				this.current_line.Length = 0;
			}
			return text;
		}

		// Token: 0x06001B64 RID: 7012 RVA: 0x0004E1C4 File Offset: 0x0004C3C4
		public void SendError(string msg, int status)
		{
			try
			{
				HttpListenerResponse response = this.context.Response;
				response.StatusCode = status;
				response.ContentType = "text/html";
				string statusDescription = HttpListenerResponse.GetStatusDescription(status);
				string text;
				if (msg != null)
				{
					text = string.Format("<h1>{0} ({1})</h1>", statusDescription, msg);
				}
				else
				{
					text = string.Format("<h1>{0}</h1>", statusDescription);
				}
				byte[] bytes = this.context.Response.ContentEncoding.GetBytes(text);
				response.Close(bytes, false);
			}
			catch
			{
			}
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x0004E260 File Offset: 0x0004C460
		public void SendError()
		{
			this.SendError(this.context.ErrorMessage, this.context.ErrorStatus);
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x0004E280 File Offset: 0x0004C480
		private void Unbind()
		{
			if (this.context_bound)
			{
				this.epl.UnbindContext(this.context);
				this.context_bound = false;
			}
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x0004E2A8 File Offset: 0x0004C4A8
		public void Close()
		{
			this.Close(false);
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x0004E2B4 File Offset: 0x0004C4B4
		private void CloseSocket()
		{
			if (this.sock == null)
			{
				return;
			}
			try
			{
				this.sock.Close();
			}
			catch
			{
			}
			finally
			{
				this.sock = null;
			}
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x0004E320 File Offset: 0x0004C520
		internal void Close(bool force_close)
		{
			if (this.sock != null)
			{
				Stream responseStream = this.GetResponseStream();
				responseStream.Close();
				this.o_stream = null;
			}
			if (this.sock == null)
			{
				return;
			}
			force_close |= this.context.Request.Headers["connection"] == "close";
			if (!force_close)
			{
				int statusCode = this.context.Response.StatusCode;
				bool flag = statusCode == 400 || statusCode == 408 || statusCode == 411 || statusCode == 413 || statusCode == 414 || statusCode == 500 || statusCode == 503;
				force_close |= this.context.Request.ProtocolVersion <= HttpVersion.Version10;
			}
			if (force_close || !this.context.Request.FlushInput())
			{
				global::System.Net.Sockets.Socket socket = this.sock;
				this.sock = null;
				try
				{
					if (socket != null)
					{
						socket.Shutdown(global::System.Net.Sockets.SocketShutdown.Both);
					}
				}
				catch
				{
				}
				finally
				{
					if (socket != null)
					{
						socket.Close();
					}
				}
				this.Unbind();
				return;
			}
			if (this.chunked && !this.context.Response.ForceCloseChunked)
			{
				this.chunked_uses++;
				this.Unbind();
				this.Init();
				this.BeginReadRequest();
				return;
			}
			this.Unbind();
			this.Init();
			this.BeginReadRequest();
		}

		// Token: 0x040010E6 RID: 4326
		private const int BufferSize = 8192;

		// Token: 0x040010E7 RID: 4327
		private global::System.Net.Sockets.Socket sock;

		// Token: 0x040010E8 RID: 4328
		private Stream stream;

		// Token: 0x040010E9 RID: 4329
		private EndPointListener epl;

		// Token: 0x040010EA RID: 4330
		private MemoryStream ms;

		// Token: 0x040010EB RID: 4331
		private byte[] buffer;

		// Token: 0x040010EC RID: 4332
		private HttpListenerContext context;

		// Token: 0x040010ED RID: 4333
		private StringBuilder current_line;

		// Token: 0x040010EE RID: 4334
		private ListenerPrefix prefix;

		// Token: 0x040010EF RID: 4335
		private RequestStream i_stream;

		// Token: 0x040010F0 RID: 4336
		private ResponseStream o_stream;

		// Token: 0x040010F1 RID: 4337
		private bool chunked;

		// Token: 0x040010F2 RID: 4338
		private int chunked_uses;

		// Token: 0x040010F3 RID: 4339
		private bool context_bound;

		// Token: 0x040010F4 RID: 4340
		private bool secure;

		// Token: 0x040010F5 RID: 4341
		private AsymmetricAlgorithm key;

		// Token: 0x040010F6 RID: 4342
		private HttpConnection.InputState input_state;

		// Token: 0x040010F7 RID: 4343
		private HttpConnection.LineState line_state;

		// Token: 0x040010F8 RID: 4344
		private int position;

		// Token: 0x02000312 RID: 786
		private enum InputState
		{
			// Token: 0x040010FA RID: 4346
			RequestLine,
			// Token: 0x040010FB RID: 4347
			Headers
		}

		// Token: 0x02000313 RID: 787
		private enum LineState
		{
			// Token: 0x040010FD RID: 4349
			None,
			// Token: 0x040010FE RID: 4350
			CR,
			// Token: 0x040010FF RID: 4351
			LF
		}
	}
}
