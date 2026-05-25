using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200008E RID: 142
	internal abstract class JsonSerializerInternalBase
	{
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x0001775A File Offset: 0x0001595A
		// (set) Token: 0x060006D8 RID: 1752 RVA: 0x00017762 File Offset: 0x00015962
		internal JsonSerializer Serializer { get; private set; }

		// Token: 0x060006D9 RID: 1753 RVA: 0x0001776B File Offset: 0x0001596B
		protected JsonSerializerInternalBase(JsonSerializer serializer)
		{
			ValidationUtils.ArgumentNotNull(serializer, "serializer");
			this.Serializer = serializer;
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x00017785 File Offset: 0x00015985
		internal BidirectionalDictionary<string, object> DefaultReferenceMappings
		{
			get
			{
				if (this._mappings == null)
				{
					this._mappings = new BidirectionalDictionary<string, object>(EqualityComparer<string>.Default, new JsonSerializerInternalBase.ReferenceEqualsEqualityComparer());
				}
				return this._mappings;
			}
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x000177AA File Offset: 0x000159AA
		protected ErrorContext GetErrorContext(object currentObject, object member, Exception error)
		{
			if (this._currentErrorContext == null)
			{
				this._currentErrorContext = new ErrorContext(currentObject, member, error);
			}
			if (this._currentErrorContext.Error != error)
			{
				throw new InvalidOperationException("Current error context error is different to requested error.");
			}
			return this._currentErrorContext;
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x000177E1 File Offset: 0x000159E1
		protected void ClearErrorContext()
		{
			if (this._currentErrorContext == null)
			{
				throw new InvalidOperationException("Could not clear error context. Error context is already null.");
			}
			this._currentErrorContext = null;
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00017800 File Offset: 0x00015A00
		protected bool IsErrorHandled(object currentObject, JsonContract contract, object keyValue, Exception ex)
		{
			ErrorContext errorContext = this.GetErrorContext(currentObject, keyValue, ex);
			contract.InvokeOnError(currentObject, this.Serializer.Context, errorContext);
			if (!errorContext.Handled)
			{
				this.Serializer.OnError(new ErrorEventArgs(currentObject, errorContext));
			}
			return errorContext.Handled;
		}

		// Token: 0x0400022D RID: 557
		private ErrorContext _currentErrorContext;

		// Token: 0x0400022E RID: 558
		private BidirectionalDictionary<string, object> _mappings;

		// Token: 0x0200008F RID: 143
		private class ReferenceEqualsEqualityComparer : IEqualityComparer<object>
		{
			// Token: 0x060006DE RID: 1758 RVA: 0x0001784B File Offset: 0x00015A4B
			bool IEqualityComparer<object>.Equals(object x, object y)
			{
				return object.ReferenceEquals(x, y);
			}

			// Token: 0x060006DF RID: 1759 RVA: 0x00017854 File Offset: 0x00015A54
			int IEqualityComparer<object>.GetHashCode(object obj)
			{
				return RuntimeHelpers.GetHashCode(obj);
			}
		}
	}
}
