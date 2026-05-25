using System;
using System.Collections.Generic;

namespace System.Xml.Linq
{
	/// <summary>Contains the LINQ to XML extension methods.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200000A RID: 10
	public static class Extensions
	{
		/// <summary>Returns a collection of elements that contains the ancestors of every node in the source collection.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the ancestors of every node in the source collection.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> that contains the source collection.</param>
		/// <typeparam name="T">The type of the objects in <paramref name="source" />, constrained to <see cref="T:System.Xml.Linq.XNode" />.</typeparam>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600000C RID: 12 RVA: 0x00002160 File Offset: 0x00000360
		public static IEnumerable<XElement> Ancestors<T>(this IEnumerable<T> source) where T : XNode
		{
			foreach (T item in source)
			{
				for (XElement i = item.Parent; i != null; i = i.Parent)
				{
					yield return i;
				}
			}
			yield break;
		}

		/// <summary>Returns a filtered collection of elements that contains the ancestors of every node in the source collection. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the ancestors of every node in the source collection. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> that contains the source collection.</param>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		/// <typeparam name="T">The type of the objects in <paramref name="source" />, constrained to <see cref="T:System.Xml.Linq.XNode" />.</typeparam>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600000D RID: 13 RVA: 0x0000218C File Offset: 0x0000038C
		public static IEnumerable<XElement> Ancestors<T>(this IEnumerable<T> source, XName name) where T : XNode
		{
			foreach (T item in source)
			{
				for (XElement i = item.Parent; i != null; i = i.Parent)
				{
					if (i.Name == name)
					{
						yield return i;
					}
				}
			}
			yield break;
		}

		/// <summary>Returns a collection of elements that contains every element in the source collection, and the ancestors of every element in the source collection.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains every element in the source collection, and the ancestors of every element in the source collection.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the source collection.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600000E RID: 14 RVA: 0x000021C4 File Offset: 0x000003C4
		public static IEnumerable<XElement> AncestorsAndSelf(this IEnumerable<XElement> source)
		{
			foreach (XElement item in source)
			{
				for (XElement i = item; i != null; i = i.Parent)
				{
					yield return i;
				}
			}
			yield break;
		}

		/// <summary>Returns a filtered collection of elements that contains every element in the source collection, and the ancestors of every element in the source collection. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains every element in the source collection, and the ancestors of every element in the source collection. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the source collection.</param>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600000F RID: 15 RVA: 0x000021F0 File Offset: 0x000003F0
		public static IEnumerable<XElement> AncestorsAndSelf(this IEnumerable<XElement> source, XName name)
		{
			foreach (XElement item in source)
			{
				for (XElement i = item; i != null; i = i.Parent)
				{
					if (i.Name == name)
					{
						yield return i;
					}
				}
			}
			yield break;
		}

		/// <summary>Returns a collection of the attributes of every element in the source collection.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XAttribute" /> that contains the attributes of every element in the source collection.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the source collection.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000010 RID: 16 RVA: 0x00002228 File Offset: 0x00000428
		public static IEnumerable<XAttribute> Attributes(this IEnumerable<XElement> source)
		{
			foreach (XElement item in source)
			{
				foreach (XAttribute attr in item.Attributes())
				{
					yield return attr;
				}
			}
			yield break;
		}

		/// <summary>Returns a filtered collection of the attributes of every element in the source collection. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XAttribute" /> that contains a filtered collection of the attributes of every element in the source collection. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the source collection.</param>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000011 RID: 17 RVA: 0x00002254 File Offset: 0x00000454
		public static IEnumerable<XAttribute> Attributes(this IEnumerable<XElement> source, XName name)
		{
			foreach (XElement item in source)
			{
				foreach (XAttribute attr in item.Attributes(name))
				{
					yield return attr;
				}
			}
			yield break;
		}

		/// <summary>Returns a collection of the descendant nodes of every document and element in the source collection.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> of the descendant nodes of every document and element in the source collection.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XContainer" /> that contains the source collection.</param>
		/// <typeparam name="T">The type of the objects in <paramref name="source" />, constrained to <see cref="T:System.Xml.Linq.XContainer" />.</typeparam>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000012 RID: 18 RVA: 0x0000228C File Offset: 0x0000048C
		public static IEnumerable<XNode> DescendantNodes<T>(this IEnumerable<T> source) where T : XContainer
		{
			foreach (T t in source)
			{
				XContainer item = t;
				foreach (XNode i in item.DescendantNodes())
				{
					yield return i;
				}
			}
			yield break;
		}

		/// <summary>Returns a collection of nodes that contains every element in the source collection, and the descendant nodes of every element in the source collection.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> that contains every element in the source collection, and the descendant nodes of every element in the source collection.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the source collection.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000013 RID: 19 RVA: 0x000022B8 File Offset: 0x000004B8
		public static IEnumerable<XNode> DescendantNodesAndSelf(this IEnumerable<XElement> source)
		{
			foreach (XElement item in source)
			{
				foreach (XNode i in item.DescendantNodesAndSelf())
				{
					yield return i;
				}
			}
			yield break;
		}

		/// <summary>Returns a collection of elements that contains the descendant elements of every element and document in the source collection.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the descendant elements of every element and document in the source collection.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XContainer" /> that contains the source collection.</param>
		/// <typeparam name="T">The type of the objects in <paramref name="source" />, constrained to <see cref="T:System.Xml.Linq.XContainer" />.</typeparam>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000014 RID: 20 RVA: 0x000022E4 File Offset: 0x000004E4
		public static IEnumerable<XElement> Descendants<T>(this IEnumerable<T> source) where T : XContainer
		{
			foreach (T t in source)
			{
				XContainer item = t;
				foreach (XElement i in item.Descendants())
				{
					yield return i;
				}
			}
			yield break;
		}

