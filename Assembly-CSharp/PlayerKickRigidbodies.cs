using System;
using UnityEngine;

// Token: 0x020001ED RID: 493
public class PlayerKickRigidbodies : MonoBehaviour
{
	// Token: 0x06000A7E RID: 2686 RVA: 0x00031944 File Offset: 0x0002FB44
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

	// Token: 0x04000D61 RID: 3425
	public PlayerMovement playerMovement;

	// Token: 0x04000D62 RID: 3426
	public Rigidbody rigidbody;
}
