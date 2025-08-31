using System;
using UnityEngine;

[AddComponentMenu("Terrain/Tree LODs")]
public class TreeLODs : MonoBehaviour
{
	// Token: 0x06000DA8 RID: 3496 RVA: 0x00042416 File Offset: 0x00040616
	private void OnValidate()
	{
		if (this.terrain == null)
		{
			this.terrain = base.GetComponent<Terrain>();
		}
	}

	// Token: 0x06000DA9 RID: 3497 RVA: 0x00042434 File Offset: 0x00040634
	[ContextMenu("Update Prototypes")]
	public void UpdatePrototypes()
	{
	}

	// Token: 0x06000DAA RID: 3498 RVA: 0x00042444 File Offset: 0x00040644
	public void SetLOD(int level = 0)
	{
	}

	// Token: 0x06000DAB RID: 3499 RVA: 0x00042451 File Offset: 0x00040651
	private void OnDisable()
	{
		this.SetLOD(0);
	}

	// Token: 0x06000DAC RID: 3500 RVA: 0x0004245A File Offset: 0x0004065A
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
