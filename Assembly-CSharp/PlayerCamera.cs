using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001FB RID: 507
public class PlayerCamera : MonoBehaviour
{
	// Token: 0x06000B0C RID: 2828 RVA: 0x000372B8 File Offset: 0x000354B8
	public static void FocusOnConversation(DialogueActor actor)
	{
		if (actor == null)
		{
			PlayerCamera.p.StartCoroutine(PlayerCamera.p.Focus(DialogueActor.playerActor.GetDialoguePosition()));
			return;
		}
		PlayerCamera.p.StartCoroutine(PlayerCamera.p.Focus(Vector3.Lerp(actor.GetDialoguePosition(), DialogueActor.playerActor.GetDialoguePosition(), 0.5f)));
	}

	// Token: 0x06000B0D RID: 2829 RVA: 0x0003731D File Offset: 0x0003551D
	private void OnEnable()
	{
		PlayerCamera.p = this;
	}

	// Token: 0x06000B0E RID: 2830 RVA: 0x00037325 File Offset: 0x00035525
	private void Start()
	{
		this.angle = this.defaultAngle;
		this.distance = this.defaultDistance;
	}

	// Token: 0x06000B0F RID: 2831 RVA: 0x00037340 File Offset: 0x00035540
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

	// Token: 0x06000B10 RID: 2832 RVA: 0x000374B9 File Offset: 0x000356B9
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

	// Token: 0x06000B11 RID: 2833 RVA: 0x000374CF File Offset: 0x000356CF
	public void StopFocus()
	{
		this.focusing = false;
	}

	// Token: 0x04000EB5 RID: 3765
	public static PlayerCamera p;

	// Token: 0x04000EB6 RID: 3766
	public Camera camera;

	// Token: 0x04000EB7 RID: 3767
	public Transform cameraTransform;

	// Token: 0x04000EB8 RID: 3768
	public Collider terrainCollider;

	// Token: 0x04000EB9 RID: 3769
	public float defaultAngle = 40f;

	// Token: 0x04000EBA RID: 3770
	public float maxAngle = 90f;

	// Token: 0x04000EBB RID: 3771
	public float angleStep = 10f;

	// Token: 0x04000EBC RID: 3772
	public float defaultDistance = 15f;

	// Token: 0x04000EBD RID: 3773
	public float rotationTime = 5f;

	// Token: 0x04000EBE RID: 3774
	public float standardFOV = 35f;

	// Token: 0x04000EBF RID: 3775
	public float focusFOV = 20f;

	// Token: 0x04000EC0 RID: 3776
	private Vector3 focusTarget;

	// Token: 0x04000EC1 RID: 3777
	private bool focusing;

	// Token: 0x04000EC2 RID: 3778
	private float focusLerp;

	// Token: 0x04000EC3 RID: 3779
	private float focusLerpVelocity;

	// Token: 0x04000EC4 RID: 3780
	private float angle;

	// Token: 0x04000EC5 RID: 3781
	private float distance;

	// Token: 0x04000EC6 RID: 3782
	private float angleVelocity;
}
