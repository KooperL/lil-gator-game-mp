using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	[AddComponentMenu("")]
	public abstract class UIElementInfo : MonoBehaviour, ISelectHandler, IEventSystemHandler
	{
		// (add) Token: 0x06001C96 RID: 7318 RVA: 0x00070E70 File Offset: 0x0006F070
		// (remove) Token: 0x06001C97 RID: 7319 RVA: 0x00070EA8 File Offset: 0x0006F0A8
		public event Action<GameObject> OnSelectedEvent;

		// Token: 0x06001C98 RID: 7320 RVA: 0x00015DA8 File Offset: 0x00013FA8
		public void OnSelect(BaseEventData eventData)
		{
			if (this.OnSelectedEvent != null)
			{
				this.OnSelectedEvent(base.gameObject);
			}
		}

		public string identifier;

		public int intData;

		public Text text;
	}
}
