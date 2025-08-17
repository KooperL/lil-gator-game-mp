using System;
using UnityEngine;

public class CameraCollidePlayer : MonoBehaviour
{
	// Token: 0x06000AAE RID: 2734 RVA: 0x0000A33A File Offset: 0x0000853A
	private void Awake()
	{
		CameraCollidePlayer.c = this;
	}

	// Token: 0x06000AAF RID: 2735 RVA: 0x0000A342 File Offset: 0x00008542
	private void OnEnable()
	{
		if (this.lockMode != CameraCollidePlayer.LockMode.None)
		{
			return;
		}
		this.camera.cullingMask = this.playerExcludedMask;
	}

	// Token: 0x06000AB0 RID: 2736 RVA: 0x0000A363 File Offset: 0x00008563
	private void OnDisable()
	{
		if (this.lockMode != CameraCollidePlayer.LockMode.None)
		{
			return;
		}
		this.camera.cullingMask = this.normalMask;
	}

	// Token: 0x06000AB1 RID: 2737 RVA: 0x0000A384 File Offset: 0x00008584
	private void FixedUpdate()
	{
		if (this.lastTriggerTime + 0.2f < Time.time)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06000AB2 RID: 2738 RVA: 0x0003DCE4 File Offset: 0x0003BEE4
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

	// Token: 0x06000AB3 RID: 2739 RVA: 0x0000A3A0 File Offset: 0x000085A0
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
