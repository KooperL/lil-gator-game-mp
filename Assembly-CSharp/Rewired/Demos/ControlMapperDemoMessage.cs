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
		// Token: 0x060017C7 RID: 6087 RVA: 0x0006573B File Offset: 0x0006393B
		private void Awake()
		{
			if (this.controlMapper != null)
			{
				this.controlMapper.ScreenClosedEvent += this.OnControlMapperClosed;
				this.controlMapper.ScreenOpenedEvent += this.OnControlMapperOpened;
			}
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x00065779 File Offset: 0x00063979
		private void Start()
		{
			this.SelectDefault();
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x00065781 File Offset: 0x00063981
		private void OnControlMapperClosed()
		{
			base.gameObject.SetActive(true);
			base.StartCoroutine(this.SelectDefaultDeferred());
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x0006579C File Offset: 0x0006399C
		private void OnControlMapperOpened()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x000657AA File Offset: 0x000639AA
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

		// Token: 0x060017CC RID: 6092 RVA: 0x000657DD File Offset: 0x000639DD
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
