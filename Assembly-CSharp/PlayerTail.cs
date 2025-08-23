using System;
using UnityEngine;

public class PlayerTail : MonoBehaviour
{
	// Token: 0x06000CF0 RID: 3312 RVA: 0x0000C009 File Offset: 0x0000A209
	private void Awake()
	{
		this.neutralRotation = Quaternion.Euler(this.neutralX, 0f, 0f);
		this.swimmingRotation = Quaternion.Euler(this.swimmingX, 0f, 0f);
	}

	// Token: 0x06000CF1 RID: 3313 RVA: 0x00049348 File Offset: 0x00047548
	private void LateUpdate()
	{
		if (this.movement != null)
		{
			this.swimmingLerp = Mathf.MoveTowards(this.swimmingLerp, this.movement.IsSwimming ? 1f : 0f, 2f * Time.deltaTime);
		}
		base.transform.localRotation = Quaternion.Lerp(this.neutralRotation, this.swimmingRotation, this.swimmingLerp);
	}

	public PlayerMovement movement;

	public float neutralX;

	public float swimmingX;

	private Quaternion neutralRotation;

	private Quaternion swimmingRotation;

	private float swimmingLerp;
}
