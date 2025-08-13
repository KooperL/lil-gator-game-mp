using System;
using Rewired.Dev;

namespace RewiredConsts
{
	// Token: 0x020002FB RID: 763
	public static class Action
	{
		// Token: 0x0400154E RID: 5454
		[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Move Horizontal")]
		public const int Move_Horizontal = 0;

		// Token: 0x0400154F RID: 5455
		[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Move Vertical")]
		public const int Move_Vertical = 1;

		// Token: 0x04001550 RID: 5456
		[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Look Vertical")]
		public const int Look_Vertical = 7;

		// Token: 0x04001551 RID: 5457
		[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Look Horizontal")]
		public const int Look_Horizontal = 8;

		// Token: 0x04001552 RID: 5458
		[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Jump")]
		public const int Jump = 2;

		// Token: 0x04001553 RID: 5459
		[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Primary")]
		public const int Primary = 10;

		// Token: 0x04001554 RID: 5460
		[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Secondary")]
		public const int Secondary = 11;

		// Token: 0x04001555 RID: 5461
		[ActionIdFieldInfo(categoryName = "Default", friendlyName = "UseItem")]
		public const int UseItem = 12;

		// Token: 0x04001556 RID: 5462
		[ActionIdFieldInfo(categoryName = "Default", friendlyName = "UseItemR")]
		public const int UseItemR = 42;

		// Token: 0x04001557 RID: 5463
		[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Interact")]
		public const int Interact = 9;

		// Token: 0x04001558 RID: 5464
		[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Inventory")]
		public const int Inventory = 36;

		// Token: 0x04001559 RID: 5465
		[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Skip Dialogue")]
		public const int SkipDialogue = 45;

		// Token: 0x0400155A RID: 5466
		[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UIHorizontal")]
		public const int UIHorizontal = 3;

		// Token: 0x0400155B RID: 5467
		[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UIVertical")]
		public const int UIVertical = 4;

		// Token: 0x0400155C RID: 5468
		[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UI Scroll")]
		public const int UIScroll = 33;

		// Token: 0x0400155D RID: 5469
		[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UISubmit")]
		public const int UISubmit = 5;

		// Token: 0x0400155E RID: 5470
		[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UISubmitInput")]
		public const int UISubmitInput = 31;

		// Token: 0x0400155F RID: 5471
		[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UICancel")]
		public const int UICancel = 6;

		// Token: 0x04001560 RID: 5472
		[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UITabRight")]
		public const int UITabRight = 16;

		// Token: 0x04001561 RID: 5473
		[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UITabLeft")]
		public const int UITabLeft = 17;

		// Token: 0x04001562 RID: 5474
		[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UISubmit Button Prompt")]
		public const int UISubmitPrompt = 38;

		// Token: 0x04001563 RID: 5475
		[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UICancel Button Prompt")]
		public const int UICancelPrompt = 39;

		// Token: 0x04001564 RID: 5476
		[ActionIdFieldInfo(categoryName = "UI", friendlyName = "Interact Dialogue")]
		public const int Interact_Dialogue = 41;

		// Token: 0x04001565 RID: 5477
		[ActionIdFieldInfo(categoryName = "Menu", friendlyName = "Pause")]
		public const int Pause = 37;

		// Token: 0x04001566 RID: 5478
		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugBracelet")]
		public const int DebugBracelet = 18;

		// Token: 0x04001567 RID: 5479
		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugItems")]
		public const int DebugItems = 19;

		// Token: 0x04001568 RID: 5480
		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugToggle")]
		public const int DebugToggle = 20;

		// Token: 0x04001569 RID: 5481
		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugSkip")]
		public const int DebugSkip = 21;

		// Token: 0x0400156A RID: 5482
		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugToggleHide")]
		public const int DebugToggleHide = 22;

		// Token: 0x0400156B RID: 5483
		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugLaunch")]
		public const int DebugLaunch = 23;

		// Token: 0x0400156C RID: 5484
		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugTime")]
		public const int DebugTime = 24;

		// Token: 0x0400156D RID: 5485
		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugToggleCamera")]
		public const int DebugToggleCamera = 25;

		// Token: 0x0400156E RID: 5486
		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugCameraHorizontal")]
		public const int DebugCameraHorizontal = 26;

		// Token: 0x0400156F RID: 5487
		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugCameraForward")]
		public const int DebugCameraForward = 27;

		// Token: 0x04001570 RID: 5488
		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugCameraVertical")]
		public const int DebugCameraVertical = 28;

		// Token: 0x04001571 RID: 5489
		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugCameraSpeed")]
		public const int DebugCameraSpeed = 29;

		// Token: 0x04001572 RID: 5490
		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugToggleUI")]
		public const int DebugToggleUI = 30;

		// Token: 0x04001573 RID: 5491
		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "Progress State")]
		public const int DebugProgressState = 32;

		// Token: 0x04001574 RID: 5492
		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "Toggle Save")]
		public const int DebugToggleSave = 34;

		// Token: 0x04001575 RID: 5493
		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "Toggle Debug Info")]
		public const int DebugToggleInfo = 43;

		// Token: 0x04001576 RID: 5494
		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "Proof Reading Mode")]
		public const int DebugProofRead = 44;
	}
}
