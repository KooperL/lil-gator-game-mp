using System;
using UnityEngine;

// Token: 0x02000054 RID: 84
public class StaminaDrainItem : MonoBehaviour
{
	// Token: 0x0600012C RID: 300 RVA: 0x00003035 File Offset: 0x00001235
	private void OnEnable()
	{
		Player.movement.Stamina -= this.initialStaminaDrain;
	}

	// Token: 0x0600012D RID: 301 RVA: 0x0000304E File Offset: 0x0000124E
	public void FixedUpdate()
	{
		Player.movement.Stamina -= Time.deltaTime * this.drainSpeed;
		if (Player.movement.Stamina < 0f)
		{
			Player.movement.ClearMods();
		}
	}

	// Token: 0x040001BA RID: 442
	public float initialStaminaDrain;

	// Token: 0x040001BB RID: 443
	public float drainSpeed = 0.2f;
}
