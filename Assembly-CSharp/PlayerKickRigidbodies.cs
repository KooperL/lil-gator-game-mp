using System;
using UnityEngine;

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

	public PlayerMovement playerMovement;

	public Rigidbody rigidbody;
}
