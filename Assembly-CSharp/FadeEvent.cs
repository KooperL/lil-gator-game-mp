using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class FadeEvent : MonoBehaviour
{
	// Token: 0x06000576 RID: 1398 RVA: 0x0001CED3 File Offset: 0x0001B0D3
	private void OnEnable()
	{
		if (this.triggerOnEnable)
		{
			this.StartFade();
		}
	}

	// Token: 0x06000577 RID: 1399 RVA: 0x0001CEE3 File Offset: 0x0001B0E3
	private void StartFade()
	{
		CoroutineUtil.Start(this.DoFade());
	}

	// Token: 0x06000578 RID: 1400 RVA: 0x0001CEF1 File Offset: 0x0001B0F1
	private IEnumerator DoFade()
	{
		yield return Blackout.FadeIn();
		this.onFade.Invoke();
		Blackout.FadeOut();
		yield break;
	}

	public bool triggerOnEnable = true;

	public UnityEvent onFade;
}
