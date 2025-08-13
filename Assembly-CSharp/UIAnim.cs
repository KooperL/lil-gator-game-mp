using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002B2 RID: 690
public class UIAnim : MonoBehaviour
{
	// Token: 0x06000E8A RID: 3722 RVA: 0x0004566A File Offset: 0x0004386A
	private void OnValidate()
	{
		if (this.image == null)
		{
			this.image = base.GetComponent<Image>();
		}
	}

	// Token: 0x06000E8B RID: 3723 RVA: 0x00045686 File Offset: 0x00043886
	private void OnEnable()
	{
		this.index = 0;
		this.nextFrameTime = Time.time + 1f / this.fps;
	}

	// Token: 0x06000E8C RID: 3724 RVA: 0x000456A8 File Offset: 0x000438A8
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

	// Token: 0x040012EB RID: 4843
	public Image image;

	// Token: 0x040012EC RID: 4844
	public Sprite[] sprites;

	// Token: 0x040012ED RID: 4845
	public float fps = 15f;

	// Token: 0x040012EE RID: 4846
	private float nextFrameTime = -1f;

	// Token: 0x040012EF RID: 4847
	private int index;
}
