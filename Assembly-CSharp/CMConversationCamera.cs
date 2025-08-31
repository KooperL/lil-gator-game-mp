using System;
using Cinemachine;
using UnityEngine;

public class CMConversationCamera : MonoBehaviour
{
	// Token: 0x060008D6 RID: 2262 RVA: 0x000299AC File Offset: 0x00027BAC
	private void OnEnable()
	{
		CMConversationCamera.c = this;
		this.virtualCamera = base.GetComponent<CinemachineVirtualCamera>();
	}

	// Token: 0x060008D7 RID: 2263 RVA: 0x000299C0 File Offset: 0x00027BC0
	private void Start()
	{
		this.defaultFocus = this.virtualCamera.LookAt;
		this.centerLocalPosition = base.transform.localPosition;
		this.maxDistanceFromCenter = this.centerLocalPosition.x;
		this.centerLocalPosition.x = 0f;
		this.virtualCamera.enabled = this.isEnabled;
		base.enabled = this.isEnabled;
	}

	// Token: 0x060008D8 RID: 2264 RVA: 0x00029A2D File Offset: 0x00027C2D
	public void SetFocus(DialogueActor actor)
	{
		this.virtualCamera.LookAt = actor.DialogueAnchor;
		if (!this.isEnabled)
		{
			base.enabled = true;
			this.virtualCamera.enabled = true;
		}
	}

	// Token: 0x060008D9 RID: 2265 RVA: 0x00029A5C File Offset: 0x00027C5C
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

	// Token: 0x060008DA RID: 2266 RVA: 0x00029B3B File Offset: 0x00027D3B
	public void LockPosition(Vector3 position)
	{
		this.lockedPosition = position;
		this.isPositionLocked = true;
	}

	// Token: 0x060008DB RID: 2267 RVA: 0x00029B4B File Offset: 0x00027D4B
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
