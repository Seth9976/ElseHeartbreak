using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200021E RID: 542
	public sealed class RectTransform : Transform
	{
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06001A4B RID: 6731 RVA: 0x000258A8 File Offset: 0x00023AA8
		// (remove) Token: 0x06001A4C RID: 6732 RVA: 0x000258C0 File Offset: 0x00023AC0
		public static event RectTransform.ReapplyDrivenProperties reapplyDrivenProperties;

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001A4D RID: 6733 RVA: 0x000258D8 File Offset: 0x00023AD8
		public Rect rect
		{
			get
			{
				Rect rect;
				this.INTERNAL_get_rect(out rect);
				return rect;
			}
		}

		// Token: 0x06001A4E RID: 6734
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_rect(out Rect value);

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06001A4F RID: 6735 RVA: 0x000258F0 File Offset: 0x00023AF0
		// (set) Token: 0x06001A50 RID: 6736 RVA: 0x00025908 File Offset: 0x00023B08
		public Vector2 anchorMin
		{
			get
			{
				Vector2 vector;
				this.INTERNAL_get_anchorMin(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_anchorMin(ref value);
			}
		}

		// Token: 0x06001A51 RID: 6737
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_anchorMin(out Vector2 value);

		// Token: 0x06001A52 RID: 6738
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_anchorMin(ref Vector2 value);

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06001A53 RID: 6739 RVA: 0x00025914 File Offset: 0x00023B14
		// (set) Token: 0x06001A54 RID: 6740 RVA: 0x0002592C File Offset: 0x00023B2C
		public Vector2 anchorMax
		{
			get
			{
				Vector2 vector;
				this.INTERNAL_get_anchorMax(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_anchorMax(ref value);
			}
		}

		// Token: 0x06001A55 RID: 6741
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_anchorMax(out Vector2 value);

		// Token: 0x06001A56 RID: 6742
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_anchorMax(ref Vector2 value);

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06001A57 RID: 6743 RVA: 0x00025938 File Offset: 0x00023B38
		// (set) Token: 0x06001A58 RID: 6744 RVA: 0x00025970 File Offset: 0x00023B70
		public Vector3 anchoredPosition3D
		{
			get
			{
				Vector2 anchoredPosition = this.anchoredPosition;
				return new Vector3(anchoredPosition.x, anchoredPosition.y, base.localPosition.z);
			}
			set
			{
				this.anchoredPosition = new Vector2(value.x, value.y);
				Vector3 localPosition = base.localPosition;
				localPosition.z = value.z;
				base.localPosition = localPosition;
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06001A59 RID: 6745 RVA: 0x000259B4 File Offset: 0x00023BB4
		// (set) Token: 0x06001A5A RID: 6746 RVA: 0x000259CC File Offset: 0x00023BCC
		public Vector2 anchoredPosition
		{
			get
			{
				Vector2 vector;
				this.INTERNAL_get_anchoredPosition(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_anchoredPosition(ref value);
			}
		}

		// Token: 0x06001A5B RID: 6747
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_anchoredPosition(out Vector2 value);

		// Token: 0x06001A5C RID: 6748
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_anchoredPosition(ref Vector2 value);

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06001A5D RID: 6749 RVA: 0x000259D8 File Offset: 0x00023BD8
		// (set) Token: 0x06001A5E RID: 6750 RVA: 0x000259F0 File Offset: 0x00023BF0
		public Vector2 sizeDelta
		{
			get
			{
				Vector2 vector;
				this.INTERNAL_get_sizeDelta(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_sizeDelta(ref value);
			}
		}

		// Token: 0x06001A5F RID: 6751
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_sizeDelta(out Vector2 value);

		// Token: 0x06001A60 RID: 6752
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_sizeDelta(ref Vector2 value);

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06001A61 RID: 6753 RVA: 0x000259FC File Offset: 0x00023BFC
		// (set) Token: 0x06001A62 RID: 6754 RVA: 0x00025A14 File Offset: 0x00023C14
		public Vector2 pivot
		{
			get
			{
				Vector2 vector;
				this.INTERNAL_get_pivot(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_pivot(ref value);
			}
		}

		// Token: 0x06001A63 RID: 6755
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_pivot(out Vector2 value);

		// Token: 0x06001A64 RID: 6756
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_pivot(ref Vector2 value);

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06001A65 RID: 6757
		// (set) Token: 0x06001A66 RID: 6758
		internal extern Object drivenByObject
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06001A67 RID: 6759
		// (set) Token: 0x06001A68 RID: 6760
		internal extern DrivenTransformProperties drivenProperties
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x00025A20 File Offset: 0x00023C20
		internal static void SendReapplyDrivenProperties(RectTransform driven)
		{
			if (RectTransform.reapplyDrivenProperties != null)
			{
				RectTransform.reapplyDrivenProperties(driven);
			}
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x00025A38 File Offset: 0x00023C38
		public void GetLocalCorners(Vector3[] fourCornersArray)
		{
			if (fourCornersArray == null || fourCornersArray.Length < 4)
			{
				Debug.LogError("Calling GetLocalCorners with an array that is null or has less than 4 elements.");
				return;
			}
			Rect rect = this.rect;
			float x = rect.x;
			float y = rect.y;
			float xMax = rect.xMax;
			float yMax = rect.yMax;
			fourCornersArray[0] = new Vector3(x, y, 0f);
			fourCornersArray[1] = new Vector3(x, yMax, 0f);
			fourCornersArray[2] = new Vector3(xMax, yMax, 0f);
			fourCornersArray[3] = new Vector3(xMax, y, 0f);
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x00025AEC File Offset: 0x00023CEC
		public void GetWorldCorners(Vector3[] fourCornersArray)
		{
			if (fourCornersArray == null || fourCornersArray.Length < 4)
			{
				Debug.LogError("Calling GetWorldCorners with an array that is null or has less than 4 elements.");
				return;
			}
			this.GetLocalCorners(fourCornersArray);
			Transform transform = base.transform;
			for (int i = 0; i < 4; i++)
			{
				fourCornersArray[i] = transform.TransformPoint(fourCornersArray[i]);
			}
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x00025B54 File Offset: 0x00023D54
		internal Rect GetRectInParentSpace()
		{
			Rect rect = this.rect;
			Vector2 vector = this.offsetMin + Vector2.Scale(this.pivot, rect.size);
			Transform parent = base.transform.parent;
			if (parent)
			{
				RectTransform component = parent.GetComponent<RectTransform>();
				if (component)
				{
					vector += Vector2.Scale(this.anchorMin, component.rect.size);
				}
			}
			rect.x += vector.x;
			rect.y += vector.y;
			return rect;
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06001A6D RID: 6765 RVA: 0x00025BFC File Offset: 0x00023DFC
		// (set) Token: 0x06001A6E RID: 6766 RVA: 0x00025C28 File Offset: 0x00023E28
		public Vector2 offsetMin
		{
			get
			{
				return this.anchoredPosition - Vector2.Scale(this.sizeDelta, this.pivot);
			}
			set
			{
				Vector2 vector = value - (this.anchoredPosition - Vector2.Scale(this.sizeDelta, this.pivot));
				this.sizeDelta -= vector;
				this.anchoredPosition += Vector2.Scale(vector, Vector2.one - this.pivot);
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06001A6F RID: 6767 RVA: 0x00025C94 File Offset: 0x00023E94
		// (set) Token: 0x06001A70 RID: 6768 RVA: 0x00025CC8 File Offset: 0x00023EC8
		public Vector2 offsetMax
		{
			get
			{
				return this.anchoredPosition + Vector2.Scale(this.sizeDelta, Vector2.one - this.pivot);
			}
			set
			{
				Vector2 vector = value - (this.anchoredPosition + Vector2.Scale(this.sizeDelta, Vector2.one - this.pivot));
				this.sizeDelta += vector;
				this.anchoredPosition += Vector2.Scale(vector, this.pivot);
			}
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x00025D34 File Offset: 0x00023F34
		public void SetInsetAndSizeFromParentEdge(RectTransform.Edge edge, float inset, float size)
		{
			int num = ((edge != RectTransform.Edge.Top && edge != RectTransform.Edge.Bottom) ? 0 : 1);
			bool flag = edge == RectTransform.Edge.Top || edge == RectTransform.Edge.Right;
			float num2 = (float)((!flag) ? 0 : 1);
			Vector2 vector = this.anchorMin;
			vector[num] = num2;
			this.anchorMin = vector;
			vector = this.anchorMax;
			vector[num] = num2;
			this.anchorMax = vector;
			Vector2 sizeDelta = this.sizeDelta;
			sizeDelta[num] = size;
			this.sizeDelta = sizeDelta;
			Vector2 anchoredPosition = this.anchoredPosition;
			anchoredPosition[num] = ((!flag) ? (inset + size * this.pivot[num]) : (-inset - size * (1f - this.pivot[num])));
			this.anchoredPosition = anchoredPosition;
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x00025E10 File Offset: 0x00024010
		public void SetSizeWithCurrentAnchors(RectTransform.Axis axis, float size)
		{
			Vector2 sizeDelta = this.sizeDelta;
			sizeDelta[(int)axis] = size - this.GetParentSize()[(int)axis] * (this.anchorMax[(int)axis] - this.anchorMin[(int)axis]);
			this.sizeDelta = sizeDelta;
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x00025E68 File Offset: 0x00024068
		private Vector2 GetParentSize()
		{
			RectTransform rectTransform = base.parent as RectTransform;
			if (!rectTransform)
			{
				return Vector2.zero;
			}
			return rectTransform.rect.size;
		}

		// Token: 0x0200021F RID: 543
		public enum Edge
		{
			// Token: 0x04000863 RID: 2147
			Left,
			// Token: 0x04000864 RID: 2148
			Right,
			// Token: 0x04000865 RID: 2149
			Top,
			// Token: 0x04000866 RID: 2150
			Bottom
		}

		// Token: 0x02000220 RID: 544
		public enum Axis
		{
			// Token: 0x04000868 RID: 2152
			Horizontal,
			// Token: 0x04000869 RID: 2153
			Vertical
		}

		// Token: 0x0200022F RID: 559
		// (Invoke) Token: 0x06001AE1 RID: 6881
		public delegate void ReapplyDrivenProperties(RectTransform driven);
	}
}
