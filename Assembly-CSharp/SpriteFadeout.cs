using System;
using UnityEngine;

public class SpriteFadeout : MonoBehaviour
{
	// Token: 0x06000CA8 RID: 3240 RVA: 0x0003D605 File Offset: 0x0003B805
	private void Awake()
	{
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
	}

	// Token: 0x06000CA9 RID: 3241 RVA: 0x0003D613 File Offset: 0x0003B813
	private void Start()
	{
		this.color = this.spriteRenderer.color;
		this.alpha = this.color.a;
	}

	// Token: 0x06000CAA RID: 3242 RVA: 0x0003D638 File Offset: 0x0003B838
	private void Update()
	{
		this.t += Time.deltaTime;
		if (this.t >= this.fadeTime)
		{
			Object.Destroy(base.gameObject);
		}
		this.color.a = this.alpha * (this.fadeTime - this.t) / this.fadeTime;
		this.spriteRenderer.color = this.color;
	}

	private SpriteRenderer spriteRenderer;

	public float fadeTime = 1f;

	private float t;

	private Color color;

	private float alpha;
}
