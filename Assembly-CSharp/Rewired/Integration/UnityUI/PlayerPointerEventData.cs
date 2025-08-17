using System;
using System.Text;
using Rewired.UI;
using UnityEngine.EventSystems;

namespace Rewired.Integration.UnityUI
{
	public class PlayerPointerEventData : PointerEventData
	{
		// (get) Token: 0x06001756 RID: 5974 RVA: 0x00011F33 File Offset: 0x00010133
		// (set) Token: 0x06001757 RID: 5975 RVA: 0x00011F3B File Offset: 0x0001013B
		public int playerId { get; set; }

		// (get) Token: 0x06001758 RID: 5976 RVA: 0x00011F44 File Offset: 0x00010144
		// (set) Token: 0x06001759 RID: 5977 RVA: 0x00011F4C File Offset: 0x0001014C
		public int inputSourceIndex { get; set; }

		// (get) Token: 0x0600175A RID: 5978 RVA: 0x00011F55 File Offset: 0x00010155
		// (set) Token: 0x0600175B RID: 5979 RVA: 0x00011F5D File Offset: 0x0001015D
		public IMouseInputSource mouseSource { get; set; }

		// (get) Token: 0x0600175C RID: 5980 RVA: 0x00011F66 File Offset: 0x00010166
		// (set) Token: 0x0600175D RID: 5981 RVA: 0x00011F6E File Offset: 0x0001016E
		public ITouchInputSource touchSource { get; set; }

		// (get) Token: 0x0600175E RID: 5982 RVA: 0x00011F77 File Offset: 0x00010177
		// (set) Token: 0x0600175F RID: 5983 RVA: 0x00011F7F File Offset: 0x0001017F
		public PointerEventType sourceType { get; set; }

		// (get) Token: 0x06001760 RID: 5984 RVA: 0x00011F88 File Offset: 0x00010188
		// (set) Token: 0x06001761 RID: 5985 RVA: 0x00011F90 File Offset: 0x00010190
		public int buttonIndex { get; set; }

		// Token: 0x06001762 RID: 5986 RVA: 0x00011F99 File Offset: 0x00010199
		public PlayerPointerEventData(EventSystem eventSystem)
			: base(eventSystem)
		{
			this.playerId = -1;
			this.inputSourceIndex = -1;
			this.buttonIndex = -1;
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x00064004 File Offset: 0x00062204
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
