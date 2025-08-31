using System;
using UnityEngine;
using UnityEngine.UI;

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

	public Image image;

	public Sprite[] sprites;

	public float fps = 15f;

	private float nextFrameTime = -1f;

	private int index;
}
