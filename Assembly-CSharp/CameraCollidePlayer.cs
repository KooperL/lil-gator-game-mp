using System;
using UnityEngine;

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

	public static CameraCollidePlayer c;

	public Camera camera;

	public LayerMask normalMask;

	public LayerMask playerExcludedMask;

	private float lastTriggerTime = -100f;

	private CameraCollidePlayer.LockMode lockMode;

	public enum LockMode
	{
		None,
		LockedOn,
		LockedOff
	}
}
