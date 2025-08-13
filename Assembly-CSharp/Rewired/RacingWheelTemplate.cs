using System;

namespace Rewired
{
	// Token: 0x02000309 RID: 777
	public sealed class RacingWheelTemplate : ControllerTemplate, IRacingWheelTemplate, IControllerTemplate
	{
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06001153 RID: 4435 RVA: 0x0004DF1D File Offset: 0x0004C11D
		IControllerTemplateAxis IRacingWheelTemplate.wheel
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(0);
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06001154 RID: 4436 RVA: 0x0004DF26 File Offset: 0x0004C126
		IControllerTemplateAxis IRacingWheelTemplate.accelerator
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(1);
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06001155 RID: 4437 RVA: 0x0004DF2F File Offset: 0x0004C12F
		IControllerTemplateAxis IRacingWheelTemplate.brake
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(2);
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06001156 RID: 4438 RVA: 0x0004DF38 File Offset: 0x0004C138
		IControllerTemplateAxis IRacingWheelTemplate.clutch
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(3);
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x0004DF41 File Offset: 0x0004C141
		IControllerTemplateButton IRacingWheelTemplate.shiftDown
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(4);
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06001158 RID: 4440 RVA: 0x0004DF4A File Offset: 0x0004C14A
		IControllerTemplateButton IRacingWheelTemplate.shiftUp
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(5);
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06001159 RID: 4441 RVA: 0x0004DF53 File Offset: 0x0004C153
		IControllerTemplateButton IRacingWheelTemplate.wheelButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(6);
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600115A RID: 4442 RVA: 0x0004DF5C File Offset: 0x0004C15C
		IControllerTemplateButton IRacingWheelTemplate.wheelButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(7);
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600115B RID: 4443 RVA: 0x0004DF65 File Offset: 0x0004C165
		IControllerTemplateButton IRacingWheelTemplate.wheelButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(8);
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600115C RID: 4444 RVA: 0x0004DF6E File Offset: 0x0004C16E
		IControllerTemplateButton IRacingWheelTemplate.wheelButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(9);
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600115D RID: 4445 RVA: 0x0004DF78 File Offset: 0x0004C178
		IControllerTemplateButton IRacingWheelTemplate.wheelButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(10);
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x0600115E RID: 4446 RVA: 0x0004DF82 File Offset: 0x0004C182
		IControllerTemplateButton IRacingWheelTemplate.wheelButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(11);
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600115F RID: 4447 RVA: 0x0004DF8C File Offset: 0x0004C18C
		IControllerTemplateButton IRacingWheelTemplate.wheelButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(12);
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06001160 RID: 4448 RVA: 0x0004DF96 File Offset: 0x0004C196
		IControllerTemplateButton IRacingWheelTemplate.wheelButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(13);
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06001161 RID: 4449 RVA: 0x0004DFA0 File Offset: 0x0004C1A0
		IControllerTemplateButton IRacingWheelTemplate.wheelButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(14);
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06001162 RID: 4450 RVA: 0x0004DFAA File Offset: 0x0004C1AA
		IControllerTemplateButton IRacingWheelTemplate.wheelButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(15);
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06001163 RID: 4451 RVA: 0x0004DFB4 File Offset: 0x0004C1B4
		IControllerTemplateButton IRacingWheelTemplate.consoleButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(16);
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06001164 RID: 4452 RVA: 0x0004DFBE File Offset: 0x0004C1BE
		IControllerTemplateButton IRacingWheelTemplate.consoleButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(17);
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06001165 RID: 4453 RVA: 0x0004DFC8 File Offset: 0x0004C1C8
		IControllerTemplateButton IRacingWheelTemplate.consoleButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(18);
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06001166 RID: 4454 RVA: 0x0004DFD2 File Offset: 0x0004C1D2
		IControllerTemplateButton IRacingWheelTemplate.consoleButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(19);
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06001167 RID: 4455 RVA: 0x0004DFDC File Offset: 0x0004C1DC
		IControllerTemplateButton IRacingWheelTemplate.consoleButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(20);
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06001168 RID: 4456 RVA: 0x0004DFE6 File Offset: 0x0004C1E6
		IControllerTemplateButton IRacingWheelTemplate.consoleButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(21);
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06001169 RID: 4457 RVA: 0x0004DFF0 File Offset: 0x0004C1F0
		IControllerTemplateButton IRacingWheelTemplate.consoleButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(22);
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600116A RID: 4458 RVA: 0x0004DFFA File Offset: 0x0004C1FA
		IControllerTemplateButton IRacingWheelTemplate.consoleButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(23);
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600116B RID: 4459 RVA: 0x0004E004 File Offset: 0x0004C204
		IControllerTemplateButton IRacingWheelTemplate.consoleButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(24);
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600116C RID: 4460 RVA: 0x0004E00E File Offset: 0x0004C20E
		IControllerTemplateButton IRacingWheelTemplate.consoleButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(25);
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600116D RID: 4461 RVA: 0x0004E018 File Offset: 0x0004C218
		IControllerTemplateButton IRacingWheelTemplate.shifter1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(26);
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600116E RID: 4462 RVA: 0x0004E022 File Offset: 0x0004C222
		IControllerTemplateButton IRacingWheelTemplate.shifter2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(27);
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600116F RID: 4463 RVA: 0x0004E02C File Offset: 0x0004C22C
		IControllerTemplateButton IRacingWheelTemplate.shifter3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(28);
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06001170 RID: 4464 RVA: 0x0004E036 File Offset: 0x0004C236
		IControllerTemplateButton IRacingWheelTemplate.shifter4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(29);
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06001171 RID: 4465 RVA: 0x0004E040 File Offset: 0x0004C240
		IControllerTemplateButton IRacingWheelTemplate.shifter5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(30);
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06001172 RID: 4466 RVA: 0x0004E04A File Offset: 0x0004C24A
		IControllerTemplateButton IRacingWheelTemplate.shifter6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(31);
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06001173 RID: 4467 RVA: 0x0004E054 File Offset: 0x0004C254
		IControllerTemplateButton IRacingWheelTemplate.shifter7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(32);
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06001174 RID: 4468 RVA: 0x0004E05E File Offset: 0x0004C25E
		IControllerTemplateButton IRacingWheelTemplate.shifter8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(33);
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06001175 RID: 4469 RVA: 0x0004E068 File Offset: 0x0004C268
		IControllerTemplateButton IRacingWheelTemplate.shifter9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(34);
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06001176 RID: 4470 RVA: 0x0004E072 File Offset: 0x0004C272
		IControllerTemplateButton IRacingWheelTemplate.shifter10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(35);
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06001177 RID: 4471 RVA: 0x0004E07C File Offset: 0x0004C27C
		IControllerTemplateButton IRacingWheelTemplate.reverseGear
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(44);
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06001178 RID: 4472 RVA: 0x0004E086 File Offset: 0x0004C286
		IControllerTemplateButton IRacingWheelTemplate.select
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(36);
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06001179 RID: 4473 RVA: 0x0004E090 File Offset: 0x0004C290
		IControllerTemplateButton IRacingWheelTemplate.start
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(37);
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x0600117A RID: 4474 RVA: 0x0004E09A File Offset: 0x0004C29A
		IControllerTemplateButton IRacingWheelTemplate.systemButton
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(38);
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x0600117B RID: 4475 RVA: 0x0004E0A4 File Offset: 0x0004C2A4
		IControllerTemplateButton IRacingWheelTemplate.horn
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(43);
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x0600117C RID: 4476 RVA: 0x0004E0AE File Offset: 0x0004C2AE
		IControllerTemplateDPad IRacingWheelTemplate.dPad
		{
			get
			{
				return base.GetElement<IControllerTemplateDPad>(45);
			}
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x0004E0B8 File Offset: 0x0004C2B8
		public RacingWheelTemplate(object payload)
			: base(payload)
		{
		}

		// Token: 0x040015A5 RID: 5541
		public static readonly Guid typeGuid = new Guid("104e31d8-9115-4dd5-a398-2e54d35e6c83");

		// Token: 0x040015A6 RID: 5542
		public const int elementId_wheel = 0;

		// Token: 0x040015A7 RID: 5543
		public const int elementId_accelerator = 1;

		// Token: 0x040015A8 RID: 5544
		public const int elementId_brake = 2;

		// Token: 0x040015A9 RID: 5545
		public const int elementId_clutch = 3;

		// Token: 0x040015AA RID: 5546
		public const int elementId_shiftDown = 4;

		// Token: 0x040015AB RID: 5547
		public const int elementId_shiftUp = 5;

		// Token: 0x040015AC RID: 5548
		public const int elementId_wheelButton1 = 6;

		// Token: 0x040015AD RID: 5549
		public const int elementId_wheelButton2 = 7;

		// Token: 0x040015AE RID: 5550
		public const int elementId_wheelButton3 = 8;

		// Token: 0x040015AF RID: 5551
		public const int elementId_wheelButton4 = 9;

		// Token: 0x040015B0 RID: 5552
		public const int elementId_wheelButton5 = 10;

		// Token: 0x040015B1 RID: 5553
		public const int elementId_wheelButton6 = 11;

		// Token: 0x040015B2 RID: 5554
		public const int elementId_wheelButton7 = 12;

		// Token: 0x040015B3 RID: 5555
		public const int elementId_wheelButton8 = 13;

		// Token: 0x040015B4 RID: 5556
		public const int elementId_wheelButton9 = 14;

		// Token: 0x040015B5 RID: 5557
		public const int elementId_wheelButton10 = 15;

		// Token: 0x040015B6 RID: 5558
		public const int elementId_consoleButton1 = 16;

		// Token: 0x040015B7 RID: 5559
		public const int elementId_consoleButton2 = 17;

		// Token: 0x040015B8 RID: 5560
		public const int elementId_consoleButton3 = 18;

		// Token: 0x040015B9 RID: 5561
		public const int elementId_consoleButton4 = 19;

		// Token: 0x040015BA RID: 5562
		public const int elementId_consoleButton5 = 20;

		// Token: 0x040015BB RID: 5563
		public const int elementId_consoleButton6 = 21;

		// Token: 0x040015BC RID: 5564
		public const int elementId_consoleButton7 = 22;

		// Token: 0x040015BD RID: 5565
		public const int elementId_consoleButton8 = 23;

		// Token: 0x040015BE RID: 5566
		public const int elementId_consoleButton9 = 24;

		// Token: 0x040015BF RID: 5567
		public const int elementId_consoleButton10 = 25;

		// Token: 0x040015C0 RID: 5568
		public const int elementId_shifter1 = 26;

		// Token: 0x040015C1 RID: 5569
		public const int elementId_shifter2 = 27;

		// Token: 0x040015C2 RID: 5570
		public const int elementId_shifter3 = 28;

		// Token: 0x040015C3 RID: 5571
		public const int elementId_shifter4 = 29;

		// Token: 0x040015C4 RID: 5572
		public const int elementId_shifter5 = 30;

		// Token: 0x040015C5 RID: 5573
		public const int elementId_shifter6 = 31;

		// Token: 0x040015C6 RID: 5574
		public const int elementId_shifter7 = 32;

		// Token: 0x040015C7 RID: 5575
		public const int elementId_shifter8 = 33;

		// Token: 0x040015C8 RID: 5576
		public const int elementId_shifter9 = 34;

		// Token: 0x040015C9 RID: 5577
		public const int elementId_shifter10 = 35;

		// Token: 0x040015CA RID: 5578
		public const int elementId_reverseGear = 44;

		// Token: 0x040015CB RID: 5579
		public const int elementId_select = 36;

		// Token: 0x040015CC RID: 5580
		public const int elementId_start = 37;

		// Token: 0x040015CD RID: 5581
		public const int elementId_systemButton = 38;

		// Token: 0x040015CE RID: 5582
		public const int elementId_horn = 43;

		// Token: 0x040015CF RID: 5583
		public const int elementId_dPadUp = 39;

		// Token: 0x040015D0 RID: 5584
		public const int elementId_dPadRight = 40;

		// Token: 0x040015D1 RID: 5585
		public const int elementId_dPadDown = 41;

		// Token: 0x040015D2 RID: 5586
		public const int elementId_dPadLeft = 42;

		// Token: 0x040015D3 RID: 5587
		public const int elementId_dPad = 45;
	}
}
