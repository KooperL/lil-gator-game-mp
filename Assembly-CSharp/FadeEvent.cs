using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class FadeEvent : MonoBehaviour
{
	// Token: 0x060006C8 RID: 1736 RVA: 0x00006EF3 File Offset: 0x000050F3
	private void OnEnable()
	{
		if (this.triggerOnEnable)
		{
			this.StartFade();
		}
	}

	// Token: 0x060006C9 RID: 1737 RVA: 0x00006F03 File Offset: 0x00005103
	private void StartFade()
	{
		CoroutineUtil.Start(this.DoFade());
	}

	// Token: 0x060006CA RID: 1738 RVA: 0x00006F11 File Offset: 0x00005111
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
