using System;
using UnityEngine;

// Token: 0x020003A6 RID: 934
public class UIFollow : MonoBehaviour
{
	// Token: 0x060011B6 RID: 4534 RVA: 0x0005838C File Offset: 0x0005658C
	private void OnValidate()
	{
		if (this.canvas == null)
		{
			this.canvas = base.GetComponentInParent<Canvas>();
		}
		if (this.canvasTransform == null && this.canvas != null)
		{
			this.canvasTransform = this.canvas.GetComponent<RectTransform>();
		}
	}

	// Token: 0x060011B7 RID: 4535 RVA: 0x0000F20D File Offset: 0x0000D40D
	private void Awake()
	{
		this.rectTransform = base.transform as RectTransform;
	}

	// Token: 0x060011B8 RID: 4536 RVA: 0x000583E0 File Offset: 0x000565E0
	private void Start()
	{
		if (this.canvas == null)
		{
			this.canvas = base.GetComponentInParent<Canvas>();
		}
		if (this.canvasTransform == null && this.canvas != null)
		{
			this.canvasTransform = this.canvas.GetComponent<RectTransform>();
		}
		this.smoothPosition = (this.initialPosition = this.rectTransform.anchoredPosition);
		this.mainCamera = Camera.main;
	}

	// Token: 0x060011B9 RID: 4537 RVA: 0x0000F220 File Offset: 0x0000D420
	private void OnDisable()
	{
		this.smoothPosition = this.initialPosition;
	}

	// Token: 0x060011BA RID: 4538 RVA: 0x0000F22E File Offset: 0x0000D42E
	public void SetTarget(DialogueActor actor)
	{
		this.followTarget = actor.DialogueAnchor;
	}

	// Token: 0x060011BB RID: 4539 RVA: 0x0005845C File Offset: 0x0005665C
	private Vector3 GetViewportPoint(Vector3 worldPosition)
	{
		Vector3 vector = Vector3.RotateTowards(this.mainCamera.transform.forward, worldPosition - this.mainCamera.transform.position, 1.3962634f, float.PositiveInfinity);
		Vector3 vector2 = this.mainCamera.transform.position + vector;
		return this.mainCamera.WorldToViewportPoint(vector2) + this.screenOffset;
	}

	// Token: 0x060011BC RID: 4540 RVA: 0x000584D0 File Offset: 0x000566D0
	private void ClampToBoundsGood(ref Vector3 position)
	{
		this.rectTransform.anchoredPosition = position;
		this.rectTransform.ForceUpdateRectTransforms();
		Vector3 vector = this.canvasTransform.TransformPoint(position);
		Vector3 vector2 = this.boundsReference.TransformPoint(this.boundsReference.rect.min);
		Vector3 vector3 = this.boundsReference.TransformPoint(this.boundsReference.rect.max);
		Vector3 vector4 = this.boundsReference.TransformPoint(this.boundsReference.rect.center);
		Vector3 vector5 = this.bounds.TransformPoint(this.bounds.rect.min);
		Vector3 vector6 = this.bounds.TransformPoint(this.bounds.rect.max);
		Vector3 vector7 = this.bounds.TransformPoint(this.bounds.rect.center);
		if (this.canvas.renderMode != null)
		{
			vector = this.rectTransform.parent.TransformPoint(position);
			vector = this.canvasTransform.InverseTransformPoint(vector);
			vector2 = this.canvasTransform.InverseTransformPoint(vector2);
			vector3 = this.canvasTransform.InverseTransformPoint(vector3);
			vector4 = this.canvasTransform.InverseTransformPoint(vector4);
			vector5 = this.canvasTransform.InverseTransformPoint(vector5);
			vector6 = this.canvasTransform.InverseTransformPoint(vector6);
			vector7 = this.canvasTransform.InverseTransformPoint(vector7);
		}
		Debug.DrawLine(vector2, vector3);
		Debug.DrawLine(vector5, vector6);
		vector2 -= vector5;
		vector3 -= vector5;
		vector6 -= vector5;
		bool flag = false;
		Vector3 vector8 = vector;
		if (vector2.x < 0f)
		{
			vector8.x += -vector2.x;
			flag = true;
		}
		else if (vector3.x > vector6.x)
		{
			vector8.x -= vector3.x - vector6.x;
			flag = true;
		}
		if (vector2.y < 0f)
		{
			vector8.y += -vector2.y;
			flag = true;
		}
		else if (vector3.y > vector6.y)
		{
			vector8.y -= vector3.y - vector6.y;
			flag = true;
		}
		if (flag)
		{
			Vector3 normalized = (vector7 - vector4).normalized;
			Vector3 vector9 = vector8 - vector;
			Mathf.Max(vector9.x / normalized.x, vector9.y / normalized.y);
			if (Mathf.Abs(vector9.x / normalized.x) > Mathf.Abs(vector9.y / normalized.y))
			{
				vector9.y = vector9.x * (normalized.y / normalized.x);
			}
			else
			{
				vector9.x = vector9.y * (normalized.x / normalized.y);
			}
			vector += vector9;
			if (this.canvas.renderMode == null)
			{
				position = this.canvasTransform.InverseTransformPoint(vector);
			}
			else
			{
				vector = this.canvasTransform.TransformPoint(vector);
				position = this.rectTransform.parent.InverseTransformPoint(vector);
			}
		}
		this.clamped = flag;
	}

