using System;
using UnityEngine;

[AddComponentMenu("Dialogue Sequence/Phone Image")]
public class DSDialoguePhoneImage : DialogueSequence
{
	// Token: 0x060005B9 RID: 1465 RVA: 0x000061AC File Offset: 0x000043AC
	private void OnValidate()
	{
		if (this.phoneDisplay == null)
		{
			this.phoneDisplay = global::UnityEngine.Object.FindObjectOfType<UIScrollingPhoneDisplay>();
		}
	}

	// Token: 0x060005BA RID: 1466 RVA: 0x000061C7 File Offset: 0x000043C7
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
