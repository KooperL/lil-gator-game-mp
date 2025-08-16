using System;
using System.Runtime.InteropServices;
using Rewired;
using UnityEngine;

public class RewiredKeyboardLayouts : MonoBehaviour
{
	// Token: 0x06000F0A RID: 3850 RVA: 0x0000D16D File Offset: 0x0000B36D
	private void Start()
	{
		this.player = ReInput.players.GetPlayer(0);
		this._currentLayout = this.GetCurrentKeyboardLayout();
		this.UpdateKeyboardLayouts(this._currentLayout);
		base.InvokeRepeating("CheckKeyboardLayout", 1f, 1f);
	}

	// Token: 0x06000F0B RID: 3851 RVA: 0x00002229 File Offset: 0x00000429
	private void OnDestroy()
	{
	}

	// Token: 0x06000F0C RID: 3852 RVA: 0x0000D1AD File Offset: 0x0000B3AD
	[ContextMenu("Force Update")]
	public void ForceUpdate()
	{
		this.ApplyKeyboardLayouts();
	}

	// Token: 0x06000F0D RID: 3853 RVA: 0x0004EA7C File Offset: 0x0004CC7C
	private void UpdateKeyboardLayouts(int keyboardID)
	{
		Debug.Log(keyboardID);
		for (int i = 0; i < this.layouts.Length; i++)
		{
			this.layouts[i].hasKeyboard = false;
			int[] keyboardIDs = this.layouts[i].keyboardIDs;
			for (int j = 0; j < keyboardIDs.Length; j++)
			{
				if (keyboardIDs[j] == keyboardID)
				{
					this.layouts[i].hasKeyboard = true;
					break;
				}
			}
		}
		this.ApplyKeyboardLayouts();
	}

	// Token: 0x06000F0E RID: 3854 RVA: 0x0004EAFC File Offset: 0x0004CCFC
	private void ApplyKeyboardLayouts()
	{
		int num = -1;
		int num2 = -1;
		for (int i = 0; i < this.layouts.Length; i++)
		{
			RewiredKeyboardLayouts.KeyboardLayout keyboardLayout = this.layouts[i];
			if (keyboardLayout.hasKeyboard && keyboardLayout.priority > num)
			{
				num = keyboardLayout.priority;
				num2 = i;
			}
		}
		if (num == -1)
		{
			for (int j = 0; j < this.layouts.Length; j++)
			{
				RewiredKeyboardLayouts.KeyboardLayout keyboardLayout2 = this.layouts[j];
				if (keyboardLayout2.isFallback && keyboardLayout2.priority > num)
				{
					num = keyboardLayout2.priority;
					num2 = j;
				}
			}
		}
		if (num == -1)
		{
			num2 = 0;
		}
		string currentActiveTag = this.layouts[num2].tag;
		foreach (ControllerMapLayoutManager.RuleSet ruleSet in this.player.controllers.maps.layoutManager.ruleSets)
		{
			ruleSet.enabled = false;
		}
		Debug.Log("Current active tag: " + currentActiveTag);
		this.player.controllers.maps.layoutManager.ruleSets.Find((ControllerMapLayoutManager.RuleSet item) => item.tag == currentActiveTag).enabled = true;
		this.player.controllers.maps.layoutManager.Apply();
	}

	[DllImport("user32.dll")]
	private static extern IntPtr GetForegroundWindow();

	[DllImport("user32.dll")]
	private static extern uint GetWindowThreadProcessId(IntPtr hwnd, IntPtr proccess);

	[DllImport("user32.dll")]
	private static extern IntPtr GetKeyboardLayout(uint thread);

	// Token: 0x06000F12 RID: 3858 RVA: 0x0004EC70 File Offset: 0x0004CE70
	public int GetCurrentKeyboardLayout()
	{
		int num2;
		try
		{
			int num = RewiredKeyboardLayouts.GetKeyboardLayout(RewiredKeyboardLayouts.GetWindowThreadProcessId(RewiredKeyboardLayouts.GetForegroundWindow(), IntPtr.Zero)).ToInt32() & 65535;
			if (num == 0)
			{
				num = 1033;
			}
			num2 = num;
		}
		catch (Exception)
		{
			num2 = 1033;
		}
		return num2;
	}

	// Token: 0x06000F13 RID: 3859 RVA: 0x0004ECC8 File Offset: 0x0004CEC8
	private void CheckKeyboardLayout()
	{
		int currentKeyboardLayout = this.GetCurrentKeyboardLayout();
		if (this._currentLayout != currentKeyboardLayout)
		{
			this.UpdateKeyboardLayouts(currentKeyboardLayout);
			this._currentLayout = currentKeyboardLayout;
		}
	}

	private global::Rewired.Player player;

	public RewiredKeyboardLayouts.KeyboardLayout[] layouts;

	private int _currentLayout = 1033;

	[Serializable]
	public struct KeyboardLayout
	{
		public string tag;

		public bool isFallback;

		public int priority;

		public int[] keyboardIDs;

		public bool hasKeyboard;
	}
}
