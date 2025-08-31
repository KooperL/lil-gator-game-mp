using System;
using UnityEngine;

namespace Rewired.UI.ControlMapper
{
	[AddComponentMenu("")]
	public class InputFieldInfo : UIElementInfo
	{
		// (get) Token: 0x060015AB RID: 5547 RVA: 0x0005D038 File Offset: 0x0005B238
		// (set) Token: 0x060015AC RID: 5548 RVA: 0x0005D040 File Offset: 0x0005B240
		public int actionId { get; set; }

		// (get) Token: 0x060015AD RID: 5549 RVA: 0x0005D049 File Offset: 0x0005B249
		// (set) Token: 0x060015AE RID: 5550 RVA: 0x0005D051 File Offset: 0x0005B251
		public AxisRange axisRange { get; set; }

		// (get) Token: 0x060015AF RID: 5551 RVA: 0x0005D05A File Offset: 0x0005B25A
		// (set) Token: 0x060015B0 RID: 5552 RVA: 0x0005D062 File Offset: 0x0005B262
		public int actionElementMapId { get; set; }

		// (get) Token: 0x060015B1 RID: 5553 RVA: 0x0005D06B File Offset: 0x0005B26B
		// (set) Token: 0x060015B2 RID: 5554 RVA: 0x0005D073 File Offset: 0x0005B273
		public ControllerType controllerType { get; set; }

		// (get) Token: 0x060015B3 RID: 5555 RVA: 0x0005D07C File Offset: 0x0005B27C
		// (set) Token: 0x060015B4 RID: 5556 RVA: 0x0005D084 File Offset: 0x0005B284
		public int controllerId { get; set; }
	}
}
