using System;

namespace Rewired
{
	// Token: 0x020003FE RID: 1022
	public interface IFlightYokeTemplate : IControllerTemplate
	{
		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06001406 RID: 5126
		IControllerTemplateButton leftPaddle { get; }

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06001407 RID: 5127
		IControllerTemplateButton rightPaddle { get; }

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06001408 RID: 5128
		IControllerTemplateButton leftGripButton1 { get; }

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06001409 RID: 5129
		IControllerTemplateButton leftGripButton2 { get; }

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x0600140A RID: 5130
		IControllerTemplateButton leftGripButton3 { get; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x0600140B RID: 5131
		IControllerTemplateButton leftGripButton4 { get; }

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x0600140C RID: 5132
		IControllerTemplateButton leftGripButton5 { get; }

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x0600140D RID: 5133
		IControllerTemplateButton leftGripButton6 { get; }

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x0600140E RID: 5134
		IControllerTemplateButton rightGripButton1 { get; }

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x0600140F RID: 5135
		IControllerTemplateButton rightGripButton2 { get; }

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06001410 RID: 5136
		IControllerTemplateButton rightGripButton3 { get; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06001411 RID: 5137
		IControllerTemplateButton rightGripButton4 { get; }

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06001412 RID: 5138
		IControllerTemplateButton rightGripButton5 { get; }

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06001413 RID: 5139
		IControllerTemplateButton rightGripButton6 { get; }

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06001414 RID: 5140
		IControllerTemplateButton centerButton1 { get; }

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06001415 RID: 5141
		IControllerTemplateButton centerButton2 { get; }

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06001416 RID: 5142
		IControllerTemplateButton centerButton3 { get; }

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06001417 RID: 5143
		IControllerTemplateButton centerButton4 { get; }

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06001418 RID: 5144
		IControllerTemplateButton centerButton5 { get; }

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06001419 RID: 5145
		IControllerTemplateButton centerButton6 { get; }

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x0600141A RID: 5146
		IControllerTemplateButton centerButton7 { get; }

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x0600141B RID: 5147
		IControllerTemplateButton centerButton8 { get; }

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x0600141C RID: 5148
		IControllerTemplateButton wheel1Up { get; }

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x0600141D RID: 5149
		IControllerTemplateButton wheel1Down { get; }

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x0600141E RID: 5150
		IControllerTemplateButton wheel1Press { get; }

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x0600141F RID: 5151
		IControllerTemplateButton wheel2Up { get; }

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06001420 RID: 5152
		IControllerTemplateButton wheel2Down { get; }

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06001421 RID: 5153
		IControllerTemplateButton wheel2Press { get; }

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06001422 RID: 5154
		IControllerTemplateButton consoleButton1 { get; }

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06001423 RID: 5155
		IControllerTemplateButton consoleButton2 { get; }

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06001424 RID: 5156
		IControllerTemplateButton consoleButton3 { get; }

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06001425 RID: 5157
		IControllerTemplateButton consoleButton4 { get; }

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06001426 RID: 5158
		IControllerTemplateButton consoleButton5 { get; }

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06001427 RID: 5159
		IControllerTemplateButton consoleButton6 { get; }

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06001428 RID: 5160
		IControllerTemplateButton consoleButton7 { get; }

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06001429 RID: 5161
		IControllerTemplateButton consoleButton8 { get; }

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x0600142A RID: 5162
		IControllerTemplateButton consoleButton9 { get; }

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x0600142B RID: 5163
		IControllerTemplateButton consoleButton10 { get; }

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x0600142C RID: 5164
		IControllerTemplateButton mode1 { get; }

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x0600142D RID: 5165
		IControllerTemplateButton mode2 { get; }

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x0600142E RID: 5166
		IControllerTemplateButton mode3 { get; }

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x0600142F RID: 5167
		IControllerTemplateYoke yoke { get; }

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06001430 RID: 5168
		IControllerTemplateThrottle lever1 { get; }

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06001431 RID: 5169
		IControllerTemplateThrottle lever2 { get; }

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06001432 RID: 5170
		IControllerTemplateThrottle lever3 { get; }

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06001433 RID: 5171
		IControllerTemplateThrottle lever4 { get; }

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06001434 RID: 5172
		IControllerTemplateThrottle lever5 { get; }

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06001435 RID: 5173
		IControllerTemplateHat leftGripHat { get; }

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06001436 RID: 5174
		IControllerTemplateHat rightGripHat { get; }
	}
}
