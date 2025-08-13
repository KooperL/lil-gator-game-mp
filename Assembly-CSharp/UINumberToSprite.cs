using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002D3 RID: 723
public class UINumberToSprite : MonoBehaviour
{
	// Token: 0x06000F47 RID: 3911 RVA: 0x0004984C File Offset: 0x00047A4C
	private void Start()
	{
		if (this.hideAtStart)
		{
			if (this.unitImage != null)
			{
				this.unitImage.enabled = false;
			}
			this.zerosImage.enabled = false;
			this.tensImage.enabled = false;
			this.hundredsImage.enabled = false;
			return;
		}
		if (this.unitImage != null)
		{
			this.unitImage.enabled = false;
		}
		this.SetNumber(0);
	}

	// Token: 0x06000F48 RID: 3912 RVA: 0x000498C4 File Offset: 0x00047AC4
	public void SetNumber(int number)
	{
		if (this.unitImage != null && !this.unitImage.enabled)
		{
			this.unitImage.enabled = true;
		}
		if (this.hideAtStart)
		{
			this.zerosImage.enabled = true;
			this.tensImage.enabled = true;
			this.hundredsImage.enabled = true;
		}
		if (this.hideIfZero)
		{
			this.hundredsImage.enabled = number > 99;
			this.tensImage.enabled = number > 9;
		}
		number = Mathf.Min(number, 999);
		int num = Mathf.FloorToInt((float)number / 100f);
		number -= num * 100;
		int num2 = Mathf.FloorToInt((float)number / 10f);
		number -= num2 * 10;
		int num3 = number;
		this.zerosImage.sprite = this.numbers[num3];
		this.tensImage.sprite = this.numbers[num2];
		this.hundredsImage.sprite = this.numbers[num];
	}

	// Token: 0x04001416 RID: 5142
	public Image unitImage;

	// Token: 0x04001417 RID: 5143
	public Sprite[] numbers;

	// Token: 0x04001418 RID: 5144
	public Image zerosImage;

	// Token: 0x04001419 RID: 5145
	public Image tensImage;

	// Token: 0x0400141A RID: 5146
	public Image hundredsImage;

	// Token: 0x0400141B RID: 5147
	public bool hideIfZero = true;

	// Token: 0x0400141C RID: 5148
	public bool hideAtStart = true;
}
