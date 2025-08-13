using System;
using UnityEngine;

// Token: 0x02000082 RID: 130
public class BatchBakedActors : MonoBehaviour
{
	// Token: 0x06000221 RID: 545 RVA: 0x0000BC9F File Offset: 0x00009E9F
	private Vector2 Remap(Vector2 uv, Vector2 min, Vector2 max)
	{
		uv.x = Mathf.Lerp(min.x, max.x, uv.x);
		uv.y = Mathf.Lerp(min.y, max.y, uv.y);
		return uv;
	}

	// Token: 0x040002CE RID: 718
	public BatchBakedActors.BatchedBakedActor[] actors;

	// Token: 0x040002CF RID: 719
	public Material combinedMaterial;

	// Token: 0x02000373 RID: 883
	[Serializable]
	public struct BatchedBakedActor
	{
		// Token: 0x04001A6A RID: 6762
		public BakeActor bakedActor;

		// Token: 0x04001A6B RID: 6763
		public int materialIndex;

		// Token: 0x04001A6C RID: 6764
		public Vector2 min;

		// Token: 0x04001A6D RID: 6765
		public Vector2 max;
	}
}
