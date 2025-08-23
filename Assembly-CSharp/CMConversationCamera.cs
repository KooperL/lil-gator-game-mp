using System;
using Cinemachine;
using UnityEngine;

public class CMConversationCamera : MonoBehaviour
{
	// Token: 0x06000AA8 RID: 2728 RVA: 0x0000A2E9 File Offset: 0x000084E9
	private void OnEnable()
	{
		CMConversationCamera.c = this;
		this.virtualCamera = base.GetComponent<CinemachineVirtualCamera>();
	}

	// Token: 0x06000AA9 RID: 2729 RVA: 0x0003DE5C File Offset: 0x0003C05C
	private void Start()
	{
		this.defaultFocus = this.virtualCamera.LookAt;
		this.centerLocalPosition = base.transform.localPosition;
		this.maxDistanceFromCenter = this.centerLocalPosition.x;
		this.centerLocalPosition.x = 0f;
		this.virtualCamera.enabled = this.isEnabled;
		base.enabled = this.isEnabled;
	}

	// Token: 0x06000AAA RID: 2730 RVA: 0x0000A2FD File Offset: 0x000084FD
	public void SetFocus(DialogueActor actor)
	{
		this.virtualCamera.LookAt = actor.DialogueAnchor;
		if (!this.isEnabled)
		{
			base.enabled = true;
			this.virtualCamera.enabled = true;
		}
	}

	// Token: 0x06000AAB RID: 2731 RVA: 0x0003DECC File Offset: 0x0003C0CC
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
		if (Physics.Raycast(vector, vector2, out raycastHit, this.maxDistanceFromCenter, this.raycastMask))
		{
			vector3 = raycastHit.point - 0.1f * vector2;
		}
		base.transform.position = vector3;
	}

	// Token: 0x06000AAC RID: 2732 RVA: 0x0000A32B File Offset: 0x0000852B
	public void LockPosition(Vector3 position)
	{
		this.lockedPosition = position;
		this.isPositionLocked = true;
	}

	// Token: 0x06000AAD RID: 2733 RVA: 0x0000A33B File Offset: 0x0000853B
	public void UnlockPosition()
	{
		this.isPositionLocked = false;
	}

	public static CMConversationCamera c;

	private CinemachineVirtualCamera virtualCamera;

	private Transform defaultFocus;

	public LayerMask raycastMask;

	private Vector3 centerLocalPosition;

	private float maxDistanceFromCenter;

	private bool isEnabled;

	private Vector3 lockedPosition;

	private bool isPositionLocked;
}
