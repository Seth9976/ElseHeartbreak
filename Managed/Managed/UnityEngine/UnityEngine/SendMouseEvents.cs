using System;

namespace UnityEngine
{
	// Token: 0x0200002A RID: 42
	internal class SendMouseEvents
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00003894 File Offset: 0x00001A94
		[NotRenamed]
		private static void DoSendMouseEvents(int mouseUsed, int skipRTCameras)
		{
			Vector3 mousePosition = Input.mousePosition;
			int allCamerasCount = Camera.allCamerasCount;
			if (SendMouseEvents.m_Cameras == null || SendMouseEvents.m_Cameras.Length != allCamerasCount)
			{
				SendMouseEvents.m_Cameras = new Camera[allCamerasCount];
			}
			int allCameras = Camera.GetAllCameras(SendMouseEvents.m_Cameras);
			for (int i = 0; i < SendMouseEvents.m_CurrentHit.Length; i++)
			{
				SendMouseEvents.m_CurrentHit[i] = default(SendMouseEvents.HitInfo);
			}
			if (mouseUsed == 0)
			{
				for (int j = 0; j < allCameras; j++)
				{
					Camera camera = SendMouseEvents.m_Cameras[j];
					if (!(camera == null) && (skipRTCameras == 0 || !(camera.targetTexture != null)))
					{
						if (camera.pixelRect.Contains(mousePosition))
						{
							GUILayer component = camera.GetComponent<GUILayer>();
							if (component)
							{
								GUIElement guielement = component.HitTest(mousePosition);
								if (guielement)
								{
									SendMouseEvents.m_CurrentHit[0].target = guielement.gameObject;
									SendMouseEvents.m_CurrentHit[0].camera = camera;
								}
								else
								{
									SendMouseEvents.m_CurrentHit[0].target = null;
									SendMouseEvents.m_CurrentHit[0].camera = null;
								}
							}
							if (camera.eventMask != 0)
							{
								Ray ray = camera.ScreenPointToRay(mousePosition);
								float z = ray.direction.z;
								float num = ((!Mathf.Approximately(0f, z)) ? Mathf.Abs((camera.farClipPlane - camera.nearClipPlane) / z) : float.PositiveInfinity);
								RaycastHit raycastHit;
								if (Physics.Raycast(ray, out raycastHit, num + 1f, camera.cullingMask & camera.eventMask & -5))
								{
									SendMouseEvents.m_CurrentHit[1].camera = camera;
									SendMouseEvents.m_CurrentHit[1].target = ((!raycastHit.rigidbody) ? raycastHit.collider.gameObject : raycastHit.rigidbody.gameObject);
								}
								else if (camera.clearFlags == CameraClearFlags.Skybox || camera.clearFlags == CameraClearFlags.Color)
								{
									SendMouseEvents.m_CurrentHit[1].target = null;
									SendMouseEvents.m_CurrentHit[1].camera = null;
								}
								if (Physics2D.GetRayIntersectionNonAlloc(ray, SendMouseEvents.m_MouseRayHits2D, num, camera.cullingMask & camera.eventMask & -5) == 1)
								{
									SendMouseEvents.m_CurrentHit[2].camera = camera;
									SendMouseEvents.m_CurrentHit[2].target = ((!SendMouseEvents.m_MouseRayHits2D[0].rigidbody) ? SendMouseEvents.m_MouseRayHits2D[0].collider.gameObject : SendMouseEvents.m_MouseRayHits2D[0].rigidbody.gameObject);
								}
								else if (camera.clearFlags == CameraClearFlags.Skybox || camera.clearFlags == CameraClearFlags.Color)
								{
									SendMouseEvents.m_CurrentHit[2].target = null;
									SendMouseEvents.m_CurrentHit[2].camera = null;
								}
							}
						}
					}
				}
			}
			for (int k = 0; k < SendMouseEvents.m_CurrentHit.Length; k++)
			{
				SendMouseEvents.SendEvents(k, SendMouseEvents.m_CurrentHit[k]);
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003C14 File Offset: 0x00001E14
		private static void SendEvents(int i, SendMouseEvents.HitInfo hit)
		{
			bool mouseButtonDown = Input.GetMouseButtonDown(0);
			bool mouseButton = Input.GetMouseButton(0);
			if (mouseButtonDown)
			{
				if (hit)
				{
					SendMouseEvents.m_MouseDownHit[i] = hit;
					SendMouseEvents.m_MouseDownHit[i].SendMessage("OnMouseDown");
				}
			}
			else if (!mouseButton)
			{
				if (SendMouseEvents.m_MouseDownHit[i])
				{
					if (SendMouseEvents.HitInfo.Compare(hit, SendMouseEvents.m_MouseDownHit[i]))
					{
						SendMouseEvents.m_MouseDownHit[i].SendMessage("OnMouseUpAsButton");
					}
					SendMouseEvents.m_MouseDownHit[i].SendMessage("OnMouseUp");
					SendMouseEvents.m_MouseDownHit[i] = default(SendMouseEvents.HitInfo);
				}
			}
			else if (SendMouseEvents.m_MouseDownHit[i])
			{
				SendMouseEvents.m_MouseDownHit[i].SendMessage("OnMouseDrag");
			}
			if (SendMouseEvents.HitInfo.Compare(hit, SendMouseEvents.m_LastHit[i]))
			{
				if (hit)
				{
					hit.SendMessage("OnMouseOver");
				}
			}
			else
			{
				if (SendMouseEvents.m_LastHit[i])
				{
					SendMouseEvents.m_LastHit[i].SendMessage("OnMouseExit");
				}
				if (hit)
				{
					hit.SendMessage("OnMouseEnter");
					hit.SendMessage("OnMouseOver");
				}
			}
			SendMouseEvents.m_LastHit[i] = hit;
		}

		// Token: 0x040000C1 RID: 193
		private const int m_HitIndexGUI = 0;

		// Token: 0x040000C2 RID: 194
		private const int m_HitIndexPhysics3D = 1;

		// Token: 0x040000C3 RID: 195
		private const int m_HitIndexPhysics2D = 2;

		// Token: 0x040000C4 RID: 196
		private static readonly SendMouseEvents.HitInfo[] m_LastHit = new SendMouseEvents.HitInfo[]
		{
			default(SendMouseEvents.HitInfo),
			default(SendMouseEvents.HitInfo),
			default(SendMouseEvents.HitInfo)
		};

		// Token: 0x040000C5 RID: 197
		private static readonly SendMouseEvents.HitInfo[] m_MouseDownHit = new SendMouseEvents.HitInfo[]
		{
			default(SendMouseEvents.HitInfo),
			default(SendMouseEvents.HitInfo),
			default(SendMouseEvents.HitInfo)
		};

		// Token: 0x040000C6 RID: 198
		private static readonly SendMouseEvents.HitInfo[] m_CurrentHit = new SendMouseEvents.HitInfo[]
		{
			default(SendMouseEvents.HitInfo),
			default(SendMouseEvents.HitInfo),
			default(SendMouseEvents.HitInfo)
		};

		// Token: 0x040000C7 RID: 199
		private static readonly RaycastHit2D[] m_MouseRayHits2D = new RaycastHit2D[] { default(RaycastHit2D) };

		// Token: 0x040000C8 RID: 200
		private static Camera[] m_Cameras;

		// Token: 0x0200002B RID: 43
		private struct HitInfo
		{
			// Token: 0x06000096 RID: 150 RVA: 0x00003DB8 File Offset: 0x00001FB8
			public void SendMessage(string name)
			{
				this.target.SendMessage(name, null, SendMessageOptions.DontRequireReceiver);
			}

			// Token: 0x06000097 RID: 151 RVA: 0x00003DC8 File Offset: 0x00001FC8
			public static bool Compare(SendMouseEvents.HitInfo lhs, SendMouseEvents.HitInfo rhs)
			{
				return lhs.target == rhs.target && lhs.camera == rhs.camera;
			}

			// Token: 0x06000098 RID: 152 RVA: 0x00003E04 File Offset: 0x00002004
			public static implicit operator bool(SendMouseEvents.HitInfo exists)
			{
				return exists.target != null && exists.camera != null;
			}

			// Token: 0x040000C9 RID: 201
			public GameObject target;

			// Token: 0x040000CA RID: 202
			public Camera camera;
		}
	}
}
