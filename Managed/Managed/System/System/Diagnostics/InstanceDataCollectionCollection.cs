using System;
using System.Collections;

namespace System.Diagnostics
{
	/// <summary>Provides a strongly typed collection of <see cref="T:System.Diagnostics.InstanceDataCollection" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200022E RID: 558
	public class InstanceDataCollectionCollection : DictionaryBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.InstanceDataCollectionCollection" /> class.</summary>
		// Token: 0x0600131E RID: 4894 RVA: 0x00033104 File Offset: 0x00031304
		[Obsolete("Use PerformanceCounterCategory.ReadCategory()")]
		public InstanceDataCollectionCollection()
		{
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x0003310C File Offset: 0x0003130C
		private static void CheckNull(object value, string name)
		{
			if (value == null)
			{
				throw new ArgumentNullException(name);
			}
		}

		/// <summary>Gets the instance data for the specified counter.</summary>
		/// <returns>An <see cref="T:System.Diagnostics.InstanceDataCollection" /> item, by which the <see cref="T:System.Diagnostics.InstanceDataCollectionCollection" /> object is indexed.</returns>
		/// <param name="counterName">The name of the performance counter. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="counterName" /> parameter is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700046F RID: 1135
		public InstanceDataCollection this[string counterName]
		{
			get
			{
				InstanceDataCollectionCollection.CheckNull(counterName, "counterName");
				return (InstanceDataCollection)base.Dictionary[counterName];
			}
		}

		/// <summary>Gets the object and counter registry keys for the objects associated with this instance data collection.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that represents a set of object-specific registry keys.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001321 RID: 4897 RVA: 0x0003313C File Offset: 0x0003133C
		public ICollection Keys
		{
			get
			{
				return base.Dictionary.Keys;
			}
		}

		/// <summary>Gets the instance data values that comprise the collection of instances for the counter.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that represents the counter's instances and their associated data values.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06001322 RID: 4898 RVA: 0x0003314C File Offset: 0x0003134C
		public ICollection Values
		{
			get
			{
				return base.Dictionary.Values;
			}
		}

		/// <summary>Determines whether an instance data collection for the specified counter (identified by one of the indexed <see cref="T:System.Diagnostics.InstanceDataCollection" /> objects) exists in the collection.</summary>
		/// <returns>true if an instance data collection containing the specified counter exists in the collection; otherwise, false.</returns>
		/// <param name="counterName">The name of the performance counter. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="counterName" /> parameter is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001323 RID: 4899 RVA: 0x0003315C File Offset: 0x0003135C
		public bool Contains(string counterName)
		{
			InstanceDataCollectionCollection.CheckNull(counterName, "counterName");
			return base.Dictionary.Contains(counterName);
		}

		/// <summary>Copies an array of <see cref="T:System.Diagnostics.InstanceDataCollection" /> instances to the collection, at the specified index.</summary>
		/// <param name="counters">An array of <see cref="T:System.Diagnostics.InstanceDataCollection" /> instances (identified by the counters they contain) to add to the collection. </param>
		/// <param name="index">The location at which to add the new instances. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001324 RID: 4900 RVA: 0x00033178 File Offset: 0x00031378
		public void CopyTo(InstanceDataCollection[] counters, int index)
		{
			base.Dictionary.CopyTo(counters, index);
		}
	}
}
