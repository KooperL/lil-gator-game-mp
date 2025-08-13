using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002DA RID: 730
public class UISaveIcon : MonoBehaviour
{
	// Token: 0x06000F6A RID: 3946 RVA: 0x00049FD0 File Offset: 0x000481D0
	public static void ShowIcon()
	{
		UISaveIcon.lastSaveTime = Time.time;
		foreach (UISaveIcon uisaveIcon in UISaveIcon.instances)
		{
			uisaveIcon.Show();
		}
	}

	// Token: 0x06000F6B RID: 3947 RVA: 0x0004A02C File Offset: 0x0004822C
	private void Awake()
	{
		UISaveIcon.instances.Add(this);
		if (Time.time > UISaveIcon.lastSaveTime + 2f)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000F6C RID: 3948 RVA: 0x0004A057 File Offset: 0x00048257
	private void OnDestroy()
	{
		UISaveIcon.instances.Remove(this);
	}

	// Token: 0x06000F6D RID: 3949 RVA: 0x0004A065 File Offset: 0x00048265
	private void Update()
	{
		if (Time.time > UISaveIcon.lastSaveTime + 2f)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000F6E RID: 3950 RVA: 0x0004A085 File Offset: 0x00048285
	public void Show()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x0400143D RID: 5181
	public static float lastSaveTime = -1f;

	// Token: 0x0400143E RID: 5182
	private static List<UISaveIcon> instances = new List<UISaveIcon>();
}
