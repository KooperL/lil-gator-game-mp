using System;

namespace Rewired
{
	// Token: 0x02000306 RID: 774
	public interface IFlightPedalsTemplate : IControllerTemplate
	{
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06001108 RID: 4360
		IControllerTemplateAxis leftPedal { get; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06001109 RID: 4361
		IControllerTemplateAxis rightPedal { get; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600110A RID: 4362
		IControllerTemplateAxis slide { get; }
	}
}
