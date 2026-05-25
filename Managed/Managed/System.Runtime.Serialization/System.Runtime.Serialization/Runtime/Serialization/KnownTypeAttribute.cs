using System;

namespace System.Runtime.Serialization
{
	/// <summary>Specifies types that should be recognized by the <see cref="T:System.Runtime.Serialization.DataContractSerializer" /> when serializing or deserializing a given type. </summary>
	// Token: 0x0200001F RID: 31
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = true, AllowMultiple = true)]
	public sealed class KnownTypeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.KnownTypeAttribute" /> class with the name of a method that returns an <see cref="T:System.Collections.IEnumerable" /> of known types.</summary>
		/// <param name="methodName">The name of the method that returns an <see cref="T:System.Collections.IEnumerable" /> of types used when serializing or deserializing data.</param>
		// Token: 0x06000080 RID: 128 RVA: 0x00002BA0 File Offset: 0x00000DA0
		public KnownTypeAttribute(string methodName)
		{
			this.method_name = methodName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.KnownTypeAttribute" /> class with the specified type. </summary>
		/// <param name="type">The <see cref="T:System.Type" /> that is included as a known type when serializing or deserializing data.</param>
		// Token: 0x06000081 RID: 129 RVA: 0x00002BB0 File Offset: 0x00000DB0
		public KnownTypeAttribute(Type type)
		{
			this.type = type;
		}

		/// <summary>Gets the name of a method that will return a list of types that should be recognized during serialization or deserialization. </summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name of the method on the type defined by the <see cref="T:System.Runtime.Serialization.KnownTypeAttribute" /> class.</returns>
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002BC0 File Offset: 0x00000DC0
		public string MethodName
		{
			get
			{
				return this.method_name;
			}
		}

		/// <summary>Gets the type that should be recognized during serialization or deserialization by the <see cref="T:System.Runtime.Serialization.DataContractSerializer" />. </summary>
		/// <returns>The <see cref="T:System.Type" /> that is used during serialization or deserialization. </returns>
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00002BC8 File Offset: 0x00000DC8
		public Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x04000048 RID: 72
		private string method_name;

		// Token: 0x04000049 RID: 73
		private Type type;
	}
}
