using System;
using UnityEngine;

// Token: 0x02000240 RID: 576
public class MakePlayerWet : MonoBehaviour
{
	// Token: 0x06000AD7 RID: 2775 RVA: 0x0000A533 File Offset: 0x00008733
	private void OnEnable()
	{
		this.playerEffects = Player.effects;
		if (this.playerEffects != null)
		{
			this.playerEffects.overrideIsWet++;
		}
	}

	// Token: 0x06000AD8 RID: 2776 RVA: 0x0000A561 File Offset: 0x00008761
	private void OnDisable()
	{
		if (this.playerEffects != null)
		{
			this.playerEffects.overrideIsWet--;
		}
	}

	// Token: 0x04000DBB RID: 3515
	private PlayerEffects playerEffects;
}
