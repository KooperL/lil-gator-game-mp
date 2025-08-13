using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000019 RID: 25
public class FadeOverTime : MonoBehaviour
{
	// Token: 0x06000051 RID: 81 RVA: 0x0000345C File Offset: 0x0000165C
	public void OnEnable()
	{
		this.fade = this.initialFadeLevel;
		this.color = this.image.color;
		this.color.a = this.fade;
		this.image.color = this.color;
	}

	// Token: 0x06000052 RID: 82 RVA: 0x000034A8 File Offset: 0x000016A8
	private void Update()
	{
		this.fade = Mathf.MoveTowards(this.fade, this.targetFadeLevel, Time.deltaTime * this.fadeSpeed);
		this.color.a = this.fade;
		this.image.color = this.color;
	}

	// Token: 0x04000079 RID: 121
	public float fadeSpeed = 0.2f;

	// Token: 0x0400007A RID: 122
	public float initialFadeLevel = 1f;

	// Token: 0x0400007B RID: 123
	public float targetFadeLevel = 0.5f;

	// Token: 0x0400007C RID: 124
	private float fade;

	// Token: 0x0400007D RID: 125
	public Image image;

	// Token: 0x0400007E RID: 126
	private Color color;
}
