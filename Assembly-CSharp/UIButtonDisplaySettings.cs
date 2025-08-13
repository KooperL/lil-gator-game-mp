using System;
using System.Collections.Generic;
using Rewired;
using Rewired.Data.Mapping;
using RewiredConsts;
using Steamworks;
using UnityEngine;

// Token: 0x02000397 RID: 919
[CreateAssetMenu(menuName = "Rewired/Button Display Settings")]
public class UIButtonDisplaySettings : ScriptableObject
{
	// Token: 0x06001173 RID: 4467 RVA: 0x000575EC File Offset: 0x000557EC
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

	// Token: 0x06001174 RID: 4468 RVA: 0x000576B4 File Offset: 0x000558B4
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

	// Token: 0x06001175 RID: 4469 RVA: 0x000576F8 File Offset: 0x000558F8
	public GameObject GetDisplay(ActionElementMap map, bool showUnbound = false)
	{
		if (map == null)
		{
			if (showUnbound)
			{
				return this.GenerateTextButton(this.document.FetchString(this.unboundText, Language.English), 2);
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
							goto IL_014F;
						}
					}
					UIButtonDisplaySettings.ControllerEntry controllerEntry = this.GetControllerEntry(guid);
					if (controllerEntry != null)
					{
						sprite = controllerEntry.GetGlyph(map.elementIdentifierId, map.axisRange);
						if (sprite != null)
						{
							goto IL_014F;
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
			IL_014F:
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

	// Token: 0x06001176 RID: 4470 RVA: 0x0000EF72 File Offset: 0x0000D172
	private GameObject GenerateButtonSprite(Sprite sprite)
	{
		ButtonDisplayTemplate component = Object.Instantiate<GameObject>(this.spriteButtonTemplate).GetComponent<ButtonDisplayTemplate>();
		component.image.sprite = sprite;
		component.image.SetNativeSize();
		return component.gameObject;
	}

	// Token: 0x06001177 RID: 4471 RVA: 0x00057890 File Offset: 0x00055A90
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
		ButtonDisplayTemplate component = Object.Instantiate<GameObject>(gameObject).GetComponent<ButtonDisplayTemplate>();
		component.nameText.text = name;
		return component.gameObject;
	}

	// Token: 0x06001178 RID: 4472 RVA: 0x0000EFA0 File Offset: 0x0000D1A0
	private GameObject GenerateModifiedTextButton(string name, string[] modifiers)
	{
		ButtonDisplayModifierTemplate component = Object.Instantiate<GameObject>(this.keyWithModifierTemplate).GetComponent<ButtonDisplayModifierTemplate>();
		component.LoadModifierAndText(modifiers, name);
		return component.gameObject;
	}

	// Token: 0x06001179 RID: 4473 RVA: 0x000578DC File Offset: 0x00055ADC
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

	// Token: 0x0600117A RID: 4474 RVA: 0x00057940 File Offset: 0x00055B40
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

	// Token: 0x0600117B RID: 4475 RVA: 0x000579A4 File Offset: 0x00055BA4
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

	// Token: 0x0600117C RID: 4476 RVA: 0x000579F8 File Offset: 0x00055BF8
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
			name = this.document.FetchString(name, Language.English);
		}
	}

	// Token: 0x0600117D RID: 4477 RVA: 0x00057A80 File Offset: 0x00055C80
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

	// Token: 0x0600117E RID: 4478 RVA: 0x0000EFBF File Offset: 0x0000D1BF
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

