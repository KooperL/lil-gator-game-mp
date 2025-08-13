using System;
using UnityEngine;

// Token: 0x020001F6 RID: 502
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

	// Token: 0x04000E72 RID: 3698
	public PlayerMovement movement;

	// Token: 0x04000E73 RID: 3699
	public float neutralX;

	// Token: 0x04000E74 RID: 3700
	public float swimmingX;

	// Token: 0x04000E75 RID: 3701
	private Quaternion neutralRotation;

	// Token: 0x04000E76 RID: 3702
	private Quaternion swimmingRotation;

	// Token: 0x04000E77 RID: 3703
	private float swimmingLerp;
}
