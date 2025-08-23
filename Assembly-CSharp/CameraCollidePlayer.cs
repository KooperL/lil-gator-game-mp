using System;
using UnityEngine;

public class CameraCollidePlayer : MonoBehaviour
{
	// Token: 0x06000AAF RID: 2735 RVA: 0x0000A344 File Offset: 0x00008544
	private void Awake()
	{
		CameraCollidePlayer.c = this;
	}

	// Token: 0x06000AB0 RID: 2736 RVA: 0x0000A34C File Offset: 0x0000854C
	private void OnEnable()
	{
		if (this.lockMode != CameraCollidePlayer.LockMode.None)
		{
			return;
		}
		this.camera.cullingMask = this.playerExcludedMask;
	}

	// Token: 0x06000AB1 RID: 2737 RVA: 0x0000A36D File Offset: 0x0000856D
	private void OnDisable()
	{
		if (this.lockMode != CameraCollidePlayer.LockMode.None)
		{
			return;
		}
		this.camera.cullingMask = this.normalMask;
	}

	// Token: 0x06000AB2 RID: 2738 RVA: 0x0000A38E File Offset: 0x0000858E
	private void FixedUpdate()
	{
		if (this.lastTriggerTime + 0.2f < Time.time)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06000AB3 RID: 2739 RVA: 0x0003DFAC File Offset: 0x0003C1AC
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

	// Token: 0x06000AB4 RID: 2740 RVA: 0x0000A3AA File Offset: 0x000085AA
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
