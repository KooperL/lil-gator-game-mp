using System;
using UnityEngine;

// Token: 0x020001BF RID: 447
public class MakePlayerWet : MonoBehaviour
{
	// Token: 0x06000940 RID: 2368 RVA: 0x0002BF28 File Offset: 0x0002A128
	private void OnEnable()
	{
		this.playerEffects = Player.effects;
		if (this.playerEffects != null)
		{
			this.playerEffects.overrideIsWet++;
		}
	}

	// Token: 0x06000941 RID: 2369 RVA: 0x0002BF56 File Offset: 0x0002A156
	private void OnDisable()
	{
		if (this.playerEffects != null)
		{
			this.playerEffects.overrideIsWet--;
		}
	}

	// Token: 0x04000B9F RID: 2975
	private PlayerEffects playerEffects;
}
