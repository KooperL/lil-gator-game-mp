using System;
using UnityEngine;

namespace Cinemachine.Examples
{
	// Token: 0x020002FA RID: 762
	[AddComponentMenu("")]
	public class MixingCameraBlend : MonoBehaviour
	{
		// Token: 0x06001035 RID: 4149 RVA: 0x0004DCE1 File Offset: 0x0004BEE1
		private void Start()
		{
			if (this.followTarget)
			{
				this.vcam = base.GetComponent<CinemachineMixingCamera>();
				this.vcam.m_Weight0 = this.initialBottomWeight;
			}
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x0004DD10 File Offset: 0x0004BF10
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

		// Token: 0x0400154A RID: 5450
		public Transform followTarget;

		// Token: 0x0400154B RID: 5451
		public float initialBottomWeight = 20f;

		// Token: 0x0400154C RID: 5452
		public MixingCameraBlend.AxisEnum axisToTrack;

		// Token: 0x0400154D RID: 5453
		private CinemachineMixingCamera vcam;

		// Token: 0x02000452 RID: 1106
		public enum AxisEnum
		{
			// Token: 0x04001E03 RID: 7683
			X,
			// Token: 0x04001E04 RID: 7684
			Z,
			// Token: 0x04001E05 RID: 7685
			XZ
		}
	}
}
