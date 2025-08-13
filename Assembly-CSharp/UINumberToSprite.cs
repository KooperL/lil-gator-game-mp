using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003BC RID: 956
public class UINumberToSprite : MonoBehaviour
{
	// Token: 0x06001249 RID: 4681 RVA: 0x0005AB68 File Offset: 0x00058D68
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

	// Token: 0x0600124A RID: 4682 RVA: 0x0005ABE0 File Offset: 0x00058DE0
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

	// Token: 0x040017A8 RID: 6056
	public Image unitImage;

	// Token: 0x040017A9 RID: 6057
	public Sprite[] numbers;

	// Token: 0x040017AA RID: 6058
	public Image zerosImage;

	// Token: 0x040017AB RID: 6059
	public Image tensImage;

	// Token: 0x040017AC RID: 6060
	public Image hundredsImage;

	// Token: 0x040017AD RID: 6061
	public bool hideIfZero = true;

	// Token: 0x040017AE RID: 6062
	public bool hideAtStart = true;
}
