using System;
using UnityEngine;

namespace Cinemachine.Examples
{
	[AddComponentMenu("")]
	public class MixingCameraBlend : MonoBehaviour
	{
		// Token: 0x060013C4 RID: 5060 RVA: 0x00010BC8 File Offset: 0x0000EDC8
		private void Start()
		{
			if (this.followTarget)
			{
				this.vcam = base.GetComponent<CinemachineMixingCamera>();
				this.vcam.m_Weight0 = this.initialBottomWeight;
			}
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x00060C50 File Offset: 0x0005EE50
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
