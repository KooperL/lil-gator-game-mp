using System;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
	// Token: 0x06000981 RID: 2433 RVA: 0x00009378 File Offset: 0x00007578
	private static void Split(int cents, out int dollars, out int dimes, out int pennies)
	{
		dollars = Mathf.FloorToInt((float)cents / 100f);
		cents -= dollars * 100;
		dimes = Mathf.FloorToInt((float)cents / 10f);
		cents -= dimes * 10;
		pennies = cents;
	}

	// (get) Token: 0x06000982 RID: 2434 RVA: 0x000093AD File Offset: 0x000075AD
	// (set) Token: 0x06000983 RID: 2435 RVA: 0x000093BF File Offset: 0x000075BF
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

	// Token: 0x06000984 RID: 2436 RVA: 0x000093DD File Offset: 0x000075DD
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
