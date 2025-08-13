using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000143 RID: 323
public class HitOre : MonoBehaviour
{
	// Token: 0x1700005B RID: 91
	// (get) Token: 0x06000697 RID: 1687 RVA: 0x000218F8 File Offset: 0x0001FAF8
	private string saveID
	{
		get
		{
			return "Ore_" + this.id.ToString();
		}
	}

	// Token: 0x06000698 RID: 1688 RVA: 0x0002190F File Offset: 0x0001FB0F
	private void Start()
	{
		this.SetState(GameData.g.ReadBool(this.saveID, false));
	}

	// Token: 0x06000699 RID: 1689 RVA: 0x00021928 File Offset: 0x0001FB28
	public void Hit(Vector3 velocity)
	{
		this.SetState(true);
	}

	// Token: 0x0600069A RID: 1690 RVA: 0x00021931 File Offset: 0x0001FB31
	private void SetState(bool state)
	{
		this.renderer.material = (state ? this.oreEmptyMaterial : this.oreFullMaterial);
		base.gameObject.SetActive(!state);
	}

	// Token: 0x0600069B RID: 1691 RVA: 0x00021960 File Offset: 0x0001FB60
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

	// Token: 0x040008E0 RID: 2272
	public Renderer renderer;

	// Token: 0x040008E1 RID: 2273
	public Material oreFullMaterial;

	// Token: 0x040008E2 RID: 2274
	public Material oreEmptyMaterial;

	// Token: 0x040008E3 RID: 2275
	public int id = -1;
}
