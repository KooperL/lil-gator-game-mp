using System;

namespace Rewired
{
	public sealed class SixDofControllerTemplate : ControllerTemplate, ISixDofControllerTemplate, IControllerTemplate
	{
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x0004E6FC File Offset: 0x0004C8FC
		IControllerTemplateAxis ISixDofControllerTemplate.extraAxis1
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(8);
			}
		}

		// (get) Token: 0x06001212 RID: 4626 RVA: 0x0004E705 File Offset: 0x0004C905
		IControllerTemplateAxis ISixDofControllerTemplate.extraAxis2
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(9);
			}
		}

		// (get) Token: 0x06001213 RID: 4627 RVA: 0x0004E70F File Offset: 0x0004C90F
		IControllerTemplateAxis ISixDofControllerTemplate.extraAxis3
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(10);
			}
		}

		// (get) Token: 0x06001214 RID: 4628 RVA: 0x0004E719 File Offset: 0x0004C919
		IControllerTemplateAxis ISixDofControllerTemplate.extraAxis4
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(11);
			}
		}

		// (get) Token: 0x06001215 RID: 4629 RVA: 0x0004E723 File Offset: 0x0004C923
		IControllerTemplateButton ISixDofControllerTemplate.button1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(12);
			}
		}

		// (get) Token: 0x06001216 RID: 4630 RVA: 0x0004E72D File Offset: 0x0004C92D
		IControllerTemplateButton ISixDofControllerTemplate.button2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(13);
			}
		}

		// (get) Token: 0x06001217 RID: 4631 RVA: 0x0004E737 File Offset: 0x0004C937
		IControllerTemplateButton ISixDofControllerTemplate.button3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(14);
			}
		}

		// (get) Token: 0x06001218 RID: 4632 RVA: 0x0004E741 File Offset: 0x0004C941
		IControllerTemplateButton ISixDofControllerTemplate.button4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(15);
			}
		}

		// (get) Token: 0x06001219 RID: 4633 RVA: 0x0004E74B File Offset: 0x0004C94B
		IControllerTemplateButton ISixDofControllerTemplate.button5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(16);
			}
		}

		// (get) Token: 0x0600121A RID: 4634 RVA: 0x0004E755 File Offset: 0x0004C955
		IControllerTemplateButton ISixDofControllerTemplate.button6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(17);
			}
		}

		// (get) Token: 0x0600121B RID: 4635 RVA: 0x0004E75F File Offset: 0x0004C95F
		IControllerTemplateButton ISixDofControllerTemplate.button7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(18);
			}
		}

		// (get) Token: 0x0600121C RID: 4636 RVA: 0x0004E769 File Offset: 0x0004C969
		IControllerTemplateButton ISixDofControllerTemplate.button8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(19);
			}
		}

		// (get) Token: 0x0600121D RID: 4637 RVA: 0x0004E773 File Offset: 0x0004C973
		IControllerTemplateButton ISixDofControllerTemplate.button9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(20);
			}
		}

		// (get) Token: 0x0600121E RID: 4638 RVA: 0x0004E77D File Offset: 0x0004C97D
		IControllerTemplateButton ISixDofControllerTemplate.button10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(21);
			}
		}

		// (get) Token: 0x0600121F RID: 4639 RVA: 0x0004E787 File Offset: 0x0004C987
		IControllerTemplateButton ISixDofControllerTemplate.button11
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(22);
			}
		}

		// (get) Token: 0x06001220 RID: 4640 RVA: 0x0004E791 File Offset: 0x0004C991
		IControllerTemplateButton ISixDofControllerTemplate.button12
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(23);
			}
		}

		// (get) Token: 0x06001221 RID: 4641 RVA: 0x0004E79B File Offset: 0x0004C99B
		IControllerTemplateButton ISixDofControllerTemplate.button13
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(24);
			}
		}

		// (get) Token: 0x06001222 RID: 4642 RVA: 0x0004E7A5 File Offset: 0x0004C9A5
		IControllerTemplateButton ISixDofControllerTemplate.button14
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(25);
			}
		}

		// (get) Token: 0x06001223 RID: 4643 RVA: 0x0004E7AF File Offset: 0x0004C9AF
		IControllerTemplateButton ISixDofControllerTemplate.button15
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(26);
			}
		}

		// (get) Token: 0x06001224 RID: 4644 RVA: 0x0004E7B9 File Offset: 0x0004C9B9
		IControllerTemplateButton ISixDofControllerTemplate.button16
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(27);
			}
		}

		// (get) Token: 0x06001225 RID: 4645 RVA: 0x0004E7C3 File Offset: 0x0004C9C3
		IControllerTemplateButton ISixDofControllerTemplate.button17
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(28);
			}
		}

		// (get) Token: 0x06001226 RID: 4646 RVA: 0x0004E7CD File Offset: 0x0004C9CD
		IControllerTemplateButton ISixDofControllerTemplate.button18
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(29);
			}
		}

		// (get) Token: 0x06001227 RID: 4647 RVA: 0x0004E7D7 File Offset: 0x0004C9D7
		IControllerTemplateButton ISixDofControllerTemplate.button19
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(30);
			}
		}

		// (get) Token: 0x06001228 RID: 4648 RVA: 0x0004E7E1 File Offset: 0x0004C9E1
		IControllerTemplateButton ISixDofControllerTemplate.button20
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(31);
			}
		}

		// (get) Token: 0x06001229 RID: 4649 RVA: 0x0004E7EB File Offset: 0x0004C9EB
		IControllerTemplateButton ISixDofControllerTemplate.button21
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(55);
			}
		}

		// (get) Token: 0x0600122A RID: 4650 RVA: 0x0004E7F5 File Offset: 0x0004C9F5
		IControllerTemplateButton ISixDofControllerTemplate.button22
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(56);
			}
		}

		// (get) Token: 0x0600122B RID: 4651 RVA: 0x0004E7FF File Offset: 0x0004C9FF
		IControllerTemplateButton ISixDofControllerTemplate.button23
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(57);
			}
		}

		// (get) Token: 0x0600122C RID: 4652 RVA: 0x0004E809 File Offset: 0x0004CA09
		IControllerTemplateButton ISixDofControllerTemplate.button24
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(58);
			}
		}

		// (get) Token: 0x0600122D RID: 4653 RVA: 0x0004E813 File Offset: 0x0004CA13
		IControllerTemplateButton ISixDofControllerTemplate.button25
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(59);
			}
		}

		// (get) Token: 0x0600122E RID: 4654 RVA: 0x0004E81D File Offset: 0x0004CA1D
		IControllerTemplateButton ISixDofControllerTemplate.button26
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(60);
			}
		}

		// (get) Token: 0x0600122F RID: 4655 RVA: 0x0004E827 File Offset: 0x0004CA27
		IControllerTemplateButton ISixDofControllerTemplate.button27
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(61);
			}
		}

		// (get) Token: 0x06001230 RID: 4656 RVA: 0x0004E831 File Offset: 0x0004CA31
		IControllerTemplateButton ISixDofControllerTemplate.button28
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(62);
			}
		}

		// (get) Token: 0x06001231 RID: 4657 RVA: 0x0004E83B File Offset: 0x0004CA3B
		IControllerTemplateButton ISixDofControllerTemplate.button29
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(63);
			}
		}

		// (get) Token: 0x06001232 RID: 4658 RVA: 0x0004E845 File Offset: 0x0004CA45
		IControllerTemplateButton ISixDofControllerTemplate.button30
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(64);
			}
		}

		// (get) Token: 0x06001233 RID: 4659 RVA: 0x0004E84F File Offset: 0x0004CA4F
		IControllerTemplateButton ISixDofControllerTemplate.button31
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(65);
			}
		}

		// (get) Token: 0x06001234 RID: 4660 RVA: 0x0004E859 File Offset: 0x0004CA59
		IControllerTemplateButton ISixDofControllerTemplate.button32
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(66);
			}
		}

		// (get) Token: 0x06001235 RID: 4661 RVA: 0x0004E863 File Offset: 0x0004CA63
		IControllerTemplateHat ISixDofControllerTemplate.hat1
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(48);
			}
		}

		// (get) Token: 0x06001236 RID: 4662 RVA: 0x0004E86D File Offset: 0x0004CA6D
		IControllerTemplateHat ISixDofControllerTemplate.hat2
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(49);
			}
		}

		// (get) Token: 0x06001237 RID: 4663 RVA: 0x0004E877 File Offset: 0x0004CA77
		IControllerTemplateThrottle ISixDofControllerTemplate.throttle1
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(52);
			}
		}

		// (get) Token: 0x06001238 RID: 4664 RVA: 0x0004E881 File Offset: 0x0004CA81
		IControllerTemplateThrottle ISixDofControllerTemplate.throttle2
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(53);
			}
		}

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
