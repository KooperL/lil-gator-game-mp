using System;
using UnityEngine;
using UnityEngine.UI;

public class UIAnim : MonoBehaviour
{
	// Token: 0x060011BA RID: 4538 RVA: 0x0000F187 File Offset: 0x0000D387
	private void OnValidate()
	{
		if (this.image == null)
		{
			this.image = base.GetComponent<Image>();
		}
	}

	// Token: 0x060011BB RID: 4539 RVA: 0x0000F1A3 File Offset: 0x0000D3A3
	private void OnEnable()
	{
		this.index = 0;
		this.nextFrameTime = Time.time + 1f / this.fps;
	}

	// Token: 0x060011BC RID: 4540 RVA: 0x00058C38 File Offset: 0x00056E38
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
