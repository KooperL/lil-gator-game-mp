using System;

namespace Rewired
{
	// Token: 0x02000401 RID: 1025
	public sealed class GamepadTemplate : ControllerTemplate, IGamepadTemplate, IControllerTemplate
	{
		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06001463 RID: 5219 RVA: 0x00010800 File Offset: 0x0000EA00
		IControllerTemplateButton IGamepadTemplate.actionBottomRow1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(4);
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06001464 RID: 5220 RVA: 0x00010800 File Offset: 0x0000EA00
		IControllerTemplateButton IGamepadTemplate.a
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(4);
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06001465 RID: 5221 RVA: 0x00010809 File Offset: 0x0000EA09
		IControllerTemplateButton IGamepadTemplate.actionBottomRow2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(5);
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06001466 RID: 5222 RVA: 0x00010809 File Offset: 0x0000EA09
		IControllerTemplateButton IGamepadTemplate.b
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(5);
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06001467 RID: 5223 RVA: 0x00010812 File Offset: 0x0000EA12
		IControllerTemplateButton IGamepadTemplate.actionBottomRow3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(6);
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06001468 RID: 5224 RVA: 0x00010812 File Offset: 0x0000EA12
		IControllerTemplateButton IGamepadTemplate.c
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(6);
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06001469 RID: 5225 RVA: 0x0001081B File Offset: 0x0000EA1B
		IControllerTemplateButton IGamepadTemplate.actionTopRow1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(7);
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x0600146A RID: 5226 RVA: 0x0001081B File Offset: 0x0000EA1B
		IControllerTemplateButton IGamepadTemplate.x
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(7);
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x0600146B RID: 5227 RVA: 0x00010824 File Offset: 0x0000EA24
		IControllerTemplateButton IGamepadTemplate.actionTopRow2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(8);
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x0600146C RID: 5228 RVA: 0x00010824 File Offset: 0x0000EA24
		IControllerTemplateButton IGamepadTemplate.y
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(8);
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x0600146D RID: 5229 RVA: 0x0001082D File Offset: 0x0000EA2D
		IControllerTemplateButton IGamepadTemplate.actionTopRow3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(9);
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x0600146E RID: 5230 RVA: 0x0001082D File Offset: 0x0000EA2D
		IControllerTemplateButton IGamepadTemplate.z
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(9);
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x0600146F RID: 5231 RVA: 0x00010837 File Offset: 0x0000EA37
		IControllerTemplateButton IGamepadTemplate.leftShoulder1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(10);
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x00010837 File Offset: 0x0000EA37
		IControllerTemplateButton IGamepadTemplate.leftBumper
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(10);
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06001471 RID: 5233 RVA: 0x00010841 File Offset: 0x0000EA41
		IControllerTemplateAxis IGamepadTemplate.leftShoulder2
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(11);
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x00010841 File Offset: 0x0000EA41
		IControllerTemplateAxis IGamepadTemplate.leftTrigger
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(11);
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06001473 RID: 5235 RVA: 0x0001084B File Offset: 0x0000EA4B
		IControllerTemplateButton IGamepadTemplate.rightShoulder1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(12);
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06001474 RID: 5236 RVA: 0x0001084B File Offset: 0x0000EA4B
		IControllerTemplateButton IGamepadTemplate.rightBumper
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(12);
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06001475 RID: 5237 RVA: 0x00010855 File Offset: 0x0000EA55
		IControllerTemplateAxis IGamepadTemplate.rightShoulder2
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(13);
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06001476 RID: 5238 RVA: 0x00010855 File Offset: 0x0000EA55
		IControllerTemplateAxis IGamepadTemplate.rightTrigger
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(13);
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06001477 RID: 5239 RVA: 0x0001085F File Offset: 0x0000EA5F
		IControllerTemplateButton IGamepadTemplate.center1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(14);
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06001478 RID: 5240 RVA: 0x0001085F File Offset: 0x0000EA5F
		IControllerTemplateButton IGamepadTemplate.back
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(14);
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06001479 RID: 5241 RVA: 0x00010869 File Offset: 0x0000EA69
		IControllerTemplateButton IGamepadTemplate.center2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(15);
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x0600147A RID: 5242 RVA: 0x00010869 File Offset: 0x0000EA69
		IControllerTemplateButton IGamepadTemplate.start
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(15);
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x0600147B RID: 5243 RVA: 0x00010873 File Offset: 0x0000EA73
		IControllerTemplateButton IGamepadTemplate.center3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(16);
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x0600147C RID: 5244 RVA: 0x00010873 File Offset: 0x0000EA73
		IControllerTemplateButton IGamepadTemplate.guide
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(16);
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x0600147D RID: 5245 RVA: 0x0001087D File Offset: 0x0000EA7D
		IControllerTemplateThumbStick IGamepadTemplate.leftStick
		{
			get
			{
				return base.GetElement<IControllerTemplateThumbStick>(23);
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x0600147E RID: 5246 RVA: 0x00010887 File Offset: 0x0000EA87
		IControllerTemplateThumbStick IGamepadTemplate.rightStick
		{
			get
			{
				return base.GetElement<IControllerTemplateThumbStick>(24);
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x0600147F RID: 5247 RVA: 0x00010891 File Offset: 0x0000EA91
		IControllerTemplateDPad IGamepadTemplate.dPad
		{
			get
			{
				return base.GetElement<IControllerTemplateDPad>(25);
			}
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x0001089B File Offset: 0x0000EA9B
		public GamepadTemplate(object payload)
			: base(payload)
		{
		}

		// Token: 0x0400194A RID: 6474
		public static readonly Guid typeGuid = new Guid("83b427e4-086f-47f3-bb06-be266abd1ca5");

		// Token: 0x0400194B RID: 6475
		public const int elementId_leftStickX = 0;

		// Token: 0x0400194C RID: 6476
		public const int elementId_leftStickY = 1;

		// Token: 0x0400194D RID: 6477
		public const int elementId_rightStickX = 2;

		// Token: 0x0400194E RID: 6478
		public const int elementId_rightStickY = 3;

		// Token: 0x0400194F RID: 6479
		public const int elementId_actionBottomRow1 = 4;

		// Token: 0x04001950 RID: 6480
		public const int elementId_a = 4;

		// Token: 0x04001951 RID: 6481
		public const int elementId_actionBottomRow2 = 5;

		// Token: 0x04001952 RID: 6482
		public const int elementId_b = 5;

		// Token: 0x04001953 RID: 6483
		public const int elementId_actionBottomRow3 = 6;

		// Token: 0x04001954 RID: 6484
		public const int elementId_c = 6;

		// Token: 0x04001955 RID: 6485
		public const int elementId_actionTopRow1 = 7;

		// Token: 0x04001956 RID: 6486
		public const int elementId_x = 7;

		// Token: 0x04001957 RID: 6487
		public const int elementId_actionTopRow2 = 8;

		// Token: 0x04001958 RID: 6488
		public const int elementId_y = 8;

		// Token: 0x04001959 RID: 6489
		public const int elementId_actionTopRow3 = 9;

		// Token: 0x0400195A RID: 6490
		public const int elementId_z = 9;

		// Token: 0x0400195B RID: 6491
		public const int elementId_leftShoulder1 = 10;

		// Token: 0x0400195C RID: 6492
		public const int elementId_leftBumper = 10;

		// Token: 0x0400195D RID: 6493
		public const int elementId_leftShoulder2 = 11;

		// Token: 0x0400195E RID: 6494
		public const int elementId_leftTrigger = 11;

		// Token: 0x0400195F RID: 6495
		public const int elementId_rightShoulder1 = 12;

		// Token: 0x04001960 RID: 6496
		public const int elementId_rightBumper = 12;

		// Token: 0x04001961 RID: 6497
		public const int elementId_rightShoulder2 = 13;

		// Token: 0x04001962 RID: 6498
		public const int elementId_rightTrigger = 13;

		// Token: 0x04001963 RID: 6499
		public const int elementId_center1 = 14;

		// Token: 0x04001964 RID: 6500
		public const int elementId_back = 14;

		// Token: 0x04001965 RID: 6501
		public const int elementId_center2 = 15;

		// Token: 0x04001966 RID: 6502
		public const int elementId_start = 15;

		// Token: 0x04001967 RID: 6503
		public const int elementId_center3 = 16;

		// Token: 0x04001968 RID: 6504
		public const int elementId_guide = 16;

		// Token: 0x04001969 RID: 6505
		public const int elementId_leftStickButton = 17;

		// Token: 0x0400196A RID: 6506
		public const int elementId_rightStickButton = 18;

		// Token: 0x0400196B RID: 6507
		public const int elementId_dPadUp = 19;

		// Token: 0x0400196C RID: 6508
		public const int elementId_dPadRight = 20;

		// Token: 0x0400196D RID: 6509
		public const int elementId_dPadDown = 21;

		// Token: 0x0400196E RID: 6510
		public const int elementId_dPadLeft = 22;

		// Token: 0x0400196F RID: 6511
		public const int elementId_leftStick = 23;

		// Token: 0x04001970 RID: 6512
		public const int elementId_rightStick = 24;

		// Token: 0x04001971 RID: 6513
		public const int elementId_dPad = 25;
	}
}
