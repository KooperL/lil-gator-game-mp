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
		// Token: 0x06001E25 RID: 7717 RVA: 0x0001706B File Offset: 0x0001526B
		private void Awake()
		{
			if (this.controlMapper != null)
			{
				this.controlMapper.ScreenClosedEvent += this.OnControlMapperClosed;
				this.controlMapper.ScreenOpenedEvent += this.OnControlMapperOpened;
			}
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x000170A9 File Offset: 0x000152A9
		private void Start()
		{
			this.SelectDefault();
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x000170B1 File Offset: 0x000152B1
		private void OnControlMapperClosed()
		{
			base.gameObject.SetActive(true);
			base.StartCoroutine(this.SelectDefaultDeferred());
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x000096A2 File Offset: 0x000078A2
		private void OnControlMapperOpened()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x000170CC File Offset: 0x000152CC
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

		// Token: 0x06001E2A RID: 7722 RVA: 0x000170FF File Offset: 0x000152FF
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
