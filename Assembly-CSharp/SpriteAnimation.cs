using System;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAnimation : MonoBehaviour
{
	// Token: 0x06000FAC RID: 4012 RVA: 0x0000D8C6 File Offset: 0x0000BAC6
	private void OnValidate()
	{
		if (this.spriteRenderer == null)
		{
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		}
	}

	// Token: 0x06000FAD RID: 4013 RVA: 0x0000D8E2 File Offset: 0x0000BAE2
	private void OnEnable()
	{
		this.nextFrameTime = Time.time + 1f / this.fps;
		if (this.resetIndexOnEnable && this.index != 0)
		{
			this.index = 0;
			this.SetSpriteIndex(this.index);
		}
	}

	// Token: 0x06000FAE RID: 4014 RVA: 0x000525B0 File Offset: 0x000507B0
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

	// Token: 0x06000FAF RID: 4015 RVA: 0x00052614 File Offset: 0x00050814
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
