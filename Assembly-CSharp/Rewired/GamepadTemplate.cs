using System;

namespace Rewired
{
	public sealed class GamepadTemplate : ControllerTemplate, IGamepadTemplate, IControllerTemplate
	{
		// (get) Token: 0x060014C3 RID: 5315 RVA: 0x00010C07 File Offset: 0x0000EE07
		IControllerTemplateButton IGamepadTemplate.actionBottomRow1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(4);
			}
		}

		// (get) Token: 0x060014C4 RID: 5316 RVA: 0x00010C07 File Offset: 0x0000EE07
		IControllerTemplateButton IGamepadTemplate.a
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(4);
			}
		}

		// (get) Token: 0x060014C5 RID: 5317 RVA: 0x00010C10 File Offset: 0x0000EE10
		IControllerTemplateButton IGamepadTemplate.actionBottomRow2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(5);
			}
		}

		// (get) Token: 0x060014C6 RID: 5318 RVA: 0x00010C10 File Offset: 0x0000EE10
		IControllerTemplateButton IGamepadTemplate.b
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(5);
			}
		}

		// (get) Token: 0x060014C7 RID: 5319 RVA: 0x00010C19 File Offset: 0x0000EE19
		IControllerTemplateButton IGamepadTemplate.actionBottomRow3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(6);
			}
		}

		// (get) Token: 0x060014C8 RID: 5320 RVA: 0x00010C19 File Offset: 0x0000EE19
		IControllerTemplateButton IGamepadTemplate.c
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(6);
			}
		}

		// (get) Token: 0x060014C9 RID: 5321 RVA: 0x00010C22 File Offset: 0x0000EE22
		IControllerTemplateButton IGamepadTemplate.actionTopRow1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(7);
			}
		}

		// (get) Token: 0x060014CA RID: 5322 RVA: 0x00010C22 File Offset: 0x0000EE22
		IControllerTemplateButton IGamepadTemplate.x
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(7);
			}
		}

		// (get) Token: 0x060014CB RID: 5323 RVA: 0x00010C2B File Offset: 0x0000EE2B
		IControllerTemplateButton IGamepadTemplate.actionTopRow2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(8);
			}
		}

		// (get) Token: 0x060014CC RID: 5324 RVA: 0x00010C2B File Offset: 0x0000EE2B
		IControllerTemplateButton IGamepadTemplate.y
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(8);
			}
		}

		// (get) Token: 0x060014CD RID: 5325 RVA: 0x00010C34 File Offset: 0x0000EE34
		IControllerTemplateButton IGamepadTemplate.actionTopRow3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(9);
			}
		}

		// (get) Token: 0x060014CE RID: 5326 RVA: 0x00010C34 File Offset: 0x0000EE34
		IControllerTemplateButton IGamepadTemplate.z
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(9);
			}
		}

		// (get) Token: 0x060014CF RID: 5327 RVA: 0x00010C3E File Offset: 0x0000EE3E
		IControllerTemplateButton IGamepadTemplate.leftShoulder1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(10);
			}
		}

		// (get) Token: 0x060014D0 RID: 5328 RVA: 0x00010C3E File Offset: 0x0000EE3E
		IControllerTemplateButton IGamepadTemplate.leftBumper
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(10);
			}
		}

		// (get) Token: 0x060014D1 RID: 5329 RVA: 0x00010C48 File Offset: 0x0000EE48
		IControllerTemplateAxis IGamepadTemplate.leftShoulder2
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(11);
			}
		}

		// (get) Token: 0x060014D2 RID: 5330 RVA: 0x00010C48 File Offset: 0x0000EE48
		IControllerTemplateAxis IGamepadTemplate.leftTrigger
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(11);
			}
		}

		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x00010C52 File Offset: 0x0000EE52
		IControllerTemplateButton IGamepadTemplate.rightShoulder1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(12);
			}
		}

		// (get) Token: 0x060014D4 RID: 5332 RVA: 0x00010C52 File Offset: 0x0000EE52
		IControllerTemplateButton IGamepadTemplate.rightBumper
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(12);
			}
		}

		// (get) Token: 0x060014D5 RID: 5333 RVA: 0x00010C5C File Offset: 0x0000EE5C
		IControllerTemplateAxis IGamepadTemplate.rightShoulder2
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(13);
			}
		}

		// (get) Token: 0x060014D6 RID: 5334 RVA: 0x00010C5C File Offset: 0x0000EE5C
		IControllerTemplateAxis IGamepadTemplate.rightTrigger
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(13);
			}
		}

		// (get) Token: 0x060014D7 RID: 5335 RVA: 0x00010C66 File Offset: 0x0000EE66
		IControllerTemplateButton IGamepadTemplate.center1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(14);
			}
		}

		// (get) Token: 0x060014D8 RID: 5336 RVA: 0x00010C66 File Offset: 0x0000EE66
		IControllerTemplateButton IGamepadTemplate.back
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(14);
			}
		}

		// (get) Token: 0x060014D9 RID: 5337 RVA: 0x00010C70 File Offset: 0x0000EE70
		IControllerTemplateButton IGamepadTemplate.center2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(15);
			}
		}

		// (get) Token: 0x060014DA RID: 5338 RVA: 0x00010C70 File Offset: 0x0000EE70
		IControllerTemplateButton IGamepadTemplate.start
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(15);
			}
		}

		// (get) Token: 0x060014DB RID: 5339 RVA: 0x00010C7A File Offset: 0x0000EE7A
		IControllerTemplateButton IGamepadTemplate.center3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(16);
			}
		}

		// (get) Token: 0x060014DC RID: 5340 RVA: 0x00010C7A File Offset: 0x0000EE7A
		IControllerTemplateButton IGamepadTemplate.guide
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(16);
			}
		}

		// (get) Token: 0x060014DD RID: 5341 RVA: 0x00010C84 File Offset: 0x0000EE84
		IControllerTemplateThumbStick IGamepadTemplate.leftStick
		{
			get
			{
				return base.GetElement<IControllerTemplateThumbStick>(23);
			}
		}

		// (get) Token: 0x060014DE RID: 5342 RVA: 0x00010C8E File Offset: 0x0000EE8E
		IControllerTemplateThumbStick IGamepadTemplate.rightStick
		{
			get
			{
				return base.GetElement<IControllerTemplateThumbStick>(24);
			}
		}

		// (get) Token: 0x060014DF RID: 5343 RVA: 0x00010C98 File Offset: 0x0000EE98
		IControllerTemplateDPad IGamepadTemplate.dPad
		{
			get
			{
				return base.GetElement<IControllerTemplateDPad>(25);
			}
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x00010CA2 File Offset: 0x0000EEA2
		public GamepadTemplate(object payload)
			: base(payload)
		{
		}

		public static readonly Guid typeGuid = new Guid("83b427e4-086f-47f3-bb06-be266abd1ca5");

		public const int elementId_leftStickX = 0;

		public const int elementId_leftStickY = 1;

		public const int elementId_rightStickX = 2;

		public const int elementId_rightStickY = 3;

		public const int elementId_actionBottomRow1 = 4;

		public const int elementId_a = 4;

		public const int elementId_actionBottomRow2 = 5;

		public const int elementId_b = 5;

		public const int elementId_actionBottomRow3 = 6;

		public const int elementId_c = 6;

		public const int elementId_actionTopRow1 = 7;

		public const int elementId_x = 7;

		public const int elementId_actionTopRow2 = 8;

		public const int elementId_y = 8;

		public const int elementId_actionTopRow3 = 9;

		public const int elementId_z = 9;

		public const int elementId_leftShoulder1 = 10;

		public const int elementId_leftBumper = 10;

		public const int elementId_leftShoulder2 = 11;

		public const int elementId_leftTrigger = 11;

		public const int elementId_rightShoulder1 = 12;

		public const int elementId_rightBumper = 12;

		public const int elementId_rightShoulder2 = 13;

		public const int elementId_rightTrigger = 13;

		public const int elementId_center1 = 14;

		public const int elementId_back = 14;

		public const int elementId_center2 = 15;

		public const int elementId_start = 15;

		public const int elementId_center3 = 16;

		public const int elementId_guide = 16;

		public const int elementId_leftStickButton = 17;

		public const int elementId_rightStickButton = 18;

		public const int elementId_dPadUp = 19;

		public const int elementId_dPadRight = 20;

		public const int elementId_dPadDown = 21;

		public const int elementId_dPadLeft = 22;

		public const int elementId_leftStick = 23;

		public const int elementId_rightStick = 24;

		public const int elementId_dPad = 25;
	}
}
