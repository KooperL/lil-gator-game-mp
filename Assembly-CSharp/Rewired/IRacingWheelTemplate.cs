using System;

namespace Rewired
{
	public interface IRacingWheelTemplate : IControllerTemplate
	{
		// (get) Token: 0x06001055 RID: 4181
		IControllerTemplateAxis wheel { get; }

		// (get) Token: 0x06001056 RID: 4182
		IControllerTemplateAxis accelerator { get; }

		// (get) Token: 0x06001057 RID: 4183
		IControllerTemplateAxis brake { get; }

		// (get) Token: 0x06001058 RID: 4184
		IControllerTemplateAxis clutch { get; }

		// (get) Token: 0x06001059 RID: 4185
		IControllerTemplateButton shiftDown { get; }

		// (get) Token: 0x0600105A RID: 4186
		IControllerTemplateButton shiftUp { get; }

		// (get) Token: 0x0600105B RID: 4187
		IControllerTemplateButton wheelButton1 { get; }

		// (get) Token: 0x0600105C RID: 4188
		IControllerTemplateButton wheelButton2 { get; }

		// (get) Token: 0x0600105D RID: 4189
		IControllerTemplateButton wheelButton3 { get; }

		// (get) Token: 0x0600105E RID: 4190
		IControllerTemplateButton wheelButton4 { get; }

		// (get) Token: 0x0600105F RID: 4191
		IControllerTemplateButton wheelButton5 { get; }

		// (get) Token: 0x06001060 RID: 4192
		IControllerTemplateButton wheelButton6 { get; }

		// (get) Token: 0x06001061 RID: 4193
		IControllerTemplateButton wheelButton7 { get; }

		// (get) Token: 0x06001062 RID: 4194
		IControllerTemplateButton wheelButton8 { get; }

		// (get) Token: 0x06001063 RID: 4195
		IControllerTemplateButton wheelButton9 { get; }

		// (get) Token: 0x06001064 RID: 4196
		IControllerTemplateButton wheelButton10 { get; }

		// (get) Token: 0x06001065 RID: 4197
		IControllerTemplateButton consoleButton1 { get; }

		// (get) Token: 0x06001066 RID: 4198
		IControllerTemplateButton consoleButton2 { get; }

		// (get) Token: 0x06001067 RID: 4199
		IControllerTemplateButton consoleButton3 { get; }

		// (get) Token: 0x06001068 RID: 4200
		IControllerTemplateButton consoleButton4 { get; }

		// (get) Token: 0x06001069 RID: 4201
		IControllerTemplateButton consoleButton5 { get; }

		// (get) Token: 0x0600106A RID: 4202
		IControllerTemplateButton consoleButton6 { get; }

		// (get) Token: 0x0600106B RID: 4203
		IControllerTemplateButton consoleButton7 { get; }

		// (get) Token: 0x0600106C RID: 4204
		IControllerTemplateButton consoleButton8 { get; }

		// (get) Token: 0x0600106D RID: 4205
		IControllerTemplateButton consoleButton9 { get; }

		// (get) Token: 0x0600106E RID: 4206
		IControllerTemplateButton consoleButton10 { get; }

		// (get) Token: 0x0600106F RID: 4207
		IControllerTemplateButton shifter1 { get; }

		// (get) Token: 0x06001070 RID: 4208
		IControllerTemplateButton shifter2 { get; }

		// (get) Token: 0x06001071 RID: 4209
		IControllerTemplateButton shifter3 { get; }

		// (get) Token: 0x06001072 RID: 4210
		IControllerTemplateButton shifter4 { get; }

		// (get) Token: 0x06001073 RID: 4211
		IControllerTemplateButton shifter5 { get; }

		// (get) Token: 0x06001074 RID: 4212
		IControllerTemplateButton shifter6 { get; }

		// (get) Token: 0x06001075 RID: 4213
		IControllerTemplateButton shifter7 { get; }

		// (get) Token: 0x06001076 RID: 4214
		IControllerTemplateButton shifter8 { get; }

		// (get) Token: 0x06001077 RID: 4215
		IControllerTemplateButton shifter9 { get; }

		// (get) Token: 0x06001078 RID: 4216
		IControllerTemplateButton shifter10 { get; }

		// (get) Token: 0x06001079 RID: 4217
		IControllerTemplateButton reverseGear { get; }

		// (get) Token: 0x0600107A RID: 4218
		IControllerTemplateButton select { get; }

		// (get) Token: 0x0600107B RID: 4219
		IControllerTemplateButton start { get; }

		// (get) Token: 0x0600107C RID: 4220
		IControllerTemplateButton systemButton { get; }

		// (get) Token: 0x0600107D RID: 4221
		IControllerTemplateButton horn { get; }

		// (get) Token: 0x0600107E RID: 4222
		IControllerTemplateDPad dPad { get; }
	}
}
