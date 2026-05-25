using System;
using System.Collections.Generic;

namespace System.Xml.Linq
{
	/// <summary>Represents a node or an attribute in an XML tree. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200001F RID: 31
	public abstract class XObject : IXmlLineInfo
	{
		// Token: 0x060001AA RID: 426 RVA: 0x000087E0 File Offset: 0x000069E0
		internal XObject()
		{
		}

		/// <summary>Raised when this <see cref="T:System.Xml.Linq.XObject" /> or any of its descendants are about to change.</summary>
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060001AB RID: 427 RVA: 0x000087E8 File Offset: 0x000069E8
		// (remove) Token: 0x060001AC RID: 428 RVA: 0x00008804 File Offset: 0x00006A04
		public event EventHandler<XObjectChangeEventArgs> Changing;

		/// <summary>Raised when this <see cref="T:System.Xml.Linq.XObject" /> or any of its descendants have changed.</summary>
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060001AD RID: 429 RVA: 0x00008820 File Offset: 0x00006A20
		// (remove) Token: 0x060001AE RID: 430 RVA: 0x0000883C File Offset: 0x00006A3C
		public event EventHandler<XObjectChangeEventArgs> Changed;

		/// <summary>Gets the line number that the underlying <see cref="T:System.Xml.XmlReader" /> reported for this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the line number reported by the <see cref="T:System.Xml.XmlReader" /> for this <see cref="T:System.Xml.Linq.XObject" />.</returns>
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00008858 File Offset: 0x00006A58
		int IXmlLineInfo.LineNumber
		{
			get
			{
				return this.LineNumber;
			}
		}

		/// <summary>Gets the line position that the underlying <see cref="T:System.Xml.XmlReader" /> reported for this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the line position reported by the <see cref="T:System.Xml.XmlReader" /> for this <see cref="T:System.Xml.Linq.XObject" />.</returns>
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00008860 File Offset: 0x00006A60
		int IXmlLineInfo.LinePosition
		{
			get
			{
				return this.LinePosition;
			}
		}

		/// <summary>Gets a value indicating whether or not this <see cref="T:System.Xml.Linq.XObject" /> has line information.</summary>
		/// <returns>true if the <see cref="T:System.Xml.Linq.XObject" /> has line information, otherwise false.</returns>
		// Token: 0x060001B1 RID: 433 RVA: 0x00008868 File Offset: 0x00006A68
		bool IXmlLineInfo.HasLineInfo()
		{
			return this.line > 0;
		}

