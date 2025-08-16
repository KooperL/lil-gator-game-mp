using System;
using System.Collections;
using UnityEngine;

public class CoroutineUtil : MonoBehaviour
{
	// Token: 0x0600032A RID: 810 RVA: 0x00004769 File Offset: 0x00002969
	public static Coroutine Start(IEnumerator enumerator)
	{
		if (CoroutineUtil.c == null)
		{
			CoroutineUtil.c = global::UnityEngine.Object.FindObjectOfType<CoroutineUtil>();
		}
		if (CoroutineUtil.c != null)
		{
			return CoroutineUtil.c.StartCo(enumerator);
		}
		return null;
	}

	// Token: 0x0600032B RID: 811 RVA: 0x0000479C File Offset: 0x0000299C
	public static void Stop(IEnumerator enumerator)
	{
		if (CoroutineUtil.c == null)
		{
			CoroutineUtil.c = global::UnityEngine.Object.FindObjectOfType<CoroutineUtil>();
		}
		if (CoroutineUtil.c != null)
		{
			CoroutineUtil.c.StopCo(enumerator);
		}
	}

	// Token: 0x0600032C RID: 812 RVA: 0x000047CD File Offset: 0x000029CD
	private void Awake()
	{
		if (CoroutineUtil.c == null)
		{
			CoroutineUtil.c = this;
		}
	}

	// Token: 0x0600032D RID: 813 RVA: 0x000047CD File Offset: 0x000029CD
	private void OnEnable()
	{
		if (CoroutineUtil.c == null)
		{
			CoroutineUtil.c = this;
		}
	}

	// Token: 0x0600032E RID: 814 RVA: 0x000047E2 File Offset: 0x000029E2
	public Coroutine StartCo(IEnumerator enumerator)
	{
		return base.StartCoroutine(enumerator);
	}

	// Token: 0x0600032F RID: 815 RVA: 0x000047EB File Offset: 0x000029EB
	public void StopCo(IEnumerator enumerator)
	{
		base.StopCoroutine(enumerator);
	}

	public static CoroutineUtil c;
}
