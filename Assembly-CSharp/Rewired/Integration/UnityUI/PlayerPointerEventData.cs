using System;
using System.Text;
using Rewired.UI;
using UnityEngine.EventSystems;

namespace Rewired.Integration.UnityUI
{
	// Token: 0x02000420 RID: 1056
	public class PlayerPointerEventData : PointerEventData
	{
		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060016F6 RID: 5878 RVA: 0x00011B36 File Offset: 0x0000FD36
		// (set) Token: 0x060016F7 RID: 5879 RVA: 0x00011B3E File Offset: 0x0000FD3E
		public int playerId { get; set; }

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060016F8 RID: 5880 RVA: 0x00011B47 File Offset: 0x0000FD47
		// (set) Token: 0x060016F9 RID: 5881 RVA: 0x00011B4F File Offset: 0x0000FD4F
		public int inputSourceIndex { get; set; }

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060016FA RID: 5882 RVA: 0x00011B58 File Offset: 0x0000FD58
		// (set) Token: 0x060016FB RID: 5883 RVA: 0x00011B60 File Offset: 0x0000FD60
		public IMouseInputSource mouseSource { get; set; }

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x060016FC RID: 5884 RVA: 0x00011B69 File Offset: 0x0000FD69
		// (set) Token: 0x060016FD RID: 5885 RVA: 0x00011B71 File Offset: 0x0000FD71
		public ITouchInputSource touchSource { get; set; }

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x060016FE RID: 5886 RVA: 0x00011B7A File Offset: 0x0000FD7A
		// (set) Token: 0x060016FF RID: 5887 RVA: 0x00011B82 File Offset: 0x0000FD82
		public PointerEventType sourceType { get; set; }

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001700 RID: 5888 RVA: 0x00011B8B File Offset: 0x0000FD8B
		// (set) Token: 0x06001701 RID: 5889 RVA: 0x00011B93 File Offset: 0x0000FD93
		public int buttonIndex { get; set; }

		// Token: 0x06001702 RID: 5890 RVA: 0x00011B9C File Offset: 0x0000FD9C
		public PlayerPointerEventData(EventSystem eventSystem)
			: base(eventSystem)
		{
			this.playerId = -1;
			this.inputSourceIndex = -1;
			this.buttonIndex = -1;
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x00061FDC File Offset: 0x000601DC
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
