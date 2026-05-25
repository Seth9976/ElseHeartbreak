using System;
using System.Runtime.Serialization;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Thrown when SQL Server or the ADO.NET <see cref="N:System.Data.SqlClient" /> provider detects an invalid user-defined type (UDT). </summary>
	// Token: 0x02000147 RID: 327
	[Serializable]
	public sealed class InvalidUdtException : SystemException
	{
		// Token: 0x0600116D RID: 4461 RVA: 0x00044514 File Offset: 0x00042714
		[MonoTODO]
		internal InvalidUdtException()
		{
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x0004451C File Offset: 0x0004271C
		[MonoTODO]
		internal InvalidUdtException(string message)
		{
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x00044524 File Offset: 0x00042724
		[MonoTODO]
		internal InvalidUdtException(string message, Exception innerException)
		{
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x0004452C File Offset: 0x0004272C
		[MonoTODO]
		internal InvalidUdtException(Type t, string reason)
		{
		}

		/// <summary>Streams all the <see cref="T:Microsoft.SqlServer.Server.InvalidUdtException" /> properties into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> class for the given <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="si">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object.</param>
		// Token: 0x06001171 RID: 4465 RVA: 0x00044534 File Offset: 0x00042734
		[MonoTODO]
		public override void GetObjectData(SerializationInfo si, StreamingContext context)
		{
		}
	}
}
