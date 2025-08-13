using System;

namespace Rewired.Internal
{
	// Token: 0x0200041F RID: 1055
	public static class ControllerTemplateFactory
	{
		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060016F2 RID: 5874 RVA: 0x00011B28 File Offset: 0x0000FD28
		public static Type[] templateTypes
		{
			get
			{
				return ControllerTemplateFactory._defaultTemplateTypes;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060016F3 RID: 5875 RVA: 0x00011B2F File Offset: 0x0000FD2F
		public static Type[] templateInterfaceTypes
		{
			get
			{
				return ControllerTemplateFactory._defaultTemplateInterfaceTypes;
			}
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x00061E94 File Offset: 0x00060094
		public static IControllerTemplate Create(Guid typeGuid, object payload)
		{
			if (typeGuid == GamepadTemplate.typeGuid)
			{
				return new GamepadTemplate(payload);
			}
			if (typeGuid == RacingWheelTemplate.typeGuid)
			{
				return new RacingWheelTemplate(payload);
			}
			if (typeGuid == HOTASTemplate.typeGuid)
			{
				return new HOTASTemplate(payload);
			}
			if (typeGuid == FlightYokeTemplate.typeGuid)
			{
				return new FlightYokeTemplate(payload);
			}
			if (typeGuid == FlightPedalsTemplate.typeGuid)
			{
				return new FlightPedalsTemplate(payload);
			}
			if (typeGuid == SixDofControllerTemplate.typeGuid)
			{
				return new SixDofControllerTemplate(payload);
			}
			return null;
		}

		// Token: 0x04001B47 RID: 6983
		private static readonly Type[] _defaultTemplateTypes = new Type[]
		{
			typeof(GamepadTemplate),
			typeof(RacingWheelTemplate),
			typeof(HOTASTemplate),
			typeof(FlightYokeTemplate),
			typeof(FlightPedalsTemplate),
			typeof(SixDofControllerTemplate)
		};

		// Token: 0x04001B48 RID: 6984
		private static readonly Type[] _defaultTemplateInterfaceTypes = new Type[]
		{
			typeof(IGamepadTemplate),
			typeof(IRacingWheelTemplate),
			typeof(IHOTASTemplate),
			typeof(IFlightYokeTemplate),
			typeof(IFlightPedalsTemplate),
			typeof(ISixDofControllerTemplate)
		};
	}
}
