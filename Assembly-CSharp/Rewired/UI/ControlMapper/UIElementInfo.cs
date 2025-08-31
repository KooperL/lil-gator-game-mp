using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	[AddComponentMenu("")]
	public abstract class UIElementInfo : MonoBehaviour, ISelectHandler, IEventSystemHandler
	{
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

		public string identifier;

		public int intData;

		public Text text;
	}
}
