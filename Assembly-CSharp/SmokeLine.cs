using System;
using UnityEngine;

// Token: 0x02000155 RID: 341
[RequireComponent(typeof(LineRenderer))]
public class SmokeLine : MonoBehaviour
{
	// Token: 0x0600065E RID: 1630 RVA: 0x00006956 File Offset: 0x00004B56
	private void Awake()
	{
		this.lineRenderer = base.GetComponent<LineRenderer>();
		this.seed = Random.value * 100f;
	}

	// Token: 0x0600065F RID: 1631 RVA: 0x00006975 File Offset: 0x00004B75
	private void Start()
	{
		this.positions = new Vector3[this.lineRenderer.positionCount];
		this.lineRenderer.GetPositions(this.positions);
	}

	// Token: 0x06000660 RID: 1632 RVA: 0x0003093C File Offset: 0x0002EB3C
	private void Update()
	{
		this.t += Time.deltaTime * this.speed;
		if (this.lineRenderer.isVisible)
		{
			for (int i = 0; i < this.positions.Length; i++)
			{
				float num = (float)i / (float)(this.positions.Length - 1);
				float num2 = num * this.height;
				float num3 = this.displacementCurve.Evaluate(num);
				float num4 = num3 * (Mathf.PerlinNoise(this.seed, this.t - num2 * this.scale) - 0.5f);
				float num5 = num3 * (Mathf.PerlinNoise(this.seed + 1000f, this.t - num2 * this.scale) - 0.5f);
				this.positions[i] = new Vector3(num4, num2, num5);
			}
			this.lineRenderer.SetPositions(this.positions);
		}
	}

	// Token: 0x0400088B RID: 2187
	private float seed;

	// Token: 0x0400088C RID: 2188
	public float speed;

	// Token: 0x0400088D RID: 2189
	private float t;

	// Token: 0x0400088E RID: 2190
	public float height = 50f;

	// Token: 0x0400088F RID: 2191
	private LineRenderer lineRenderer;

	// Token: 0x04000890 RID: 2192
	public float scale = 1f;

	// Token: 0x04000891 RID: 2193
	public AnimationCurve displacementCurve;

	// Token: 0x04000892 RID: 2194
	private Vector3[] positions;
}
