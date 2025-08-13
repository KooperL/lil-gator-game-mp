using System;
using UnityEngine;

// Token: 0x020001AE RID: 430
public class CameraCollidePlayer : MonoBehaviour
{
	// Token: 0x060008DD RID: 2269 RVA: 0x00029B5C File Offset: 0x00027D5C
	private void Awake()
	{
		CameraCollidePlayer.c = this;
	}

	// Token: 0x060008DE RID: 2270 RVA: 0x00029B64 File Offset: 0x00027D64
	private void OnEnable()
	{
		if (this.lockMode != CameraCollidePlayer.LockMode.None)
		{
			return;
		}
		this.camera.cullingMask = this.playerExcludedMask;
	}

	// Token: 0x060008DF RID: 2271 RVA: 0x00029B85 File Offset: 0x00027D85
	private void OnDisable()
	{
		if (this.lockMode != CameraCollidePlayer.LockMode.None)
		{
			return;
		}
		this.camera.cullingMask = this.normalMask;
	}

	// Token: 0x060008E0 RID: 2272 RVA: 0x00029BA6 File Offset: 0x00027DA6
	private void FixedUpdate()
	{
		if (this.lastTriggerTime + 0.2f < Time.time)
		{
			base.enabled = false;
		}
	}

	// Token: 0x060008E1 RID: 2273 RVA: 0x00029BC4 File Offset: 0x00027DC4
	public void SetLockMode(CameraCollidePlayer.LockMode lockMode)
	{
		this.lockMode = lockMode;
		base.enabled = false;
		switch (lockMode)
		{
		case CameraCollidePlayer.LockMode.None:
			this.lastTriggerTime = -1f;
			this.camera.cullingMask = this.normalMask;
			return;
		case CameraCollidePlayer.LockMode.LockedOn:
			this.camera.cullingMask = this.playerExcludedMask;
			return;
		case CameraCollidePlayer.LockMode.LockedOff:
			this.camera.cullingMask = this.normalMask;
			return;
		default:
			return;
		}
	}

	// Token: 0x060008E2 RID: 2274 RVA: 0x00029C41 File Offset: 0x00027E41
	private void OnTriggerStay(Collider other)
	{
		base.enabled = true;
		this.lastTriggerTime = Time.time;
	}

	// Token: 0x04000AF4 RID: 2804
	public static CameraCollidePlayer c;

	// Token: 0x04000AF5 RID: 2805
	public Camera camera;

	// Token: 0x04000AF6 RID: 2806
	public LayerMask normalMask;

	// Token: 0x04000AF7 RID: 2807
	public LayerMask playerExcludedMask;

	// Token: 0x04000AF8 RID: 2808
	private float lastTriggerTime = -100f;

	// Token: 0x04000AF9 RID: 2809
	private CameraCollidePlayer.LockMode lockMode;

	// Token: 0x020003D7 RID: 983
	public enum LockMode
	{
		// Token: 0x04001C30 RID: 7216
		None,
		// Token: 0x04001C31 RID: 7217
		LockedOn,
		// Token: 0x04001C32 RID: 7218
		LockedOff
	}
}
