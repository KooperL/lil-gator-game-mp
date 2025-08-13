using System;

namespace Rewired
{
	// Token: 0x0200030D RID: 781
	public sealed class SixDofControllerTemplate : ControllerTemplate, ISixDofControllerTemplate, IControllerTemplate
	{
		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x0004E6FC File Offset: 0x0004C8FC
		IControllerTemplateAxis ISixDofControllerTemplate.extraAxis1
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(8);
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06001212 RID: 4626 RVA: 0x0004E705 File Offset: 0x0004C905
		IControllerTemplateAxis ISixDofControllerTemplate.extraAxis2
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(9);
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x0004E70F File Offset: 0x0004C90F
		IControllerTemplateAxis ISixDofControllerTemplate.extraAxis3
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(10);
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06001214 RID: 4628 RVA: 0x0004E719 File Offset: 0x0004C919
		IControllerTemplateAxis ISixDofControllerTemplate.extraAxis4
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(11);
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06001215 RID: 4629 RVA: 0x0004E723 File Offset: 0x0004C923
		IControllerTemplateButton ISixDofControllerTemplate.button1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(12);
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06001216 RID: 4630 RVA: 0x0004E72D File Offset: 0x0004C92D
		IControllerTemplateButton ISixDofControllerTemplate.button2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(13);
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06001217 RID: 4631 RVA: 0x0004E737 File Offset: 0x0004C937
		IControllerTemplateButton ISixDofControllerTemplate.button3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(14);
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06001218 RID: 4632 RVA: 0x0004E741 File Offset: 0x0004C941
		IControllerTemplateButton ISixDofControllerTemplate.button4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(15);
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06001219 RID: 4633 RVA: 0x0004E74B File Offset: 0x0004C94B
		IControllerTemplateButton ISixDofControllerTemplate.button5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(16);
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x0600121A RID: 4634 RVA: 0x0004E755 File Offset: 0x0004C955
		IControllerTemplateButton ISixDofControllerTemplate.button6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(17);
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x0600121B RID: 4635 RVA: 0x0004E75F File Offset: 0x0004C95F
		IControllerTemplateButton ISixDofControllerTemplate.button7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(18);
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x0600121C RID: 4636 RVA: 0x0004E769 File Offset: 0x0004C969
		IControllerTemplateButton ISixDofControllerTemplate.button8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(19);
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x0004E773 File Offset: 0x0004C973
		IControllerTemplateButton ISixDofControllerTemplate.button9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(20);
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x0600121E RID: 4638 RVA: 0x0004E77D File Offset: 0x0004C97D
		IControllerTemplateButton ISixDofControllerTemplate.button10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(21);
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x0600121F RID: 4639 RVA: 0x0004E787 File Offset: 0x0004C987
		IControllerTemplateButton ISixDofControllerTemplate.button11
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(22);
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06001220 RID: 4640 RVA: 0x0004E791 File Offset: 0x0004C991
		IControllerTemplateButton ISixDofControllerTemplate.button12
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(23);
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06001221 RID: 4641 RVA: 0x0004E79B File Offset: 0x0004C99B
		IControllerTemplateButton ISixDofControllerTemplate.button13
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(24);
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06001222 RID: 4642 RVA: 0x0004E7A5 File Offset: 0x0004C9A5
		IControllerTemplateButton ISixDofControllerTemplate.button14
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(25);
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06001223 RID: 4643 RVA: 0x0004E7AF File Offset: 0x0004C9AF
		IControllerTemplateButton ISixDofControllerTemplate.button15
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(26);
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06001224 RID: 4644 RVA: 0x0004E7B9 File Offset: 0x0004C9B9
		IControllerTemplateButton ISixDofControllerTemplate.button16
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(27);
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06001225 RID: 4645 RVA: 0x0004E7C3 File Offset: 0x0004C9C3
		IControllerTemplateButton ISixDofControllerTemplate.button17
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(28);
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06001226 RID: 4646 RVA: 0x0004E7CD File Offset: 0x0004C9CD
		IControllerTemplateButton ISixDofControllerTemplate.button18
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(29);
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06001227 RID: 4647 RVA: 0x0004E7D7 File Offset: 0x0004C9D7
		IControllerTemplateButton ISixDofControllerTemplate.button19
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(30);
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06001228 RID: 4648 RVA: 0x0004E7E1 File Offset: 0x0004C9E1
		IControllerTemplateButton ISixDofControllerTemplate.button20
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(31);
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06001229 RID: 4649 RVA: 0x0004E7EB File Offset: 0x0004C9EB
		IControllerTemplateButton ISixDofControllerTemplate.button21
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(55);
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x0600122A RID: 4650 RVA: 0x0004E7F5 File Offset: 0x0004C9F5
		IControllerTemplateButton ISixDofControllerTemplate.button22
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(56);
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x0600122B RID: 4651 RVA: 0x0004E7FF File Offset: 0x0004C9FF
		IControllerTemplateButton ISixDofControllerTemplate.button23
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(57);
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x0600122C RID: 4652 RVA: 0x0004E809 File Offset: 0x0004CA09
		IControllerTemplateButton ISixDofControllerTemplate.button24
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(58);
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x0600122D RID: 4653 RVA: 0x0004E813 File Offset: 0x0004CA13
		IControllerTemplateButton ISixDofControllerTemplate.button25
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(59);
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x0600122E RID: 4654 RVA: 0x0004E81D File Offset: 0x0004CA1D
		IControllerTemplateButton ISixDofControllerTemplate.button26
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(60);
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x0600122F RID: 4655 RVA: 0x0004E827 File Offset: 0x0004CA27
		IControllerTemplateButton ISixDofControllerTemplate.button27
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(61);
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06001230 RID: 4656 RVA: 0x0004E831 File Offset: 0x0004CA31
		IControllerTemplateButton ISixDofControllerTemplate.button28
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(62);
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06001231 RID: 4657 RVA: 0x0004E83B File Offset: 0x0004CA3B
		IControllerTemplateButton ISixDofControllerTemplate.button29
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(63);
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06001232 RID: 4658 RVA: 0x0004E845 File Offset: 0x0004CA45
		IControllerTemplateButton ISixDofControllerTemplate.button30
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(64);
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06001233 RID: 4659 RVA: 0x0004E84F File Offset: 0x0004CA4F
		IControllerTemplateButton ISixDofControllerTemplate.button31
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(65);
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x0004E859 File Offset: 0x0004CA59
		IControllerTemplateButton ISixDofControllerTemplate.button32
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(66);
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06001235 RID: 4661 RVA: 0x0004E863 File Offset: 0x0004CA63
		IControllerTemplateHat ISixDofControllerTemplate.hat1
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(48);
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06001236 RID: 4662 RVA: 0x0004E86D File Offset: 0x0004CA6D
		IControllerTemplateHat ISixDofControllerTemplate.hat2
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(49);
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06001237 RID: 4663 RVA: 0x0004E877 File Offset: 0x0004CA77
		IControllerTemplateThrottle ISixDofControllerTemplate.throttle1
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(52);
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06001238 RID: 4664 RVA: 0x0004E881 File Offset: 0x0004CA81
		IControllerTemplateThrottle ISixDofControllerTemplate.throttle2
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(53);
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06001239 RID: 4665 RVA: 0x0004E88B File Offset: 0x0004CA8B
		IControllerTemplateStick6D ISixDofControllerTemplate.stick
		{
			get
			{
				return base.GetElement<IControllerTemplateStick6D>(54);
			}
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x0004E895 File Offset: 0x0004CA95
		public SixDofControllerTemplate(object payload)
			: base(payload)
		{
		}

		// Token: 0x040016CF RID: 5839
		public static readonly Guid typeGuid = new Guid("2599beb3-522b-43dd-a4ef-93fd60e5eafa");

		// Token: 0x040016D0 RID: 5840
		public const int elementId_positionX = 1;

		// Token: 0x040016D1 RID: 5841
		public const int elementId_positionY = 2;

		// Token: 0x040016D2 RID: 5842
		public const int elementId_positionZ = 0;

		// Token: 0x040016D3 RID: 5843
		public const int elementId_rotationX = 3;

		// Token: 0x040016D4 RID: 5844
		public const int elementId_rotationY = 5;

		// Token: 0x040016D5 RID: 5845
		public const int elementId_rotationZ = 4;

		// Token: 0x040016D6 RID: 5846
		public const int elementId_throttle1Axis = 6;

		// Token: 0x040016D7 RID: 5847
		public const int elementId_throttle1MinDetent = 50;

		// Token: 0x040016D8 RID: 5848
		public const int elementId_throttle2Axis = 7;

		// Token: 0x040016D9 RID: 5849
		public const int elementId_throttle2MinDetent = 51;

		// Token: 0x040016DA RID: 5850
		public const int elementId_extraAxis1 = 8;

		// Token: 0x040016DB RID: 5851
		public const int elementId_extraAxis2 = 9;

		// Token: 0x040016DC RID: 5852
		public const int elementId_extraAxis3 = 10;

		// Token: 0x040016DD RID: 5853
		public const int elementId_extraAxis4 = 11;

		// Token: 0x040016DE RID: 5854
		public const int elementId_button1 = 12;

		// Token: 0x040016DF RID: 5855
		public const int elementId_button2 = 13;

		// Token: 0x040016E0 RID: 5856
		public const int elementId_button3 = 14;

		// Token: 0x040016E1 RID: 5857
		public const int elementId_button4 = 15;

		// Token: 0x040016E2 RID: 5858
		public const int elementId_button5 = 16;

		// Token: 0x040016E3 RID: 5859
		public const int elementId_button6 = 17;

		// Token: 0x040016E4 RID: 5860
		public const int elementId_button7 = 18;

		// Token: 0x040016E5 RID: 5861
		public const int elementId_button8 = 19;

		// Token: 0x040016E6 RID: 5862
		public const int elementId_button9 = 20;

		// Token: 0x040016E7 RID: 5863
		public const int elementId_button10 = 21;

		// Token: 0x040016E8 RID: 5864
		public const int elementId_button11 = 22;

		// Token: 0x040016E9 RID: 5865
		public const int elementId_button12 = 23;

		// Token: 0x040016EA RID: 5866
		public const int elementId_button13 = 24;

		// Token: 0x040016EB RID: 5867
		public const int elementId_button14 = 25;

		// Token: 0x040016EC RID: 5868
		public const int elementId_button15 = 26;

		// Token: 0x040016ED RID: 5869
		public const int elementId_button16 = 27;

		// Token: 0x040016EE RID: 5870
		public const int elementId_button17 = 28;

		// Token: 0x040016EF RID: 5871
		public const int elementId_button18 = 29;

		// Token: 0x040016F0 RID: 5872
		public const int elementId_button19 = 30;

		// Token: 0x040016F1 RID: 5873
		public const int elementId_button20 = 31;

		// Token: 0x040016F2 RID: 5874
		public const int elementId_button21 = 55;

		// Token: 0x040016F3 RID: 5875
		public const int elementId_button22 = 56;

		// Token: 0x040016F4 RID: 5876
		public const int elementId_button23 = 57;

		// Token: 0x040016F5 RID: 5877
		public const int elementId_button24 = 58;

		// Token: 0x040016F6 RID: 5878
		public const int elementId_button25 = 59;

		// Token: 0x040016F7 RID: 5879
		public const int elementId_button26 = 60;

		// Token: 0x040016F8 RID: 5880
		public const int elementId_button27 = 61;

		// Token: 0x040016F9 RID: 5881
		public const int elementId_button28 = 62;

		// Token: 0x040016FA RID: 5882
		public const int elementId_button29 = 63;

		// Token: 0x040016FB RID: 5883
		public const int elementId_button30 = 64;

		// Token: 0x040016FC RID: 5884
		public const int elementId_button31 = 65;

		// Token: 0x040016FD RID: 5885
		public const int elementId_button32 = 66;

		// Token: 0x040016FE RID: 5886
		public const int elementId_hat1Up = 32;

		// Token: 0x040016FF RID: 5887
		public const int elementId_hat1UpRight = 33;

		// Token: 0x04001700 RID: 5888
		public const int elementId_hat1Right = 34;

		// Token: 0x04001701 RID: 5889
		public const int elementId_hat1DownRight = 35;

		// Token: 0x04001702 RID: 5890
		public const int elementId_hat1Down = 36;

		// Token: 0x04001703 RID: 5891
		public const int elementId_hat1DownLeft = 37;

		// Token: 0x04001704 RID: 5892
		public const int elementId_hat1Left = 38;

		// Token: 0x04001705 RID: 5893
		public const int elementId_hat1UpLeft = 39;

		// Token: 0x04001706 RID: 5894
		public const int elementId_hat2Up = 40;

		// Token: 0x04001707 RID: 5895
		public const int elementId_hat2UpRight = 41;

		// Token: 0x04001708 RID: 5896
		public const int elementId_hat2Right = 42;

		// Token: 0x04001709 RID: 5897
		public const int elementId_hat2DownRight = 43;

		// Token: 0x0400170A RID: 5898
		public const int elementId_hat2Down = 44;

		// Token: 0x0400170B RID: 5899
		public const int elementId_hat2DownLeft = 45;

		// Token: 0x0400170C RID: 5900
		public const int elementId_hat2Left = 46;

		// Token: 0x0400170D RID: 5901
		public const int elementId_hat2UpLeft = 47;

		// Token: 0x0400170E RID: 5902
		public const int elementId_hat1 = 48;

		// Token: 0x0400170F RID: 5903
		public const int elementId_hat2 = 49;

		// Token: 0x04001710 RID: 5904
		public const int elementId_throttle1 = 52;

		// Token: 0x04001711 RID: 5905
		public const int elementId_throttle2 = 53;

		// Token: 0x04001712 RID: 5906
		public const int elementId_stick = 54;
	}
}
