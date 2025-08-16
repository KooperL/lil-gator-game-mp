using System;
using UnityEngine;

[AddComponentMenu("Dialogue Sequence/Picture Display")]
public class DSPictureDisplay : DialogueSequence
{
	// Token: 0x060005D8 RID: 1496 RVA: 0x00006320 File Offset: 0x00004520
	private void OnValidate()
	{
		if (this.pictureDisplay == null)
		{
			this.pictureDisplay = global::UnityEngine.Object.FindObjectOfType<UIPictureDisplay>(true);
		}
	}

	// Token: 0x060005D9 RID: 1497 RVA: 0x0000633C File Offset: 0x0000453C
	public override YieldInstruction Run()
	{
		this.pictureDisplay.pictureImage.sprite = this.picture;
		return base.StartCoroutine(this.pictureDisplay.RunDisplay());
	}

	[ReadOnly]
	public UIPictureDisplay pictureDisplay;

	public Sprite picture;
}
