using System;

namespace Boo.Lang.Runtime.DynamicDispatching
{
	// Token: 0x02000035 RID: 53
	public class NumericPromotions
	{
		// Token: 0x06000148 RID: 328 RVA: 0x000052F0 File Offset: 0x000034F0
		public static object FromSByteToByte(object value, object[] args)
		{
			return (byte)((sbyte)value);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00005300 File Offset: 0x00003500
		public static object FromSByteToInt16(object value, object[] args)
		{
			return (short)((sbyte)value);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00005310 File Offset: 0x00003510
		public static object FromSByteToUInt16(object value, object[] args)
		{
			return (ushort)((sbyte)value);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00005320 File Offset: 0x00003520
		public static object FromSByteToInt32(object value, object[] args)
		{
			return (int)((sbyte)value);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00005330 File Offset: 0x00003530
		public static object FromSByteToUInt32(object value, object[] args)
		{
			return (uint)((sbyte)value);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00005340 File Offset: 0x00003540
		public static object FromSByteToInt64(object value, object[] args)
		{
			return (long)((sbyte)value);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00005350 File Offset: 0x00003550
		public static object FromSByteToUInt64(object value, object[] args)
		{
			return (ulong)((long)((sbyte)value));
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00005360 File Offset: 0x00003560
		public static object FromSByteToSingle(object value, object[] args)
		{
			return (float)((sbyte)value);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00005370 File Offset: 0x00003570
		public static object FromSByteToDouble(object value, object[] args)
		{
			return (double)((sbyte)value);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005380 File Offset: 0x00003580
		public static object FromSByteToChar(object value, object[] args)
		{
			return (char)((sbyte)value);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005390 File Offset: 0x00003590
		public static object FromByteToSByte(object value, object[] args)
		{
			return (sbyte)((byte)value);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000053A0 File Offset: 0x000035A0
		public static object FromByteToInt16(object value, object[] args)
		{
			return (short)((byte)value);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000053B0 File Offset: 0x000035B0
		public static object FromByteToUInt16(object value, object[] args)
		{
			return (ushort)((byte)value);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000053C0 File Offset: 0x000035C0
		public static object FromByteToInt32(object value, object[] args)
		{
			return (int)((byte)value);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000053D0 File Offset: 0x000035D0
		public static object FromByteToUInt32(object value, object[] args)
		{
			return (uint)((byte)value);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000053E0 File Offset: 0x000035E0
		public static object FromByteToInt64(object value, object[] args)
		{
			return (long)((byte)value);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000053F0 File Offset: 0x000035F0
		public static object FromByteToUInt64(object value, object[] args)
		{
			return (ulong)((byte)value);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005400 File Offset: 0x00003600
		public static object FromByteToSingle(object value, object[] args)
		{
			return (float)((byte)value);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00005410 File Offset: 0x00003610
		public static object FromByteToDouble(object value, object[] args)
		{
			return (double)((byte)value);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005420 File Offset: 0x00003620
		public static object FromByteToChar(object value, object[] args)
		{
			return (char)((byte)value);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00005430 File Offset: 0x00003630
		public static object FromInt16ToSByte(object value, object[] args)
		{
			return (sbyte)((short)value);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00005440 File Offset: 0x00003640
		public static object FromInt16ToByte(object value, object[] args)
		{
			return (byte)((short)value);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00005450 File Offset: 0x00003650
		public static object FromInt16ToUInt16(object value, object[] args)
		{
			return (ushort)((short)value);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00005460 File Offset: 0x00003660
		public static object FromInt16ToInt32(object value, object[] args)
		{
			return (int)((short)value);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00005470 File Offset: 0x00003670
		public static object FromInt16ToUInt32(object value, object[] args)
		{
			return (uint)((short)value);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00005480 File Offset: 0x00003680
		public static object FromInt16ToInt64(object value, object[] args)
		{
			return (long)((short)value);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00005490 File Offset: 0x00003690
		public static object FromInt16ToUInt64(object value, object[] args)
		{
			return (ulong)((long)((short)value));
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000054A0 File Offset: 0x000036A0
		public static object FromInt16ToSingle(object value, object[] args)
		{
			return (float)((short)value);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000054B0 File Offset: 0x000036B0
		public static object FromInt16ToDouble(object value, object[] args)
		{
			return (double)((short)value);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000054C0 File Offset: 0x000036C0
		public static object FromInt16ToChar(object value, object[] args)
		{
			return (char)((short)value);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000054D0 File Offset: 0x000036D0
		public static object FromUInt16ToSByte(object value, object[] args)
		{
			return (sbyte)((ushort)value);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000054E0 File Offset: 0x000036E0
		public static object FromUInt16ToByte(object value, object[] args)
		{
			return (byte)((ushort)value);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000054F0 File Offset: 0x000036F0
		public static object FromUInt16ToInt16(object value, object[] args)
		{
			return (short)((ushort)value);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00005500 File Offset: 0x00003700
		public static object FromUInt16ToInt32(object value, object[] args)
		{
			return (int)((ushort)value);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00005510 File Offset: 0x00003710
		public static object FromUInt16ToUInt32(object value, object[] args)
		{
			return (uint)((ushort)value);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00005520 File Offset: 0x00003720
		public static object FromUInt16ToInt64(object value, object[] args)
		{
			return (long)((ushort)value);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00005530 File Offset: 0x00003730
		public static object FromUInt16ToUInt64(object value, object[] args)
		{
			return (ulong)((ushort)value);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00005540 File Offset: 0x00003740
		public static object FromUInt16ToSingle(object value, object[] args)
		{
			return (float)((ushort)value);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00005550 File Offset: 0x00003750
		public static object FromUInt16ToDouble(object value, object[] args)
		{
			return (double)((ushort)value);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00005560 File Offset: 0x00003760
		public static object FromUInt16ToChar(object value, object[] args)
		{
			return (char)((ushort)value);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00005570 File Offset: 0x00003770
		public static object FromInt32ToSByte(object value, object[] args)
		{
			return (sbyte)((int)value);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00005580 File Offset: 0x00003780
		public static object FromInt32ToByte(object value, object[] args)
		{
			return (byte)((int)value);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00005590 File Offset: 0x00003790
		public static object FromInt32ToInt16(object value, object[] args)
		{
			return (short)((int)value);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000055A0 File Offset: 0x000037A0
		public static object FromInt32ToUInt16(object value, object[] args)
		{
			return (ushort)((int)value);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000055B0 File Offset: 0x000037B0
		public static object FromInt32ToUInt32(object value, object[] args)
		{
			return (uint)((int)value);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000055C0 File Offset: 0x000037C0
		public static object FromInt32ToInt64(object value, object[] args)
		{
			return (long)((int)value);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000055D0 File Offset: 0x000037D0
		public static object FromInt32ToUInt64(object value, object[] args)
		{
			return (ulong)((long)((int)value));
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000055E0 File Offset: 0x000037E0
		public static object FromInt32ToSingle(object value, object[] args)
		{
			return (float)((int)value);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000055F0 File Offset: 0x000037F0
		public static object FromInt32ToDouble(object value, object[] args)
		{
			return (double)((int)value);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00005600 File Offset: 0x00003800
		public static object FromInt32ToChar(object value, object[] args)
		{
			return (char)((int)value);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00005610 File Offset: 0x00003810
		public static object FromUInt32ToSByte(object value, object[] args)
		{
			return (sbyte)((uint)value);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00005620 File Offset: 0x00003820
		public static object FromUInt32ToByte(object value, object[] args)
		{
			return (byte)((uint)value);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00005630 File Offset: 0x00003830
		public static object FromUInt32ToInt16(object value, object[] args)
		{
			return (short)((uint)value);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00005640 File Offset: 0x00003840
		public static object FromUInt32ToUInt16(object value, object[] args)
		{
			return (ushort)((uint)value);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00005650 File Offset: 0x00003850
		public static object FromUInt32ToInt32(object value, object[] args)
		{
			return (int)((uint)value);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00005660 File Offset: 0x00003860
		public static object FromUInt32ToInt64(object value, object[] args)
		{
			return (long)((ulong)((uint)value));
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00005670 File Offset: 0x00003870
		public static object FromUInt32ToUInt64(object value, object[] args)
		{
			return (ulong)((uint)value);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00005680 File Offset: 0x00003880
		public static object FromUInt32ToSingle(object value, object[] args)
		{
			return (uint)value;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00005690 File Offset: 0x00003890
		public static object FromUInt32ToDouble(object value, object[] args)
		{
			return (uint)value;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000056A0 File Offset: 0x000038A0
		public static object FromUInt32ToChar(object value, object[] args)
		{
			return (char)((uint)value);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000056B0 File Offset: 0x000038B0
		public static object FromInt64ToSByte(object value, object[] args)
		{
			return (sbyte)((long)value);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000056C0 File Offset: 0x000038C0
		public static object FromInt64ToByte(object value, object[] args)
		{
			return (byte)((long)value);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000056D0 File Offset: 0x000038D0
		public static object FromInt64ToInt16(object value, object[] args)
		{
			return (short)((long)value);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000056E0 File Offset: 0x000038E0
		public static object FromInt64ToUInt16(object value, object[] args)
		{
			return (ushort)((long)value);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000056F0 File Offset: 0x000038F0
		public static object FromInt64ToInt32(object value, object[] args)
		{
			return (int)((long)value);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00005700 File Offset: 0x00003900
		public static object FromInt64ToUInt32(object value, object[] args)
		{
			return (uint)((long)value);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00005710 File Offset: 0x00003910
		public static object FromInt64ToUInt64(object value, object[] args)
		{
			return (ulong)((long)value);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00005720 File Offset: 0x00003920
		public static object FromInt64ToSingle(object value, object[] args)
		{
			return (float)((long)value);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00005730 File Offset: 0x00003930
		public static object FromInt64ToDouble(object value, object[] args)
		{
			return (double)((long)value);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00005740 File Offset: 0x00003940
		public static object FromInt64ToChar(object value, object[] args)
		{
			return (char)((long)value);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00005750 File Offset: 0x00003950
		public static object FromUInt64ToSByte(object value, object[] args)
		{
			return (sbyte)((ulong)value);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00005760 File Offset: 0x00003960
		public static object FromUInt64ToByte(object value, object[] args)
		{
			return (byte)((ulong)value);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00005770 File Offset: 0x00003970
		public static object FromUInt64ToInt16(object value, object[] args)
		{
			return (short)((ulong)value);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00005780 File Offset: 0x00003980
		public static object FromUInt64ToUInt16(object value, object[] args)
		{
			return (ushort)((ulong)value);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00005790 File Offset: 0x00003990
		public static object FromUInt64ToInt32(object value, object[] args)
		{
			return (int)((ulong)value);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000057A0 File Offset: 0x000039A0
		public static object FromUInt64ToUInt32(object value, object[] args)
		{
			return (uint)((ulong)value);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000057B0 File Offset: 0x000039B0
		public static object FromUInt64ToInt64(object value, object[] args)
		{
			return (long)((ulong)value);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000057C0 File Offset: 0x000039C0
		public static object FromUInt64ToSingle(object value, object[] args)
		{
			return (ulong)value;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000057D0 File Offset: 0x000039D0
		public static object FromUInt64ToDouble(object value, object[] args)
		{
			return (ulong)value;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000057E0 File Offset: 0x000039E0
		public static object FromUInt64ToChar(object value, object[] args)
		{
			return (char)((ulong)value);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000057F0 File Offset: 0x000039F0
		public static object FromSingleToSByte(object value, object[] args)
		{
			return (sbyte)((float)value);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00005800 File Offset: 0x00003A00
		public static object FromSingleToByte(object value, object[] args)
		{
			return (byte)((float)value);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00005810 File Offset: 0x00003A10
		public static object FromSingleToInt16(object value, object[] args)
		{
			return (short)((float)value);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00005820 File Offset: 0x00003A20
		public static object FromSingleToUInt16(object value, object[] args)
		{
			return (ushort)((float)value);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00005830 File Offset: 0x00003A30
		public static object FromSingleToInt32(object value, object[] args)
		{
			return (int)((float)value);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00005840 File Offset: 0x00003A40
		public static object FromSingleToUInt32(object value, object[] args)
		{
			return (uint)((float)value);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00005850 File Offset: 0x00003A50
		public static object FromSingleToInt64(object value, object[] args)
		{
			return (long)((float)value);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00005860 File Offset: 0x00003A60
		public static object FromSingleToUInt64(object value, object[] args)
		{
			return (ulong)((float)value);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00005870 File Offset: 0x00003A70
		public static object FromSingleToDouble(object value, object[] args)
		{
			return (double)((float)value);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00005880 File Offset: 0x00003A80
		public static object FromSingleToChar(object value, object[] args)
		{
			return (char)((float)value);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00005890 File Offset: 0x00003A90
		public static object FromDoubleToSByte(object value, object[] args)
		{
			return (sbyte)((double)value);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000058A0 File Offset: 0x00003AA0
		public static object FromDoubleToByte(object value, object[] args)
		{
			return (byte)((double)value);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000058B0 File Offset: 0x00003AB0
		public static object FromDoubleToInt16(object value, object[] args)
		{
			return (short)((double)value);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000058C0 File Offset: 0x00003AC0
		public static object FromDoubleToUInt16(object value, object[] args)
		{
			return (ushort)((double)value);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000058D0 File Offset: 0x00003AD0
		public static object FromDoubleToInt32(object value, object[] args)
		{
			return (int)((double)value);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000058E0 File Offset: 0x00003AE0
		public static object FromDoubleToUInt32(object value, object[] args)
		{
			return (uint)((double)value);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x000058F0 File Offset: 0x00003AF0
		public static object FromDoubleToInt64(object value, object[] args)
		{
			return (long)((double)value);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00005900 File Offset: 0x00003B00
		public static object FromDoubleToUInt64(object value, object[] args)
		{
			return (ulong)((double)value);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00005910 File Offset: 0x00003B10
		public static object FromDoubleToSingle(object value, object[] args)
		{
			return (float)((double)value);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00005920 File Offset: 0x00003B20
		public static object FromDoubleToChar(object value, object[] args)
		{
			return (char)((double)value);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00005930 File Offset: 0x00003B30
		public static object FromCharToSByte(object value, object[] args)
		{
			return (sbyte)((char)value);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00005940 File Offset: 0x00003B40
		public static object FromCharToByte(object value, object[] args)
		{
			return (byte)((char)value);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00005950 File Offset: 0x00003B50
		public static object FromCharToInt16(object value, object[] args)
		{
			return (short)((char)value);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00005960 File Offset: 0x00003B60
		public static object FromCharToUInt16(object value, object[] args)
		{
			return (ushort)((char)value);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00005970 File Offset: 0x00003B70
		public static object FromCharToInt32(object value, object[] args)
		{
			return (int)((char)value);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00005980 File Offset: 0x00003B80
		public static object FromCharToUInt32(object value, object[] args)
		{
			return (uint)((char)value);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00005990 File Offset: 0x00003B90
		public static object FromCharToInt64(object value, object[] args)
		{
			return (long)((char)value);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000059A0 File Offset: 0x00003BA0
		public static object FromCharToUInt64(object value, object[] args)
		{
			return (ulong)((char)value);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000059B0 File Offset: 0x00003BB0
		public static object FromCharToSingle(object value, object[] args)
		{
			return (float)((char)value);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000059C0 File Offset: 0x00003BC0
		public static object FromCharToDouble(object value, object[] args)
		{
			return (double)((char)value);
		}
	}
}
