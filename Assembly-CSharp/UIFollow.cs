using System;
using UnityEngine;

public class UIFollow : MonoBehaviour
{
	// Token: 0x06001216 RID: 4630 RVA: 0x0005A350 File Offset: 0x00058550
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

	// Token: 0x06001217 RID: 4631 RVA: 0x0000F5F6 File Offset: 0x0000D7F6
	private void Awake()
	{
		this.rectTransform = base.transform as RectTransform;
	}

	// Token: 0x06001218 RID: 4632 RVA: 0x0005A3A4 File Offset: 0x000585A4
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

	// Token: 0x06001219 RID: 4633 RVA: 0x0000F609 File Offset: 0x0000D809
	private void OnDisable()
	{
		this.smoothPosition = this.initialPosition;
	}

	// Token: 0x0600121A RID: 4634 RVA: 0x0000F617 File Offset: 0x0000D817
	public void SetTarget(DialogueActor actor)
	{
		this.followTarget = actor.DialogueAnchor;
	}

	// Token: 0x0600121B RID: 4635 RVA: 0x0005A420 File Offset: 0x00058620
	private Vector3 GetViewportPoint(Vector3 worldPosition)
	{
		Vector3 vector = Vector3.RotateTowards(this.mainCamera.transform.forward, worldPosition - this.mainCamera.transform.position, 1.3962634f, float.PositiveInfinity);
		Vector3 vector2 = this.mainCamera.transform.position + vector;
		return this.mainCamera.WorldToViewportPoint(vector2) + this.screenOffset;
	}

	// Token: 0x0600121C RID: 4636 RVA: 0x0005A494 File Offset: 0x00058694
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

	// Token: 0x0600121D RID: 4637 RVA: 0x0005A814 File Offset: 0x00058A14
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

	// Token: 0x0600121E RID: 4638 RVA: 0x0005AA64 File Offset: 0x00058C64
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

	private Vector2 initialPosition;

	private RectTransform rectTransform;

	public Transform followTarget;

	public bool snapToPixel = true;

	public bool keepWithinBounds;

	[ConditionalHide("keepWithinBounds", true)]
	public RectTransform boundsReference;

	[ConditionalHide("keepWithinBounds", true)]
	public RectTransform bounds;

	public Vector3 offset = Vector3.up;

	public Vector3 localOffset = Vector3.zero;

	public Vector3 screenOffset = Vector3.zero;

	private Camera mainCamera;

	public Canvas canvas;

	public RectTransform canvasTransform;

	public Vector2 maxDistance;

	public bool isSmooth;

	public float smoothTime = 1f;

	private Vector2 velocity;

	private Vector2 smoothPosition;

	private bool changedTarget = true;

	public bool revertToPosition;

	[ConditionalHide("revertToPosition", true)]
	public Vector2 revertPosition;

	[Header("Scale")]
	public bool scaleWithTarget;

	[ConditionalHide("scaleWithTarget", true)]
	public float referenceDistance = 10f;

	public bool scaleDownOffscreen;

	[ConditionalHide("scaleDownOffscreen", true)]
	public float offscreenScale = 0.25f;

	[ConditionalHide("scaleWithTarget", true, ConditionalSourceField2 = "scaleDownOffscreen", UseOrLogic = true)]
	public RectTransform scaleTransform;

	[ConditionalHide("scaleWithTarget", true, ConditionalSourceField2 = "scaleDownOffscreen", UseOrLogic = true)]
	public float minScale;

	[ConditionalHide("scaleWithTarget", true, ConditionalSourceField2 = "scaleDownOffscreen", UseOrLogic = true)]
	public float maxScale = 5f;

	[HideInInspector]
	public bool clamped;
}
