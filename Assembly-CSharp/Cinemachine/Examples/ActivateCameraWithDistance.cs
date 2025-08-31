using System;
using UnityEngine;

namespace Cinemachine.Examples
{
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

		public GameObject objectToCheck;

		public float distanceToObject = 15f;

		public CinemachineVirtualCameraBase initialActiveCam;

		public CinemachineVirtualCameraBase switchCameraTo;
	}
}
