using System;
using UnityEngine;

public class SpriteFadeout : MonoBehaviour
{
	// Token: 0x06000FB1 RID: 4017 RVA: 0x0000D939 File Offset: 0x0000BB39
	private void Awake()
	{
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
	}

	// Token: 0x06000FB2 RID: 4018 RVA: 0x0000D947 File Offset: 0x0000BB47
	private void Start()
	{
		this.color = this.spriteRenderer.color;
		this.alpha = this.color.a;
	}

	// Token: 0x06000FB3 RID: 4019 RVA: 0x00052664 File Offset: 0x00050864
	private void Update()
	{
		this.t += Time.deltaTime;
		if (this.t >= this.fadeTime)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
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