	// Token: 0x060011BD RID: 4541 RVA: 0x00058850 File Offset: 0x00056A50
	private void ClampToBoundsBad(ref Vector3 position)
	{
		this.rectTransform.anchoredPosition = position;
		this.rectTransform.ForceUpdateRectTransforms();
		Vector3 vector = this.canvasTransform.TransformPoint(position);
		Vector3 vector2 = this.boundsReference.TransformPoint(this.boundsReference.rect.min);
		Vector3 vector3 = this.boundsReference.TransformPoint(this.boundsReference.rect.max);
		Vector3 vector4 = this.bounds.TransformPoint(this.bounds.rect.min);
		Vector3 vector5 = this.bounds.TransformPoint(this.bounds.rect.max);
		if (this.canvas.renderMode != null)
		{
			vector = this.rectTransform.parent.TransformPoint(position);
			vector = this.canvasTransform.InverseTransformPoint(vector);
			vector2 = this.canvasTransform.InverseTransformPoint(vector2);
			vector3 = this.canvasTransform.InverseTransformPoint(vector3);
			vector4 = this.canvasTransform.InverseTransformPoint(vector4);
			vector5 = this.canvasTransform.InverseTransformPoint(vector5);
		}
		Debug.DrawLine(vector2, vector3);
		Debug.DrawLine(vector4, vector5);
		vector2 -= vector4;
		vector3 -= vector4;
		vector5 -= vector4;
		bool flag = false;
		if (vector2.x < 0f)
		{
			vector.x += -vector2.x;
			flag = true;
		}
		else if (vector3.x > vector5.x)
		{
			vector.x -= vector3.x - vector5.x;
			flag = true;
		}
		if (vector2.y < 0f)
		{
			vector.y += -vector2.y;
			flag = true;
		}
		else if (vector3.y > vector5.y)
		{
			vector.y -= vector3.y - vector5.y;
			flag = true;
		}
		if (flag)
		{
			if (this.canvas.renderMode == null)
			{
				position = this.canvasTransform.InverseTransformPoint(vector);
				return;
			}
			vector = this.canvasTransform.TransformPoint(vector);
			position = this.rectTransform.parent.InverseTransformPoint(vector);
		}
	}

	// Token: 0x060011BE RID: 4542 RVA: 0x00058AA0 File Offset: 0x00056CA0
	private void LateUpdate()
	{
		Vector3 vector = this.rectTransform.anchoredPosition;
		if (this.followTarget != null)
		{
			vector = this.GetViewportPoint(this.followTarget.position + this.offset + this.mainCamera.transform.rotation * this.localOffset) + this.screenOffset;
			float num = 1f;
			if (this.scaleDownOffscreen || this.scaleWithTarget)
			{
				this.scaleTransform.localScale = Vector3.one;
			}
			if (this.scaleWithTarget)
			{
				float magnitude = (this.mainCamera.transform.position - (this.followTarget.position + this.offset)).magnitude;
				num *= this.referenceDistance / Mathf.Max(magnitude, 1f);
			}
			vector *= this.canvasTransform.rect.size;
			Vector3 vector2 = vector;
			if (this.keepWithinBounds)
			{
				this.ClampToBoundsGood(ref vector);
			}
			if (this.maxDistance != Vector2.zero)
			{
				vector.x = Mathf.MoveTowards(this.initialPosition.x, vector.x, this.maxDistance.x);
				vector.y = Mathf.MoveTowards(this.initialPosition.y, vector.y, this.maxDistance.y);
			}
			if (this.scaleDownOffscreen && vector2 != vector)
			{
				num = Mathf.Lerp(num, this.offscreenScale, Mathf.InverseLerp(0f, 1000f, (vector2 - vector).magnitude));
			}
			if (this.scaleDownOffscreen || this.scaleWithTarget)
			{
				num = Mathf.Clamp(num, this.minScale, this.maxScale);
				this.scaleTransform.localScale = num * Vector3.one;
			}
		}
		else if (this.revertToPosition)
		{
			vector = this.revertPosition;
		}
		if (this.isSmooth)
		{
			if (this.smoothPosition == this.initialPosition || this.changedTarget)
			{
				this.smoothPosition = vector;
			}
			else
			{
				this.smoothPosition = Vector2.SmoothDamp(this.smoothPosition, vector, ref this.velocity, this.smoothTime);
			}
			vector = this.smoothPosition;
		}
		if (this.snapToPixel)
		{
			vector.x = (float)Mathf.FloorToInt(vector.x);
			vector.y = (float)Mathf.FloorToInt(vector.y);
		}
		this.rectTransform.anchoredPosition = vector;
		this.changedTarget = false;
	}

