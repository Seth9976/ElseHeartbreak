using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x02000028 RID: 40
	public abstract class JToken : IJEnumerable<JToken>, IEnumerable<JToken>, IEnumerable, IJsonLineInfo, ICloneable
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000149 RID: 329 RVA: 0x000066BC File Offset: 0x000048BC
		public static JTokenEqualityComparer EqualityComparer
		{
			get
			{
				if (JToken._equalityComparer == null)
				{
					JToken._equalityComparer = new JTokenEqualityComparer();
				}
				return JToken._equalityComparer;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600014A RID: 330 RVA: 0x000066D4 File Offset: 0x000048D4
		// (set) Token: 0x0600014B RID: 331 RVA: 0x000066DC File Offset: 0x000048DC
		public JContainer Parent
		{
			[DebuggerStepThrough]
			get
			{
				return this._parent;
			}
			internal set
			{
				this._parent = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600014C RID: 332 RVA: 0x000066E8 File Offset: 0x000048E8
		public JToken Root
		{
			get
			{
				JContainer jcontainer = this.Parent;
				if (jcontainer == null)
				{
					return this;
				}
				while (jcontainer.Parent != null)
				{
					jcontainer = jcontainer.Parent;
				}
				return jcontainer;
			}
		}

		// Token: 0x0600014D RID: 333
		internal abstract JToken CloneToken();

		// Token: 0x0600014E RID: 334
		internal abstract bool DeepEquals(JToken node);

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600014F RID: 335
		public abstract JTokenType Type { get; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000150 RID: 336
		public abstract bool HasValues { get; }

		// Token: 0x06000151 RID: 337 RVA: 0x00006711 File Offset: 0x00004911
		public static bool DeepEquals(JToken t1, JToken t2)
		{
			return t1 == t2 || (t1 != null && t2 != null && t1.DeepEquals(t2));
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00006728 File Offset: 0x00004928
		// (set) Token: 0x06000153 RID: 339 RVA: 0x00006730 File Offset: 0x00004930
		public JToken Next
		{
			get
			{
				return this._next;
			}
			internal set
			{
				this._next = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00006739 File Offset: 0x00004939
		// (set) Token: 0x06000155 RID: 341 RVA: 0x00006741 File Offset: 0x00004941
		public JToken Previous
		{
			get
			{
				return this._previous;
			}
			internal set
			{
				this._previous = value;
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000674A File Offset: 0x0000494A
		internal JToken()
		{
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00006754 File Offset: 0x00004954
		public void AddAfterSelf(object content)
		{
			if (this._parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			int num = this._parent.IndexOfItem(this);
			this._parent.AddInternal(num + 1, content);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00006790 File Offset: 0x00004990
		public void AddBeforeSelf(object content)
		{
			if (this._parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			int num = this._parent.IndexOfItem(this);
			this._parent.AddInternal(num, content);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000068C8 File Offset: 0x00004AC8
		public IEnumerable<JToken> Ancestors()
		{
			for (JToken parent = this.Parent; parent != null; parent = parent.Parent)
			{
				yield return parent;
			}
			yield break;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000069F4 File Offset: 0x00004BF4
		public IEnumerable<JToken> AfterSelf()
		{
			if (this.Parent != null)
			{
				for (JToken o = this.Next; o != null; o = o.Next)
				{
					yield return o;
				}
			}
			yield break;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00006B1C File Offset: 0x00004D1C
		public IEnumerable<JToken> BeforeSelf()
		{
			for (JToken o = this.Parent.First; o != this; o = o.Next)
			{
				yield return o;
			}
			yield break;
		}

		// Token: 0x17000035 RID: 53
		public virtual JToken this[object key]
		{
			get
			{
				throw new InvalidOperationException("Cannot access child value on {0}.".FormatWith(CultureInfo.InvariantCulture, new object[] { base.GetType() }));
			}
			set
			{
				throw new InvalidOperationException("Cannot set child value on {0}.".FormatWith(CultureInfo.InvariantCulture, new object[] { base.GetType() }));
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00006BA4 File Offset: 0x00004DA4
		public virtual T Value<T>(object key)
		{
			JToken jtoken = this[key];
			return jtoken.Convert<JToken, T>();
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00006BC0 File Offset: 0x00004DC0
		public virtual JToken First
		{
			get
			{
				throw new InvalidOperationException("Cannot access child value on {0}.".FormatWith(CultureInfo.InvariantCulture, new object[] { base.GetType() }));
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00006BF4 File Offset: 0x00004DF4
		public virtual JToken Last
		{
			get
			{
				throw new InvalidOperationException("Cannot access child value on {0}.".FormatWith(CultureInfo.InvariantCulture, new object[] { base.GetType() }));
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00006C26 File Offset: 0x00004E26
		public virtual JEnumerable<JToken> Children()
		{
			return JEnumerable<JToken>.Empty;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00006C2D File Offset: 0x00004E2D
		public JEnumerable<T> Children<T>() where T : JToken
		{
			return new JEnumerable<T>(this.Children().OfType<T>());
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00006C44 File Offset: 0x00004E44
		public virtual IEnumerable<T> Values<T>()
		{
			throw new InvalidOperationException("Cannot access child value on {0}.".FormatWith(CultureInfo.InvariantCulture, new object[] { base.GetType() }));
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00006C76 File Offset: 0x00004E76
		public void Remove()
		{
			if (this._parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			this._parent.RemoveItem(this);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00006C98 File Offset: 0x00004E98
		public void Replace(JToken value)
		{
			if (this._parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			this._parent.ReplaceItem(this, value);
		}

		// Token: 0x06000166 RID: 358
		public abstract void WriteTo(JsonWriter writer, params JsonConverter[] converters);

		// Token: 0x06000167 RID: 359 RVA: 0x00006CBA File Offset: 0x00004EBA
		public override string ToString()
		{
			return this.ToString(Formatting.Indented, new JsonConverter[0]);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00006CCC File Offset: 0x00004ECC
		public string ToString(Formatting formatting, params JsonConverter[] converters)
		{
			string text;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				this.WriteTo(new JsonTextWriter(stringWriter)
				{
					Formatting = formatting
				}, converters);
				text = stringWriter.ToString();
			}
			return text;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00006D20 File Offset: 0x00004F20
		private static JValue EnsureValue(JToken value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value is JProperty)
			{
				value = ((JProperty)value).Value;
			}
			return value as JValue;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00006D58 File Offset: 0x00004F58
		private static string GetType(JToken token)
		{
			ValidationUtils.ArgumentNotNull(token, "token");
			if (token is JProperty)
			{
				token = ((JProperty)token).Value;
			}
			return token.Type.ToString();
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00006D8A File Offset: 0x00004F8A
		private static bool IsNullable(JToken o)
		{
			return o.Type == JTokenType.Undefined || o.Type == JTokenType.Null;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00006DA2 File Offset: 0x00004FA2
		private static bool ValidateFloat(JToken o, bool nullable)
		{
			return o.Type == JTokenType.Float || o.Type == JTokenType.Integer || (nullable && JToken.IsNullable(o));
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00006DC3 File Offset: 0x00004FC3
		private static bool ValidateInteger(JToken o, bool nullable)
		{
			return o.Type == JTokenType.Integer || (nullable && JToken.IsNullable(o));
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00006DDB File Offset: 0x00004FDB
		private static bool ValidateDate(JToken o, bool nullable)
		{
			return o.Type == JTokenType.Date || (nullable && JToken.IsNullable(o));
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00006DF4 File Offset: 0x00004FF4
		private static bool ValidateBoolean(JToken o, bool nullable)
		{
			return o.Type == JTokenType.Boolean || (nullable && JToken.IsNullable(o));
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00006E0D File Offset: 0x0000500D
		private static bool ValidateString(JToken o)
		{
			return o.Type == JTokenType.String || o.Type == JTokenType.Comment || o.Type == JTokenType.Raw || JToken.IsNullable(o);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00006E33 File Offset: 0x00005033
		private static bool ValidateBytes(JToken o)
		{
			return o.Type == JTokenType.Bytes || JToken.IsNullable(o);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00006E48 File Offset: 0x00005048
		public static explicit operator bool(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateBoolean(jvalue, false))
			{
				throw new ArgumentException("Can not convert {0} to Boolean.".FormatWith(CultureInfo.InvariantCulture, new object[] { JToken.GetType(value) }));
			}
			return Convert.ToBoolean(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00006EA0 File Offset: 0x000050A0
		public static explicit operator DateTimeOffset(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateDate(jvalue, false))
			{
				throw new ArgumentException("Can not convert {0} to DateTimeOffset.".FormatWith(CultureInfo.InvariantCulture, new object[] { JToken.GetType(value) }));
			}
			return (DateTimeOffset)jvalue.Value;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00006EF4 File Offset: 0x000050F4
		public static explicit operator bool?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateBoolean(jvalue, true))
			{
				throw new ArgumentException("Can not convert {0} to Boolean.".FormatWith(CultureInfo.InvariantCulture, new object[] { JToken.GetType(value) }));
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new bool?(Convert.ToBoolean(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00006F70 File Offset: 0x00005170
		public static explicit operator long(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateInteger(jvalue, false))
			{
				throw new ArgumentException("Can not convert {0} to Int64.".FormatWith(CultureInfo.InvariantCulture, new object[] { JToken.GetType(value) }));
			}
			return Convert.ToInt64(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00006FC8 File Offset: 0x000051C8
		public static explicit operator DateTime?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateDate(jvalue, true))
			{
				throw new ArgumentException("Can not convert {0} to DateTime.".FormatWith(CultureInfo.InvariantCulture, new object[] { JToken.GetType(value) }));
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new DateTime?(Convert.ToDateTime(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00007044 File Offset: 0x00005244
		public static explicit operator DateTimeOffset?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateDate(jvalue, true))
			{
				throw new ArgumentException("Can not convert {0} to DateTimeOffset.".FormatWith(CultureInfo.InvariantCulture, new object[] { JToken.GetType(value) }));
			}
			return (DateTimeOffset?)jvalue.Value;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000070A4 File Offset: 0x000052A4
		public static explicit operator decimal?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateFloat(jvalue, true))
			{
				throw new ArgumentException("Can not convert {0} to Decimal.".FormatWith(CultureInfo.InvariantCulture, new object[] { JToken.GetType(value) }));
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new decimal?(Convert.ToDecimal(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00007120 File Offset: 0x00005320
		public static explicit operator double?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateFloat(jvalue, true))
			{
				throw new ArgumentException("Can not convert {0} to Double.".FormatWith(CultureInfo.InvariantCulture, new object[] { JToken.GetType(value) }));
			}
			return (double?)jvalue.Value;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00007180 File Offset: 0x00005380
		public static explicit operator int(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateInteger(jvalue, false))
			{
				throw new ArgumentException("Can not convert {0} to Int32.".FormatWith(CultureInfo.InvariantCulture, new object[] { JToken.GetType(value) }));
			}
			return Convert.ToInt32(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000071D8 File Offset: 0x000053D8
		public static explicit operator short(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateInteger(jvalue, false))
			{
				throw new ArgumentException("Can not convert {0} to Int16.".FormatWith(CultureInfo.InvariantCulture, new object[] { JToken.GetType(value) }));
			}
			return Convert.ToInt16(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00007230 File Offset: 0x00005430
		[CLSCompliant(false)]
		public static explicit operator ushort(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateInteger(jvalue, false))
			{
				throw new ArgumentException("Can not convert {0} to UInt16.".FormatWith(CultureInfo.InvariantCulture, new object[] { JToken.GetType(value) }));
			}
			return Convert.ToUInt16(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00007288 File Offset: 0x00005488
		public static explicit operator int?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateInteger(jvalue, true))
			{
				throw new ArgumentException("Can not convert {0} to Int32.".FormatWith(CultureInfo.InvariantCulture, new object[] { JToken.GetType(value) }));
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new int?(Convert.ToInt32(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00007304 File Offset: 0x00005504
		public static explicit operator short?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateInteger(jvalue, true))
			{
				throw new ArgumentException("Can not convert {0} to Int16.".FormatWith(CultureInfo.InvariantCulture, new object[] { JToken.GetType(value) }));
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new short?(Convert.ToInt16(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00007380 File Offset: 0x00005580
		[CLSCompliant(false)]
		public static explicit operator ushort?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateInteger(jvalue, true))
			{
				throw new ArgumentException("Can not convert {0} to UInt16.".FormatWith(CultureInfo.InvariantCulture, new object[] { JToken.GetType(value) }));
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new ushort?((ushort)Convert.ToInt16(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000073FC File Offset: 0x000055FC
		public static explicit operator DateTime(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateDate(jvalue, false))
			{
				throw new ArgumentException("Can not convert {0} to DateTime.".FormatWith(CultureInfo.InvariantCulture, new object[] { JToken.GetType(value) }));
			}
			return Convert.ToDateTime(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00007454 File Offset: 0x00005654
		public static explicit operator long?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateInteger(jvalue, true))
			{
				throw new ArgumentException("Can not convert {0} to Int64.".FormatWith(CultureInfo.InvariantCulture, new object[] { JToken.GetType(value) }));
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new long?(Convert.ToInt64(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000074D0 File Offset: 0x000056D0
		public static explicit operator float?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateFloat(jvalue, true))
			{
				throw new ArgumentException("Can not convert {0} to Single.".FormatWith(CultureInfo.InvariantCulture, new object[] { JToken.GetType(value) }));
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new float?(Convert.ToSingle(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000754C File Offset: 0x0000574C
		public static explicit operator decimal(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateFloat(jvalue, false))
			{
				throw new ArgumentException("Can not convert {0} to Decimal.".FormatWith(CultureInfo.InvariantCulture, new object[] { JToken.GetType(value) }));
			}
			return Convert.ToDecimal(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000075A4 File Offset: 0x000057A4
		[CLSCompliant(false)]
		public static explicit operator uint?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateInteger(jvalue, true))
			{
				throw new ArgumentException("Can not convert {0} to UInt32.".FormatWith(CultureInfo.InvariantCulture, new object[] { JToken.GetType(value) }));
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new uint?(Convert.ToUInt32(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00007620 File Offset: 0x00005820
		[CLSCompliant(false)]
		public static explicit operator ulong?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateInteger(jvalue, true))
			{
				throw new ArgumentException("Can not convert {0} to UInt64.".FormatWith(CultureInfo.InvariantCulture, new object[] { JToken.GetType(value) }));
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new ulong?(Convert.ToUInt64(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000769C File Offset: 0x0000589C
		public static explicit operator double(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateFloat(jvalue, false))
			{
				throw new ArgumentException("Can not convert {0} to Double.".FormatWith(CultureInfo.InvariantCulture, new object[] { JToken.GetType(value) }));
			}
			return Convert.ToDouble(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000076F4 File Offset: 0x000058F4
		public static explicit operator float(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateFloat(jvalue, false))
			{
				throw new ArgumentException("Can not convert {0} to Single.".FormatWith(CultureInfo.InvariantCulture, new object[] { JToken.GetType(value) }));
			}
			return Convert.ToSingle(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000774C File Offset: 0x0000594C
		public static explicit operator string(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateString(jvalue))
			{
				throw new ArgumentException("Can not convert {0} to String.".FormatWith(CultureInfo.InvariantCulture, new object[] { JToken.GetType(value) }));
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return Convert.ToString(jvalue.Value);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000077AC File Offset: 0x000059AC
		[CLSCompliant(false)]
		public static explicit operator uint(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateInteger(jvalue, false))
			{
				throw new ArgumentException("Can not convert {0} to UInt32.".FormatWith(CultureInfo.InvariantCulture, new object[] { JToken.GetType(value) }));
			}
			return Convert.ToUInt32(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00007804 File Offset: 0x00005A04
		[CLSCompliant(false)]
		public static explicit operator ulong(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateInteger(jvalue, false))
			{
				throw new ArgumentException("Can not convert {0} to UInt64.".FormatWith(CultureInfo.InvariantCulture, new object[] { JToken.GetType(value) }));
			}
			return Convert.ToUInt64(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000785C File Offset: 0x00005A5C
		public static explicit operator byte[](JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateBytes(jvalue))
			{
				throw new ArgumentException("Can not convert {0} to byte array.".FormatWith(CultureInfo.InvariantCulture, new object[] { JToken.GetType(value) }));
			}
			return (byte[])jvalue.Value;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000078AC File Offset: 0x00005AAC
		public static implicit operator JToken(bool value)
		{
			return new JValue(value);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000078B4 File Offset: 0x00005AB4
		public static implicit operator JToken(DateTimeOffset value)
		{
			return new JValue(value);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000078C1 File Offset: 0x00005AC1
		public static implicit operator JToken(bool? value)
		{
			return new JValue(value);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000078CE File Offset: 0x00005ACE
		public static implicit operator JToken(long value)
		{
			return new JValue(value);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000078D6 File Offset: 0x00005AD6
		public static implicit operator JToken(DateTime? value)
		{
			return new JValue(value);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000078E3 File Offset: 0x00005AE3
		public static implicit operator JToken(DateTimeOffset? value)
		{
			return new JValue(value);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000078F0 File Offset: 0x00005AF0
		public static implicit operator JToken(decimal? value)
		{
			return new JValue(value);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000078FD File Offset: 0x00005AFD
		public static implicit operator JToken(double? value)
		{
			return new JValue(value);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000790A File Offset: 0x00005B0A
		[CLSCompliant(false)]
		public static implicit operator JToken(short value)
		{
			return new JValue((long)value);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00007913 File Offset: 0x00005B13
		[CLSCompliant(false)]
		public static implicit operator JToken(ushort value)
		{
			return new JValue((long)((ulong)value));
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000791C File Offset: 0x00005B1C
		public static implicit operator JToken(int value)
		{
			return new JValue((long)value);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00007925 File Offset: 0x00005B25
		public static implicit operator JToken(int? value)
		{
			return new JValue(value);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00007932 File Offset: 0x00005B32
		public static implicit operator JToken(DateTime value)
		{
			return new JValue(value);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000793A File Offset: 0x00005B3A
		public static implicit operator JToken(long? value)
		{
			return new JValue(value);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00007947 File Offset: 0x00005B47
		public static implicit operator JToken(float? value)
		{
			return new JValue(value);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00007954 File Offset: 0x00005B54
		public static implicit operator JToken(decimal value)
		{
			return new JValue(value);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00007961 File Offset: 0x00005B61
		[CLSCompliant(false)]
		public static implicit operator JToken(short? value)
		{
			return new JValue(value);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000796E File Offset: 0x00005B6E
		[CLSCompliant(false)]
		public static implicit operator JToken(ushort? value)
		{
			return new JValue(value);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000797B File Offset: 0x00005B7B
		[CLSCompliant(false)]
		public static implicit operator JToken(uint? value)
		{
			return new JValue(value);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00007988 File Offset: 0x00005B88
		[CLSCompliant(false)]
		public static implicit operator JToken(ulong? value)
		{
			return new JValue(value);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00007995 File Offset: 0x00005B95
		public static implicit operator JToken(double value)
		{
			return new JValue(value);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000799D File Offset: 0x00005B9D
		public static implicit operator JToken(float value)
		{
			return new JValue((double)value);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000079A6 File Offset: 0x00005BA6
		public static implicit operator JToken(string value)
		{
			return new JValue(value);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000079AE File Offset: 0x00005BAE
		[CLSCompliant(false)]
		public static implicit operator JToken(uint value)
		{
			return new JValue((long)((ulong)value));
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000079B7 File Offset: 0x00005BB7
		[CLSCompliant(false)]
		public static implicit operator JToken(ulong value)
		{
			return new JValue(value);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000079BF File Offset: 0x00005BBF
		public static implicit operator JToken(byte[] value)
		{
			return new JValue(value);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000079C7 File Offset: 0x00005BC7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<JToken>)this).GetEnumerator();
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000079D0 File Offset: 0x00005BD0
		IEnumerator<JToken> IEnumerable<JToken>.GetEnumerator()
		{
			return this.Children().GetEnumerator();
		}

		// Token: 0x060001A8 RID: 424
		internal abstract int GetDeepHashCode();

		// Token: 0x17000038 RID: 56
		IJEnumerable<JToken> IJEnumerable<JToken>.this[object key]
		{
			get
			{
				return this[key];
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000079F4 File Offset: 0x00005BF4
		public JsonReader CreateReader()
		{
			return new JTokenReader(this);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000079FC File Offset: 0x00005BFC
		internal static JToken FromObjectInternal(object o, JsonSerializer jsonSerializer)
		{
			ValidationUtils.ArgumentNotNull(o, "o");
			ValidationUtils.ArgumentNotNull(jsonSerializer, "jsonSerializer");
			JToken token;
			using (JTokenWriter jtokenWriter = new JTokenWriter())
			{
				jsonSerializer.Serialize(jtokenWriter, o);
				token = jtokenWriter.Token;
			}
			return token;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00007A54 File Offset: 0x00005C54
		public static JToken FromObject(object o)
		{
			return JToken.FromObjectInternal(o, new JsonSerializer());
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00007A61 File Offset: 0x00005C61
		public static JToken FromObject(object o, JsonSerializer jsonSerializer)
		{
			return JToken.FromObjectInternal(o, jsonSerializer);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00007A6A File Offset: 0x00005C6A
		public T ToObject<T>()
		{
			return this.ToObject<T>(new JsonSerializer());
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00007A78 File Offset: 0x00005C78
		public T ToObject<T>(JsonSerializer jsonSerializer)
		{
			ValidationUtils.ArgumentNotNull(jsonSerializer, "jsonSerializer");
			T t;
			using (JTokenReader jtokenReader = new JTokenReader(this))
			{
				t = jsonSerializer.Deserialize<T>(jtokenReader);
			}
			return t;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00007ABC File Offset: 0x00005CBC
		public static JToken ReadFrom(JsonReader reader)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw new Exception("Error reading JToken from JsonReader.");
			}
			if (reader.TokenType == JsonToken.StartObject)
			{
				return JObject.Load(reader);
			}
			if (reader.TokenType == JsonToken.StartArray)
			{
				return JArray.Load(reader);
			}
			if (reader.TokenType == JsonToken.PropertyName)
			{
				return JProperty.Load(reader);
			}
			if (reader.TokenType == JsonToken.StartConstructor)
			{
				return JConstructor.Load(reader);
			}
			if (!JsonReader.IsStartToken(reader.TokenType))
			{
				return new JValue(reader.Value);
			}
			throw new Exception("Error reading JToken from JsonReader. Unexpected token: {0}".FormatWith(CultureInfo.InvariantCulture, new object[] { reader.TokenType }));
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00007B74 File Offset: 0x00005D74
		public static JToken Parse(string json)
		{
			JsonReader jsonReader = new JsonTextReader(new StringReader(json));
			JToken jtoken = JToken.Load(jsonReader);
			if (jsonReader.Read() && jsonReader.TokenType != JsonToken.Comment)
			{
				throw new Exception("Additional text found in JSON string after parsing content.");
			}
			return jtoken;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00007BB1 File Offset: 0x00005DB1
		public static JToken Load(JsonReader reader)
		{
			return JToken.ReadFrom(reader);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00007BB9 File Offset: 0x00005DB9
		internal void SetLineInfo(IJsonLineInfo lineInfo)
		{
			if (lineInfo == null || !lineInfo.HasLineInfo())
			{
				return;
			}
			this.SetLineInfo(lineInfo.LineNumber, lineInfo.LinePosition);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00007BD9 File Offset: 0x00005DD9
		internal void SetLineInfo(int lineNumber, int linePosition)
		{
			this._lineNumber = new int?(lineNumber);
			this._linePosition = new int?(linePosition);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00007BF3 File Offset: 0x00005DF3
		bool IJsonLineInfo.HasLineInfo()
		{
			return this._lineNumber != null && this._linePosition != null;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00007C10 File Offset: 0x00005E10
		int IJsonLineInfo.LineNumber
		{
			get
			{
				int? lineNumber = this._lineNumber;
				if (lineNumber == null)
				{
					return 0;
				}
				return lineNumber.GetValueOrDefault();
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00007C38 File Offset: 0x00005E38
		int IJsonLineInfo.LinePosition
		{
			get
			{
				int? linePosition = this._linePosition;
				if (linePosition == null)
				{
					return 0;
				}
				return linePosition.GetValueOrDefault();
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00007C5E File Offset: 0x00005E5E
		public JToken SelectToken(string path)
		{
			return this.SelectToken(path, false);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00007C68 File Offset: 0x00005E68
		public JToken SelectToken(string path, bool errorWhenNoMatch)
		{
			JPath jpath = new JPath(path);
			return jpath.Evaluate(this, errorWhenNoMatch);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00007C84 File Offset: 0x00005E84
		object ICloneable.Clone()
		{
			return this.DeepClone();
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00007C8C File Offset: 0x00005E8C
		public JToken DeepClone()
		{
			return this.CloneToken();
		}

		// Token: 0x04000085 RID: 133
		private JContainer _parent;

		// Token: 0x04000086 RID: 134
		private JToken _previous;

		// Token: 0x04000087 RID: 135
		private JToken _next;

		// Token: 0x04000088 RID: 136
		private static JTokenEqualityComparer _equalityComparer;

		// Token: 0x04000089 RID: 137
		private int? _lineNumber;

		// Token: 0x0400008A RID: 138
		private int? _linePosition;
	}
}
