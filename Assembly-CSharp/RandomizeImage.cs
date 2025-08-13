using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200036D RID: 877
public class RandomizeImage : MonoBehaviour
{
	// Token: 0x060010D4 RID: 4308 RVA: 0x00055C34 File Offset: 0x00053E34
	public void Start()
	{
		Sprite sprite = this.randomSprites.RandomValue<Sprite>();
		Image[] array = this.images;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].sprite = sprite;
		}
	}

	// Token: 0x040015DC RID: 5596
	public Image[] images;

	// Token: 0x040015DD RID: 5597
	public Sprite[] randomSprites;
}
