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
		// Token: 0x06001E25 RID: 7717 RVA: 0x00017056 File Offset: 0x00015256
		private void Awake()
		{
			if (this.controlMapper != null)
			{
				this.controlMapper.ScreenClosedEvent += this.OnControlMapperClosed;
				this.controlMapper.ScreenOpenedEvent += this.OnControlMapperOpened;
			}
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x00017094 File Offset: 0x00015294
		private void Start()
		{
			this.SelectDefault();
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x0001709C File Offset: 0x0001529C
		private void OnControlMapperClosed()
		{
			base.gameObject.SetActive(true);
			base.StartCoroutine(this.SelectDefaultDeferred());
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x0000968D File Offset: 0x0000788D
		private void OnControlMapperOpened()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x000170B7 File Offset: 0x000152B7
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

		// Token: 0x06001E2A RID: 7722 RVA: 0x000170EA File Offset: 0x000152EA
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
