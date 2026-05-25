using System;
using System.Collections;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>The exception that is thrown when a name conflict occurs while generating a strongly typed <see cref="T:System.Data.DataSet" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000084 RID: 132
	[Serializable]
	public class TypedDataSetGeneratorException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.TypedDataSetGeneratorException" /> class.</summary>
		// Token: 0x06000665 RID: 1637 RVA: 0x0001F4A4 File Offset: 0x0001D6A4
		public TypedDataSetGeneratorException()
			: base(Locale.GetText("System error."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.TypedDataSetGeneratorException" /> class.</summary>
		/// <param name="list">
		///   <see cref="T:System.Collections.ArrayList" /> object containing a dynamic list of exceptions. </param>
		// Token: 0x06000666 RID: 1638 RVA: 0x0001F4B8 File Offset: 0x0001D6B8
		public TypedDataSetGeneratorException(ArrayList list)
			: base(Locale.GetText("System error."))
		{
			this.errorList = list;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.TypedDataSetGeneratorException" /> class using the specified serialization information and streaming context.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object. </param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure. </param>
		// Token: 0x06000667 RID: 1639 RVA: 0x0001F4D4 File Offset: 0x0001D6D4
		protected TypedDataSetGeneratorException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			int @int = info.GetInt32("KEY_ARRAYCOUNT");
			this.errorList = new ArrayList(@int);
			for (int i = 0; i < @int; i++)
			{
				this.errorList.Add(info.GetString("KEY_ARRAYVALUES" + i));
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.TypedDataSetGeneratorException" /> class with the specified string. </summary>
		/// <param name="message">The string to display when the exception is thrown.</param>
		// Token: 0x06000668 RID: 1640 RVA: 0x0001F538 File Offset: 0x0001D738
		public TypedDataSetGeneratorException(string error)
			: base(error)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.TypedDataSetGeneratorException" /> class with the specified string and inner exception. </summary>
		/// <param name="message">The string to display when the exception is thrown.</param>
		/// <param name="innerException">A reference to an inner exception.</param>
		// Token: 0x06000669 RID: 1641 RVA: 0x0001F544 File Offset: 0x0001D744
		public TypedDataSetGeneratorException(string error, Exception inner)
			: base(error, inner)
		{
		}

		/// <summary>Gets a dynamic list of generated errors.</summary>
		/// <returns>
		///   <see cref="T:System.Collections.ArrayList" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x0001F550 File Offset: 0x0001D750
		public ArrayList ErrorList
		{
			get
			{
				return this.errorList;
			}
		}

		/// <summary>Implements the ISerializable interface and returns the data needed to serialize the <see cref="T:System.Data.TypedDataSetGeneratorException" /> object.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object. </param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x0600066B RID: 1643 RVA: 0x0001F558 File Offset: 0x0001D758
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			int num = ((this.errorList == null) ? 0 : this.ErrorList.Count);
			info.AddValue("KEY_ARRAYCOUNT", num);
			for (int i = 0; i < num; i++)
			{
				info.AddValue("KEY_ARRAYVALUES" + i, this.ErrorList[i]);
			}
		}

		// Token: 0x0400024D RID: 589
		private readonly ArrayList errorList;
	}
}
