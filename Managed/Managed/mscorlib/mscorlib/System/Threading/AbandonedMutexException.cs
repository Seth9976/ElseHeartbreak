using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	/// <summary>The exception that is thrown when one thread acquires a <see cref="T:System.Threading.Mutex" /> object that another thread has abandoned by exiting without releasing it.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000696 RID: 1686
	[ComVisible(false)]
	[Serializable]
	public class AbandonedMutexException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.AbandonedMutexException" /> class with default values.</summary>
		// Token: 0x0600404C RID: 16460 RVA: 0x000DEA8C File Offset: 0x000DCC8C
		public AbandonedMutexException()
			: base("Mutex was abandoned")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.AbandonedMutexException" /> class with a specified error message.</summary>
		/// <param name="message">An error message that explains the reason for the exception.</param>
		// Token: 0x0600404D RID: 16461 RVA: 0x000DEAA0 File Offset: 0x000DCCA0
		public AbandonedMutexException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.AbandonedMutexException" /> class with a specified index for the abandoned mutex, if applicable, and a <see cref="T:System.Threading.Mutex" /> object that represents the mutex.</summary>
		/// <param name="location">The index of the abandoned mutex in the array of wait handles if the exception is thrown for the <see cref="Overload:System.Threading.WaitHandle.WaitAny" /> method, or –1 if the exception is thrown for the <see cref="Overload:System.Threading.WaitHandle.WaitOne" /> or <see cref="Overload:System.Threading.WaitHandle.WaitAll" /> methods.</param>
		/// <param name="handle">A <see cref="T:System.Threading.Mutex" /> object that represents the abandoned mutex.</param>
		// Token: 0x0600404E RID: 16462 RVA: 0x000DEAB0 File Offset: 0x000DCCB0
		public AbandonedMutexException(int location, WaitHandle handle)
			: base("Mutex was abandoned")
		{
			this.mutex_index = location;
			this.mutex = handle as Mutex;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.AbandonedMutexException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination.</param>
		// Token: 0x0600404F RID: 16463 RVA: 0x000DEAD8 File Offset: 0x000DCCD8
		protected AbandonedMutexException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.AbandonedMutexException" /> class with a specified error message and inner exception. </summary>
		/// <param name="message">An error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception.</param>
		// Token: 0x06004050 RID: 16464 RVA: 0x000DEAEC File Offset: 0x000DCCEC
		public AbandonedMutexException(string message, Exception inner)
			: base(message, inner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.AbandonedMutexException" /> class with a specified error message, the index of the abandoned mutex, if applicable, and the abandoned mutex. </summary>
		/// <param name="message">An error message that explains the reason for the exception.</param>
		/// <param name="location">The index of the abandoned mutex in the array of wait handles if the exception is thrown for the <see cref="Overload:System.Threading.WaitHandle.WaitAny" /> method, or –1 if the exception is thrown for the <see cref="Overload:System.Threading.WaitHandle.WaitOne" /> or <see cref="Overload:System.Threading.WaitHandle.WaitAll" /> methods.</param>
		/// <param name="handle">A <see cref="T:System.Threading.Mutex" /> object that represents the abandoned mutex.</param>
		// Token: 0x06004051 RID: 16465 RVA: 0x000DEB00 File Offset: 0x000DCD00
		public AbandonedMutexException(string message, int location, WaitHandle handle)
			: base(message)
		{
			this.mutex_index = location;
			this.mutex = handle as Mutex;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.AbandonedMutexException" /> class with a specified error message, the inner exception, the index for the abandoned mutex, if applicable, and a <see cref="T:System.Threading.Mutex" /> object that represents the mutex.</summary>
		/// <param name="message">An error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception.</param>
		/// <param name="location">The index of the abandoned mutex in the array of wait handles if the exception is thrown for the <see cref="Overload:System.Threading.WaitHandle.WaitAny" /> method, or –1 if the exception is thrown for the <see cref="Overload:System.Threading.WaitHandle.WaitOne" /> or <see cref="Overload:System.Threading.WaitHandle.WaitAll" /> methods.</param>
		/// <param name="handle">A <see cref="T:System.Threading.Mutex" /> object that represents the abandoned mutex.</param>
		// Token: 0x06004052 RID: 16466 RVA: 0x000DEB24 File Offset: 0x000DCD24
		public AbandonedMutexException(string message, Exception inner, int location, WaitHandle handle)
			: base(message, inner)
		{
			this.mutex_index = location;
			this.mutex = handle as Mutex;
		}

		/// <summary>Gets the abandoned mutex that caused the exception, if known.</summary>
		/// <returns>A <see cref="T:System.Threading.Mutex" /> object that represents the abandoned mutex, or null if the abandoned mutex could not be identified.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x06004053 RID: 16467 RVA: 0x000DEB4C File Offset: 0x000DCD4C
		public Mutex Mutex
		{
			get
			{
				return this.mutex;
			}
		}

		/// <summary>Gets the index of the abandoned mutex that caused the exception, if known.</summary>
		/// <returns>The index, in the array of wait handles passed to the <see cref="Overload:System.Threading.WaitHandle.WaitAny" /> method, of the <see cref="T:System.Threading.Mutex" /> object that represents the abandoned mutex, or –1 if the index of the abandoned mutex could not be determined.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x06004054 RID: 16468 RVA: 0x000DEB54 File Offset: 0x000DCD54
		public int MutexIndex
		{
			get
			{
				return this.mutex_index;
			}
		}

		// Token: 0x04001BA0 RID: 7072
		private Mutex mutex;

		// Token: 0x04001BA1 RID: 7073
		private int mutex_index = -1;
	}
}
