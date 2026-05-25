using System;

namespace System.Xml.Linq
{
	/// <summary>Specifies the event type when an event is raised for an <see cref="T:System.Xml.Linq.XObject" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000020 RID: 32
	public enum XObjectChange
	{
		/// <summary>An <see cref="T:System.Xml.Linq.XObject" /> has been or will be added to an <see cref="T:System.Xml.Linq.XContainer" />.</summary>
		// Token: 0x0400006D RID: 109
		Add,
		/// <summary>An <see cref="T:System.Xml.Linq.XObject" /> has been or will be removed from an <see cref="T:System.Xml.Linq.XContainer" />.</summary>
		// Token: 0x0400006E RID: 110
		Remove,
		/// <summary>An <see cref="T:System.Xml.Linq.XObject" /> has been or will be renamed.</summary>
		// Token: 0x0400006F RID: 111
		Name,
		/// <summary>The value of an <see cref="T:System.Xml.Linq.XObject" /> has been or will be changed. In addition, a change in the serialization of an empty element (either from an empty tag to start/end tag pair or vice versa) raises this event.</summary>
		// Token: 0x04000070 RID: 112
		Value
	}
}
