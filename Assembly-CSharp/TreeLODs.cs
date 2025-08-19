using System;
using UnityEngine;

[AddComponentMenu("Terrain/Tree LODs")]
public class TreeLODs : MonoBehaviour
{
	// Token: 0x060010BF RID: 4287 RVA: 0x0000E4F2 File Offset: 0x0000C6F2
	private void OnValidate()
	{
		if (this.terrain == null)
		{
			this.terrain = base.GetComponent<Terrain>();
		}
	}

	// Token: 0x060010C0 RID: 4288 RVA: 0x000566DC File Offset: 0x000548DC
	[ContextMenu("Update Prototypes")]
	public void UpdatePrototypes()
	{
	}

	// Token: 0x060010C1 RID: 4289 RVA: 0x000566EC File Offset: 0x000548EC
	public void SetLOD(int level = 0)
	{
	}

	// Token: 0x060010C2 RID: 4290 RVA: 0x0000E50E File Offset: 0x0000C70E
	private void OnDisable()
	{
		this.SetLOD(0);
	}

	// Token: 0x060010C3 RID: 4291 RVA: 0x0000E50E File Offset: 0x0000C70E
	private void OnDestroy()
	{
		this.SetLOD(0);
	}

	public TreeLODs.TreePrototypesLODs[] prototypeLODs;

	[HideInInspector]
	public Terrain terrain;

	[Serializable]
	public struct TreePrototypesLODs
	{
		[HideInInspector]
		public string name;

		public GameObject[] LODs;
	}
}
