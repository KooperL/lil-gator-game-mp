using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x0200047C RID: 1148
	[AddComponentMenu("")]
	public class UIGroup : MonoBehaviour
	{
		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001C39 RID: 7225 RVA: 0x00015983 File Offset: 0x00013B83
		// (set) Token: 0x06001C3A RID: 7226 RVA: 0x000159A4 File Offset: 0x00013BA4
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

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001C3B RID: 7227 RVA: 0x000159C1 File Offset: 0x00013BC1
		public Transform content
		{
			get
			{
				return this._content;
			}
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x000159C9 File Offset: 0x00013BC9
		public void SetLabelActive(bool state)
		{
			if (this._label == null)
			{
				return;
			}
			this._label.gameObject.SetActive(state);
		}

		// Token: 0x04001E08 RID: 7688
		[SerializeField]
		private Text _label;

		// Token: 0x04001E09 RID: 7689
		[SerializeField]
		private Transform _content;
	}
}
