using System;
using Cinemachine;
using UnityEngine;

// Token: 0x02000228 RID: 552
public class CMConversationCamera : MonoBehaviour
{
	// Token: 0x06000A5D RID: 2653 RVA: 0x00009FAB File Offset: 0x000081AB
	private void OnEnable()
	{
		CMConversationCamera.c = this;
		this.virtualCamera = base.GetComponent<CinemachineVirtualCamera>();
	}

	// Token: 0x06000A5E RID: 2654 RVA: 0x0003C0E8 File Offset: 0x0003A2E8
	private void Start()
	{
		this.defaultFocus = this.virtualCamera.LookAt;
		this.centerLocalPosition = base.transform.localPosition;
		this.maxDistanceFromCenter = this.centerLocalPosition.x;
		this.centerLocalPosition.x = 0f;
		this.virtualCamera.enabled = this.isEnabled;
		base.enabled = this.isEnabled;
	}

	// Token: 0x06000A5F RID: 2655 RVA: 0x00009FBF File Offset: 0x000081BF
	public void SetFocus(DialogueActor actor)
	{
		this.virtualCamera.LookAt = actor.DialogueAnchor;
		if (!this.isEnabled)
		{
			base.enabled = true;
			this.virtualCamera.enabled = true;
		}
	}

	// Token: 0x06000A60 RID: 2656 RVA: 0x0003C158 File Offset: 0x0003A358
	private void Update()
	{
		if (Game.HasControl)
		{
			base.enabled = false;
			this.virtualCamera.enabled = false;
			this.isEnabled = false;
			this.virtualCamera.LookAt = this.defaultFocus;
			this.isPositionLocked = false;
			return;
		}
		if (this.isPositionLocked)
		{
			base.transform.position = this.lockedPosition;
			return;
		}
		Vector3 vector = base.transform.parent.TransformPoint(this.centerLocalPosition);
		Vector3 vector2 = base.transform.parent.TransformVector(Vector3.right);
		Vector3 vector3 = vector + this.maxDistanceFromCenter * vector2;
		RaycastHit raycastHit;
		if (Physics.Raycast(vector, vector2, ref raycastHit, this.maxDistanceFromCenter, this.raycastMask))
		{
			vector3 = raycastHit.point - 0.1f * vector2;
		}
		base.transform.position = vector3;
	}

	// Token: 0x06000A61 RID: 2657 RVA: 0x00009FED File Offset: 0x000081ED
	public void LockPosition(Vector3 position)
	{
		this.lockedPosition = position;
		this.isPositionLocked = true;
	}

	// Token: 0x06000A62 RID: 2658 RVA: 0x00009FFD File Offset: 0x000081FD
	public void UnlockPosition()
	{
		this.isPositionLocked = false;
	}

	// Token: 0x04000CEF RID: 3311
	public static CMConversationCamera c;

	// Token: 0x04000CF0 RID: 3312
	private CinemachineVirtualCamera virtualCamera;

	// Token: 0x04000CF1 RID: 3313
	private Transform defaultFocus;

	// Token: 0x04000CF2 RID: 3314
	public LayerMask raycastMask;

	// Token: 0x04000CF3 RID: 3315
	private Vector3 centerLocalPosition;

	// Token: 0x04000CF4 RID: 3316
	private float maxDistanceFromCenter;

	// Token: 0x04000CF5 RID: 3317
	private bool isEnabled;

	// Token: 0x04000CF6 RID: 3318
	private Vector3 lockedPosition;

	// Token: 0x04000CF7 RID: 3319
	private bool isPositionLocked;
}
