using System;
using System.Collections;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
	// Token: 0x06000D17 RID: 3351 RVA: 0x00049DB8 File Offset: 0x00047FB8
	public static void FocusOnConversation(DialogueActor actor)
	{
		if (actor == null)
		{
			PlayerCamera.p.StartCoroutine(PlayerCamera.p.Focus(DialogueActor.playerActor.GetDialoguePosition()));
			return;
		}
		PlayerCamera.p.StartCoroutine(PlayerCamera.p.Focus(Vector3.Lerp(actor.GetDialoguePosition(), DialogueActor.playerActor.GetDialoguePosition(), 0.5f)));
	}

	// Token: 0x06000D18 RID: 3352 RVA: 0x0000C23A File Offset: 0x0000A43A
	private void OnEnable()
	{
		PlayerCamera.p = this;
	}

	// Token: 0x06000D19 RID: 3353 RVA: 0x0000C242 File Offset: 0x0000A442
	private void Start()
	{
		this.angle = this.defaultAngle;
		this.distance = this.defaultDistance;
	}

	// Token: 0x06000D1A RID: 3354 RVA: 0x00049E20 File Offset: 0x00048020
	private void FixedUpdate()
	{
		float num = this.defaultAngle;
		RaycastHit raycastHit;
		while (this.terrainCollider.Raycast(new Ray(base.transform.position, Quaternion.Euler(num, 0f, 0f) * Vector3.back), out raycastHit, this.defaultDistance))
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

	// Token: 0x06000D1B RID: 3355 RVA: 0x0000C25C File Offset: 0x0000A45C
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

	// Token: 0x06000D1C RID: 3356 RVA: 0x0000C272 File Offset: 0x0000A472
	public void StopFocus()
	{
		this.focusing = false;
	}

	public static PlayerCamera p;

	public Camera camera;

	public Transform cameraTransform;

	public Collider terrainCollider;

	public float defaultAngle = 40f;

	public float maxAngle = 90f;

	public float angleStep = 10f;

	public float defaultDistance = 15f;

	public float rotationTime = 5f;

	public float standardFOV = 35f;

	public float focusFOV = 20f;

	private Vector3 focusTarget;

	private bool focusing;

	private float focusLerp;

	private float focusLerpVelocity;

	private float angle;

	private float distance;

	private float angleVelocity;
}
