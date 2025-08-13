using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000478 RID: 1144
	[AddComponentMenu("")]
	public class UIControl : MonoBehaviour
	{
		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001C26 RID: 7206 RVA: 0x000158AF File Offset: 0x00013AAF
		public int id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x000158B7 File Offset: 0x00013AB7
		private void Awake()
		{
			this._id = UIControl.GetNextUid();
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001C28 RID: 7208 RVA: 0x000158C4 File Offset: 0x00013AC4
		// (set) Token: 0x06001C29 RID: 7209 RVA: 0x000158CC File Offset: 0x00013ACC
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

		// Token: 0x06001C2A RID: 7210 RVA: 0x00002229 File Offset: 0x00000429
		public virtual void SetCancelCallback(Action cancelCallback)
		{
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x000158F5 File Offset: 0x00013AF5
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

		// Token: 0x04001DFB RID: 7675
		public Text title;

		// Token: 0x04001DFC RID: 7676
		private int _id;

		// Token: 0x04001DFD RID: 7677
		private bool _showTitle;

		// Token: 0x04001DFE RID: 7678
		private static int _uidCounter;
	}
}
