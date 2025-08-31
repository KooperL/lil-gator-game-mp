using System;

namespace Rewired
{
	public sealed class RacingWheelTemplate : ControllerTemplate, IRacingWheelTemplate, IControllerTemplate
	{
		// (get) Token: 0x06001153 RID: 4435 RVA: 0x0004DF1D File Offset: 0x0004C11D
		IControllerTemplateAxis IRacingWheelTemplate.wheel
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(0);
			}
		}

		// (get) Token: 0x06001154 RID: 4436 RVA: 0x0004DF26 File Offset: 0x0004C126
		IControllerTemplateAxis IRacingWheelTemplate.accelerator
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(1);
			}
		}

		// (get) Token: 0x06001155 RID: 4437 RVA: 0x0004DF2F File Offset: 0x0004C12F
		IControllerTemplateAxis IRacingWheelTemplate.brake
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(2);
			}
		}

		// (get) Token: 0x06001156 RID: 4438 RVA: 0x0004DF38 File Offset: 0x0004C138
		IControllerTemplateAxis IRacingWheelTemplate.clutch
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(3);
			}
		}

		// (get) Token: 0x06001157 RID: 4439 RVA: 0x0004DF41 File Offset: 0x0004C141
		IControllerTemplateButton IRacingWheelTemplate.shiftDown
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(4);
			}
		}

		// (get) Token: 0x06001158 RID: 4440 RVA: 0x0004DF4A File Offset: 0x0004C14A
		IControllerTemplateButton IRacingWheelTemplate.shiftUp
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(5);
			}
		}

		// (get) Token: 0x06001159 RID: 4441 RVA: 0x0004DF53 File Offset: 0x0004C153
		IControllerTemplateButton IRacingWheelTemplate.wheelButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(6);
			}
		}

		// (get) Token: 0x0600115A RID: 4442 RVA: 0x0004DF5C File Offset: 0x0004C15C
		IControllerTemplateButton IRacingWheelTemplate.wheelButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(7);
			}
		}

		// (get) Token: 0x0600115B RID: 4443 RVA: 0x0004DF65 File Offset: 0x0004C165
		IControllerTemplateButton IRacingWheelTemplate.wheelButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(8);
			}
		}

		// (get) Token: 0x0600115C RID: 4444 RVA: 0x0004DF6E File Offset: 0x0004C16E
		IControllerTemplateButton IRacingWheelTemplate.wheelButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(9);
			}
		}

		// (get) Token: 0x0600115D RID: 4445 RVA: 0x0004DF78 File Offset: 0x0004C178
		IControllerTemplateButton IRacingWheelTemplate.wheelButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(10);
			}
		}

		// (get) Token: 0x0600115E RID: 4446 RVA: 0x0004DF82 File Offset: 0x0004C182
		IControllerTemplateButton IRacingWheelTemplate.wheelButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(11);
			}
		}

		// (get) Token: 0x0600115F RID: 4447 RVA: 0x0004DF8C File Offset: 0x0004C18C
		IControllerTemplateButton IRacingWheelTemplate.wheelButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(12);
			}
		}

		// (get) Token: 0x06001160 RID: 4448 RVA: 0x0004DF96 File Offset: 0x0004C196
		IControllerTemplateButton IRacingWheelTemplate.wheelButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(13);
			}
		}

		// (get) Token: 0x06001161 RID: 4449 RVA: 0x0004DFA0 File Offset: 0x0004C1A0
		IControllerTemplateButton IRacingWheelTemplate.wheelButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(14);
			}
		}

		// (get) Token: 0x06001162 RID: 4450 RVA: 0x0004DFAA File Offset: 0x0004C1AA
		IControllerTemplateButton IRacingWheelTemplate.wheelButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(15);
			}
		}

		// (get) Token: 0x06001163 RID: 4451 RVA: 0x0004DFB4 File Offset: 0x0004C1B4
		IControllerTemplateButton IRacingWheelTemplate.consoleButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(16);
			}
		}

		// (get) Token: 0x06001164 RID: 4452 RVA: 0x0004DFBE File Offset: 0x0004C1BE
		IControllerTemplateButton IRacingWheelTemplate.consoleButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(17);
			}
		}

		// (get) Token: 0x06001165 RID: 4453 RVA: 0x0004DFC8 File Offset: 0x0004C1C8
		IControllerTemplateButton IRacingWheelTemplate.consoleButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(18);
			}
		}

		// (get) Token: 0x06001166 RID: 4454 RVA: 0x0004DFD2 File Offset: 0x0004C1D2
		IControllerTemplateButton IRacingWheelTemplate.consoleButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(19);
			}
		}

		// (get) Token: 0x06001167 RID: 4455 RVA: 0x0004DFDC File Offset: 0x0004C1DC
		IControllerTemplateButton IRacingWheelTemplate.consoleButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(20);
			}
		}

		// (get) Token: 0x06001168 RID: 4456 RVA: 0x0004DFE6 File Offset: 0x0004C1E6
		IControllerTemplateButton IRacingWheelTemplate.consoleButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(21);
			}
		}

		// (get) Token: 0x06001169 RID: 4457 RVA: 0x0004DFF0 File Offset: 0x0004C1F0
		IControllerTemplateButton IRacingWheelTemplate.consoleButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(22);
			}
		}

		// (get) Token: 0x0600116A RID: 4458 RVA: 0x0004DFFA File Offset: 0x0004C1FA
		IControllerTemplateButton IRacingWheelTemplate.consoleButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(23);
			}
		}

		// (get) Token: 0x0600116B RID: 4459 RVA: 0x0004E004 File Offset: 0x0004C204
		IControllerTemplateButton IRacingWheelTemplate.consoleButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(24);
			}
		}

		// (get) Token: 0x0600116C RID: 4460 RVA: 0x0004E00E File Offset: 0x0004C20E
		IControllerTemplateButton IRacingWheelTemplate.consoleButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(25);
			}
		}

		// (get) Token: 0x0600116D RID: 4461 RVA: 0x0004E018 File Offset: 0x0004C218
		IControllerTemplateButton IRacingWheelTemplate.shifter1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(26);
			}
		}

		// (get) Token: 0x0600116E RID: 4462 RVA: 0x0004E022 File Offset: 0x0004C222
		IControllerTemplateButton IRacingWheelTemplate.shifter2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(27);
			}
		}

		// (get) Token: 0x0600116F RID: 4463 RVA: 0x0004E02C File Offset: 0x0004C22C
		IControllerTemplateButton IRacingWheelTemplate.shifter3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(28);
			}
		}

		// (get) Token: 0x06001170 RID: 4464 RVA: 0x0004E036 File Offset: 0x0004C236
		IControllerTemplateButton IRacingWheelTemplate.shifter4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(29);
			}
		}

		// (get) Token: 0x06001171 RID: 4465 RVA: 0x0004E040 File Offset: 0x0004C240
		IControllerTemplateButton IRacingWheelTemplate.shifter5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(30);
			}
		}

		// (get) Token: 0x06001172 RID: 4466 RVA: 0x0004E04A File Offset: 0x0004C24A
		IControllerTemplateButton IRacingWheelTemplate.shifter6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(31);
			}
		}

		// (get) Token: 0x06001173 RID: 4467 RVA: 0x0004E054 File Offset: 0x0004C254
		IControllerTemplateButton IRacingWheelTemplate.shifter7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(32);
			}
		}

		// (get) Token: 0x06001174 RID: 4468 RVA: 0x0004E05E File Offset: 0x0004C25E
		IControllerTemplateButton IRacingWheelTemplate.shifter8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(33);
			}
		}

		// (get) Token: 0x06001175 RID: 4469 RVA: 0x0004E068 File Offset: 0x0004C268
		IControllerTemplateButton IRacingWheelTemplate.shifter9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(34);
			}
		}

		// (get) Token: 0x06001176 RID: 4470 RVA: 0x0004E072 File Offset: 0x0004C272
		IControllerTemplateButton IRacingWheelTemplate.shifter10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(35);
			}
		}

		// (get) Token: 0x06001177 RID: 4471 RVA: 0x0004E07C File Offset: 0x0004C27C
		IControllerTemplateButton IRacingWheelTemplate.reverseGear
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(44);
			}
		}

		// (get) Token: 0x06001178 RID: 4472 RVA: 0x0004E086 File Offset: 0x0004C286
		IControllerTemplateButton IRacingWheelTemplate.select
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(36);
			}
		}

		// (get) Token: 0x06001179 RID: 4473 RVA: 0x0004E090 File Offset: 0x0004C290
		IControllerTemplateButton IRacingWheelTemplate.start
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(37);
			}
		}

		// (get) Token: 0x0600117A RID: 4474 RVA: 0x0004E09A File Offset: 0x0004C29A
		IControllerTemplateButton IRacingWheelTemplate.systemButton
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(38);
			}
		}

		// (get) Token: 0x0600117B RID: 4475 RVA: 0x0004E0A4 File Offset: 0x0004C2A4
		IControllerTemplateButton IRacingWheelTemplate.horn
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(43);
			}
		}

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
