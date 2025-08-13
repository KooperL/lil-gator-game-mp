using System;
using UnityEngine;

// Token: 0x02000348 RID: 840
public class TerrainTreeProximity : MonoBehaviour, IManagedUpdate
{
	// Token: 0x06001050 RID: 4176 RVA: 0x00053F4C File Offset: 0x0005214C
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

	// Token: 0x06001051 RID: 4177 RVA: 0x00053FBC File Offset: 0x000521BC
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

	// Token: 0x06001052 RID: 4178 RVA: 0x0000266A File Offset: 0x0000086A
	private void OnDisable()
	{
		FastUpdateManager.updateEvery4.Remove(this);
	}

	// Token: 0x06001053 RID: 4179 RVA: 0x00054070 File Offset: 0x00052270
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

	// Token: 0x04001523 RID: 5411
	public Terrain terrain;

	// Token: 0x04001524 RID: 5412
	public TerrainData terrainData;

	// Token: 0x04001525 RID: 5413
	public TerrainTreeProximity.TreeProximityType[] trees;

	// Token: 0x04001526 RID: 5414
	public float strengthMultiplier = 0.2f;

	// Token: 0x04001527 RID: 5415
	public float maxDistance = 20f;

	// Token: 0x04001528 RID: 5416
	public float minDistance = 20f;

	// Token: 0x04001529 RID: 5417
	private float maxDistanceSqr;

	// Token: 0x0400152A RID: 5418
	private int currentStep;

	// Token: 0x0400152B RID: 5419
	public Transform anchor;

	// Token: 0x0400152C RID: 5420
	private TreeInstance[] treeInstances;

	// Token: 0x02000349 RID: 841
	[Serializable]
	public struct TreeProximityType
	{
		// Token: 0x0400152D RID: 5421
		public string name;

		// Token: 0x0400152E RID: 5422
		public float strength;

		// Token: 0x0400152F RID: 5423
		public ParticleSystem particleSystem;

		// Token: 0x04001530 RID: 5424
		public float totalStrength;
	}
}
