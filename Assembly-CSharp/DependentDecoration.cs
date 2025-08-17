using System;
using System.Collections.Generic;
using UnityEngine;

public class DependentDecoration : MonoBehaviour
{
	// Token: 0x0600035B RID: 859 RVA: 0x00025B30 File Offset: 0x00023D30
	public static void ActivateAll()
	{
		foreach (DependentDecoration dependentDecoration in DependentDecoration.dependentDecorations)
		{
			dependentDecoration.ActivateDecorations();
		}
	}

	// Token: 0x0600035C RID: 860 RVA: 0x000049EC File Offset: 0x00002BEC
	private void OnEnable()
	{
		DependentDecoration.dependentDecorations.Add(this);
		this.ActivateDecorations();
	}

	// Token: 0x0600035D RID: 861 RVA: 0x00025B80 File Offset: 0x00023D80
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

	// Token: 0x0600035E RID: 862 RVA: 0x00025BCC File Offset: 0x00023DCC
	public void ActivateDecorations()
	{
		GameObject[] array = this.decorations;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(true);
		}
	}

	public static List<DependentDecoration> dependentDecorations = new List<DependentDecoration>();

	public GameObject[] decorations;
}
