using System;
using System.Text;
using Rewired.UI;
using UnityEngine.EventSystems;

namespace Rewired.Integration.UnityUI
{
	public class PlayerPointerEventData : PointerEventData
	{
		// (get) Token: 0x0600134D RID: 4941 RVA: 0x00051F57 File Offset: 0x00050157
		// (set) Token: 0x0600134E RID: 4942 RVA: 0x00051F5F File Offset: 0x0005015F
		public int playerId { get; set; }

		// (get) Token: 0x0600134F RID: 4943 RVA: 0x00051F68 File Offset: 0x00050168
		// (set) Token: 0x06001350 RID: 4944 RVA: 0x00051F70 File Offset: 0x00050170
		public int inputSourceIndex { get; set; }

		// (get) Token: 0x06001351 RID: 4945 RVA: 0x00051F79 File Offset: 0x00050179
		// (set) Token: 0x06001352 RID: 4946 RVA: 0x00051F81 File Offset: 0x00050181
		public IMouseInputSource mouseSource { get; set; }

		// (get) Token: 0x06001353 RID: 4947 RVA: 0x00051F8A File Offset: 0x0005018A
		// (set) Token: 0x06001354 RID: 4948 RVA: 0x00051F92 File Offset: 0x00050192
		public ITouchInputSource touchSource { get; set; }

		// (get) Token: 0x06001355 RID: 4949 RVA: 0x00051F9B File Offset: 0x0005019B
		// (set) Token: 0x06001356 RID: 4950 RVA: 0x00051FA3 File Offset: 0x000501A3
		public PointerEventType sourceType { get; set; }

		// (get) Token: 0x06001357 RID: 4951 RVA: 0x00051FAC File Offset: 0x000501AC
		// (set) Token: 0x06001358 RID: 4952 RVA: 0x00051FB4 File Offset: 0x000501B4
		public int buttonIndex { get; set; }

		// Token: 0x06001359 RID: 4953 RVA: 0x00051FBD File Offset: 0x000501BD
		public PlayerPointerEventData(EventSystem eventSystem)
			: base(eventSystem)
		{
			this.playerId = -1;
			this.inputSourceIndex = -1;
			this.buttonIndex = -1;
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x00051FDC File Offset: 0x000501DC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("<b>Player Id</b>: " + this.playerId.ToString());
			string text = "<b>Mouse Source</b>: ";
			IMouseInputSource mouseSource = this.mouseSource;
			stringBuilder.AppendLine(text + ((mouseSource != null) ? mouseSource.ToString() : null));
			stringBuilder.AppendLine("<b>Input Source Index</b>: " + this.inputSourceIndex.ToString());
			string text2 = "<b>Touch Source/b>: ";
			ITouchInputSource touchSource = this.touchSource;
			stringBuilder.AppendLine(text2 + ((touchSource != null) ? touchSource.ToString() : null));
			stringBuilder.AppendLine("<b>Source Type</b>: " + this.sourceType.ToString());
			stringBuilder.AppendLine("<b>Button Index</b>: " + this.buttonIndex.ToString());
			stringBuilder.Append(base.ToString());
			return stringBuilder.ToString();
		}
	}
}
