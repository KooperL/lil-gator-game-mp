using System;
using UnityEngine;

namespace Cinemachine.Examples
{
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

		public CinemachineVirtualCameraBase vcam;
	}
}
