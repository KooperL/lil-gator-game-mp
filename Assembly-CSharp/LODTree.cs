using System;
using System.Collections.Generic;
using UnityEngine;

public class LODTree : MonoBehaviour
{
	// Token: 0x06000D6E RID: 3438 RVA: 0x00040AA8 File Offset: 0x0003ECA8
	public static void SetTreeQualitySettings(float fogDistance, bool useHighQualityTrees)
	{
		if (LODTree.fogDistance != fogDistance || LODTree.useHighQualityTrees != useHighQualityTrees)
		{
			LODTree.fogDistance = fogDistance;
			LODTree.useHighQualityTrees = useHighQualityTrees;
			LODTree.UpdateTrees();
		}
	}

	// Token: 0x06000D6F RID: 3439 RVA: 0x00040ACC File Offset: 0x0003ECCC
	public static void UpdateTrees()
	{
		foreach (LODTree lodtree in LODTree.instances)
		{
			lodtree.SyncWithSettings();
		}
	}

	// Token: 0x06000D70 RID: 3440 RVA: 0x00040B1C File Offset: 0x0003ED1C
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

	// Token: 0x06000D71 RID: 3441 RVA: 0x00040CA0 File Offset: 0x0003EEA0
	private void OnEnable()
	{
		LODTree.instances.Add(this);
		this.SyncWithSettings();
	}

	// Token: 0x06000D72 RID: 3442 RVA: 0x00040CB3 File Offset: 0x0003EEB3
	private void OnDisable()
	{
		if (LODTree.instances.Contains(this))
		{
			LODTree.instances.Remove(this);
		}
	}

	// Token: 0x06000D73 RID: 3443 RVA: 0x00040CCE File Offset: 0x0003EECE
	public void SyncWithSettings()
	{
		if (LODTree.useHighQualityTrees)
		{
			this.UpdateLODs(LODTree.fogDistance, -1f);
			return;
		}
		this.UpdateLODs(-1f, LODTree.fogDistance);
	}

	// Token: 0x06000D74 RID: 3444 RVA: 0x00040CF8 File Offset: 0x0003EEF8
	[ContextMenu("Randomize")]
	public void Randomize()
	{
		this.RandomizeFlat();
		base.transform.rotation = Quaternion.RotateTowards(base.transform.rotation, Random.rotationUniform, Random.value * 4f);
	}

	// Token: 0x06000D75 RID: 3445 RVA: 0x00040D2B File Offset: 0x0003EF2B
	[ContextMenu("Randomize Flat")]
	public void RandomizeFlat()
	{
		base.transform.rotation = Quaternion.LookRotation(Random.insideUnitSphere.Flat());
	}

	// Token: 0x06000D76 RID: 3446 RVA: 0x00040D47 File Offset: 0x0003EF47
	[ContextMenu("Clear Rotation")]
	public void ClearRotation()
	{
		base.transform.rotation = Quaternion.identity;
	}

	// Token: 0x06000D77 RID: 3447 RVA: 0x00040D59 File Offset: 0x0003EF59
	[ContextMenu("UpdateToFog")]
	public void UpdateToFog()
	{
		this.UpdateLODs(60f, -1f);
	}

	// Token: 0x06000D78 RID: 3448 RVA: 0x00040D6B File Offset: 0x0003EF6B
	[ContextMenu("UpdateToFogLow")]
	public void UpdateToFogLow()
	{
		this.UpdateLODs(-1f, 60f);
	}

	// Token: 0x06000D79 RID: 3449 RVA: 0x00040D80 File Offset: 0x0003EF80
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

	private static Vector3[] bakedPositionData = new Vector3[8];

	public static float fogDistance = 60f;

	public static bool useHighQualityTrees = true;

	public static List<LODTree> instances = new List<LODTree>();

	public LODGroup lodGroup;

	public Renderer[] highs;

	public Renderer[] lows;

	public Renderer[] billboards;

	public Renderer[] cubes;

	public float size;

	public MeshFilter billboardFilter;

	public Mesh localBillboardMesh;

	[ReadOnly]
	public Vector3 bakedPosition = Vector3.zero;

	[ReadOnly]
	public string objectName;

	public bool isDuplicated;

	private const float randomRotationAngle = 4f;

	private const float fov = 50f;
}
