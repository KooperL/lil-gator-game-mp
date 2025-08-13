using System;
using UnityEngine;

// Token: 0x02000286 RID: 646
public class PlayerTail : MonoBehaviour
{
	// Token: 0x06000CA3 RID: 3235 RVA: 0x0000BCF7 File Offset: 0x00009EF7
	private void Awake()
	{
		this.neutralRotation = Quaternion.Euler(this.neutralX, 0f, 0f);
		this.swimmingRotation = Quaternion.Euler(this.swimmingX, 0f, 0f);
	}

	// Token: 0x06000CA4 RID: 3236 RVA: 0x000474F8 File Offset: 0x000456F8
	private void LateUpdate()
	{
		if (this.movement != null)
		{
			this.swimmingLerp = Mathf.MoveTowards(this.swimmingLerp, this.movement.IsSwimming ? 1f : 0f, 2f * Time.deltaTime);
		}
		base.transform.localRotation = Quaternion.Lerp(this.neutralRotation, this.swimmingRotation, this.swimmingLerp);
	}

	// Token: 0x040010CA RID: 4298
	public PlayerMovement movement;

	// Token: 0x040010CB RID: 4299
	public float neutralX;

	// Token: 0x040010CC RID: 4300
	public float swimmingX;

	// Token: 0x040010CD RID: 4301
	private Quaternion neutralRotation;

	// Token: 0x040010CE RID: 4302
	private Quaternion swimmingRotation;

	// Token: 0x040010CF RID: 4303
	private float swimmingLerp;
}
