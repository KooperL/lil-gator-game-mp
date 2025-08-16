using System;

namespace Rewired
{
	public sealed class GamepadTemplate : ControllerTemplate, IGamepadTemplate, IControllerTemplate
	{
		// (get) Token: 0x060014C3 RID: 5315 RVA: 0x00010BE8 File Offset: 0x0000EDE8
		IControllerTemplateButton IGamepadTemplate.actionBottomRow1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(4);
			}
		}

		// (get) Token: 0x060014C4 RID: 5316 RVA: 0x00010BE8 File Offset: 0x0000EDE8
		IControllerTemplateButton IGamepadTemplate.a
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(4);
			}
		}

		// (get) Token: 0x060014C5 RID: 5317 RVA: 0x00010BF1 File Offset: 0x0000EDF1
		IControllerTemplateButton IGamepadTemplate.actionBottomRow2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(5);
			}
		}

		// (get) Token: 0x060014C6 RID: 5318 RVA: 0x00010BF1 File Offset: 0x0000EDF1
		IControllerTemplateButton IGamepadTemplate.b
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(5);
			}
		}

		// (get) Token: 0x060014C7 RID: 5319 RVA: 0x00010BFA File Offset: 0x0000EDFA
		IControllerTemplateButton IGamepadTemplate.actionBottomRow3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(6);
			}
		}

		// (get) Token: 0x060014C8 RID: 5320 RVA: 0x00010BFA File Offset: 0x0000EDFA
		IControllerTemplateButton IGamepadTemplate.c
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(6);
			}
		}

		// (get) Token: 0x060014C9 RID: 5321 RVA: 0x00010C03 File Offset: 0x0000EE03
		IControllerTemplateButton IGamepadTemplate.actionTopRow1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(7);
			}
		}

		// (get) Token: 0x060014CA RID: 5322 RVA: 0x00010C03 File Offset: 0x0000EE03
		IControllerTemplateButton IGamepadTemplate.x
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(7);
			}
		}

		// (get) Token: 0x060014CB RID: 5323 RVA: 0x00010C0C File Offset: 0x0000EE0C
		IControllerTemplateButton IGamepadTemplate.actionTopRow2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(8);
			}
		}

		// (get) Token: 0x060014CC RID: 5324 RVA: 0x00010C0C File Offset: 0x0000EE0C
		IControllerTemplateButton IGamepadTemplate.y
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(8);
			}
		}

		// (get) Token: 0x060014CD RID: 5325 RVA: 0x00010C15 File Offset: 0x0000EE15
		IControllerTemplateButton IGamepadTemplate.actionTopRow3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(9);
			}
		}

		// (get) Token: 0x060014CE RID: 5326 RVA: 0x00010C15 File Offset: 0x0000EE15
		IControllerTemplateButton IGamepadTemplate.z
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(9);
			}
		}

		// (get) Token: 0x060014CF RID: 5327 RVA: 0x00010C1F File Offset: 0x0000EE1F
		IControllerTemplateButton IGamepadTemplate.leftShoulder1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(10);
			}
		}

		// (get) Token: 0x060014D0 RID: 5328 RVA: 0x00010C1F File Offset: 0x0000EE1F
		IControllerTemplateButton IGamepadTemplate.leftBumper
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(10);
			}
		}

		// (get) Token: 0x060014D1 RID: 5329 RVA: 0x00010C29 File Offset: 0x0000EE29
		IControllerTemplateAxis IGamepadTemplate.leftShoulder2
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(11);
			}
		}

		// (get) Token: 0x060014D2 RID: 5330 RVA: 0x00010C29 File Offset: 0x0000EE29
		IControllerTemplateAxis IGamepadTemplate.leftTrigger
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(11);
			}
		}

		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x00010C33 File Offset: 0x0000EE33
		IControllerTemplateButton IGamepadTemplate.rightShoulder1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(12);
			}
		}

		// (get) Token: 0x060014D4 RID: 5332 RVA: 0x00010C33 File Offset: 0x0000EE33
		IControllerTemplateButton IGamepadTemplate.rightBumper
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(12);
			}
		}

		// (get) Token: 0x060014D5 RID: 5333 RVA: 0x00010C3D File Offset: 0x0000EE3D
		IControllerTemplateAxis IGamepadTemplate.rightShoulder2
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(13);
			}
		}

		// (get) Token: 0x060014D6 RID: 5334 RVA: 0x00010C3D File Offset: 0x0000EE3D
		IControllerTemplateAxis IGamepadTemplate.rightTrigger
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(13);
			}
		}

		// (get) Token: 0x060014D7 RID: 5335 RVA: 0x00010C47 File Offset: 0x0000EE47
		IControllerTemplateButton IGamepadTemplate.center1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(14);
			}
		}

		// (get) Token: 0x060014D8 RID: 5336 RVA: 0x00010C47 File Offset: 0x0000EE47
		IControllerTemplateButton IGamepadTemplate.back
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(14);
			}
		}

		// (get) Token: 0x060014D9 RID: 5337 RVA: 0x00010C51 File Offset: 0x0000EE51
		IControllerTemplateButton IGamepadTemplate.center2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(15);
			}
		}

		// (get) Token: 0x060014DA RID: 5338 RVA: 0x00010C51 File Offset: 0x0000EE51
		IControllerTemplateButton IGamepadTemplate.start
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(15);
			}
		}

		// (get) Token: 0x060014DB RID: 5339 RVA: 0x00010C5B File Offset: 0x0000EE5B
		IControllerTemplateButton IGamepadTemplate.center3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(16);
			}
		}

		// (get) Token: 0x060014DC RID: 5340 RVA: 0x00010C5B File Offset: 0x0000EE5B
		IControllerTemplateButton IGamepadTemplate.guide
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(16);
			}
		}

		// (get) Token: 0x060014DD RID: 5341 RVA: 0x00010C65 File Offset: 0x0000EE65
		IControllerTemplateThumbStick IGamepadTemplate.leftStick
		{
			get
			{
				return base.GetElement<IControllerTemplateThumbStick>(23);
			}
		}

		// (get) Token: 0x060014DE RID: 5342 RVA: 0x00010C6F File Offset: 0x0000EE6F
		IControllerTemplateThumbStick IGamepadTemplate.rightStick
		{
			get
			{
				return base.GetElement<IControllerTemplateThumbStick>(24);
			}
		}

		// (get) Token: 0x060014DF RID: 5343 RVA: 0x00010C79 File Offset: 0x0000EE79
		IControllerTemplateDPad IGamepadTemplate.dPad
		{
			get
			{
				return base.GetElement<IControllerTemplateDPad>(25);
			}
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x00010C83 File Offset: 0x0000EE83
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
