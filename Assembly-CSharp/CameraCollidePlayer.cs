using System;
using UnityEngine;

// Token: 0x02000229 RID: 553
public class CameraCollidePlayer : MonoBehaviour
{
	// Token: 0x06000A64 RID: 2660 RVA: 0x0000A006 File Offset: 0x00008206
	private void Awake()
	{
		CameraCollidePlayer.c = this;
	}

	// Token: 0x06000A65 RID: 2661 RVA: 0x0000A00E File Offset: 0x0000820E
	private void OnEnable()
	{
		if (this.lockMode != CameraCollidePlayer.LockMode.None)
		{
			return;
		}
		this.camera.cullingMask = this.playerExcludedMask;
	}

	// Token: 0x06000A66 RID: 2662 RVA: 0x0000A02F File Offset: 0x0000822F
	private void OnDisable()
	{
		if (this.lockMode != CameraCollidePlayer.LockMode.None)
		{
			return;
		}
		this.camera.cullingMask = this.normalMask;
	}

	// Token: 0x06000A67 RID: 2663 RVA: 0x0000A050 File Offset: 0x00008250
	private void FixedUpdate()
	{
		if (this.lastTriggerTime + 0.2f < Time.time)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06000A68 RID: 2664 RVA: 0x0003C238 File Offset: 0x0003A438
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

	// Token: 0x06000A69 RID: 2665 RVA: 0x0000A06C File Offset: 0x0000826C
	private void OnTriggerStay(Collider other)
	{
		base.enabled = true;
		this.lastTriggerTime = Time.time;
	}

	// Token: 0x04000CF8 RID: 3320
	public static CameraCollidePlayer c;

	// Token: 0x04000CF9 RID: 3321
	public Camera camera;

	// Token: 0x04000CFA RID: 3322
	public LayerMask normalMask;

	// Token: 0x04000CFB RID: 3323
	public LayerMask playerExcludedMask;

	// Token: 0x04000CFC RID: 3324
	private float lastTriggerTime = -100f;

	// Token: 0x04000CFD RID: 3325
	private CameraCollidePlayer.LockMode lockMode;

	// Token: 0x0200022A RID: 554
	public enum LockMode
	{
		// Token: 0x04000CFF RID: 3327
		None,
		// Token: 0x04000D00 RID: 3328
		LockedOn,
		// Token: 0x04000D01 RID: 3329
		LockedOff
	}
}
