using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	[AddComponentMenu("")]
	public class UIControl : MonoBehaviour
	{
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

		public Text title;

		private int _id;

		private bool _showTitle;

		private static int _uidCounter;
	}
}
