using System;
using UnityEngine;

// Token: 0x02000309 RID: 777
public class SpriteFadeout : MonoBehaviour
{
	// Token: 0x06000F54 RID: 3924 RVA: 0x0000D587 File Offset: 0x0000B787
	private void Awake()
	{
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
	}

	// Token: 0x06000F55 RID: 3925 RVA: 0x0000D595 File Offset: 0x0000B795
	private void Start()
	{
		this.color = this.spriteRenderer.color;
		this.alpha = this.color.a;
	}

	// Token: 0x06000F56 RID: 3926 RVA: 0x00050478 File Offset: 0x0004E678
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

	// Token: 0x040013D4 RID: 5076
	private SpriteRenderer spriteRenderer;

	// Token: 0x040013D5 RID: 5077
	public float fadeTime = 1f;

	// Token: 0x040013D6 RID: 5078
	private float t;

	// Token: 0x040013D7 RID: 5079
	private Color color;

	// Token: 0x040013D8 RID: 5080
	private float alpha;
}
