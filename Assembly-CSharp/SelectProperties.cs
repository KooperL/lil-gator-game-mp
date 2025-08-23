using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectProperties : MonoBehaviour, ISelectHandler, IEventSystemHandler, IDeselectHandler
{
	// Token: 0x060011AF RID: 4527 RVA: 0x0000F12E File Offset: 0x0000D32E
	private void OnEnable()
	{
		if (EventSystem.current.currentSelectedGameObject == base.gameObject)
		{
			this.SetSelected(true);
		}
	}

	// Token: 0x060011B0 RID: 4528 RVA: 0x0000F14E File Offset: 0x0000D34E
	private void OnDisable()
	{
		if (this == null)
		{
			return;
		}
		this.SetSelected(false);
	}

	// Token: 0x060011B1 RID: 4529 RVA: 0x0000F161 File Offset: 0x0000D361
	public void OnSelect(BaseEventData eventData)
	{
		this.SetSelected(true);
	}

	// Token: 0x060011B2 RID: 4530 RVA: 0x0000F16A File Offset: 0x0000D36A
	public void OnDeselect(BaseEventData eventData)
	{
		this.SetSelected(false);
	}

	// Token: 0x060011B3 RID: 4531 RVA: 0x00058DD8 File Offset: 0x00056FD8
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
			base.transform.rotation = Quaternion.Euler(0f, 0f, isSelected ? global::UnityEngine.Random.Range(-this.randomizedAngle, this.randomizedAngle) : 0f);
		}
		if (this.randomizeGraphic && (this.randomizeGraphicOnlySelected || isSelected))
		{
			this.randomizedGraphic.sprite = this.randomizedGraphicVariants.RandomValue<Sprite>();
		}
	}

	public bool randomizeSelectedRotation;

	[ConditionalHide("randomizeSelectedRotation", true)]
	public float randomizedAngle = 5f;

	[Space]
	public bool modifyText;

	[ConditionalHide("modifyText", true)]
	public Text text;

	[ConditionalHide("modifyText", true)]
	public Color textUnselectedColor;

	[ConditionalHide("modifyText", true)]
	public Color textSelectedColor;

	[Space]
	public bool modifyMiscStuff;

	[ConditionalHide("modifyMiscStuff", true)]
	public Image[] modifiedImages;

	[ConditionalHide("modifyMiscStuff", true)]
	public Text[] modifiedText;

	[ConditionalHide("modifyMiscStuff", true)]
	public Color modifiedUnselectedColor;

	[ConditionalHide("modifyMiscStuff", true)]
	public Color modifiedSelectedColor;

	[Space]
	public bool modifyMiscStuff2;

	[ConditionalHide("modifyMiscStuff2", true)]
	public Image[] modifiedImages2;

	[ConditionalHide("modifyMiscStuff2", true)]
	public Text[] modifiedText2;

	[ConditionalHide("modifyMiscStuff2", true)]
	public Color modifiedUnselectedColor2;

	[ConditionalHide("modifyMiscStuff", true)]
	public Color modifiedSelectedColor2;

	[Space]
	public bool randomizeGraphic;

	[ConditionalHide("randomizeGraphic", true)]
	public bool randomizeGraphicOnlySelected = true;

	[ConditionalHide("randomizeGraphic", true)]
	public Image randomizedGraphic;

	[ConditionalHide("randomizeGraphic", true)]
	public Sprite[] randomizedGraphicVariants;
}
