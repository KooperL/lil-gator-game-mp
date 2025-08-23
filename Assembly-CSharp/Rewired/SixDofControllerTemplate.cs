using System;

namespace Rewired
{
	public sealed class SixDofControllerTemplate : ControllerTemplate, ISixDofControllerTemplate, IControllerTemplate
	{
		// (get) Token: 0x060015A1 RID: 5537 RVA: 0x00011198 File Offset: 0x0000F398
		IControllerTemplateAxis ISixDofControllerTemplate.extraAxis1
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(8);
			}
		}

		// (get) Token: 0x060015A2 RID: 5538 RVA: 0x000111A1 File Offset: 0x0000F3A1
		IControllerTemplateAxis ISixDofControllerTemplate.extraAxis2
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(9);
			}
		}

		// (get) Token: 0x060015A3 RID: 5539 RVA: 0x000111AB File Offset: 0x0000F3AB
		IControllerTemplateAxis ISixDofControllerTemplate.extraAxis3
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(10);
			}
		}

		// (get) Token: 0x060015A4 RID: 5540 RVA: 0x00010C48 File Offset: 0x0000EE48
		IControllerTemplateAxis ISixDofControllerTemplate.extraAxis4
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(11);
			}
		}

		// (get) Token: 0x060015A5 RID: 5541 RVA: 0x00010C52 File Offset: 0x0000EE52
		IControllerTemplateButton ISixDofControllerTemplate.button1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(12);
			}
		}

		// (get) Token: 0x060015A6 RID: 5542 RVA: 0x00010CEA File Offset: 0x0000EEEA
		IControllerTemplateButton ISixDofControllerTemplate.button2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(13);
			}
		}

		// (get) Token: 0x060015A7 RID: 5543 RVA: 0x00010C66 File Offset: 0x0000EE66
		IControllerTemplateButton ISixDofControllerTemplate.button3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(14);
			}
		}

		// (get) Token: 0x060015A8 RID: 5544 RVA: 0x00010C70 File Offset: 0x0000EE70
		IControllerTemplateButton ISixDofControllerTemplate.button4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(15);
			}
		}

		// (get) Token: 0x060015A9 RID: 5545 RVA: 0x00010C7A File Offset: 0x0000EE7A
		IControllerTemplateButton ISixDofControllerTemplate.button5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(16);
			}
		}

		// (get) Token: 0x060015AA RID: 5546 RVA: 0x00010CF4 File Offset: 0x0000EEF4
		IControllerTemplateButton ISixDofControllerTemplate.button6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(17);
			}
		}

		// (get) Token: 0x060015AB RID: 5547 RVA: 0x00010CFE File Offset: 0x0000EEFE
		IControllerTemplateButton ISixDofControllerTemplate.button7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(18);
			}
		}

		// (get) Token: 0x060015AC RID: 5548 RVA: 0x00010D08 File Offset: 0x0000EF08
		IControllerTemplateButton ISixDofControllerTemplate.button8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(19);
			}
		}

		// (get) Token: 0x060015AD RID: 5549 RVA: 0x00010D12 File Offset: 0x0000EF12
		IControllerTemplateButton ISixDofControllerTemplate.button9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(20);
			}
		}

		// (get) Token: 0x060015AE RID: 5550 RVA: 0x00010D1C File Offset: 0x0000EF1C
		IControllerTemplateButton ISixDofControllerTemplate.button10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(21);
			}
		}

		// (get) Token: 0x060015AF RID: 5551 RVA: 0x00010D26 File Offset: 0x0000EF26
		IControllerTemplateButton ISixDofControllerTemplate.button11
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(22);
			}
		}

		// (get) Token: 0x060015B0 RID: 5552 RVA: 0x00010D30 File Offset: 0x0000EF30
		IControllerTemplateButton ISixDofControllerTemplate.button12
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(23);
			}
		}

		// (get) Token: 0x060015B1 RID: 5553 RVA: 0x00010D3A File Offset: 0x0000EF3A
		IControllerTemplateButton ISixDofControllerTemplate.button13
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(24);
			}
		}

		// (get) Token: 0x060015B2 RID: 5554 RVA: 0x00010D44 File Offset: 0x0000EF44
		IControllerTemplateButton ISixDofControllerTemplate.button14
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(25);
			}
		}

		// (get) Token: 0x060015B3 RID: 5555 RVA: 0x00010D4E File Offset: 0x0000EF4E
		IControllerTemplateButton ISixDofControllerTemplate.button15
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(26);
			}
		}

		// (get) Token: 0x060015B4 RID: 5556 RVA: 0x00010D58 File Offset: 0x0000EF58
		IControllerTemplateButton ISixDofControllerTemplate.button16
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(27);
			}
		}

		// (get) Token: 0x060015B5 RID: 5557 RVA: 0x00010D62 File Offset: 0x0000EF62
		IControllerTemplateButton ISixDofControllerTemplate.button17
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(28);
			}
		}

		// (get) Token: 0x060015B6 RID: 5558 RVA: 0x00010D6C File Offset: 0x0000EF6C
		IControllerTemplateButton ISixDofControllerTemplate.button18
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(29);
			}
		}

		// (get) Token: 0x060015B7 RID: 5559 RVA: 0x00010D76 File Offset: 0x0000EF76
		IControllerTemplateButton ISixDofControllerTemplate.button19
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(30);
			}
		}

		// (get) Token: 0x060015B8 RID: 5560 RVA: 0x00010D80 File Offset: 0x0000EF80
		IControllerTemplateButton ISixDofControllerTemplate.button20
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(31);
			}
		}

		// (get) Token: 0x060015B9 RID: 5561 RVA: 0x00010E75 File Offset: 0x0000F075
		IControllerTemplateButton ISixDofControllerTemplate.button21
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(55);
			}
		}

		// (get) Token: 0x060015BA RID: 5562 RVA: 0x00010E7F File Offset: 0x0000F07F
		IControllerTemplateButton ISixDofControllerTemplate.button22
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(56);
			}
		}

		// (get) Token: 0x060015BB RID: 5563 RVA: 0x00010E89 File Offset: 0x0000F089
		IControllerTemplateButton ISixDofControllerTemplate.button23
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(57);
			}
		}

		// (get) Token: 0x060015BC RID: 5564 RVA: 0x00010E93 File Offset: 0x0000F093
		IControllerTemplateButton ISixDofControllerTemplate.button24
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(58);
			}
		}

		// (get) Token: 0x060015BD RID: 5565 RVA: 0x00010E9D File Offset: 0x0000F09D
		IControllerTemplateButton ISixDofControllerTemplate.button25
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(59);
			}
		}

		// (get) Token: 0x060015BE RID: 5566 RVA: 0x00010EA7 File Offset: 0x0000F0A7
		IControllerTemplateButton ISixDofControllerTemplate.button26
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(60);
			}
		}

		// (get) Token: 0x060015BF RID: 5567 RVA: 0x00010EB1 File Offset: 0x0000F0B1
		IControllerTemplateButton ISixDofControllerTemplate.button27
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(61);
			}
		}

		// (get) Token: 0x060015C0 RID: 5568 RVA: 0x00010EBB File Offset: 0x0000F0BB
		IControllerTemplateButton ISixDofControllerTemplate.button28
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(62);
			}
		}

		// (get) Token: 0x060015C1 RID: 5569 RVA: 0x00010EC5 File Offset: 0x0000F0C5
		IControllerTemplateButton ISixDofControllerTemplate.button29
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(63);
			}
		}

		// (get) Token: 0x060015C2 RID: 5570 RVA: 0x00010ECF File Offset: 0x0000F0CF
		IControllerTemplateButton ISixDofControllerTemplate.button30
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(64);
			}
		}

		// (get) Token: 0x060015C3 RID: 5571 RVA: 0x00010ED9 File Offset: 0x0000F0D9
		IControllerTemplateButton ISixDofControllerTemplate.button31
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(65);
			}
		}

		// (get) Token: 0x060015C4 RID: 5572 RVA: 0x00010EE3 File Offset: 0x0000F0E3
		IControllerTemplateButton ISixDofControllerTemplate.button32
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(66);
			}
		}

		// (get) Token: 0x060015C5 RID: 5573 RVA: 0x000111B5 File Offset: 0x0000F3B5
		IControllerTemplateHat ISixDofControllerTemplate.hat1
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(48);
			}
		}

		// (get) Token: 0x060015C6 RID: 5574 RVA: 0x000111BF File Offset: 0x0000F3BF
		IControllerTemplateHat ISixDofControllerTemplate.hat2
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(49);
			}
		}

		// (get) Token: 0x060015C7 RID: 5575 RVA: 0x000111C9 File Offset: 0x0000F3C9
		IControllerTemplateThrottle ISixDofControllerTemplate.throttle1
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(52);
			}
		}

		// (get) Token: 0x060015C8 RID: 5576 RVA: 0x000111D3 File Offset: 0x0000F3D3
		IControllerTemplateThrottle ISixDofControllerTemplate.throttle2
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(53);
			}
		}

		// (get) Token: 0x060015C9 RID: 5577 RVA: 0x000111DD File Offset: 0x0000F3DD
		IControllerTemplateStick6D ISixDofControllerTemplate.stick
		{
			get
			{
				return base.GetElement<IControllerTemplateStick6D>(54);
			}
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x00010CA2 File Offset: 0x0000EEA2
		public SixDofControllerTemplate(object payload)
			: base(payload)
		{
		}

		public static readonly Guid typeGuid = new Guid("2599beb3-522b-43dd-a4ef-93fd60e5eafa");

		public const int elementId_positionX = 1;

		public const int elementId_positionY = 2;

		public const int elementId_positionZ = 0;

		public const int elementId_rotationX = 3;

		public const int elementId_rotationY = 5;

		public const int elementId_rotationZ = 4;

		public const int elementId_throttle1Axis = 6;

		public const int elementId_throttle1MinDetent = 50;

		public const int elementId_throttle2Axis = 7;

		public const int elementId_throttle2MinDetent = 51;

		public const int elementId_extraAxis1 = 8;

		public const int elementId_extraAxis2 = 9;

		public const int elementId_extraAxis3 = 10;

		public const int elementId_extraAxis4 = 11;

		public const int elementId_button1 = 12;

		public const int elementId_button2 = 13;

		public const int elementId_button3 = 14;

		public const int elementId_button4 = 15;

		public const int elementId_button5 = 16;

		public const int elementId_button6 = 17;

		public const int elementId_button7 = 18;

		public const int elementId_button8 = 19;

		public const int elementId_button9 = 20;

		public const int elementId_button10 = 21;

		public const int elementId_button11 = 22;

		public const int elementId_button12 = 23;

		public const int elementId_button13 = 24;

		public const int elementId_button14 = 25;

		public const int elementId_button15 = 26;

		public const int elementId_button16 = 27;

		public const int elementId_button17 = 28;

		public const int elementId_button18 = 29;

		public const int elementId_button19 = 30;

		public const int elementId_button20 = 31;

		public const int elementId_button21 = 55;

		public const int elementId_button22 = 56;

		public const int elementId_button23 = 57;

		public const int elementId_button24 = 58;

		public const int elementId_button25 = 59;

		public const int elementId_button26 = 60;

		public const int elementId_button27 = 61;

		public const int elementId_button28 = 62;

		public const int elementId_button29 = 63;

		public const int elementId_button30 = 64;

		public const int elementId_button31 = 65;

		public const int elementId_button32 = 66;

		public const int elementId_hat1Up = 32;

		public const int elementId_hat1UpRight = 33;

		public const int elementId_hat1Right = 34;

		public const int elementId_hat1DownRight = 35;

		public const int elementId_hat1Down = 36;

		public const int elementId_hat1DownLeft = 37;

		public const int elementId_hat1Left = 38;

		public const int elementId_hat1UpLeft = 39;

		public const int elementId_hat2Up = 40;

		public const int elementId_hat2UpRight = 41;

		public const int elementId_hat2Right = 42;

		public const int elementId_hat2DownRight = 43;

		public const int elementId_hat2Down = 44;

		public const int elementId_hat2DownLeft = 45;

		public const int elementId_hat2Left = 46;

		public const int elementId_hat2UpLeft = 47;

		public const int elementId_hat1 = 48;

		public const int elementId_hat2 = 49;

		public const int elementId_throttle1 = 52;

		public const int elementId_throttle2 = 53;

		public const int elementId_stick = 54;
	}
}
