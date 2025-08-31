using System;
using UnityEngine;

public class StaminaDrainItem : MonoBehaviour
{
	// Token: 0x06000107 RID: 263 RVA: 0x00006B2D File Offset: 0x00004D2D
	private void OnEnable()
	{
		Player.movement.Stamina -= this.initialStaminaDrain;
	}

	// Token: 0x06000108 RID: 264 RVA: 0x00006B46 File Offset: 0x00004D46
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
