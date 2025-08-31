using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	[AddComponentMenu("")]
	public class InputRow : MonoBehaviour
	{
		// (get) Token: 0x060015B6 RID: 5558 RVA: 0x0005D095 File Offset: 0x0005B295
		// (set) Token: 0x060015B7 RID: 5559 RVA: 0x0005D09D File Offset: 0x0005B29D
		public ButtonInfo[] buttons { get; private set; }

		// Token: 0x060015B8 RID: 5560 RVA: 0x0005D0A6 File Offset: 0x0005B2A6
		public void Initialize(int rowIndex, string label, Action<int, ButtonInfo> inputFieldActivatedCallback)
		{
			this.rowIndex = rowIndex;
			this.label.text = label;
			this.inputFieldActivatedCallback = inputFieldActivatedCallback;
			this.buttons = base.transform.GetComponentsInChildren<ButtonInfo>(true);
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x0005D0D4 File Offset: 0x0005B2D4
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
