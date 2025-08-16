using System;
using Rewired.Dev;

namespace RewiredConsts
{
	public static class Action
	{
		[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Move Horizontal")]
		public const int Move_Horizontal = 0;

		[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Move Vertical")]
		public const int Move_Vertical = 1;

		[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Look Vertical")]
		public const int Look_Vertical = 7;

		[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Look Horizontal")]
		public const int Look_Horizontal = 8;

		[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Jump")]
		public const int Jump = 2;

		[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Primary")]
		public const int Primary = 10;

		[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Secondary")]
		public const int Secondary = 11;

		[ActionIdFieldInfo(categoryName = "Default", friendlyName = "UseItem")]
		public const int UseItem = 12;

		[ActionIdFieldInfo(categoryName = "Default", friendlyName = "UseItemR")]
		public const int UseItemR = 42;

		[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Interact")]
		public const int Interact = 9;

		[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Inventory")]
		public const int Inventory = 36;

		[ActionIdFieldInfo(categoryName = "Default", friendlyName = "Skip Dialogue")]
		public const int SkipDialogue = 45;

		[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UIHorizontal")]
		public const int UIHorizontal = 3;

		[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UIVertical")]
		public const int UIVertical = 4;

		[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UI Scroll")]
		public const int UIScroll = 33;

		[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UISubmit")]
		public const int UISubmit = 5;

		[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UISubmitInput")]
		public const int UISubmitInput = 31;

		[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UICancel")]
		public const int UICancel = 6;

		[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UITabRight")]
		public const int UITabRight = 16;

		[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UITabLeft")]
		public const int UITabLeft = 17;

		[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UISubmit Button Prompt")]
		public const int UISubmitPrompt = 38;

		[ActionIdFieldInfo(categoryName = "UI", friendlyName = "UICancel Button Prompt")]
		public const int UICancelPrompt = 39;

		[ActionIdFieldInfo(categoryName = "UI", friendlyName = "Interact Dialogue")]
		public const int Interact_Dialogue = 41;

		[ActionIdFieldInfo(categoryName = "Menu", friendlyName = "Pause")]
		public const int Pause = 37;

		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugBracelet")]
		public const int DebugBracelet = 18;

		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugItems")]
		public const int DebugItems = 19;

		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugToggle")]
		public const int DebugToggle = 20;

		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugSkip")]
		public const int DebugSkip = 21;

		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugToggleHide")]
		public const int DebugToggleHide = 22;

		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugLaunch")]
		public const int DebugLaunch = 23;

		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugTime")]
		public const int DebugTime = 24;

		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugToggleCamera")]
		public const int DebugToggleCamera = 25;

		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugCameraHorizontal")]
		public const int DebugCameraHorizontal = 26;

		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugCameraForward")]
		public const int DebugCameraForward = 27;

		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugCameraVertical")]
		public const int DebugCameraVertical = 28;

		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugCameraSpeed")]
		public const int DebugCameraSpeed = 29;

		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "DebugToggleUI")]
		public const int DebugToggleUI = 30;

		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "Progress State")]
		public const int DebugProgressState = 32;

		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "Toggle Save")]
		public const int DebugToggleSave = 34;

		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "Toggle Debug Info")]
		public const int DebugToggleInfo = 43;

		[ActionIdFieldInfo(categoryName = "Debug", friendlyName = "Proof Reading Mode")]
		public const int DebugProofRead = 44;
	}
}
