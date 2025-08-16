using System;

namespace Rewired
{
	public sealed class RacingWheelTemplate : ControllerTemplate, IRacingWheelTemplate, IControllerTemplate
	{
		// (get) Token: 0x060014E2 RID: 5346 RVA: 0x00010C9D File Offset: 0x0000EE9D
		IControllerTemplateAxis IRacingWheelTemplate.wheel
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(0);
			}
		}

		// (get) Token: 0x060014E3 RID: 5347 RVA: 0x00010CA6 File Offset: 0x0000EEA6
		IControllerTemplateAxis IRacingWheelTemplate.accelerator
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(1);
			}
		}

		// (get) Token: 0x060014E4 RID: 5348 RVA: 0x00010CAF File Offset: 0x0000EEAF
		IControllerTemplateAxis IRacingWheelTemplate.brake
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(2);
			}
		}

		// (get) Token: 0x060014E5 RID: 5349 RVA: 0x00010CB8 File Offset: 0x0000EEB8
		IControllerTemplateAxis IRacingWheelTemplate.clutch
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(3);
			}
		}

		// (get) Token: 0x060014E6 RID: 5350 RVA: 0x00010BE8 File Offset: 0x0000EDE8
		IControllerTemplateButton IRacingWheelTemplate.shiftDown
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(4);
			}
		}

		// (get) Token: 0x060014E7 RID: 5351 RVA: 0x00010BF1 File Offset: 0x0000EDF1
		IControllerTemplateButton IRacingWheelTemplate.shiftUp
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(5);
			}
		}

		// (get) Token: 0x060014E8 RID: 5352 RVA: 0x00010BFA File Offset: 0x0000EDFA
		IControllerTemplateButton IRacingWheelTemplate.wheelButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(6);
			}
		}

		// (get) Token: 0x060014E9 RID: 5353 RVA: 0x00010C03 File Offset: 0x0000EE03
		IControllerTemplateButton IRacingWheelTemplate.wheelButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(7);
			}
		}

		// (get) Token: 0x060014EA RID: 5354 RVA: 0x00010C0C File Offset: 0x0000EE0C
		IControllerTemplateButton IRacingWheelTemplate.wheelButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(8);
			}
		}

		// (get) Token: 0x060014EB RID: 5355 RVA: 0x00010C15 File Offset: 0x0000EE15
		IControllerTemplateButton IRacingWheelTemplate.wheelButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(9);
			}
		}

		// (get) Token: 0x060014EC RID: 5356 RVA: 0x00010C1F File Offset: 0x0000EE1F
		IControllerTemplateButton IRacingWheelTemplate.wheelButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(10);
			}
		}

		// (get) Token: 0x060014ED RID: 5357 RVA: 0x00010CC1 File Offset: 0x0000EEC1
		IControllerTemplateButton IRacingWheelTemplate.wheelButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(11);
			}
		}

		// (get) Token: 0x060014EE RID: 5358 RVA: 0x00010C33 File Offset: 0x0000EE33
		IControllerTemplateButton IRacingWheelTemplate.wheelButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(12);
			}
		}

		// (get) Token: 0x060014EF RID: 5359 RVA: 0x00010CCB File Offset: 0x0000EECB
		IControllerTemplateButton IRacingWheelTemplate.wheelButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(13);
			}
		}

		// (get) Token: 0x060014F0 RID: 5360 RVA: 0x00010C47 File Offset: 0x0000EE47
		IControllerTemplateButton IRacingWheelTemplate.wheelButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(14);
			}
		}

		// (get) Token: 0x060014F1 RID: 5361 RVA: 0x00010C51 File Offset: 0x0000EE51
		IControllerTemplateButton IRacingWheelTemplate.wheelButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(15);
			}
		}

		// (get) Token: 0x060014F2 RID: 5362 RVA: 0x00010C5B File Offset: 0x0000EE5B
		IControllerTemplateButton IRacingWheelTemplate.consoleButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(16);
			}
		}

		// (get) Token: 0x060014F3 RID: 5363 RVA: 0x00010CD5 File Offset: 0x0000EED5
		IControllerTemplateButton IRacingWheelTemplate.consoleButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(17);
			}
		}

		// (get) Token: 0x060014F4 RID: 5364 RVA: 0x00010CDF File Offset: 0x0000EEDF
		IControllerTemplateButton IRacingWheelTemplate.consoleButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(18);
			}
		}

		// (get) Token: 0x060014F5 RID: 5365 RVA: 0x00010CE9 File Offset: 0x0000EEE9
		IControllerTemplateButton IRacingWheelTemplate.consoleButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(19);
			}
		}

		// (get) Token: 0x060014F6 RID: 5366 RVA: 0x00010CF3 File Offset: 0x0000EEF3
		IControllerTemplateButton IRacingWheelTemplate.consoleButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(20);
			}
		}

		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x00010CFD File Offset: 0x0000EEFD
		IControllerTemplateButton IRacingWheelTemplate.consoleButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(21);
			}
		}

		// (get) Token: 0x060014F8 RID: 5368 RVA: 0x00010D07 File Offset: 0x0000EF07
		IControllerTemplateButton IRacingWheelTemplate.consoleButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(22);
			}
		}

		// (get) Token: 0x060014F9 RID: 5369 RVA: 0x00010D11 File Offset: 0x0000EF11
		IControllerTemplateButton IRacingWheelTemplate.consoleButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(23);
			}
		}

		// (get) Token: 0x060014FA RID: 5370 RVA: 0x00010D1B File Offset: 0x0000EF1B
		IControllerTemplateButton IRacingWheelTemplate.consoleButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(24);
			}
		}

		// (get) Token: 0x060014FB RID: 5371 RVA: 0x00010D25 File Offset: 0x0000EF25
		IControllerTemplateButton IRacingWheelTemplate.consoleButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(25);
			}
		}

		// (get) Token: 0x060014FC RID: 5372 RVA: 0x00010D2F File Offset: 0x0000EF2F
		IControllerTemplateButton IRacingWheelTemplate.shifter1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(26);
			}
		}

		// (get) Token: 0x060014FD RID: 5373 RVA: 0x00010D39 File Offset: 0x0000EF39
		IControllerTemplateButton IRacingWheelTemplate.shifter2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(27);
			}
		}

		// (get) Token: 0x060014FE RID: 5374 RVA: 0x00010D43 File Offset: 0x0000EF43
		IControllerTemplateButton IRacingWheelTemplate.shifter3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(28);
			}
		}

		// (get) Token: 0x060014FF RID: 5375 RVA: 0x00010D4D File Offset: 0x0000EF4D
		IControllerTemplateButton IRacingWheelTemplate.shifter4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(29);
			}
		}

		// (get) Token: 0x06001500 RID: 5376 RVA: 0x00010D57 File Offset: 0x0000EF57
		IControllerTemplateButton IRacingWheelTemplate.shifter5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(30);
			}
		}

		// (get) Token: 0x06001501 RID: 5377 RVA: 0x00010D61 File Offset: 0x0000EF61
		IControllerTemplateButton IRacingWheelTemplate.shifter6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(31);
			}
		}

		// (get) Token: 0x06001502 RID: 5378 RVA: 0x00010D6B File Offset: 0x0000EF6B
		IControllerTemplateButton IRacingWheelTemplate.shifter7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(32);
			}
		}

		// (get) Token: 0x06001503 RID: 5379 RVA: 0x00010D75 File Offset: 0x0000EF75
		IControllerTemplateButton IRacingWheelTemplate.shifter8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(33);
			}
		}

		// (get) Token: 0x06001504 RID: 5380 RVA: 0x00010D7F File Offset: 0x0000EF7F
		IControllerTemplateButton IRacingWheelTemplate.shifter9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(34);
			}
		}

		// (get) Token: 0x06001505 RID: 5381 RVA: 0x00010D89 File Offset: 0x0000EF89
		IControllerTemplateButton IRacingWheelTemplate.shifter10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(35);
			}
		}

		// (get) Token: 0x06001506 RID: 5382 RVA: 0x00010D93 File Offset: 0x0000EF93
		IControllerTemplateButton IRacingWheelTemplate.reverseGear
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(44);
			}
		}

		// (get) Token: 0x06001507 RID: 5383 RVA: 0x00010D9D File Offset: 0x0000EF9D
		IControllerTemplateButton IRacingWheelTemplate.select
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(36);
			}
		}

		// (get) Token: 0x06001508 RID: 5384 RVA: 0x00010DA7 File Offset: 0x0000EFA7
		IControllerTemplateButton IRacingWheelTemplate.start
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(37);
			}
		}

		// (get) Token: 0x06001509 RID: 5385 RVA: 0x00010DB1 File Offset: 0x0000EFB1
		IControllerTemplateButton IRacingWheelTemplate.systemButton
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(38);
			}
		}

		// (get) Token: 0x0600150A RID: 5386 RVA: 0x00010DBB File Offset: 0x0000EFBB
		IControllerTemplateButton IRacingWheelTemplate.horn
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(43);
			}
		}

		// (get) Token: 0x0600150B RID: 5387 RVA: 0x00010DC5 File Offset: 0x0000EFC5
		IControllerTemplateDPad IRacingWheelTemplate.dPad
		{
			get
			{
				return base.GetElement<IControllerTemplateDPad>(45);
			}
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x00010C83 File Offset: 0x0000EE83
		public RacingWheelTemplate(object payload)
			: base(payload)
		{
		}

		public static readonly Guid typeGuid = new Guid("104e31d8-9115-4dd5-a398-2e54d35e6c83");

		public const int elementId_wheel = 0;

		public const int elementId_accelerator = 1;

		public const int elementId_brake = 2;

		public const int elementId_clutch = 3;

		public const int elementId_shiftDown = 4;

		public const int elementId_shiftUp = 5;

		public const int elementId_wheelButton1 = 6;

		public const int elementId_wheelButton2 = 7;

		public const int elementId_wheelButton3 = 8;

		public const int elementId_wheelButton4 = 9;

		public const int elementId_wheelButton5 = 10;

		public const int elementId_wheelButton6 = 11;

		public const int elementId_wheelButton7 = 12;

		public const int elementId_wheelButton8 = 13;

		public const int elementId_wheelButton9 = 14;

		public const int elementId_wheelButton10 = 15;

		public const int elementId_consoleButton1 = 16;

		public const int elementId_consoleButton2 = 17;

		public const int elementId_consoleButton3 = 18;

		public const int elementId_consoleButton4 = 19;

		public const int elementId_consoleButton5 = 20;

		public const int elementId_consoleButton6 = 21;

		public const int elementId_consoleButton7 = 22;

		public const int elementId_consoleButton8 = 23;

		public const int elementId_consoleButton9 = 24;

		public const int elementId_consoleButton10 = 25;

		public const int elementId_shifter1 = 26;

		public const int elementId_shifter2 = 27;

		public const int elementId_shifter3 = 28;

		public const int elementId_shifter4 = 29;

		public const int elementId_shifter5 = 30;

		public const int elementId_shifter6 = 31;

		public const int elementId_shifter7 = 32;

		public const int elementId_shifter8 = 33;

		public const int elementId_shifter9 = 34;

		public const int elementId_shifter10 = 35;

		public const int elementId_reverseGear = 44;

		public const int elementId_select = 36;

		public const int elementId_start = 37;

		public const int elementId_systemButton = 38;

		public const int elementId_horn = 43;

		public const int elementId_dPadUp = 39;

		public const int elementId_dPadRight = 40;

		public const int elementId_dPadDown = 41;

		public const int elementId_dPadLeft = 42;

		public const int elementId_dPad = 45;
	}
}
