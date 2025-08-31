using System;
using UnityEngine;

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

	public Terrain terrain;

	public TerrainData terrainData;

	public TerrainTreeProximity.TreeProximityType[] trees;

	public float strengthMultiplier = 0.2f;

	public float maxDistance = 20f;

	public float minDistance = 20f;

	private float maxDistanceSqr;

	private int currentStep;

	public Transform anchor;

	private TreeInstance[] treeInstances;

	[Serializable]
	public struct TreeProximityType
	{
		public string name;

		public float strength;

		public ParticleSystem particleSystem;

		public float totalStrength;
	}
}
