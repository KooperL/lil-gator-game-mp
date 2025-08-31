using System;
using UnityEngine;

public class UILineFollow : MonoBehaviour
{
	// (get) Token: 0x06000F17 RID: 3863 RVA: 0x000488FA File Offset: 0x00046AFA
	private float CorrectedSourceDistance
	{
		get
		{
			return ((this.canvas.renderMode == RenderMode.ScreenSpaceCamera) ? 1f : 2f) * this.sourceDistance;
		}
	}

	// Token: 0x06000F18 RID: 3864 RVA: 0x0004891D File Offset: 0x00046B1D
	private void OnValidate()
	{
		if (this.canvas == null)
		{
			this.canvas = base.GetComponentInParent<Canvas>();
		}
	}

	// Token: 0x06000F19 RID: 3865 RVA: 0x0004893C File Offset: 0x00046B3C
	private void Awake()
	{
		this.lineRenderer = base.GetComponent<LineRenderer>();
		this.positions = new Vector3[this.lineRenderer.positionCount];
		this.lineRenderer.GetPositions(this.positions);
		this.mainCamera = Camera.main;
		this.viewBounds = new Bounds(0.5f * Vector3.one, new Vector3(1f, 1f, 5000f));
		if (this.pullTransform != null)
		{
			this.pullDefaultPosition = this.pullTransform.anchoredPosition;
		}
	}

	// Token: 0x06000F1A RID: 3866 RVA: 0x000489D5 File Offset: 0x00046BD5
	private void Start()
	{
		this.width = this.lineRenderer.widthMultiplier;
		this.perlinTime = Time.time;
	}

	// Token: 0x06000F1B RID: 3867 RVA: 0x000489F4 File Offset: 0x00046BF4
	public void SetTarget(DialogueActor actor)
	{
		this.target = actor.DialogueAnchor;
		if (actor.profile != null)
		{
			this.lineRenderer.startColor = actor.profile.brightColor;
			this.lineRenderer.endColor = actor.profile.brightColor;
		}
		this.t = 0f;
		this.perlinTime = Time.time;
	}

