using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000460 RID: 1120
	[AddComponentMenu("")]
	public class InputRow : MonoBehaviour
	{
		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06001AE2 RID: 6882 RVA: 0x00014BB7 File Offset: 0x00012DB7
		// (set) Token: 0x06001AE3 RID: 6883 RVA: 0x00014BBF File Offset: 0x00012DBF
		public ButtonInfo[] buttons { get; private set; }

		// Token: 0x06001AE4 RID: 6884 RVA: 0x00014BC8 File Offset: 0x00012DC8
		public void Initialize(int rowIndex, string label, Action<int, ButtonInfo> inputFieldActivatedCallback)
		{
			this.rowIndex = rowIndex;
			this.label.text = label;
			this.inputFieldActivatedCallback = inputFieldActivatedCallback;
			this.buttons = base.transform.GetComponentsInChildren<ButtonInfo>(true);
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x00014BF6 File Offset: 0x00012DF6
		public void OnButtonActivated(ButtonInfo buttonInfo)
		{
			if (this.inputFieldActivatedCallback == null)
			{
				return;
			}
			this.inputFieldActivatedCallback(this.rowIndex, buttonInfo);
		}

		// Token: 0x04001D22 RID: 7458
		public Text label;

		// Token: 0x04001D24 RID: 7460
		private int rowIndex;

		// Token: 0x04001D25 RID: 7461
		private Action<int, ButtonInfo> inputFieldActivatedCallback;
	}
}
