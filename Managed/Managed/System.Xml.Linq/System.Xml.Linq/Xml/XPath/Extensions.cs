using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace System.Xml.XPath
{
	/// <summary>This class contains the LINQ to XML extension methods that enable you to evaluate XPath expressions.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000027 RID: 39
	public static class Extensions
	{
		/// <summary>Creates an <see cref="T:System.Xml.XPath.XPathNavigator" /> for an <see cref="T:System.Xml.Linq.XNode" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNavigator" /> that can process XPath queries.</returns>
		/// <param name="node">An <see cref="T:System.Xml.Linq.XNode" /> that can process XPath queries.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060001F9 RID: 505 RVA: 0x00009340 File Offset: 0x00007540
		public static XPathNavigator CreateNavigator(this XNode node)
		{
			return node.CreateNavigator(new NameTable());
		}

		/// <summary>Creates an <see cref="T:System.Xml.XPath.XPathNavigator" /> for an <see cref="T:System.Xml.Linq.XNode" />. The <see cref="T:System.Xml.XmlNameTable" /> enables more efficient XPath expression processing.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNavigator" /> that can process XPath queries.</returns>
		/// <param name="node">An <see cref="T:System.Xml.Linq.XNode" /> that can process an XPath query.</param>
		/// <param name="nameTable">A <see cref="T:System.Xml.XmlNameTable" /> to be used by <see cref="T:System.Xml.XPath.XPathNavigator" />.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060001FA RID: 506 RVA: 0x00009350 File Offset: 0x00007550
		public static XPathNavigator CreateNavigator(this XNode node, XmlNameTable nameTable)
		{
			return new XNodeNavigator(node, nameTable);
		}

		/// <summary>Evaluates an XPath expression.</summary>
		/// <returns>An object that can contain a bool, a double, a string, or an <see cref="T:System.Collections.Generic.IEnumerable`1" />. </returns>
		/// <param name="node">The <see cref="T:System.Xml.Linq.XNode" /> on which to evaluate the XPath expression.</param>
		/// <param name="expression">A <see cref="T:System.String" /> that contains an XPath expression.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060001FB RID: 507 RVA: 0x0000935C File Offset: 0x0000755C
		public static object XPathEvaluate(this XNode node, string expression)
		{
			return node.XPathEvaluate(expression, null);
		}

		/// <summary>Evaluates an XPath expression, resolving namespace prefixes using the specified <see cref="T:System.Xml.IXmlNamespaceResolver" />.</summary>
		/// <returns>An object that contains the result of evaluating the expression. The object can be a bool, a double, a string, or an <see cref="T:System.Collections.Generic.IEnumerable`1" />.</returns>
		/// <param name="node">The <see cref="T:System.Xml.Linq.XNode" /> on which to evaluate the XPath expression.</param>
		/// <param name="expression">A <see cref="T:System.String" /> that contains an XPath expression.</param>
		/// <param name="resolver">A <see cref="T:System.Xml.IXmlNamespaceResolver" /> for the namespace prefixes in the XPath expression.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060001FC RID: 508 RVA: 0x00009368 File Offset: 0x00007568
		public static object XPathEvaluate(this XNode node, string expression, IXmlNamespaceResolver nsResolver)
		{
			return node.CreateNavigator().Evaluate(expression, nsResolver);
		}

		/// <summary>Selects an <see cref="T:System.Xml.Linq.XElement" /> using a XPath expression.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XElement" />, or null.</returns>
		/// <param name="node">The <see cref="T:System.Xml.Linq.XNode" /> on which to evaluate the XPath expression.</param>
		/// <param name="expression">A <see cref="T:System.String" /> that contains an XPath expression.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060001FD RID: 509 RVA: 0x00009378 File Offset: 0x00007578
		public static XElement XPathSelectElement(this XNode node, string xpath)
		{
			return node.XPathSelectElement(xpath, null);
		}

		/// <summary>Selects an <see cref="T:System.Xml.Linq.XElement" /> using a XPath expression, resolving namespace prefixes using the specified <see cref="T:System.Xml.IXmlNamespaceResolver" />.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XElement" />, or null.</returns>
		/// <param name="node">The <see cref="T:System.Xml.Linq.XNode" /> on which to evaluate the XPath expression.</param>
		/// <param name="expression">A <see cref="T:System.String" /> that contains an XPath expression.</param>
		/// <param name="resolver">An <see cref="T:System.Xml.IXmlNamespaceResolver" /> for the namespace prefixes in the XPath expression.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060001FE RID: 510 RVA: 0x00009384 File Offset: 0x00007584
		public static XElement XPathSelectElement(this XNode node, string xpath, IXmlNamespaceResolver nsResolver)
		{
			XPathNavigator xpathNavigator = node.CreateNavigator().SelectSingleNode(xpath, nsResolver);
			if (xpathNavigator == null)
			{
				return null;
			}
			return xpathNavigator.UnderlyingObject as XElement;
		}

		/// <summary>Selects a collection of elements using an XPath expression.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the selected elements.</returns>
		/// <param name="node">The <see cref="T:System.Xml.Linq.XNode" /> on which to evaluate the XPath expression.</param>
		/// <param name="expression">A <see cref="T:System.String" /> that contains an XPath expression.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060001FF RID: 511 RVA: 0x000093B4 File Offset: 0x000075B4
		public static IEnumerable<XElement> XPathSelectElements(this XNode node, string xpath)
		{
			return node.XPathSelectElements(xpath, null);
		}

		/// <summary>Selects a collection of elements using an XPath expression, resolving namespace prefixes using the specified <see cref="T:System.Xml.IXmlNamespaceResolver" />.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the selected elements.</returns>
		/// <param name="node">The <see cref="T:System.Xml.Linq.XNode" /> on which to evaluate the XPath expression.</param>
		/// <param name="expression">A <see cref="T:System.String" /> that contains an XPath expression.</param>
		/// <param name="resolver">A <see cref="T:System.Xml.IXmlNamespaceResolver" /> for the namespace prefixes in the XPath expression.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000200 RID: 512 RVA: 0x000093C0 File Offset: 0x000075C0
		public static IEnumerable<XElement> XPathSelectElements(this XNode node, string xpath, IXmlNamespaceResolver nsResolver)
		{
			XPathNodeIterator iter = node.CreateNavigator().Select(xpath, nsResolver);
			foreach (object obj in iter)
			{
				XPathNavigator nav = (XPathNavigator)obj;
				if (nav.UnderlyingObject is XElement)
				{
					yield return (XElement)nav.UnderlyingObject;
				}
			}
			yield break;
		}
	}
}
