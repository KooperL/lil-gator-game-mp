using System;
using UnityEngine;

namespace Cinemachine.Examples
{
	[AddComponentMenu("")]
	public class ActivateCamOnPlay : MonoBehaviour
	{
		// Token: 0x060013B9 RID: 5049 RVA: 0x00010B22 File Offset: 0x0000ED22
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
