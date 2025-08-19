using System;
using UnityEngine;
using UnityEngine.UI;

public class UINumberToSprite : MonoBehaviour
{
	// Token: 0x060012A9 RID: 4777 RVA: 0x0005CAF4 File Offset: 0x0005ACF4
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

	// Token: 0x060012AA RID: 4778 RVA: 0x0005CB6C File Offset: 0x0005AD6C
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

	public Image unitImage;

	public Sprite[] numbers;

	public Image zerosImage;

	public Image tensImage;

	public Image hundredsImage;

	public bool hideIfZero = true;

	public bool hideAtStart = true;
}