	// Token: 0x06000F1C RID: 3868 RVA: 0x00048A60 File Offset: 0x00046C60
	private void LateUpdate()
	{
		if (this.target == null)
		{
			return;
		}
		if (Time.time - this.perlinTime > 1f / this.perlinFramerate)
		{
			this.perlinTime = Time.time;
		}
		Vector3 vector;
		if (this.canvas.renderMode == RenderMode.ScreenSpaceOverlay)
		{
			vector = this.mainCamera.ScreenPointToRay(this.source.position).GetPoint(this.CorrectedSourceDistance) + this.sourceOffset;
		}
		else
		{
			vector = this.source.position + this.sourceOffset;
		}
		Vector3 vector2 = this.target.position + this.targetOffset;
		float num = Vector3.Angle(this.mainCamera.transform.forward, vector2 - this.mainCamera.transform.position);
		Vector3 vector3 = this.mainCamera.transform.position + this.mainCamera.transform.forward * (vector - this.mainCamera.transform.position).magnitude;
		Vector3 vector4 = vector3 + 2f * (vector - vector3);
		Vector3 vector5 = Vector3.RotateTowards(this.mainCamera.transform.forward, vector2 - this.mainCamera.transform.position, 1.3962634f, float.PositiveInfinity);
		vector2 = this.mainCamera.transform.position + vector5;
		vector2 = Vector3.Lerp(vector2, vector4, Mathf.InverseLerp(70f, 100f, num));
		vector2 = Vector3.MoveTowards(vector2, vector, this.targetDistance);
		Vector3 vector6 = this.mainCamera.WorldToViewportPoint(vector);
		Vector3 vector7 = this.mainCamera.WorldToViewportPoint(vector2);
		bool flag = false;
		vector7 *= Mathf.Sign(vector7.z);
		vector7.z = 0f;
		vector6 *= Mathf.Sign(vector6.z);
		vector6.z = 0f;
		if (Vector3.Distance(vector7, vector6) > this.maxScreenLength)
		{
			vector7 = Vector3.MoveTowards(vector6, vector7, this.maxScreenLength);
			flag = true;
		}
		if (flag)
		{
			float num2 = Mathf.Max(this.mainCamera.transform.InverseTransformPoint(vector2).z, 5f);
			vector7.z = 1f;
			vector2 = this.mainCamera.ViewportPointToRay(vector7).GetPoint(num2);
		}
		vector2 = Vector3.MoveTowards(vector, vector2, this.maxLineLength);
		if (this.t < 1f)
		{
			this.lineRenderer.widthMultiplier = this.t * this.width;
			vector2 = Vector3.Lerp(vector, vector2, this.t);
		}
		else
		{
			this.lineRenderer.widthMultiplier = this.width;
		}
		if (this.lineScaleReference != null)
		{
			this.lineRenderer.widthMultiplier *= this.lineScaleReference.localScale.x;
		}
		this.lineRenderer.widthMultiplier *= this.mainCamera.fieldOfView / 50f;
		for (int i = 0; i < this.positions.Length; i++)
		{
			Vector3 vector8 = Vector3.Lerp(vector, vector2, (float)i / (float)(this.positions.Length - 1));
			if (i != 0 && i != this.positions.Length - 1)
			{
				float num3 = this.perlinSpacing * (float)i / (float)this.positions.Length + this.perlinSpeed * this.perlinTime;
				vector8 += this.perlinAmount * new Vector3(Mathf.PerlinNoise(num3, 0f) - 0.5f, Mathf.PerlinNoise(num3, 100f) - 0.5f, Mathf.PerlinNoise(num3, 200f) - 0.5f);
			}
			this.positions[i] = vector8;
		}
		this.lineRenderer.SetPositions(this.positions);
		if (this.pullTransform != null)
		{
			Ray ray = new Ray(this.mainCamera.transform.position, this.positions[Mathf.FloorToInt((float)this.positions.Length * this.pullT)] - this.mainCamera.transform.position);
			Vector3 point = ray.GetPoint((this.pullTransform.parent.TransformPoint(this.pullDefaultPosition) - this.mainCamera.transform.position).magnitude);
			Vector3 vector9 = this.pullTransform.parent.InverseTransformPoint(point);
			vector9.z = 0f;
			Vector2 vector10 = Vector3.Lerp(this.pullDefaultPosition, vector9, this.pullAmount);
			vector10 = Vector3.MoveTowards(this.pullDefaultPosition, vector10, this.pullMaxDistance);
			float num4 = this.perlinSpeed * this.perlinTime;
			vector10 += this.pullPerlin * new Vector2(Mathf.PerlinNoise(num4, 1000f) - 0.5f, Mathf.PerlinNoise(num4, 1500f) - 0.5f);
			this.pullTransform.anchoredPosition = vector10;
		}
	}

	public Canvas canvas;

	public RectTransform source;

	public UIFollow sourceFollow;

	public Vector3 sourceOffset;

	public float sourceDistance = 5f;

	public Transform target;

	public Vector3 targetOffset = Vector3.up;

	public float targetDistance;

	public float maxLineLength = 10f;

	public float maxScreenLength = 0.25f;

	private LineRenderer lineRenderer;

	private Camera mainCamera;

	private Vector3[] positions;

	private float width;

	public float t;

	private Bounds viewBounds;

	public float perlinAmount;

	public float perlinSpeed;

	public float perlinSpacing;

	public float perlinFramerate;

	private float perlinTime;

	public RectTransform pullTransform;

	private Vector2 pullDefaultPosition;

	public float pullAmount = 0.5f;

	public float pullMaxDistance = 10f;

	public float pullT = 0.5f;

	public float pullPerlin = 20f;

	public Transform lineScaleReference;
}
