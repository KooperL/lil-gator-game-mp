using System;

namespace Rewired
{
	public interface IFlightPedalsTemplate : IControllerTemplate
	{
		// (get) Token: 0x06001497 RID: 5271
		IControllerTemplateAxis leftPedal { get; }

		// (get) Token: 0x06001498 RID: 5272
		IControllerTemplateAxis rightPedal { get; }

		// (get) Token: 0x06001499 RID: 5273
		IControllerTemplateAxis slide { get; }
	}
}
