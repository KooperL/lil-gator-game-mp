using System;
using UnityEngine;
using UnityEngine.UI;

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

	public SpriteRenderer spriteRenderer;

	public Image image;

	public Sprite[] sprites;

	private int index;

	public float fps = 12f;

	private float nextFrameTime;

	public bool resetIndexOnEnable = true;
}