		/// <summary>Gets the base URI for this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the base URI for this <see cref="T:System.Xml.Linq.XObject" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00008874 File Offset: 0x00006A74
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x0000887C File Offset: 0x00006A7C
		public string BaseUri
		{
			get
			{
				return this.baseuri;
			}
			internal set
			{
				this.baseuri = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Linq.XDocument" /> for this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <returns>The <see cref="T:System.Xml.Linq.XDocument" /> for this <see cref="T:System.Xml.Linq.XObject" />. </returns>
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00008888 File Offset: 0x00006A88
		public XDocument Document
		{
			get
			{
				if (this is XDocument)
				{
					return (XDocument)this;
				}
				for (XContainer xcontainer = this.owner; xcontainer != null; xcontainer = xcontainer.owner)
				{
					if (xcontainer is XDocument)
					{
						return (XDocument)xcontainer;
					}
				}
				return null;
			}
		}

		/// <summary>Gets the node type for this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <returns>The node type for this <see cref="T:System.Xml.Linq.XObject" />. </returns>
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001B5 RID: 437
		public abstract XmlNodeType NodeType { get; }

		/// <summary>Gets the parent <see cref="T:System.Xml.Linq.XElement" /> of this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <returns>The parent <see cref="T:System.Xml.Linq.XElement" /> of this <see cref="T:System.Xml.Linq.XObject" />.</returns>
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x000088D4 File Offset: 0x00006AD4
		public XElement Parent
		{
			get
			{
				return this.owner as XElement;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x000088E4 File Offset: 0x00006AE4
		internal XContainer Owner
		{
			get
			{
				return this.owner;
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000088EC File Offset: 0x00006AEC
		internal void SetOwner(XContainer node)
		{
			this.owner = node;
		}

		/// <summary>Adds an object to the annotation list of this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <param name="annotation">An <see cref="T:System.Object" /> that contains the annotation to add.</param>
		// Token: 0x060001B9 RID: 441 RVA: 0x000088F8 File Offset: 0x00006AF8
		public void AddAnnotation(object annotation)
		{
			if (annotation == null)
			{
				throw new ArgumentNullException("annotation");
			}
			if (this.annotations == null)
			{
				this.annotations = new List<object>();
			}
			this.annotations.Add(annotation);
		}

		/// <summary>Get the first annotation object of the specified type from this <see cref="T:System.Xml.Linq.XObject" />. </summary>
		/// <returns>The first annotation object that matches the specified type, or null if no annotation is of the specified type.</returns>
		/// <typeparam name="T">The type of the annotation to retrieve.</typeparam>
		// Token: 0x060001BA RID: 442 RVA: 0x00008930 File Offset: 0x00006B30
		public T Annotation<T>() where T : class
		{
			return (T)((object)this.Annotation(typeof(T)));
		}

		/// <summary>Gets the first annotation object of the specified type from this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <returns>The <see cref="T:System.Object" /> that contains the first annotation object that matches the specified type, or null if no annotation is of the specified type.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of the annotation to retrieve.</param>
		// Token: 0x060001BB RID: 443 RVA: 0x00008948 File Offset: 0x00006B48
		public object Annotation(Type type)
		{
			if (this.annotations != null)
			{
				foreach (object obj in this.annotations)
				{
					if (obj.GetType() == type)
					{
						return obj;
					}
				}
			}
			return null;
		}

		/// <summary>Gets a collection of annotations of the specified type for this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains the annotations for this <see cref="T:System.Xml.Linq.XObject" />.</returns>
		/// <typeparam name="T">The type of the annotations to retrieve.</typeparam>
		// Token: 0x060001BC RID: 444 RVA: 0x000089C8 File Offset: 0x00006BC8
		public IEnumerable<T> Annotations<T>() where T : class
		{
			foreach (object obj in this.Annotations(typeof(T)))
			{
				T o = (T)((object)obj);
				yield return o;
			}
			yield break;
		}

		/// <summary>Gets a collection of annotations of the specified type for this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Object" /> that contains the annotations that match the specified type for this <see cref="T:System.Xml.Linq.XObject" />.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of the annotations to retrieve.</param>
		// Token: 0x060001BD RID: 445 RVA: 0x000089EC File Offset: 0x00006BEC
		public IEnumerable<object> Annotations(Type type)
		{
			if (this.annotations == null)
			{
				yield break;
			}
			foreach (object o in this.annotations)
			{
				if (o.GetType() == type)
				{
					yield return o;
				}
			}
			yield break;
		}

		/// <summary>Removes the annotations of the specified type from this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <typeparam name="T">The type of annotations to remove.</typeparam>
		// Token: 0x060001BE RID: 446 RVA: 0x00008A20 File Offset: 0x00006C20
		public void RemoveAnnotations<T>() where T : class
		{
			this.RemoveAnnotations(typeof(T));
		}

		/// <summary>Removes the annotations of the specified type from this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of annotations to remove.</param>
		// Token: 0x060001BF RID: 447 RVA: 0x00008A34 File Offset: 0x00006C34
		public void RemoveAnnotations(Type type)
		{
			if (this.annotations == null)
			{
				return;
			}
			for (int i = 0; i < this.annotations.Count; i++)
			{
				if (this.annotations[i].GetType() == type)
				{
					this.annotations.RemoveAt(i);
				}
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00008A8C File Offset: 0x00006C8C
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x00008A94 File Offset: 0x00006C94
		internal int LineNumber
		{
			get
			{
				return this.line;
			}
			set
			{
				this.line = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00008AA0 File Offset: 0x00006CA0
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x00008AA8 File Offset: 0x00006CA8
		internal int LinePosition
		{
			get
			{
				return this.column;
			}
			set
			{
				this.column = value;
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00008AB4 File Offset: 0x00006CB4
		internal void FillLineInfoAndBaseUri(XmlReader r, LoadOptions options)
		{
			if ((options & LoadOptions.SetLineInfo) != LoadOptions.None)
			{
				IXmlLineInfo xmlLineInfo = r as IXmlLineInfo;
				if (xmlLineInfo != null && xmlLineInfo.HasLineInfo())
				{
					this.LineNumber = xmlLineInfo.LineNumber;
					this.LinePosition = xmlLineInfo.LinePosition;
				}
			}
			if ((options & LoadOptions.SetBaseUri) != LoadOptions.None)
			{
				this.BaseUri = r.BaseURI;
			}
		}

		// Token: 0x04000065 RID: 101
		private XContainer owner;

		// Token: 0x04000066 RID: 102
		private List<object> annotations;

		// Token: 0x04000067 RID: 103
		private string baseuri;

		// Token: 0x04000068 RID: 104
		private int line;

		// Token: 0x04000069 RID: 105
		private int column;
	}
}
