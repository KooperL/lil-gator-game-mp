using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001F4 RID: 500
public class MoneyManager : MonoBehaviour
{
	// Token: 0x06000940 RID: 2368 RVA: 0x00009047 File Offset: 0x00007247
	private static void Split(int cents, out int dollars, out int dimes, out int pennies)
	{
		dollars = Mathf.FloorToInt((float)cents / 100f);
		cents -= dollars * 100;
		dimes = Mathf.FloorToInt((float)cents / 10f);
		cents -= dimes * 10;
		pennies = cents;
	}

	// Token: 0x170000E2 RID: 226
	// (get) Token: 0x06000941 RID: 2369 RVA: 0x0000907C File Offset: 0x0000727C
	// (set) Token: 0x06000942 RID: 2370 RVA: 0x0000908E File Offset: 0x0000728E
	public int CollectedCents
	{
		get
		{
			return GameData.g.ReadInt("Money", 0);
		}
		set
		{
			GameData.g.Write("Money", value);
			this.uiDisplay.SetNumber(value);
		}
	}

	// Token: 0x06000943 RID: 2371 RVA: 0x000090AC File Offset: 0x000072AC
	private void OnEnable()
	{
		MoneyManager.m = this;
	}

	// Token: 0x04000BED RID: 3053
	public static MoneyManager m;

	// Token: 0x04000BEE RID: 3054
	public Text text;

	// Token: 0x04000BEF RID: 3055
	public UINumberToSprite uiDisplay;

	// Token: 0x04000BF0 RID: 3056
	public GameObject dollarPrefab;

	// Token: 0x04000BF1 RID: 3057
	public GameObject dimePrefab;

	// Token: 0x04000BF2 RID: 3058
	public GameObject pennyPrefab;

	// Token: 0x04000BF3 RID: 3059
	[Space]
	public AudioClip collectSound;

	// Token: 0x04000BF4 RID: 3060
	public float volume = 0.5f;

	// Token: 0x04000BF5 RID: 3061
	public float maxPitch = 1.2f;

	// Token: 0x04000BF6 RID: 3062
	public float minPitch = 0.8f;
}
