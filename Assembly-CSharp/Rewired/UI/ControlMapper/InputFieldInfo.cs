using System;
using UnityEngine;

namespace Rewired.UI.ControlMapper
{
	[AddComponentMenu("")]
	public class InputFieldInfo : UIElementInfo
	{
		// (get) Token: 0x06001B37 RID: 6967 RVA: 0x00014F4A File Offset: 0x0001314A
		// (set) Token: 0x06001B38 RID: 6968 RVA: 0x00014F52 File Offset: 0x00013152
		public int actionId { get; set; }

		// (get) Token: 0x06001B39 RID: 6969 RVA: 0x00014F5B File Offset: 0x0001315B
		// (set) Token: 0x06001B3A RID: 6970 RVA: 0x00014F63 File Offset: 0x00013163
		public AxisRange axisRange { get; set; }

		// (get) Token: 0x06001B3B RID: 6971 RVA: 0x00014F6C File Offset: 0x0001316C
		// (set) Token: 0x06001B3C RID: 6972 RVA: 0x00014F74 File Offset: 0x00013174
		public int actionElementMapId { get; set; }

		// (get) Token: 0x06001B3D RID: 6973 RVA: 0x00014F7D File Offset: 0x0001317D
		// (set) Token: 0x06001B3E RID: 6974 RVA: 0x00014F85 File Offset: 0x00013185
		public ControllerType controllerType { get; set; }

		// (get) Token: 0x06001B3F RID: 6975 RVA: 0x00014F8E File Offset: 0x0001318E
		// (set) Token: 0x06001B40 RID: 6976 RVA: 0x00014F96 File Offset: 0x00013196
		public int controllerId { get; set; }
	}
}
