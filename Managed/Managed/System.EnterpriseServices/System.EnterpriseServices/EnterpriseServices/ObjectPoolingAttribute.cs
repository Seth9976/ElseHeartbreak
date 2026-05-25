using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Enables and configures object pooling for a component. This class cannot be inherited.</summary>
	// Token: 0x0200002F RID: 47
	[ComVisible(false)]
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ObjectPoolingAttribute : Attribute, IConfigurationAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ObjectPoolingAttribute" /> class and sets the <see cref="P:System.EnterpriseServices.ObjectPoolingAttribute.Enabled" />, <see cref="P:System.EnterpriseServices.ObjectPoolingAttribute.MaxPoolSize" />, <see cref="P:System.EnterpriseServices.ObjectPoolingAttribute.MinPoolSize" />, and <see cref="P:System.EnterpriseServices.ObjectPoolingAttribute.CreationTimeout" /> properties to their default values.</summary>
		// Token: 0x06000099 RID: 153 RVA: 0x000025F8 File Offset: 0x000007F8
		public ObjectPoolingAttribute()
			: this(true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ObjectPoolingAttribute" /> class and sets the <see cref="P:System.EnterpriseServices.ObjectPoolingAttribute.Enabled" /> property.</summary>
		/// <param name="enable">true to enable object pooling; otherwise, false. </param>
		// Token: 0x0600009A RID: 154 RVA: 0x00002604 File Offset: 0x00000804
		public ObjectPoolingAttribute(bool enable)
		{
			this.enabled = enable;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ObjectPoolingAttribute" /> class and sets the <see cref="P:System.EnterpriseServices.ObjectPoolingAttribute.MaxPoolSize" /> and <see cref="P:System.EnterpriseServices.ObjectPoolingAttribute.MinPoolSize" /> properties.</summary>
		/// <param name="minPoolSize">The minimum pool size. </param>
		/// <param name="maxPoolSize">The maximum pool size. </param>
		// Token: 0x0600009B RID: 155 RVA: 0x00002614 File Offset: 0x00000814
		public ObjectPoolingAttribute(int minPoolSize, int maxPoolSize)
			: this(true, minPoolSize, maxPoolSize)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ObjectPoolingAttribute" /> class and sets the <see cref="P:System.EnterpriseServices.ObjectPoolingAttribute.Enabled" />, <see cref="P:System.EnterpriseServices.ObjectPoolingAttribute.MaxPoolSize" />, and <see cref="P:System.EnterpriseServices.ObjectPoolingAttribute.MinPoolSize" /> properties.</summary>
		/// <param name="enable">true to enable object pooling; otherwise, false. </param>
		/// <param name="minPoolSize">The minimum pool size.</param>
		/// <param name="maxPoolSize">The maximum pool size.</param>
		// Token: 0x0600009C RID: 156 RVA: 0x00002620 File Offset: 0x00000820
		public ObjectPoolingAttribute(bool enable, int minPoolSize, int maxPoolSize)
		{
			this.enabled = enable;
			this.minPoolSize = minPoolSize;
			this.maxPoolSize = maxPoolSize;
		}

		/// <summary>Gets or sets the length of time to wait for an object to become available in the pool before throwing an exception. This value is in milliseconds.</summary>
		/// <returns>The time-out value in milliseconds.</returns>
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00002640 File Offset: 0x00000840
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00002648 File Offset: 0x00000848
		public int CreationTimeout
		{
			get
			{
				return this.creationTimeout;
			}
			set
			{
				this.creationTimeout = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether object pooling is enabled.</summary>
		/// <returns>true if object pooling is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00002654 File Offset: 0x00000854
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x0000265C File Offset: 0x0000085C
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
			}
		}

		/// <summary>Gets or sets the value for the maximum size of the pool.</summary>
		/// <returns>The maximum number of objects in the pool.</returns>
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00002668 File Offset: 0x00000868
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00002670 File Offset: 0x00000870
		public int MaxPoolSize
		{
			get
			{
				return this.maxPoolSize;
			}
			set
			{
				this.maxPoolSize = value;
			}
		}

		/// <summary>Gets or sets the value for the minimum size of the pool.</summary>
		/// <returns>The minimum number of objects in the pool.</returns>
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x0000267C File Offset: 0x0000087C
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00002684 File Offset: 0x00000884
		public int MinPoolSize
		{
			get
			{
				return this.minPoolSize;
			}
			set
			{
				this.minPoolSize = value;
			}
		}

		/// <summary>Called internally by the .NET Framework infrastructure while installing and configuring assemblies in the COM+ catalog.</summary>
		/// <returns>true if the method has made changes.</returns>
		/// <param name="info">A hash table that contains internal objects referenced by internal keys.</param>
		// Token: 0x060000A5 RID: 165 RVA: 0x00002690 File Offset: 0x00000890
		[MonoTODO]
		public bool AfterSaveChanges(Hashtable info)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called internally by the .NET Framework infrastructure while applying the <see cref="T:System.EnterpriseServices.ObjectPoolingAttribute" /> class attribute to a serviced component.</summary>
		/// <returns>true if the method has made changes.</returns>
		/// <param name="info">A hash table that contains an internal object to which object pooling properties are applied, referenced by an internal key.</param>
		// Token: 0x060000A6 RID: 166 RVA: 0x00002698 File Offset: 0x00000898
		[MonoTODO]
		public bool Apply(Hashtable info)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called internally by the .NET Framework infrastructure while installing and configuring assemblies in the COM+ catalog.</summary>
		/// <returns>true if the attribute is applied to a serviced component class.</returns>
		/// <param name="s">A string generated by the .NET Framework infrastructure that is checked for a special value that indicates a serviced component.</param>
		// Token: 0x060000A7 RID: 167 RVA: 0x000026A0 File Offset: 0x000008A0
		[MonoTODO]
		public bool IsValidTarget(string s)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000062 RID: 98
		private int creationTimeout;

		// Token: 0x04000063 RID: 99
		private bool enabled;

		// Token: 0x04000064 RID: 100
		private int minPoolSize;

		// Token: 0x04000065 RID: 101
		private int maxPoolSize;
	}
}
