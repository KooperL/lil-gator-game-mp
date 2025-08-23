using System;
using UnityEngine;

namespace Cinemachine.Examples
{
	[AddComponentMenu("")]
	public class ActivateCameraWithDistance : MonoBehaviour
	{
		// Token: 0x060013BC RID: 5052 RVA: 0x00010B5B File Offset: 0x0000ED5B
		private void Start()
		{
			this.SwitchCam(this.initialActiveCam);
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x00060DB0 File Offset: 0x0005EFB0
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

		// Token: 0x060013BE RID: 5054 RVA: 0x00010B69 File Offset: 0x0000ED69
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
