using System;
using UnityEngine;

namespace Cinemachine.Examples
{
	// Token: 0x020002F7 RID: 759
	[AddComponentMenu("")]
	public class ActivateCamOnPlay : MonoBehaviour
	{
		// Token: 0x0600102C RID: 4140 RVA: 0x0004DAD9 File Offset: 0x0004BCD9
		private void Start()
		{
			if (this.vcam)
			{
				this.vcam.MoveToTopOfPrioritySubqueue();
			}
		}

		// Token: 0x04001541 RID: 5441
		public CinemachineVirtualCameraBase vcam;
	}
}
