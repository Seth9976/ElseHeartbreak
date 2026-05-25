using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents a mutable reference type that wraps either a <see cref="P:System.Data.SqlTypes.SqlBytes.Buffer" /> or a <see cref="P:System.Data.SqlTypes.SqlBytes.Stream" />.</summary>
	// Token: 0x02000104 RID: 260
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public sealed class SqlBytes : IXmlSerializable, INullable, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlBytes" /> class.</summary>
		// Token: 0x06000CE6 RID: 3302 RVA: 0x00035CA8 File Offset: 0x00033EA8
		public SqlBytes()
		{
			this.buffer = null;
			this.notNull = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlBytes" /> class based on the specified byte array.</summary>
		/// <param name="buffer">The array of unsigned bytes. </param>
		// Token: 0x06000CE7 RID: 3303 RVA: 0x00035CC8 File Offset: 0x00033EC8
		public SqlBytes(byte[] buffer)
		{
			if (buffer == null)
			{
				this.notNull = false;
				buffer = null;
			}
			else
			{
				this.notNull = true;
				this.buffer = buffer;
				this.storage = StorageState.Buffer;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlBytes" /> class based on the specified <see cref="T:System.Data.SqlTypes.SqlBinary" /> value.</summary>
		/// <param name="value">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> value.</param>
		// Token: 0x06000CE8 RID: 3304 RVA: 0x00035D04 File Offset: 0x00033F04
		public SqlBytes(SqlBinary value)
		{
			if (value.IsNull)
			{
				this.notNull = false;
				this.buffer = null;
			}
			else
			{
				this.notNull = true;
				this.buffer = value.Value;
				this.storage = StorageState.Buffer;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlBytes" /> class based on the specified <see cref="T:System.IO.Stream" /> value.</summary>
		/// <param name="s">A <see cref="T:System.IO.Stream" />. </param>
		// Token: 0x06000CE9 RID: 3305 RVA: 0x00035D58 File Offset: 0x00033F58
		public SqlBytes(Stream s)
		{
			if (s == null)
			{
				this.notNull = false;
				this.buffer = null;
			}
			else
			{
				this.notNull = true;
				int num = (int)s.Length;
				this.buffer = new byte[num];
				s.Read(this.buffer, 0, num);
				this.storage = StorageState.Stream;
				this.stream = s;
			}
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</returns>
		// Token: 0x06000CEA RID: 3306 RVA: 0x00035DC4 File Offset: 0x00033FC4
		[MonoTODO]
		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new NotImplementedException();
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="r">XmlReader</param>
		// Token: 0x06000CEB RID: 3307 RVA: 0x00035DCC File Offset: 0x00033FCC
		[MonoTODO]
		void IXmlSerializable.ReadXml(XmlReader r)
		{
			throw new NotImplementedException();
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">XmlWriter</param>
		// Token: 0x06000CEC RID: 3308 RVA: 0x00035DD4 File Offset: 0x00033FD4
		[MonoTODO]
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets serialization information with all the data needed to reinstantiate this <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance.</summary>
		/// <param name="info">The object to be populated with serialization information. </param>
		/// <param name="context">The destination context of the serialization.</param>
		// Token: 0x06000CED RID: 3309 RVA: 0x00035DDC File Offset: 0x00033FDC
		[MonoTODO]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a reference to the internal buffer. </summary>
		/// <returns>Returns a reference to the internal buffer. For <see cref="T:System.Data.SqlTypes.SqlBytes" /> instances created on top of unmanaged pointers, it returns a managed copy of the internal buffer.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x00035DE4 File Offset: 0x00033FE4
		public byte[] Buffer
		{
			get
			{
				return this.buffer;
			}
		}

		/// <summary>Gets a Boolean value that indicates whether this <see cref="T:System.Data.SqlTypes.SqlBytes" /> is null.</summary>
		/// <returns>true if the <see cref="T:System.Data.SqlTypes.SqlBytes" /> is null, false otherwise.</returns>
		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000CEF RID: 3311 RVA: 0x00035DEC File Offset: 0x00033FEC
		public bool IsNull
		{
			get
			{
				return !this.notNull;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance at the specified index.</summary>
		/// <returns>A <see cref="T:System.Byte" /> value. </returns>
		/// <param name="offset">A <see cref="T:System.Int64" /> value.</param>
		// Token: 0x17000261 RID: 609
		public byte this[long offset]
		{
			get
			{
				if (this.buffer == null)
				{
					throw new SqlNullValueException("Data is Null");
				}
				if (offset < 0L || offset >= (long)this.buffer.Length)
				{
					throw new ArgumentOutOfRangeException("Parameter name: offset");
				}
				return this.buffer[(int)(checked((IntPtr)offset))];
			}
			set
			{
				if (this.notNull && offset >= 0L && offset < (long)this.buffer.Length)
				{
					this.buffer[(int)(checked((IntPtr)offset))] = value;
				}
			}
		}

		/// <summary>Gets the length of the value that is contained in the <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance.</summary>
		/// <returns>A <see cref="T:System.Int64" /> value representing the length of the value that is contained in the <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance. Returns -1 if no buffer is available to the instance or if the value is null. Returns a <see cref="P:System.IO.Stream.Length" /> for a stream-wrapped instance.</returns>
		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x00035E84 File Offset: 0x00034084
		public long Length
		{
			get
			{
				if (!this.notNull || this.buffer == null)
				{
					throw new SqlNullValueException("Data is Null");
				}
				if (this.buffer.Length < 0)
				{
					return -1L;
				}
				return (long)this.buffer.Length;
			}
		}

		/// <summary>Gets the maximum length of the value of the internal buffer of this <see cref="T:System.Data.SqlTypes.SqlBytes" />.</summary>
		/// <returns>A long representing the maximum length of the value of the internal buffer. Returns -1 for a stream-wrapped <see cref="T:System.Data.SqlTypes.SqlBytes" />.</returns>
		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x00035EC4 File Offset: 0x000340C4
		public long MaxLength
		{
			get
			{
				if (!this.notNull || this.buffer == null || this.storage == StorageState.Stream)
				{
					return -1L;
				}
				return (long)this.buffer.Length;
			}
		}

		/// <summary>Returns a null instance of this <see cref="T:System.Data.SqlTypes.SqlBytes" />.</summary>
		/// <returns>Returns an instance in such a way that <see cref="P:System.Data.SqlTypes.SqlBytes.IsNull" /> returns true.</returns>
		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x00035F00 File Offset: 0x00034100
		public static SqlBytes Null
		{
			get
			{
				return new SqlBytes();
			}
		}

		/// <summary>Returns information about the storage state of this <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.StorageState" /> enumeration.</returns>
		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x00035F08 File Offset: 0x00034108
		public StorageState Storage
		{
			get
			{
				if (this.storage == StorageState.UnmanagedBuffer)
				{
					throw new SqlNullValueException("Data is Null");
				}
				return this.storage;
			}
		}

		/// <summary>Gets or sets the data of this <see cref="T:System.Data.SqlTypes.SqlBytes" /> as a stream.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" />.</returns>
		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x00035F34 File Offset: 0x00034134
		// (set) Token: 0x06000CF6 RID: 3318 RVA: 0x00035F28 File Offset: 0x00034128
		public Stream Stream
		{
			get
			{
				return this.stream;
			}
			set
			{
				this.stream = value;
			}
		}

		/// <summary>Returns a managed copy of the value held by this <see cref="T:System.Data.SqlTypes.SqlBytes" />.</summary>
		/// <returns>The value of this <see cref="T:System.Data.SqlTypes.SqlBytes" /> as an array of bytes.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x00035F3C File Offset: 0x0003413C
		public byte[] Value
		{
			get
			{
				if (this.buffer == null)
				{
					return this.buffer;
				}
				return (byte[])this.buffer.Clone();
			}
		}

		/// <summary>Sets the length of this <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance.</summary>
		/// <param name="value">The <see cref="T:System.Int64" /> long value representing the length.</param>
		// Token: 0x06000CF9 RID: 3321 RVA: 0x00035F6C File Offset: 0x0003416C
		public void SetLength(long value)
		{
			if (this.buffer == null)
			{
				throw new SqlTypeException("There is no buffer. Read or write operation failed.");
			}
			if (value < 0L || value > (long)this.buffer.Length)
			{
				throw new ArgumentOutOfRangeException("Specified argument was out of the range of valid values.");
			}
			Array.Resize<byte>(ref this.buffer, (int)value);
		}

		/// <summary>Sets this <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance to null.</summary>
		// Token: 0x06000CFA RID: 3322 RVA: 0x00035FC0 File Offset: 0x000341C0
		public void SetNull()
		{
			this.buffer = null;
			this.notNull = false;
		}

		/// <summary>Constructs and returns a <see cref="T:System.Data.SqlTypes.SqlBinary" /> from this <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBinary" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000CFB RID: 3323 RVA: 0x00035FD0 File Offset: 0x000341D0
		public SqlBinary ToSqlBinary()
		{
			return new SqlBinary(this.buffer);
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>A string that indicates the XSD of the specified XmlSchemaSet.</returns>
		/// <param name="schemaSet">A <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x06000CFC RID: 3324 RVA: 0x00035FE0 File Offset: 0x000341E0
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("base64Binary", "http://www.w3.org/2001/XMLSchema");
		}

		/// <summary>Copies bytes from this <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance to the passed-in buffer and returns the number of copied bytes.</summary>
		/// <returns>An <see cref="T:System.Int64" /> long value representing the number of copied bytes.</returns>
		/// <param name="offset">An <see cref="T:System.Int64" /> long value offset into the value that is contained in the <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance.</param>
		/// <param name="buffer">The byte array buffer to copy into.</param>
		/// <param name="offsetInBuffer">An <see cref="T:System.Int32" /> integer offset into the buffer to start copying into.</param>
		/// <param name="count">An <see cref="T:System.Int32" /> integer representing the number of bytes to copy.</param>
		// Token: 0x06000CFD RID: 3325 RVA: 0x00036000 File Offset: 0x00034200
		public long Read(long offset, byte[] buffer, int offsetInBuffer, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (this.IsNull)
			{
				throw new SqlNullValueException("There is no buffer. Read or write failed");
			}
			if ((long)count > this.MaxLength || count > buffer.Length || count < 0 || offsetInBuffer + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (offset < 0L || offset > this.MaxLength)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offsetInBuffer < 0 || offsetInBuffer > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offsetInBuffer");
			}
			long num = (long)count;
			if ((long)count + offset > this.Length)
			{
				num = this.Length - offset;
			}
			Array.Copy(this.buffer, offset, buffer, (long)offsetInBuffer, num);
			return num;
		}

		/// <summary>Copies bytes from the passed-in buffer to this <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance.</summary>
		/// <param name="offset">An <see cref="T:System.Int64" /> long value offset into the value that is contained in the <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance.</param>
		/// <param name="buffer">The byte array buffer to copy into.</param>
		/// <param name="offsetInBuffer">An <see cref="T:System.Int32" /> integer offset into the buffer to start copying into.</param>
		/// <param name="count">An <see cref="T:System.Int32" /> integer representing the number of bytes to copy.</param>
		// Token: 0x06000CFE RID: 3326 RVA: 0x000360D4 File Offset: 0x000342D4
		public void Write(long offset, byte[] buffer, int offsetInBuffer, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (this.IsNull)
			{
				throw new SqlTypeException("There is no buffer. Read or write operation failed.");
			}
			if (offset < 0L)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offsetInBuffer < 0 || offsetInBuffer > buffer.Length || (long)offsetInBuffer > this.Length || (long)(offsetInBuffer + count) > this.Length || offsetInBuffer + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offsetInBuffer");
			}
			if (count < 0 || (long)count > this.MaxLength)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (offset > this.MaxLength || offset + (long)count > this.MaxLength)
			{
				throw new SqlTypeException("The buffer is insufficient. Read or write operation failed.");
			}
			if ((long)count + offset > this.Length && (long)count + offset <= this.MaxLength)
			{
				this.SetLength((long)count);
			}
			Array.Copy(buffer, (long)offsetInBuffer, this.buffer, offset, (long)count);
		}

		/// <summary>Converts a <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure to a <see cref="T:System.Data.SqlTypes.SqlBytes" /> structure.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBytes" /> structure.</returns>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure to be converted.</param>
		// Token: 0x06000CFF RID: 3327 RVA: 0x000361E4 File Offset: 0x000343E4
		public static explicit operator SqlBytes(SqlBinary value)
		{
			if (value.IsNull)
			{
				return SqlBytes.Null;
			}
			return new SqlBytes(value.Value);
		}

		/// <summary>Converts a <see cref="T:System.Data.SqlTypes.SqlBytes" /> structure to a <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure.</returns>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlBytes" /> structure to be converted.</param>
		// Token: 0x06000D00 RID: 3328 RVA: 0x00036204 File Offset: 0x00034404
		public static explicit operator SqlBinary(SqlBytes value)
		{
			if (value.IsNull)
			{
				return SqlBinary.Null;
			}
			return new SqlBinary(value.Value);
		}

		// Token: 0x040004D7 RID: 1239
		private bool notNull;

		// Token: 0x040004D8 RID: 1240
		private byte[] buffer;

		// Token: 0x040004D9 RID: 1241
		private StorageState storage = StorageState.UnmanagedBuffer;

		// Token: 0x040004DA RID: 1242
		private Stream stream;
	}
}
