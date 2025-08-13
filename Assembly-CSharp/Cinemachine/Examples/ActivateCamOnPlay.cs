using System;
using UnityEngine;

namespace Cinemachine.Examples
{
	// Token: 0x020003EA RID: 1002
	[AddComponentMenu("")]
	public class ActivateCamOnPlay : MonoBehaviour
	{
		// Token: 0x06001359 RID: 4953 RVA: 0x0001073A File Offset: 0x0000E93A
		private void Start()
		{
			if (this.vcam)
			{
				this.vcam.MoveToTopOfPrioritySubqueue();
			}
		}

		// Token: 0x04001904 RID: 6404
		public CinemachineVirtualCameraBase vcam;
	}
}
