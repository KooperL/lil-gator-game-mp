using System;
using UnityEngine;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x0200045F RID: 1119
	[AddComponentMenu("")]
	public class InputFieldInfo : UIElementInfo
	{
		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001AD7 RID: 6871 RVA: 0x00014B62 File Offset: 0x00012D62
		// (set) Token: 0x06001AD8 RID: 6872 RVA: 0x00014B6A File Offset: 0x00012D6A
		public int actionId { get; set; }

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001AD9 RID: 6873 RVA: 0x00014B73 File Offset: 0x00012D73
		// (set) Token: 0x06001ADA RID: 6874 RVA: 0x00014B7B File Offset: 0x00012D7B
		public AxisRange axisRange { get; set; }

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06001ADB RID: 6875 RVA: 0x00014B84 File Offset: 0x00012D84
		// (set) Token: 0x06001ADC RID: 6876 RVA: 0x00014B8C File Offset: 0x00012D8C
		public int actionElementMapId { get; set; }

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06001ADD RID: 6877 RVA: 0x00014B95 File Offset: 0x00012D95
		// (set) Token: 0x06001ADE RID: 6878 RVA: 0x00014B9D File Offset: 0x00012D9D
		public ControllerType controllerType { get; set; }

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06001ADF RID: 6879 RVA: 0x00014BA6 File Offset: 0x00012DA6
		// (set) Token: 0x06001AE0 RID: 6880 RVA: 0x00014BAE File Offset: 0x00012DAE
		public int controllerId { get; set; }
	}
}
