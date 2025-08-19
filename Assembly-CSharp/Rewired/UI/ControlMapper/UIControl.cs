using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	[AddComponentMenu("")]
	public class UIControl : MonoBehaviour
	{
		// (get) Token: 0x06001C86 RID: 7302 RVA: 0x00015CEF File Offset: 0x00013EEF
		public int id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x00015CF7 File Offset: 0x00013EF7
		private void Awake()
		{
			this._id = UIControl.GetNextUid();
		}

		// (get) Token: 0x06001C88 RID: 7304 RVA: 0x00015D04 File Offset: 0x00013F04
		// (set) Token: 0x06001C89 RID: 7305 RVA: 0x00015D0C File Offset: 0x00013F0C
		public bool showTitle
		{
			get
			{
				return this._showTitle;
			}
			set
			{
				if (this.title == null)
				{
					return;
				}
				this.title.gameObject.SetActive(value);
				this._showTitle = value;
			}
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x00002229 File Offset: 0x00000429
		public virtual void SetCancelCallback(Action cancelCallback)
		{
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x00015D35 File Offset: 0x00013F35
		private static int GetNextUid()
		{
			if (UIControl._uidCounter == 2147483647)
			{
				UIControl._uidCounter = 0;
			}
			int uidCounter = UIControl._uidCounter;
			UIControl._uidCounter++;
			return uidCounter;
		}

		public Text title;

		private int _id;

		private bool _showTitle;

		private static int _uidCounter;
	}
}
