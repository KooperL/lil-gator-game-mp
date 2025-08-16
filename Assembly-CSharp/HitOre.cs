using System;
using System.Collections.Generic;
using UnityEngine;

public class HitOre : MonoBehaviour
{
	// (get) Token: 0x0600080F RID: 2063 RVA: 0x00007ED1 File Offset: 0x000060D1
	private string saveID
	{
		get
		{
			return "Ore_" + this.id.ToString();
		}
	}

	// Token: 0x06000810 RID: 2064 RVA: 0x00007EE8 File Offset: 0x000060E8
	private void Start()
	{
		this.SetState(GameData.g.ReadBool(this.saveID, false));
	}

	// Token: 0x06000811 RID: 2065 RVA: 0x00007F01 File Offset: 0x00006101
	public void Hit(Vector3 velocity)
	{
		this.SetState(true);
	}

	// Token: 0x06000812 RID: 2066 RVA: 0x00007F0A File Offset: 0x0000610A
	private void SetState(bool state)
	{
		this.renderer.material = (state ? this.oreEmptyMaterial : this.oreFullMaterial);
		base.gameObject.SetActive(!state);
	}

	// Token: 0x06000813 RID: 2067 RVA: 0x00036B04 File Offset: 0x00034D04
	[ContextMenu("Assign Unique ID")]
	public void AssignUniqueID()
	{
		List<int> list = new List<int>();
		foreach (HitOre hitOre in global::UnityEngine.Object.FindObjectsOfType<HitOre>())
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

	public Renderer renderer;

	public Material oreFullMaterial;

	public Material oreEmptyMaterial;

	public int id = -1;
}
