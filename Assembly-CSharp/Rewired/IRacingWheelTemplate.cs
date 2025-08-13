using System;

namespace Rewired
{
	// Token: 0x020003FC RID: 1020
	public interface IRacingWheelTemplate : IControllerTemplate
	{
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06001384 RID: 4996
		IControllerTemplateAxis wheel { get; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06001385 RID: 4997
		IControllerTemplateAxis accelerator { get; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06001386 RID: 4998
		IControllerTemplateAxis brake { get; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06001387 RID: 4999
		IControllerTemplateAxis clutch { get; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06001388 RID: 5000
		IControllerTemplateButton shiftDown { get; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06001389 RID: 5001
		IControllerTemplateButton shiftUp { get; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600138A RID: 5002
		IControllerTemplateButton wheelButton1 { get; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600138B RID: 5003
		IControllerTemplateButton wheelButton2 { get; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600138C RID: 5004
		IControllerTemplateButton wheelButton3 { get; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600138D RID: 5005
		IControllerTemplateButton wheelButton4 { get; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x0600138E RID: 5006
		IControllerTemplateButton wheelButton5 { get; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x0600138F RID: 5007
		IControllerTemplateButton wheelButton6 { get; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06001390 RID: 5008
		IControllerTemplateButton wheelButton7 { get; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06001391 RID: 5009
		IControllerTemplateButton wheelButton8 { get; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06001392 RID: 5010
		IControllerTemplateButton wheelButton9 { get; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06001393 RID: 5011
		IControllerTemplateButton wheelButton10 { get; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06001394 RID: 5012
		IControllerTemplateButton consoleButton1 { get; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06001395 RID: 5013
		IControllerTemplateButton consoleButton2 { get; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06001396 RID: 5014
		IControllerTemplateButton consoleButton3 { get; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06001397 RID: 5015
		IControllerTemplateButton consoleButton4 { get; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06001398 RID: 5016
		IControllerTemplateButton consoleButton5 { get; }

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06001399 RID: 5017
		IControllerTemplateButton consoleButton6 { get; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x0600139A RID: 5018
		IControllerTemplateButton consoleButton7 { get; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x0600139B RID: 5019
		IControllerTemplateButton consoleButton8 { get; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x0600139C RID: 5020
		IControllerTemplateButton consoleButton9 { get; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x0600139D RID: 5021
		IControllerTemplateButton consoleButton10 { get; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x0600139E RID: 5022
		IControllerTemplateButton shifter1 { get; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x0600139F RID: 5023
		IControllerTemplateButton shifter2 { get; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060013A0 RID: 5024
		IControllerTemplateButton shifter3 { get; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060013A1 RID: 5025
		IControllerTemplateButton shifter4 { get; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060013A2 RID: 5026
		IControllerTemplateButton shifter5 { get; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060013A3 RID: 5027
		IControllerTemplateButton shifter6 { get; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060013A4 RID: 5028
		IControllerTemplateButton shifter7 { get; }

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060013A5 RID: 5029
		IControllerTemplateButton shifter8 { get; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060013A6 RID: 5030
		IControllerTemplateButton shifter9 { get; }

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060013A7 RID: 5031
		IControllerTemplateButton shifter10 { get; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060013A8 RID: 5032
		IControllerTemplateButton reverseGear { get; }

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060013A9 RID: 5033
		IControllerTemplateButton select { get; }

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060013AA RID: 5034
		IControllerTemplateButton start { get; }

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060013AB RID: 5035
		IControllerTemplateButton systemButton { get; }

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060013AC RID: 5036
		IControllerTemplateButton horn { get; }

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060013AD RID: 5037
		IControllerTemplateDPad dPad { get; }
	}
}
