using System;

namespace Rewired
{
	public sealed class GamepadTemplate : ControllerTemplate, IGamepadTemplate, IControllerTemplate
	{
		// (get) Token: 0x06001134 RID: 4404 RVA: 0x0004DDEB File Offset: 0x0004BFEB
		IControllerTemplateButton IGamepadTemplate.actionBottomRow1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(4);
			}
		}

		// (get) Token: 0x06001135 RID: 4405 RVA: 0x0004DDF4 File Offset: 0x0004BFF4
		IControllerTemplateButton IGamepadTemplate.a
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(4);
			}
		}

		// (get) Token: 0x06001136 RID: 4406 RVA: 0x0004DDFD File Offset: 0x0004BFFD
		IControllerTemplateButton IGamepadTemplate.actionBottomRow2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(5);
			}
		}

		// (get) Token: 0x06001137 RID: 4407 RVA: 0x0004DE06 File Offset: 0x0004C006
		IControllerTemplateButton IGamepadTemplate.b
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(5);
			}
		}

		// (get) Token: 0x06001138 RID: 4408 RVA: 0x0004DE0F File Offset: 0x0004C00F
		IControllerTemplateButton IGamepadTemplate.actionBottomRow3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(6);
			}
		}

		// (get) Token: 0x06001139 RID: 4409 RVA: 0x0004DE18 File Offset: 0x0004C018
		IControllerTemplateButton IGamepadTemplate.c
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(6);
			}
		}

		// (get) Token: 0x0600113A RID: 4410 RVA: 0x0004DE21 File Offset: 0x0004C021
		IControllerTemplateButton IGamepadTemplate.actionTopRow1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(7);
			}
		}

		// (get) Token: 0x0600113B RID: 4411 RVA: 0x0004DE2A File Offset: 0x0004C02A
		IControllerTemplateButton IGamepadTemplate.x
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(7);
			}
		}

		// (get) Token: 0x0600113C RID: 4412 RVA: 0x0004DE33 File Offset: 0x0004C033
		IControllerTemplateButton IGamepadTemplate.actionTopRow2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(8);
			}
		}

		// (get) Token: 0x0600113D RID: 4413 RVA: 0x0004DE3C File Offset: 0x0004C03C
		IControllerTemplateButton IGamepadTemplate.y
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(8);
			}
		}

		// (get) Token: 0x0600113E RID: 4414 RVA: 0x0004DE45 File Offset: 0x0004C045
		IControllerTemplateButton IGamepadTemplate.actionTopRow3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(9);
			}
		}

		// (get) Token: 0x0600113F RID: 4415 RVA: 0x0004DE4F File Offset: 0x0004C04F
		IControllerTemplateButton IGamepadTemplate.z
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(9);
			}
		}

		// (get) Token: 0x06001140 RID: 4416 RVA: 0x0004DE59 File Offset: 0x0004C059
		IControllerTemplateButton IGamepadTemplate.leftShoulder1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(10);
			}
		}

		// (get) Token: 0x06001141 RID: 4417 RVA: 0x0004DE63 File Offset: 0x0004C063
		IControllerTemplateButton IGamepadTemplate.leftBumper
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(10);
			}
		}

		// (get) Token: 0x06001142 RID: 4418 RVA: 0x0004DE6D File Offset: 0x0004C06D
		IControllerTemplateAxis IGamepadTemplate.leftShoulder2
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(11);
			}
		}

		// (get) Token: 0x06001143 RID: 4419 RVA: 0x0004DE77 File Offset: 0x0004C077
		IControllerTemplateAxis IGamepadTemplate.leftTrigger
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(11);
			}
		}

		// (get) Token: 0x06001144 RID: 4420 RVA: 0x0004DE81 File Offset: 0x0004C081
		IControllerTemplateButton IGamepadTemplate.rightShoulder1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(12);
			}
		}

		// (get) Token: 0x06001145 RID: 4421 RVA: 0x0004DE8B File Offset: 0x0004C08B
		IControllerTemplateButton IGamepadTemplate.rightBumper
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(12);
			}
		}

		// (get) Token: 0x06001146 RID: 4422 RVA: 0x0004DE95 File Offset: 0x0004C095
		IControllerTemplateAxis IGamepadTemplate.rightShoulder2
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(13);
			}
		}

		// (get) Token: 0x06001147 RID: 4423 RVA: 0x0004DE9F File Offset: 0x0004C09F
		IControllerTemplateAxis IGamepadTemplate.rightTrigger
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(13);
			}
		}

		// (get) Token: 0x06001148 RID: 4424 RVA: 0x0004DEA9 File Offset: 0x0004C0A9
		IControllerTemplateButton IGamepadTemplate.center1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(14);
			}
		}

		// (get) Token: 0x06001149 RID: 4425 RVA: 0x0004DEB3 File Offset: 0x0004C0B3
		IControllerTemplateButton IGamepadTemplate.back
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(14);
			}
		}

		// (get) Token: 0x0600114A RID: 4426 RVA: 0x0004DEBD File Offset: 0x0004C0BD
		IControllerTemplateButton IGamepadTemplate.center2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(15);
			}
		}

		// (get) Token: 0x0600114B RID: 4427 RVA: 0x0004DEC7 File Offset: 0x0004C0C7
		IControllerTemplateButton IGamepadTemplate.start
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(15);
			}
		}

		// (get) Token: 0x0600114C RID: 4428 RVA: 0x0004DED1 File Offset: 0x0004C0D1
		IControllerTemplateButton IGamepadTemplate.center3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(16);
			}
		}

		// (get) Token: 0x0600114D RID: 4429 RVA: 0x0004DEDB File Offset: 0x0004C0DB
		IControllerTemplateButton IGamepadTemplate.guide
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(16);
			}
		}

		// (get) Token: 0x0600114E RID: 4430 RVA: 0x0004DEE5 File Offset: 0x0004C0E5
		IControllerTemplateThumbStick IGamepadTemplate.leftStick
		{
			get
			{
				return base.GetElement<IControllerTemplateThumbStick>(23);
			}
		}

		// (get) Token: 0x0600114F RID: 4431 RVA: 0x0004DEEF File Offset: 0x0004C0EF
		IControllerTemplateThumbStick IGamepadTemplate.rightStick
		{
			get
			{
				return base.GetElement<IControllerTemplateThumbStick>(24);
			}
		}

		// (get) Token: 0x06001150 RID: 4432 RVA: 0x0004DEF9 File Offset: 0x0004C0F9
		IControllerTemplateDPad IGamepadTemplate.dPad
		{
			get
			{
				return base.GetElement<IControllerTemplateDPad>(25);
			}
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x0004DF03 File Offset: 0x0004C103
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
