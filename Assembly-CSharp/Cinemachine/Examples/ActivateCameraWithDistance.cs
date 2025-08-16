using System;
using UnityEngine;

namespace Cinemachine.Examples
{
	[AddComponentMenu("")]
	public class ActivateCameraWithDistance : MonoBehaviour
	{
		// Token: 0x060013BB RID: 5051 RVA: 0x00010B3C File Offset: 0x0000ED3C
		private void Start()
		{
			this.SwitchCam(this.initialActiveCam);
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x00060954 File Offset: 0x0005EB54
		private void Update()
		{
			if (this.objectToCheck && this.switchCameraTo)
			{
				if (Vector3.Distance(base.transform.position, this.objectToCheck.transform.position) < this.distanceToObject)
				{
					this.SwitchCam(this.switchCameraTo);
					return;
				}
				this.SwitchCam(this.initialActiveCam);
			}
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x00010B4A File Offset: 0x0000ED4A
		public void SwitchCam(CinemachineVirtualCameraBase vcam)
		{
			if (Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera.Name != vcam.Name)
			{
				vcam.MoveToTopOfPrioritySubqueue();
			}
		}

		public GameObject objectToCheck;

		public float distanceToObject = 15f;

		public CinemachineVirtualCameraBase initialActiveCam;

		public CinemachineVirtualCameraBase switchCameraTo;
	}
}
