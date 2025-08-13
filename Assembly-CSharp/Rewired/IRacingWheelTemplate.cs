using System;

namespace Rewired
{
	// Token: 0x02000303 RID: 771
	public interface IRacingWheelTemplate : IControllerTemplate
	{
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06001055 RID: 4181
		IControllerTemplateAxis wheel { get; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06001056 RID: 4182
		IControllerTemplateAxis accelerator { get; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06001057 RID: 4183
		IControllerTemplateAxis brake { get; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06001058 RID: 4184
		IControllerTemplateAxis clutch { get; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06001059 RID: 4185
		IControllerTemplateButton shiftDown { get; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600105A RID: 4186
		IControllerTemplateButton shiftUp { get; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600105B RID: 4187
		IControllerTemplateButton wheelButton1 { get; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600105C RID: 4188
		IControllerTemplateButton wheelButton2 { get; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600105D RID: 4189
		IControllerTemplateButton wheelButton3 { get; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600105E RID: 4190
		IControllerTemplateButton wheelButton4 { get; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600105F RID: 4191
		IControllerTemplateButton wheelButton5 { get; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06001060 RID: 4192
		IControllerTemplateButton wheelButton6 { get; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06001061 RID: 4193
		IControllerTemplateButton wheelButton7 { get; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06001062 RID: 4194
		IControllerTemplateButton wheelButton8 { get; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06001063 RID: 4195
		IControllerTemplateButton wheelButton9 { get; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06001064 RID: 4196
		IControllerTemplateButton wheelButton10 { get; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06001065 RID: 4197
		IControllerTemplateButton consoleButton1 { get; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06001066 RID: 4198
		IControllerTemplateButton consoleButton2 { get; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06001067 RID: 4199
		IControllerTemplateButton consoleButton3 { get; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06001068 RID: 4200
		IControllerTemplateButton consoleButton4 { get; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06001069 RID: 4201
		IControllerTemplateButton consoleButton5 { get; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600106A RID: 4202
		IControllerTemplateButton consoleButton6 { get; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600106B RID: 4203
		IControllerTemplateButton consoleButton7 { get; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600106C RID: 4204
		IControllerTemplateButton consoleButton8 { get; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600106D RID: 4205
		IControllerTemplateButton consoleButton9 { get; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600106E RID: 4206
		IControllerTemplateButton consoleButton10 { get; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600106F RID: 4207
		IControllerTemplateButton shifter1 { get; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06001070 RID: 4208
		IControllerTemplateButton shifter2 { get; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06001071 RID: 4209
		IControllerTemplateButton shifter3 { get; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06001072 RID: 4210
		IControllerTemplateButton shifter4 { get; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06001073 RID: 4211
		IControllerTemplateButton shifter5 { get; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06001074 RID: 4212
		IControllerTemplateButton shifter6 { get; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06001075 RID: 4213
		IControllerTemplateButton shifter7 { get; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06001076 RID: 4214
		IControllerTemplateButton shifter8 { get; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06001077 RID: 4215
		IControllerTemplateButton shifter9 { get; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06001078 RID: 4216
		IControllerTemplateButton shifter10 { get; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06001079 RID: 4217
		IControllerTemplateButton reverseGear { get; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600107A RID: 4218
		IControllerTemplateButton select { get; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600107B RID: 4219
		IControllerTemplateButton start { get; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600107C RID: 4220
		IControllerTemplateButton systemButton { get; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600107D RID: 4221
		IControllerTemplateButton horn { get; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600107E RID: 4222
		IControllerTemplateDPad dPad { get; }
	}
}
