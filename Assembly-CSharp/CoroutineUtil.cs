using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000C0 RID: 192
public class CoroutineUtil : MonoBehaviour
{
	// Token: 0x0600031D RID: 797 RVA: 0x0000467D File Offset: 0x0000287D
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

	// Token: 0x0600031E RID: 798 RVA: 0x000046B0 File Offset: 0x000028B0
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

	// Token: 0x0600031F RID: 799 RVA: 0x000046E1 File Offset: 0x000028E1
	private void Awake()
	{
		if (CoroutineUtil.c == null)
		{
			CoroutineUtil.c = this;
		}
	}

	// Token: 0x06000320 RID: 800 RVA: 0x000046E1 File Offset: 0x000028E1
	private void OnEnable()
	{
		if (CoroutineUtil.c == null)
		{
			CoroutineUtil.c = this;
		}
	}

	// Token: 0x06000321 RID: 801 RVA: 0x000046F6 File Offset: 0x000028F6
	public Coroutine StartCo(IEnumerator enumerator)
	{
		return base.StartCoroutine(enumerator);
	}

	// Token: 0x06000322 RID: 802 RVA: 0x000046FF File Offset: 0x000028FF
	public void StopCo(IEnumerator enumerator)
	{
		base.StopCoroutine(enumerator);
	}

	// Token: 0x0400048F RID: 1167
	public static CoroutineUtil c;
}
