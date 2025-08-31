using System;
using UnityEngine;

namespace Cinemachine.Examples
{
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

		public Transform followTarget;

		public float initialBottomWeight = 20f;

		public MixingCameraBlend.AxisEnum axisToTrack;

		private CinemachineMixingCamera vcam;

		public enum AxisEnum
		{
			X,
			Z,
			XZ
		}
	}
}
