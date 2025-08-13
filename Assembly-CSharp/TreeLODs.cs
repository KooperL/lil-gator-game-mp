using System;
using UnityEngine;

// Token: 0x0200034F RID: 847
[AddComponentMenu("Terrain/Tree LODs")]
public class TreeLODs : MonoBehaviour
{
	// Token: 0x06001064 RID: 4196 RVA: 0x0000E17F File Offset: 0x0000C37F
	private void OnValidate()
	{
		if (this.terrain == null)
		{
			this.terrain = base.GetComponent<Terrain>();
		}
	}

	// Token: 0x06001065 RID: 4197 RVA: 0x000547DC File Offset: 0x000529DC
	[ContextMenu("Update Prototypes")]
	public void UpdatePrototypes()
	{
	}

	// Token: 0x06001066 RID: 4198 RVA: 0x000547EC File Offset: 0x000529EC
	public void SetLOD(int level = 0)
	{
	}

	// Token: 0x06001067 RID: 4199 RVA: 0x0000E19B File Offset: 0x0000C39B
	private void OnDisable()
	{
		this.SetLOD(0);
	}

	// Token: 0x06001068 RID: 4200 RVA: 0x0000E19B File Offset: 0x0000C39B
	private void OnDestroy()
	{
		this.SetLOD(0);
	}

	// Token: 0x04001542 RID: 5442
	public TreeLODs.TreePrototypesLODs[] prototypeLODs;

	// Token: 0x04001543 RID: 5443
	[HideInInspector]
	public Terrain terrain;

	// Token: 0x02000350 RID: 848
	[Serializable]
	public struct TreePrototypesLODs
	{
		// Token: 0x04001544 RID: 5444
		[HideInInspector]
		public string name;

		// Token: 0x04001545 RID: 5445
		public GameObject[] LODs;
	}
}
