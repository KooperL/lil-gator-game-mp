using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000330 RID: 816
	[AddComponentMenu("")]
	public class UIGroup : MonoBehaviour
	{
		// Token: 0x1700040E RID: 1038
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

		// Token: 0x1700040F RID: 1039
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

		// Token: 0x040018DF RID: 6367
		[SerializeField]
		private Text _label;

		// Token: 0x040018E0 RID: 6368
		[SerializeField]
		private Transform _content;
	}
}
