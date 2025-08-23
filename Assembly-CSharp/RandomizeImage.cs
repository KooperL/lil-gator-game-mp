using System;
using UnityEngine;
using UnityEngine.UI;

public class RandomizeImage : MonoBehaviour
{
	// Token: 0x06001131 RID: 4401 RVA: 0x00057E9C File Offset: 0x0005609C
	public void Start()
	{
		Sprite sprite = this.randomSprites.RandomValue<Sprite>();
		Image[] array = this.images;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].sprite = sprite;
		}
	}

	public Image[] images;

	public Sprite[] randomSprites;
}
