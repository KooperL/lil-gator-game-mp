using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200033E RID: 830
public class LODTree : MonoBehaviour
{
	// Token: 0x06001023 RID: 4131 RVA: 0x0000DEFC File Offset: 0x0000C0FC
	public static void SetTreeQualitySettings(float fogDistance, bool useHighQualityTrees)
	{
		if (LODTree.fogDistance != fogDistance || LODTree.useHighQualityTrees != useHighQualityTrees)
		{
			LODTree.fogDistance = fogDistance;
			LODTree.useHighQualityTrees = useHighQualityTrees;
			LODTree.UpdateTrees();
		}
	}

	// Token: 0x06001024 RID: 4132 RVA: 0x00053014 File Offset: 0x00051214
	public static void UpdateTrees()
	{
		foreach (LODTree lodtree in LODTree.instances)
		{
			lodtree.SyncWithSettings();
		}
	}

	// Token: 0x06001025 RID: 4133 RVA: 0x00053064 File Offset: 0x00051264
	public void Initialize(GameObject high, GameObject low, GameObject billboard, GameObject cube)
	{
		this.lodGroup = base.GetComponent<LODGroup>();
		this.highs = new Renderer[] { high.GetComponent<Renderer>() };
		if (low != null)
		{
			this.lows = new Renderer[] { low.GetComponent<Renderer>() };
		}
		else
		{
			this.lows = new Renderer[0];
		}
		this.billboards = new Renderer[] { billboard.GetComponent<Renderer>() };
		this.cubes = new Renderer[] { cube.GetComponent<Renderer>() };
		LOD[] array;
		if (low != null)
		{
			array = new LOD[]
			{
				new LOD(0.5f, this.highs),
				new LOD(0.25f, this.lows),
				new LOD(1E-08f, this.billboards),
				new LOD(0f, this.cubes)
			};
		}
		else
		{
			array = new LOD[]
			{
				new LOD(0.5f, this.highs),
				new LOD(1E-08f, this.billboards),
				new LOD(0f, this.cubes)
			};
		}
		this.lodGroup.SetLODs(array);
		this.lodGroup.RecalculateBounds();
		this.lodGroup.localReferencePoint = cube.transform.localPosition;
		this.size = cube.transform.localScale.y;
	}

	// Token: 0x06001026 RID: 4134 RVA: 0x0000DF1F File Offset: 0x0000C11F
	private void OnEnable()
	{
		LODTree.instances.Add(this);
		this.SyncWithSettings();
	}

	// Token: 0x06001027 RID: 4135 RVA: 0x0000DF32 File Offset: 0x0000C132
	private void OnDisable()
	{
		if (LODTree.instances.Contains(this))
		{
			LODTree.instances.Remove(this);
		}
	}

	// Token: 0x06001028 RID: 4136 RVA: 0x0000DF4D File Offset: 0x0000C14D
	public void SyncWithSettings()
	{
		if (LODTree.useHighQualityTrees)
		{
			this.UpdateLODs(LODTree.fogDistance, -1f);
			return;
		}
		this.UpdateLODs(-1f, LODTree.fogDistance);
	}

	// Token: 0x06001029 RID: 4137 RVA: 0x0000DF77 File Offset: 0x0000C177
	[ContextMenu("Randomize")]
	public void Randomize()
	{
		this.RandomizeFlat();
		base.transform.rotation = Quaternion.RotateTowards(base.transform.rotation, Random.rotationUniform, Random.value * 4f);
	}

	// Token: 0x0600102A RID: 4138 RVA: 0x0000DFAA File Offset: 0x0000C1AA
	[ContextMenu("Randomize Flat")]
	public void RandomizeFlat()
	{
		base.transform.rotation = Quaternion.LookRotation(Random.insideUnitSphere.Flat());
	}

	// Token: 0x0600102B RID: 4139 RVA: 0x0000DFC6 File Offset: 0x0000C1C6
	[ContextMenu("Clear Rotation")]
	public void ClearRotation()
	{
		base.transform.rotation = Quaternion.identity;
	}

