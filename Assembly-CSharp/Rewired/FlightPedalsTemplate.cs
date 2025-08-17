using System;

namespace Rewired
{
	public sealed class FlightPedalsTemplate : ControllerTemplate, IFlightPedalsTemplate, IControllerTemplate
	{
		// (get) Token: 0x0600159B RID: 5531 RVA: 0x00010CB2 File Offset: 0x0000EEB2
		IControllerTemplateAxis IFlightPedalsTemplate.leftPedal
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(0);
			}
		}

		// (get) Token: 0x0600159C RID: 5532 RVA: 0x00010CBB File Offset: 0x0000EEBB
		IControllerTemplateAxis IFlightPedalsTemplate.rightPedal
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(1);
			}
		}

		// (get) Token: 0x0600159D RID: 5533 RVA: 0x00010CC4 File Offset: 0x0000EEC4
		IControllerTemplateAxis IFlightPedalsTemplate.slide
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(2);
			}
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x00010C98 File Offset: 0x0000EE98
		public FlightPedalsTemplate(object payload)
			: base(payload)
		{
		}

		public static readonly Guid typeGuid = new Guid("f6fe76f8-be2a-4db2-b853-9e3652075913");

		public const int elementId_leftPedal = 0;

		public const int elementId_rightPedal = 1;

		public const int elementId_slide = 2;
	}
}
