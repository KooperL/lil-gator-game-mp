using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000308 RID: 776
public class SpriteAnimation : MonoBehaviour
{
	// Token: 0x06000F4F RID: 3919 RVA: 0x0000D514 File Offset: 0x0000B714
	private void OnValidate()
	{
		if (this.spriteRenderer == null)
		{
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		}
	}

	// Token: 0x06000F50 RID: 3920 RVA: 0x0000D530 File Offset: 0x0000B730
	private void OnEnable()
	{
		this.nextFrameTime = Time.time + 1f / this.fps;
		if (this.resetIndexOnEnable && this.index != 0)
		{
			this.index = 0;
			this.SetSpriteIndex(this.index);
		}
	}

	// Token: 0x06000F51 RID: 3921 RVA: 0x000503C4 File Offset: 0x0004E5C4
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

	// Token: 0x06000F52 RID: 3922 RVA: 0x00050428 File Offset: 0x0004E628
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

	// Token: 0x040013CD RID: 5069
	public SpriteRenderer spriteRenderer;

	// Token: 0x040013CE RID: 5070
	public Image image;

	// Token: 0x040013CF RID: 5071
	public Sprite[] sprites;

	// Token: 0x040013D0 RID: 5072
	private int index;

	// Token: 0x040013D1 RID: 5073
	public float fps = 12f;

	// Token: 0x040013D2 RID: 5074
	private float nextFrameTime;

	// Token: 0x040013D3 RID: 5075
	public bool resetIndexOnEnable = true;
}
