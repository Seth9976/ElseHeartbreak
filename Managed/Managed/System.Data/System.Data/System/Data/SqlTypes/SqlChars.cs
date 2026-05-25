using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>
	///   <see cref="T:System.Data.SqlTypes.SqlChars" /> is a mutable reference type that wraps a <see cref="T:System.Char" /> array or a <see cref="T:System.Data.SqlTypes.SqlString" /> instance.</summary>
	// Token: 0x02000105 RID: 261
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public sealed class SqlChars : IXmlSerializable, INullable, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlChars" /> class.</summary>
		// Token: 0x06000D01 RID: 3329 RVA: 0x00036224 File Offset: 0x00034424
		public SqlChars()
		{
			this.notNull = false;
			this.buffer = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlChars" /> class based on the specified character array.</summary>
		/// <param name="buffer">A <see cref="T:System.Char" /> array.</param>
		// Token: 0x06000D02 RID: 3330 RVA: 0x00036244 File Offset: 0x00034444
		public SqlChars(char[] buffer)
		{
			if (buffer == null)
			{
				this.notNull = false;
				this.buffer = null;
			}
			else
			{
				this.notNull = true;
				this.buffer = buffer;
				this.storage = StorageState.Buffer;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlChars" /> class based on the specified <see cref="T:System.Data.SqlTypes.SqlString" /> value.</summary>
		/// <param name="value">A <see cref="T:System.Data.SqlTypes.SqlString" />.</param>
		// Token: 0x06000D03 RID: 3331 RVA: 0x00036284 File Offset: 0x00034484
		public SqlChars(SqlString value)
		{
			if (value.IsNull)
			{
				this.notNull = false;
				this.buffer = null;
			}
			else
			{
				this.notNull = true;
				this.buffer = value.Value.ToCharArray();
				this.storage = StorageState.Buffer;
			}
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</returns>
		// Token: 0x06000D04 RID: 3332 RVA: 0x000362E0 File Offset: 0x000344E0
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="r">XmlReader</param>
		// Token: 0x06000D05 RID: 3333 RVA: 0x000362E4 File Offset: 0x000344E4
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			if (reader == null)
			{
				return;
			}
			switch (reader.ReadState)
			{
			case ReadState.Error:
			case ReadState.EndOfFile:
			case ReadState.Closed:
				return;
			default:
				reader.MoveToContent();
				if (reader.EOF)
				{
					return;
				}
				reader.Read();
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					return;
				}
				if (reader.Value.Length > 0)
				{
					if (string.Compare("Null", reader.Value) == 0)
					{
						this.notNull = false;
						return;
					}
					this.buffer = reader.Value.ToCharArray();
					this.notNull = true;
					this.storage = StorageState.Buffer;
				}
				return;
			}
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">XmlWriter</param>
		// Token: 0x06000D06 RID: 3334 RVA: 0x00036390 File Offset: 0x00034590
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteString(this.buffer.ToString());
		}

		/// <summary>Gets serialization information with all the data needed to reinstantiate this <see cref="T:System.Data.SqlTypes.SqlChars" /> instance.</summary>
		/// <param name="info">The object to be populated with serialization information. </param>
		/// <param name="context">The destination context of the serialization.</param>
		// Token: 0x06000D07 RID: 3335 RVA: 0x000363A4 File Offset: 0x000345A4
		[MonoTODO]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a reference to the internal buffer. </summary>
		/// <returns>Returns a reference to the internal buffer. For <see cref="T:System.Data.SqlTypes.SqlChars" /> instances created on top of unmanaged pointers, it returns a managed copy of the internal buffer.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x000363AC File Offset: 0x000345AC
		public char[] Buffer
		{
			get
			{
				return this.buffer;
			}
		}

		/// <summary>Gets a Boolean value that indicates whether this <see cref="T:System.Data.SqlTypes.SqlChars" /> is null.</summary>
		/// <returns>true if the <see cref="T:System.Data.SqlTypes.SqlChars" /> is null. Otherwise, false. </returns>
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000D09 RID: 3337 RVA: 0x000363B4 File Offset: 0x000345B4
		public bool IsNull
		{
			get
			{
				return !this.notNull;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.SqlTypes.SqlChars" /> instance at the specified index.</summary>
		/// <returns>A <see cref="T:System.Char" /> value. </returns>
		/// <param name="offset">An <see cref="T:System.Int64" /> value.</param>
		// Token: 0x1700026A RID: 618
		public char this[long offset]
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

		/// <summary>Gets the length of the value that is contained in the <see cref="T:System.Data.SqlTypes.SqlChars" /> instance.</summary>
		/// <returns>A <see cref="T:System.Int64" /> value that indicates the length in characters of the value that is contained in the <see cref="T:System.Data.SqlTypes.SqlChars" /> instance.Returns -1 if no buffer is available to the instance, or if the value is null. Returns a <see cref="P:System.IO.Stream.Length" /> for a stream-wrapped instance.</returns>
		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x0003644C File Offset: 0x0003464C
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

		/// <summary>Gets the maximum length in two-byte characters of the value the internal buffer can hold.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value representing the maximum length in two-byte characters of the value of the internal buffer. Returns -1 for a stream-wrapped <see cref="T:System.Data.SqlTypes.SqlChars" />.</returns>
		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000D0D RID: 3341 RVA: 0x0003648C File Offset: 0x0003468C
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

		/// <summary>Returns a null instance of this <see cref="T:System.Data.SqlTypes.SqlChars" />.</summary>
		/// <returns>Returns an instance in such a way that <see cref="P:System.Data.SqlTypes.SqlChars.IsNull" /> returns true. For more information, see Handling Null Values (ADO.NET).</returns>
		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x000364C8 File Offset: 0x000346C8
		public static SqlChars Null
		{
			get
			{
				return new SqlChars();
			}
		}

		/// <summary>Returns information about the storage state of this <see cref="T:System.Data.SqlTypes.SqlChars" /> instance.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.StorageState" /> enumeration.</returns>
		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x000364D0 File Offset: 0x000346D0
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

		/// <summary>Returns a managed copy of the value held by this <see cref="T:System.Data.SqlTypes.SqlChars" />.</summary>
		/// <returns>The value of this <see cref="T:System.Data.SqlTypes.SqlChars" /> as an array of characters.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x000364F0 File Offset: 0x000346F0
		public char[] Value
		{
			get
			{
				if (this.buffer == null)
				{
					return this.buffer;
				}
				return (char[])this.buffer.Clone();
			}
		}

		/// <summary>Sets the length of this <see cref="T:System.Data.SqlTypes.SqlChars" /> instance.</summary>
		/// <param name="value">The <see cref="T:System.Int64" />long value representing the length.</param>
		// Token: 0x06000D11 RID: 3345 RVA: 0x00036520 File Offset: 0x00034720
		public void SetLength(long value)
		{
			if (this.buffer == null)
			{
				throw new SqlTypeException("There is no buffer");
			}
			if (value < 0L || value > (long)this.buffer.Length)
			{
				throw new ArgumentOutOfRangeException("Specified argument was out of the range of valid values.");
			}
			Array.Resize<char>(ref this.buffer, (int)value);
		}

		/// <summary>Sets this <see cref="T:System.Data.SqlTypes.SqlChars" /> instance to null.</summary>
		// Token: 0x06000D12 RID: 3346 RVA: 0x00036574 File Offset: 0x00034774
		public void SetNull()
		{
			this.buffer = null;
			this.notNull = false;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlChars" /> instance to its equivalent <see cref="T:System.Data.SqlTypes.SqlString" /> representation.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlString" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D13 RID: 3347 RVA: 0x00036584 File Offset: 0x00034784
		public SqlString ToSqlString()
		{
			if (this.buffer == null)
			{
				return SqlString.Null;
			}
			return new SqlString(this.buffer.ToString());
		}

		/// <summary>Copies characters from this <see cref="T:System.Data.SqlTypes.SqlChars" /> instance to the passed-in buffer and returns the number of copied characters.</summary>
		/// <returns>An <see cref="T:System.Int64" />long value representing the number of copied bytes.</returns>
		/// <param name="offset">An <see cref="T:System.Int64" />long value offset into the value that is contained in the <see cref="T:System.Data.SqlTypes.SqlChars" /> instance.</param>
		/// <param name="buffer">The character array buffer to copy into.</param>
		/// <param name="offsetInBuffer">An <see cref="T:System.Int32" /> integer offset into the buffer to start copying into.</param>
		/// <param name="count">An <see cref="T:System.Int32" /> integer value representing the number of characters to copy.</param>
		// Token: 0x06000D14 RID: 3348 RVA: 0x000365A8 File Offset: 0x000347A8
		public long Read(long offset, char[] buffer, int offsetInBuffer, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (this.IsNull)
			{
				throw new SqlNullValueException("There is no buffer. Read or write operation failed");
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

		/// <summary>Copies characters from the passed-in buffer to this <see cref="T:System.Data.SqlTypes.SqlChars" /> instance.</summary>
		/// <param name="offset">A long value offset into the value that is contained in the <see cref="T:System.Data.SqlTypes.SqlChars" /> instance.</param>
		/// <param name="buffer">The character array buffer to copy into.</param>
		/// <param name="offsetInBuffer">An <see cref="T:System.Int32" /> integer offset into the buffer to start copying into.</param>
		/// <param name="count">An <see cref="T:System.Int32" /> integer representing the number of characters to copy.</param>
		// Token: 0x06000D15 RID: 3349 RVA: 0x0003667C File Offset: 0x0003487C
		public void Write(long offset, char[] buffer, int offsetInBuffer, int count)
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

		/// <summary>Returns the XML Schema definition language (XSD) of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>A string value that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		/// <param name="schemaSet">A <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x06000D16 RID: 3350 RVA: 0x0003678C File Offset: 0x0003498C
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			if (schemaSet != null && schemaSet.Count == 0)
			{
				XmlSchema xmlSchema = new XmlSchema();
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				xmlSchemaComplexType.Name = "string";
				xmlSchema.Items.Add(xmlSchemaComplexType);
				schemaSet.Add(xmlSchema);
			}
			return new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
		}

		/// <summary>Converts a <see cref="T:System.Data.SqlTypes.SqlChars" /> structure to a <see cref="T:System.Data.SqlTypes.SqlString" /> structure.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlString" /> structure.</returns>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlChars" /> structure to be converted.</param>
		// Token: 0x06000D17 RID: 3351 RVA: 0x000367E8 File Offset: 0x000349E8
		public static explicit operator SqlString(SqlChars value)
		{
			if (value.IsNull)
			{
				return SqlString.Null;
			}
			return new SqlString(new string(value.Value));
		}

		/// <summary>Converts a <see cref="T:System.Data.SqlTypes.SqlString" /> structure to a <see cref="T:System.Data.SqlTypes.SqlChars" /> structure.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlChars" /> structure.</returns>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlString" /> structure to be converted.</param>
		// Token: 0x06000D18 RID: 3352 RVA: 0x0003680C File Offset: 0x00034A0C
		public static explicit operator SqlChars(SqlString value)
		{
			if (value.IsNull)
			{
				return SqlChars.Null;
			}
			return new SqlChars(value.Value);
		}

		// Token: 0x040004DB RID: 1243
		private bool notNull;

		// Token: 0x040004DC RID: 1244
		private char[] buffer;

		// Token: 0x040004DD RID: 1245
		private StorageState storage = StorageState.UnmanagedBuffer;
	}
}
