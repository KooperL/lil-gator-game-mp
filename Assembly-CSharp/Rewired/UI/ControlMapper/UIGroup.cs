using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	[AddComponentMenu("")]
	public class UIGroup : MonoBehaviour
	{
		// (get) Token: 0x060016AF RID: 5807 RVA: 0x0005F00C File Offset: 0x0005D20C
		// (set) Token: 0x060016B0 RID: 5808 RVA: 0x0005F02D File Offset: 0x0005D22D
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

		// (get) Token: 0x060016B1 RID: 5809 RVA: 0x0005F04A File Offset: 0x0005D24A
		public Transform content
		{
			get
			{
				return this._content;
			}
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x0005F052 File Offset: 0x0005D252
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
