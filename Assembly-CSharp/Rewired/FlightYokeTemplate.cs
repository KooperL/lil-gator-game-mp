using System;

namespace Rewired
{
	// Token: 0x02000404 RID: 1028
	public sealed class FlightYokeTemplate : ControllerTemplate, IFlightYokeTemplate, IControllerTemplate
	{
		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06001508 RID: 5384 RVA: 0x00010A96 File Offset: 0x0000EC96
		IControllerTemplateButton IFlightYokeTemplate.leftPaddle
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(59);
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06001509 RID: 5385 RVA: 0x00010AA0 File Offset: 0x0000ECA0
		IControllerTemplateButton IFlightYokeTemplate.rightPaddle
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(60);
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x0600150A RID: 5386 RVA: 0x0001081B File Offset: 0x0000EA1B
		IControllerTemplateButton IFlightYokeTemplate.leftGripButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(7);
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x0600150B RID: 5387 RVA: 0x00010824 File Offset: 0x0000EA24
		IControllerTemplateButton IFlightYokeTemplate.leftGripButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(8);
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x0600150C RID: 5388 RVA: 0x0001082D File Offset: 0x0000EA2D
		IControllerTemplateButton IFlightYokeTemplate.leftGripButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(9);
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x0600150D RID: 5389 RVA: 0x00010837 File Offset: 0x0000EA37
		IControllerTemplateButton IFlightYokeTemplate.leftGripButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(10);
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x0600150E RID: 5390 RVA: 0x000108D9 File Offset: 0x0000EAD9
		IControllerTemplateButton IFlightYokeTemplate.leftGripButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(11);
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x0600150F RID: 5391 RVA: 0x0001084B File Offset: 0x0000EA4B
		IControllerTemplateButton IFlightYokeTemplate.leftGripButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(12);
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06001510 RID: 5392 RVA: 0x000108E3 File Offset: 0x0000EAE3
		IControllerTemplateButton IFlightYokeTemplate.rightGripButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(13);
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06001511 RID: 5393 RVA: 0x0001085F File Offset: 0x0000EA5F
		IControllerTemplateButton IFlightYokeTemplate.rightGripButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(14);
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06001512 RID: 5394 RVA: 0x00010869 File Offset: 0x0000EA69
		IControllerTemplateButton IFlightYokeTemplate.rightGripButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(15);
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001513 RID: 5395 RVA: 0x00010873 File Offset: 0x0000EA73
		IControllerTemplateButton IFlightYokeTemplate.rightGripButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(16);
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001514 RID: 5396 RVA: 0x000108ED File Offset: 0x0000EAED
		IControllerTemplateButton IFlightYokeTemplate.rightGripButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(17);
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06001515 RID: 5397 RVA: 0x000108F7 File Offset: 0x0000EAF7
		IControllerTemplateButton IFlightYokeTemplate.rightGripButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(18);
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06001516 RID: 5398 RVA: 0x00010901 File Offset: 0x0000EB01
		IControllerTemplateButton IFlightYokeTemplate.centerButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(19);
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001517 RID: 5399 RVA: 0x0001090B File Offset: 0x0000EB0B
		IControllerTemplateButton IFlightYokeTemplate.centerButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(20);
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06001518 RID: 5400 RVA: 0x00010915 File Offset: 0x0000EB15
		IControllerTemplateButton IFlightYokeTemplate.centerButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(21);
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06001519 RID: 5401 RVA: 0x0001091F File Offset: 0x0000EB1F
		IControllerTemplateButton IFlightYokeTemplate.centerButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(22);
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x0600151A RID: 5402 RVA: 0x00010929 File Offset: 0x0000EB29
		IControllerTemplateButton IFlightYokeTemplate.centerButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(23);
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x0600151B RID: 5403 RVA: 0x00010933 File Offset: 0x0000EB33
		IControllerTemplateButton IFlightYokeTemplate.centerButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(24);
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x0600151C RID: 5404 RVA: 0x0001093D File Offset: 0x0000EB3D
		IControllerTemplateButton IFlightYokeTemplate.centerButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(25);
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x0600151D RID: 5405 RVA: 0x00010947 File Offset: 0x0000EB47
		IControllerTemplateButton IFlightYokeTemplate.centerButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(26);
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x0600151E RID: 5406 RVA: 0x00010A5A File Offset: 0x0000EC5A
		IControllerTemplateButton IFlightYokeTemplate.wheel1Up
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(53);
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x0600151F RID: 5407 RVA: 0x00010A64 File Offset: 0x0000EC64
		IControllerTemplateButton IFlightYokeTemplate.wheel1Down
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(54);
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06001520 RID: 5408 RVA: 0x00010A6E File Offset: 0x0000EC6E
		IControllerTemplateButton IFlightYokeTemplate.wheel1Press
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(55);
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06001521 RID: 5409 RVA: 0x00010A78 File Offset: 0x0000EC78
		IControllerTemplateButton IFlightYokeTemplate.wheel2Up
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(56);
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06001522 RID: 5410 RVA: 0x00010A82 File Offset: 0x0000EC82
		IControllerTemplateButton IFlightYokeTemplate.wheel2Down
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(57);
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06001523 RID: 5411 RVA: 0x00010A8C File Offset: 0x0000EC8C
		IControllerTemplateButton IFlightYokeTemplate.wheel2Press
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(58);
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06001524 RID: 5412 RVA: 0x000109D3 File Offset: 0x0000EBD3
		IControllerTemplateButton IFlightYokeTemplate.consoleButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(43);
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06001525 RID: 5413 RVA: 0x000109AB File Offset: 0x0000EBAB
		IControllerTemplateButton IFlightYokeTemplate.consoleButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(44);
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06001526 RID: 5414 RVA: 0x00010A28 File Offset: 0x0000EC28
		IControllerTemplateButton IFlightYokeTemplate.consoleButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(45);
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06001527 RID: 5415 RVA: 0x00010A32 File Offset: 0x0000EC32
		IControllerTemplateButton IFlightYokeTemplate.consoleButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(46);
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06001528 RID: 5416 RVA: 0x00010D01 File Offset: 0x0000EF01
		IControllerTemplateButton IFlightYokeTemplate.consoleButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(47);
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x00010D0B File Offset: 0x0000EF0B
		IControllerTemplateButton IFlightYokeTemplate.consoleButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(48);
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x0600152A RID: 5418 RVA: 0x00010D15 File Offset: 0x0000EF15
		IControllerTemplateButton IFlightYokeTemplate.consoleButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(49);
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x0600152B RID: 5419 RVA: 0x00010A3C File Offset: 0x0000EC3C
		IControllerTemplateButton IFlightYokeTemplate.consoleButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(50);
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x0600152C RID: 5420 RVA: 0x00010A46 File Offset: 0x0000EC46
		IControllerTemplateButton IFlightYokeTemplate.consoleButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(51);
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x0600152D RID: 5421 RVA: 0x00010A50 File Offset: 0x0000EC50
		IControllerTemplateButton IFlightYokeTemplate.consoleButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(52);
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x00010AAA File Offset: 0x0000ECAA
		IControllerTemplateButton IFlightYokeTemplate.mode1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(61);
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x0600152F RID: 5423 RVA: 0x00010AB4 File Offset: 0x0000ECB4
		IControllerTemplateButton IFlightYokeTemplate.mode2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(62);
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001530 RID: 5424 RVA: 0x00010ABE File Offset: 0x0000ECBE
		IControllerTemplateButton IFlightYokeTemplate.mode3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(63);
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001531 RID: 5425 RVA: 0x00010D1F File Offset: 0x0000EF1F
		IControllerTemplateYoke IFlightYokeTemplate.yoke
		{
			get
			{
				return base.GetElement<IControllerTemplateYoke>(69);
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001532 RID: 5426 RVA: 0x00010D29 File Offset: 0x0000EF29
		IControllerTemplateThrottle IFlightYokeTemplate.lever1
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(70);
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001533 RID: 5427 RVA: 0x00010D33 File Offset: 0x0000EF33
		IControllerTemplateThrottle IFlightYokeTemplate.lever2
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(71);
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001534 RID: 5428 RVA: 0x00010D3D File Offset: 0x0000EF3D
		IControllerTemplateThrottle IFlightYokeTemplate.lever3
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(72);
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001535 RID: 5429 RVA: 0x00010D47 File Offset: 0x0000EF47
		IControllerTemplateThrottle IFlightYokeTemplate.lever4
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(73);
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06001536 RID: 5430 RVA: 0x00010D51 File Offset: 0x0000EF51
		IControllerTemplateThrottle IFlightYokeTemplate.lever5
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(74);
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06001537 RID: 5431 RVA: 0x00010D5B File Offset: 0x0000EF5B
		IControllerTemplateHat IFlightYokeTemplate.leftGripHat
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(75);
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06001538 RID: 5432 RVA: 0x00010D65 File Offset: 0x0000EF65
		IControllerTemplateHat IFlightYokeTemplate.rightGripHat
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(76);
			}
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x0001089B File Offset: 0x0000EA9B
		public FlightYokeTemplate(object payload)
			: base(payload)
		{
		}

		// Token: 0x04001A4A RID: 6730
		public static readonly Guid typeGuid = new Guid("f311fa16-0ccc-41c0-ac4b-50f7100bb8ff");

		// Token: 0x04001A4B RID: 6731
		public const int elementId_rotateYoke = 0;

		// Token: 0x04001A4C RID: 6732
		public const int elementId_yokeZ = 1;

		// Token: 0x04001A4D RID: 6733
		public const int elementId_leftPaddle = 59;

		// Token: 0x04001A4E RID: 6734
		public const int elementId_rightPaddle = 60;

		// Token: 0x04001A4F RID: 6735
		public const int elementId_lever1Axis = 2;

		// Token: 0x04001A50 RID: 6736
		public const int elementId_lever1MinDetent = 64;

		// Token: 0x04001A51 RID: 6737
		public const int elementId_lever2Axis = 3;

		// Token: 0x04001A52 RID: 6738
		public const int elementId_lever2MinDetent = 65;

		// Token: 0x04001A53 RID: 6739
		public const int elementId_lever3Axis = 4;

		// Token: 0x04001A54 RID: 6740
		public const int elementId_lever3MinDetent = 66;

		// Token: 0x04001A55 RID: 6741
		public const int elementId_lever4Axis = 5;

		// Token: 0x04001A56 RID: 6742
		public const int elementId_lever4MinDetent = 67;

		// Token: 0x04001A57 RID: 6743
		public const int elementId_lever5Axis = 6;

		// Token: 0x04001A58 RID: 6744
		public const int elementId_lever5MinDetent = 68;

		// Token: 0x04001A59 RID: 6745
		public const int elementId_leftGripButton1 = 7;

		// Token: 0x04001A5A RID: 6746
		public const int elementId_leftGripButton2 = 8;

		// Token: 0x04001A5B RID: 6747
		public const int elementId_leftGripButton3 = 9;

		// Token: 0x04001A5C RID: 6748
		public const int elementId_leftGripButton4 = 10;

		// Token: 0x04001A5D RID: 6749
		public const int elementId_leftGripButton5 = 11;

		// Token: 0x04001A5E RID: 6750
		public const int elementId_leftGripButton6 = 12;

		// Token: 0x04001A5F RID: 6751
		public const int elementId_rightGripButton1 = 13;

		// Token: 0x04001A60 RID: 6752
		public const int elementId_rightGripButton2 = 14;

		// Token: 0x04001A61 RID: 6753
		public const int elementId_rightGripButton3 = 15;

		// Token: 0x04001A62 RID: 6754
		public const int elementId_rightGripButton4 = 16;

		// Token: 0x04001A63 RID: 6755
		public const int elementId_rightGripButton5 = 17;

		// Token: 0x04001A64 RID: 6756
		public const int elementId_rightGripButton6 = 18;

		// Token: 0x04001A65 RID: 6757
		public const int elementId_centerButton1 = 19;

		// Token: 0x04001A66 RID: 6758
		public const int elementId_centerButton2 = 20;

		// Token: 0x04001A67 RID: 6759
		public const int elementId_centerButton3 = 21;

		// Token: 0x04001A68 RID: 6760
		public const int elementId_centerButton4 = 22;

		// Token: 0x04001A69 RID: 6761
		public const int elementId_centerButton5 = 23;

		// Token: 0x04001A6A RID: 6762
		public const int elementId_centerButton6 = 24;

		// Token: 0x04001A6B RID: 6763
		public const int elementId_centerButton7 = 25;

		// Token: 0x04001A6C RID: 6764
		public const int elementId_centerButton8 = 26;

		// Token: 0x04001A6D RID: 6765
		public const int elementId_wheel1Up = 53;

		// Token: 0x04001A6E RID: 6766
		public const int elementId_wheel1Down = 54;

		// Token: 0x04001A6F RID: 6767
		public const int elementId_wheel1Press = 55;

		// Token: 0x04001A70 RID: 6768
		public const int elementId_wheel2Up = 56;

		// Token: 0x04001A71 RID: 6769
		public const int elementId_wheel2Down = 57;

		// Token: 0x04001A72 RID: 6770
		public const int elementId_wheel2Press = 58;

		// Token: 0x04001A73 RID: 6771
		public const int elementId_leftGripHatUp = 27;

		// Token: 0x04001A74 RID: 6772
		public const int elementId_leftGripHatUpRight = 28;

		// Token: 0x04001A75 RID: 6773
		public const int elementId_leftGripHatRight = 29;

		// Token: 0x04001A76 RID: 6774
		public const int elementId_leftGripHatDownRight = 30;

		// Token: 0x04001A77 RID: 6775
		public const int elementId_leftGripHatDown = 31;

		// Token: 0x04001A78 RID: 6776
		public const int elementId_leftGripHatDownLeft = 32;

		// Token: 0x04001A79 RID: 6777
		public const int elementId_leftGripHatLeft = 33;

		// Token: 0x04001A7A RID: 6778
		public const int elementId_leftGripHatUpLeft = 34;

		// Token: 0x04001A7B RID: 6779
		public const int elementId_rightGripHatUp = 35;

		// Token: 0x04001A7C RID: 6780
		public const int elementId_rightGripHatUpRight = 36;

		// Token: 0x04001A7D RID: 6781
		public const int elementId_rightGripHatRight = 37;

		// Token: 0x04001A7E RID: 6782
		public const int elementId_rightGripHatDownRight = 38;

		// Token: 0x04001A7F RID: 6783
		public const int elementId_rightGripHatDown = 39;

		// Token: 0x04001A80 RID: 6784
		public const int elementId_rightGripHatDownLeft = 40;

		// Token: 0x04001A81 RID: 6785
		public const int elementId_rightGripHatLeft = 41;

		// Token: 0x04001A82 RID: 6786
		public const int elementId_rightGripHatUpLeft = 42;

		// Token: 0x04001A83 RID: 6787
		public const int elementId_consoleButton1 = 43;

		// Token: 0x04001A84 RID: 6788
		public const int elementId_consoleButton2 = 44;

		// Token: 0x04001A85 RID: 6789
		public const int elementId_consoleButton3 = 45;

		// Token: 0x04001A86 RID: 6790
		public const int elementId_consoleButton4 = 46;

		// Token: 0x04001A87 RID: 6791
		public const int elementId_consoleButton5 = 47;

		// Token: 0x04001A88 RID: 6792
		public const int elementId_consoleButton6 = 48;

		// Token: 0x04001A89 RID: 6793
		public const int elementId_consoleButton7 = 49;

		// Token: 0x04001A8A RID: 6794
		public const int elementId_consoleButton8 = 50;

		// Token: 0x04001A8B RID: 6795
		public const int elementId_consoleButton9 = 51;

		// Token: 0x04001A8C RID: 6796
		public const int elementId_consoleButton10 = 52;

		// Token: 0x04001A8D RID: 6797
		public const int elementId_mode1 = 61;

		// Token: 0x04001A8E RID: 6798
		public const int elementId_mode2 = 62;

		// Token: 0x04001A8F RID: 6799
		public const int elementId_mode3 = 63;

		// Token: 0x04001A90 RID: 6800
		public const int elementId_yoke = 69;

		// Token: 0x04001A91 RID: 6801
		public const int elementId_lever1 = 70;

		// Token: 0x04001A92 RID: 6802
		public const int elementId_lever2 = 71;

		// Token: 0x04001A93 RID: 6803
		public const int elementId_lever3 = 72;

		// Token: 0x04001A94 RID: 6804
		public const int elementId_lever4 = 73;

		// Token: 0x04001A95 RID: 6805
		public const int elementId_lever5 = 74;

		// Token: 0x04001A96 RID: 6806
		public const int elementId_leftGripHat = 75;

		// Token: 0x04001A97 RID: 6807
		public const int elementId_rightGripHat = 76;
	}
}
