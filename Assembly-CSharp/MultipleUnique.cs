using System;
using System.Collections.Generic;
using UnityEngine;

public class MultipleUnique : MonoBehaviour
{
	// Token: 0x0600098A RID: 2442 RVA: 0x0000945A File Offset: 0x0000765A
	public void Awake()
	{
		MultipleUnique.allUnique.Add(this);
		this.Load();
	}

	// Token: 0x0600098B RID: 2443 RVA: 0x0000946D File Offset: 0x0000766D
	private void OnDestroy()
	{
		MultipleUnique.allUnique.Remove(this);
	}

	// Token: 0x0600098C RID: 2444 RVA: 0x0003B0E8 File Offset: 0x000392E8
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

	// Token: 0x0600098D RID: 2445 RVA: 0x0003B140 File Offset: 0x00039340
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
