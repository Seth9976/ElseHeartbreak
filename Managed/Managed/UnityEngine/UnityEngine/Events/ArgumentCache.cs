using System;
using UnityEngine.Serialization;

namespace UnityEngine.Events
{
	// Token: 0x02000055 RID: 85
	[Serializable]
	internal class ArgumentCache
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00008458 File Offset: 0x00006658
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x00008460 File Offset: 0x00006660
		public Object unityObjectArgument
		{
			get
			{
				return this.m_ObjectArgument;
			}
			set
			{
				this.m_ObjectArgument = value;
				this.m_ObjectArgumentAssemblyTypeName = ((!(value != null)) ? string.Empty : value.GetType().AssemblyQualifiedName);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x0000849C File Offset: 0x0000669C
		public string unityObjectArgumentAssemblyTypeName
		{
			get
			{
				return this.m_ObjectArgumentAssemblyTypeName;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x000084A4 File Offset: 0x000066A4
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x000084AC File Offset: 0x000066AC
		public int intArgument
		{
			get
			{
				return this.m_IntArgument;
			}
			set
			{
				this.m_IntArgument = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001BA RID: 442 RVA: 0x000084B8 File Offset: 0x000066B8
		// (set) Token: 0x060001BB RID: 443 RVA: 0x000084C0 File Offset: 0x000066C0
		public float floatArgument
		{
			get
			{
				return this.m_FloatArgument;
			}
			set
			{
				this.m_FloatArgument = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001BC RID: 444 RVA: 0x000084CC File Offset: 0x000066CC
		// (set) Token: 0x060001BD RID: 445 RVA: 0x000084D4 File Offset: 0x000066D4
		public string stringArgument
		{
			get
			{
				return this.m_StringArgument;
			}
			set
			{
				this.m_StringArgument = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001BE RID: 446 RVA: 0x000084E0 File Offset: 0x000066E0
		// (set) Token: 0x060001BF RID: 447 RVA: 0x000084E8 File Offset: 0x000066E8
		public bool boolArgument
		{
			get
			{
				return this.m_BoolArgument;
			}
			set
			{
				this.m_BoolArgument = value;
			}
		}

		// Token: 0x04000174 RID: 372
		[SerializeField]
		[FormerlySerializedAs("objectArgument")]
		private Object m_ObjectArgument;

		// Token: 0x04000175 RID: 373
		[FormerlySerializedAs("objectArgumentAssemblyTypeName")]
		[SerializeField]
		private string m_ObjectArgumentAssemblyTypeName;

		// Token: 0x04000176 RID: 374
		[FormerlySerializedAs("intArgument")]
		[SerializeField]
		private int m_IntArgument;

		// Token: 0x04000177 RID: 375
		[FormerlySerializedAs("floatArgument")]
		[SerializeField]
		private float m_FloatArgument;

		// Token: 0x04000178 RID: 376
		[SerializeField]
		[FormerlySerializedAs("stringArgument")]
		private string m_StringArgument;

		// Token: 0x04000179 RID: 377
		[SerializeField]
		private bool m_BoolArgument;
	}
}
