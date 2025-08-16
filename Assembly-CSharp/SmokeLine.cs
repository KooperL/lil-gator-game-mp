using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SmokeLine : MonoBehaviour
{
	// Token: 0x06000698 RID: 1688 RVA: 0x00006C1C File Offset: 0x00004E1C
	private void Awake()
	{
		this.lineRenderer = base.GetComponent<LineRenderer>();
		this.seed = global::UnityEngine.Random.value * 100f;
	}

	// Token: 0x06000699 RID: 1689 RVA: 0x00006C3B File Offset: 0x00004E3B
	private void Start()
	{
		this.positions = new Vector3[this.lineRenderer.positionCount];
		this.lineRenderer.GetPositions(this.positions);
	}

	// Token: 0x0600069A RID: 1690 RVA: 0x00031EBC File Offset: 0x000300BC
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
