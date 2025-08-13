using System;
using UnityEngine;

// Token: 0x0200004E RID: 78
public class BabyPlayerToggle : MonoBehaviour
{
	// Token: 0x06000116 RID: 278 RVA: 0x00002E6A File Offset: 0x0000106A
	public static void SetBabyMode(bool isBabyMode)
	{
		BabyPlayerToggle.isBabyMode = isBabyMode;
		if (BabyPlayerToggle.b != null)
		{
			BabyPlayerToggle.b.UpdateBabyMode();
		}
	}

	// Token: 0x06000117 RID: 279 RVA: 0x00002E89 File Offset: 0x00001089
	private void Start()
	{
		BabyPlayerToggle.b = this;
		this.UpdateBabyMode();
	}

	// Token: 0x06000118 RID: 280 RVA: 0x0001B0EC File Offset: 0x000192EC
	protected void UpdateBabyMode()
	{
		if (this != null && base.gameObject.activeInHierarchy && Game.WorldState != WorldState.Flashback)
		{
			this.playerCharacter.SetActive(!BabyPlayerToggle.isBabyMode);
			this.babyCharacter.SetActive(BabyPlayerToggle.isBabyMode);
		}
	}

	// Token: 0x04000191 RID: 401
	private static bool isBabyMode;

	// Token: 0x04000192 RID: 402
	private static BabyPlayerToggle b;

	// Token: 0x04000193 RID: 403
	public GameObject playerCharacter;

	// Token: 0x04000194 RID: 404
	public GameObject babyCharacter;
}
