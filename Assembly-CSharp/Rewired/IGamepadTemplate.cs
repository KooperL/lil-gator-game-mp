using System;

namespace Rewired
{
	public interface IGamepadTemplate : IControllerTemplate
	{
		// (get) Token: 0x060013C8 RID: 5064
		IControllerTemplateButton actionBottomRow1 { get; }

		// (get) Token: 0x060013C9 RID: 5065
		IControllerTemplateButton a { get; }

		// (get) Token: 0x060013CA RID: 5066
		IControllerTemplateButton actionBottomRow2 { get; }

		// (get) Token: 0x060013CB RID: 5067
		IControllerTemplateButton b { get; }

		// (get) Token: 0x060013CC RID: 5068
		IControllerTemplateButton actionBottomRow3 { get; }

		// (get) Token: 0x060013CD RID: 5069
		IControllerTemplateButton c { get; }

		// (get) Token: 0x060013CE RID: 5070
		IControllerTemplateButton actionTopRow1 { get; }

		// (get) Token: 0x060013CF RID: 5071
		IControllerTemplateButton x { get; }

		// (get) Token: 0x060013D0 RID: 5072
		IControllerTemplateButton actionTopRow2 { get; }

		// (get) Token: 0x060013D1 RID: 5073
		IControllerTemplateButton y { get; }

		// (get) Token: 0x060013D2 RID: 5074
		IControllerTemplateButton actionTopRow3 { get; }

		// (get) Token: 0x060013D3 RID: 5075
		IControllerTemplateButton z { get; }

		// (get) Token: 0x060013D4 RID: 5076
		IControllerTemplateButton leftShoulder1 { get; }

		// (get) Token: 0x060013D5 RID: 5077
		IControllerTemplateButton leftBumper { get; }

		// (get) Token: 0x060013D6 RID: 5078
		IControllerTemplateAxis leftShoulder2 { get; }

		// (get) Token: 0x060013D7 RID: 5079
		IControllerTemplateAxis leftTrigger { get; }

		// (get) Token: 0x060013D8 RID: 5080
		IControllerTemplateButton rightShoulder1 { get; }

		// (get) Token: 0x060013D9 RID: 5081
		IControllerTemplateButton rightBumper { get; }

		// (get) Token: 0x060013DA RID: 5082
		IControllerTemplateAxis rightShoulder2 { get; }

		// (get) Token: 0x060013DB RID: 5083
		IControllerTemplateAxis rightTrigger { get; }

		// (get) Token: 0x060013DC RID: 5084
		IControllerTemplateButton center1 { get; }

		// (get) Token: 0x060013DD RID: 5085
		IControllerTemplateButton back { get; }

		// (get) Token: 0x060013DE RID: 5086
		IControllerTemplateButton center2 { get; }

		// (get) Token: 0x060013DF RID: 5087
		IControllerTemplateButton start { get; }

		// (get) Token: 0x060013E0 RID: 5088
		IControllerTemplateButton center3 { get; }

		// (get) Token: 0x060013E1 RID: 5089
		IControllerTemplateButton guide { get; }

		// (get) Token: 0x060013E2 RID: 5090
		IControllerTemplateThumbStick leftStick { get; }

		// (get) Token: 0x060013E3 RID: 5091
		IControllerTemplateThumbStick rightStick { get; }

		// (get) Token: 0x060013E4 RID: 5092
		IControllerTemplateDPad dPad { get; }
	}
}
