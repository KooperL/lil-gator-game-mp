using System;

namespace Rewired
{
	// Token: 0x02000406 RID: 1030
	public sealed class SixDofControllerTemplate : ControllerTemplate, ISixDofControllerTemplate, IControllerTemplate
	{
		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001540 RID: 5440 RVA: 0x00010D91 File Offset: 0x0000EF91
		IControllerTemplateAxis ISixDofControllerTemplate.extraAxis1
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(8);
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001541 RID: 5441 RVA: 0x00010D9A File Offset: 0x0000EF9A
		IControllerTemplateAxis ISixDofControllerTemplate.extraAxis2
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(9);
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06001542 RID: 5442 RVA: 0x00010DA4 File Offset: 0x0000EFA4
		IControllerTemplateAxis ISixDofControllerTemplate.extraAxis3
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(10);
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06001543 RID: 5443 RVA: 0x00010841 File Offset: 0x0000EA41
		IControllerTemplateAxis ISixDofControllerTemplate.extraAxis4
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(11);
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06001544 RID: 5444 RVA: 0x0001084B File Offset: 0x0000EA4B
		IControllerTemplateButton ISixDofControllerTemplate.button1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(12);
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06001545 RID: 5445 RVA: 0x000108E3 File Offset: 0x0000EAE3
		IControllerTemplateButton ISixDofControllerTemplate.button2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(13);
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001546 RID: 5446 RVA: 0x0001085F File Offset: 0x0000EA5F
		IControllerTemplateButton ISixDofControllerTemplate.button3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(14);
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06001547 RID: 5447 RVA: 0x00010869 File Offset: 0x0000EA69
		IControllerTemplateButton ISixDofControllerTemplate.button4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(15);
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06001548 RID: 5448 RVA: 0x00010873 File Offset: 0x0000EA73
		IControllerTemplateButton ISixDofControllerTemplate.button5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(16);
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06001549 RID: 5449 RVA: 0x000108ED File Offset: 0x0000EAED
		IControllerTemplateButton ISixDofControllerTemplate.button6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(17);
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x000108F7 File Offset: 0x0000EAF7
		IControllerTemplateButton ISixDofControllerTemplate.button7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(18);
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x0600154B RID: 5451 RVA: 0x00010901 File Offset: 0x0000EB01
		IControllerTemplateButton ISixDofControllerTemplate.button8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(19);
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x0600154C RID: 5452 RVA: 0x0001090B File Offset: 0x0000EB0B
		IControllerTemplateButton ISixDofControllerTemplate.button9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(20);
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x0600154D RID: 5453 RVA: 0x00010915 File Offset: 0x0000EB15
		IControllerTemplateButton ISixDofControllerTemplate.button10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(21);
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x0600154E RID: 5454 RVA: 0x0001091F File Offset: 0x0000EB1F
		IControllerTemplateButton ISixDofControllerTemplate.button11
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(22);
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x0600154F RID: 5455 RVA: 0x00010929 File Offset: 0x0000EB29
		IControllerTemplateButton ISixDofControllerTemplate.button12
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(23);
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06001550 RID: 5456 RVA: 0x00010933 File Offset: 0x0000EB33
		IControllerTemplateButton ISixDofControllerTemplate.button13
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(24);
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06001551 RID: 5457 RVA: 0x0001093D File Offset: 0x0000EB3D
		IControllerTemplateButton ISixDofControllerTemplate.button14
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(25);
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06001552 RID: 5458 RVA: 0x00010947 File Offset: 0x0000EB47
		IControllerTemplateButton ISixDofControllerTemplate.button15
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(26);
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06001553 RID: 5459 RVA: 0x00010951 File Offset: 0x0000EB51
		IControllerTemplateButton ISixDofControllerTemplate.button16
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(27);
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x0001095B File Offset: 0x0000EB5B
		IControllerTemplateButton ISixDofControllerTemplate.button17
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(28);
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06001555 RID: 5461 RVA: 0x00010965 File Offset: 0x0000EB65
		IControllerTemplateButton ISixDofControllerTemplate.button18
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(29);
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06001556 RID: 5462 RVA: 0x0001096F File Offset: 0x0000EB6F
		IControllerTemplateButton ISixDofControllerTemplate.button19
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(30);
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001557 RID: 5463 RVA: 0x00010979 File Offset: 0x0000EB79
		IControllerTemplateButton ISixDofControllerTemplate.button20
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(31);
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06001558 RID: 5464 RVA: 0x00010A6E File Offset: 0x0000EC6E
		IControllerTemplateButton ISixDofControllerTemplate.button21
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(55);
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06001559 RID: 5465 RVA: 0x00010A78 File Offset: 0x0000EC78
		IControllerTemplateButton ISixDofControllerTemplate.button22
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(56);
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x0600155A RID: 5466 RVA: 0x00010A82 File Offset: 0x0000EC82
		IControllerTemplateButton ISixDofControllerTemplate.button23
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(57);
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x0600155B RID: 5467 RVA: 0x00010A8C File Offset: 0x0000EC8C
		IControllerTemplateButton ISixDofControllerTemplate.button24
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(58);
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x0600155C RID: 5468 RVA: 0x00010A96 File Offset: 0x0000EC96
		IControllerTemplateButton ISixDofControllerTemplate.button25
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(59);
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x0600155D RID: 5469 RVA: 0x00010AA0 File Offset: 0x0000ECA0
		IControllerTemplateButton ISixDofControllerTemplate.button26
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(60);
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x00010AAA File Offset: 0x0000ECAA
		IControllerTemplateButton ISixDofControllerTemplate.button27
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(61);
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x0600155F RID: 5471 RVA: 0x00010AB4 File Offset: 0x0000ECB4
		IControllerTemplateButton ISixDofControllerTemplate.button28
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(62);
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06001560 RID: 5472 RVA: 0x00010ABE File Offset: 0x0000ECBE
		IControllerTemplateButton ISixDofControllerTemplate.button29
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(63);
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06001561 RID: 5473 RVA: 0x00010AC8 File Offset: 0x0000ECC8
		IControllerTemplateButton ISixDofControllerTemplate.button30
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(64);
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06001562 RID: 5474 RVA: 0x00010AD2 File Offset: 0x0000ECD2
		IControllerTemplateButton ISixDofControllerTemplate.button31
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(65);
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06001563 RID: 5475 RVA: 0x00010ADC File Offset: 0x0000ECDC
		IControllerTemplateButton ISixDofControllerTemplate.button32
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(66);
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06001564 RID: 5476 RVA: 0x00010DAE File Offset: 0x0000EFAE
		IControllerTemplateHat ISixDofControllerTemplate.hat1
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(48);
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06001565 RID: 5477 RVA: 0x00010DB8 File Offset: 0x0000EFB8
		IControllerTemplateHat ISixDofControllerTemplate.hat2
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(49);
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06001566 RID: 5478 RVA: 0x00010DC2 File Offset: 0x0000EFC2
		IControllerTemplateThrottle ISixDofControllerTemplate.throttle1
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(52);
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001567 RID: 5479 RVA: 0x00010DCC File Offset: 0x0000EFCC
		IControllerTemplateThrottle ISixDofControllerTemplate.throttle2
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(53);
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001568 RID: 5480 RVA: 0x00010DD6 File Offset: 0x0000EFD6
		IControllerTemplateStick6D ISixDofControllerTemplate.stick
		{
			get
			{
				return base.GetElement<IControllerTemplateStick6D>(54);
			}
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x0001089B File Offset: 0x0000EA9B
		public SixDofControllerTemplate(object payload)
			: base(payload)
		{
		}

		// Token: 0x04001A9C RID: 6812
		public static readonly Guid typeGuid = new Guid("2599beb3-522b-43dd-a4ef-93fd60e5eafa");

		// Token: 0x04001A9D RID: 6813
		public const int elementId_positionX = 1;

		// Token: 0x04001A9E RID: 6814
		public const int elementId_positionY = 2;

		// Token: 0x04001A9F RID: 6815
		public const int elementId_positionZ = 0;

		// Token: 0x04001AA0 RID: 6816
		public const int elementId_rotationX = 3;

		// Token: 0x04001AA1 RID: 6817
		public const int elementId_rotationY = 5;

		// Token: 0x04001AA2 RID: 6818
		public const int elementId_rotationZ = 4;

		// Token: 0x04001AA3 RID: 6819
		public const int elementId_throttle1Axis = 6;

		// Token: 0x04001AA4 RID: 6820
		public const int elementId_throttle1MinDetent = 50;

		// Token: 0x04001AA5 RID: 6821
		public const int elementId_throttle2Axis = 7;

		// Token: 0x04001AA6 RID: 6822
		public const int elementId_throttle2MinDetent = 51;

		// Token: 0x04001AA7 RID: 6823
		public const int elementId_extraAxis1 = 8;

		// Token: 0x04001AA8 RID: 6824
		public const int elementId_extraAxis2 = 9;

		// Token: 0x04001AA9 RID: 6825
		public const int elementId_extraAxis3 = 10;

		// Token: 0x04001AAA RID: 6826
		public const int elementId_extraAxis4 = 11;

		// Token: 0x04001AAB RID: 6827
		public const int elementId_button1 = 12;

		// Token: 0x04001AAC RID: 6828
		public const int elementId_button2 = 13;

		// Token: 0x04001AAD RID: 6829
		public const int elementId_button3 = 14;

		// Token: 0x04001AAE RID: 6830
		public const int elementId_button4 = 15;

		// Token: 0x04001AAF RID: 6831
		public const int elementId_button5 = 16;

		// Token: 0x04001AB0 RID: 6832
		public const int elementId_button6 = 17;

		// Token: 0x04001AB1 RID: 6833
		public const int elementId_button7 = 18;

		// Token: 0x04001AB2 RID: 6834
		public const int elementId_button8 = 19;

		// Token: 0x04001AB3 RID: 6835
		public const int elementId_button9 = 20;

		// Token: 0x04001AB4 RID: 6836
		public const int elementId_button10 = 21;

		// Token: 0x04001AB5 RID: 6837
		public const int elementId_button11 = 22;

		// Token: 0x04001AB6 RID: 6838
		public const int elementId_button12 = 23;

		// Token: 0x04001AB7 RID: 6839
		public const int elementId_button13 = 24;

		// Token: 0x04001AB8 RID: 6840
		public const int elementId_button14 = 25;

		// Token: 0x04001AB9 RID: 6841
		public const int elementId_button15 = 26;

		// Token: 0x04001ABA RID: 6842
		public const int elementId_button16 = 27;

		// Token: 0x04001ABB RID: 6843
		public const int elementId_button17 = 28;

		// Token: 0x04001ABC RID: 6844
		public const int elementId_button18 = 29;

		// Token: 0x04001ABD RID: 6845
		public const int elementId_button19 = 30;

		// Token: 0x04001ABE RID: 6846
		public const int elementId_button20 = 31;

		// Token: 0x04001ABF RID: 6847
		public const int elementId_button21 = 55;

		// Token: 0x04001AC0 RID: 6848
		public const int elementId_button22 = 56;

		// Token: 0x04001AC1 RID: 6849
		public const int elementId_button23 = 57;

		// Token: 0x04001AC2 RID: 6850
		public const int elementId_button24 = 58;

		// Token: 0x04001AC3 RID: 6851
		public const int elementId_button25 = 59;

		// Token: 0x04001AC4 RID: 6852
		public const int elementId_button26 = 60;

		// Token: 0x04001AC5 RID: 6853
		public const int elementId_button27 = 61;

		// Token: 0x04001AC6 RID: 6854
		public const int elementId_button28 = 62;

		// Token: 0x04001AC7 RID: 6855
		public const int elementId_button29 = 63;

		// Token: 0x04001AC8 RID: 6856
		public const int elementId_button30 = 64;

		// Token: 0x04001AC9 RID: 6857
		public const int elementId_button31 = 65;

		// Token: 0x04001ACA RID: 6858
		public const int elementId_button32 = 66;

		// Token: 0x04001ACB RID: 6859
		public const int elementId_hat1Up = 32;

		// Token: 0x04001ACC RID: 6860
		public const int elementId_hat1UpRight = 33;

		// Token: 0x04001ACD RID: 6861
		public const int elementId_hat1Right = 34;

		// Token: 0x04001ACE RID: 6862
		public const int elementId_hat1DownRight = 35;

		// Token: 0x04001ACF RID: 6863
		public const int elementId_hat1Down = 36;

		// Token: 0x04001AD0 RID: 6864
		public const int elementId_hat1DownLeft = 37;

		// Token: 0x04001AD1 RID: 6865
		public const int elementId_hat1Left = 38;

		// Token: 0x04001AD2 RID: 6866
		public const int elementId_hat1UpLeft = 39;

		// Token: 0x04001AD3 RID: 6867
		public const int elementId_hat2Up = 40;

		// Token: 0x04001AD4 RID: 6868
		public const int elementId_hat2UpRight = 41;

		// Token: 0x04001AD5 RID: 6869
		public const int elementId_hat2Right = 42;

		// Token: 0x04001AD6 RID: 6870
		public const int elementId_hat2DownRight = 43;

		// Token: 0x04001AD7 RID: 6871
		public const int elementId_hat2Down = 44;

		// Token: 0x04001AD8 RID: 6872
		public const int elementId_hat2DownLeft = 45;

		// Token: 0x04001AD9 RID: 6873
		public const int elementId_hat2Left = 46;

		// Token: 0x04001ADA RID: 6874
		public const int elementId_hat2UpLeft = 47;

		// Token: 0x04001ADB RID: 6875
		public const int elementId_hat1 = 48;

		// Token: 0x04001ADC RID: 6876
		public const int elementId_hat2 = 49;

		// Token: 0x04001ADD RID: 6877
		public const int elementId_throttle1 = 52;

		// Token: 0x04001ADE RID: 6878
		public const int elementId_throttle2 = 53;

		// Token: 0x04001ADF RID: 6879
		public const int elementId_stick = 54;
	}
}
