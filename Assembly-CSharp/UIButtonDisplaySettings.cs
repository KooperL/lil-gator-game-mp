using System;
using System.Collections.Generic;
using Rewired;
using Rewired.Data.Mapping;
using RewiredConsts;
using Steamworks;
using UnityEngine;

[CreateAssetMenu(menuName = "Rewired/Button Display Settings")]
public class UIButtonDisplaySettings : ScriptableObject
{
	// Token: 0x06000EA1 RID: 3745 RVA: 0x0004601C File Offset: 0x0004421C
	public GameObject GetDisplay(InputAction action)
	{
		global::Rewired.Player player = ReInput.players.GetPlayer(0);
		ControllerType controllerType = InputHelper.lastActiveControllerType;
		if (controllerType != ControllerType.Keyboard)
		{
			if (controllerType == ControllerType.Mouse)
			{
				if (player.controllers.maps.GetFirstElementMapWithAction(controllerType, action.id, true) == null && player.controllers.hasKeyboard)
				{
					controllerType = ControllerType.Keyboard;
				}
			}
		}
		else
		{
			foreach (int num in this.ignoreKeyboardActions)
			{
				if (action.id == num)
				{
					controllerType = ControllerType.Mouse;
					break;
				}
			}
			if (player.controllers.maps.GetFirstElementMapWithAction(controllerType, action.id, true) == null && player.controllers.hasMouse)
			{
				controllerType = ControllerType.Mouse;
			}
		}
		return this.GetDisplay(player.controllers.maps.GetFirstElementMapWithAction(controllerType, InputHelper.lastActiveControllerID, action.id, true), true);
	}

	// Token: 0x06000EA2 RID: 3746 RVA: 0x000460E4 File Offset: 0x000442E4
	public GameObject GetDisplay(InputAction action, Controller controller)
	{
		global::Rewired.Player player = ReInput.players.GetPlayer(0);
		if (controller == null)
		{
			return null;
		}
		ActionElementMap firstElementMapWithAction = player.controllers.maps.GetFirstElementMapWithAction(controller, action.id, true);
		if (firstElementMapWithAction == null)
		{
			return null;
		}
		return this.GetDisplay(firstElementMapWithAction, true);
	}

	// Token: 0x06000EA3 RID: 3747 RVA: 0x00046128 File Offset: 0x00044328
	public GameObject GetDisplay(ActionElementMap map, bool showUnbound = false)
	{
		if (map == null)
		{
			if (showUnbound)
			{
				return this.GenerateTextButton(this.document.FetchString(this.unboundText, Language.Auto), ControllerType.Joystick);
			}
			return null;
		}
		else
		{
			Sprite sprite = null;
			Controller controller = map.controllerMap.controller;
			ControllerType controllerType = map.controllerMap.controllerType;
			Guid guid = default(Guid);
			if (controllerType == ControllerType.Joystick)
			{
				guid = (controller as Joystick).hardwareTypeGuid;
			}
			if (controllerType != ControllerType.Mouse)
			{
				if (controllerType == ControllerType.Joystick)
				{
					if (SteamManager.Initialized && SteamUtils.IsSteamRunningOnSteamDeck() && this.steamOverrideController.Guid == guid)
					{
						sprite = this.steamDeckController.GetGlyph(map.elementIdentifierId, map.axisRange);
						if (sprite != null)
						{
							goto IL_0150;
						}
					}
					UIButtonDisplaySettings.ControllerEntry controllerEntry = this.GetControllerEntry(guid);
					if (controllerEntry != null)
					{
						sprite = controllerEntry.GetGlyph(map.elementIdentifierId, map.axisRange);
						if (sprite != null)
						{
							goto IL_0150;
						}
					}
					if (controller.templateCount != 0)
					{
						IControllerTemplate controllerTemplate = controller.Templates[0];
						if (controllerTemplate.GetElementTargets(map, this._tempTemplateElementTargets) != 0)
						{
							ControllerTemplateElementTarget controllerTemplateElementTarget = this._tempTemplateElementTargets[0];
							UIButtonDisplaySettings.ControllerTemplateEntry controllerTemplateEntry = this.GetControllerTemplateEntry(controllerTemplate.typeGuid);
							if (controllerTemplateEntry != null)
							{
								sprite = controllerTemplateEntry.GetGlyph(controllerTemplateElementTarget.element.id, controllerTemplateElementTarget.axisRange);
							}
						}
					}
				}
			}
			else
			{
				sprite = this.GetMouseGlyph(map.elementIdentifierId, map.axisRange);
			}
			IL_0150:
			if (sprite != null)
			{
				return this.GenerateButtonSprite(sprite);
			}
			string text;
			string[] array;
			this.GetElementIdentifierName(map, out text, out array);
			if (array != null)
			{
				return this.GenerateModifiedTextButton(text, array);
			}
			return this.GenerateTextButton(text, controller.type);
		}
	}

