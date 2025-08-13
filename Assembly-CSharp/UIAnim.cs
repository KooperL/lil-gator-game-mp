using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200038F RID: 911
public class UIAnim : MonoBehaviour
{
	// Token: 0x0600115A RID: 4442 RVA: 0x0000EDB3 File Offset: 0x0000CFB3
	private void OnValidate()
	{
		if (this.image == null)
		{
			this.image = base.GetComponent<Image>();
		}
	}

	// Token: 0x0600115B RID: 4443 RVA: 0x0000EDCF File Offset: 0x0000CFCF
	private void OnEnable()
	{
		this.index = 0;
		this.nextFrameTime = Time.time + 1f / this.fps;
	}

	// Token: 0x0600115C RID: 4444 RVA: 0x00056E0C File Offset: 0x0005500C
	private void FixedUpdate()
	{
		if (Time.time > this.nextFrameTime)
		{
			this.nextFrameTime += 1f / this.fps;
			this.index++;
			if (this.index >= this.sprites.Length)
			{
				this.index = 0;
			}
			this.image.sprite = this.sprites[this.index];
		}
	}

	// Token: 0x04001653 RID: 5715
	public Image image;

	// Token: 0x04001654 RID: 5716
	public Sprite[] sprites;

	// Token: 0x04001655 RID: 5717
	public float fps = 15f;

	// Token: 0x04001656 RID: 5718
	private float nextFrameTime = -1f;

	// Token: 0x04001657 RID: 5719
	private int index;
}
