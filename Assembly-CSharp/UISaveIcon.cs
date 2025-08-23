using System;
using System.Collections.Generic;
using UnityEngine;

public class UISaveIcon : MonoBehaviour
{
	// Token: 0x060012DF RID: 4831 RVA: 0x0005D6C4 File Offset: 0x0005B8C4
	public static void ShowIcon()
	{
		UISaveIcon.lastSaveTime = Time.time;
		foreach (UISaveIcon uisaveIcon in UISaveIcon.instances)
		{
			uisaveIcon.Show();
		}
	}

	// Token: 0x060012E0 RID: 4832 RVA: 0x0000FEC1 File Offset: 0x0000E0C1
	private void Awake()
	{
		UISaveIcon.instances.Add(this);
		if (Time.time > UISaveIcon.lastSaveTime + 2f)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x060012E1 RID: 4833 RVA: 0x0000FEEC File Offset: 0x0000E0EC
	private void OnDestroy()
	{
		UISaveIcon.instances.Remove(this);
	}

	// Token: 0x060012E2 RID: 4834 RVA: 0x0000FEFA File Offset: 0x0000E0FA
	private void Update()
	{
		if (Time.time > UISaveIcon.lastSaveTime + 2f)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x060012E3 RID: 4835 RVA: 0x0000FF1A File Offset: 0x0000E11A
	public void Show()
	{
		base.gameObject.SetActive(true);
	}

	public static float lastSaveTime = -1f;

	private static List<UISaveIcon> instances = new List<UISaveIcon>();
}
