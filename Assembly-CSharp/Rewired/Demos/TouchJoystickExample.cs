using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.Demos
{
	[AddComponentMenu("")]
	[RequireComponent(typeof(Image))]
	public class TouchJoystickExample : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IDragHandler
	{
		// (get) Token: 0x06001D95 RID: 7573 RVA: 0x00016A09 File Offset: 0x00014C09
		// (set) Token: 0x06001D96 RID: 7574 RVA: 0x00016A11 File Offset: 0x00014C11
		public Vector2 position { get; private set; }

		// Token: 0x06001D97 RID: 7575 RVA: 0x00016A1A File Offset: 0x00014C1A
		private void Start()
		{
			if (SystemInfo.deviceType == DeviceType.Handheld)
			{
				this.allowMouseControl = false;
			}
			this.StoreOrigValues();
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x00074344 File Offset: 0x00072544
		private void Update()
		{
			if ((float)Screen.width != this.origScreenResolution.x || (float)Screen.height != this.origScreenResolution.y || Screen.orientation != this.origScreenOrientation)
			{
				this.Restart();
				this.StoreOrigValues();
			}
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x00016A31 File Offset: 0x00014C31
		private void Restart()
		{
			this.hasFinger = false;
			(base.transform as RectTransform).anchoredPosition = this.origAnchoredPosition;
			this.position = Vector2.zero;
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x00074390 File Offset: 0x00072590
		private void StoreOrigValues()
		{
			this.origAnchoredPosition = (base.transform as RectTransform).anchoredPosition;
			this.origWorldPosition = base.transform.position;
			this.origScreenResolution = new Vector2((float)Screen.width, (float)Screen.height);
			this.origScreenOrientation = Screen.orientation;
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x000743E8 File Offset: 0x000725E8
		private void UpdateValue(Vector3 value)
		{
			Vector3 vector = this.origWorldPosition - value;
			vector.y = -vector.y;
			vector /= (float)this.radius;
			this.position = new Vector2(-vector.x, vector.y);
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x00016A5B File Offset: 0x00014C5B
		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			if (this.hasFinger)
			{
				return;
			}
			if (!this.allowMouseControl && TouchJoystickExample.IsMousePointerId(eventData.pointerId))
			{
				return;
			}
			this.hasFinger = true;
			this.lastFingerId = eventData.pointerId;
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x00016A8F File Offset: 0x00014C8F
		void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
		{
			if (eventData.pointerId != this.lastFingerId)
			{
				return;
			}
			if (!this.allowMouseControl && TouchJoystickExample.IsMousePointerId(eventData.pointerId))
			{
				return;
			}
			this.Restart();
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x00074438 File Offset: 0x00072638
		void IDragHandler.OnDrag(PointerEventData eventData)
		{
			if (!this.hasFinger || eventData.pointerId != this.lastFingerId)
			{
				return;
			}
			Vector3 vector = new Vector3(eventData.position.x - this.origWorldPosition.x, eventData.position.y - this.origWorldPosition.y);
			vector = Vector3.ClampMagnitude(vector, (float)this.radius);
			Vector3 vector2 = this.origWorldPosition + vector;
			base.transform.position = vector2;
			this.UpdateValue(vector2);
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x000169E8 File Offset: 0x00014BE8
		private static bool IsMousePointerId(int id)
		{
			return id == -1 || id == -2 || id == -3;
		}

		public bool allowMouseControl = true;

		public int radius = 50;

		private Vector2 origAnchoredPosition;

		private Vector3 origWorldPosition;

		private Vector2 origScreenResolution;

		private ScreenOrientation origScreenOrientation;

		[NonSerialized]
		private bool hasFinger;

		[NonSerialized]
		private int lastFingerId;
	}
}
