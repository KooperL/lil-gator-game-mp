using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x0200032D RID: 813
	[AddComponentMenu("")]
	public class UIControl : MonoBehaviour
	{
		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x0600169F RID: 5791 RVA: 0x0005ED67 File Offset: 0x0005CF67
		public int id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x0005ED6F File Offset: 0x0005CF6F
		private void Awake()
		{
			this._id = UIControl.GetNextUid();
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x0005ED7C File Offset: 0x0005CF7C
		// (set) Token: 0x060016A2 RID: 5794 RVA: 0x0005ED84 File Offset: 0x0005CF84
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

		// Token: 0x060016A3 RID: 5795 RVA: 0x0005EDAD File Offset: 0x0005CFAD
		public virtual void SetCancelCallback(Action cancelCallback)
		{
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x0005EDAF File Offset: 0x0005CFAF
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

		// Token: 0x040018D5 RID: 6357
		public Text title;

		// Token: 0x040018D6 RID: 6358
		private int _id;

		// Token: 0x040018D7 RID: 6359
		private bool _showTitle;

		// Token: 0x040018D8 RID: 6360
		private static int _uidCounter;
	}
}
