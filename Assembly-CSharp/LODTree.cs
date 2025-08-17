using System;
using System.Collections.Generic;
using UnityEngine;

public class LODTree : MonoBehaviour
{
	// Token: 0x0600107E RID: 4222 RVA: 0x0000E265 File Offset: 0x0000C465
	public static void SetTreeQualitySettings(float fogDistance, bool useHighQualityTrees)
	{
		if (LODTree.fogDistance != fogDistance || LODTree.useHighQualityTrees != useHighQualityTrees)
		{
			LODTree.fogDistance = fogDistance;
			LODTree.useHighQualityTrees = useHighQualityTrees;
			LODTree.UpdateTrees();
		}
	}

	// Token: 0x0600107F RID: 4223 RVA: 0x00054F38 File Offset: 0x00053138
	public static void UpdateTrees()
	{
		foreach (LODTree lodtree in LODTree.instances)
		{
			lodtree.SyncWithSettings();
		}
	}

	// Token: 0x06001080 RID: 4224 RVA: 0x00054F88 File Offset: 0x00053188
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

	// Token: 0x06001081 RID: 4225 RVA: 0x0000E288 File Offset: 0x0000C488
	private void OnEnable()
	{
		LODTree.instances.Add(this);
		this.SyncWithSettings();
	}

	// Token: 0x06001082 RID: 4226 RVA: 0x0000E29B File Offset: 0x0000C49B
	private void OnDisable()
	{
		if (LODTree.instances.Contains(this))
		{
			LODTree.instances.Remove(this);
		}
	}

	// Token: 0x06001083 RID: 4227 RVA: 0x0000E2B6 File Offset: 0x0000C4B6
	public void SyncWithSettings()
	{
		if (LODTree.useHighQualityTrees)
		{
			this.UpdateLODs(LODTree.fogDistance, -1f);
			return;
		}
		this.UpdateLODs(-1f, LODTree.fogDistance);
	}

	// Token: 0x06001084 RID: 4228 RVA: 0x0000E2E0 File Offset: 0x0000C4E0
	[ContextMenu("Randomize")]
	public void Randomize()
	{
		this.RandomizeFlat();
		base.transform.rotation = Quaternion.RotateTowards(base.transform.rotation, global::UnityEngine.Random.rotationUniform, global::UnityEngine.Random.value * 4f);
	}

	// Token: 0x06001085 RID: 4229 RVA: 0x0000E313 File Offset: 0x0000C513
	[ContextMenu("Randomize Flat")]
	public void RandomizeFlat()
	{
		base.transform.rotation = Quaternion.LookRotation(global::UnityEngine.Random.insideUnitSphere.Flat());
	}

	// Token: 0x06001086 RID: 4230 RVA: 0x0000E32F File Offset: 0x0000C52F
	[ContextMenu("Clear Rotation")]
	public void ClearRotation()
	{
		base.transform.rotation = Quaternion.identity;
	}

	// Token: 0x06001087 RID: 4231 RVA: 0x0000E341 File Offset: 0x0000C541
	[ContextMenu("UpdateToFog")]
	public void UpdateToFog()
	{
		this.UpdateLODs(60f, -1f);
	}

	// Token: 0x06001088 RID: 4232 RVA: 0x0000E353 File Offset: 0x0000C553
	[ContextMenu("UpdateToFogLow")]
	public void UpdateToFogLow()
	{
		this.UpdateLODs(-1f, 60f);
	}

	// Token: 0x06001089 RID: 4233 RVA: 0x0005510C File Offset: 0x0005330C
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
