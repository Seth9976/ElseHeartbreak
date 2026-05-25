using System;
using System.Collections;

namespace System.Data
{
	/// <summary>Collects all parameters relevant to a Command object and their mappings to <see cref="T:System.Data.DataSet" /> columns, and is implemented by .NET Framework data providers that access data sources.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200004D RID: 77
	public interface IDataParameterCollection : IList, IEnumerable, ICollection
	{
		/// <summary>Removes the <see cref="T:System.Data.IDataParameter" /> from the collection.</summary>
		/// <param name="parameterName">The name of the parameter. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600058E RID: 1422
		void RemoveAt(string parameterName);

		/// <summary>Gets the location of the <see cref="T:System.Data.IDataParameter" /> within the collection.</summary>
		/// <returns>The zero-based location of the <see cref="T:System.Data.IDataParameter" /> within the collection.</returns>
		/// <param name="parameterName">The name of the parameter. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600058F RID: 1423
		int IndexOf(string parameterName);

		/// <summary>Gets a value indicating whether a parameter in the collection has the specified name.</summary>
		/// <returns>true if the collection contains the parameter; otherwise, false.</returns>
		/// <param name="parameterName">The name of the parameter. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000590 RID: 1424
		bool Contains(string parameterName);

		/// <summary>Gets or sets the parameter at the specified index.</summary>
		/// <returns>An <see cref="T:System.Object" /> at the specified index.</returns>
		/// <param name="parameterName">The name of the parameter to retrieve. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000108 RID: 264
		object this[string parameterName] { get; set; }
	}
}
