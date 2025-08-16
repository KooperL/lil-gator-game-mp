using System;
using UnityEngine;

public class BabyPlayerToggle : MonoBehaviour
{
	// Token: 0x0600011E RID: 286 RVA: 0x00002ECE File Offset: 0x000010CE
	public static void SetBabyMode(bool isBabyMode)
	{
		BabyPlayerToggle.isBabyMode = isBabyMode;
		if (BabyPlayerToggle.b != null)
		{
			BabyPlayerToggle.b.UpdateBabyMode();
		}
	}

	// Token: 0x0600011F RID: 287 RVA: 0x00002EED File Offset: 0x000010ED
	private void Start()
	{
		BabyPlayerToggle.b = this;
		this.UpdateBabyMode();
	}

	// Token: 0x06000120 RID: 288 RVA: 0x00002EFB File Offset: 0x000010FB
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
