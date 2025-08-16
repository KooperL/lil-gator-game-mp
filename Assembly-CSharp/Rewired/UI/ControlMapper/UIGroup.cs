using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	[AddComponentMenu("")]
	public class UIGroup : MonoBehaviour
	{
		// (get) Token: 0x06001C99 RID: 7321 RVA: 0x00015DA4 File Offset: 0x00013FA4
		// (set) Token: 0x06001C9A RID: 7322 RVA: 0x00015DC5 File Offset: 0x00013FC5
		public string labelText
		{
			get
			{
				if (!(this._label != null))
				{
					return string.Empty;
				}
				return this._label.text;
			}
			set
			{
				if (this._label == null)
				{
					return;
				}
				this._label.text = value;
			}
		}

		// (get) Token: 0x06001C9B RID: 7323 RVA: 0x00015DE2 File Offset: 0x00013FE2
		public Transform content
		{
			get
			{
				return this._content;
			}
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x00015DEA File Offset: 0x00013FEA
		public void SetLabelActive(bool state)
		{
			if (this._label == null)
			{
				return;
			}
			this._label.gameObject.SetActive(state);
		}

		[SerializeField]
		private Text _label;

		[SerializeField]
		private Transform _content;
	}
}
