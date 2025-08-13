using System;

namespace Rewired
{
	// Token: 0x020003FF RID: 1023
	public interface IFlightPedalsTemplate : IControllerTemplate
	{
		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06001437 RID: 5175
		IControllerTemplateAxis leftPedal { get; }

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06001438 RID: 5176
		IControllerTemplateAxis rightPedal { get; }

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06001439 RID: 5177
		IControllerTemplateAxis slide { get; }
	}
}
