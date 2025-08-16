using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	[AddComponentMenu("")]
	public abstract class UIElementInfo : MonoBehaviour, ISelectHandler, IEventSystemHandler
	{
		// (add) Token: 0x06001C95 RID: 7317 RVA: 0x00070A14 File Offset: 0x0006EC14
		// (remove) Token: 0x06001C96 RID: 7318 RVA: 0x00070A4C File Offset: 0x0006EC4C
		public event Action<GameObject> OnSelectedEvent;

		// Token: 0x06001C97 RID: 7319 RVA: 0x00015D89 File Offset: 0x00013F89
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
