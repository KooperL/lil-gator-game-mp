using System;

namespace Rewired
{
	public interface IFlightYokeTemplate : IControllerTemplate
	{
		// (get) Token: 0x060010D7 RID: 4311
		IControllerTemplateButton leftPaddle { get; }

		// (get) Token: 0x060010D8 RID: 4312
		IControllerTemplateButton rightPaddle { get; }

		// (get) Token: 0x060010D9 RID: 4313
		IControllerTemplateButton leftGripButton1 { get; }

		// (get) Token: 0x060010DA RID: 4314
		IControllerTemplateButton leftGripButton2 { get; }

		// (get) Token: 0x060010DB RID: 4315
		IControllerTemplateButton leftGripButton3 { get; }

		// (get) Token: 0x060010DC RID: 4316
		IControllerTemplateButton leftGripButton4 { get; }

		// (get) Token: 0x060010DD RID: 4317
		IControllerTemplateButton leftGripButton5 { get; }

		// (get) Token: 0x060010DE RID: 4318
		IControllerTemplateButton leftGripButton6 { get; }

		// (get) Token: 0x060010DF RID: 4319
		IControllerTemplateButton rightGripButton1 { get; }

		// (get) Token: 0x060010E0 RID: 4320
		IControllerTemplateButton rightGripButton2 { get; }

		// (get) Token: 0x060010E1 RID: 4321
		IControllerTemplateButton rightGripButton3 { get; }

		// (get) Token: 0x060010E2 RID: 4322
		IControllerTemplateButton rightGripButton4 { get; }

		// (get) Token: 0x060010E3 RID: 4323
		IControllerTemplateButton rightGripButton5 { get; }

		// (get) Token: 0x060010E4 RID: 4324
		IControllerTemplateButton rightGripButton6 { get; }

		// (get) Token: 0x060010E5 RID: 4325
		IControllerTemplateButton centerButton1 { get; }

		// (get) Token: 0x060010E6 RID: 4326
		IControllerTemplateButton centerButton2 { get; }

		// (get) Token: 0x060010E7 RID: 4327
		IControllerTemplateButton centerButton3 { get; }

		// (get) Token: 0x060010E8 RID: 4328
		IControllerTemplateButton centerButton4 { get; }

		// (get) Token: 0x060010E9 RID: 4329
		IControllerTemplateButton centerButton5 { get; }

		// (get) Token: 0x060010EA RID: 4330
		IControllerTemplateButton centerButton6 { get; }

		// (get) Token: 0x060010EB RID: 4331
		IControllerTemplateButton centerButton7 { get; }

		// (get) Token: 0x060010EC RID: 4332
		IControllerTemplateButton centerButton8 { get; }

		// (get) Token: 0x060010ED RID: 4333
		IControllerTemplateButton wheel1Up { get; }

		// (get) Token: 0x060010EE RID: 4334
		IControllerTemplateButton wheel1Down { get; }

		// (get) Token: 0x060010EF RID: 4335
		IControllerTemplateButton wheel1Press { get; }

		// (get) Token: 0x060010F0 RID: 4336
		IControllerTemplateButton wheel2Up { get; }

		// (get) Token: 0x060010F1 RID: 4337
		IControllerTemplateButton wheel2Down { get; }

		// (get) Token: 0x060010F2 RID: 4338
		IControllerTemplateButton wheel2Press { get; }

		// (get) Token: 0x060010F3 RID: 4339
		IControllerTemplateButton consoleButton1 { get; }

		// (get) Token: 0x060010F4 RID: 4340
		IControllerTemplateButton consoleButton2 { get; }

		// (get) Token: 0x060010F5 RID: 4341
		IControllerTemplateButton consoleButton3 { get; }

		// (get) Token: 0x060010F6 RID: 4342
		IControllerTemplateButton consoleButton4 { get; }

		// (get) Token: 0x060010F7 RID: 4343
		IControllerTemplateButton consoleButton5 { get; }

		// (get) Token: 0x060010F8 RID: 4344
		IControllerTemplateButton consoleButton6 { get; }

		// (get) Token: 0x060010F9 RID: 4345
		IControllerTemplateButton consoleButton7 { get; }

		// (get) Token: 0x060010FA RID: 4346
		IControllerTemplateButton consoleButton8 { get; }

		// (get) Token: 0x060010FB RID: 4347
		IControllerTemplateButton consoleButton9 { get; }

		// (get) Token: 0x060010FC RID: 4348
		IControllerTemplateButton consoleButton10 { get; }

		// (get) Token: 0x060010FD RID: 4349
		IControllerTemplateButton mode1 { get; }

		// (get) Token: 0x060010FE RID: 4350
		IControllerTemplateButton mode2 { get; }

		// (get) Token: 0x060010FF RID: 4351
		IControllerTemplateButton mode3 { get; }

		// (get) Token: 0x06001100 RID: 4352
		IControllerTemplateYoke yoke { get; }

		// (get) Token: 0x06001101 RID: 4353
		IControllerTemplateThrottle lever1 { get; }

		// (get) Token: 0x06001102 RID: 4354
		IControllerTemplateThrottle lever2 { get; }

		// (get) Token: 0x06001103 RID: 4355
		IControllerTemplateThrottle lever3 { get; }

		// (get) Token: 0x06001104 RID: 4356
		IControllerTemplateThrottle lever4 { get; }

		// (get) Token: 0x06001105 RID: 4357
		IControllerTemplateThrottle lever5 { get; }

		// (get) Token: 0x06001106 RID: 4358
		IControllerTemplateHat leftGripHat { get; }

		// (get) Token: 0x06001107 RID: 4359
		IControllerTemplateHat rightGripHat { get; }
	}
}
