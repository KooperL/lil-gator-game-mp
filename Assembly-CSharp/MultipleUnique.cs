using System;
using System.Collections.Generic;
using UnityEngine;

public class MultipleUnique : MonoBehaviour
{
	// Token: 0x06000989 RID: 2441 RVA: 0x00009450 File Offset: 0x00007650
	public void Awake()
	{
		MultipleUnique.allUnique.Add(this);
		this.Load();
	}

	// Token: 0x0600098A RID: 2442 RVA: 0x00009463 File Offset: 0x00007663
	private void OnDestroy()
	{
		MultipleUnique.allUnique.Remove(this);
	}

	// Token: 0x0600098B RID: 2443 RVA: 0x0003AE20 File Offset: 0x00039020
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

	// Token: 0x0600098C RID: 2444 RVA: 0x0003AE78 File Offset: 0x00039078
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