	// Token: 0x040016D7 RID: 5847
	private Vector2 initialPosition;

	// Token: 0x040016D8 RID: 5848
	private RectTransform rectTransform;

	// Token: 0x040016D9 RID: 5849
	public Transform followTarget;

	// Token: 0x040016DA RID: 5850
	public bool snapToPixel = true;

	// Token: 0x040016DB RID: 5851
	public bool keepWithinBounds;

	// Token: 0x040016DC RID: 5852
	[ConditionalHide("keepWithinBounds", true)]
	public RectTransform boundsReference;

	// Token: 0x040016DD RID: 5853
	[ConditionalHide("keepWithinBounds", true)]
	public RectTransform bounds;

	// Token: 0x040016DE RID: 5854
	public Vector3 offset = Vector3.up;

	// Token: 0x040016DF RID: 5855
	public Vector3 localOffset = Vector3.zero;

	// Token: 0x040016E0 RID: 5856
	public Vector3 screenOffset = Vector3.zero;

	// Token: 0x040016E1 RID: 5857
	private Camera mainCamera;

	// Token: 0x040016E2 RID: 5858
	public Canvas canvas;

	// Token: 0x040016E3 RID: 5859
	public RectTransform canvasTransform;

	// Token: 0x040016E4 RID: 5860
	public Vector2 maxDistance;

	// Token: 0x040016E5 RID: 5861
	public bool isSmooth;

	// Token: 0x040016E6 RID: 5862
	public float smoothTime = 1f;

	// Token: 0x040016E7 RID: 5863
	private Vector2 velocity;

	// Token: 0x040016E8 RID: 5864
	private Vector2 smoothPosition;

	// Token: 0x040016E9 RID: 5865
	private bool changedTarget = true;

	// Token: 0x040016EA RID: 5866
	public bool revertToPosition;

	// Token: 0x040016EB RID: 5867
	[ConditionalHide("revertToPosition", true)]
	public Vector2 revertPosition;

	// Token: 0x040016EC RID: 5868
	[Header("Scale")]
	public bool scaleWithTarget;

	// Token: 0x040016ED RID: 5869
	[ConditionalHide("scaleWithTarget", true)]
	public float referenceDistance = 10f;

	// Token: 0x040016EE RID: 5870
	public bool scaleDownOffscreen;

	// Token: 0x040016EF RID: 5871
	[ConditionalHide("scaleDownOffscreen", true)]
	public float offscreenScale = 0.25f;

	// Token: 0x040016F0 RID: 5872
	[ConditionalHide("scaleWithTarget", true, ConditionalSourceField2 = "scaleDownOffscreen", UseOrLogic = true)]
	public RectTransform scaleTransform;

	// Token: 0x040016F1 RID: 5873
	[ConditionalHide("scaleWithTarget", true, ConditionalSourceField2 = "scaleDownOffscreen", UseOrLogic = true)]
	public float minScale;

	// Token: 0x040016F2 RID: 5874
	[ConditionalHide("scaleWithTarget", true, ConditionalSourceField2 = "scaleDownOffscreen", UseOrLogic = true)]
	public float maxScale = 5f;

	// Token: 0x040016F3 RID: 5875
	[HideInInspector]
	public bool clamped;
}
