using System;
using UnityEngine;

public class PlayerTail : MonoBehaviour
{
	// Token: 0x06000AEA RID: 2794 RVA: 0x0003658F File Offset: 0x0003478F
	private void Awake()
	{
		this.neutralRotation = Quaternion.Euler(this.neutralX, 0f, 0f);
		this.swimmingRotation = Quaternion.Euler(this.swimmingX, 0f, 0f);
	}

	// Token: 0x06000AEB RID: 2795 RVA: 0x000365C8 File Offset: 0x000347C8
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
