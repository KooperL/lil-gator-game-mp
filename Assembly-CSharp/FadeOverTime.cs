using System;
using UnityEngine;
using UnityEngine.UI;

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

	public float fadeSpeed = 0.2f;

	public float initialFadeLevel = 1f;

	public float targetFadeLevel = 0.5f;

	private float fade;

	public Image image;

	private Color color;
}
