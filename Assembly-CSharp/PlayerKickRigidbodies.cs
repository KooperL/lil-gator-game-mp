using System;
using UnityEngine;

public class PlayerKickRigidbodies : MonoBehaviour
{
	// Token: 0x06000C80 RID: 3200 RVA: 0x00044CF0 File Offset: 0x00042EF0
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
