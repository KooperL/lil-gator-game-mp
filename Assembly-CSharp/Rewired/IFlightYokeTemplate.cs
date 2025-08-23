using System;

namespace Rewired
{
	public interface IFlightYokeTemplate : IControllerTemplate
	{
		// (get) Token: 0x06001467 RID: 5223
		IControllerTemplateButton leftPaddle { get; }

		// (get) Token: 0x06001468 RID: 5224
		IControllerTemplateButton rightPaddle { get; }

		// (get) Token: 0x06001469 RID: 5225
		IControllerTemplateButton leftGripButton1 { get; }

		// (get) Token: 0x0600146A RID: 5226
		IControllerTemplateButton leftGripButton2 { get; }

		// (get) Token: 0x0600146B RID: 5227
		IControllerTemplateButton leftGripButton3 { get; }

		// (get) Token: 0x0600146C RID: 5228
		IControllerTemplateButton leftGripButton4 { get; }

		// (get) Token: 0x0600146D RID: 5229
		IControllerTemplateButton leftGripButton5 { get; }

		// (get) Token: 0x0600146E RID: 5230
		IControllerTemplateButton leftGripButton6 { get; }

		// (get) Token: 0x0600146F RID: 5231
		IControllerTemplateButton rightGripButton1 { get; }

		// (get) Token: 0x06001470 RID: 5232
		IControllerTemplateButton rightGripButton2 { get; }

		// (get) Token: 0x06001471 RID: 5233
		IControllerTemplateButton rightGripButton3 { get; }

		// (get) Token: 0x06001472 RID: 5234
		IControllerTemplateButton rightGripButton4 { get; }

		// (get) Token: 0x06001473 RID: 5235
		IControllerTemplateButton rightGripButton5 { get; }

		// (get) Token: 0x06001474 RID: 5236
		IControllerTemplateButton rightGripButton6 { get; }

		// (get) Token: 0x06001475 RID: 5237
		IControllerTemplateButton centerButton1 { get; }

		// (get) Token: 0x06001476 RID: 5238
		IControllerTemplateButton centerButton2 { get; }

		// (get) Token: 0x06001477 RID: 5239
		IControllerTemplateButton centerButton3 { get; }

		// (get) Token: 0x06001478 RID: 5240
		IControllerTemplateButton centerButton4 { get; }

		// (get) Token: 0x06001479 RID: 5241
		IControllerTemplateButton centerButton5 { get; }

		// (get) Token: 0x0600147A RID: 5242
		IControllerTemplateButton centerButton6 { get; }

		// (get) Token: 0x0600147B RID: 5243
		IControllerTemplateButton centerButton7 { get; }

		// (get) Token: 0x0600147C RID: 5244
		IControllerTemplateButton centerButton8 { get; }

		// (get) Token: 0x0600147D RID: 5245
		IControllerTemplateButton wheel1Up { get; }

		// (get) Token: 0x0600147E RID: 5246
		IControllerTemplateButton wheel1Down { get; }

		// (get) Token: 0x0600147F RID: 5247
		IControllerTemplateButton wheel1Press { get; }

		// (get) Token: 0x06001480 RID: 5248
		IControllerTemplateButton wheel2Up { get; }

		// (get) Token: 0x06001481 RID: 5249
		IControllerTemplateButton wheel2Down { get; }

		// (get) Token: 0x06001482 RID: 5250
		IControllerTemplateButton wheel2Press { get; }

		// (get) Token: 0x06001483 RID: 5251
		IControllerTemplateButton consoleButton1 { get; }

		// (get) Token: 0x06001484 RID: 5252
		IControllerTemplateButton consoleButton2 { get; }

		// (get) Token: 0x06001485 RID: 5253
		IControllerTemplateButton consoleButton3 { get; }

		// (get) Token: 0x06001486 RID: 5254
		IControllerTemplateButton consoleButton4 { get; }

		// (get) Token: 0x06001487 RID: 5255
		IControllerTemplateButton consoleButton5 { get; }

		// (get) Token: 0x06001488 RID: 5256
		IControllerTemplateButton consoleButton6 { get; }

		// (get) Token: 0x06001489 RID: 5257
		IControllerTemplateButton consoleButton7 { get; }

		// (get) Token: 0x0600148A RID: 5258
		IControllerTemplateButton consoleButton8 { get; }

		// (get) Token: 0x0600148B RID: 5259
		IControllerTemplateButton consoleButton9 { get; }

		// (get) Token: 0x0600148C RID: 5260
		IControllerTemplateButton consoleButton10 { get; }

		// (get) Token: 0x0600148D RID: 5261
		IControllerTemplateButton mode1 { get; }

		// (get) Token: 0x0600148E RID: 5262
		IControllerTemplateButton mode2 { get; }

		// (get) Token: 0x0600148F RID: 5263
		IControllerTemplateButton mode3 { get; }

		// (get) Token: 0x06001490 RID: 5264
		IControllerTemplateYoke yoke { get; }

		// (get) Token: 0x06001491 RID: 5265
		IControllerTemplateThrottle lever1 { get; }

		// (get) Token: 0x06001492 RID: 5266
		IControllerTemplateThrottle lever2 { get; }

		// (get) Token: 0x06001493 RID: 5267
		IControllerTemplateThrottle lever3 { get; }

		// (get) Token: 0x06001494 RID: 5268
		IControllerTemplateThrottle lever4 { get; }

		// (get) Token: 0x06001495 RID: 5269
		IControllerTemplateThrottle lever5 { get; }

		// (get) Token: 0x06001496 RID: 5270
		IControllerTemplateHat leftGripHat { get; }

		// (get) Token: 0x06001497 RID: 5271
		IControllerTemplateHat rightGripHat { get; }
	}
}
