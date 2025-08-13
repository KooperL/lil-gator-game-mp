using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000017 RID: 23
public class FadeOverTime : MonoBehaviour
{
	// Token: 0x0600004A RID: 74 RVA: 0x00017E84 File Offset: 0x00016084
	public void OnEnable()
	{
		this.fade = this.initialFadeLevel;
		this.color = this.image.color;
		this.color.a = this.fade;
		this.image.color = this.color;
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00017ED0 File Offset: 0x000160D0
	private void Update()
	{
		this.fade = Mathf.MoveTowards(this.fade, this.targetFadeLevel, Time.deltaTime * this.fadeSpeed);
		this.color.a = this.fade;
		this.image.color = this.color;
	}

	// Token: 0x04000063 RID: 99
	public float fadeSpeed = 0.2f;

	// Token: 0x04000064 RID: 100
	public float initialFadeLevel = 1f;

	// Token: 0x04000065 RID: 101
	public float targetFadeLevel = 0.5f;

	// Token: 0x04000066 RID: 102
	private float fade;

	// Token: 0x04000067 RID: 103
	public Image image;

	// Token: 0x04000068 RID: 104
	private Color color;
}
