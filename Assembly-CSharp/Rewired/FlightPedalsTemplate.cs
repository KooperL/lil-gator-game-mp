using System;

namespace Rewired
{
	// Token: 0x0200030C RID: 780
	public sealed class FlightPedalsTemplate : ControllerTemplate, IFlightPedalsTemplate, IControllerTemplate
	{
		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x0004E6C7 File Offset: 0x0004C8C7
		IControllerTemplateAxis IFlightPedalsTemplate.leftPedal
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(0);
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x0600120D RID: 4621 RVA: 0x0004E6D0 File Offset: 0x0004C8D0
		IControllerTemplateAxis IFlightPedalsTemplate.rightPedal
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(1);
			}
		}

		// Token: 0x170002B9 RID: 697
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

		// Token: 0x040016CB RID: 5835
		public static readonly Guid typeGuid = new Guid("f6fe76f8-be2a-4db2-b853-9e3652075913");

		// Token: 0x040016CC RID: 5836
		public const int elementId_leftPedal = 0;

		// Token: 0x040016CD RID: 5837
		public const int elementId_rightPedal = 1;

		// Token: 0x040016CE RID: 5838
		public const int elementId_slide = 2;
	}
}
