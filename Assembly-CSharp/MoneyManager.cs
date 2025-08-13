using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200017E RID: 382
public class MoneyManager : MonoBehaviour
{
	// Token: 0x060007DE RID: 2014 RVA: 0x00026468 File Offset: 0x00024668
	private static void Split(int cents, out int dollars, out int dimes, out int pennies)
	{
		dollars = Mathf.FloorToInt((float)cents / 100f);
		cents -= dollars * 100;
		dimes = Mathf.FloorToInt((float)cents / 10f);
		cents -= dimes * 10;
		pennies = cents;
	}

	// Token: 0x1700006E RID: 110
	// (get) Token: 0x060007DF RID: 2015 RVA: 0x0002649D File Offset: 0x0002469D
	// (set) Token: 0x060007E0 RID: 2016 RVA: 0x000264AF File Offset: 0x000246AF
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

	// Token: 0x060007E1 RID: 2017 RVA: 0x000264CD File Offset: 0x000246CD
	private void OnEnable()
	{
		MoneyManager.m = this;
	}

	// Token: 0x04000A11 RID: 2577
	public static MoneyManager m;

	// Token: 0x04000A12 RID: 2578
	public Text text;

	// Token: 0x04000A13 RID: 2579
	public UINumberToSprite uiDisplay;

	// Token: 0x04000A14 RID: 2580
	public GameObject dollarPrefab;

	// Token: 0x04000A15 RID: 2581
	public GameObject dimePrefab;

	// Token: 0x04000A16 RID: 2582
	public GameObject pennyPrefab;

	// Token: 0x04000A17 RID: 2583
	[Space]
	public AudioClip collectSound;

	// Token: 0x04000A18 RID: 2584
	public float volume = 0.5f;

	// Token: 0x04000A19 RID: 2585
	public float maxPitch = 1.2f;

	// Token: 0x04000A1A RID: 2586
	public float minPitch = 0.8f;
}
