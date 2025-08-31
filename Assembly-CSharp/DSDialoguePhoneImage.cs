using System;
using UnityEngine;

[AddComponentMenu("Dialogue Sequence/Phone Image")]
public class DSDialoguePhoneImage : DialogueSequence
{
	// Token: 0x0600048E RID: 1166 RVA: 0x0001997B File Offset: 0x00017B7B
	private void OnValidate()
	{
		if (this.phoneDisplay == null)
		{
			this.phoneDisplay = Object.FindObjectOfType<UIScrollingPhoneDisplay>();
		}
	}

	// Token: 0x0600048F RID: 1167 RVA: 0x00019996 File Offset: 0x00017B96
	public override YieldInstruction Run()
	{
		return CoroutineUtil.Start(this.phoneDisplay.DisplayImage(this.image, this.character, this.displayNames, this.clearPhoneAfter));
	}

	public Sprite image;

	public CharacterProfile character;

	public bool displayNames = true;

	public bool clearPhoneAfter = true;

	public UIScrollingPhoneDisplay phoneDisplay;
}
