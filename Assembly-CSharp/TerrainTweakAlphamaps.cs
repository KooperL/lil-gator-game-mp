using System;
using UnityEngine;

public class TerrainTweakAlphamaps : MonoBehaviour
{
	// Token: 0x060010B1 RID: 4273 RVA: 0x0005640C File Offset: 0x0005460C
	private void OnValidate()
	{
		if (this.terrain == null)
		{
			this.terrain = base.GetComponent<Terrain>();
		}
		this.areAlphamapsGrabbed = this.alphamaps != null;
		if (this.areAlphamapsGrabbed)
		{
			for (int i = 0; i < this.alphamaps.GetLength(2); i++)
			{
				if (this.offsets[i] != 0f)
				{
					for (int j = 0; j < this.alphamaps.GetLength(0); j++)
					{
						for (int k = 0; k < this.alphamaps.GetLength(1); k++)
						{
							this.modifiedAlphamaps[j, k, i] = this.alphamaps[j, k, i] + this.offsets[i];
						}
					}
				}
			}
			this.terrain.terrainData.SetAlphamaps(0, 0, this.modifiedAlphamaps);
		}
	}

	// Token: 0x060010B2 RID: 4274 RVA: 0x000564E0 File Offset: 0x000546E0
	[ContextMenu("Grab Alphamaps")]
	public void GrabAlphamaps()
	{
		this.areAlphamapsGrabbed = true;
		this.alphamaps = this.terrain.terrainData.GetAlphamaps(0, 0, this.terrain.terrainData.alphamapWidth, this.terrain.terrainData.alphamapHeight);
		this.modifiedAlphamaps = (float[,,])this.alphamaps.Clone();
	}

	// Token: 0x060010B3 RID: 4275 RVA: 0x0000E49B File Offset: 0x0000C69B
	[ContextMenu("Apply Alphamaps")]
	public void ApplyAlphamaps()
	{
		this.areAlphamapsGrabbed = false;
		this.terrain.terrainData.SetAlphamaps(0, 0, this.alphamaps);
	}

	private Terrain terrain;

	public bool areAlphamapsGrabbed;

	public float[,,] alphamaps;

	public float[,,] modifiedAlphamaps;

	[Range(-1f, 1f)]
	public float[] offsets;
}
