using System;
using UnityEngine;

// Token: 0x020000A4 RID: 164
public class BatchBakedActors : MonoBehaviour
{
	// Token: 0x06000259 RID: 601 RVA: 0x00003F5A File Offset: 0x0000215A
	private Vector2 Remap(Vector2 uv, Vector2 min, Vector2 max)
	{
		uv.x = Mathf.Lerp(min.x, max.x, uv.x);
		uv.y = Mathf.Lerp(min.y, max.y, uv.y);
		return uv;
	}

	// Token: 0x0400035A RID: 858
	public BatchBakedActors.BatchedBakedActor[] actors;

	// Token: 0x0400035B RID: 859
	public Material combinedMaterial;

	// Token: 0x020000A5 RID: 165
	[Serializable]
	public struct BatchedBakedActor
	{
		// Token: 0x0400035C RID: 860
		public BakeActor bakedActor;

		// Token: 0x0400035D RID: 861
		public int materialIndex;

		// Token: 0x0400035E RID: 862
		public Vector2 min;

		// Token: 0x0400035F RID: 863
		public Vector2 max;
	}
}
