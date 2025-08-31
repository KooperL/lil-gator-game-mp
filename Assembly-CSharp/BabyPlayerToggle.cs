using System;
using UnityEngine;

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

	private static bool isBabyMode;

	private static BabyPlayerToggle b;

	public GameObject playerCharacter;

	public GameObject babyCharacter;
}
