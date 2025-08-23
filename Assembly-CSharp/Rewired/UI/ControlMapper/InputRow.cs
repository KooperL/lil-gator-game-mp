using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	[AddComponentMenu("")]
	public class InputRow : MonoBehaviour
	{
		// (get) Token: 0x06001B43 RID: 6979 RVA: 0x00014FBE File Offset: 0x000131BE
		// (set) Token: 0x06001B44 RID: 6980 RVA: 0x00014FC6 File Offset: 0x000131C6
		public ButtonInfo[] buttons { get; private set; }

		// Token: 0x06001B45 RID: 6981 RVA: 0x00014FCF File Offset: 0x000131CF
		public void Initialize(int rowIndex, string label, Action<int, ButtonInfo> inputFieldActivatedCallback)
		{
			this.rowIndex = rowIndex;
			this.label.text = label;
			this.inputFieldActivatedCallback = inputFieldActivatedCallback;
			this.buttons = base.transform.GetComponentsInChildren<ButtonInfo>(true);
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x00014FFD File Offset: 0x000131FD
		public void OnButtonActivated(ButtonInfo buttonInfo)
		{
			if (this.inputFieldActivatedCallback == null)
			{
				return;
			}
			this.inputFieldActivatedCallback(this.rowIndex, buttonInfo);
		}

		public Text label;

		private int rowIndex;

		private Action<int, ButtonInfo> inputFieldActivatedCallback;
	}
}
