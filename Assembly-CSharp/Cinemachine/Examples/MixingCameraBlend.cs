using System;
using UnityEngine;

namespace Cinemachine.Examples
{
	// Token: 0x020003EE RID: 1006
	[AddComponentMenu("")]
	public class MixingCameraBlend : MonoBehaviour
	{
		// Token: 0x06001364 RID: 4964 RVA: 0x000107C1 File Offset: 0x0000E9C1
		private void Start()
		{
			if (this.followTarget)
			{
				this.vcam = base.GetComponent<CinemachineMixingCamera>();
				this.vcam.m_Weight0 = this.initialBottomWeight;
			}
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x0005EC4C File Offset: 0x0005CE4C
		private void Update()
		{
			if (this.followTarget)
			{
				switch (this.axisToTrack)
				{
				case MixingCameraBlend.AxisEnum.X:
					this.vcam.m_Weight1 = Mathf.Abs(this.followTarget.transform.position.x);
					return;
				case MixingCameraBlend.AxisEnum.Z:
					this.vcam.m_Weight1 = Mathf.Abs(this.followTarget.transform.position.z);
					return;
				case MixingCameraBlend.AxisEnum.XZ:
					this.vcam.m_Weight1 = Mathf.Abs(Mathf.Abs(this.followTarget.transform.position.x) + Mathf.Abs(this.followTarget.transform.position.z));
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x0400190F RID: 6415
		public Transform followTarget;

		// Token: 0x04001910 RID: 6416
		public float initialBottomWeight = 20f;

		// Token: 0x04001911 RID: 6417
		public MixingCameraBlend.AxisEnum axisToTrack;

		// Token: 0x04001912 RID: 6418
		private CinemachineMixingCamera vcam;

		// Token: 0x020003EF RID: 1007
		public enum AxisEnum
		{
			// Token: 0x04001914 RID: 6420
			X,
			// Token: 0x04001915 RID: 6421
			Z,
			// Token: 0x04001916 RID: 6422
			XZ
		}
	}
}
