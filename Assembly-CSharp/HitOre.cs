using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001A6 RID: 422
public class HitOre : MonoBehaviour
{
	// Token: 0x170000C1 RID: 193
	// (get) Token: 0x060007CF RID: 1999 RVA: 0x00007BD7 File Offset: 0x00005DD7
	private string saveID
	{
		get
		{
			return "Ore_" + this.id.ToString();
		}
	}

	// Token: 0x060007D0 RID: 2000 RVA: 0x00007BEE File Offset: 0x00005DEE
	private void Start()
	{
		this.SetState(GameData.g.ReadBool(this.saveID, false));
	}

	// Token: 0x060007D1 RID: 2001 RVA: 0x00007C07 File Offset: 0x00005E07
	public void Hit(Vector3 velocity)
	{
		this.SetState(true);
	}

	// Token: 0x060007D2 RID: 2002 RVA: 0x00007C10 File Offset: 0x00005E10
	private void SetState(bool state)
	{
		this.renderer.material = (state ? this.oreEmptyMaterial : this.oreFullMaterial);
		base.gameObject.SetActive(!state);
	}

	// Token: 0x060007D3 RID: 2003 RVA: 0x0003537C File Offset: 0x0003357C
	[ContextMenu("Assign Unique ID")]
	public void AssignUniqueID()
	{
		List<int> list = new List<int>();
		foreach (HitOre hitOre in Object.FindObjectsOfType<HitOre>())
		{
			if (hitOre.id != -1 && hitOre != this && !list.Contains(hitOre.id))
			{
				list.Add(hitOre.id);
			}
		}
		if (this.id == -1 || list.Contains(this.id))
		{
			this.id = 0;
			while (list.Contains(this.id))
			{
				this.id++;
			}
		}
	}

	// Token: 0x04000A64 RID: 2660
	public Renderer renderer;

	// Token: 0x04000A65 RID: 2661
	public Material oreFullMaterial;

	// Token: 0x04000A66 RID: 2662
	public Material oreEmptyMaterial;

	// Token: 0x04000A67 RID: 2663
	public int id = -1;
}
