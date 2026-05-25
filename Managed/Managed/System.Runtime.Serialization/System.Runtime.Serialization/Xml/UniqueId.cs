using System;
using System.Collections.Generic;
using System.Security;

namespace System.Xml
{
	/// <summary>A unique identifier optimized for Guids.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200003F RID: 63
	public class UniqueId
	{
		/// <summary>Creates a new instance of this class with a new, unique Guid.</summary>
		// Token: 0x0600016E RID: 366 RVA: 0x00007FC0 File Offset: 0x000061C0
		public UniqueId()
			: this(Guid.NewGuid())
		{
		}

		/// <summary>Creates a new instance of this class using a byte array that represents a <see cref="T:System.Guid" />.</summary>
		/// <param name="guid">A byte array that represents a <see cref="T:System.Guid" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="guid" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="guid" /> provides less than 16 valid bytes.</exception>
		// Token: 0x0600016F RID: 367 RVA: 0x00007FD0 File Offset: 0x000061D0
		public UniqueId(byte[] id)
			: this(id, 0)
		{
		}

		/// <summary>Creates a new instance of this class using a <see cref="T:System.Guid" />.</summary>
		/// <param name="guid">A <see cref="T:System.Guid" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="guid" /> is null.</exception>
		// Token: 0x06000170 RID: 368 RVA: 0x00007FDC File Offset: 0x000061DC
		public UniqueId(Guid id)
		{
			this.guid = id;
		}

		/// <summary>Creates a new instance of this class using a string.</summary>
		/// <param name="value">A string used to generate the <see cref="T:System.Xml.UniqueId" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.FormatException">Length of<paramref name=" value" /> is zero.</exception>
		// Token: 0x06000171 RID: 369 RVA: 0x00007FEC File Offset: 0x000061EC
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public UniqueId(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value cannot be null", "value");
			}
			if (value.Length == 0)
			{
				throw new FormatException("UniqueId cannot be zero length");
			}
			this.id = value;
		}

		/// <summary>Creates a new instance of this class starting from an offset within a byte array that represents a <see cref="T:System.Guid" />.</summary>
		/// <param name="guid">A byte array that represents a <see cref="T:System.Guid" />.</param>
		/// <param name="offset">Offset position within the byte array that represents a <see cref="T:System.Guid" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="guid" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> less than zero or greater than the length of the array.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="guid " />and<paramref name=" offset" /> provide less than 16 valid bytes.</exception>
		// Token: 0x06000172 RID: 370 RVA: 0x00008028 File Offset: 0x00006228
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public UniqueId(byte[] id, int offset)
		{
			if (id == null)
			{
				throw new ArgumentNullException();
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset >= id.Length)
			{
				throw new ArgumentException("id too small.", "offset");
			}
			if (id.Length - offset != 16)
			{
				throw new ArgumentException("id and offset provide less than 16 bytes");
			}
			if (offset == 0)
			{
				this.guid = new Guid(id);
			}
			else
			{
				List<byte> list = new List<byte>(id);
				list.RemoveRange(0, offset);
				this.guid = new Guid(list.ToArray());
			}
		}

