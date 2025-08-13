using System;
using UnityEngine;

// Token: 0x0200027B RID: 635
public class TerrainTreeProximity : MonoBehaviour, IManagedUpdate
{
	// Token: 0x06000D94 RID: 3476 RVA: 0x00041AD8 File Offset: 0x0003FCD8
	public void OnValidate()
	{
		if (this.terrainData != null && this.trees != null)
		{
			for (int i = 0; i < Mathf.Min(this.terrainData.treePrototypes.Length, this.trees.Length); i++)
			{
				this.trees[i].name = this.terrainData.treePrototypes[i].prefab.name;
			}
		}
	}

	// Token: 0x06000D95 RID: 3477 RVA: 0x00041B48 File Offset: 0x0003FD48
	private void OnEnable()
	{
		this.treeInstances = this.terrainData.treeInstances;
		for (int i = 0; i < this.treeInstances.Length; i++)
		{
			this.treeInstances[i].position = Vector3.Scale(this.treeInstances[i].position, this.terrainData.size);
			TreeInstance[] array = this.treeInstances;
			int num = i;
			array[num].position = array[num].position + this.terrain.transform.position;
		}
		this.maxDistanceSqr = this.maxDistance * this.maxDistance;
		FastUpdateManager.updateEvery4.Add(this);
	}

	// Token: 0x06000D96 RID: 3478 RVA: 0x00041BFA File Offset: 0x0003FDFA
	private void OnDisable()
	{
		FastUpdateManager.updateEvery4.Remove(this);
	}

	// Token: 0x06000D97 RID: 3479 RVA: 0x00041C08 File Offset: 0x0003FE08
	public void ManagedUpdate()
	{
		for (int i = 0; i < this.trees.Length; i++)
		{
			if (this.trees[i].strength != 0f && !(this.trees[i].particleSystem == null))
			{
				this.trees[i].totalStrength = 0f;
				this.trees[i].particleSystem.emission.rateOverTimeMultiplier = 0f;
			}
		}
		foreach (TreeInstance treeInstance in this.treeInstances)
		{
			if (this.trees.Length > treeInstance.prototypeIndex && this.trees[treeInstance.prototypeIndex].strength != 0f)
			{
				float num = Vector3.SqrMagnitude(treeInstance.position - this.anchor.position);
				if (num <= this.maxDistanceSqr)
				{
					float num2 = Mathf.InverseLerp(this.maxDistance, this.minDistance, Mathf.Sqrt(num));
					TerrainTreeProximity.TreeProximityType[] array2 = this.trees;
					int prototypeIndex = treeInstance.prototypeIndex;
					array2[prototypeIndex].totalStrength = array2[prototypeIndex].totalStrength + num2;
				}
			}
		}
		foreach (TerrainTreeProximity.TreeProximityType treeProximityType in this.trees)
		{
			if (treeProximityType.strength != 0f && !(treeProximityType.particleSystem == null))
			{
				float num3 = treeProximityType.totalStrength * treeProximityType.strength * this.strengthMultiplier;
				ParticleSystem.EmissionModule emissionModule;
				treeProximityType.particleSystem.emission.rateOverTimeMultiplier = emissionModule.rateOverTimeMultiplier + num3;
			}
		}
	}

	// Token: 0x040011E7 RID: 4583
	public Terrain terrain;

	// Token: 0x040011E8 RID: 4584
	public TerrainData terrainData;

	// Token: 0x040011E9 RID: 4585
	public TerrainTreeProximity.TreeProximityType[] trees;

	// Token: 0x040011EA RID: 4586
	public float strengthMultiplier = 0.2f;

	// Token: 0x040011EB RID: 4587
	public float maxDistance = 20f;

	// Token: 0x040011EC RID: 4588
	public float minDistance = 20f;

	// Token: 0x040011ED RID: 4589
	private float maxDistanceSqr;

	// Token: 0x040011EE RID: 4590
	private int currentStep;

	// Token: 0x040011EF RID: 4591
	public Transform anchor;

	// Token: 0x040011F0 RID: 4592
	private TreeInstance[] treeInstances;

	// Token: 0x0200042B RID: 1067
	[Serializable]
	public struct TreeProximityType
	{
		// Token: 0x04001D70 RID: 7536
		public string name;

		// Token: 0x04001D71 RID: 7537
		public float strength;

		// Token: 0x04001D72 RID: 7538
		public ParticleSystem particleSystem;

		// Token: 0x04001D73 RID: 7539
		public float totalStrength;
	}
}
