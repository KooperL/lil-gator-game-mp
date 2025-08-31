using System;
using Rewired;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIScrollToSelected : MonoBehaviour
{
	// (get) Token: 0x06000F7F RID: 3967 RVA: 0x0004A621 File Offset: 0x00048821
	private bool IsScrolling
	{
		get
		{
			return Time.time - this.scrollTime < 0.5f;
		}
	}

	// (get) Token: 0x06000F80 RID: 3968 RVA: 0x0004A636 File Offset: 0x00048836
	private bool ShouldCheckScroll
	{
		get
		{
			return Time.time - this.lastUINavigation < 0.05f;
		}
	}

	// Token: 0x06000F81 RID: 3969 RVA: 0x0004A64B File Offset: 0x0004884B
	private void OnValidate()
	{
		if (this.scrollRect == null)
		{
			this.scrollRect = base.GetComponent<ScrollRect>();
		}
	}

	// Token: 0x06000F82 RID: 3970 RVA: 0x0004A668 File Offset: 0x00048868
	private void Awake()
	{
		this.scrollRT = this.scrollRect.GetComponent<RectTransform>();
		this.scrollRTHeight = this.scrollRT.rect.height * (1f - 2f * this.scrollBoundary);
		this.contentRT = this.scrollRect.content;
		this.rePlayer = ReInput.players.GetPlayer(0);
	}

	// Token: 0x06000F83 RID: 3971 RVA: 0x0004A6D4 File Offset: 0x000488D4
	private void OnEnable()
	{
		this.worldCorners = new Vector3[4];
		this.eventSystem = EventSystem.current;
		this.smoothPosition = (this.position = (this.desiredPosition = this.scrollRect.verticalNormalizedPosition));
		this.lastUINavigation = Time.time;
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnUINavigate), UpdateLoopType.Update, InputActionEventType.AxisActive, "UIHorizontal");
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnUINavigate), UpdateLoopType.Update, InputActionEventType.AxisActive, "UIVertical");
	}

	// Token: 0x06000F84 RID: 3972 RVA: 0x0004A764 File Offset: 0x00048964
	private void OnDisable()
	{
		this.rePlayer.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnUINavigate));
	}

	// Token: 0x06000F85 RID: 3973 RVA: 0x0004A77D File Offset: 0x0004897D
	private void OnUINavigate(InputActionEventData obj)
	{
		this.lastUINavigation = Time.time;
	}

	// Token: 0x06000F86 RID: 3974 RVA: 0x0004A78A File Offset: 0x0004898A
	private void LateUpdate()
	{
		this.UpdateScrollToSelected();
	}

	// Token: 0x06000F87 RID: 3975 RVA: 0x0004A794 File Offset: 0x00048994
	private void UpdatePositionBad()
	{
		this.selectedGameObject = this.eventSystem.currentSelectedGameObject;
		if (this.selectedGameObject != null)
		{
			float num;
			float num2;
			this.GetWorldMinMax(this.scrollRect.content, out num, out num2);
			float num3;
			float num4;
			this.GetWorldMinMax(this.scrollRect.viewport, out num3, out num4);
			float num5 = (num2 - num) / (num4 - num3);
			float num6 = Mathf.Sign(this.selectedGameObject.transform.position.y - Mathf.Lerp(num3, num4, 0.5f));
			RectTransform component = this.selectedGameObject.GetComponent<RectTransform>();
			float num7;
			float num8;
			this.GetWorldMinMax(component, out num7, out num8);
			float num9 = ((num6 > 0f) ? num8 : num7);
			this.GetNormalizedPosition(num, num2, num9);
			if (Mathf.Abs(this.GetNormalizedPosition(num3, num4, num9) - 0.5f) >= 0.5f - this.edgeThreshold)
			{
				float num10 = this.scrollSpeed / (num2 - num);
				this.position = Mathf.MoveTowards(this.position, Mathf.Clamp01(this.position + num6), Time.deltaTime * num10);
				this.scrollRect.verticalNormalizedPosition = this.position;
			}
		}
	}

	// Token: 0x06000F88 RID: 3976 RVA: 0x0004A8BA File Offset: 0x00048ABA
	private float GetNormalizedPosition(float a, float b, float t)
	{
		return (t - a) / (b - a);
	}

	// Token: 0x06000F89 RID: 3977 RVA: 0x0004A8C3 File Offset: 0x00048AC3
	private void GetWorldMinMax(RectTransform rectTransform, out float min, out float max)
	{
		rectTransform.GetWorldCorners(this.worldCorners);
		min = this.worldCorners[0].y;
		max = this.worldCorners[1].y;
	}

	// Token: 0x06000F8A RID: 3978 RVA: 0x0004A8F8 File Offset: 0x00048AF8
	private void UpdateScrollToSelected()
	{
		GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
		if (currentSelectedGameObject == null)
		{
			return;
		}
		if (currentSelectedGameObject.transform.parent != this.contentRT.transform)
		{
			return;
		}
		RectTransform component = currentSelectedGameObject.GetComponent<RectTransform>();
		Vector3 vector = this.scrollRT.localPosition - component.localPosition;
		float num = this.scrollRect.content.rect.height - this.scrollRTHeight;
		float num2 = this.contentRT.rect.height - vector.y;
		float num3 = this.scrollRect.normalizedPosition.y * num;
		float num4 = num3 - component.rect.height / 2f + this.scrollRTHeight;
		float num5 = num3 + component.rect.height / 2f;
		if (!this.IsScrolling && this.ShouldCheckScroll)
		{
			this.desiredPosition = this.scrollRect.normalizedPosition.y;
			if (num2 > num4)
			{
				float num6 = num2 - num4;
				float num7 = (num3 + num6) / num;
				this.desiredPosition = num7;
			}
			else if (num2 < num5)
			{
				float num8 = num2 - num5;
				float num9 = (num3 + num8) / num;
				this.desiredPosition = num9;
			}
		}
		float num10 = Mathf.SmoothDamp(this.scrollRect.normalizedPosition.y, this.desiredPosition, ref this.scrollVelocity, this.scrollSmoothTime);
		this.scrollRect.normalizedPosition = new Vector2(0f, num10);
	}

	// Token: 0x06000F8B RID: 3979 RVA: 0x0004AA88 File Offset: 0x00048C88
	public void Scroll(float heightDelta)
	{
		this.scrollTime = Time.time;
		heightDelta *= 1080f / (float)Screen.currentResolution.height;
		this.desiredPosition += heightDelta / (this.scrollRect.content.rect.height - this.scrollRT.rect.height);
		this.desiredPosition = Mathf.Clamp01(this.desiredPosition);
	}

	public ScrollRect scrollRect;

	private RectTransform scrollRT;

	private float scrollRTHeight;

	private RectTransform contentRT;

	private EventSystem eventSystem;

	public float edgeThreshold = 0.2f;

	private GameObject selectedGameObject;

	private Vector3[] worldCorners;

	private float position;

	private float smoothPosition;

	private float desiredPosition;

	public float scrollSmoothTime = 0.2f;

	private float scrollVelocity;

	public float scrollSpeed = 50f;

	private float scrollTime = -1f;

	private float lastUINavigation;

	public float scrollBoundary;

	private global::Rewired.Player rePlayer;
}
