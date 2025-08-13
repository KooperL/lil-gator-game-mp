using System;
using UnityEngine;

namespace Cinemachine.Examples
{
	// Token: 0x020003EB RID: 1003
	[AddComponentMenu("")]
	public class ActivateCameraWithDistance : MonoBehaviour
	{
		// Token: 0x0600135B RID: 4955 RVA: 0x00010754 File Offset: 0x0000E954
		private void Start()
		{
			this.SwitchCam(this.initialActiveCam);
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x0005EAC0 File Offset: 0x0005CCC0
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

		// Token: 0x0600135D RID: 4957 RVA: 0x00010762 File Offset: 0x0000E962
		public void SwitchCam(CinemachineVirtualCameraBase vcam)
		{
			if (Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera.Name != vcam.Name)
			{
				vcam.MoveToTopOfPrioritySubqueue();
			}
		}

		// Token: 0x04001905 RID: 6405
		public GameObject objectToCheck;

		// Token: 0x04001906 RID: 6406
		public float distanceToObject = 15f;

		// Token: 0x04001907 RID: 6407
		public CinemachineVirtualCameraBase initialActiveCam;

		// Token: 0x04001908 RID: 6408
		public CinemachineVirtualCameraBase switchCameraTo;
	}
}
