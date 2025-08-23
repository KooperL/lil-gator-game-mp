using System;
using System.Collections;
using Rewired.UI.ControlMapper;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.Demos
{
	[AddComponentMenu("")]
	public class ControlMapperDemoMessage : MonoBehaviour
	{
		// Token: 0x06001E26 RID: 7718 RVA: 0x00017075 File Offset: 0x00015275
		private void Awake()
		{
			if (this.controlMapper != null)
			{
				this.controlMapper.ScreenClosedEvent += this.OnControlMapperClosed;
				this.controlMapper.ScreenOpenedEvent += this.OnControlMapperOpened;
			}
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x000170B3 File Offset: 0x000152B3
		private void Start()
		{
			this.SelectDefault();
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x000170BB File Offset: 0x000152BB
		private void OnControlMapperClosed()
		{
			base.gameObject.SetActive(true);
			base.StartCoroutine(this.SelectDefaultDeferred());
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x000096AC File Offset: 0x000078AC
		private void OnControlMapperOpened()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x000170D6 File Offset: 0x000152D6
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

		// Token: 0x06001E2B RID: 7723 RVA: 0x00017109 File Offset: 0x00015309
		private IEnumerator SelectDefaultDeferred()
		{
			yield return null;
			this.SelectDefault();
			yield break;
		}

		public ControlMapper controlMapper;

		public Selectable defaultSelectable;
	}
}
