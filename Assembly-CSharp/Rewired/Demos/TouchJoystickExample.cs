using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.Demos
{
	// Token: 0x02000498 RID: 1176
	[AddComponentMenu("")]
	[RequireComponent(typeof(Image))]
	public class TouchJoystickExample : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IDragHandler
	{
		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001D35 RID: 7477 RVA: 0x000165C9 File Offset: 0x000147C9
		// (set) Token: 0x06001D36 RID: 7478 RVA: 0x000165D1 File Offset: 0x000147D1
		public Vector2 position { get; private set; }

		// Token: 0x06001D37 RID: 7479 RVA: 0x000165DA File Offset: 0x000147DA
		private void Start()
		{
			if (SystemInfo.deviceType == 1)
			{
				this.allowMouseControl = false;
			}
			this.StoreOrigValues();
		}

		// Token: 0x06001D38 RID: 7480 RVA: 0x000723BC File Offset: 0x000705BC
		private void Update()
		{
			if ((float)Screen.width != this.origScreenResolution.x || (float)Screen.height != this.origScreenResolution.y || Screen.orientation != this.origScreenOrientation)
			{
				this.Restart();
				this.StoreOrigValues();
			}
		}

		// Token: 0x06001D39 RID: 7481 RVA: 0x000165F1 File Offset: 0x000147F1
		private void Restart()
		{
			this.hasFinger = false;
			(base.transform as RectTransform).anchoredPosition = this.origAnchoredPosition;
			this.position = Vector2.zero;
		}

		// Token: 0x06001D3A RID: 7482 RVA: 0x00072408 File Offset: 0x00070608
		private void StoreOrigValues()
		{
			this.origAnchoredPosition = (base.transform as RectTransform).anchoredPosition;
			this.origWorldPosition = base.transform.position;
			this.origScreenResolution = new Vector2((float)Screen.width, (float)Screen.height);
			this.origScreenOrientation = Screen.orientation;
		}

		// Token: 0x06001D3B RID: 7483 RVA: 0x00072460 File Offset: 0x00070660
		private void UpdateValue(Vector3 value)
		{
			Vector3 vector = this.origWorldPosition - value;
			vector.y = -vector.y;
			vector /= (float)this.radius;
			this.position = new Vector2(-vector.x, vector.y);
		}

		// Token: 0x06001D3C RID: 7484 RVA: 0x0001661B File Offset: 0x0001481B
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

		// Token: 0x06001D3D RID: 7485 RVA: 0x0001664F File Offset: 0x0001484F
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

		// Token: 0x06001D3E RID: 7486 RVA: 0x000724B0 File Offset: 0x000706B0
		void IDragHandler.OnDrag(PointerEventData eventData)
		{
			if (!this.hasFinger || eventData.pointerId != this.lastFingerId)
			{
				return;
			}
			Vector3 vector;
			vector..ctor(eventData.position.x - this.origWorldPosition.x, eventData.position.y - this.origWorldPosition.y);
			vector = Vector3.ClampMagnitude(vector, (float)this.radius);
			Vector3 vector2 = this.origWorldPosition + vector;
			base.transform.position = vector2;
			this.UpdateValue(vector2);
		}

		// Token: 0x06001D3F RID: 7487 RVA: 0x000165A8 File Offset: 0x000147A8
		private static bool IsMousePointerId(int id)
		{
			return id == -1 || id == -2 || id == -3;
		}

		// Token: 0x04001E97 RID: 7831
		public bool allowMouseControl = true;

		// Token: 0x04001E98 RID: 7832
		public int radius = 50;

		// Token: 0x04001E99 RID: 7833
		private Vector2 origAnchoredPosition;

		// Token: 0x04001E9A RID: 7834
		private Vector3 origWorldPosition;

		// Token: 0x04001E9B RID: 7835
		private Vector2 origScreenResolution;

		// Token: 0x04001E9C RID: 7836
		private ScreenOrientation origScreenOrientation;

		// Token: 0x04001E9D RID: 7837
		[NonSerialized]
		private bool hasFinger;

		// Token: 0x04001E9E RID: 7838
		[NonSerialized]
		private int lastFingerId;
	}
}
