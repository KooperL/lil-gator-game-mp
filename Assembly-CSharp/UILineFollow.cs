using System;
using UnityEngine;

// Token: 0x020003B2 RID: 946
public class UILineFollow : MonoBehaviour
{
	// Token: 0x170001DC RID: 476
	// (get) Token: 0x060011FB RID: 4603 RVA: 0x0000F535 File Offset: 0x0000D735
	private float CorrectedSourceDistance
	{
		get
		{
			return ((this.canvas.renderMode == 1) ? 1f : 2f) * this.sourceDistance;
		}
	}

	// Token: 0x060011FC RID: 4604 RVA: 0x0000F558 File Offset: 0x0000D758
	private void OnValidate()
	{
		if (this.canvas == null)
		{
			this.canvas = base.GetComponentInParent<Canvas>();
		}
	}

	// Token: 0x060011FD RID: 4605 RVA: 0x00059B68 File Offset: 0x00057D68
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

	// Token: 0x060011FE RID: 4606 RVA: 0x0000F574 File Offset: 0x0000D774
	private void Start()
	{
		this.width = this.lineRenderer.widthMultiplier;
		this.perlinTime = Time.time;
	}

	// Token: 0x060011FF RID: 4607 RVA: 0x00059C04 File Offset: 0x00057E04
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

	// Token: 0x06001200 RID: 4608 RVA: 0x00059C70 File Offset: 0x00057E70
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
		if (this.canvas.renderMode == null)
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
			Ray ray;
			ray..ctor(this.mainCamera.transform.position, this.positions[Mathf.FloorToInt((float)this.positions.Length * this.pullT)] - this.mainCamera.transform.position);
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

	// Token: 0x04001749 RID: 5961
	public Canvas canvas;

	// Token: 0x0400174A RID: 5962
	public RectTransform source;

	// Token: 0x0400174B RID: 5963
	public UIFollow sourceFollow;

	// Token: 0x0400174C RID: 5964
	public Vector3 sourceOffset;

	// Token: 0x0400174D RID: 5965
	public float sourceDistance = 5f;

	// Token: 0x0400174E RID: 5966
	public Transform target;

	// Token: 0x0400174F RID: 5967
	public Vector3 targetOffset = Vector3.up;

	// Token: 0x04001750 RID: 5968
	public float targetDistance;

	// Token: 0x04001751 RID: 5969
	public float maxLineLength = 10f;

	// Token: 0x04001752 RID: 5970
	public float maxScreenLength = 0.25f;

	// Token: 0x04001753 RID: 5971
	private LineRenderer lineRenderer;

	// Token: 0x04001754 RID: 5972
	private Camera mainCamera;

	// Token: 0x04001755 RID: 5973
	private Vector3[] positions;

	// Token: 0x04001756 RID: 5974
	private float width;

	// Token: 0x04001757 RID: 5975
	public float t;

	// Token: 0x04001758 RID: 5976
	private Bounds viewBounds;

	// Token: 0x04001759 RID: 5977
	public float perlinAmount;

	// Token: 0x0400175A RID: 5978
	public float perlinSpeed;

	// Token: 0x0400175B RID: 5979
	public float perlinSpacing;

	// Token: 0x0400175C RID: 5980
	public float perlinFramerate;

	// Token: 0x0400175D RID: 5981
	private float perlinTime;

	// Token: 0x0400175E RID: 5982
	public RectTransform pullTransform;

	// Token: 0x0400175F RID: 5983
	private Vector2 pullDefaultPosition;

	// Token: 0x04001760 RID: 5984
	public float pullAmount = 0.5f;

	// Token: 0x04001761 RID: 5985
	public float pullMaxDistance = 10f;

	// Token: 0x04001762 RID: 5986
	public float pullT = 0.5f;

	// Token: 0x04001763 RID: 5987
	public float pullPerlin = 20f;

	// Token: 0x04001764 RID: 5988
	public Transform lineScaleReference;
}
