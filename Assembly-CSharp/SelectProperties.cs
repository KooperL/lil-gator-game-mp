using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020002B0 RID: 688
public class SelectProperties : MonoBehaviour, ISelectHandler, IEventSystemHandler, IDeselectHandler
{
	// Token: 0x06000E7E RID: 3710 RVA: 0x0004532D File Offset: 0x0004352D
	private void OnEnable()
	{
		if (EventSystem.current.currentSelectedGameObject == base.gameObject)
		{
			this.SetSelected(true);
		}
	}

	// Token: 0x06000E7F RID: 3711 RVA: 0x0004534D File Offset: 0x0004354D
	private void OnDisable()
	{
		if (this == null)
		{
			return;
		}
		this.SetSelected(false);
	}

	// Token: 0x06000E80 RID: 3712 RVA: 0x00045360 File Offset: 0x00043560
	public void OnSelect(BaseEventData eventData)
	{
		this.SetSelected(true);
	}

	// Token: 0x06000E81 RID: 3713 RVA: 0x00045369 File Offset: 0x00043569
	public void OnDeselect(BaseEventData eventData)
	{
		this.SetSelected(false);
	}

	// Token: 0x06000E82 RID: 3714 RVA: 0x00045374 File Offset: 0x00043574
	private void SetSelected(bool isSelected)
	{
		if (this.modifyText)
		{
			this.text.color = (isSelected ? this.textSelectedColor : this.textUnselectedColor);
		}
		if (this.modifyMiscStuff)
		{
			Text[] array = this.modifiedText;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].color = (isSelected ? this.modifiedSelectedColor : this.modifiedUnselectedColor);
			}
			Image[] array2 = this.modifiedImages;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].color = (isSelected ? this.modifiedSelectedColor : this.modifiedUnselectedColor);
			}
		}
		if (this.modifyMiscStuff2)
		{
			Text[] array = this.modifiedText2;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].color = (isSelected ? this.modifiedSelectedColor2 : this.modifiedUnselectedColor2);
			}
			Image[] array2 = this.modifiedImages2;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].color = (isSelected ? this.modifiedSelectedColor2 : this.modifiedUnselectedColor2);
			}
		}
		if (this.randomizeSelectedRotation)
		{
			base.transform.rotation = Quaternion.Euler(0f, 0f, isSelected ? Random.Range(-this.randomizedAngle, this.randomizedAngle) : 0f);
		}
		if (this.randomizeGraphic && (this.randomizeGraphicOnlySelected || isSelected))
		{
			this.randomizedGraphic.sprite = this.randomizedGraphicVariants.RandomValue<Sprite>();
		}
	}

	// Token: 0x040012D2 RID: 4818
	public bool randomizeSelectedRotation;

	// Token: 0x040012D3 RID: 4819
	[ConditionalHide("randomizeSelectedRotation", true)]
	public float randomizedAngle = 5f;

	// Token: 0x040012D4 RID: 4820
	[Space]
	public bool modifyText;

	// Token: 0x040012D5 RID: 4821
	[ConditionalHide("modifyText", true)]
	public Text text;

	// Token: 0x040012D6 RID: 4822
	[ConditionalHide("modifyText", true)]
	public Color textUnselectedColor;

	// Token: 0x040012D7 RID: 4823
	[ConditionalHide("modifyText", true)]
	public Color textSelectedColor;

	// Token: 0x040012D8 RID: 4824
	[Space]
	public bool modifyMiscStuff;

	// Token: 0x040012D9 RID: 4825
	[ConditionalHide("modifyMiscStuff", true)]
	public Image[] modifiedImages;

	// Token: 0x040012DA RID: 4826
	[ConditionalHide("modifyMiscStuff", true)]
	public Text[] modifiedText;

	// Token: 0x040012DB RID: 4827
	[ConditionalHide("modifyMiscStuff", true)]
	public Color modifiedUnselectedColor;

	// Token: 0x040012DC RID: 4828
	[ConditionalHide("modifyMiscStuff", true)]
	public Color modifiedSelectedColor;

	// Token: 0x040012DD RID: 4829
	[Space]
	public bool modifyMiscStuff2;

	// Token: 0x040012DE RID: 4830
	[ConditionalHide("modifyMiscStuff2", true)]
	public Image[] modifiedImages2;

	// Token: 0x040012DF RID: 4831
	[ConditionalHide("modifyMiscStuff2", true)]
	public Text[] modifiedText2;

	// Token: 0x040012E0 RID: 4832
	[ConditionalHide("modifyMiscStuff2", true)]
	public Color modifiedUnselectedColor2;

	// Token: 0x040012E1 RID: 4833
	[ConditionalHide("modifyMiscStuff", true)]
	public Color modifiedSelectedColor2;

	// Token: 0x040012E2 RID: 4834
	[Space]
	public bool randomizeGraphic;

	// Token: 0x040012E3 RID: 4835
	[ConditionalHide("randomizeGraphic", true)]
	public bool randomizeGraphicOnlySelected = true;

	// Token: 0x040012E4 RID: 4836
	[ConditionalHide("randomizeGraphic", true)]
	public Image randomizedGraphic;

	// Token: 0x040012E5 RID: 4837
	[ConditionalHide("randomizeGraphic", true)]
	public Sprite[] randomizedGraphicVariants;
}