	// Token: 0x06000EA4 RID: 3748 RVA: 0x000462BE File Offset: 0x000444BE
	private GameObject GenerateButtonSprite(Sprite sprite)
	{
		ButtonDisplayTemplate component = Object.Instantiate<GameObject>(this.spriteButtonTemplate).GetComponent<ButtonDisplayTemplate>();
		component.image.sprite = sprite;
		component.image.SetNativeSize();
		return component.gameObject;
	}

	// Token: 0x06000EA5 RID: 3749 RVA: 0x000462EC File Offset: 0x000444EC
	private GameObject GenerateTextButton(string name, ControllerType controllerType)
	{
		GameObject gameObject;
		if (controllerType != ControllerType.Keyboard)
		{
			if (controllerType == ControllerType.Joystick)
			{
				gameObject = this.buttonTemplate;
			}
			else
			{
				gameObject = this.genericTemplate;
			}
		}
		else
		{
			gameObject = this.keyTemplate;
		}
		ButtonDisplayTemplate component = Object.Instantiate<GameObject>(gameObject).GetComponent<ButtonDisplayTemplate>();
		component.nameText.text = name;
		return component.gameObject;
	}

	// Token: 0x06000EA6 RID: 3750 RVA: 0x00046337 File Offset: 0x00044537
	private GameObject GenerateModifiedTextButton(string name, string[] modifiers)
	{
		ButtonDisplayModifierTemplate component = Object.Instantiate<GameObject>(this.keyWithModifierTemplate).GetComponent<ButtonDisplayModifierTemplate>();
		component.LoadModifierAndText(modifiers, name);
		return component.gameObject;
	}

	// Token: 0x06000EA7 RID: 3751 RVA: 0x00046358 File Offset: 0x00044558
	private UIButtonDisplaySettings.ControllerEntry GetControllerEntry(Guid joystickGuid)
	{
		for (int i = 0; i < this.controllers.Length; i++)
		{
			if (this.controllers[i] != null && !(this.controllers[i].joystick == null) && !(this.controllers[i].joystick.Guid != joystickGuid))
			{
				return this.controllers[i];
			}
		}
		return null;
	}

	// Token: 0x06000EA8 RID: 3752 RVA: 0x000463BC File Offset: 0x000445BC
	private UIButtonDisplaySettings.ControllerTemplateEntry GetControllerTemplateEntry(Guid templateGuid)
	{
		for (int i = 0; i < this.templates.Length; i++)
		{
			if (this.templates[i] != null && !(this.templates[i].template == null) && !(this.templates[i].template.Guid != templateGuid))
			{
				return this.templates[i];
			}
		}
		return null;
	}

	// Token: 0x06000EA9 RID: 3753 RVA: 0x00046420 File Offset: 0x00044620
	private Sprite GetMouseGlyph(int elementIdentifierId, AxisRange axisRange)
	{
		if (this.mouseGlyphs == null)
		{
			return null;
		}
		for (int i = 0; i < this.mouseGlyphs.Length; i++)
		{
			if (this.mouseGlyphs[i] != null && this.mouseGlyphs[i].elementIdentifierId == elementIdentifierId)
			{
				return this.mouseGlyphs[i].GetGlyph(axisRange);
			}
		}
		return null;
	}

	// Token: 0x06000EAA RID: 3754 RVA: 0x00046474 File Offset: 0x00044674
	public void GetElementIdentifierName(ActionElementMap actionElementMap, out string name, out string[] modifier)
	{
		name = "";
		modifier = null;
		if (actionElementMap == null)
		{
			throw new ArgumentNullException("actionElementMap");
		}
		if (actionElementMap.controllerMap.controllerType == ControllerType.Keyboard)
		{
			this.GetElementIdentifierName(actionElementMap.keyCode, actionElementMap.modifierKeyFlags, out name, out modifier);
		}
		else
		{
			name = this.GetElementIdentifierName(actionElementMap.controllerMap.controller, actionElementMap.elementIdentifierId, actionElementMap.axisRange);
		}
		if (this.document.HasString(name))
		{
			name = this.document.FetchString(name, Language.Auto);
		}
	}

	// Token: 0x06000EAB RID: 3755 RVA: 0x000464FC File Offset: 0x000446FC
	public string GetElementIdentifierName(Controller controller, int elementIdentifierId, AxisRange axisRange)
	{
		if (controller == null)
		{
			throw new ArgumentNullException("controller");
		}
		ControllerElementIdentifier elementIdentifierById = controller.GetElementIdentifierById(elementIdentifierId);
		if (elementIdentifierById == null)
		{
			throw new ArgumentException("Invalid element identifier id: " + elementIdentifierId.ToString());
		}
		Controller.Element elementById = controller.GetElementById(elementIdentifierId);
		if (elementById == null)
		{
			return string.Empty;
		}
		ControllerElementType type = elementById.type;
		if (type == ControllerElementType.Axis)
		{
			return elementIdentifierById.GetDisplayName(elementById.type, axisRange);
		}
		if (type != ControllerElementType.Button)
		{
			return elementIdentifierById.name;
		}
		return elementIdentifierById.name;
	}

