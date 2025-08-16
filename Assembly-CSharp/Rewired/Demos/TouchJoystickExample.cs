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
		// (get) Token: 0x06001D95 RID: 7573 RVA: 0x000169EA File Offset: 0x00014BEA
		// (set) Token: 0x06001D96 RID: 7574 RVA: 0x000169F2 File Offset: 0x00014BF2
		public Vector2 position { get; private set; }

		// Token: 0x06001D97 RID: 7575 RVA: 0x000169FB File Offset: 0x00014BFB
		private void Start()
		{
			if (SystemInfo.deviceType == DeviceType.Handheld)
			{
				this.allowMouseControl = false;
			}
			this.StoreOrigValues();
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x000741D4 File Offset: 0x000723D4
		private void Update()
		{
			if ((float)Screen.width != this.origScreenResolution.x || (float)Screen.height != this.origScreenResolution.y || Screen.orientation != this.origScreenOrientation)
			{
				this.Restart();
				this.StoreOrigValues();
			}
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x00016A12 File Offset: 0x00014C12
		private void Restart()
		{
			this.hasFinger = false;
			(base.transform as RectTransform).anchoredPosition = this.origAnchoredPosition;
			this.position = Vector2.zero;
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x00074220 File Offset: 0x00072420
		private void StoreOrigValues()
		{
			this.origAnchoredPosition = (base.transform as RectTransform).anchoredPosition;
			this.origWorldPosition = base.transform.position;
			this.origScreenResolution = new Vector2((float)Screen.width, (float)Screen.height);
			this.origScreenOrientation = Screen.orientation;
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x00074278 File Offset: 0x00072478
		private void UpdateValue(Vector3 value)
		{
			Vector3 vector = this.origWorldPosition - value;
			vector.y = -vector.y;
			vector /= (float)this.radius;
			this.position = new Vector2(-vector.x, vector.y);
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x00016A3C File Offset: 0x00014C3C
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

		// Token: 0x06001D9D RID: 7581 RVA: 0x00016A70 File Offset: 0x00014C70
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

		// Token: 0x06001D9E RID: 7582 RVA: 0x000742C8 File Offset: 0x000724C8
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

		// Token: 0x06001D9F RID: 7583 RVA: 0x000169C9 File Offset: 0x00014BC9
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
