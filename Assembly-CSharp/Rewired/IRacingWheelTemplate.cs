using System;

namespace Rewired
{
	public interface IRacingWheelTemplate : IControllerTemplate
	{
		// (get) Token: 0x060013E4 RID: 5092
		IControllerTemplateAxis wheel { get; }

		// (get) Token: 0x060013E5 RID: 5093
		IControllerTemplateAxis accelerator { get; }

		// (get) Token: 0x060013E6 RID: 5094
		IControllerTemplateAxis brake { get; }

		// (get) Token: 0x060013E7 RID: 5095
		IControllerTemplateAxis clutch { get; }

		// (get) Token: 0x060013E8 RID: 5096
		IControllerTemplateButton shiftDown { get; }

		// (get) Token: 0x060013E9 RID: 5097
		IControllerTemplateButton shiftUp { get; }

		// (get) Token: 0x060013EA RID: 5098
		IControllerTemplateButton wheelButton1 { get; }

		// (get) Token: 0x060013EB RID: 5099
		IControllerTemplateButton wheelButton2 { get; }

		// (get) Token: 0x060013EC RID: 5100
		IControllerTemplateButton wheelButton3 { get; }

		// (get) Token: 0x060013ED RID: 5101
		IControllerTemplateButton wheelButton4 { get; }

		// (get) Token: 0x060013EE RID: 5102
		IControllerTemplateButton wheelButton5 { get; }

		// (get) Token: 0x060013EF RID: 5103
		IControllerTemplateButton wheelButton6 { get; }

		// (get) Token: 0x060013F0 RID: 5104
		IControllerTemplateButton wheelButton7 { get; }

		// (get) Token: 0x060013F1 RID: 5105
		IControllerTemplateButton wheelButton8 { get; }

		// (get) Token: 0x060013F2 RID: 5106
		IControllerTemplateButton wheelButton9 { get; }

		// (get) Token: 0x060013F3 RID: 5107
		IControllerTemplateButton wheelButton10 { get; }

		// (get) Token: 0x060013F4 RID: 5108
		IControllerTemplateButton consoleButton1 { get; }

		// (get) Token: 0x060013F5 RID: 5109
		IControllerTemplateButton consoleButton2 { get; }

		// (get) Token: 0x060013F6 RID: 5110
		IControllerTemplateButton consoleButton3 { get; }

		// (get) Token: 0x060013F7 RID: 5111
		IControllerTemplateButton consoleButton4 { get; }

		// (get) Token: 0x060013F8 RID: 5112
		IControllerTemplateButton consoleButton5 { get; }

		// (get) Token: 0x060013F9 RID: 5113
		IControllerTemplateButton consoleButton6 { get; }

		// (get) Token: 0x060013FA RID: 5114
		IControllerTemplateButton consoleButton7 { get; }

		// (get) Token: 0x060013FB RID: 5115
		IControllerTemplateButton consoleButton8 { get; }

		// (get) Token: 0x060013FC RID: 5116
		IControllerTemplateButton consoleButton9 { get; }

		// (get) Token: 0x060013FD RID: 5117
		IControllerTemplateButton consoleButton10 { get; }

		// (get) Token: 0x060013FE RID: 5118
		IControllerTemplateButton shifter1 { get; }

		// (get) Token: 0x060013FF RID: 5119
		IControllerTemplateButton shifter2 { get; }

		// (get) Token: 0x06001400 RID: 5120
		IControllerTemplateButton shifter3 { get; }

		// (get) Token: 0x06001401 RID: 5121
		IControllerTemplateButton shifter4 { get; }

		// (get) Token: 0x06001402 RID: 5122
		IControllerTemplateButton shifter5 { get; }

		// (get) Token: 0x06001403 RID: 5123
		IControllerTemplateButton shifter6 { get; }

		// (get) Token: 0x06001404 RID: 5124
		IControllerTemplateButton shifter7 { get; }

		// (get) Token: 0x06001405 RID: 5125
		IControllerTemplateButton shifter8 { get; }

		// (get) Token: 0x06001406 RID: 5126
		IControllerTemplateButton shifter9 { get; }

		// (get) Token: 0x06001407 RID: 5127
		IControllerTemplateButton shifter10 { get; }

		// (get) Token: 0x06001408 RID: 5128
		IControllerTemplateButton reverseGear { get; }

		// (get) Token: 0x06001409 RID: 5129
		IControllerTemplateButton select { get; }

		// (get) Token: 0x0600140A RID: 5130
		IControllerTemplateButton start { get; }

		// (get) Token: 0x0600140B RID: 5131
		IControllerTemplateButton systemButton { get; }

		// (get) Token: 0x0600140C RID: 5132
		IControllerTemplateButton horn { get; }

		// (get) Token: 0x0600140D RID: 5133
		IControllerTemplateDPad dPad { get; }
	}
}