	// Token: 0x0600102C RID: 4140 RVA: 0x0000DFD8 File Offset: 0x0000C1D8
	[ContextMenu("UpdateToFog")]
	public void UpdateToFog()
	{
		this.UpdateLODs(60f, -1f);
	}

	// Token: 0x0600102D RID: 4141 RVA: 0x0000DFEA File Offset: 0x0000C1EA
	[ContextMenu("UpdateToFogLow")]
	public void UpdateToFogLow()
	{
		this.UpdateLODs(-1f, 60f);
	}

	// Token: 0x0600102E RID: 4142 RVA: 0x000531E8 File Offset: 0x000513E8
	public void UpdateLODs(float highDistance, float lowDistance)
	{
		float num = 2f * Mathf.Tan(0.43633232f);
		LOD[] array;
		if (this.lows.Length == 0)
		{
			float num2 = 0f;
			if (highDistance > 0f)
			{
				num2 = highDistance;
			}
			if (lowDistance > 0f && lowDistance > num2)
			{
				num2 = lowDistance;
			}
			float num3 = base.transform.localScale.y * this.size / (num * num2);
			array = new LOD[]
			{
				new LOD(num3, this.highs),
				new LOD(1E-08f, this.billboards),
				new LOD(0f, this.cubes)
			};
		}
		else
		{
			float num4;
			if (highDistance > 0f)
			{
				num4 = base.transform.localScale.y * this.size / (num * highDistance);
			}
			else
			{
				num4 = -1f;
			}
			float num5;
			if (lowDistance > 0f)
			{
				num5 = base.transform.localScale.y * this.size / (num * lowDistance);
			}
			else
			{
				num5 = -1f;
			}
			array = new LOD[4];
			if (highDistance > 0f && lowDistance > 0f)
			{
				array[0] = new LOD(num4, this.highs);
				array[1] = new LOD(num5, this.lows);
				array[2] = new LOD(1E-08f, this.billboards);
			}
			else if (highDistance < 0f)
			{
				array[0] = new LOD(num5, this.lows);
				array[1] = new LOD(1E-08f, this.billboards);
				array[2] = new LOD(1E-09f, this.highs);
			}
			else
			{
				array[0] = new LOD(num4, this.highs);
				array[1] = new LOD(1E-08f, this.billboards);
				array[2] = new LOD(1E-09f, this.lows);
			}
			array[3] = new LOD(0f, this.cubes);
		}
		this.lodGroup.SetLODs(array);
	}

	// Token: 0x040014E8 RID: 5352
	private static Vector3[] bakedPositionData = new Vector3[8];

	// Token: 0x040014E9 RID: 5353
	public static float fogDistance = 60f;

	// Token: 0x040014EA RID: 5354
	public static bool useHighQualityTrees = true;

	// Token: 0x040014EB RID: 5355
	public static List<LODTree> instances = new List<LODTree>();

	// Token: 0x040014EC RID: 5356
	public LODGroup lodGroup;

	// Token: 0x040014ED RID: 5357
	public Renderer[] highs;

	// Token: 0x040014EE RID: 5358
	public Renderer[] lows;

	// Token: 0x040014EF RID: 5359
	public Renderer[] billboards;

	// Token: 0x040014F0 RID: 5360
	public Renderer[] cubes;

	// Token: 0x040014F1 RID: 5361
	public float size;

	// Token: 0x040014F2 RID: 5362
	public MeshFilter billboardFilter;

	// Token: 0x040014F3 RID: 5363
	public Mesh localBillboardMesh;

	// Token: 0x040014F4 RID: 5364
	[ReadOnly]
	public Vector3 bakedPosition = Vector3.zero;

	// Token: 0x040014F5 RID: 5365
	[ReadOnly]
	public string objectName;

	// Token: 0x040014F6 RID: 5366
	public bool isDuplicated;

	// Token: 0x040014F7 RID: 5367
	private const float randomRotationAngle = 4f;

	// Token: 0x040014F8 RID: 5368
	private const float fov = 50f;
}
