using System;
using System.Text;
using Rewired.UI;
using UnityEngine.EventSystems;

namespace Rewired.Integration.UnityUI
{
	public class PlayerPointerEventData : PointerEventData
	{
		// (get) Token: 0x06001756 RID: 5974 RVA: 0x00011F3D File Offset: 0x0001013D
		// (set) Token: 0x06001757 RID: 5975 RVA: 0x00011F45 File Offset: 0x00010145
		public int playerId { get; set; }

		// (get) Token: 0x06001758 RID: 5976 RVA: 0x00011F4E File Offset: 0x0001014E
		// (set) Token: 0x06001759 RID: 5977 RVA: 0x00011F56 File Offset: 0x00010156
		public int inputSourceIndex { get; set; }

		// (get) Token: 0x0600175A RID: 5978 RVA: 0x00011F5F File Offset: 0x0001015F
		// (set) Token: 0x0600175B RID: 5979 RVA: 0x00011F67 File Offset: 0x00010167
		public IMouseInputSource mouseSource { get; set; }

		// (get) Token: 0x0600175C RID: 5980 RVA: 0x00011F70 File Offset: 0x00010170
		// (set) Token: 0x0600175D RID: 5981 RVA: 0x00011F78 File Offset: 0x00010178
		public ITouchInputSource touchSource { get; set; }

		// (get) Token: 0x0600175E RID: 5982 RVA: 0x00011F81 File Offset: 0x00010181
		// (set) Token: 0x0600175F RID: 5983 RVA: 0x00011F89 File Offset: 0x00010189
		public PointerEventType sourceType { get; set; }

		// (get) Token: 0x06001760 RID: 5984 RVA: 0x00011F92 File Offset: 0x00010192
		// (set) Token: 0x06001761 RID: 5985 RVA: 0x00011F9A File Offset: 0x0001019A
		public int buttonIndex { get; set; }

		// Token: 0x06001762 RID: 5986 RVA: 0x00011FA3 File Offset: 0x000101A3
		public PlayerPointerEventData(EventSystem eventSystem)
			: base(eventSystem)
		{
			this.playerId = -1;
			this.inputSourceIndex = -1;
			this.buttonIndex = -1;
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x00063FE0 File Offset: 0x000621E0
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
