using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	[Serializable]
	public class ThemeSettings : ScriptableObject
	{
		// Token: 0x06001C28 RID: 7208 RVA: 0x000700F8 File Offset: 0x0006E2F8
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

		// Token: 0x06001C29 RID: 7209 RVA: 0x00070134 File Offset: 0x0006E334
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

		// Token: 0x06001C2A RID: 7210 RVA: 0x000701B4 File Offset: 0x0006E3B4
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

		// Token: 0x06001C2B RID: 7211 RVA: 0x00070268 File Offset: 0x0006E468
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

		// Token: 0x06001C2C RID: 7212 RVA: 0x00070500 File Offset: 0x0006E700
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

		// Token: 0x06001C2D RID: 7213 RVA: 0x000159EC File Offset: 0x00013BEC
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

		// Token: 0x06001C2E RID: 7214 RVA: 0x00015A1B File Offset: 0x00013C1B
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
			// (get) Token: 0x06001C30 RID: 7216 RVA: 0x00015A20 File Offset: 0x00013C20
			public Selectable.Transition transition
			{
				get
				{
					return this._transition;
				}
			}

			// (get) Token: 0x06001C31 RID: 7217 RVA: 0x00015A28 File Offset: 0x00013C28
			public ThemeSettings.CustomColorBlock selectableColors
			{
				get
				{
					return this._colors;
				}
			}

			// (get) Token: 0x06001C32 RID: 7218 RVA: 0x00015A30 File Offset: 0x00013C30
			public ThemeSettings.CustomSpriteState spriteState
			{
				get
				{
					return this._spriteState;
				}
			}

			// (get) Token: 0x06001C33 RID: 7219 RVA: 0x00015A38 File Offset: 0x00013C38
			public ThemeSettings.CustomAnimationTriggers animationTriggers
			{
				get
				{
					return this._animationTriggers;
				}
			}

			// Token: 0x06001C34 RID: 7220 RVA: 0x000705E8 File Offset: 0x0006E7E8
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
			// (get) Token: 0x06001C36 RID: 7222 RVA: 0x00015A40 File Offset: 0x00013C40
			public ThemeSettings.ImageSettings imageSettings
			{
				get
				{
					return this._imageSettings;
				}
			}

			// Token: 0x06001C37 RID: 7223 RVA: 0x00015A48 File Offset: 0x00013C48
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
			// (get) Token: 0x06001C39 RID: 7225 RVA: 0x00015A81 File Offset: 0x00013C81
			public ThemeSettings.ImageSettings handleImageSettings
			{
				get
				{
					return this._handleImageSettings;
				}
			}

			// (get) Token: 0x06001C3A RID: 7226 RVA: 0x00015A89 File Offset: 0x00013C89
			public ThemeSettings.ImageSettings fillImageSettings
			{
				get
				{
					return this._fillImageSettings;
				}
			}

			// (get) Token: 0x06001C3B RID: 7227 RVA: 0x00015A91 File Offset: 0x00013C91
			public ThemeSettings.ImageSettings backgroundImageSettings
			{
				get
				{
					return this._backgroundImageSettings;
				}
			}

			// Token: 0x06001C3C RID: 7228 RVA: 0x0007072C File Offset: 0x0006E92C
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

			// Token: 0x06001C3D RID: 7229 RVA: 0x00015A99 File Offset: 0x00013C99
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
			// (get) Token: 0x06001C3F RID: 7231 RVA: 0x00015AAE File Offset: 0x00013CAE
			public ThemeSettings.ImageSettings handle
			{
				get
				{
					return this._handleImageSettings;
				}
			}

			// (get) Token: 0x06001C40 RID: 7232 RVA: 0x00015AB6 File Offset: 0x00013CB6
			public ThemeSettings.ImageSettings background
			{
				get
				{
					return this._backgroundImageSettings;
				}
			}

			// Token: 0x06001C41 RID: 7233 RVA: 0x000707C0 File Offset: 0x0006E9C0
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

			// Token: 0x06001C42 RID: 7234 RVA: 0x00015ABE File Offset: 0x00013CBE
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
			// (get) Token: 0x06001C44 RID: 7236 RVA: 0x00015AD3 File Offset: 0x00013CD3
			public Color color
			{
				get
				{
					return this._color;
				}
			}

			// (get) Token: 0x06001C45 RID: 7237 RVA: 0x00015ADB File Offset: 0x00013CDB
			public Sprite sprite
			{
				get
				{
					return this._sprite;
				}
			}

			// (get) Token: 0x06001C46 RID: 7238 RVA: 0x00015AE3 File Offset: 0x00013CE3
			public Material materal
			{
				get
				{
					return this._materal;
				}
			}

			// (get) Token: 0x06001C47 RID: 7239 RVA: 0x00015AEB File Offset: 0x00013CEB
			public Image.Type type
			{
				get
				{
					return this._type;
				}
			}

			// (get) Token: 0x06001C48 RID: 7240 RVA: 0x00015AF3 File Offset: 0x00013CF3
			public bool preserveAspect
			{
				get
				{
					return this._preserveAspect;
				}
			}

			// (get) Token: 0x06001C49 RID: 7241 RVA: 0x00015AFB File Offset: 0x00013CFB
			public bool fillCenter
			{
				get
				{
					return this._fillCenter;
				}
			}

			// (get) Token: 0x06001C4A RID: 7242 RVA: 0x00015B03 File Offset: 0x00013D03
			public Image.FillMethod fillMethod
			{
				get
				{
					return this._fillMethod;
				}
			}

			// (get) Token: 0x06001C4B RID: 7243 RVA: 0x00015B0B File Offset: 0x00013D0B
			public float fillAmout
			{
				get
				{
					return this._fillAmout;
				}
			}

			// (get) Token: 0x06001C4C RID: 7244 RVA: 0x00015B13 File Offset: 0x00013D13
			public bool fillClockwise
			{
				get
				{
					return this._fillClockwise;
				}
			}

			// (get) Token: 0x06001C4D RID: 7245 RVA: 0x00015B1B File Offset: 0x00013D1B
			public int fillOrigin
			{
				get
				{
					return this._fillOrigin;
				}
			}

			// Token: 0x06001C4E RID: 7246 RVA: 0x00070810 File Offset: 0x0006EA10
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
			// (get) Token: 0x06001C50 RID: 7248 RVA: 0x00015B36 File Offset: 0x00013D36
			// (set) Token: 0x06001C51 RID: 7249 RVA: 0x00015B3E File Offset: 0x00013D3E
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

			// (get) Token: 0x06001C52 RID: 7250 RVA: 0x00015B47 File Offset: 0x00013D47
			// (set) Token: 0x06001C53 RID: 7251 RVA: 0x00015B4F File Offset: 0x00013D4F
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

			// (get) Token: 0x06001C54 RID: 7252 RVA: 0x00015B58 File Offset: 0x00013D58
			// (set) Token: 0x06001C55 RID: 7253 RVA: 0x00015B60 File Offset: 0x00013D60
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

			// (get) Token: 0x06001C56 RID: 7254 RVA: 0x00015B69 File Offset: 0x00013D69
			// (set) Token: 0x06001C57 RID: 7255 RVA: 0x00015B71 File Offset: 0x00013D71
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

			// (get) Token: 0x06001C58 RID: 7256 RVA: 0x00015B7A File Offset: 0x00013D7A
			// (set) Token: 0x06001C59 RID: 7257 RVA: 0x00015B82 File Offset: 0x00013D82
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

			// (get) Token: 0x06001C5A RID: 7258 RVA: 0x00015B8B File Offset: 0x00013D8B
			// (set) Token: 0x06001C5B RID: 7259 RVA: 0x00015B93 File Offset: 0x00013D93
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

			// (get) Token: 0x06001C5C RID: 7260 RVA: 0x00015B9C File Offset: 0x00013D9C
			// (set) Token: 0x06001C5D RID: 7261 RVA: 0x00015BA4 File Offset: 0x00013DA4
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

			// (get) Token: 0x06001C5E RID: 7262 RVA: 0x00015BAD File Offset: 0x00013DAD
			// (set) Token: 0x06001C5F RID: 7263 RVA: 0x00015BB5 File Offset: 0x00013DB5
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

			// Token: 0x06001C60 RID: 7264 RVA: 0x000708A0 File Offset: 0x0006EAA0
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
			// (get) Token: 0x06001C61 RID: 7265 RVA: 0x00015BBE File Offset: 0x00013DBE
			// (set) Token: 0x06001C62 RID: 7266 RVA: 0x00015BC6 File Offset: 0x00013DC6
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

			// (get) Token: 0x06001C63 RID: 7267 RVA: 0x00015BCF File Offset: 0x00013DCF
			// (set) Token: 0x06001C64 RID: 7268 RVA: 0x00015BD7 File Offset: 0x00013DD7
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

			// (get) Token: 0x06001C65 RID: 7269 RVA: 0x00015BE0 File Offset: 0x00013DE0
			// (set) Token: 0x06001C66 RID: 7270 RVA: 0x00015BE8 File Offset: 0x00013DE8
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

			// (get) Token: 0x06001C67 RID: 7271 RVA: 0x00015BF1 File Offset: 0x00013DF1
			// (set) Token: 0x06001C68 RID: 7272 RVA: 0x00015BF9 File Offset: 0x00013DF9
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

			// (get) Token: 0x06001C69 RID: 7273 RVA: 0x00015C02 File Offset: 0x00013E02
			// (set) Token: 0x06001C6A RID: 7274 RVA: 0x00015C0A File Offset: 0x00013E0A
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

			// Token: 0x06001C6B RID: 7275 RVA: 0x00070914 File Offset: 0x0006EB14
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
			// Token: 0x06001C6C RID: 7276 RVA: 0x00070960 File Offset: 0x0006EB60
			public CustomAnimationTriggers()
			{
				this.m_DisabledTrigger = string.Empty;
				this.m_HighlightedTrigger = string.Empty;
				this.m_NormalTrigger = string.Empty;
				this.m_PressedTrigger = string.Empty;
				this.m_SelectedTrigger = string.Empty;
				this.m_DisabledHighlightedTrigger = string.Empty;
			}

			// (get) Token: 0x06001C6D RID: 7277 RVA: 0x00015C13 File Offset: 0x00013E13
			// (set) Token: 0x06001C6E RID: 7278 RVA: 0x00015C1B File Offset: 0x00013E1B
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

			// (get) Token: 0x06001C6F RID: 7279 RVA: 0x00015C24 File Offset: 0x00013E24
			// (set) Token: 0x06001C70 RID: 7280 RVA: 0x00015C2C File Offset: 0x00013E2C
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

			// (get) Token: 0x06001C71 RID: 7281 RVA: 0x00015C35 File Offset: 0x00013E35
			// (set) Token: 0x06001C72 RID: 7282 RVA: 0x00015C3D File Offset: 0x00013E3D
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

			// (get) Token: 0x06001C73 RID: 7283 RVA: 0x00015C46 File Offset: 0x00013E46
			// (set) Token: 0x06001C74 RID: 7284 RVA: 0x00015C4E File Offset: 0x00013E4E
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

			// (get) Token: 0x06001C75 RID: 7285 RVA: 0x00015C57 File Offset: 0x00013E57
			// (set) Token: 0x06001C76 RID: 7286 RVA: 0x00015C5F File Offset: 0x00013E5F
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

			// (get) Token: 0x06001C77 RID: 7287 RVA: 0x00015C68 File Offset: 0x00013E68
			// (set) Token: 0x06001C78 RID: 7288 RVA: 0x00015C70 File Offset: 0x00013E70
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

			// Token: 0x06001C79 RID: 7289 RVA: 0x000709B8 File Offset: 0x0006EBB8
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
			// (get) Token: 0x06001C7A RID: 7290 RVA: 0x00015C79 File Offset: 0x00013E79
			public Color color
			{
				get
				{
					return this._color;
				}
			}

			// (get) Token: 0x06001C7B RID: 7291 RVA: 0x00015C81 File Offset: 0x00013E81
			public Font font
			{
				get
				{
					return this._font;
				}
			}

			// (get) Token: 0x06001C7C RID: 7292 RVA: 0x00015C89 File Offset: 0x00013E89
			public ThemeSettings.FontStyleOverride style
			{
				get
				{
					return this._style;
				}
			}

			// (get) Token: 0x06001C7D RID: 7293 RVA: 0x00015C91 File Offset: 0x00013E91
			public float sizeMultiplier
			{
				get
				{
					return this._sizeMultiplier;
				}
			}

			// (get) Token: 0x06001C7E RID: 7294 RVA: 0x00015C99 File Offset: 0x00013E99
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
