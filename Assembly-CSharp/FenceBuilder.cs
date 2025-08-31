using System;
using UnityEngine;

public class FenceBuilder : GenericPath
{
	// Token: 0x0600010A RID: 266 RVA: 0x00006B93 File Offset: 0x00004D93
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