	// Token: 0x06000EAC RID: 3756 RVA: 0x00046575 File Offset: 0x00044775
	public void GetElementIdentifierName(KeyCode keyCode, ModifierKeyFlags modifierKeyFlags, out string name, out string[] modifier)
	{
		if (modifierKeyFlags != ModifierKeyFlags.None)
		{
			modifier = this.ModifierKeyFlagsToString(modifierKeyFlags);
		}
		else
		{
			modifier = null;
		}
		name = Keyboard.GetKeyName(keyCode);
	}

	// Token: 0x06000EAD RID: 3757 RVA: 0x00046594 File Offset: 0x00044794
	public string[] ModifierKeyFlagsToString(ModifierKeyFlags flags)
	{
		int num = 0;
		string empty = string.Empty;
		List<string> list = new List<string>();
		if (Keyboard.ModifierKeyFlagsContain(flags, ModifierKey.Control))
		{
			list.Add(this.document.FetchString("Control", Language.Auto));
			num++;
		}
		if (Keyboard.ModifierKeyFlagsContain(flags, ModifierKey.Command))
		{
			list.Add(this.document.FetchString("Command", Language.Auto));
			num++;
		}
		if (Keyboard.ModifierKeyFlagsContain(flags, ModifierKey.Alt))
		{
			list.Add(this.document.FetchString("Alt", Language.Auto));
			num++;
		}
		if (Keyboard.ModifierKeyFlagsContain(flags, ModifierKey.Shift))
		{
			list.Add(this.document.FetchString("Shift", Language.Auto));
			num++;
		}
		return list.ToArray();
	}

	public MultilingualTextDocument document;

	[SerializeField]
	private UIButtonDisplaySettings.GlyphEntry[] mouseGlyphs;

	[SerializeField]
	private UIButtonDisplaySettings.ControllerEntry[] controllers;

	[SerializeField]
	private UIButtonDisplaySettings.ControllerTemplateEntry[] templates;

	[Space]
	public HardwareJoystickMap steamOverrideController;

	[SerializeField]
	private UIButtonDisplaySettings.ControllerEntry steamController;

	[SerializeField]
	private UIButtonDisplaySettings.ControllerEntry steamDeckController;

	public GameObject spriteButtonTemplate;

	public GameObject keyTemplate;

	public GameObject keyWithModifierTemplate;

	public GameObject buttonTemplate;

	public GameObject genericTemplate;

	[TextLookup("document")]
	public string unboundText;

	private List<ControllerTemplateElementTarget> _tempTemplateElementTargets = new List<ControllerTemplateElementTarget>();

	[ActionIdProperty(typeof(Action))]
	public int[] ignoreKeyboardActions;

	[Serializable]
	private class GlyphEntry
	{
		// Token: 0x06001B25 RID: 6949 RVA: 0x000731A4 File Offset: 0x000713A4
		public Sprite GetGlyph(AxisRange axisRange)
		{
			switch (axisRange)
			{
			case AxisRange.Full:
				return this.glyph;
			case AxisRange.Positive:
				if (!(this.glyphPos != null))
				{
					return this.glyph;
				}
				return this.glyphPos;
			case AxisRange.Negative:
				if (!(this.glyphNeg != null))
				{
					return this.glyph;
				}
				return this.glyphNeg;
			default:
				return null;
			}
		}

		public int elementIdentifierId;

		public Sprite glyph;

		public Sprite glyphPos;

		public Sprite glyphNeg;
	}

	[Serializable]
	private class ControllerEntry
	{
		// Token: 0x06001B27 RID: 6951 RVA: 0x00073210 File Offset: 0x00071410
		public Sprite GetGlyph(int elementIdentifierId, AxisRange axisRange)
		{
			if (this.glyphs == null)
			{
				return null;
			}
			for (int i = 0; i < this.glyphs.Length; i++)
			{
				if (this.glyphs[i] != null && this.glyphs[i].elementIdentifierId == elementIdentifierId)
				{
					return this.glyphs[i].GetGlyph(axisRange);
				}
			}
			return null;
		}

		public string name;

		public HardwareJoystickMap joystick;

		public UIButtonDisplaySettings.GlyphEntry[] glyphs;
	}

	[Serializable]
	private class ControllerTemplateEntry
	{
		// Token: 0x06001B29 RID: 6953 RVA: 0x0007326C File Offset: 0x0007146C
		public Sprite GetGlyph(int elementIdentifierId, AxisRange axisRange)
		{
			if (this.glyphs == null)
			{
				return null;
			}
			for (int i = 0; i < this.glyphs.Length; i++)
			{
				if (this.glyphs[i] != null && this.glyphs[i].elementIdentifierId == elementIdentifierId)
				{
					return this.glyphs[i].GetGlyph(axisRange);
				}
			}
			return null;
		}

		public string name;

		public HardwareControllerTemplateMap template;

		public UIButtonDisplaySettings.GlyphEntry[] glyphs;
	}
}
