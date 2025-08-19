using System;
using UnityEngine;

namespace Rewired.UI.ControlMapper
{
	[AddComponentMenu("")]
	public class InputFieldInfo : UIElementInfo
	{
		// (get) Token: 0x06001B37 RID: 6967 RVA: 0x00014F69 File Offset: 0x00013169
		// (set) Token: 0x06001B38 RID: 6968 RVA: 0x00014F71 File Offset: 0x00013171
		public int actionId { get; set; }

		// (get) Token: 0x06001B39 RID: 6969 RVA: 0x00014F7A File Offset: 0x0001317A
		// (set) Token: 0x06001B3A RID: 6970 RVA: 0x00014F82 File Offset: 0x00013182
		public AxisRange axisRange { get; set; }

		// (get) Token: 0x06001B3B RID: 6971 RVA: 0x00014F8B File Offset: 0x0001318B
		// (set) Token: 0x06001B3C RID: 6972 RVA: 0x00014F93 File Offset: 0x00013193
		public int actionElementMapId { get; set; }

		// (get) Token: 0x06001B3D RID: 6973 RVA: 0x00014F9C File Offset: 0x0001319C
		// (set) Token: 0x06001B3E RID: 6974 RVA: 0x00014FA4 File Offset: 0x000131A4
		public ControllerType controllerType { get; set; }

		// (get) Token: 0x06001B3F RID: 6975 RVA: 0x00014FAD File Offset: 0x000131AD
		// (set) Token: 0x06001B40 RID: 6976 RVA: 0x00014FB5 File Offset: 0x000131B5
		public int controllerId { get; set; }
	}
}
