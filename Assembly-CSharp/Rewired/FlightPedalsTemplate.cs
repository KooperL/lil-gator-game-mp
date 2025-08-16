using System;

namespace Rewired
{
	public sealed class FlightPedalsTemplate : ControllerTemplate, IFlightPedalsTemplate, IControllerTemplate
	{
		// (get) Token: 0x0600159B RID: 5531 RVA: 0x00010C9D File Offset: 0x0000EE9D
		IControllerTemplateAxis IFlightPedalsTemplate.leftPedal
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(0);
			}
		}

		// (get) Token: 0x0600159C RID: 5532 RVA: 0x00010CA6 File Offset: 0x0000EEA6
		IControllerTemplateAxis IFlightPedalsTemplate.rightPedal
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(1);
			}
		}

		// (get) Token: 0x0600159D RID: 5533 RVA: 0x00010CAF File Offset: 0x0000EEAF
		IControllerTemplateAxis IFlightPedalsTemplate.slide
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(2);
			}
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x00010C83 File Offset: 0x0000EE83
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
