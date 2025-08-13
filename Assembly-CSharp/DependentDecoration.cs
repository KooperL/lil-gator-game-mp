using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200009E RID: 158
public class DependentDecoration : MonoBehaviour
{
	// Token: 0x060002F2 RID: 754 RVA: 0x0001159C File Offset: 0x0000F79C
	public static void ActivateAll()
	{
		foreach (DependentDecoration dependentDecoration in DependentDecoration.dependentDecorations)
		{
			dependentDecoration.ActivateDecorations();
		}
	}

	// Token: 0x060002F3 RID: 755 RVA: 0x000115EC File Offset: 0x0000F7EC
	private void OnEnable()
	{
		DependentDecoration.dependentDecorations.Add(this);
		this.ActivateDecorations();
	}

	// Token: 0x060002F4 RID: 756 RVA: 0x00011600 File Offset: 0x0000F800
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

	// Token: 0x060002F5 RID: 757 RVA: 0x0001164C File Offset: 0x0000F84C
	public void ActivateDecorations()
	{
		GameObject[] array = this.decorations;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(true);
		}
	}

	// Token: 0x04000411 RID: 1041
	public static List<DependentDecoration> dependentDecorations = new List<DependentDecoration>();

	// Token: 0x04000412 RID: 1042
	public GameObject[] decorations;
}
