using System;

namespace Rewired
{
	public sealed class FlightPedalsTemplate : ControllerTemplate, IFlightPedalsTemplate, IControllerTemplate
	{
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x0004E6C7 File Offset: 0x0004C8C7
		IControllerTemplateAxis IFlightPedalsTemplate.leftPedal
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(0);
			}
		}

		// (get) Token: 0x0600120D RID: 4621 RVA: 0x0004E6D0 File Offset: 0x0004C8D0
		IControllerTemplateAxis IFlightPedalsTemplate.rightPedal
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(1);
			}
		}

		// (get) Token: 0x0600120E RID: 4622 RVA: 0x0004E6D9 File Offset: 0x0004C8D9
		IControllerTemplateAxis IFlightPedalsTemplate.slide
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(2);
			}
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x0004E6E2 File Offset: 0x0004C8E2
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
