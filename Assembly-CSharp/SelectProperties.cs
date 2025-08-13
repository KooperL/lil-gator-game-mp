using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200038D RID: 909
public class SelectProperties : MonoBehaviour, ISelectHandler, IEventSystemHandler, IDeselectHandler
{
	// Token: 0x0600114E RID: 4430 RVA: 0x0000ED3B File Offset: 0x0000CF3B
	private void OnEnable()
	{
		if (EventSystem.current.currentSelectedGameObject == base.gameObject)
		{
			this.SetSelected(true);
		}
	}

	// Token: 0x0600114F RID: 4431 RVA: 0x0000ED5B File Offset: 0x0000CF5B
	private void OnDisable()
	{
		if (this == null)
		{
			return;
		}
		this.SetSelected(false);
	}

	// Token: 0x06001150 RID: 4432 RVA: 0x0000ED6E File Offset: 0x0000CF6E
	public void OnSelect(BaseEventData eventData)
	{
		this.SetSelected(true);
	}

	// Token: 0x06001151 RID: 4433 RVA: 0x0000ED77 File Offset: 0x0000CF77
	public void OnDeselect(BaseEventData eventData)
	{
		this.SetSelected(false);
	}

	// Token: 0x06001152 RID: 4434 RVA: 0x00056B50 File Offset: 0x00054D50
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

	// Token: 0x0400163A RID: 5690
	public bool randomizeSelectedRotation;

	// Token: 0x0400163B RID: 5691
	[ConditionalHide("randomizeSelectedRotation", true)]
	public float randomizedAngle = 5f;

	// Token: 0x0400163C RID: 5692
	[Space]
	public bool modifyText;

	// Token: 0x0400163D RID: 5693
	[ConditionalHide("modifyText", true)]
	public Text text;

	// Token: 0x0400163E RID: 5694
	[ConditionalHide("modifyText", true)]
	public Color textUnselectedColor;

	// Token: 0x0400163F RID: 5695
	[ConditionalHide("modifyText", true)]
	public Color textSelectedColor;

	// Token: 0x04001640 RID: 5696
	[Space]
	public bool modifyMiscStuff;

	// Token: 0x04001641 RID: 5697
	[ConditionalHide("modifyMiscStuff", true)]
	public Image[] modifiedImages;

	// Token: 0x04001642 RID: 5698
	[ConditionalHide("modifyMiscStuff", true)]
	public Text[] modifiedText;

	// Token: 0x04001643 RID: 5699
	[ConditionalHide("modifyMiscStuff", true)]
	public Color modifiedUnselectedColor;

	// Token: 0x04001644 RID: 5700
	[ConditionalHide("modifyMiscStuff", true)]
	public Color modifiedSelectedColor;

	// Token: 0x04001645 RID: 5701
	[Space]
	public bool modifyMiscStuff2;

	// Token: 0x04001646 RID: 5702
	[ConditionalHide("modifyMiscStuff2", true)]
	public Image[] modifiedImages2;

	// Token: 0x04001647 RID: 5703
	[ConditionalHide("modifyMiscStuff2", true)]
	public Text[] modifiedText2;

	// Token: 0x04001648 RID: 5704
	[ConditionalHide("modifyMiscStuff2", true)]
	public Color modifiedUnselectedColor2;

	// Token: 0x04001649 RID: 5705
	[ConditionalHide("modifyMiscStuff", true)]
	public Color modifiedSelectedColor2;

	// Token: 0x0400164A RID: 5706
	[Space]
	public bool randomizeGraphic;

	// Token: 0x0400164B RID: 5707
	[ConditionalHide("randomizeGraphic", true)]
	public bool randomizeGraphicOnlySelected = true;

	// Token: 0x0400164C RID: 5708
	[ConditionalHide("randomizeGraphic", true)]
	public Image randomizedGraphic;

	// Token: 0x0400164D RID: 5709
	[ConditionalHide("randomizeGraphic", true)]
	public Sprite[] randomizedGraphicVariants;
}
