using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x0200032F RID: 815
	[AddComponentMenu("")]
	public abstract class UIElementInfo : MonoBehaviour, ISelectHandler, IEventSystemHandler
	{
		// Token: 0x14000019 RID: 25
		// (add) Token: 0x060016AB RID: 5803 RVA: 0x0005EF7C File Offset: 0x0005D17C
		// (remove) Token: 0x060016AC RID: 5804 RVA: 0x0005EFB4 File Offset: 0x0005D1B4
		public event Action<GameObject> OnSelectedEvent;

		// Token: 0x060016AD RID: 5805 RVA: 0x0005EFE9 File Offset: 0x0005D1E9
		public void OnSelect(BaseEventData eventData)
		{
			if (this.OnSelectedEvent != null)
			{
				this.OnSelectedEvent(base.gameObject);
			}
		}

		// Token: 0x040018DB RID: 6363
		public string identifier;

		// Token: 0x040018DC RID: 6364
		public int intData;

		// Token: 0x040018DD RID: 6365
		public Text text;
	}
}
