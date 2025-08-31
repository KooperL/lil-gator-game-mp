using System;
using UnityEngine;

public class InteractRock : MonoBehaviour, Interaction
{
	// Token: 0x060006B7 RID: 1719 RVA: 0x00022219 File Offset: 0x00020419
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x060006B8 RID: 1720 RVA: 0x00022227 File Offset: 0x00020427
	public void Interact()
	{
		ItemManager.i.UnlockItem("Rock");
		Object.Destroy(base.gameObject);
	}

	// Token: 0x060006B9 RID: 1721 RVA: 0x00022244 File Offset: 0x00020444
	private void FixedUpdate()
	{
		this.interactionCollider.enabled = this.rigidbody.velocity.sqrMagnitude < this.maxSpeedForInteraction;
		this.hitCollider.enabled = !this.interactionCollider.enabled;
	}

	private Rigidbody rigidbody;

	public float maxSpeedForInteraction = 0.5f;

	public BoxCollider interactionCollider;

	public BoxCollider hitCollider;
}
