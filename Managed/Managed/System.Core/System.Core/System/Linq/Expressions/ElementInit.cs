using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Linq.Expressions
{
	/// <summary>Represents an initializer for a single element of an <see cref="T:System.Collections.IEnumerable" /> collection.</summary>
	// Token: 0x02000038 RID: 56
	public sealed class ElementInit
	{
		// Token: 0x060003A3 RID: 931 RVA: 0x000111C4 File Offset: 0x0000F3C4
		internal ElementInit(MethodInfo add_method, ReadOnlyCollection<Expression> arguments)
		{
			this.add_method = add_method;
			this.arguments = arguments;
		}

		/// <summary>Gets the instance method that is used to add an element to an <see cref="T:System.Collections.IEnumerable" /> collection.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> that represents an instance method that adds an element to a collection.</returns>
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x000111DC File Offset: 0x0000F3DC
		public MethodInfo AddMethod
		{
			get
			{
				return this.add_method;
			}
		}

		/// <summary>Gets the collection of arguments that are passed to a method that adds an element to an <see cref="T:System.Collections.IEnumerable" /> collection.</summary>
		/// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see cref="T:System.Linq.Expressions.Expression" /> objects that represent the arguments for a method that adds an element to a collection.</returns>
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x000111E4 File Offset: 0x0000F3E4
		public ReadOnlyCollection<Expression> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		/// <summary>Returns a textual representation of an <see cref="T:System.Linq.Expressions.ElementInit" /> object.</summary>
		/// <returns>A textual representation of the <see cref="T:System.Linq.Expressions.ElementInit" /> object.</returns>
		// Token: 0x060003A6 RID: 934 RVA: 0x000111EC File Offset: 0x0000F3EC
		public override string ToString()
		{
			return ExpressionPrinter.ToString(this);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x000111F4 File Offset: 0x0000F3F4
		private void EmitPopIfNeeded(EmitContext ec)
		{
			if (this.add_method.ReturnType == typeof(void))
			{
				return;
			}
			ec.ig.Emit(OpCodes.Pop);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00011224 File Offset: 0x0000F424
		internal void Emit(EmitContext ec, LocalBuilder local)
		{
			ec.EmitCall(local, this.arguments, this.add_method);
			this.EmitPopIfNeeded(ec);
		}

		// Token: 0x040000BD RID: 189
		private MethodInfo add_method;

		// Token: 0x040000BE RID: 190
		private ReadOnlyCollection<Expression> arguments;
	}
}
