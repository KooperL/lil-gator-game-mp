using System;
using UnityEngine;

// Token: 0x0200012D RID: 301
[AddComponentMenu("Dialogue Sequence/Picture Display")]
public class DSPictureDisplay : DialogueSequence
{
	// Token: 0x0600059E RID: 1438 RVA: 0x0000605A File Offset: 0x0000425A
	private void OnValidate()
	{
		if (this.pictureDisplay == null)
		{
			this.pictureDisplay = Object.FindObjectOfType<UIPictureDisplay>(true);
		}
	}

	// Token: 0x0600059F RID: 1439 RVA: 0x00006076 File Offset: 0x00004276
	public override YieldInstruction Run()
	{
		this.pictureDisplay.pictureImage.sprite = this.picture;
		return base.StartCoroutine(this.pictureDisplay.RunDisplay());
	}

	// Token: 0x040007AC RID: 1964
	[ReadOnly]
	public UIPictureDisplay pictureDisplay;

	// Token: 0x040007AD RID: 1965
	public Sprite picture;
}