		/// <summary>Returns a filtered collection of elements that contains the descendant elements of every element and document in the source collection. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the descendant elements of every element and document in the source collection. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XContainer" /> that contains the source collection.</param>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		/// <typeparam name="T">The type of the objects in <paramref name="source" />, constrained to <see cref="T:System.Xml.Linq.XContainer" />.</typeparam>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000015 RID: 21 RVA: 0x00002310 File Offset: 0x00000510
		public static IEnumerable<XElement> Descendants<T>(this IEnumerable<T> source, XName name) where T : XContainer
		{
			foreach (T t in source)
			{
				XContainer item = t;
				foreach (XElement i in item.Descendants(name))
				{
					yield return i;
				}
			}
			yield break;
		}

		/// <summary>Returns a collection of elements that contains every element in the source collection, and the descendent elements of every element in the source collection.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains every element in the source collection, and the descendent elements of every element in the source collection.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the source collection.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000016 RID: 22 RVA: 0x00002348 File Offset: 0x00000548
		public static IEnumerable<XElement> DescendantsAndSelf(this IEnumerable<XElement> source)
		{
			foreach (XElement item in source)
			{
				foreach (XElement i in item.DescendantsAndSelf())
				{
					yield return i;
				}
			}
			yield break;
		}

		/// <summary>Returns a filtered collection of elements that contains every element in the source collection, and the descendents of every element in the source collection. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains every element in the source collection, and the descendents of every element in the source collection. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the source collection.</param>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000017 RID: 23 RVA: 0x00002374 File Offset: 0x00000574
		public static IEnumerable<XElement> DescendantsAndSelf(this IEnumerable<XElement> source, XName name)
		{
			foreach (XElement item in source)
			{
				foreach (XElement i in item.DescendantsAndSelf(name))
				{
					yield return i;
				}
			}
			yield break;
		}

		/// <summary>Returns a collection of the child elements of every element and document in the source collection.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> of the child elements of every element or document in the source collection.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the source collection.</param>
		/// <typeparam name="T">The type of the objects in <paramref name="source" />, constrained to <see cref="T:System.Xml.Linq.XContainer" />.</typeparam>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000018 RID: 24 RVA: 0x000023AC File Offset: 0x000005AC
		public static IEnumerable<XElement> Elements<T>(this IEnumerable<T> source) where T : XContainer
		{
			foreach (T t in source)
			{
				XContainer item = t;
				foreach (XElement i in item.Elements())
				{
					yield return i;
				}
			}
			yield break;
		}

		/// <summary>Returns a filtered collection of the child elements of every element and document in the source collection. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> of the child elements of every element and document in the source collection. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the source collection.</param>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		/// <typeparam name="T">The type of the objects in <paramref name="source" />, constrained to <see cref="T:System.Xml.Linq.XContainer" />.</typeparam>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000019 RID: 25 RVA: 0x000023D8 File Offset: 0x000005D8
		public static IEnumerable<XElement> Elements<T>(this IEnumerable<T> source, XName name) where T : XContainer
		{
			foreach (T t in source)
			{
				XContainer item = t;
				foreach (XElement i in item.Elements(name))
				{
					yield return i;
				}
			}
			yield break;
		}

		/// <summary>Returns a collection of nodes that contains all nodes in the source collection, sorted in document order.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> that contains all nodes in the source collection, sorted in document order.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> that contains the source collection.</param>
		/// <typeparam name="T">The type of the objects in <paramref name="source" />, constrained to <see cref="T:System.Xml.Linq.XNode" />.</typeparam>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600001A RID: 26 RVA: 0x00002410 File Offset: 0x00000610
		public static IEnumerable<T> InDocumentOrder<T>(this IEnumerable<T> source) where T : XNode
		{
			List<XNode> list = new List<XNode>();
			foreach (T t in source)
			{
				XNode i = t;
				list.Add(i);
			}
			list.Sort(XNode.DocumentOrderComparer);
			foreach (XNode xnode in list)
			{
				T j = (T)((object)xnode);
				yield return j;
			}
			yield break;
		}

		/// <summary>Returns a collection of the child nodes of every document and element in the source collection.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> of the child nodes of every document and element in the source collection.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> that contains the source collection.</param>
		/// <typeparam name="T">The type of the objects in <paramref name="source" />, constrained to <see cref="T:System.Xml.Linq.XContainer" />.</typeparam>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600001B RID: 27 RVA: 0x0000243C File Offset: 0x0000063C
		public static IEnumerable<XNode> Nodes<T>(this IEnumerable<T> source) where T : XContainer
		{
			foreach (T t in source)
			{
				XContainer item = t;
				foreach (XNode i in item.Nodes())
				{
					yield return i;
				}
			}
			yield break;
		}

		/// <summary>Removes every attribute in the source collection from its parent element.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XAttribute" /> that contains the source collection.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600001C RID: 28 RVA: 0x00002468 File Offset: 0x00000668
		public static void Remove(this IEnumerable<XAttribute> source)
		{
			foreach (XAttribute xattribute in source)
			{
				xattribute.Remove();
			}
		}

		/// <summary>Removes every node in the source collection from its parent node.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> that contains the source collection.</param>
		/// <typeparam name="T">The type of the objects in <paramref name="source" />, constrained to <see cref="T:System.Xml.Linq.XNode" />.</typeparam>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600001D RID: 29 RVA: 0x000024C8 File Offset: 0x000006C8
		public static void Remove<T>(this IEnumerable<T> source) where T : XNode
		{
			foreach (T t in source)
			{
				t.Remove();
			}
		}
	}
}
