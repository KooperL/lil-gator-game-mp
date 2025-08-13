using System;
using UnityEngine;

namespace Cinemachine.Examples
{
	// Token: 0x020003E6 RID: 998
	public class ScriptingExample : MonoBehaviour
	{
		// Token: 0x0600134A RID: 4938 RVA: 0x0005E384 File Offset: 0x0005C584
		private void Start()
		{
			CinemachineBrain cinemachineBrain = GameObject.Find("Main Camera").AddComponent<CinemachineBrain>();
			cinemachineBrain.m_ShowDebugText = true;
			cinemachineBrain.m_DefaultBlend.m_Time = 1f;
			this.vcam = new GameObject("VirtualCamera").AddComponent<CinemachineVirtualCamera>();
			this.vcam.m_LookAt = GameObject.Find("Cube").transform;
			this.vcam.m_Priority = 10;
			this.vcam.gameObject.transform.position = new Vector3(0f, 1f, 0f);
			CinemachineComposer cinemachineComposer = this.vcam.AddCinemachineComponent<CinemachineComposer>();
			cinemachineComposer.m_ScreenX = 0.3f;
			cinemachineComposer.m_ScreenY = 0.35f;
			this.freelook = new GameObject("FreeLook").AddComponent<CinemachineFreeLook>();
			this.freelook.m_LookAt = GameObject.Find("Cylinder/Sphere").transform;
			this.freelook.m_Follow = GameObject.Find("Cylinder").transform;
			this.freelook.m_Priority = 11;
			CinemachineVirtualCamera rig = this.freelook.GetRig(0);
			CinemachineVirtualCamera rig2 = this.freelook.GetRig(1);
			CinemachineVirtualCamera rig3 = this.freelook.GetRig(2);
			rig.GetCinemachineComponent<CinemachineComposer>().m_ScreenY = 0.35f;
			rig2.GetCinemachineComponent<CinemachineComposer>().m_ScreenY = 0.25f;
			rig3.GetCinemachineComponent<CinemachineComposer>().m_ScreenY = 0.15f;
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x00010610 File Offset: 0x0000E810
		private void Update()
		{
			if (Time.realtimeSinceStartup - this.lastSwapTime > 5f)
			{
				this.freelook.enabled = !this.freelook.enabled;
				this.lastSwapTime = Time.realtimeSinceStartup;
			}
		}

		// Token: 0x040018E2 RID: 6370
		private CinemachineVirtualCamera vcam;

		// Token: 0x040018E3 RID: 6371
		private CinemachineFreeLook freelook;

		// Token: 0x040018E4 RID: 6372
		private float lastSwapTime;
	}
}
