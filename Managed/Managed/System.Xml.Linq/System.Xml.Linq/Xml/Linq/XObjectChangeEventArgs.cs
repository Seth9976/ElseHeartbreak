using System;

namespace System.Xml.Linq
{
	/// <summary>Provides data for the <see cref="E:System.Xml.Linq.XObject.Changing" /> and <see cref="E:System.Xml.Linq.XObject.Changed" /> events.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000021 RID: 33
	public class XObjectChangeEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XObjectChangeEventArgs" /> class. </summary>
		/// <param name="objectChange">An <see cref="T:System.Xml.Linq.XObjectChange" /> that contains the event arguments for LINQ to XML events.</param>
		// Token: 0x060001C5 RID: 453 RVA: 0x00008B10 File Offset: 0x00006D10
		public XObjectChangeEventArgs(XObjectChange change)
		{
			this.type = change;
		}

		/// <summary>Gets the type of change.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XObjectChange" /> that contains the type of change.</returns>
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00008B5C File Offset: 0x00006D5C
		public XObjectChange ObjectChange
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Event argument for an <see cref="F:System.Xml.Linq.XObjectChange.Add" /> change event.</summary>
		// Token: 0x04000071 RID: 113
		public static readonly XObjectChangeEventArgs Add = new XObjectChangeEventArgs(XObjectChange.Add);

		/// <summary>Event argument for a <see cref="F:System.Xml.Linq.XObjectChange.Name" /> change event.</summary>
		// Token: 0x04000072 RID: 114
		public static readonly XObjectChangeEventArgs Name = new XObjectChangeEventArgs(XObjectChange.Name);

		/// <summary>Event argument for a <see cref="F:System.Xml.Linq.XObjectChange.Remove" /> change event.</summary>
		// Token: 0x04000073 RID: 115
		public static readonly XObjectChangeEventArgs Remove = new XObjectChangeEventArgs(XObjectChange.Remove);

		/// <summary>Event argument for a <see cref="F:System.Xml.Linq.XObjectChange.Value" /> change event.</summary>
		// Token: 0x04000074 RID: 116
		public static readonly XObjectChangeEventArgs Value = new XObjectChangeEventArgs(XObjectChange.Value);

		// Token: 0x04000075 RID: 117
		private XObjectChange type;
	}
}
