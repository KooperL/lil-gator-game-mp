using System;
using Rewired.Dev;

namespace RewiredConsts
{
	// Token: 0x020003F0 RID: 1008
	public static class Action
	{
		// Token: 0x04001917 RID: 6423
		[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Move Horizontal")]
		public const int Move_Horizontal = 0;

		// Token: 0x04001918 RID: 6424
		[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Move Vertical")]
		public const int Move_Vertical = 1;

		// Token: 0x04001919 RID: 6425
		[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Look Vertical")]
		public const int Look_Vertical = 7;

		// Token: 0x0400191A RID: 6426
		[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Look Horizontal")]
		public const int Look_Horizontal = 8;

		// Token: 0x0400191B RID: 6427
		[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Jump")]
		public const int Jump = 2;

		// Token: 0x0400191C RID: 6428
		[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Primary")]
		public const int Primary = 10;

		// Token: 0x0400191D RID: 6429
		[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Secondary")]
		public const int Secondary = 11;

		// Token: 0x0400191E RID: 6430
		[ActionIdFieldInfo(categoryName = "Default", friendlyName = "UseItem")]
		public const int UseItem = 12;

		// Token: 0x0400191F RID: 6431
		[ActionIdFieldInfo(categoryName = "Default", friendlyName = "UseItemR")]
		public const int UseItemR = 42;

		// Token: 0x04001920 RID: 6432
		[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Interact")]
		public const int Interact = 9;

		// Token: 0x04001921 RID: 6433
		[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Inventory")]
		public const int Inventory = 36;

		// Token: 0x04001922 RID: 6434
		[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Skip Dialogue")]
		public const int SkipDialogue = 45;

		// Token: 0x04001923 RID: 6435
		[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UIHorizontal")]
		public const int UIHorizontal = 3;

		// Token: 0x04001924 RID: 6436
		[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UIVertical")]
		public const int UIVertical = 4;

		// Token: 0x04001925 RID: 6437
		[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UI Scroll")]
		public const int UIScroll = 33;

		// Token: 0x04001926 RID: 6438
		[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UISubmit")]
		public const int UISubmit = 5;

		// Token: 0x04001927 RID: 6439
		[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UISubmitInput")]
		public const int UISubmitInput = 31;

		// Token: 0x04001928 RID: 6440
		[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UICancel")]
		public const int UICancel = 6;

		// Token: 0x04001929 RID: 6441
		[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UITabRight")]
		public const int UITabRight = 16;

		// Token: 0x0400192A RID: 6442
		[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UITabLeft")]
		public const int UITabLeft = 17;

		// Token: 0x0400192B RID: 6443
		[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UISubmit Button Prompt")]
		public const int UISubmitPrompt = 38;

		// Token: 0x0400192C RID: 6444
		[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UICancel Button Prompt")]
		public const int UICancelPrompt = 39;

		// Token: 0x0400192D RID: 6445
		[ActionIdFieldInfo(categoryName = "UI", friendlyName = "Interact Dialogue")]
		public const int Interact_Dialogue = 41;

		// Token: 0x0400192E RID: 6446
		[ActionIdFieldInfo(categoryName = "Menu", friendlyName = "Pause")]
		public const int Pause = 37;

		// Token: 0x0400192F RID: 6447
		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugBracelet")]
		public const int DebugBracelet = 18;

		// Token: 0x04001930 RID: 6448
		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugItems")]
		public const int DebugItems = 19;

		// Token: 0x04001931 RID: 6449
		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugToggle")]
		public const int DebugToggle = 20;

		// Token: 0x04001932 RID: 6450
		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugSkip")]
		public const int DebugSkip = 21;

		// Token: 0x04001933 RID: 6451
		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugToggleHide")]
		public const int DebugToggleHide = 22;

		// Token: 0x04001934 RID: 6452
		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugLaunch")]
		public const int DebugLaunch = 23;

		// Token: 0x04001935 RID: 6453
		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugTime")]
		public const int DebugTime = 24;

		// Token: 0x04001936 RID: 6454
		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugToggleCamera")]
		public const int DebugToggleCamera = 25;

		// Token: 0x04001937 RID: 6455
		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugCameraHorizontal")]
		public const int DebugCameraHorizontal = 26;

		// Token: 0x04001938 RID: 6456
		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugCameraForward")]
		public const int DebugCameraForward = 27;

		// Token: 0x04001939 RID: 6457
		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugCameraVertical")]
		public const int DebugCameraVertical = 28;

		// Token: 0x0400193A RID: 6458
		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugCameraSpeed")]
		public const int DebugCameraSpeed = 29;

		// Token: 0x0400193B RID: 6459
		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugToggleUI")]
		public const int DebugToggleUI = 30;

		// Token: 0x0400193C RID: 6460
		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "Progress State")]
		public const int DebugProgressState = 32;

		// Token: 0x0400193D RID: 6461
		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "Toggle Save")]
		public const int DebugToggleSave = 34;

		// Token: 0x0400193E RID: 6462
		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "Toggle Debug Info")]
		public const int DebugToggleInfo = 43;

		// Token: 0x0400193F RID: 6463
		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "Proof Reading Mode")]
		public const int DebugProofRead = 44;
	}
}
