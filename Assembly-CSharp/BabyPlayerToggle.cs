using System;
using UnityEngine;

// Token: 0x0200003C RID: 60
public class BabyPlayerToggle : MonoBehaviour
{
	// Token: 0x060000F1 RID: 241 RVA: 0x00006711 File Offset: 0x00004911
	public static void SetBabyMode(bool isBabyMode)
	{
		BabyPlayerToggle.isBabyMode = isBabyMode;
		if (BabyPlayerToggle.b != null)
		{
			BabyPlayerToggle.b.UpdateBabyMode();
		}
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x00006730 File Offset: 0x00004930
	private void Start()
	{
		BabyPlayerToggle.b = this;
		this.UpdateBabyMode();
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x0000673E File Offset: 0x0000493E
	protected void UpdateBabyMode()
	{
		if (this != null && base.gameObject.activeInHierarchy)
		{
			this.playerCharacter.SetActive(!BabyPlayerToggle.isBabyMode);
			this.babyCharacter.SetActive(BabyPlayerToggle.isBabyMode);
		}
	}

	// Token: 0x0400014A RID: 330
	private static bool isBabyMode;

	// Token: 0x0400014B RID: 331
	private static BabyPlayerToggle b;

	// Token: 0x0400014C RID: 332
	public GameObject playerCharacter;

	// Token: 0x0400014D RID: 333
	public GameObject babyCharacter;
}
