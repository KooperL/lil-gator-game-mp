using System;

namespace Rewired
{
	public interface IFlightPedalsTemplate : IControllerTemplate
	{
		// (get) Token: 0x06001108 RID: 4360
		IControllerTemplateAxis leftPedal { get; }

		// (get) Token: 0x06001109 RID: 4361
		IControllerTemplateAxis rightPedal { get; }

		// (get) Token: 0x0600110A RID: 4362
		IControllerTemplateAxis slide { get; }
	}
}
