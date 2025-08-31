using System;
using System.Collections.Generic;
using UnityEngine;

public class MultipleUnique : MonoBehaviour
{
	// Token: 0x060007E6 RID: 2022 RVA: 0x00026540 File Offset: 0x00024740
	public void Awake()
	{
		MultipleUnique.allUnique.Add(this);
		this.Load();
	}

	// Token: 0x060007E7 RID: 2023 RVA: 0x00026553 File Offset: 0x00024753
	private void OnDestroy()
	{
		MultipleUnique.allUnique.Remove(this);
	}

	// Token: 0x060007E8 RID: 2024 RVA: 0x00026564 File Offset: 0x00024764
	public void Load()
	{
		int num = GameData.g.ReadInt(this.key, -1);
		if (num != -1 && num != this.id)
		{
			base.gameObject.SetActive(false);
			GameObject[] array = this.alsoDisable;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(false);
			}
		}
	}

	// Token: 0x060007E9 RID: 2025 RVA: 0x000265BC File Offset: 0x000247BC
	public void MarkThisOne()
	{
		GameData.g.Write(this.key, this.id);
		foreach (MultipleUnique multipleUnique in new List<MultipleUnique>(MultipleUnique.allUnique))
		{
			if (multipleUnique != this && multipleUnique.key == this.key)
			{
				multipleUnique.Load();
			}
		}
	}

	public static List<MultipleUnique> allUnique = new List<MultipleUnique>();

	public string key;

	public int id;

	public GameObject[] alsoDisable;
}
