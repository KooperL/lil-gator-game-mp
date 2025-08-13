using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.Demos
{
	// Token: 0x0200033A RID: 826
	[AddComponentMenu("")]
	[RequireComponent(typeof(Image))]
	public class TouchButtonExample : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
	{
		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001745 RID: 5957 RVA: 0x00062C4F File Offset: 0x00060E4F
		// (set) Token: 0x06001746 RID: 5958 RVA: 0x00062C57 File Offset: 0x00060E57
		public bool isPressed { get; private set; }

		// Token: 0x06001747 RID: 5959 RVA: 0x00062C60 File Offset: 0x00060E60
		private void Awake()
		{
			if (SystemInfo.deviceType == DeviceType.Handheld)
			{
				this.allowMouseControl = false;
			}
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x00062C71 File Offset: 0x00060E71
		private void Restart()
		{
			this.isPressed = false;
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x00062C7A File Offset: 0x00060E7A
		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			if (!this.allowMouseControl && TouchButtonExample.IsMousePointerId(eventData.pointerId))
			{
				return;
			}
			this.isPressed = true;
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x00062C99 File Offset: 0x00060E99
		void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
		{
			if (!this.allowMouseControl && TouchButtonExample.IsMousePointerId(eventData.pointerId))
			{
				return;
			}
			this.isPressed = false;
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x00062CB8 File Offset: 0x00060EB8
		private static bool IsMousePointerId(int id)
		{
			return id == -1 || id == -2 || id == -3;
		}

		// Token: 0x04001923 RID: 6435
		public bool allowMouseControl = true;
	}
}
