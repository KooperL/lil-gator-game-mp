using System;
using UnityEngine;
using UnityEngine.UI;

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

	public static MoneyManager m;

	public Text text;

	public UINumberToSprite uiDisplay;

	public GameObject dollarPrefab;

	public GameObject dimePrefab;

	public GameObject pennyPrefab;

	[Space]
	public AudioClip collectSound;

	public float volume = 0.5f;

	public float maxPitch = 1.2f;

	public float minPitch = 0.8f;
}
