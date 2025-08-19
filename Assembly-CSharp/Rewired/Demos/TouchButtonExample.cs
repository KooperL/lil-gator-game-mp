using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.Demos
{
	[AddComponentMenu("")]
	[RequireComponent(typeof(Image))]
	public class TouchButtonExample : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
	{
		// (get) Token: 0x06001D8D RID: 7565 RVA: 0x0001697F File Offset: 0x00014B7F
		// (set) Token: 0x06001D8E RID: 7566 RVA: 0x00016987 File Offset: 0x00014B87
		public bool isPressed { get; private set; }

		// Token: 0x06001D8F RID: 7567 RVA: 0x00016990 File Offset: 0x00014B90
		private void Awake()
		{
			if (SystemInfo.deviceType == DeviceType.Handheld)
			{
				this.allowMouseControl = false;
			}
		}

		// Token: 0x06001D90 RID: 7568 RVA: 0x000169A1 File Offset: 0x00014BA1
		private void Restart()
		{
			this.isPressed = false;
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x000169AA File Offset: 0x00014BAA
		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			if (!this.allowMouseControl && TouchButtonExample.IsMousePointerId(eventData.pointerId))
			{
				return;
			}
			this.isPressed = true;
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x000169C9 File Offset: 0x00014BC9
		void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
		{
			if (!this.allowMouseControl && TouchButtonExample.IsMousePointerId(eventData.pointerId))
			{
				return;
			}
			this.isPressed = false;
		}

		// Token: 0x06001D93 RID: 7571 RVA: 0x000169E8 File Offset: 0x00014BE8
		private static bool IsMousePointerId(int id)
		{
			return id == -1 || id == -2 || id == -3;
		}

		public bool allowMouseControl = true;
	}
}
