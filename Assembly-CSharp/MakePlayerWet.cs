using System;
using UnityEngine;

public class MakePlayerWet : MonoBehaviour
{
	// Token: 0x06000B23 RID: 2851 RVA: 0x0000A867 File Offset: 0x00008A67
	private void OnEnable()
	{
		this.playerEffects = Player.effects;
		if (this.playerEffects != null)
		{
			this.playerEffects.overrideIsWet++;
		}
	}

	// Token: 0x06000B24 RID: 2852 RVA: 0x0000A895 File Offset: 0x00008A95
	private void OnDisable()
	{
		if (this.playerEffects != null)
		{
			this.playerEffects.overrideIsWet--;
		}
	}

	private PlayerEffects playerEffects;
}
