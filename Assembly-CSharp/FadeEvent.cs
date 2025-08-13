using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000160 RID: 352
public class FadeEvent : MonoBehaviour
{
	// Token: 0x0600068E RID: 1678 RVA: 0x00006C2D File Offset: 0x00004E2D
	private void OnEnable()
	{
		if (this.triggerOnEnable)
		{
			this.StartFade();
		}
	}

	// Token: 0x0600068F RID: 1679 RVA: 0x00006C3D File Offset: 0x00004E3D
	private void StartFade()
	{
		CoroutineUtil.Start(this.DoFade());
	}

	// Token: 0x06000690 RID: 1680 RVA: 0x00006C4B File Offset: 0x00004E4B
	private IEnumerator DoFade()
	{
		yield return Blackout.FadeIn();
		this.onFade.Invoke();
		Blackout.FadeOut();
		yield break;
	}

	// Token: 0x040008DB RID: 2267
	public bool triggerOnEnable = true;

	// Token: 0x040008DC RID: 2268
	public UnityEvent onFade;
}
