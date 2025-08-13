using System;

namespace Rewired
{
	// Token: 0x02000405 RID: 1029
	public sealed class FlightPedalsTemplate : ControllerTemplate, IFlightPedalsTemplate, IControllerTemplate
	{
		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x0600153B RID: 5435 RVA: 0x000108B5 File Offset: 0x0000EAB5
		IControllerTemplateAxis IFlightPedalsTemplate.leftPedal
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(0);
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x0600153C RID: 5436 RVA: 0x000108BE File Offset: 0x0000EABE
		IControllerTemplateAxis IFlightPedalsTemplate.rightPedal
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(1);
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x0600153D RID: 5437 RVA: 0x000108C7 File Offset: 0x0000EAC7
		IControllerTemplateAxis IFlightPedalsTemplate.slide
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(2);
			}
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x0001089B File Offset: 0x0000EA9B
		public FlightPedalsTemplate(object payload)
			: base(payload)
		{
		}

		// Token: 0x04001A98 RID: 6808
		public static readonly Guid typeGuid = new Guid("f6fe76f8-be2a-4db2-b853-9e3652075913");

		// Token: 0x04001A99 RID: 6809
		public const int elementId_leftPedal = 0;

		// Token: 0x04001A9A RID: 6810
		public const int elementId_rightPedal = 1;

		// Token: 0x04001A9B RID: 6811
		public const int elementId_slide = 2;
	}
}
