using System;
using Rewired;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIScrollToSelected : MonoBehaviour
{
	// (get) Token: 0x060012F3 RID: 4851 RVA: 0x0000FFDE File Offset: 0x0000E1DE
	private bool IsScrolling
	{
		get
		{
			return Time.time - this.scrollTime < 0.5f;
		}
	}

	// (get) Token: 0x060012F4 RID: 4852 RVA: 0x0000FFF3 File Offset: 0x0000E1F3
	private bool ShouldCheckScroll
	{
		get
		{
			return Time.time - this.lastUINavigation < 0.05f;
		}
	}

	// Token: 0x060012F5 RID: 4853 RVA: 0x00010008 File Offset: 0x0000E208
	private void OnValidate()
	{
		if (this.scrollRect == null)
		{
			this.scrollRect = base.GetComponent<ScrollRect>();
		}
	}

	// Token: 0x060012F6 RID: 4854 RVA: 0x0005D770 File Offset: 0x0005B970
	private void Awake()
	{
		this.scrollRT = this.scrollRect.GetComponent<RectTransform>();
		this.scrollRTHeight = this.scrollRT.rect.height * (1f - 2f * this.scrollBoundary);
		this.contentRT = this.scrollRect.content;
		this.rePlayer = ReInput.players.GetPlayer(0);
	}

	// Token: 0x060012F7 RID: 4855 RVA: 0x0005D7DC File Offset: 0x0005B9DC
	private void OnEnable()
	{
		this.worldCorners = new Vector3[4];
		this.eventSystem = EventSystem.current;
		this.smoothPosition = (this.position = (this.desiredPosition = this.scrollRect.verticalNormalizedPosition));
		this.lastUINavigation = Time.time;
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnUINavigate), UpdateLoopType.Update, InputActionEventType.AxisActive, "UIHorizontal");
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnUINavigate), UpdateLoopType.Update, InputActionEventType.AxisActive, "UIVertical");
	}

	// Token: 0x060012F8 RID: 4856 RVA: 0x00010024 File Offset: 0x0000E224
	private void OnDisable()
	{
		this.rePlayer.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnUINavigate));
	}

	// Token: 0x060012F9 RID: 4857 RVA: 0x0001003D File Offset: 0x0000E23D
	private void OnUINavigate(InputActionEventData obj)
	{
		this.lastUINavigation = Time.time;
	}

	// Token: 0x060012FA RID: 4858 RVA: 0x0001004A File Offset: 0x0000E24A
	private void LateUpdate()
	{
		this.UpdateScrollToSelected();
	}

	// Token: 0x060012FB RID: 4859 RVA: 0x0005D86C File Offset: 0x0005BA6C
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

	// Token: 0x060012FC RID: 4860 RVA: 0x00010052 File Offset: 0x0000E252
	private float GetNormalizedPosition(float a, float b, float t)
	{
		return (t - a) / (b - a);
	}

	// Token: 0x060012FD RID: 4861 RVA: 0x0001005B File Offset: 0x0000E25B
	private void GetWorldMinMax(RectTransform rectTransform, out float min, out float max)
	{
		rectTransform.GetWorldCorners(this.worldCorners);
		min = this.worldCorners[0].y;
		max = this.worldCorners[1].y;
	}

	// Token: 0x060012FE RID: 4862 RVA: 0x0005D994 File Offset: 0x0005BB94
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

	// Token: 0x060012FF RID: 4863 RVA: 0x0005DB24 File Offset: 0x0005BD24
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
