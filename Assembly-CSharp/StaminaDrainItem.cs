using System;
using UnityEngine;

public class StaminaDrainItem : MonoBehaviour
{
	// Token: 0x06000134 RID: 308 RVA: 0x000030D8 File Offset: 0x000012D8
	private void OnEnable()
	{
		Player.movement.Stamina -= this.initialStaminaDrain;
	}

	// Token: 0x06000135 RID: 309 RVA: 0x000030F1 File Offset: 0x000012F1
	public void FixedUpdate()
	{
		Player.movement.Stamina -= Time.deltaTime * this.drainSpeed;
		if (Player.movement.Stamina < 0f)
		{
			Player.movement.ClearMods();
		}
	}

	public float initialStaminaDrain;

	public float drainSpeed = 0.2f;
}
