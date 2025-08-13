using System;
using UnityEngine;

// Token: 0x02000125 RID: 293
[AddComponentMenu("Dialogue Sequence/Phone Image")]
public class DSDialoguePhoneImage : DialogueSequence
{
	// Token: 0x0600057F RID: 1407 RVA: 0x00005EE6 File Offset: 0x000040E6
	private void OnValidate()
	{
		if (this.phoneDisplay == null)
		{
			this.phoneDisplay = Object.FindObjectOfType<UIScrollingPhoneDisplay>();
		}
	}

	// Token: 0x06000580 RID: 1408 RVA: 0x00005F01 File Offset: 0x00004101
	public override YieldInstruction Run()
	{
		return CoroutineUtil.Start(this.phoneDisplay.DisplayImage(this.image, this.character, this.displayNames, this.clearPhoneAfter));
	}

	// Token: 0x0400078E RID: 1934
	public Sprite image;

	// Token: 0x0400078F RID: 1935
	public CharacterProfile character;

	// Token: 0x04000790 RID: 1936
	public bool displayNames = true;

	// Token: 0x04000791 RID: 1937
	public bool clearPhoneAfter = true;

	// Token: 0x04000792 RID: 1938
	public UIScrollingPhoneDisplay phoneDisplay;
}
