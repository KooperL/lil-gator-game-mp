using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000244 RID: 580
public class SpriteAnimation : MonoBehaviour
{
	// Token: 0x06000CA3 RID: 3235 RVA: 0x0003D4DE File Offset: 0x0003B6DE
	private void OnValidate()
	{
		if (this.spriteRenderer == null)
		{
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		}
	}

	// Token: 0x06000CA4 RID: 3236 RVA: 0x0003D4FA File Offset: 0x0003B6FA
	private void OnEnable()
	{
		this.nextFrameTime = Time.time + 1f / this.fps;
		if (this.resetIndexOnEnable && this.index != 0)
		{
			this.index = 0;
			this.SetSpriteIndex(this.index);
		}
	}

	// Token: 0x06000CA5 RID: 3237 RVA: 0x0003D538 File Offset: 0x0003B738
	private void Update()
	{
		if (Time.time >= this.nextFrameTime)
		{
			this.index++;
			if (this.index >= this.sprites.Length)
			{
				this.index = 0;
			}
			this.SetSpriteIndex(this.index);
			this.nextFrameTime += 1f / this.fps;
		}
	}

	// Token: 0x06000CA6 RID: 3238 RVA: 0x0003D59C File Offset: 0x0003B79C
	private void SetSpriteIndex(int index)
	{
		if (this.spriteRenderer != null)
		{
			this.spriteRenderer.sprite = this.sprites[index];
		}
		if (this.image != null)
		{
			this.image.sprite = this.sprites[index];
		}
	}

	// Token: 0x040010B7 RID: 4279
	public SpriteRenderer spriteRenderer;

	// Token: 0x040010B8 RID: 4280
	public Image image;

	// Token: 0x040010B9 RID: 4281
	public Sprite[] sprites;

	// Token: 0x040010BA RID: 4282
	private int index;

	// Token: 0x040010BB RID: 4283
	public float fps = 12f;

	// Token: 0x040010BC RID: 4284
	private float nextFrameTime;

	// Token: 0x040010BD RID: 4285
	public bool resetIndexOnEnable = true;
}
