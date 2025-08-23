using System;

namespace Rewired
{
	public sealed class RacingWheelTemplate : ControllerTemplate, IRacingWheelTemplate, IControllerTemplate
	{
		// (get) Token: 0x060014E3 RID: 5347 RVA: 0x00010CBC File Offset: 0x0000EEBC
		IControllerTemplateAxis IRacingWheelTemplate.wheel
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(0);
			}
		}

		// (get) Token: 0x060014E4 RID: 5348 RVA: 0x00010CC5 File Offset: 0x0000EEC5
		IControllerTemplateAxis IRacingWheelTemplate.accelerator
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(1);
			}
		}

		// (get) Token: 0x060014E5 RID: 5349 RVA: 0x00010CCE File Offset: 0x0000EECE
		IControllerTemplateAxis IRacingWheelTemplate.brake
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(2);
			}
		}

		// (get) Token: 0x060014E6 RID: 5350 RVA: 0x00010CD7 File Offset: 0x0000EED7
		IControllerTemplateAxis IRacingWheelTemplate.clutch
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(3);
			}
		}

		// (get) Token: 0x060014E7 RID: 5351 RVA: 0x00010C07 File Offset: 0x0000EE07
		IControllerTemplateButton IRacingWheelTemplate.shiftDown
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(4);
			}
		}

		// (get) Token: 0x060014E8 RID: 5352 RVA: 0x00010C10 File Offset: 0x0000EE10
		IControllerTemplateButton IRacingWheelTemplate.shiftUp
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(5);
			}
		}

		// (get) Token: 0x060014E9 RID: 5353 RVA: 0x00010C19 File Offset: 0x0000EE19
		IControllerTemplateButton IRacingWheelTemplate.wheelButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(6);
			}
		}

		// (get) Token: 0x060014EA RID: 5354 RVA: 0x00010C22 File Offset: 0x0000EE22
		IControllerTemplateButton IRacingWheelTemplate.wheelButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(7);
			}
		}

		// (get) Token: 0x060014EB RID: 5355 RVA: 0x00010C2B File Offset: 0x0000EE2B
		IControllerTemplateButton IRacingWheelTemplate.wheelButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(8);
			}
		}

		// (get) Token: 0x060014EC RID: 5356 RVA: 0x00010C34 File Offset: 0x0000EE34
		IControllerTemplateButton IRacingWheelTemplate.wheelButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(9);
			}
		}

		// (get) Token: 0x060014ED RID: 5357 RVA: 0x00010C3E File Offset: 0x0000EE3E
		IControllerTemplateButton IRacingWheelTemplate.wheelButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(10);
			}
		}

		// (get) Token: 0x060014EE RID: 5358 RVA: 0x00010CE0 File Offset: 0x0000EEE0
		IControllerTemplateButton IRacingWheelTemplate.wheelButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(11);
			}
		}

		// (get) Token: 0x060014EF RID: 5359 RVA: 0x00010C52 File Offset: 0x0000EE52
		IControllerTemplateButton IRacingWheelTemplate.wheelButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(12);
			}
		}

		// (get) Token: 0x060014F0 RID: 5360 RVA: 0x00010CEA File Offset: 0x0000EEEA
		IControllerTemplateButton IRacingWheelTemplate.wheelButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(13);
			}
		}

		// (get) Token: 0x060014F1 RID: 5361 RVA: 0x00010C66 File Offset: 0x0000EE66
		IControllerTemplateButton IRacingWheelTemplate.wheelButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(14);
			}
		}

		// (get) Token: 0x060014F2 RID: 5362 RVA: 0x00010C70 File Offset: 0x0000EE70
		IControllerTemplateButton IRacingWheelTemplate.wheelButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(15);
			}
		}

		// (get) Token: 0x060014F3 RID: 5363 RVA: 0x00010C7A File Offset: 0x0000EE7A
		IControllerTemplateButton IRacingWheelTemplate.consoleButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(16);
			}
		}

		// (get) Token: 0x060014F4 RID: 5364 RVA: 0x00010CF4 File Offset: 0x0000EEF4
		IControllerTemplateButton IRacingWheelTemplate.consoleButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(17);
			}
		}

		// (get) Token: 0x060014F5 RID: 5365 RVA: 0x00010CFE File Offset: 0x0000EEFE
		IControllerTemplateButton IRacingWheelTemplate.consoleButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(18);
			}
		}

		// (get) Token: 0x060014F6 RID: 5366 RVA: 0x00010D08 File Offset: 0x0000EF08
		IControllerTemplateButton IRacingWheelTemplate.consoleButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(19);
			}
		}

		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x00010D12 File Offset: 0x0000EF12
		IControllerTemplateButton IRacingWheelTemplate.consoleButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(20);
			}
		}

		// (get) Token: 0x060014F8 RID: 5368 RVA: 0x00010D1C File Offset: 0x0000EF1C
		IControllerTemplateButton IRacingWheelTemplate.consoleButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(21);
			}
		}

		// (get) Token: 0x060014F9 RID: 5369 RVA: 0x00010D26 File Offset: 0x0000EF26
		IControllerTemplateButton IRacingWheelTemplate.consoleButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(22);
			}
		}

		// (get) Token: 0x060014FA RID: 5370 RVA: 0x00010D30 File Offset: 0x0000EF30
		IControllerTemplateButton IRacingWheelTemplate.consoleButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(23);
			}
		}

		// (get) Token: 0x060014FB RID: 5371 RVA: 0x00010D3A File Offset: 0x0000EF3A
		IControllerTemplateButton IRacingWheelTemplate.consoleButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(24);
			}
		}

		// (get) Token: 0x060014FC RID: 5372 RVA: 0x00010D44 File Offset: 0x0000EF44
		IControllerTemplateButton IRacingWheelTemplate.consoleButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(25);
			}
		}

		// (get) Token: 0x060014FD RID: 5373 RVA: 0x00010D4E File Offset: 0x0000EF4E
		IControllerTemplateButton IRacingWheelTemplate.shifter1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(26);
			}
		}

		// (get) Token: 0x060014FE RID: 5374 RVA: 0x00010D58 File Offset: 0x0000EF58
		IControllerTemplateButton IRacingWheelTemplate.shifter2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(27);
			}
		}

		// (get) Token: 0x060014FF RID: 5375 RVA: 0x00010D62 File Offset: 0x0000EF62
		IControllerTemplateButton IRacingWheelTemplate.shifter3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(28);
			}
		}

		// (get) Token: 0x06001500 RID: 5376 RVA: 0x00010D6C File Offset: 0x0000EF6C
		IControllerTemplateButton IRacingWheelTemplate.shifter4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(29);
			}
		}

		// (get) Token: 0x06001501 RID: 5377 RVA: 0x00010D76 File Offset: 0x0000EF76
		IControllerTemplateButton IRacingWheelTemplate.shifter5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(30);
			}
		}

		// (get) Token: 0x06001502 RID: 5378 RVA: 0x00010D80 File Offset: 0x0000EF80
		IControllerTemplateButton IRacingWheelTemplate.shifter6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(31);
			}
		}

		// (get) Token: 0x06001503 RID: 5379 RVA: 0x00010D8A File Offset: 0x0000EF8A
		IControllerTemplateButton IRacingWheelTemplate.shifter7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(32);
			}
		}

		// (get) Token: 0x06001504 RID: 5380 RVA: 0x00010D94 File Offset: 0x0000EF94
		IControllerTemplateButton IRacingWheelTemplate.shifter8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(33);
			}
		}

		// (get) Token: 0x06001505 RID: 5381 RVA: 0x00010D9E File Offset: 0x0000EF9E
		IControllerTemplateButton IRacingWheelTemplate.shifter9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(34);
			}
		}

		// (get) Token: 0x06001506 RID: 5382 RVA: 0x00010DA8 File Offset: 0x0000EFA8
		IControllerTemplateButton IRacingWheelTemplate.shifter10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(35);
			}
		}

		// (get) Token: 0x06001507 RID: 5383 RVA: 0x00010DB2 File Offset: 0x0000EFB2
		IControllerTemplateButton IRacingWheelTemplate.reverseGear
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(44);
			}
		}

		// (get) Token: 0x06001508 RID: 5384 RVA: 0x00010DBC File Offset: 0x0000EFBC
		IControllerTemplateButton IRacingWheelTemplate.select
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(36);
			}
		}

		// (get) Token: 0x06001509 RID: 5385 RVA: 0x00010DC6 File Offset: 0x0000EFC6
		IControllerTemplateButton IRacingWheelTemplate.start
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(37);
			}
		}

		// (get) Token: 0x0600150A RID: 5386 RVA: 0x00010DD0 File Offset: 0x0000EFD0
		IControllerTemplateButton IRacingWheelTemplate.systemButton
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(38);
			}
		}

		// (get) Token: 0x0600150B RID: 5387 RVA: 0x00010DDA File Offset: 0x0000EFDA
		IControllerTemplateButton IRacingWheelTemplate.horn
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(43);
			}
		}

		// (get) Token: 0x0600150C RID: 5388 RVA: 0x00010DE4 File Offset: 0x0000EFE4
		IControllerTemplateDPad IRacingWheelTemplate.dPad
		{
			get
			{
				return base.GetElement<IControllerTemplateDPad>(45);
			}
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x00010CA2 File Offset: 0x0000EEA2
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
