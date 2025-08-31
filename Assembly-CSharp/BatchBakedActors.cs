using System;
using UnityEngine;

public class BatchBakedActors : MonoBehaviour
{
	// Token: 0x06000221 RID: 545 RVA: 0x0000BC9F File Offset: 0x00009E9F
	private Vector2 Remap(Vector2 uv, Vector2 min, Vector2 max)
	{
		uv.x = Mathf.Lerp(min.x, max.x, uv.x);
		uv.y = Mathf.Lerp(min.y, max.y, uv.y);
		return uv;
	}

	public BatchBakedActors.BatchedBakedActor[] actors;

	public Material combinedMaterial;

	[Serializable]
	public struct BatchedBakedActor
	{
		public BakeActor bakedActor;

		public int materialIndex;

		public Vector2 min;

		public Vector2 max;
	}
}
