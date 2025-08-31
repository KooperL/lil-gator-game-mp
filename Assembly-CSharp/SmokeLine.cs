using System;
using UnityEngine;

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

	private float seed;

	public float speed;

	private float t;

	public float height = 50f;

	private LineRenderer lineRenderer;

	public float scale = 1f;

	public AnimationCurve displacementCurve;

	private Vector3[] positions;
}
