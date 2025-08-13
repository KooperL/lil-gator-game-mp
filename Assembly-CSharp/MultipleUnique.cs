using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001F6 RID: 502
public class MultipleUnique : MonoBehaviour
{
	// Token: 0x06000948 RID: 2376 RVA: 0x0000911F File Offset: 0x0000731F
	public void Awake()
	{
		MultipleUnique.allUnique.Add(this);
		this.Load();
	}

	// Token: 0x06000949 RID: 2377 RVA: 0x00009132 File Offset: 0x00007332
	private void OnDestroy()
	{
		MultipleUnique.allUnique.Remove(this);
	}

	// Token: 0x0600094A RID: 2378 RVA: 0x000394B0 File Offset: 0x000376B0
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

	// Token: 0x0600094B RID: 2379 RVA: 0x00039508 File Offset: 0x00037708
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

	// Token: 0x04000BFB RID: 3067
	public static List<MultipleUnique> allUnique = new List<MultipleUnique>();

	// Token: 0x04000BFC RID: 3068
	public string key;

	// Token: 0x04000BFD RID: 3069
	public int id;

	// Token: 0x04000BFE RID: 3070
	public GameObject[] alsoDisable;
}
