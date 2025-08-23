using System;
using UnityEngine;

public class InteractRock : MonoBehaviour, Interaction
{
	// Token: 0x06000836 RID: 2102 RVA: 0x00008151 File Offset: 0x00006351
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06000837 RID: 2103 RVA: 0x0000815F File Offset: 0x0000635F
	public void Interact()
	{
		ItemManager.i.UnlockItem("Rock");
		global::UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000838 RID: 2104 RVA: 0x00037744 File Offset: 0x00035944
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