	// Token: 0x0600117F RID: 4479 RVA: 0x00057AFC File Offset: 0x00055CFC
	public string[] ModifierKeyFlagsToString(ModifierKeyFlags flags)
	{
		int num = 0;
		string empty = string.Empty;
		List<string> list = new List<string>();
		if (Keyboard.ModifierKeyFlagsContain(flags, 1))
		{
			list.Add(this.document.FetchString("Control", Language.English));
			num++;
		}
		if (Keyboard.ModifierKeyFlagsContain(flags, 4))
		{
			list.Add(this.document.FetchString("Command", Language.English));
			num++;
		}
		if (Keyboard.ModifierKeyFlagsContain(flags, 2))
		{
			list.Add(this.document.FetchString("Alt", Language.English));
			num++;
		}
		if (Keyboard.ModifierKeyFlagsContain(flags, 3))
		{
			list.Add(this.document.FetchString("Shift", Language.English));
			num++;
		}
		return list.ToArray();
	}

	// Token: 0x04001680 RID: 5760
	public MultilingualTextDocument document;

	// Token: 0x04001681 RID: 5761
	[SerializeField]
	private UIButtonDisplaySettings.GlyphEntry[] mouseGlyphs;

	// Token: 0x04001682 RID: 5762
	[SerializeField]
	private UIButtonDisplaySettings.ControllerEntry[] controllers;

	// Token: 0x04001683 RID: 5763
	[SerializeField]
	private UIButtonDisplaySettings.ControllerTemplateEntry[] templates;

	// Token: 0x04001684 RID: 5764
	[Space]
	public HardwareJoystickMap steamOverrideController;

	// Token: 0x04001685 RID: 5765
	[SerializeField]
	private UIButtonDisplaySettings.ControllerEntry steamController;

	// Token: 0x04001686 RID: 5766
	[SerializeField]
	private UIButtonDisplaySettings.ControllerEntry steamDeckController;

	// Token: 0x04001687 RID: 5767
	public GameObject spriteButtonTemplate;

	// Token: 0x04001688 RID: 5768
	public GameObject keyTemplate;

	// Token: 0x04001689 RID: 5769
	public GameObject keyWithModifierTemplate;

	// Token: 0x0400168A RID: 5770
	public GameObject buttonTemplate;

	// Token: 0x0400168B RID: 5771
	public GameObject genericTemplate;

	// Token: 0x0400168C RID: 5772
	[TextLookup("document")]
	public string unboundText;

	// Token: 0x0400168D RID: 5773
	private List<ControllerTemplateElementTarget> _tempTemplateElementTargets = new List<ControllerTemplateElementTarget>();

	// Token: 0x0400168E RID: 5774
	[ActionIdProperty(typeof(global::RewiredConsts.Action))]
	public int[] ignoreKeyboardActions;

	// Token: 0x02000398 RID: 920
	[Serializable]
	private class GlyphEntry
	{
		// Token: 0x06001181 RID: 4481 RVA: 0x00057BB0 File Offset: 0x00055DB0
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

		// Token: 0x0400168F RID: 5775
		public int elementIdentifierId;

		// Token: 0x04001690 RID: 5776
		public Sprite glyph;

		// Token: 0x04001691 RID: 5777
		public Sprite glyphPos;

		// Token: 0x04001692 RID: 5778
		public Sprite glyphNeg;
	}

	// Token: 0x02000399 RID: 921
	[Serializable]
	private class ControllerEntry
	{
		// Token: 0x06001183 RID: 4483 RVA: 0x00057C14 File Offset: 0x00055E14
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

		// Token: 0x04001693 RID: 5779
		public string name;

		// Token: 0x04001694 RID: 5780
		public HardwareJoystickMap joystick;

		// Token: 0x04001695 RID: 5781
		public UIButtonDisplaySettings.GlyphEntry[] glyphs;
	}

	// Token: 0x0200039A RID: 922
	[Serializable]
	private class ControllerTemplateEntry
	{
		// Token: 0x06001185 RID: 4485 RVA: 0x00057C68 File Offset: 0x00055E68
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

		// Token: 0x04001696 RID: 5782
		public string name;

		// Token: 0x04001697 RID: 5783
		public HardwareControllerTemplateMap template;

		// Token: 0x04001698 RID: 5784
		public UIButtonDisplaySettings.GlyphEntry[] glyphs;
	}
}
