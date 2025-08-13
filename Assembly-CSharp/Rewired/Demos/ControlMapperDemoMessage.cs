using System;
using System.Collections;
using Rewired.UI.ControlMapper;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.Demos
{
	// Token: 0x020004B0 RID: 1200
	[AddComponentMenu("")]
	public class ControlMapperDemoMessage : MonoBehaviour
	{
		// Token: 0x06001DC5 RID: 7621 RVA: 0x00016C35 File Offset: 0x00014E35
		private void Awake()
		{
			if (this.controlMapper != null)
			{
				this.controlMapper.ScreenClosedEvent += this.OnControlMapperClosed;
				this.controlMapper.ScreenOpenedEvent += this.OnControlMapperOpened;
			}
		}

		// Token: 0x06001DC6 RID: 7622 RVA: 0x00016C73 File Offset: 0x00014E73
		private void Start()
		{
			this.SelectDefault();
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x00016C7B File Offset: 0x00014E7B
		private void OnControlMapperClosed()
		{
			base.gameObject.SetActive(true);
			base.StartCoroutine(this.SelectDefaultDeferred());
		}

		// Token: 0x06001DC8 RID: 7624 RVA: 0x00009344 File Offset: 0x00007544
		private void OnControlMapperOpened()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x00016C96 File Offset: 0x00014E96
		private void SelectDefault()
		{
			if (EventSystem.current == null)
			{
				return;
			}
			if (this.defaultSelectable != null)
			{
				EventSystem.current.SetSelectedGameObject(this.defaultSelectable.gameObject);
			}
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x00016CC9 File Offset: 0x00014EC9
		private IEnumerator SelectDefaultDeferred()
		{
			yield return null;
			this.SelectDefault();
			yield break;
		}

		// Token: 0x04001F1D RID: 7965
		public ControlMapper controlMapper;

		// Token: 0x04001F1E RID: 7966
		public Selectable defaultSelectable;
	}
}
