using System;
using UnityEngine;

// Token: 0x02000102 RID: 258
[RequireComponent(typeof(LineRenderer))]
public class SmokeLine : MonoBehaviour
{
	// Token: 0x0600054C RID: 1356 RVA: 0x0001C344 File Offset: 0x0001A544
	private void Awake()
	{
		this.lineRenderer = base.GetComponent<LineRenderer>();
		this.seed = Random.value * 100f;
	}

	// Token: 0x0600054D RID: 1357 RVA: 0x0001C363 File Offset: 0x0001A563
	private void Start()
	{
		this.positions = new Vector3[this.lineRenderer.positionCount];
		this.lineRenderer.GetPositions(this.positions);
	}

	// Token: 0x0600054E RID: 1358 RVA: 0x0001C390 File Offset: 0x0001A590
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

	// Token: 0x04000744 RID: 1860
	private float seed;

	// Token: 0x04000745 RID: 1861
	public float speed;

	// Token: 0x04000746 RID: 1862
	private float t;

	// Token: 0x04000747 RID: 1863
	public float height = 50f;

	// Token: 0x04000748 RID: 1864
	private LineRenderer lineRenderer;

	// Token: 0x04000749 RID: 1865
	public float scale = 1f;

	// Token: 0x0400074A RID: 1866
	public AnimationCurve displacementCurve;

	// Token: 0x0400074B RID: 1867
	private Vector3[] positions;
}
