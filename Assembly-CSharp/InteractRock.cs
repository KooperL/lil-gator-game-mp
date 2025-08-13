using System;
using UnityEngine;

// Token: 0x020001AE RID: 430
public class InteractRock : MonoBehaviour, Interaction
{
	// Token: 0x060007F5 RID: 2037 RVA: 0x00007E42 File Offset: 0x00006042
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x060007F6 RID: 2038 RVA: 0x00007E50 File Offset: 0x00006050
	public void Interact()
	{
		ItemManager.i.UnlockItem("Rock");
		Object.Destroy(base.gameObject);
	}

	// Token: 0x060007F7 RID: 2039 RVA: 0x00035B14 File Offset: 0x00033D14
	private void FixedUpdate()
	{
		this.interactionCollider.enabled = this.rigidbody.velocity.sqrMagnitude < this.maxSpeedForInteraction;
		this.hitCollider.enabled = !this.interactionCollider.enabled;
	}

	// Token: 0x04000A97 RID: 2711
	private Rigidbody rigidbody;

	// Token: 0x04000A98 RID: 2712
	public float maxSpeedForInteraction = 0.5f;

	// Token: 0x04000A99 RID: 2713
	public BoxCollider interactionCollider;

	// Token: 0x04000A9A RID: 2714
	public BoxCollider hitCollider;
}
