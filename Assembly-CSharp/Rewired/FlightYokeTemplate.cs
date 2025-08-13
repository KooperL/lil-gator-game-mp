using System;

namespace Rewired
{
	// Token: 0x0200030B RID: 779
	public sealed class FlightYokeTemplate : ControllerTemplate, IFlightYokeTemplate, IControllerTemplate
	{
		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060011D9 RID: 4569 RVA: 0x0004E4C5 File Offset: 0x0004C6C5
		IControllerTemplateButton IFlightYokeTemplate.leftPaddle
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(59);
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060011DA RID: 4570 RVA: 0x0004E4CF File Offset: 0x0004C6CF
		IControllerTemplateButton IFlightYokeTemplate.rightPaddle
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(60);
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060011DB RID: 4571 RVA: 0x0004E4D9 File Offset: 0x0004C6D9
		IControllerTemplateButton IFlightYokeTemplate.leftGripButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(7);
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060011DC RID: 4572 RVA: 0x0004E4E2 File Offset: 0x0004C6E2
		IControllerTemplateButton IFlightYokeTemplate.leftGripButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(8);
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060011DD RID: 4573 RVA: 0x0004E4EB File Offset: 0x0004C6EB
		IControllerTemplateButton IFlightYokeTemplate.leftGripButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(9);
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x060011DE RID: 4574 RVA: 0x0004E4F5 File Offset: 0x0004C6F5
		IControllerTemplateButton IFlightYokeTemplate.leftGripButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(10);
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x060011DF RID: 4575 RVA: 0x0004E4FF File Offset: 0x0004C6FF
		IControllerTemplateButton IFlightYokeTemplate.leftGripButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(11);
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x060011E0 RID: 4576 RVA: 0x0004E509 File Offset: 0x0004C709
		IControllerTemplateButton IFlightYokeTemplate.leftGripButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(12);
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x060011E1 RID: 4577 RVA: 0x0004E513 File Offset: 0x0004C713
		IControllerTemplateButton IFlightYokeTemplate.rightGripButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(13);
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x060011E2 RID: 4578 RVA: 0x0004E51D File Offset: 0x0004C71D
		IControllerTemplateButton IFlightYokeTemplate.rightGripButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(14);
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060011E3 RID: 4579 RVA: 0x0004E527 File Offset: 0x0004C727
		IControllerTemplateButton IFlightYokeTemplate.rightGripButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(15);
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060011E4 RID: 4580 RVA: 0x0004E531 File Offset: 0x0004C731
		IControllerTemplateButton IFlightYokeTemplate.rightGripButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(16);
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060011E5 RID: 4581 RVA: 0x0004E53B File Offset: 0x0004C73B
		IControllerTemplateButton IFlightYokeTemplate.rightGripButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(17);
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060011E6 RID: 4582 RVA: 0x0004E545 File Offset: 0x0004C745
		IControllerTemplateButton IFlightYokeTemplate.rightGripButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(18);
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x0004E54F File Offset: 0x0004C74F
		IControllerTemplateButton IFlightYokeTemplate.centerButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(19);
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060011E8 RID: 4584 RVA: 0x0004E559 File Offset: 0x0004C759
		IControllerTemplateButton IFlightYokeTemplate.centerButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(20);
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060011E9 RID: 4585 RVA: 0x0004E563 File Offset: 0x0004C763
		IControllerTemplateButton IFlightYokeTemplate.centerButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(21);
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x060011EA RID: 4586 RVA: 0x0004E56D File Offset: 0x0004C76D
		IControllerTemplateButton IFlightYokeTemplate.centerButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(22);
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x060011EB RID: 4587 RVA: 0x0004E577 File Offset: 0x0004C777
		IControllerTemplateButton IFlightYokeTemplate.centerButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(23);
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x060011EC RID: 4588 RVA: 0x0004E581 File Offset: 0x0004C781
		IControllerTemplateButton IFlightYokeTemplate.centerButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(24);
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x0004E58B File Offset: 0x0004C78B
		IControllerTemplateButton IFlightYokeTemplate.centerButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(25);
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x060011EE RID: 4590 RVA: 0x0004E595 File Offset: 0x0004C795
		IControllerTemplateButton IFlightYokeTemplate.centerButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(26);
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x0004E59F File Offset: 0x0004C79F
		IControllerTemplateButton IFlightYokeTemplate.wheel1Up
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(53);
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x0004E5A9 File Offset: 0x0004C7A9
		IControllerTemplateButton IFlightYokeTemplate.wheel1Down
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(54);
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x060011F1 RID: 4593 RVA: 0x0004E5B3 File Offset: 0x0004C7B3
		IControllerTemplateButton IFlightYokeTemplate.wheel1Press
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(55);
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x060011F2 RID: 4594 RVA: 0x0004E5BD File Offset: 0x0004C7BD
		IControllerTemplateButton IFlightYokeTemplate.wheel2Up
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(56);
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x060011F3 RID: 4595 RVA: 0x0004E5C7 File Offset: 0x0004C7C7
		IControllerTemplateButton IFlightYokeTemplate.wheel2Down
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(57);
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x0004E5D1 File Offset: 0x0004C7D1
		IControllerTemplateButton IFlightYokeTemplate.wheel2Press
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(58);
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060011F5 RID: 4597 RVA: 0x0004E5DB File Offset: 0x0004C7DB
		IControllerTemplateButton IFlightYokeTemplate.consoleButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(43);
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060011F6 RID: 4598 RVA: 0x0004E5E5 File Offset: 0x0004C7E5
		IControllerTemplateButton IFlightYokeTemplate.consoleButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(44);
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x0004E5EF File Offset: 0x0004C7EF
		IControllerTemplateButton IFlightYokeTemplate.consoleButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(45);
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060011F8 RID: 4600 RVA: 0x0004E5F9 File Offset: 0x0004C7F9
		IControllerTemplateButton IFlightYokeTemplate.consoleButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(46);
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060011F9 RID: 4601 RVA: 0x0004E603 File Offset: 0x0004C803
		IControllerTemplateButton IFlightYokeTemplate.consoleButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(47);
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060011FA RID: 4602 RVA: 0x0004E60D File Offset: 0x0004C80D
		IControllerTemplateButton IFlightYokeTemplate.consoleButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(48);
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x060011FB RID: 4603 RVA: 0x0004E617 File Offset: 0x0004C817
		IControllerTemplateButton IFlightYokeTemplate.consoleButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(49);
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060011FC RID: 4604 RVA: 0x0004E621 File Offset: 0x0004C821
		IControllerTemplateButton IFlightYokeTemplate.consoleButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(50);
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060011FD RID: 4605 RVA: 0x0004E62B File Offset: 0x0004C82B
		IControllerTemplateButton IFlightYokeTemplate.consoleButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(51);
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060011FE RID: 4606 RVA: 0x0004E635 File Offset: 0x0004C835
		IControllerTemplateButton IFlightYokeTemplate.consoleButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(52);
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060011FF RID: 4607 RVA: 0x0004E63F File Offset: 0x0004C83F
		IControllerTemplateButton IFlightYokeTemplate.mode1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(61);
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06001200 RID: 4608 RVA: 0x0004E649 File Offset: 0x0004C849
		IControllerTemplateButton IFlightYokeTemplate.mode2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(62);
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06001201 RID: 4609 RVA: 0x0004E653 File Offset: 0x0004C853
		IControllerTemplateButton IFlightYokeTemplate.mode3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(63);
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06001202 RID: 4610 RVA: 0x0004E65D File Offset: 0x0004C85D
		IControllerTemplateYoke IFlightYokeTemplate.yoke
		{
			get
			{
				return base.GetElement<IControllerTemplateYoke>(69);
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06001203 RID: 4611 RVA: 0x0004E667 File Offset: 0x0004C867
		IControllerTemplateThrottle IFlightYokeTemplate.lever1
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(70);
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06001204 RID: 4612 RVA: 0x0004E671 File Offset: 0x0004C871
		IControllerTemplateThrottle IFlightYokeTemplate.lever2
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(71);
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06001205 RID: 4613 RVA: 0x0004E67B File Offset: 0x0004C87B
		IControllerTemplateThrottle IFlightYokeTemplate.lever3
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(72);
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06001206 RID: 4614 RVA: 0x0004E685 File Offset: 0x0004C885
		IControllerTemplateThrottle IFlightYokeTemplate.lever4
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(73);
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06001207 RID: 4615 RVA: 0x0004E68F File Offset: 0x0004C88F
		IControllerTemplateThrottle IFlightYokeTemplate.lever5
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(74);
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06001208 RID: 4616 RVA: 0x0004E699 File Offset: 0x0004C899
		IControllerTemplateHat IFlightYokeTemplate.leftGripHat
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(75);
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06001209 RID: 4617 RVA: 0x0004E6A3 File Offset: 0x0004C8A3
		IControllerTemplateHat IFlightYokeTemplate.rightGripHat
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(76);
			}
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x0004E6AD File Offset: 0x0004C8AD
		public FlightYokeTemplate(object payload)
			: base(payload)
		{
		}

		// Token: 0x0400167D RID: 5757
		public static readonly Guid typeGuid = new Guid("f311fa16-0ccc-41c0-ac4b-50f7100bb8ff");

		// Token: 0x0400167E RID: 5758
		public const int elementId_rotateYoke = 0;

		// Token: 0x0400167F RID: 5759
		public const int elementId_yokeZ = 1;

		// Token: 0x04001680 RID: 5760
		public const int elementId_leftPaddle = 59;

		// Token: 0x04001681 RID: 5761
		public const int elementId_rightPaddle = 60;

		// Token: 0x04001682 RID: 5762
		public const int elementId_lever1Axis = 2;

		// Token: 0x04001683 RID: 5763
		public const int elementId_lever1MinDetent = 64;

		// Token: 0x04001684 RID: 5764
		public const int elementId_lever2Axis = 3;

		// Token: 0x04001685 RID: 5765
		public const int elementId_lever2MinDetent = 65;

		// Token: 0x04001686 RID: 5766
		public const int elementId_lever3Axis = 4;

		// Token: 0x04001687 RID: 5767
		public const int elementId_lever3MinDetent = 66;

		// Token: 0x04001688 RID: 5768
		public const int elementId_lever4Axis = 5;

		// Token: 0x04001689 RID: 5769
		public const int elementId_lever4MinDetent = 67;

		// Token: 0x0400168A RID: 5770
		public const int elementId_lever5Axis = 6;

		// Token: 0x0400168B RID: 5771
		public const int elementId_lever5MinDetent = 68;

		// Token: 0x0400168C RID: 5772
		public const int elementId_leftGripButton1 = 7;

		// Token: 0x0400168D RID: 5773
		public const int elementId_leftGripButton2 = 8;

		// Token: 0x0400168E RID: 5774
		public const int elementId_leftGripButton3 = 9;

		// Token: 0x0400168F RID: 5775
		public const int elementId_leftGripButton4 = 10;

		// Token: 0x04001690 RID: 5776
		public const int elementId_leftGripButton5 = 11;

		// Token: 0x04001691 RID: 5777
		public const int elementId_leftGripButton6 = 12;

		// Token: 0x04001692 RID: 5778
		public const int elementId_rightGripButton1 = 13;

		// Token: 0x04001693 RID: 5779
		public const int elementId_rightGripButton2 = 14;

		// Token: 0x04001694 RID: 5780
		public const int elementId_rightGripButton3 = 15;

		// Token: 0x04001695 RID: 5781
		public const int elementId_rightGripButton4 = 16;

		// Token: 0x04001696 RID: 5782
		public const int elementId_rightGripButton5 = 17;

		// Token: 0x04001697 RID: 5783
		public const int elementId_rightGripButton6 = 18;

		// Token: 0x04001698 RID: 5784
		public const int elementId_centerButton1 = 19;

		// Token: 0x04001699 RID: 5785
		public const int elementId_centerButton2 = 20;

		// Token: 0x0400169A RID: 5786
		public const int elementId_centerButton3 = 21;

		// Token: 0x0400169B RID: 5787
		public const int elementId_centerButton4 = 22;

		// Token: 0x0400169C RID: 5788
		public const int elementId_centerButton5 = 23;

		// Token: 0x0400169D RID: 5789
		public const int elementId_centerButton6 = 24;

		// Token: 0x0400169E RID: 5790
		public const int elementId_centerButton7 = 25;

		// Token: 0x0400169F RID: 5791
		public const int elementId_centerButton8 = 26;

		// Token: 0x040016A0 RID: 5792
		public const int elementId_wheel1Up = 53;

		// Token: 0x040016A1 RID: 5793
		public const int elementId_wheel1Down = 54;

		// Token: 0x040016A2 RID: 5794
		public const int elementId_wheel1Press = 55;

		// Token: 0x040016A3 RID: 5795
		public const int elementId_wheel2Up = 56;

		// Token: 0x040016A4 RID: 5796
		public const int elementId_wheel2Down = 57;

		// Token: 0x040016A5 RID: 5797
		public const int elementId_wheel2Press = 58;

		// Token: 0x040016A6 RID: 5798
		public const int elementId_leftGripHatUp = 27;

		// Token: 0x040016A7 RID: 5799
		public const int elementId_leftGripHatUpRight = 28;

		// Token: 0x040016A8 RID: 5800
		public const int elementId_leftGripHatRight = 29;

		// Token: 0x040016A9 RID: 5801
		public const int elementId_leftGripHatDownRight = 30;

		// Token: 0x040016AA RID: 5802
		public const int elementId_leftGripHatDown = 31;

		// Token: 0x040016AB RID: 5803
		public const int elementId_leftGripHatDownLeft = 32;

		// Token: 0x040016AC RID: 5804
		public const int elementId_leftGripHatLeft = 33;

		// Token: 0x040016AD RID: 5805
		public const int elementId_leftGripHatUpLeft = 34;

		// Token: 0x040016AE RID: 5806
		public const int elementId_rightGripHatUp = 35;

		// Token: 0x040016AF RID: 5807
		public const int elementId_rightGripHatUpRight = 36;

		// Token: 0x040016B0 RID: 5808
		public const int elementId_rightGripHatRight = 37;

		// Token: 0x040016B1 RID: 5809
		public const int elementId_rightGripHatDownRight = 38;

		// Token: 0x040016B2 RID: 5810
		public const int elementId_rightGripHatDown = 39;

		// Token: 0x040016B3 RID: 5811
		public const int elementId_rightGripHatDownLeft = 40;

		// Token: 0x040016B4 RID: 5812
		public const int elementId_rightGripHatLeft = 41;

		// Token: 0x040016B5 RID: 5813
		public const int elementId_rightGripHatUpLeft = 42;

		// Token: 0x040016B6 RID: 5814
		public const int elementId_consoleButton1 = 43;

		// Token: 0x040016B7 RID: 5815
		public const int elementId_consoleButton2 = 44;

		// Token: 0x040016B8 RID: 5816
		public const int elementId_consoleButton3 = 45;

		// Token: 0x040016B9 RID: 5817
		public const int elementId_consoleButton4 = 46;

		// Token: 0x040016BA RID: 5818
		public const int elementId_consoleButton5 = 47;

		// Token: 0x040016BB RID: 5819
		public const int elementId_consoleButton6 = 48;

		// Token: 0x040016BC RID: 5820
		public const int elementId_consoleButton7 = 49;

		// Token: 0x040016BD RID: 5821
		public const int elementId_consoleButton8 = 50;

		// Token: 0x040016BE RID: 5822
		public const int elementId_consoleButton9 = 51;

		// Token: 0x040016BF RID: 5823
		public const int elementId_consoleButton10 = 52;

		// Token: 0x040016C0 RID: 5824
		public const int elementId_mode1 = 61;

		// Token: 0x040016C1 RID: 5825
		public const int elementId_mode2 = 62;

		// Token: 0x040016C2 RID: 5826
		public const int elementId_mode3 = 63;

		// Token: 0x040016C3 RID: 5827
		public const int elementId_yoke = 69;

		// Token: 0x040016C4 RID: 5828
		public const int elementId_lever1 = 70;

		// Token: 0x040016C5 RID: 5829
		public const int elementId_lever2 = 71;

		// Token: 0x040016C6 RID: 5830
		public const int elementId_lever3 = 72;

		// Token: 0x040016C7 RID: 5831
		public const int elementId_lever4 = 73;

		// Token: 0x040016C8 RID: 5832
		public const int elementId_lever5 = 74;

		// Token: 0x040016C9 RID: 5833
		public const int elementId_leftGripHat = 75;

		// Token: 0x040016CA RID: 5834
		public const int elementId_rightGripHat = 76;
	}
}