		/// <summary>Creates a new instance of this class starting from an offset within a char using a specified number of entries.</summary>
		/// <param name="chars">A char array that represents a <see cref="T:System.Guid" />.</param>
		/// <param name="offset">Offset position within the char array that represents a <see cref="T:System.Guid" />.</param>
		/// <param name="count">Number of array entries to use, starting from <paramref name="offset" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> less than zero or greater than the length of the array.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> less than zero or greater than the length of the array minus <paramref name="offset" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="count" /> equals zero.</exception>
		// Token: 0x06000173 RID: 371 RVA: 0x000080C0 File Offset: 0x000062C0
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public UniqueId(char[] id, int offset, int count)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (offset < 0 || offset >= id.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || id.Length - offset < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count == 0)
			{
				throw new FormatException();
			}
			if (count > 8 && id[offset] == 'u' && id[offset + 1] == 'r' && id[offset + 2] == 'n' && id[offset + 3] == ':' && id[offset + 4] == 'u' && id[offset + 5] == 'u' && id[offset + 6] == 'i' && id[offset + 7] == 'd' && id[offset + 8] == ':')
			{
				if (count != 45)
				{
					throw new ArgumentOutOfRangeException("Invalid Guid");
				}
				this.guid = new Guid(new string(id, offset + 9, count - 9));
			}
			else
			{
				this.id = new string(id, offset, count);
			}
		}

		/// <summary>Gets the length of the string representation of the <see cref="T:System.Xml.UniqueId" />.</summary>
		/// <returns>The length of the string representation of the <see cref="T:System.Xml.UniqueId" />.</returns>
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000174 RID: 372 RVA: 0x000081D8 File Offset: 0x000063D8
		public int CharArrayLength
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				return (this.id == null) ? 45 : this.id.Length;
			}
		}

		/// <summary>Indicates whether the <see cref="T:System.Xml.UniqueId" /> is a <see cref="T:System.Guid" />.</summary>
		/// <returns>true if the <see cref="T:System.Xml.UniqueId" /> is a <see cref="T:System.Guid" />; otherwise false.</returns>
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000175 RID: 373 RVA: 0x000081F8 File Offset: 0x000063F8
		public bool IsGuid
		{
			get
			{
				return this.guid != default(Guid);
			}
		}

		/// <summary>Tests whether an object equals this <see cref="T:System.Xml.UniqueId" />.</summary>
		/// <returns>true if the object equals this <see cref="T:System.Xml.UniqueId" />; otherwise false.</returns>
		/// <param name="obj">The object to compare.</param>
		// Token: 0x06000176 RID: 374 RVA: 0x0000821C File Offset: 0x0000641C
		public override bool Equals(object obj)
		{
			UniqueId uniqueId = obj as UniqueId;
			if (uniqueId == null)
			{
				return false;
			}
			if (this.IsGuid && uniqueId.IsGuid)
			{
				return this.guid.Equals(uniqueId.guid);
			}
			return this.id == uniqueId.id;
		}

		/// <summary>Creates a hash-code representation of this <see cref="T:System.Xml.UniqueId" />.</summary>
		/// <returns>An integer hash-code representation of this <see cref="T:System.Xml.UniqueId" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000177 RID: 375 RVA: 0x00008278 File Offset: 0x00006478
		[MonoTODO("Determine semantics when IsGuid==true")]
		public override int GetHashCode()
		{
			return (this.id == null) ? this.guid.GetHashCode() : this.id.GetHashCode();
		}

		/// <summary>Puts the <see cref="T:System.Xml.UniqueId" /> value into a char array.</summary>
		/// <returns>Number of entries in the char array filled by the <see cref="T:System.Xml.UniqueId" /> value.</returns>
		/// <param name="chars">The char array.</param>
		/// <param name="offset">Position in the char array to start inserting the <see cref="T:System.Xml.UniqueId" /> value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> less than zero or greater than the length of the array.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="guid " />and<paramref name=" offset" /> provide less than 16 valid bytes.</exception>
		// Token: 0x06000178 RID: 376 RVA: 0x000082AC File Offset: 0x000064AC
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public int ToCharArray(char[] array, int offset)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (offset < 0 || offset >= array.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			string text = this.ToString();
			text.CopyTo(0, array, offset, text.Length);
			return text.Length;
		}

		/// <summary>Displays the <see cref="T:System.Xml.UniqueId" /> value in string format.</summary>
		/// <returns>A string representation of the <see cref="T:System.Xml.UniqueId" /> value.</returns>
		// Token: 0x06000179 RID: 377 RVA: 0x00008304 File Offset: 0x00006504
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public override string ToString()
		{
			if (this.id == null)
			{
				return "urn:uuid:" + this.guid;
			}
			return this.id;
		}

		/// <summary>Tries to get the value of the <see cref="T:System.Xml.UniqueId" /> as a <see cref="T:System.Guid" />.</summary>
		/// <returns>true if the UniqueId represents a <see cref="T:System.Guid" />; otherwise false.</returns>
		/// <param name="guid">The <see cref="T:System.Guid" /> if successful; otherwise <see cref="F:System.Guid.Empty" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="buffer " />and<paramref name=" offset" /> provide less than 16 valid bytes.</exception>
		// Token: 0x0600017A RID: 378 RVA: 0x00008330 File Offset: 0x00006530
		public bool TryGetGuid(out Guid guid)
		{
			if (this.IsGuid)
			{
				guid = this.guid;
				return true;
			}
			guid = default(Guid);
			return false;
		}

		/// <summary>Tries to get the value of the <see cref="T:System.Xml.UniqueId" /> as a <see cref="T:System.Guid" /> and store it in the given byte array at the specified offest.</summary>
		/// <returns>true if the value stored in this instance of <see cref="T:System.Xml.UniqueId" /> is a <see cref="T:System.Guid" />; otherwise false.</returns>
		/// <param name="buffer">byte array that will contain the <see cref="T:System.Guid" />.</param>
		/// <param name="offset">Position in the byte array to start inserting the <see cref="T:System.Guid" /> value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> less than zero or greater than the length of the array.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="buffer " />and<paramref name=" offset" /> provide less than 16 valid bytes.</exception>
		// Token: 0x0600017B RID: 379 RVA: 0x00008368 File Offset: 0x00006568
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public bool TryGetGuid(byte[] buffer, int offset)
		{
			if (!this.IsGuid)
			{
				return false;
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset >= buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (buffer.Length - offset < 16)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			this.guid.ToByteArray().CopyTo(buffer, offset);
			return true;
		}

		/// <summary>Overrides the equality operator to test for equality of two <see cref="T:System.Xml.UniqueId" />s.</summary>
		/// <returns>true if the two <see cref="T:System.Xml.UniqueId" />s are equal, or are both null; false if they are not equal, or if only one of them is null.</returns>
		/// <param name="id1">The first <see cref="T:System.Xml.UniqueId" />.</param>
		/// <param name="id2">The second <see cref="T:System.Xml.UniqueId" />.</param>
		// Token: 0x0600017C RID: 380 RVA: 0x000083D8 File Offset: 0x000065D8
		public static bool operator ==(UniqueId id1, UniqueId id2)
		{
			return object.Equals(id1, id2);
		}

		/// <summary>Overrides the equality operator to test for inequality of two <see cref="T:System.Xml.UniqueId" />s.</summary>
		/// <returns>true if the overridden equality operator returns false; otherwise false.</returns>
		/// <param name="id1">The first <see cref="T:System.Xml.UniqueId" />.</param>
		/// <param name="id2">The second <see cref="T:System.Xml.UniqueId" />.</param>
		// Token: 0x0600017D RID: 381 RVA: 0x000083E4 File Offset: 0x000065E4
		public static bool operator !=(UniqueId id1, UniqueId id2)
		{
			return !(id1 == id2);
		}

		// Token: 0x040000B0 RID: 176
		private Guid guid;

		// Token: 0x040000B1 RID: 177
		private string id;
	}
}
