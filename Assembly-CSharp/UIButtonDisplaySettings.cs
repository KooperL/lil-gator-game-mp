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
	// Token: 0x060011D3 RID: 4563 RVA: 0x000595AC File Offset: 0x000577AC
	public GameObject GetDisplay(InputAction action)
	{
		global::Rewired.Player player = ReInput.players.GetPlayer(0);
		ControllerType controllerType = InputHelper.lastActiveControllerType;
		if (controllerType != null)
		{
			if (controllerType == 1)
			{
				if (player.controllers.maps.GetFirstElementMapWithAction(controllerType, action.id, true) == null && player.controllers.hasKeyboard)
				{
					controllerType = 0;
				}
			}
		}
		else
		{
			foreach (int num in this.ignoreKeyboardActions)
			{
				if (action.id == num)
				{
					controllerType = 1;
					break;
				}
			}
			if (player.controllers.maps.GetFirstElementMapWithAction(controllerType, action.id, true) == null && player.controllers.hasMouse)
			{
				controllerType = 1;
			}
		}
		return this.GetDisplay(player.controllers.maps.GetFirstElementMapWithAction(controllerType, InputHelper.lastActiveControllerID, action.id, true), true);
	}

	// Token: 0x060011D4 RID: 4564 RVA: 0x00059674 File Offset: 0x00057874
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

	// Token: 0x060011D5 RID: 4565 RVA: 0x000596B8 File Offset: 0x000578B8
	public GameObject GetDisplay(ActionElementMap map, bool showUnbound = false)
	{
		if (map == null)
		{
			if (showUnbound)
			{
				return this.GenerateTextButton(this.document.FetchString(this.unboundText, Language.Auto), 2);
			}
			return null;
		}
		else
		{
			Sprite sprite = null;
			Controller controller = map.controllerMap.controller;
			ControllerType controllerType = map.controllerMap.controllerType;
			Guid guid = default(Guid);
			if (controllerType == 2)
			{
				guid = (controller as Joystick).hardwareTypeGuid;
			}
			if (controllerType != 1)
			{
				if (controllerType == 2)
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

	// Token: 0x060011D6 RID: 4566 RVA: 0x0000F35B File Offset: 0x0000D55B
	private GameObject GenerateButtonSprite(Sprite sprite)
	{
		ButtonDisplayTemplate component = global::UnityEngine.Object.Instantiate<GameObject>(this.spriteButtonTemplate).GetComponent<ButtonDisplayTemplate>();
		component.image.sprite = sprite;
		component.image.SetNativeSize();
		return component.gameObject;
	}

	// Token: 0x060011D7 RID: 4567 RVA: 0x00059850 File Offset: 0x00057A50
	private GameObject GenerateTextButton(string name, ControllerType controllerType)
	{
		GameObject gameObject;
		if (controllerType != null)
		{
			if (controllerType == 2)
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
		ButtonDisplayTemplate component = global::UnityEngine.Object.Instantiate<GameObject>(gameObject).GetComponent<ButtonDisplayTemplate>();
		component.nameText.text = name;
		return component.gameObject;
	}

	// Token: 0x060011D8 RID: 4568 RVA: 0x0000F389 File Offset: 0x0000D589
	private GameObject GenerateModifiedTextButton(string name, string[] modifiers)
	{
		ButtonDisplayModifierTemplate component = global::UnityEngine.Object.Instantiate<GameObject>(this.keyWithModifierTemplate).GetComponent<ButtonDisplayModifierTemplate>();
		component.LoadModifierAndText(modifiers, name);
		return component.gameObject;
	}

	// Token: 0x060011D9 RID: 4569 RVA: 0x0005989C File Offset: 0x00057A9C
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

	// Token: 0x060011DA RID: 4570 RVA: 0x00059900 File Offset: 0x00057B00
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

	// Token: 0x060011DB RID: 4571 RVA: 0x00059964 File Offset: 0x00057B64
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

	// Token: 0x060011DC RID: 4572 RVA: 0x000599B8 File Offset: 0x00057BB8
	public void GetElementIdentifierName(ActionElementMap actionElementMap, out string name, out string[] modifier)
	{
		name = "";
		modifier = null;
		if (actionElementMap == null)
		{
			throw new ArgumentNullException("actionElementMap");
		}
		if (actionElementMap.controllerMap.controllerType == null)
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

	// Token: 0x060011DD RID: 4573 RVA: 0x00059A40 File Offset: 0x00057C40
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
		if (type == null)
		{
			return elementIdentifierById.GetDisplayName(elementById.type, axisRange);
		}
		if (type != 1)
		{
			return elementIdentifierById.name;
		}
		return elementIdentifierById.name;
	}

	// Token: 0x060011DE RID: 4574 RVA: 0x0000F3A8 File Offset: 0x0000D5A8
	public void GetElementIdentifierName(KeyCode keyCode, ModifierKeyFlags modifierKeyFlags, out string name, out string[] modifier)
	{
		if (modifierKeyFlags != null)
		{
			modifier = this.ModifierKeyFlagsToString(modifierKeyFlags);
		}
		else
		{
			modifier = null;
		}
		name = Keyboard.GetKeyName(keyCode);
	}

	// Token: 0x060011DF RID: 4575 RVA: 0x00059ABC File Offset: 0x00057CBC
	public string[] ModifierKeyFlagsToString(ModifierKeyFlags flags)
	{
		int num = 0;
		string empty = string.Empty;
		List<string> list = new List<string>();
		if (Keyboard.ModifierKeyFlagsContain(flags, 1))
		{
			list.Add(this.document.FetchString("Control", Language.Auto));
			num++;
		}
		if (Keyboard.ModifierKeyFlagsContain(flags, 4))
		{
			list.Add(this.document.FetchString("Command", Language.Auto));
			num++;
		}
		if (Keyboard.ModifierKeyFlagsContain(flags, 2))
		{
			list.Add(this.document.FetchString("Alt", Language.Auto));
			num++;
		}
		if (Keyboard.ModifierKeyFlagsContain(flags, 3))
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

	[ActionIdProperty(typeof(global::RewiredConsts.Action))]
	public int[] ignoreKeyboardActions;

	[Serializable]
	private class GlyphEntry
	{
		// Token: 0x060011E1 RID: 4577 RVA: 0x00059B74 File Offset: 0x00057D74
		public Sprite GetGlyph(AxisRange axisRange)
		{
			switch (axisRange)
			{
			case 0:
				return this.glyph;
			case 1:
				if (!(this.glyphPos != null))
				{
					return this.glyph;
				}
				return this.glyphPos;
			case 2:
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
		// Token: 0x060011E3 RID: 4579 RVA: 0x00059BD8 File Offset: 0x00057DD8
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
		// Token: 0x060011E5 RID: 4581 RVA: 0x00059C2C File Offset: 0x00057E2C
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
