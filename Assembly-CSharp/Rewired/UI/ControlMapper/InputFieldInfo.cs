using System;
using UnityEngine;

namespace Rewired.UI.ControlMapper
{
	[AddComponentMenu("")]
	public class InputFieldInfo : UIElementInfo
	{
		// (get) Token: 0x06001B37 RID: 6967 RVA: 0x00014F5F File Offset: 0x0001315F
		// (set) Token: 0x06001B38 RID: 6968 RVA: 0x00014F67 File Offset: 0x00013167
		public int actionId { get; set; }

		// (get) Token: 0x06001B39 RID: 6969 RVA: 0x00014F70 File Offset: 0x00013170
		// (set) Token: 0x06001B3A RID: 6970 RVA: 0x00014F78 File Offset: 0x00013178
		public AxisRange axisRange { get; set; }

		// (get) Token: 0x06001B3B RID: 6971 RVA: 0x00014F81 File Offset: 0x00013181
		// (set) Token: 0x06001B3C RID: 6972 RVA: 0x00014F89 File Offset: 0x00013189
		public int actionElementMapId { get; set; }

		// (get) Token: 0x06001B3D RID: 6973 RVA: 0x00014F92 File Offset: 0x00013192
		// (set) Token: 0x06001B3E RID: 6974 RVA: 0x00014F9A File Offset: 0x0001319A
		public ControllerType controllerType { get; set; }

		// (get) Token: 0x06001B3F RID: 6975 RVA: 0x00014FA3 File Offset: 0x000131A3
		// (set) Token: 0x06001B40 RID: 6976 RVA: 0x00014FAB File Offset: 0x000131AB
		public int controllerId { get; set; }
	}
}
