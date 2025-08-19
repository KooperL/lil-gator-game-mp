using System;
using UnityEngine;
using UnityEngine.UI;

public class FadeOverTime : MonoBehaviour
{
	// Token: 0x06000052 RID: 82 RVA: 0x000186C0 File Offset: 0x000168C0
	public void OnEnable()
	{
		this.fade = this.initialFadeLevel;
		this.color = this.image.color;
		this.color.a = this.fade;
		this.image.color = this.color;
	}

	// Token: 0x06000053 RID: 83 RVA: 0x0001870C File Offset: 0x0001690C
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
