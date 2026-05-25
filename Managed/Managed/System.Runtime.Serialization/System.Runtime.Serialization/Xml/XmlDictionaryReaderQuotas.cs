using System;

namespace System.Xml
{
	/// <summary>Contains configurable quota values for XmlDictionaryReaders.</summary>
	// Token: 0x0200004E RID: 78
	public sealed class XmlDictionaryReaderQuotas
	{
		/// <summary>Creates a new instance of this class.</summary>
		// Token: 0x060002D6 RID: 726 RVA: 0x0000E958 File Offset: 0x0000CB58
		public XmlDictionaryReaderQuotas()
			: this(false)
		{
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000E964 File Offset: 0x0000CB64
		private XmlDictionaryReaderQuotas(bool max)
		{
			this.is_readonly = max;
			this.array_len = ((!max) ? 16384 : int.MaxValue);
			this.bytes = ((!max) ? 4096 : int.MaxValue);
			this.depth = ((!max) ? 32 : int.MaxValue);
			this.nt_chars = ((!max) ? 16384 : int.MaxValue);
			this.text_len = ((!max) ? 8192 : int.MaxValue);
		}

		/// <summary>Gets an instance of this class with all properties set to maximum values.</summary>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReaderQuotas" /> with properties set to <see cref="F:System.Int32.MaxValue" />.</returns>
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000EA14 File Offset: 0x0000CC14
		public static XmlDictionaryReaderQuotas Max
		{
			get
			{
				return XmlDictionaryReaderQuotas.max;
			}
		}

		/// <summary>Gets and sets the maximum allowed array length.</summary>
		/// <returns>The maximum allowed array length. The default is 16384.</returns>
		/// <exception cref="T:System.InvalidOperationException">Trying to set the value, but quota values are read-only for this instance.</exception>
		/// <exception cref="T:System.ArgumentException">Trying to set the value to less than zero.</exception>
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000EA1C File Offset: 0x0000CC1C
		// (set) Token: 0x060002DB RID: 731 RVA: 0x0000EA24 File Offset: 0x0000CC24
		public int MaxArrayLength
		{
			get
			{
				return this.array_len;
			}
			set
			{
				this.array_len = this.Check(value);
			}
		}

		/// <summary>Gets and sets the maximum allowed bytes returned for each read.</summary>
		/// <returns>The maximum allowed bytes returned for each read. The default is 4096.</returns>
		/// <exception cref="T:System.InvalidOperationException">Trying to set the value, but quota values are read-only for this instance.</exception>
		/// <exception cref="T:System.ArgumentException">Trying to set the value to less than zero.</exception>
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000EA34 File Offset: 0x0000CC34
		// (set) Token: 0x060002DD RID: 733 RVA: 0x0000EA3C File Offset: 0x0000CC3C
		public int MaxBytesPerRead
		{
			get
			{
				return this.bytes;
			}
			set
			{
				this.bytes = this.Check(value);
			}
		}

		/// <summary>Gets and sets the maximum nested node depth.</summary>
		/// <returns>The maximum nested node depth. The default is 32;</returns>
		/// <exception cref="T:System.InvalidOperationException">Trying to set the value and quota values are read-only for this instance.</exception>
		/// <exception cref="T:System.ArgumentException">Trying to set the value is less than zero.</exception>
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0000EA4C File Offset: 0x0000CC4C
		// (set) Token: 0x060002DF RID: 735 RVA: 0x0000EA54 File Offset: 0x0000CC54
		public int MaxDepth
		{
			get
			{
				return this.depth;
			}
			set
			{
				this.depth = this.Check(value);
			}
		}

		/// <summary>Gets and sets the maximum characters allowed in a table name.</summary>
		/// <returns>The maximum characters allowed in a table name. The default is 16384.</returns>
		/// <exception cref="T:System.InvalidOperationException">Trying to set the value, but quota values are read-only for this instance.</exception>
		/// <exception cref="T:System.ArgumentException">Trying to set the value to less than zero.</exception>
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000EA64 File Offset: 0x0000CC64
		// (set) Token: 0x060002E1 RID: 737 RVA: 0x0000EA6C File Offset: 0x0000CC6C
		public int MaxNameTableCharCount
		{
			get
			{
				return this.nt_chars;
			}
			set
			{
				this.nt_chars = this.Check(value);
			}
		}

		/// <summary>Gets and sets the maximum string length returned by the reader.</summary>
		/// <returns>The maximum string length returned by the reader. The default is 8192.</returns>
		/// <exception cref="T:System.InvalidOperationException">Trying to set the value, but quota values are read-only for this instance.</exception>
		/// <exception cref="T:System.ArgumentException">Trying to set the value to less than zero.</exception>
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000EA7C File Offset: 0x0000CC7C
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x0000EA84 File Offset: 0x0000CC84
		public int MaxStringContentLength
		{
			get
			{
				return this.text_len;
			}
			set
			{
				this.text_len = this.Check(value);
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000EA94 File Offset: 0x0000CC94
		private int Check(int value)
		{
			if (this.is_readonly)
			{
				throw new InvalidOperationException("This quota is read-only.");
			}
			if (value <= 0)
			{
				throw new ArgumentException("Value must be positive integer.");
			}
			return value;
		}

		/// <summary>Sets the properties on a passed-in quotas instance, based on the values in this instance.</summary>
		/// <param name="quotas">The <see cref="T:System.Xml.XmlDictionaryReaderQuotas" /> instance to which to copy values.</param>
		/// <exception cref="T:System.InvalidOperationException">Trying to set the value, but quota values are read-only for the passed in instance.</exception>
		/// <exception cref="T:System.ArgumentNullException">Passed in target<paramref name=" quotas" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060002E5 RID: 741 RVA: 0x0000EAC0 File Offset: 0x0000CCC0
		public void CopyTo(XmlDictionaryReaderQuotas quota)
		{
			quota.array_len = this.array_len;
			quota.bytes = this.bytes;
			quota.depth = this.depth;
			quota.nt_chars = this.nt_chars;
			quota.text_len = this.text_len;
		}

		// Token: 0x04000144 RID: 324
		private static XmlDictionaryReaderQuotas max = new XmlDictionaryReaderQuotas(true);

		// Token: 0x04000145 RID: 325
		private readonly bool is_readonly;

		// Token: 0x04000146 RID: 326
		private int array_len;

		// Token: 0x04000147 RID: 327
		private int bytes;

		// Token: 0x04000148 RID: 328
		private int depth;

		// Token: 0x04000149 RID: 329
		private int nt_chars;

		// Token: 0x0400014A RID: 330
		private int text_len;
	}
}
