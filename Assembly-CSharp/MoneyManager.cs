using System;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
	// Token: 0x06000981 RID: 2433 RVA: 0x00009363 File Offset: 0x00007563
	private static void Split(int cents, out int dollars, out int dimes, out int pennies)
	{
		dollars = Mathf.FloorToInt((float)cents / 100f);
		cents -= dollars * 100;
		dimes = Mathf.FloorToInt((float)cents / 10f);
		cents -= dimes * 10;
		pennies = cents;
	}

	// (get) Token: 0x06000982 RID: 2434 RVA: 0x00009398 File Offset: 0x00007598
	// (set) Token: 0x06000983 RID: 2435 RVA: 0x000093AA File Offset: 0x000075AA
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

	// Token: 0x06000984 RID: 2436 RVA: 0x000093C8 File Offset: 0x000075C8
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
