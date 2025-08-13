using System;
using UnityEngine;

// Token: 0x0200034A RID: 842
public class TerrainTweakAlphamaps : MonoBehaviour
{
	// Token: 0x06001055 RID: 4181 RVA: 0x00054220 File Offset: 0x00052420
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

	// Token: 0x06001056 RID: 4182 RVA: 0x000542F4 File Offset: 0x000524F4
	[ContextMenu("Grab Alphamaps")]
	public void GrabAlphamaps()
	{
		this.areAlphamapsGrabbed = true;
		this.alphamaps = this.terrain.terrainData.GetAlphamaps(0, 0, this.terrain.terrainData.alphamapWidth, this.terrain.terrainData.alphamapHeight);
		this.modifiedAlphamaps = (float[,,])this.alphamaps.Clone();
	}

	// Token: 0x06001057 RID: 4183 RVA: 0x0000E128 File Offset: 0x0000C328
	[ContextMenu("Apply Alphamaps")]
	public void ApplyAlphamaps()
	{
		this.areAlphamapsGrabbed = false;
		this.terrain.terrainData.SetAlphamaps(0, 0, this.alphamaps);
	}

	// Token: 0x04001531 RID: 5425
	private Terrain terrain;

	// Token: 0x04001532 RID: 5426
	public bool areAlphamapsGrabbed;

	// Token: 0x04001533 RID: 5427
	public float[,,] alphamaps;

	// Token: 0x04001534 RID: 5428
	public float[,,] modifiedAlphamaps;

	// Token: 0x04001535 RID: 5429
	[Range(-1f, 1f)]
	public float[] offsets;
}
