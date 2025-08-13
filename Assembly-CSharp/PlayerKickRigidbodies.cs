using System;
using UnityEngine;

// Token: 0x02000279 RID: 633
public class PlayerKickRigidbodies : MonoBehaviour
{
	// Token: 0x06000C33 RID: 3123 RVA: 0x00042EA0 File Offset: 0x000410A0
	private void OnValidate()
	{
		if (this.rigidbody == null)
		{
			this.rigidbody = base.GetComponent<Rigidbody>();
		}
		if (this.playerMovement == null)
		{
			this.playerMovement = base.GetComponent<PlayerMovement>();
		}
		if (this.playerMovement == null)
		{
			this.playerMovement = base.GetComponentInParent<PlayerMovement>();
		}
	}

	// Token: 0x04000FAE RID: 4014
	public PlayerMovement playerMovement;

	// Token: 0x04000FAF RID: 4015
	public Rigidbody rigidbody;
}
