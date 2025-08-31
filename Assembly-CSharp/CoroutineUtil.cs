using System;
using System.Collections;
using UnityEngine;

public class CoroutineUtil : MonoBehaviour
{
	// Token: 0x060002D3 RID: 723 RVA: 0x00011150 File Offset: 0x0000F350
	public static Coroutine Start(IEnumerator enumerator)
	{
		if (CoroutineUtil.c == null)
		{
			CoroutineUtil.c = Object.FindObjectOfType<CoroutineUtil>();
		}
		if (CoroutineUtil.c != null)
		{
			return CoroutineUtil.c.StartCo(enumerator);
		}
		return null;
	}

	// Token: 0x060002D4 RID: 724 RVA: 0x00011183 File Offset: 0x0000F383
	public static void Stop(IEnumerator enumerator)
	{
		if (CoroutineUtil.c == null)
		{
			CoroutineUtil.c = Object.FindObjectOfType<CoroutineUtil>();
		}
		if (CoroutineUtil.c != null)
		{
			CoroutineUtil.c.StopCo(enumerator);
		}
	}

	// Token: 0x060002D5 RID: 725 RVA: 0x000111B4 File Offset: 0x0000F3B4
	private void Awake()
	{
		if (CoroutineUtil.c == null)
		{
			CoroutineUtil.c = this;
		}
	}

	// Token: 0x060002D6 RID: 726 RVA: 0x000111C9 File Offset: 0x0000F3C9
	private void OnEnable()
	{
		if (CoroutineUtil.c == null)
		{
			CoroutineUtil.c = this;
		}
	}

	// Token: 0x060002D7 RID: 727 RVA: 0x000111DE File Offset: 0x0000F3DE
	public Coroutine StartCo(IEnumerator enumerator)
	{
		return base.StartCoroutine(enumerator);
	}

	// Token: 0x060002D8 RID: 728 RVA: 0x000111E7 File Offset: 0x0000F3E7
	public void StopCo(IEnumerator enumerator)
	{
		base.StopCoroutine(enumerator);
	}

	public static CoroutineUtil c;
}
