using System;
using UnityEngine;

public class FenceBuilder : GenericPath
{
	// Token: 0x06000137 RID: 311 RVA: 0x00002229 File Offset: 0x00000429
	public override void UpdatePath()
	{
	}

	public GameObject postPrefab;

	public GameObject fencePrefab;

	public float fenceLength = 1f;

	public GameObject[] posts;

	public GameObject[] fences;

	public bool normalizeScale;

	public bool lockFenceToPost;
}
