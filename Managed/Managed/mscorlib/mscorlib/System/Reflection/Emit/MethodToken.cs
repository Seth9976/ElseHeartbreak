using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>The MethodToken struct is an object representation of a token that represents a method.</summary>
	// Token: 0x020002E9 RID: 745
	[ComVisible(true)]
	[Serializable]
	public struct MethodToken
	{
		// Token: 0x06002634 RID: 9780 RVA: 0x00087188 File Offset: 0x00085388
		internal MethodToken(int val)
		{
			this.tokValue = val;
		}

		/// <summary>Tests whether the given object is equal to this MethodToken object.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of MethodToken and is equal to this object; otherwise, false.</returns>
		/// <param name="obj">The object to compare to this object. </param>
		// Token: 0x06002636 RID: 9782 RVA: 0x000871B0 File Offset: 0x000853B0
		public override bool Equals(object obj)
		{
			bool flag = obj is MethodToken;
			if (flag)
			{
				MethodToken methodToken = (MethodToken)obj;
				flag = this.tokValue == methodToken.tokValue;
			}
			return flag;
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.MethodToken" />.</summary>
		/// <returns>true if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.MethodToken" /> to compare to the current instance.</param>
		// Token: 0x06002637 RID: 9783 RVA: 0x000871E8 File Offset: 0x000853E8
		public bool Equals(MethodToken obj)
		{
			return this.tokValue == obj.tokValue;
		}

		/// <summary>Returns the generated hash code for this method.</summary>
		/// <returns>Returns the hash code for this instance.</returns>
		// Token: 0x06002638 RID: 9784 RVA: 0x000871FC File Offset: 0x000853FC
		public override int GetHashCode()
		{
			return this.tokValue;
		}

		/// <summary>Returns the metadata token for this method.</summary>
		/// <returns>Read-only. Returns the metadata token for this method.</returns>
		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06002639 RID: 9785 RVA: 0x00087204 File Offset: 0x00085404
		public int Token
		{
			get
			{
				return this.tokValue;
			}
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.MethodToken" /> structures are equal.</summary>
		/// <returns>true if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.MethodToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.MethodToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x0600263A RID: 9786 RVA: 0x0008720C File Offset: 0x0008540C
		public static bool operator ==(MethodToken a, MethodToken b)
		{
			return object.Equals(a, b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.MethodToken" /> structures are not equal.</summary>
		/// <returns>true if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.MethodToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.MethodToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x0600263B RID: 9787 RVA: 0x00087220 File Offset: 0x00085420
		public static bool operator !=(MethodToken a, MethodToken b)
		{
			return !object.Equals(a, b);
		}

		// Token: 0x04000E5E RID: 3678
		internal int tokValue;

		/// <summary>The default MethodToken with <see cref="P:System.Reflection.Emit.MethodToken.Token" /> value 0.</summary>
		// Token: 0x04000E5F RID: 3679
		public static readonly MethodToken Empty = default(MethodToken);
	}
}
