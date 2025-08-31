using System;
using UnityEngine;
using UnityEngine.UI;

public class RandomizeImage : MonoBehaviour
{
	// Token: 0x06000E0C RID: 3596 RVA: 0x00043DB0 File Offset: 0x00041FB0
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
