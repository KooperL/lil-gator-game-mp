using System;
using System.Collections.Generic;
using UnityEngine;

public class HitOre : MonoBehaviour
{
	// (get) Token: 0x06000810 RID: 2064 RVA: 0x00007EE6 File Offset: 0x000060E6
	private string saveID
	{
		get
		{
			return "Ore_" + this.id.ToString();
		}
	}

	// Token: 0x06000811 RID: 2065 RVA: 0x00007EFD File Offset: 0x000060FD
	private void Start()
	{
		this.SetState(GameData.g.ReadBool(this.saveID, false));
	}

	// Token: 0x06000812 RID: 2066 RVA: 0x00007F16 File Offset: 0x00006116
	public void Hit(Vector3 velocity)
	{
		this.SetState(true);
	}

	// Token: 0x06000813 RID: 2067 RVA: 0x00007F1F File Offset: 0x0000611F
	private void SetState(bool state)
	{
		this.renderer.material = (state ? this.oreEmptyMaterial : this.oreFullMaterial);
		base.gameObject.SetActive(!state);
	}

	// Token: 0x06000814 RID: 2068 RVA: 0x00036FAC File Offset: 0x000351AC
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
