using System;
using Rewired;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020003CA RID: 970
public class UIScrollToSelected : MonoBehaviour
{
	// Token: 0x170001ED RID: 493
	// (get) Token: 0x06001293 RID: 4755 RVA: 0x0000FBF6 File Offset: 0x0000DDF6
	private bool IsScrolling
	{
		get
		{
			return Time.time - this.scrollTime < 0.5f;
		}
	}

	// Token: 0x170001EE RID: 494
	// (get) Token: 0x06001294 RID: 4756 RVA: 0x0000FC0B File Offset: 0x0000DE0B
	private bool ShouldCheckScroll
	{
		get
		{
			return Time.time - this.lastUINavigation < 0.05f;
		}
	}

	// Token: 0x06001295 RID: 4757 RVA: 0x0000FC20 File Offset: 0x0000DE20
	private void OnValidate()
	{
		if (this.scrollRect == null)
		{
			this.scrollRect = base.GetComponent<ScrollRect>();
		}
	}

	// Token: 0x06001296 RID: 4758 RVA: 0x0005B8DC File Offset: 0x00059ADC
	private void Awake()
	{
		this.scrollRT = this.scrollRect.GetComponent<RectTransform>();
		this.scrollRTHeight = this.scrollRT.rect.height * (1f - 2f * this.scrollBoundary);
		this.contentRT = this.scrollRect.content;
		this.rePlayer = ReInput.players.GetPlayer(0);
	}

	// Token: 0x06001297 RID: 4759 RVA: 0x0005B948 File Offset: 0x00059B48
	private void OnEnable()
	{
		this.worldCorners = new Vector3[4];
		this.eventSystem = EventSystem.current;
		this.smoothPosition = (this.position = (this.desiredPosition = this.scrollRect.verticalNormalizedPosition));
		this.lastUINavigation = Time.time;
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnUINavigate), 0, 33, "UIHorizontal");
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnUINavigate), 0, 33, "UIVertical");
	}

	// Token: 0x06001298 RID: 4760 RVA: 0x0000FC3C File Offset: 0x0000DE3C
	private void OnDisable()
	{
		this.rePlayer.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnUINavigate));
	}

	// Token: 0x06001299 RID: 4761 RVA: 0x0000FC55 File Offset: 0x0000DE55
	private void OnUINavigate(InputActionEventData obj)
	{
		this.lastUINavigation = Time.time;
	}

	// Token: 0x0600129A RID: 4762 RVA: 0x0000FC62 File Offset: 0x0000DE62
	private void LateUpdate()
	{
		this.UpdateScrollToSelected();
	}

	// Token: 0x0600129B RID: 4763 RVA: 0x0005B9D8 File Offset: 0x00059BD8
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

	// Token: 0x0600129C RID: 4764 RVA: 0x0000FC6A File Offset: 0x0000DE6A
	private float GetNormalizedPosition(float a, float b, float t)
	{
		return (t - a) / (b - a);
	}

	// Token: 0x0600129D RID: 4765 RVA: 0x0000FC73 File Offset: 0x0000DE73
	private void GetWorldMinMax(RectTransform rectTransform, out float min, out float max)
	{
		rectTransform.GetWorldCorners(this.worldCorners);
		min = this.worldCorners[0].y;
		max = this.worldCorners[1].y;
	}

	// Token: 0x0600129E RID: 4766 RVA: 0x0005BB00 File Offset: 0x00059D00
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

	// Token: 0x0600129F RID: 4767 RVA: 0x0005BC90 File Offset: 0x00059E90
	public void Scroll(float heightDelta)
	{
		this.scrollTime = Time.time;
		heightDelta *= 1080f / (float)Screen.currentResolution.height;
		this.desiredPosition += heightDelta / (this.scrollRect.content.rect.height - this.scrollRT.rect.height);
		this.desiredPosition = Mathf.Clamp01(this.desiredPosition);
	}

	// Token: 0x040017F3 RID: 6131
	public ScrollRect scrollRect;

	// Token: 0x040017F4 RID: 6132
	private RectTransform scrollRT;

	// Token: 0x040017F5 RID: 6133
	private float scrollRTHeight;

	// Token: 0x040017F6 RID: 6134
	private RectTransform contentRT;

	// Token: 0x040017F7 RID: 6135
	private EventSystem eventSystem;

	// Token: 0x040017F8 RID: 6136
	public float edgeThreshold = 0.2f;

	// Token: 0x040017F9 RID: 6137
	private GameObject selectedGameObject;

	// Token: 0x040017FA RID: 6138
	private Vector3[] worldCorners;

	// Token: 0x040017FB RID: 6139
	private float position;

	// Token: 0x040017FC RID: 6140
	private float smoothPosition;

	// Token: 0x040017FD RID: 6141
	private float desiredPosition;

	// Token: 0x040017FE RID: 6142
	public float scrollSmoothTime = 0.2f;

	// Token: 0x040017FF RID: 6143
	private float scrollVelocity;

	// Token: 0x04001800 RID: 6144
	public float scrollSpeed = 50f;

	// Token: 0x04001801 RID: 6145
	private float scrollTime = -1f;

	// Token: 0x04001802 RID: 6146
	private float lastUINavigation;

	// Token: 0x04001803 RID: 6147
	public float scrollBoundary;

	// Token: 0x04001804 RID: 6148
	private Player rePlayer;
}
