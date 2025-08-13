using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.Demos
{
	// Token: 0x0200033B RID: 827
	[AddComponentMenu("")]
	[RequireComponent(typeof(Image))]
	public class TouchJoystickExample : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IDragHandler
	{
		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x0600174D RID: 5965 RVA: 0x00062CD9 File Offset: 0x00060ED9
		// (set) Token: 0x0600174E RID: 5966 RVA: 0x00062CE1 File Offset: 0x00060EE1
		public Vector2 position { get; private set; }

		// Token: 0x0600174F RID: 5967 RVA: 0x00062CEA File Offset: 0x00060EEA
		private void Start()
		{
			if (SystemInfo.deviceType == DeviceType.Handheld)
			{
				this.allowMouseControl = false;
			}
			this.StoreOrigValues();
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x00062D04 File Offset: 0x00060F04
		private void Update()
		{
			if ((float)Screen.width != this.origScreenResolution.x || (float)Screen.height != this.origScreenResolution.y || Screen.orientation != this.origScreenOrientation)
			{
				this.Restart();
				this.StoreOrigValues();
			}
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x00062D50 File Offset: 0x00060F50
		private void Restart()
		{
			this.hasFinger = false;
			(base.transform as RectTransform).anchoredPosition = this.origAnchoredPosition;
			this.position = Vector2.zero;
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x00062D7C File Offset: 0x00060F7C
		private void StoreOrigValues()
		{
			this.origAnchoredPosition = (base.transform as RectTransform).anchoredPosition;
			this.origWorldPosition = base.transform.position;
			this.origScreenResolution = new Vector2((float)Screen.width, (float)Screen.height);
			this.origScreenOrientation = Screen.orientation;
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x00062DD4 File Offset: 0x00060FD4
		private void UpdateValue(Vector3 value)
		{
			Vector3 vector = this.origWorldPosition - value;
			vector.y = -vector.y;
			vector /= (float)this.radius;
			this.position = new Vector2(-vector.x, vector.y);
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x00062E22 File Offset: 0x00061022
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

		// Token: 0x06001755 RID: 5973 RVA: 0x00062E56 File Offset: 0x00061056
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

		// Token: 0x06001756 RID: 5974 RVA: 0x00062E84 File Offset: 0x00061084
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

		// Token: 0x06001757 RID: 5975 RVA: 0x00062F0B File Offset: 0x0006110B
		private static bool IsMousePointerId(int id)
		{
			return id == -1 || id == -2 || id == -3;
		}

		// Token: 0x04001925 RID: 6437
		public bool allowMouseControl = true;

		// Token: 0x04001926 RID: 6438
		public int radius = 50;

		// Token: 0x04001927 RID: 6439
		private Vector2 origAnchoredPosition;

		// Token: 0x04001928 RID: 6440
		private Vector3 origWorldPosition;

		// Token: 0x04001929 RID: 6441
		private Vector2 origScreenResolution;

		// Token: 0x0400192A RID: 6442
		private ScreenOrientation origScreenOrientation;

		// Token: 0x0400192B RID: 6443
		[NonSerialized]
		private bool hasFinger;

		// Token: 0x0400192C RID: 6444
		[NonSerialized]
		private int lastFingerId;
	}
}
