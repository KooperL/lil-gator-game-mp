using System;
using UnityEngine;

// Token: 0x020000DF RID: 223
[AddComponentMenu("Dialogue Sequence/Picture Display")]
public class DSPictureDisplay : DialogueSequence
{
	// Token: 0x060004A1 RID: 1185 RVA: 0x00019B72 File Offset: 0x00017D72
	private void OnValidate()
	{
		if (this.pictureDisplay == null)
		{
			this.pictureDisplay = Object.FindObjectOfType<UIPictureDisplay>(true);
		}
	}

	// Token: 0x060004A2 RID: 1186 RVA: 0x00019B8E File Offset: 0x00017D8E
	public override YieldInstruction Run()
	{
		this.pictureDisplay.pictureImage.sprite = this.picture;
		return base.StartCoroutine(this.pictureDisplay.RunDisplay());
	}

	// Token: 0x04000675 RID: 1653
	[ReadOnly]
	public UIPictureDisplay pictureDisplay;

	// Token: 0x04000676 RID: 1654
	public Sprite picture;
}
