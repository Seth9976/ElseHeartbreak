using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Holds a reference to a value.</summary>
	/// <typeparam name="T">The type of the value that the <see cref="T:System.Runtime.CompilerServices.StrongBox`1" /> references.</typeparam>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000019 RID: 25
	public class StrongBox<T> : IStrongBox
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.StrongBox`1" /> class by using the supplied value. </summary>
		/// <param name="value">A value that the <see cref="T:System.Runtime.CompilerServices.StrongBox`1" /> will reference.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000164 RID: 356 RVA: 0x000083AC File Offset: 0x000065AC
		public StrongBox(T value)
		{
			this.Value = value;
		}

		/// <summary>Gets or sets the value that the <see cref="T:System.Runtime.CompilerServices.StrongBox`1" /> references.</summary>
		/// <returns>The value that the <see cref="T:System.Runtime.CompilerServices.StrongBox`1" /> references.</returns>
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000165 RID: 357 RVA: 0x000083BC File Offset: 0x000065BC
		// (set) Token: 0x06000166 RID: 358 RVA: 0x000083CC File Offset: 0x000065CC
		object IStrongBox.Value
		{
			get
			{
				return this.Value;
			}
			set
			{
				this.Value = (T)((object)value);
			}
		}

		/// <summary>Represents the value that the <see cref="T:System.Runtime.CompilerServices.StrongBox`1" /> references.</summary>
		// Token: 0x04000062 RID: 98
		public T Value;
	}
}
