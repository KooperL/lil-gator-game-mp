using System;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
	// Token: 0x06000982 RID: 2434 RVA: 0x00009382 File Offset: 0x00007582
	private static void Split(int cents, out int dollars, out int dimes, out int pennies)
	{
		dollars = Mathf.FloorToInt((float)cents / 100f);
		cents -= dollars * 100;
		dimes = Mathf.FloorToInt((float)cents / 10f);
		cents -= dimes * 10;
		pennies = cents;
	}

	// (get) Token: 0x06000983 RID: 2435 RVA: 0x000093B7 File Offset: 0x000075B7
	// (set) Token: 0x06000984 RID: 2436 RVA: 0x000093C9 File Offset: 0x000075C9
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

	// Token: 0x06000985 RID: 2437 RVA: 0x000093E7 File Offset: 0x000075E7
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
