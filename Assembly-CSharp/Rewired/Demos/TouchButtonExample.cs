using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.Demos
{
	// Token: 0x02000497 RID: 1175
	[AddComponentMenu("")]
	[RequireComponent(typeof(Image))]
	public class TouchButtonExample : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
	{
		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001D2D RID: 7469 RVA: 0x0001653F File Offset: 0x0001473F
		// (set) Token: 0x06001D2E RID: 7470 RVA: 0x00016547 File Offset: 0x00014747
		public bool isPressed { get; private set; }

		// Token: 0x06001D2F RID: 7471 RVA: 0x00016550 File Offset: 0x00014750
		private void Awake()
		{
			if (SystemInfo.deviceType == 1)
			{
				this.allowMouseControl = false;
			}
		}

		// Token: 0x06001D30 RID: 7472 RVA: 0x00016561 File Offset: 0x00014761
		private void Restart()
		{
			this.isPressed = false;
		}

		// Token: 0x06001D31 RID: 7473 RVA: 0x0001656A File Offset: 0x0001476A
		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			if (!this.allowMouseControl && TouchButtonExample.IsMousePointerId(eventData.pointerId))
			{
				return;
			}
			this.isPressed = true;
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x00016589 File Offset: 0x00014789
		void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
		{
			if (!this.allowMouseControl && TouchButtonExample.IsMousePointerId(eventData.pointerId))
			{
				return;
			}
			this.isPressed = false;
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x000165A8 File Offset: 0x000147A8
		private static bool IsMousePointerId(int id)
		{
			return id == -1 || id == -2 || id == -3;
		}

		// Token: 0x04001E95 RID: 7829
		public bool allowMouseControl = true;
	}
}
