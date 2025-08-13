using System;
using UnityEngine;

// Token: 0x020000D9 RID: 217
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

	// Token: 0x0400065D RID: 1629
	public Sprite image;

	// Token: 0x0400065E RID: 1630
	public CharacterProfile character;

	// Token: 0x0400065F RID: 1631
	public bool displayNames = true;

	// Token: 0x04000660 RID: 1632
	public bool clearPhoneAfter = true;

	// Token: 0x04000661 RID: 1633
	public UIScrollingPhoneDisplay phoneDisplay;
}
