using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	[Serializable]
	public class ThemeSettings : ScriptableObject
	{
		// Token: 0x06001C28 RID: 7208 RVA: 0x0006FF88 File Offset: 0x0006E188
		public void Apply(ThemedElement.ElementInfo[] elementInfo)
		{
			if (elementInfo == null)
			{
				return;
			}
			for (int i = 0; i < elementInfo.Length; i++)
			{
				if (elementInfo[i] != null)
				{
					this.Apply(elementInfo[i].themeClass, elementInfo[i].component);
				}
			}
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x0006FFC4 File Offset: 0x0006E1C4
		private void Apply(string themeClass, Component component)
		{
			if (component as Selectable != null)
			{
				this.Apply(themeClass, (Selectable)component);
				return;
			}
			if (component as Image != null)
			{
				this.Apply(themeClass, (Image)component);
				return;
			}
			if (component as Text != null)
			{
				this.Apply(themeClass, (Text)component);
				return;
			}
			if (component as UIImageHelper != null)
			{
				this.Apply(themeClass, (UIImageHelper)component);
				return;
			}
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x00070044 File Offset: 0x0006E244
		private void Apply(string themeClass, Selectable item)
		{
			if (item == null)
			{
				return;
			}
			ThemeSettings.SelectableSettings_Base selectableSettings_Base;
			if (item as Button != null)
			{
				if (themeClass != null && themeClass == "inputGridField")
				{
					selectableSettings_Base = this._inputGridFieldSettings;
				}
				else
				{
					selectableSettings_Base = this._buttonSettings;
				}
			}
			else if (item as Scrollbar != null)
			{
				selectableSettings_Base = this._scrollbarSettings;
			}
			else if (item as Slider != null)
			{
				selectableSettings_Base = this._sliderSettings;
			}
			else if (item as Toggle != null)
			{
				if (themeClass != null && themeClass == "button")
				{
					selectableSettings_Base = this._buttonSettings;
				}
				else
				{
					selectableSettings_Base = this._selectableSettings;
				}
			}
			else
			{
				selectableSettings_Base = this._selectableSettings;
			}
			selectableSettings_Base.Apply(item);
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x000700F8 File Offset: 0x0006E2F8
		private void Apply(string themeClass, Image item)
		{
			if (item == null)
			{
				return;
			}
			if (themeClass != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(themeClass);
				if (num <= 2822822017U)
				{
					if (num <= 665291243U)
					{
						if (num != 106194061U)
						{
							if (num != 283896133U)
							{
								if (num != 665291243U)
								{
									return;
								}
								if (!(themeClass == "calibrationBackground"))
								{
									return;
								}
								if (this._calibrationBackground != null)
								{
									this._calibrationBackground.CopyTo(item);
									return;
								}
							}
							else
							{
								if (!(themeClass == "popupWindow"))
								{
									return;
								}
								if (this._popupWindowBackground != null)
								{
									this._popupWindowBackground.CopyTo(item);
									return;
								}
							}
						}
						else
						{
							if (!(themeClass == "invertToggleButtonBackground"))
							{
								return;
							}
							if (this._buttonSettings != null)
							{
								this._buttonSettings.imageSettings.CopyTo(item);
							}
						}
					}
					else if (num != 2579191547U)
					{
						if (num != 2601460036U)
						{
							if (num != 2822822017U)
							{
								return;
							}
							if (!(themeClass == "invertToggle"))
							{
								return;
							}
							if (this._invertToggle != null)
							{
								this._invertToggle.CopyTo(item);
								return;
							}
						}
						else
						{
							if (!(themeClass == "area"))
							{
								return;
							}
							if (this._areaBackground != null)
							{
								this._areaBackground.CopyTo(item);
								return;
							}
						}
					}
					else
					{
						if (!(themeClass == "calibrationDeadzone"))
						{
							return;
						}
						if (this._calibrationDeadzone != null)
						{
							this._calibrationDeadzone.CopyTo(item);
							return;
						}
					}
				}
				else if (num <= 3490313510U)
				{
					if (num != 2998767316U)
					{
						if (num != 3338297968U)
						{
							if (num != 3490313510U)
							{
								return;
							}
							if (!(themeClass == "calibrationRawValueMarker"))
							{
								return;
							}
							if (this._calibrationRawValueMarker != null)
							{
								this._calibrationRawValueMarker.CopyTo(item);
								return;
							}
						}
						else
						{
							if (!(themeClass == "calibrationCalibratedZeroMarker"))
							{
								return;
							}
							if (this._calibrationCalibratedZeroMarker != null)
							{
								this._calibrationCalibratedZeroMarker.CopyTo(item);
								return;
							}
						}
					}
					else
					{
						if (!(themeClass == "mainWindow"))
						{
							return;
						}
						if (this._mainWindowBackground != null)
						{
							this._mainWindowBackground.CopyTo(item);
							return;
						}
					}
				}
				else if (num != 3776179782U)
				{
					if (num != 3836396811U)
					{
						if (num != 3911450241U)
						{
							return;
						}
						if (!(themeClass == "invertToggleBackground"))
						{
							return;
						}
						if (this._inputGridFieldSettings != null)
						{
							this._inputGridFieldSettings.imageSettings.CopyTo(item);
							return;
						}
					}
					else
					{
						if (!(themeClass == "calibrationZeroMarker"))
						{
							return;
						}
						if (this._calibrationZeroMarker != null)
						{
							this._calibrationZeroMarker.CopyTo(item);
							return;
						}
					}
				}
				else
				{
					if (!(themeClass == "calibrationValueMarker"))
					{
						return;
					}
					if (this._calibrationValueMarker != null)
					{
						this._calibrationValueMarker.CopyTo(item);
						return;
					}
				}
			}
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x00070390 File Offset: 0x0006E590
		private void Apply(string themeClass, Text item)
		{
			if (item == null)
			{
				return;
			}
			ThemeSettings.TextSettings textSettings;
			if (themeClass != null)
			{
				if (themeClass == "button")
				{
					textSettings = this._buttonTextSettings;
					goto IL_0042;
				}
				if (themeClass == "inputGridField")
				{
					textSettings = this._inputGridFieldTextSettings;
					goto IL_0042;
				}
			}
			textSettings = this._textSettings;
			IL_0042:
			if (textSettings.font != null)
			{
				item.font = textSettings.font;
			}
			item.color = textSettings.color;
			item.lineSpacing = textSettings.lineSpacing;
			if (textSettings.sizeMultiplier != 1f)
			{
				item.fontSize = (int)((float)item.fontSize * textSettings.sizeMultiplier);
				item.resizeTextMaxSize = (int)((float)item.resizeTextMaxSize * textSettings.sizeMultiplier);
				item.resizeTextMinSize = (int)((float)item.resizeTextMinSize * textSettings.sizeMultiplier);
			}
			if (textSettings.style != ThemeSettings.FontStyleOverride.Default)
			{
				item.fontStyle = ThemeSettings.GetFontStyle(textSettings.style);
			}
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x000159CD File Offset: 0x00013BCD
		private void Apply(string themeClass, UIImageHelper item)
		{
			if (item == null)
			{
				return;
			}
			item.SetEnabledStateColor(this._invertToggle.color);
			item.SetDisabledStateColor(this._invertToggleDisabledColor);
			item.Refresh();
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x000159FC File Offset: 0x00013BFC
		private static FontStyle GetFontStyle(ThemeSettings.FontStyleOverride style)
		{
			return (FontStyle)(style - 1);
		}

		[SerializeField]
		private ThemeSettings.ImageSettings _mainWindowBackground;

		[SerializeField]
		private ThemeSettings.ImageSettings _popupWindowBackground;

		[SerializeField]
		private ThemeSettings.ImageSettings _areaBackground;

		[SerializeField]
		private ThemeSettings.SelectableSettings _selectableSettings;

		[SerializeField]
		private ThemeSettings.SelectableSettings _buttonSettings;

		[SerializeField]
		private ThemeSettings.SelectableSettings _inputGridFieldSettings;

		[SerializeField]
		private ThemeSettings.ScrollbarSettings _scrollbarSettings;

		[SerializeField]
		private ThemeSettings.SliderSettings _sliderSettings;

		[SerializeField]
		private ThemeSettings.ImageSettings _invertToggle;

		[SerializeField]
		private Color _invertToggleDisabledColor;

		[SerializeField]
		private ThemeSettings.ImageSettings _calibrationBackground;

		[SerializeField]
		private ThemeSettings.ImageSettings _calibrationValueMarker;

		[SerializeField]
		private ThemeSettings.ImageSettings _calibrationRawValueMarker;

		[SerializeField]
		private ThemeSettings.ImageSettings _calibrationZeroMarker;

		[SerializeField]
		private ThemeSettings.ImageSettings _calibrationCalibratedZeroMarker;

		[SerializeField]
		private ThemeSettings.ImageSettings _calibrationDeadzone;

		[SerializeField]
		private ThemeSettings.TextSettings _textSettings;

		[SerializeField]
		private ThemeSettings.TextSettings _buttonTextSettings;

		[SerializeField]
		private ThemeSettings.TextSettings _inputGridFieldTextSettings;

		[Serializable]
		private abstract class SelectableSettings_Base
		{
			// (get) Token: 0x06001C30 RID: 7216 RVA: 0x00015A01 File Offset: 0x00013C01
			public Selectable.Transition transition
			{
				get
				{
					return this._transition;
				}
			}

			// (get) Token: 0x06001C31 RID: 7217 RVA: 0x00015A09 File Offset: 0x00013C09
			public ThemeSettings.CustomColorBlock selectableColors
			{
				get
				{
					return this._colors;
				}
			}

			// (get) Token: 0x06001C32 RID: 7218 RVA: 0x00015A11 File Offset: 0x00013C11
			public ThemeSettings.CustomSpriteState spriteState
			{
				get
				{
					return this._spriteState;
				}
			}

			// (get) Token: 0x06001C33 RID: 7219 RVA: 0x00015A19 File Offset: 0x00013C19
			public ThemeSettings.CustomAnimationTriggers animationTriggers
			{
				get
				{
					return this._animationTriggers;
				}
			}

			// Token: 0x06001C34 RID: 7220 RVA: 0x00070478 File Offset: 0x0006E678
			public virtual void Apply(Selectable item)
			{
				Selectable.Transition transition = this._transition;
				bool flag = item.transition != transition;
				item.transition = transition;
				ICustomSelectable customSelectable = item as ICustomSelectable;
				if (transition == Selectable.Transition.ColorTint)
				{
					ThemeSettings.CustomColorBlock colors = this._colors;
					colors.fadeDuration = 0f;
					item.colors = colors;
					colors.fadeDuration = this._colors.fadeDuration;
					item.colors = colors;
					if (customSelectable != null)
					{
						customSelectable.disabledHighlightedColor = colors.disabledHighlightedColor;
					}
				}
				else if (transition == Selectable.Transition.SpriteSwap)
				{
					item.spriteState = this._spriteState;
					if (customSelectable != null)
					{
						customSelectable.disabledHighlightedSprite = this._spriteState.disabledHighlightedSprite;
					}
				}
				else if (transition == Selectable.Transition.Animation)
				{
					item.animationTriggers.disabledTrigger = this._animationTriggers.disabledTrigger;
					item.animationTriggers.highlightedTrigger = this._animationTriggers.highlightedTrigger;
					item.animationTriggers.normalTrigger = this._animationTriggers.normalTrigger;
					item.animationTriggers.pressedTrigger = this._animationTriggers.pressedTrigger;
					if (customSelectable != null)
					{
						customSelectable.disabledHighlightedTrigger = this._animationTriggers.disabledHighlightedTrigger;
					}
				}
				if (flag)
				{
					item.targetGraphic.CrossFadeColor(item.targetGraphic.color, 0f, true, true);
				}
			}

			[SerializeField]
			protected Selectable.Transition _transition;

			[SerializeField]
			protected ThemeSettings.CustomColorBlock _colors;

			[SerializeField]
			protected ThemeSettings.CustomSpriteState _spriteState;

			[SerializeField]
			protected ThemeSettings.CustomAnimationTriggers _animationTriggers;
		}

		[Serializable]
		private class SelectableSettings : ThemeSettings.SelectableSettings_Base
		{
			// (get) Token: 0x06001C36 RID: 7222 RVA: 0x00015A21 File Offset: 0x00013C21
			public ThemeSettings.ImageSettings imageSettings
			{
				get
				{
					return this._imageSettings;
				}
			}

			// Token: 0x06001C37 RID: 7223 RVA: 0x00015A29 File Offset: 0x00013C29
			public override void Apply(Selectable item)
			{
				if (item == null)
				{
					return;
				}
				base.Apply(item);
				if (this._imageSettings != null)
				{
					this._imageSettings.CopyTo(item.targetGraphic as Image);
				}
			}

			[SerializeField]
			private ThemeSettings.ImageSettings _imageSettings;
		}

		[Serializable]
		private class SliderSettings : ThemeSettings.SelectableSettings_Base
		{
			// (get) Token: 0x06001C39 RID: 7225 RVA: 0x00015A62 File Offset: 0x00013C62
			public ThemeSettings.ImageSettings handleImageSettings
			{
				get
				{
					return this._handleImageSettings;
				}
			}

			// (get) Token: 0x06001C3A RID: 7226 RVA: 0x00015A6A File Offset: 0x00013C6A
			public ThemeSettings.ImageSettings fillImageSettings
			{
				get
				{
					return this._fillImageSettings;
				}
			}

			// (get) Token: 0x06001C3B RID: 7227 RVA: 0x00015A72 File Offset: 0x00013C72
			public ThemeSettings.ImageSettings backgroundImageSettings
			{
				get
				{
					return this._backgroundImageSettings;
				}
			}

			// Token: 0x06001C3C RID: 7228 RVA: 0x000705BC File Offset: 0x0006E7BC
			private void Apply(Slider item)
			{
				if (item == null)
				{
					return;
				}
				if (this._handleImageSettings != null)
				{
					this._handleImageSettings.CopyTo(item.targetGraphic as Image);
				}
				if (this._fillImageSettings != null)
				{
					RectTransform fillRect = item.fillRect;
					if (fillRect != null)
					{
						this._fillImageSettings.CopyTo(fillRect.GetComponent<Image>());
					}
				}
				if (this._backgroundImageSettings != null)
				{
					Transform transform = item.transform.Find("Background");
					if (transform != null)
					{
						this._backgroundImageSettings.CopyTo(transform.GetComponent<Image>());
					}
				}
			}

			// Token: 0x06001C3D RID: 7229 RVA: 0x00015A7A File Offset: 0x00013C7A
			public override void Apply(Selectable item)
			{
				base.Apply(item);
				this.Apply(item as Slider);
			}

			[SerializeField]
			private ThemeSettings.ImageSettings _handleImageSettings;

			[SerializeField]
			private ThemeSettings.ImageSettings _fillImageSettings;

			[SerializeField]
			private ThemeSettings.ImageSettings _backgroundImageSettings;
		}

		[Serializable]
		private class ScrollbarSettings : ThemeSettings.SelectableSettings_Base
		{
			// (get) Token: 0x06001C3F RID: 7231 RVA: 0x00015A8F File Offset: 0x00013C8F
			public ThemeSettings.ImageSettings handle
			{
				get
				{
					return this._handleImageSettings;
				}
			}

			// (get) Token: 0x06001C40 RID: 7232 RVA: 0x00015A97 File Offset: 0x00013C97
			public ThemeSettings.ImageSettings background
			{
				get
				{
					return this._backgroundImageSettings;
				}
			}

			// Token: 0x06001C41 RID: 7233 RVA: 0x00070650 File Offset: 0x0006E850
			private void Apply(Scrollbar item)
			{
				if (item == null)
				{
					return;
				}
				if (this._handleImageSettings != null)
				{
					this._handleImageSettings.CopyTo(item.targetGraphic as Image);
				}
				if (this._backgroundImageSettings != null)
				{
					this._backgroundImageSettings.CopyTo(item.GetComponent<Image>());
				}
			}

			// Token: 0x06001C42 RID: 7234 RVA: 0x00015A9F File Offset: 0x00013C9F
			public override void Apply(Selectable item)
			{
				base.Apply(item);
				this.Apply(item as Scrollbar);
			}

			[SerializeField]
			private ThemeSettings.ImageSettings _handleImageSettings;

			[SerializeField]
			private ThemeSettings.ImageSettings _backgroundImageSettings;
		}

		[Serializable]
		private class ImageSettings
		{
			// (get) Token: 0x06001C44 RID: 7236 RVA: 0x00015AB4 File Offset: 0x00013CB4
			public Color color
			{
				get
				{
					return this._color;
				}
			}

			// (get) Token: 0x06001C45 RID: 7237 RVA: 0x00015ABC File Offset: 0x00013CBC
			public Sprite sprite
			{
				get
				{
					return this._sprite;
				}
			}

			// (get) Token: 0x06001C46 RID: 7238 RVA: 0x00015AC4 File Offset: 0x00013CC4
			public Material materal
			{
				get
				{
					return this._materal;
				}
			}

			// (get) Token: 0x06001C47 RID: 7239 RVA: 0x00015ACC File Offset: 0x00013CCC
			public Image.Type type
			{
				get
				{
					return this._type;
				}
			}

			// (get) Token: 0x06001C48 RID: 7240 RVA: 0x00015AD4 File Offset: 0x00013CD4
			public bool preserveAspect
			{
				get
				{
					return this._preserveAspect;
				}
			}

			// (get) Token: 0x06001C49 RID: 7241 RVA: 0x00015ADC File Offset: 0x00013CDC
			public bool fillCenter
			{
				get
				{
					return this._fillCenter;
				}
			}

			// (get) Token: 0x06001C4A RID: 7242 RVA: 0x00015AE4 File Offset: 0x00013CE4
			public Image.FillMethod fillMethod
			{
				get
				{
					return this._fillMethod;
				}
			}

			// (get) Token: 0x06001C4B RID: 7243 RVA: 0x00015AEC File Offset: 0x00013CEC
			public float fillAmout
			{
				get
				{
					return this._fillAmout;
				}
			}

			// (get) Token: 0x06001C4C RID: 7244 RVA: 0x00015AF4 File Offset: 0x00013CF4
			public bool fillClockwise
			{
				get
				{
					return this._fillClockwise;
				}
			}

			// (get) Token: 0x06001C4D RID: 7245 RVA: 0x00015AFC File Offset: 0x00013CFC
			public int fillOrigin
			{
				get
				{
					return this._fillOrigin;
				}
			}

			// Token: 0x06001C4E RID: 7246 RVA: 0x000706A0 File Offset: 0x0006E8A0
			public virtual void CopyTo(Image image)
			{
				if (image == null)
				{
					return;
				}
				image.color = this._color;
				image.sprite = this._sprite;
				image.material = this._materal;
				image.type = this._type;
				image.preserveAspect = this._preserveAspect;
				image.fillCenter = this._fillCenter;
				image.fillMethod = this._fillMethod;
				image.fillAmount = this._fillAmout;
				image.fillClockwise = this._fillClockwise;
				image.fillOrigin = this._fillOrigin;
			}

			[SerializeField]
			private Color _color = Color.white;

			[SerializeField]
			private Sprite _sprite;

			[SerializeField]
			private Material _materal;

			[SerializeField]
			private Image.Type _type;

			[SerializeField]
			private bool _preserveAspect;

			[SerializeField]
			private bool _fillCenter;

			[SerializeField]
			private Image.FillMethod _fillMethod;

			[SerializeField]
			private float _fillAmout;

			[SerializeField]
			private bool _fillClockwise;

			[SerializeField]
			private int _fillOrigin;
		}

		[Serializable]
		private struct CustomColorBlock
		{
			// (get) Token: 0x06001C50 RID: 7248 RVA: 0x00015B17 File Offset: 0x00013D17
			// (set) Token: 0x06001C51 RID: 7249 RVA: 0x00015B1F File Offset: 0x00013D1F
			public float colorMultiplier
			{
				get
				{
					return this.m_ColorMultiplier;
				}
				set
				{
					this.m_ColorMultiplier = value;
				}
			}

			// (get) Token: 0x06001C52 RID: 7250 RVA: 0x00015B28 File Offset: 0x00013D28
			// (set) Token: 0x06001C53 RID: 7251 RVA: 0x00015B30 File Offset: 0x00013D30
			public Color disabledColor
			{
				get
				{
					return this.m_DisabledColor;
				}
				set
				{
					this.m_DisabledColor = value;
				}
			}

			// (get) Token: 0x06001C54 RID: 7252 RVA: 0x00015B39 File Offset: 0x00013D39
			// (set) Token: 0x06001C55 RID: 7253 RVA: 0x00015B41 File Offset: 0x00013D41
			public float fadeDuration
			{
				get
				{
					return this.m_FadeDuration;
				}
				set
				{
					this.m_FadeDuration = value;
				}
			}

			// (get) Token: 0x06001C56 RID: 7254 RVA: 0x00015B4A File Offset: 0x00013D4A
			// (set) Token: 0x06001C57 RID: 7255 RVA: 0x00015B52 File Offset: 0x00013D52
			public Color highlightedColor
			{
				get
				{
					return this.m_HighlightedColor;
				}
				set
				{
					this.m_HighlightedColor = value;
				}
			}

			// (get) Token: 0x06001C58 RID: 7256 RVA: 0x00015B5B File Offset: 0x00013D5B
			// (set) Token: 0x06001C59 RID: 7257 RVA: 0x00015B63 File Offset: 0x00013D63
			public Color normalColor
			{
				get
				{
					return this.m_NormalColor;
				}
				set
				{
					this.m_NormalColor = value;
				}
			}

			// (get) Token: 0x06001C5A RID: 7258 RVA: 0x00015B6C File Offset: 0x00013D6C
			// (set) Token: 0x06001C5B RID: 7259 RVA: 0x00015B74 File Offset: 0x00013D74
			public Color pressedColor
			{
				get
				{
					return this.m_PressedColor;
				}
				set
				{
					this.m_PressedColor = value;
				}
			}

			// (get) Token: 0x06001C5C RID: 7260 RVA: 0x00015B7D File Offset: 0x00013D7D
			// (set) Token: 0x06001C5D RID: 7261 RVA: 0x00015B85 File Offset: 0x00013D85
			public Color selectedColor
			{
				get
				{
					return this.m_SelectedColor;
				}
				set
				{
					this.m_SelectedColor = value;
				}
			}

			// (get) Token: 0x06001C5E RID: 7262 RVA: 0x00015B8E File Offset: 0x00013D8E
			// (set) Token: 0x06001C5F RID: 7263 RVA: 0x00015B96 File Offset: 0x00013D96
			public Color disabledHighlightedColor
			{
				get
				{
					return this.m_DisabledHighlightedColor;
				}
				set
				{
					this.m_DisabledHighlightedColor = value;
				}
			}

			// Token: 0x06001C60 RID: 7264 RVA: 0x00070730 File Offset: 0x0006E930
			public static implicit operator ColorBlock(ThemeSettings.CustomColorBlock item)
			{
				return new ColorBlock
				{
					selectedColor = item.m_SelectedColor,
					colorMultiplier = item.m_ColorMultiplier,
					disabledColor = item.m_DisabledColor,
					fadeDuration = item.m_FadeDuration,
					highlightedColor = item.m_HighlightedColor,
					normalColor = item.m_NormalColor,
					pressedColor = item.m_PressedColor
				};
			}

			[SerializeField]
			private float m_ColorMultiplier;

			[SerializeField]
			private Color m_DisabledColor;

			[SerializeField]
			private float m_FadeDuration;

			[SerializeField]
			private Color m_HighlightedColor;

			[SerializeField]
			private Color m_NormalColor;

			[SerializeField]
			private Color m_PressedColor;

			[SerializeField]
			private Color m_SelectedColor;

			[SerializeField]
			private Color m_DisabledHighlightedColor;
		}

		[Serializable]
		private struct CustomSpriteState
		{
			// (get) Token: 0x06001C61 RID: 7265 RVA: 0x00015B9F File Offset: 0x00013D9F
			// (set) Token: 0x06001C62 RID: 7266 RVA: 0x00015BA7 File Offset: 0x00013DA7
			public Sprite disabledSprite
			{
				get
				{
					return this.m_DisabledSprite;
				}
				set
				{
					this.m_DisabledSprite = value;
				}
			}

			// (get) Token: 0x06001C63 RID: 7267 RVA: 0x00015BB0 File Offset: 0x00013DB0
			// (set) Token: 0x06001C64 RID: 7268 RVA: 0x00015BB8 File Offset: 0x00013DB8
			public Sprite highlightedSprite
			{
				get
				{
					return this.m_HighlightedSprite;
				}
				set
				{
					this.m_HighlightedSprite = value;
				}
			}

			// (get) Token: 0x06001C65 RID: 7269 RVA: 0x00015BC1 File Offset: 0x00013DC1
			// (set) Token: 0x06001C66 RID: 7270 RVA: 0x00015BC9 File Offset: 0x00013DC9
			public Sprite pressedSprite
			{
				get
				{
					return this.m_PressedSprite;
				}
				set
				{
					this.m_PressedSprite = value;
				}
			}

			// (get) Token: 0x06001C67 RID: 7271 RVA: 0x00015BD2 File Offset: 0x00013DD2
			// (set) Token: 0x06001C68 RID: 7272 RVA: 0x00015BDA File Offset: 0x00013DDA
			public Sprite selectedSprite
			{
				get
				{
					return this.m_SelectedSprite;
				}
				set
				{
					this.m_SelectedSprite = value;
				}
			}

			// (get) Token: 0x06001C69 RID: 7273 RVA: 0x00015BE3 File Offset: 0x00013DE3
			// (set) Token: 0x06001C6A RID: 7274 RVA: 0x00015BEB File Offset: 0x00013DEB
			public Sprite disabledHighlightedSprite
			{
				get
				{
					return this.m_DisabledHighlightedSprite;
				}
				set
				{
					this.m_DisabledHighlightedSprite = value;
				}
			}

			// Token: 0x06001C6B RID: 7275 RVA: 0x000707A4 File Offset: 0x0006E9A4
			public static implicit operator SpriteState(ThemeSettings.CustomSpriteState item)
			{
				return new SpriteState
				{
					selectedSprite = item.m_SelectedSprite,
					disabledSprite = item.m_DisabledSprite,
					highlightedSprite = item.m_HighlightedSprite,
					pressedSprite = item.m_PressedSprite
				};
			}

			[SerializeField]
			private Sprite m_DisabledSprite;

			[SerializeField]
			private Sprite m_HighlightedSprite;

			[SerializeField]
			private Sprite m_PressedSprite;

			[SerializeField]
			private Sprite m_SelectedSprite;

			[SerializeField]
			private Sprite m_DisabledHighlightedSprite;
		}

		[Serializable]
		private class CustomAnimationTriggers
		{
			// Token: 0x06001C6C RID: 7276 RVA: 0x000707F0 File Offset: 0x0006E9F0
			public CustomAnimationTriggers()
			{
				this.m_DisabledTrigger = string.Empty;
				this.m_HighlightedTrigger = string.Empty;
				this.m_NormalTrigger = string.Empty;
				this.m_PressedTrigger = string.Empty;
				this.m_SelectedTrigger = string.Empty;
				this.m_DisabledHighlightedTrigger = string.Empty;
			}

			// (get) Token: 0x06001C6D RID: 7277 RVA: 0x00015BF4 File Offset: 0x00013DF4
			// (set) Token: 0x06001C6E RID: 7278 RVA: 0x00015BFC File Offset: 0x00013DFC
			public string disabledTrigger
			{
				get
				{
					return this.m_DisabledTrigger;
				}
				set
				{
					this.m_DisabledTrigger = value;
				}
			}

			// (get) Token: 0x06001C6F RID: 7279 RVA: 0x00015C05 File Offset: 0x00013E05
			// (set) Token: 0x06001C70 RID: 7280 RVA: 0x00015C0D File Offset: 0x00013E0D
			public string highlightedTrigger
			{
				get
				{
					return this.m_HighlightedTrigger;
				}
				set
				{
					this.m_HighlightedTrigger = value;
				}
			}

			// (get) Token: 0x06001C71 RID: 7281 RVA: 0x00015C16 File Offset: 0x00013E16
			// (set) Token: 0x06001C72 RID: 7282 RVA: 0x00015C1E File Offset: 0x00013E1E
			public string normalTrigger
			{
				get
				{
					return this.m_NormalTrigger;
				}
				set
				{
					this.m_NormalTrigger = value;
				}
			}

			// (get) Token: 0x06001C73 RID: 7283 RVA: 0x00015C27 File Offset: 0x00013E27
			// (set) Token: 0x06001C74 RID: 7284 RVA: 0x00015C2F File Offset: 0x00013E2F
			public string pressedTrigger
			{
				get
				{
					return this.m_PressedTrigger;
				}
				set
				{
					this.m_PressedTrigger = value;
				}
			}

			// (get) Token: 0x06001C75 RID: 7285 RVA: 0x00015C38 File Offset: 0x00013E38
			// (set) Token: 0x06001C76 RID: 7286 RVA: 0x00015C40 File Offset: 0x00013E40
			public string selectedTrigger
			{
				get
				{
					return this.m_SelectedTrigger;
				}
				set
				{
					this.m_SelectedTrigger = value;
				}
			}

			// (get) Token: 0x06001C77 RID: 7287 RVA: 0x00015C49 File Offset: 0x00013E49
			// (set) Token: 0x06001C78 RID: 7288 RVA: 0x00015C51 File Offset: 0x00013E51
			public string disabledHighlightedTrigger
			{
				get
				{
					return this.m_DisabledHighlightedTrigger;
				}
				set
				{
					this.m_DisabledHighlightedTrigger = value;
				}
			}

			// Token: 0x06001C79 RID: 7289 RVA: 0x00070848 File Offset: 0x0006EA48
			public static implicit operator AnimationTriggers(ThemeSettings.CustomAnimationTriggers item)
			{
				return new AnimationTriggers
				{
					selectedTrigger = item.m_SelectedTrigger,
					disabledTrigger = item.m_DisabledTrigger,
					highlightedTrigger = item.m_HighlightedTrigger,
					normalTrigger = item.m_NormalTrigger,
					pressedTrigger = item.m_PressedTrigger
				};
			}

			[SerializeField]
			private string m_DisabledTrigger;

			[SerializeField]
			private string m_HighlightedTrigger;

			[SerializeField]
			private string m_NormalTrigger;

			[SerializeField]
			private string m_PressedTrigger;

			[SerializeField]
			private string m_SelectedTrigger;

			[SerializeField]
			private string m_DisabledHighlightedTrigger;
		}

		[Serializable]
		private class TextSettings
		{
			// (get) Token: 0x06001C7A RID: 7290 RVA: 0x00015C5A File Offset: 0x00013E5A
			public Color color
			{
				get
				{
					return this._color;
				}
			}

			// (get) Token: 0x06001C7B RID: 7291 RVA: 0x00015C62 File Offset: 0x00013E62
			public Font font
			{
				get
				{
					return this._font;
				}
			}

			// (get) Token: 0x06001C7C RID: 7292 RVA: 0x00015C6A File Offset: 0x00013E6A
			public ThemeSettings.FontStyleOverride style
			{
				get
				{
					return this._style;
				}
			}

			// (get) Token: 0x06001C7D RID: 7293 RVA: 0x00015C72 File Offset: 0x00013E72
			public float sizeMultiplier
			{
				get
				{
					return this._sizeMultiplier;
				}
			}

			// (get) Token: 0x06001C7E RID: 7294 RVA: 0x00015C7A File Offset: 0x00013E7A
			public float lineSpacing
			{
				get
				{
					return this._lineSpacing;
				}
			}

			[SerializeField]
			private Color _color = Color.white;

			[SerializeField]
			private Font _font;

			[SerializeField]
			private ThemeSettings.FontStyleOverride _style;

			[SerializeField]
			private float _sizeMultiplier = 1f;

			[SerializeField]
			private float _lineSpacing = 1f;
		}

		private enum FontStyleOverride
		{
			Default,
			Normal,
			Bold,
			Italic,
			BoldAndItalic
		}
	}
}
