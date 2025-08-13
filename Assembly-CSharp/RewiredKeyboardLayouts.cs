using System;
using System.Runtime.InteropServices;
using Rewired;
using UnityEngine;

// Token: 0x02000230 RID: 560
public class RewiredKeyboardLayouts : MonoBehaviour
{
	// Token: 0x06000C13 RID: 3091 RVA: 0x00039870 File Offset: 0x00037A70
	private void Start()
	{
		this.player = ReInput.players.GetPlayer(0);
		this._currentLayout = this.GetCurrentKeyboardLayout();
		this.UpdateKeyboardLayouts(this._currentLayout);
		base.InvokeRepeating("CheckKeyboardLayout", 1f, 1f);
	}

	// Token: 0x06000C14 RID: 3092 RVA: 0x000398B0 File Offset: 0x00037AB0
	private void OnDestroy()
	{
	}

	// Token: 0x06000C15 RID: 3093 RVA: 0x000398B2 File Offset: 0x00037AB2
	[ContextMenu("Force Update")]
	public void ForceUpdate()
	{
		this.ApplyKeyboardLayouts();
	}

	// Token: 0x06000C16 RID: 3094 RVA: 0x000398BC File Offset: 0x00037ABC
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

	// Token: 0x06000C17 RID: 3095 RVA: 0x0003993C File Offset: 0x00037B3C
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

	// Token: 0x06000C18 RID: 3096
	[DllImport("user32.dll")]
	private static extern IntPtr GetForegroundWindow();

	// Token: 0x06000C19 RID: 3097
	[DllImport("user32.dll")]
	private static extern uint GetWindowThreadProcessId(IntPtr hwnd, IntPtr proccess);

	// Token: 0x06000C1A RID: 3098
	[DllImport("user32.dll")]
	private static extern IntPtr GetKeyboardLayout(uint thread);

	// Token: 0x06000C1B RID: 3099 RVA: 0x00039AB0 File Offset: 0x00037CB0
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

	// Token: 0x06000C1C RID: 3100 RVA: 0x00039B08 File Offset: 0x00037D08
	private void CheckKeyboardLayout()
	{
		int currentKeyboardLayout = this.GetCurrentKeyboardLayout();
		if (this._currentLayout != currentKeyboardLayout)
		{
			this.UpdateKeyboardLayouts(currentKeyboardLayout);
			this._currentLayout = currentKeyboardLayout;
		}
	}

	// Token: 0x04000FDD RID: 4061
	private global::Rewired.Player player;

	// Token: 0x04000FDE RID: 4062
	public RewiredKeyboardLayouts.KeyboardLayout[] layouts;

	// Token: 0x04000FDF RID: 4063
	private int _currentLayout = 1033;

	// Token: 0x02000419 RID: 1049
	[Serializable]
	public struct KeyboardLayout
	{
		// Token: 0x04001D1F RID: 7455
		public string tag;

		// Token: 0x04001D20 RID: 7456
		public bool isFallback;

		// Token: 0x04001D21 RID: 7457
		public int priority;

		// Token: 0x04001D22 RID: 7458
		public int[] keyboardIDs;

		// Token: 0x04001D23 RID: 7459
		public bool hasKeyboard;
	}
}
