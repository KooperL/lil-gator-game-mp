using System;
using UnityEngine;

[AddComponentMenu("Terrain/Tree LODs")]
public class TreeLODs : MonoBehaviour
{
	// Token: 0x060010BF RID: 4287 RVA: 0x0000E4E8 File Offset: 0x0000C6E8
	private void OnValidate()
	{
		if (this.terrain == null)
		{
			this.terrain = base.GetComponent<Terrain>();
		}
	}

	// Token: 0x060010C0 RID: 4288 RVA: 0x00056700 File Offset: 0x00054900
	[ContextMenu("Update Prototypes")]
	public void UpdatePrototypes()
	{
	}

	// Token: 0x060010C1 RID: 4289 RVA: 0x00056710 File Offset: 0x00054910
	public void SetLOD(int level = 0)
	{
	}

	// Token: 0x060010C2 RID: 4290 RVA: 0x0000E504 File Offset: 0x0000C704
	private void OnDisable()
	{
		this.SetLOD(0);
	}

	// Token: 0x060010C3 RID: 4291 RVA: 0x0000E504 File Offset: 0x0000C704
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
