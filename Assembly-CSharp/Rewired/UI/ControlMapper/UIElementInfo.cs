using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x0200047B RID: 1147
	[AddComponentMenu("")]
	public abstract class UIElementInfo : MonoBehaviour, ISelectHandler, IEventSystemHandler
	{
		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06001C35 RID: 7221 RVA: 0x0006EBFC File Offset: 0x0006CDFC
		// (remove) Token: 0x06001C36 RID: 7222 RVA: 0x0006EC34 File Offset: 0x0006CE34
		public event Action<GameObject> OnSelectedEvent;

		// Token: 0x06001C37 RID: 7223 RVA: 0x00015968 File Offset: 0x00013B68
		public void OnSelect(BaseEventData eventData)
		{
			if (this.OnSelectedEvent != null)
			{
				this.OnSelectedEvent(base.gameObject);
			}
		}

		// Token: 0x04001E04 RID: 7684
		public string identifier;

		// Token: 0x04001E05 RID: 7685
		public int intData;

		// Token: 0x04001E06 RID: 7686
		public Text text;
	}
}
