using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000221 RID: 545
	public sealed class RectTransformUtility
	{
		// Token: 0x06001A74 RID: 6772 RVA: 0x00025EA0 File Offset: 0x000240A0
		private RectTransformUtility()
		{
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x00025EB8 File Offset: 0x000240B8
		public static bool RectangleContainsScreenPoint(RectTransform rect, Vector2 screenPoint, Camera cam)
		{
			return RectTransformUtility.INTERNAL_CALL_RectangleContainsScreenPoint(rect, ref screenPoint, cam);
		}

		// Token: 0x06001A77 RID: 6775
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_RectangleContainsScreenPoint(RectTransform rect, ref Vector2 screenPoint, Camera cam);

		// Token: 0x06001A78 RID: 6776 RVA: 0x00025EC4 File Offset: 0x000240C4
		public static Vector2 PixelAdjustPoint(Vector2 point, Transform elementTransform, Canvas canvas)
		{
			Vector2 vector;
			RectTransformUtility.PixelAdjustPoint(point, elementTransform, canvas, out vector);
			return vector;
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x00025EDC File Offset: 0x000240DC
		private static void PixelAdjustPoint(Vector2 point, Transform elementTransform, Canvas canvas, out Vector2 output)
		{
			RectTransformUtility.INTERNAL_CALL_PixelAdjustPoint(ref point, elementTransform, canvas, out output);
		}

		// Token: 0x06001A7A RID: 6778
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_PixelAdjustPoint(ref Vector2 point, Transform elementTransform, Canvas canvas, out Vector2 output);

		// Token: 0x06001A7B RID: 6779
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Rect PixelAdjustRect(RectTransform rectTransform, Canvas canvas);

		// Token: 0x06001A7C RID: 6780 RVA: 0x00025EE8 File Offset: 0x000240E8
		public static bool ScreenPointToWorldPointInRectangle(RectTransform rect, Vector2 screenPoint, Camera cam, out Vector3 worldPoint)
		{
			worldPoint = Vector2.zero;
			Ray ray = RectTransformUtility.ScreenPointToRay(cam, screenPoint);
			Plane plane = new Plane(rect.rotation * Vector3.back, rect.position);
			float num;
			if (!plane.Raycast(ray, out num))
			{
				return false;
			}
			worldPoint = ray.GetPoint(num);
			return true;
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x00025F4C File Offset: 0x0002414C
		public static bool ScreenPointToLocalPointInRectangle(RectTransform rect, Vector2 screenPoint, Camera cam, out Vector2 localPoint)
		{
			localPoint = Vector2.zero;
			Vector3 vector;
			if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, screenPoint, cam, out vector))
			{
				localPoint = rect.InverseTransformPoint(vector);
				return true;
			}
			return false;
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x00025F88 File Offset: 0x00024188
		public static Ray ScreenPointToRay(Camera cam, Vector2 screenPos)
		{
			if (cam != null)
			{
				return cam.ScreenPointToRay(screenPos);
			}
			Vector3 vector = screenPos;
			vector.z -= 100f;
			return new Ray(vector, Vector3.forward);
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x00025FD4 File Offset: 0x000241D4
		public static Vector2 WorldToScreenPoint(Camera cam, Vector3 worldPoint)
		{
			if (cam == null)
			{
				return new Vector2(worldPoint.x, worldPoint.y);
			}
			return cam.WorldToScreenPoint(worldPoint);
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x00026010 File Offset: 0x00024210
		public static Bounds CalculateRelativeRectTransformBounds(Transform root, Transform child)
		{
			RectTransform[] componentsInChildren = child.GetComponentsInChildren<RectTransform>(false);
			if (componentsInChildren.Length > 0)
			{
				Vector3 vector = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
				Vector3 vector2 = new Vector3(float.MinValue, float.MinValue, float.MinValue);
				Matrix4x4 worldToLocalMatrix = root.worldToLocalMatrix;
				int i = 0;
				int num = componentsInChildren.Length;
				while (i < num)
				{
					componentsInChildren[i].GetWorldCorners(RectTransformUtility.s_Corners);
					for (int j = 0; j < 4; j++)
					{
						Vector3 vector3 = worldToLocalMatrix.MultiplyPoint3x4(RectTransformUtility.s_Corners[j]);
						vector = Vector3.Min(vector3, vector);
						vector2 = Vector3.Max(vector3, vector2);
					}
					i++;
				}
				Bounds bounds = new Bounds(vector, Vector3.zero);
				bounds.Encapsulate(vector2);
				return bounds;
			}
			return new Bounds(Vector3.zero, Vector3.zero);
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x000260F4 File Offset: 0x000242F4
		public static Bounds CalculateRelativeRectTransformBounds(Transform trans)
		{
			return RectTransformUtility.CalculateRelativeRectTransformBounds(trans, trans);
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x00026100 File Offset: 0x00024300
		public static void FlipLayoutOnAxis(RectTransform rect, int axis, bool keepPositioning, bool recursive)
		{
			if (rect == null)
			{
				return;
			}
			if (recursive)
			{
				for (int i = 0; i < rect.childCount; i++)
				{
					RectTransform rectTransform = rect.GetChild(i) as RectTransform;
					if (rectTransform != null)
					{
						RectTransformUtility.FlipLayoutOnAxis(rectTransform, axis, false, true);
					}
				}
			}
			Vector2 pivot = rect.pivot;
			pivot[axis] = 1f - pivot[axis];
			rect.pivot = pivot;
			if (keepPositioning)
			{
				return;
			}
			Vector2 anchoredPosition = rect.anchoredPosition;
			anchoredPosition[axis] = -anchoredPosition[axis];
			rect.anchoredPosition = anchoredPosition;
			Vector2 anchorMin = rect.anchorMin;
			Vector2 anchorMax = rect.anchorMax;
			float num = anchorMin[axis];
			anchorMin[axis] = 1f - anchorMax[axis];
			anchorMax[axis] = 1f - num;
			rect.anchorMin = anchorMin;
			rect.anchorMax = anchorMax;
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x000261F4 File Offset: 0x000243F4
		public static void FlipLayoutAxes(RectTransform rect, bool keepPositioning, bool recursive)
		{
			if (rect == null)
			{
				return;
			}
			if (recursive)
			{
				for (int i = 0; i < rect.childCount; i++)
				{
					RectTransform rectTransform = rect.GetChild(i) as RectTransform;
					if (rectTransform != null)
					{
						RectTransformUtility.FlipLayoutAxes(rectTransform, false, true);
					}
				}
			}
			rect.pivot = RectTransformUtility.GetTransposed(rect.pivot);
			rect.sizeDelta = RectTransformUtility.GetTransposed(rect.sizeDelta);
			if (keepPositioning)
			{
				return;
			}
			rect.anchoredPosition = RectTransformUtility.GetTransposed(rect.anchoredPosition);
			rect.anchorMin = RectTransformUtility.GetTransposed(rect.anchorMin);
			rect.anchorMax = RectTransformUtility.GetTransposed(rect.anchorMax);
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x000262A8 File Offset: 0x000244A8
		private static Vector2 GetTransposed(Vector2 input)
		{
			return new Vector2(input.y, input.x);
		}

		// Token: 0x0400086A RID: 2154
		private static Vector3[] s_Corners = new Vector3[4];
	}
}
