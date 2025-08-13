using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003C6 RID: 966
public class UISaveIcon : MonoBehaviour
{
	// Token: 0x0600127E RID: 4734 RVA: 0x0005B44C File Offset: 0x0005964C
	public static void ShowIcon()
	{
		UISaveIcon.lastSaveTime = Time.time;
		foreach (UISaveIcon uisaveIcon in UISaveIcon.instances)
		{
			uisaveIcon.Show();
		}
	}

	// Token: 0x0600127F RID: 4735 RVA: 0x0000FAC1 File Offset: 0x0000DCC1
	private void Awake()
	{
		UISaveIcon.instances.Add(this);
		if (Time.time > UISaveIcon.lastSaveTime + 2f)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001280 RID: 4736 RVA: 0x0000FAEC File Offset: 0x0000DCEC
	private void OnDestroy()
	{
		UISaveIcon.instances.Remove(this);
	}

	// Token: 0x06001281 RID: 4737 RVA: 0x0000FAFA File Offset: 0x0000DCFA
	private void Update()
	{
		if (Time.time > UISaveIcon.lastSaveTime + 2f)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001282 RID: 4738 RVA: 0x0000FB1A File Offset: 0x0000DD1A
	public void Show()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x040017E2 RID: 6114
	public static float lastSaveTime = -1f;

	// Token: 0x040017E3 RID: 6115
	private static List<UISaveIcon> instances = new List<UISaveIcon>();
}
