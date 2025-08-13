using System;
using UnityEngine;

namespace Cinemachine.Examples
{
	// Token: 0x020002F8 RID: 760
	[AddComponentMenu("")]
	public class ActivateCameraWithDistance : MonoBehaviour
	{
		// Token: 0x0600102E RID: 4142 RVA: 0x0004DAFB File Offset: 0x0004BCFB
		private void Start()
		{
			this.SwitchCam(this.initialActiveCam);
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x0004DB0C File Offset: 0x0004BD0C
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

		// Token: 0x06001030 RID: 4144 RVA: 0x0004DB74 File Offset: 0x0004BD74
		public void SwitchCam(CinemachineVirtualCameraBase vcam)
		{
			if (Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera.Name != vcam.Name)
			{
				vcam.MoveToTopOfPrioritySubqueue();
			}
		}

		// Token: 0x04001542 RID: 5442
		public GameObject objectToCheck;

		// Token: 0x04001543 RID: 5443
		public float distanceToObject = 15f;

		// Token: 0x04001544 RID: 5444
		public CinemachineVirtualCameraBase initialActiveCam;

		// Token: 0x04001545 RID: 5445
		public CinemachineVirtualCameraBase switchCameraTo;
	}
}
