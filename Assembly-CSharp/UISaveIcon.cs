using System;
using System.Collections.Generic;
using UnityEngine;

public class UISaveIcon : MonoBehaviour
{
	// Token: 0x060012DE RID: 4830 RVA: 0x0005D3FC File Offset: 0x0005B5FC
	public static void ShowIcon()
	{
		UISaveIcon.lastSaveTime = Time.time;
		foreach (UISaveIcon uisaveIcon in UISaveIcon.instances)
		{
			uisaveIcon.Show();
		}
	}

	// Token: 0x060012DF RID: 4831 RVA: 0x0000FEB7 File Offset: 0x0000E0B7
	private void Awake()
	{
		UISaveIcon.instances.Add(this);
		if (Time.time > UISaveIcon.lastSaveTime + 2f)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x060012E0 RID: 4832 RVA: 0x0000FEE2 File Offset: 0x0000E0E2
	private void OnDestroy()
	{
		UISaveIcon.instances.Remove(this);
	}

	// Token: 0x060012E1 RID: 4833 RVA: 0x0000FEF0 File Offset: 0x0000E0F0
	private void Update()
	{
		if (Time.time > UISaveIcon.lastSaveTime + 2f)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x060012E2 RID: 4834 RVA: 0x0000FF10 File Offset: 0x0000E110
	public void Show()
	{
		base.gameObject.SetActive(true);
	}

	public static float lastSaveTime = -1f;

	private static List<UISaveIcon> instances = new List<UISaveIcon>();
}
