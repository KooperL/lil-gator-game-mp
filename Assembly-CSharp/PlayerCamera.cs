using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200028C RID: 652
public class PlayerCamera : MonoBehaviour
{
	// Token: 0x06000CCB RID: 3275 RVA: 0x00048254 File Offset: 0x00046454
	public static void FocusOnConversation(DialogueActor actor)
	{
		if (actor == null)
		{
			PlayerCamera.p.StartCoroutine(PlayerCamera.p.Focus(DialogueActor.playerActor.GetDialoguePosition()));
			return;
		}
		PlayerCamera.p.StartCoroutine(PlayerCamera.p.Focus(Vector3.Lerp(actor.GetDialoguePosition(), DialogueActor.playerActor.GetDialoguePosition(), 0.5f)));
	}

	// Token: 0x06000CCC RID: 3276 RVA: 0x0000BF28 File Offset: 0x0000A128
	private void OnEnable()
	{
		PlayerCamera.p = this;
	}

	// Token: 0x06000CCD RID: 3277 RVA: 0x0000BF30 File Offset: 0x0000A130
	private void Start()
	{
		this.angle = this.defaultAngle;
		this.distance = this.defaultDistance;
	}

	// Token: 0x06000CCE RID: 3278 RVA: 0x000482BC File Offset: 0x000464BC
	private void FixedUpdate()
	{
		float num = this.defaultAngle;
		RaycastHit raycastHit;
		while (this.terrainCollider.Raycast(new Ray(base.transform.position, Quaternion.Euler(num, 0f, 0f) * Vector3.back), ref raycastHit, this.defaultDistance))
		{
			num += this.angleStep;
		}
		this.angle = Mathf.SmoothDamp(this.angle, num, ref this.angleVelocity, this.rotationTime, float.PositiveInfinity, Time.deltaTime);
		this.cameraTransform.localRotation = Quaternion.Euler(this.angle, 0f, 0f);
		this.cameraTransform.localPosition = this.cameraTransform.localRotation * Vector3.back * this.defaultDistance;
		if (this.focusing || this.focusLerp > 0f)
		{
			this.focusLerp = Mathf.SmoothDamp(this.focusLerp, this.focusing ? 1f : 0f, ref this.focusLerpVelocity, 0.5f);
			Vector3 vector = this.focusTarget - this.cameraTransform.position;
			this.cameraTransform.localRotation = Quaternion.Slerp(this.cameraTransform.localRotation, Quaternion.LookRotation(vector), this.focusLerp);
			this.camera.fieldOfView = Mathf.Lerp(this.standardFOV, this.focusFOV, this.focusLerp);
		}
	}

	// Token: 0x06000CCF RID: 3279 RVA: 0x0000BF4A File Offset: 0x0000A14A
	public IEnumerator Focus(Vector3 newFocusTarget)
	{
		this.focusing = true;
		if (this.focusLerp <= 0.01f)
		{
			this.focusTarget = newFocusTarget;
			yield break;
		}
		while (Vector3.SqrMagnitude(newFocusTarget - this.focusTarget) > 0.01f)
		{
			this.focusTarget = Vector3.Lerp(this.focusTarget, newFocusTarget, 2f * Time.deltaTime);
			yield return null;
		}
		yield break;
	}

	// Token: 0x06000CD0 RID: 3280 RVA: 0x0000BF60 File Offset: 0x0000A160
	public void StopFocus()
	{
		this.focusing = false;
	}

	// Token: 0x04001110 RID: 4368
	public static PlayerCamera p;

	// Token: 0x04001111 RID: 4369
	public Camera camera;

	// Token: 0x04001112 RID: 4370
	public Transform cameraTransform;

	// Token: 0x04001113 RID: 4371
	public Collider terrainCollider;

	// Token: 0x04001114 RID: 4372
	public float defaultAngle = 40f;

	// Token: 0x04001115 RID: 4373
	public float maxAngle = 90f;

	// Token: 0x04001116 RID: 4374
	public float angleStep = 10f;

	// Token: 0x04001117 RID: 4375
	public float defaultDistance = 15f;

	// Token: 0x04001118 RID: 4376
	public float rotationTime = 5f;

	// Token: 0x04001119 RID: 4377
	public float standardFOV = 35f;

	// Token: 0x0400111A RID: 4378
	public float focusFOV = 20f;

	// Token: 0x0400111B RID: 4379
	private Vector3 focusTarget;

	// Token: 0x0400111C RID: 4380
	private bool focusing;

	// Token: 0x0400111D RID: 4381
	private float focusLerp;

	// Token: 0x0400111E RID: 4382
	private float focusLerpVelocity;

	// Token: 0x0400111F RID: 4383
	private float angle;

	// Token: 0x04001120 RID: 4384
	private float distance;

	// Token: 0x04001121 RID: 4385
	private float angleVelocity;
}
