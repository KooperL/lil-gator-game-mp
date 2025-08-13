using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000C6 RID: 198
public class DependentDecoration : MonoBehaviour
{
	// Token: 0x06000335 RID: 821 RVA: 0x00024B8C File Offset: 0x00022D8C
	public static void ActivateAll()
	{
		foreach (DependentDecoration dependentDecoration in DependentDecoration.dependentDecorations)
		{
			dependentDecoration.ActivateDecorations();
		}
	}

	// Token: 0x06000336 RID: 822 RVA: 0x00004808 File Offset: 0x00002A08
	private void OnEnable()
	{
		DependentDecoration.dependentDecorations.Add(this);
		this.ActivateDecorations();
	}

	// Token: 0x06000337 RID: 823 RVA: 0x00024BDC File Offset: 0x00022DDC
	private void OnDisable()
	{
		if (DependentDecoration.dependentDecorations.Contains(this))
		{
			DependentDecoration.dependentDecorations.Remove(this);
		}
		GameObject[] array = this.decorations;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(false);
		}
		BuildingUpgradeStation.UpdateAllActive();
	}

	// Token: 0x06000338 RID: 824 RVA: 0x00024C28 File Offset: 0x00022E28
	public void ActivateDecorations()
	{
		GameObject[] array = this.decorations;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(true);
		}
	}

	// Token: 0x040004A7 RID: 1191
	public static List<DependentDecoration> dependentDecorations = new List<DependentDecoration>();

	// Token: 0x040004A8 RID: 1192
	public GameObject[] decorations;
}
