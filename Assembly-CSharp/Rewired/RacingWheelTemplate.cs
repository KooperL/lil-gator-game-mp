using System;

namespace Rewired
{
	// Token: 0x02000402 RID: 1026
	public sealed class RacingWheelTemplate : ControllerTemplate, IRacingWheelTemplate, IControllerTemplate
	{
		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06001482 RID: 5250 RVA: 0x000108B5 File Offset: 0x0000EAB5
		IControllerTemplateAxis IRacingWheelTemplate.wheel
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(0);
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06001483 RID: 5251 RVA: 0x000108BE File Offset: 0x0000EABE
		IControllerTemplateAxis IRacingWheelTemplate.accelerator
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(1);
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06001484 RID: 5252 RVA: 0x000108C7 File Offset: 0x0000EAC7
		IControllerTemplateAxis IRacingWheelTemplate.brake
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(2);
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06001485 RID: 5253 RVA: 0x000108D0 File Offset: 0x0000EAD0
		IControllerTemplateAxis IRacingWheelTemplate.clutch
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(3);
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06001486 RID: 5254 RVA: 0x00010800 File Offset: 0x0000EA00
		IControllerTemplateButton IRacingWheelTemplate.shiftDown
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(4);
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06001487 RID: 5255 RVA: 0x00010809 File Offset: 0x0000EA09
		IControllerTemplateButton IRacingWheelTemplate.shiftUp
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(5);
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06001488 RID: 5256 RVA: 0x00010812 File Offset: 0x0000EA12
		IControllerTemplateButton IRacingWheelTemplate.wheelButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(6);
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06001489 RID: 5257 RVA: 0x0001081B File Offset: 0x0000EA1B
		IControllerTemplateButton IRacingWheelTemplate.wheelButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(7);
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x0600148A RID: 5258 RVA: 0x00010824 File Offset: 0x0000EA24
		IControllerTemplateButton IRacingWheelTemplate.wheelButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(8);
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x0600148B RID: 5259 RVA: 0x0001082D File Offset: 0x0000EA2D
		IControllerTemplateButton IRacingWheelTemplate.wheelButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(9);
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x0600148C RID: 5260 RVA: 0x00010837 File Offset: 0x0000EA37
		IControllerTemplateButton IRacingWheelTemplate.wheelButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(10);
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x0600148D RID: 5261 RVA: 0x000108D9 File Offset: 0x0000EAD9
		IControllerTemplateButton IRacingWheelTemplate.wheelButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(11);
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x0600148E RID: 5262 RVA: 0x0001084B File Offset: 0x0000EA4B
		IControllerTemplateButton IRacingWheelTemplate.wheelButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(12);
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x0600148F RID: 5263 RVA: 0x000108E3 File Offset: 0x0000EAE3
		IControllerTemplateButton IRacingWheelTemplate.wheelButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(13);
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06001490 RID: 5264 RVA: 0x0001085F File Offset: 0x0000EA5F
		IControllerTemplateButton IRacingWheelTemplate.wheelButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(14);
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x00010869 File Offset: 0x0000EA69
		IControllerTemplateButton IRacingWheelTemplate.wheelButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(15);
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06001492 RID: 5266 RVA: 0x00010873 File Offset: 0x0000EA73
		IControllerTemplateButton IRacingWheelTemplate.consoleButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(16);
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06001493 RID: 5267 RVA: 0x000108ED File Offset: 0x0000EAED
		IControllerTemplateButton IRacingWheelTemplate.consoleButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(17);
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x000108F7 File Offset: 0x0000EAF7
		IControllerTemplateButton IRacingWheelTemplate.consoleButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(18);
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06001495 RID: 5269 RVA: 0x00010901 File Offset: 0x0000EB01
		IControllerTemplateButton IRacingWheelTemplate.consoleButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(19);
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06001496 RID: 5270 RVA: 0x0001090B File Offset: 0x0000EB0B
		IControllerTemplateButton IRacingWheelTemplate.consoleButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(20);
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06001497 RID: 5271 RVA: 0x00010915 File Offset: 0x0000EB15
		IControllerTemplateButton IRacingWheelTemplate.consoleButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(21);
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06001498 RID: 5272 RVA: 0x0001091F File Offset: 0x0000EB1F
		IControllerTemplateButton IRacingWheelTemplate.consoleButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(22);
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06001499 RID: 5273 RVA: 0x00010929 File Offset: 0x0000EB29
		IControllerTemplateButton IRacingWheelTemplate.consoleButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(23);
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x0600149A RID: 5274 RVA: 0x00010933 File Offset: 0x0000EB33
		IControllerTemplateButton IRacingWheelTemplate.consoleButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(24);
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x0600149B RID: 5275 RVA: 0x0001093D File Offset: 0x0000EB3D
		IControllerTemplateButton IRacingWheelTemplate.consoleButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(25);
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x0600149C RID: 5276 RVA: 0x00010947 File Offset: 0x0000EB47
		IControllerTemplateButton IRacingWheelTemplate.shifter1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(26);
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x0600149D RID: 5277 RVA: 0x00010951 File Offset: 0x0000EB51
		IControllerTemplateButton IRacingWheelTemplate.shifter2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(27);
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x0001095B File Offset: 0x0000EB5B
		IControllerTemplateButton IRacingWheelTemplate.shifter3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(28);
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x0600149F RID: 5279 RVA: 0x00010965 File Offset: 0x0000EB65
		IControllerTemplateButton IRacingWheelTemplate.shifter4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(29);
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x0001096F File Offset: 0x0000EB6F
		IControllerTemplateButton IRacingWheelTemplate.shifter5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(30);
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x060014A1 RID: 5281 RVA: 0x00010979 File Offset: 0x0000EB79
		IControllerTemplateButton IRacingWheelTemplate.shifter6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(31);
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x00010983 File Offset: 0x0000EB83
		IControllerTemplateButton IRacingWheelTemplate.shifter7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(32);
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x060014A3 RID: 5283 RVA: 0x0001098D File Offset: 0x0000EB8D
		IControllerTemplateButton IRacingWheelTemplate.shifter8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(33);
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x060014A4 RID: 5284 RVA: 0x00010997 File Offset: 0x0000EB97
		IControllerTemplateButton IRacingWheelTemplate.shifter9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(34);
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x060014A5 RID: 5285 RVA: 0x000109A1 File Offset: 0x0000EBA1
		IControllerTemplateButton IRacingWheelTemplate.shifter10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(35);
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x060014A6 RID: 5286 RVA: 0x000109AB File Offset: 0x0000EBAB
		IControllerTemplateButton IRacingWheelTemplate.reverseGear
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(44);
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x060014A7 RID: 5287 RVA: 0x000109B5 File Offset: 0x0000EBB5
		IControllerTemplateButton IRacingWheelTemplate.select
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(36);
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x060014A8 RID: 5288 RVA: 0x000109BF File Offset: 0x0000EBBF
		IControllerTemplateButton IRacingWheelTemplate.start
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(37);
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x000109C9 File Offset: 0x0000EBC9
		IControllerTemplateButton IRacingWheelTemplate.systemButton
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(38);
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x060014AA RID: 5290 RVA: 0x000109D3 File Offset: 0x0000EBD3
		IControllerTemplateButton IRacingWheelTemplate.horn
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(43);
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x060014AB RID: 5291 RVA: 0x000109DD File Offset: 0x0000EBDD
		IControllerTemplateDPad IRacingWheelTemplate.dPad
		{
			get
			{
				return base.GetElement<IControllerTemplateDPad>(45);
			}
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x0001089B File Offset: 0x0000EA9B
		public RacingWheelTemplate(object payload)
			: base(payload)
		{
		}

		// Token: 0x04001972 RID: 6514
		public static readonly Guid typeGuid = new Guid("104e31d8-9115-4dd5-a398-2e54d35e6c83");

		// Token: 0x04001973 RID: 6515
		public const int elementId_wheel = 0;

		// Token: 0x04001974 RID: 6516
		public const int elementId_accelerator = 1;

		// Token: 0x04001975 RID: 6517
		public const int elementId_brake = 2;

		// Token: 0x04001976 RID: 6518
		public const int elementId_clutch = 3;

		// Token: 0x04001977 RID: 6519
		public const int elementId_shiftDown = 4;

		// Token: 0x04001978 RID: 6520
		public const int elementId_shiftUp = 5;

		// Token: 0x04001979 RID: 6521
		public const int elementId_wheelButton1 = 6;

		// Token: 0x0400197A RID: 6522
		public const int elementId_wheelButton2 = 7;

		// Token: 0x0400197B RID: 6523
		public const int elementId_wheelButton3 = 8;

		// Token: 0x0400197C RID: 6524
		public const int elementId_wheelButton4 = 9;

		// Token: 0x0400197D RID: 6525
		public const int elementId_wheelButton5 = 10;

		// Token: 0x0400197E RID: 6526
		public const int elementId_wheelButton6 = 11;

		// Token: 0x0400197F RID: 6527
		public const int elementId_wheelButton7 = 12;

		// Token: 0x04001980 RID: 6528
		public const int elementId_wheelButton8 = 13;

		// Token: 0x04001981 RID: 6529
		public const int elementId_wheelButton9 = 14;

		// Token: 0x04001982 RID: 6530
		public const int elementId_wheelButton10 = 15;

		// Token: 0x04001983 RID: 6531
		public const int elementId_consoleButton1 = 16;

		// Token: 0x04001984 RID: 6532
		public const int elementId_consoleButton2 = 17;

		// Token: 0x04001985 RID: 6533
		public const int elementId_consoleButton3 = 18;

		// Token: 0x04001986 RID: 6534
		public const int elementId_consoleButton4 = 19;

		// Token: 0x04001987 RID: 6535
		public const int elementId_consoleButton5 = 20;

		// Token: 0x04001988 RID: 6536
		public const int elementId_consoleButton6 = 21;

		// Token: 0x04001989 RID: 6537
		public const int elementId_consoleButton7 = 22;

		// Token: 0x0400198A RID: 6538
		public const int elementId_consoleButton8 = 23;

		// Token: 0x0400198B RID: 6539
		public const int elementId_consoleButton9 = 24;

		// Token: 0x0400198C RID: 6540
		public const int elementId_consoleButton10 = 25;

		// Token: 0x0400198D RID: 6541
		public const int elementId_shifter1 = 26;

		// Token: 0x0400198E RID: 6542
		public const int elementId_shifter2 = 27;

		// Token: 0x0400198F RID: 6543
		public const int elementId_shifter3 = 28;

		// Token: 0x04001990 RID: 6544
		public const int elementId_shifter4 = 29;

		// Token: 0x04001991 RID: 6545
		public const int elementId_shifter5 = 30;

		// Token: 0x04001992 RID: 6546
		public const int elementId_shifter6 = 31;

		// Token: 0x04001993 RID: 6547
		public const int elementId_shifter7 = 32;

		// Token: 0x04001994 RID: 6548
		public const int elementId_shifter8 = 33;

		// Token: 0x04001995 RID: 6549
		public const int elementId_shifter9 = 34;

		// Token: 0x04001996 RID: 6550
		public const int elementId_shifter10 = 35;

		// Token: 0x04001997 RID: 6551
		public const int elementId_reverseGear = 44;

		// Token: 0x04001998 RID: 6552
		public const int elementId_select = 36;

		// Token: 0x04001999 RID: 6553
		public const int elementId_start = 37;

		// Token: 0x0400199A RID: 6554
		public const int elementId_systemButton = 38;

		// Token: 0x0400199B RID: 6555
		public const int elementId_horn = 43;

		// Token: 0x0400199C RID: 6556
		public const int elementId_dPadUp = 39;

		// Token: 0x0400199D RID: 6557
		public const int elementId_dPadRight = 40;

		// Token: 0x0400199E RID: 6558
		public const int elementId_dPadDown = 41;

		// Token: 0x0400199F RID: 6559
		public const int elementId_dPadLeft = 42;

		// Token: 0x040019A0 RID: 6560
		public const int elementId_dPad = 45;
	}
}
