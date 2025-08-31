using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	[Serializable]
	public class ThemeSettings : ScriptableObject
	{
		// Token: 0x06001694 RID: 5780 RVA: 0x0005E820 File Offset: 0x0005CA20
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

		// Token: 0x06001695 RID: 5781 RVA: 0x0005E85C File Offset: 0x0005CA5C
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

		// Token: 0x06001696 RID: 5782 RVA: 0x0005E8DC File Offset: 0x0005CADC
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

		// Token: 0x06001697 RID: 5783 RVA: 0x0005E990 File Offset: 0x0005CB90
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

		// Token: 0x06001698 RID: 5784 RVA: 0x0005EC28 File Offset: 0x0005CE28
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

		// Token: 0x06001699 RID: 5785 RVA: 0x0005ED0E File Offset: 0x0005CF0E
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

		// Token: 0x0600169A RID: 5786 RVA: 0x0005ED3D File Offset: 0x0005CF3D
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
			// (get) Token: 0x06001D50 RID: 7504 RVA: 0x000780D9 File Offset: 0x000762D9
			public Selectable.Transition transition
			{
				get
				{
					return this._transition;
				}
			}

			// (get) Token: 0x06001D51 RID: 7505 RVA: 0x000780E1 File Offset: 0x000762E1
			public ThemeSettings.CustomColorBlock selectableColors
			{
				get
				{
					return this._colors;
				}
			}

			// (get) Token: 0x06001D52 RID: 7506 RVA: 0x000780E9 File Offset: 0x000762E9
			public ThemeSettings.CustomSpriteState spriteState
			{
				get
				{
					return this._spriteState;
				}
			}

			// (get) Token: 0x06001D53 RID: 7507 RVA: 0x000780F1 File Offset: 0x000762F1
			public ThemeSettings.CustomAnimationTriggers animationTriggers
			{
				get
				{
					return this._animationTriggers;
				}
			}

			// Token: 0x06001D54 RID: 7508 RVA: 0x000780FC File Offset: 0x000762FC
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
			// (get) Token: 0x06001D56 RID: 7510 RVA: 0x00078248 File Offset: 0x00076448
			public ThemeSettings.ImageSettings imageSettings
			{
				get
				{
					return this._imageSettings;
				}
			}

			// Token: 0x06001D57 RID: 7511 RVA: 0x00078250 File Offset: 0x00076450
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
			// (get) Token: 0x06001D59 RID: 7513 RVA: 0x00078289 File Offset: 0x00076489
			public ThemeSettings.ImageSettings handleImageSettings
			{
				get
				{
					return this._handleImageSettings;
				}
			}

			// (get) Token: 0x06001D5A RID: 7514 RVA: 0x00078291 File Offset: 0x00076491
			public ThemeSettings.ImageSettings fillImageSettings
			{
				get
				{
					return this._fillImageSettings;
				}
			}

			// (get) Token: 0x06001D5B RID: 7515 RVA: 0x00078299 File Offset: 0x00076499
			public ThemeSettings.ImageSettings backgroundImageSettings
			{
				get
				{
					return this._backgroundImageSettings;
				}
			}

			// Token: 0x06001D5C RID: 7516 RVA: 0x000782A4 File Offset: 0x000764A4
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

			// Token: 0x06001D5D RID: 7517 RVA: 0x00078335 File Offset: 0x00076535
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
			// (get) Token: 0x06001D5F RID: 7519 RVA: 0x00078352 File Offset: 0x00076552
			public ThemeSettings.ImageSettings handle
			{
				get
				{
					return this._handleImageSettings;
				}
			}

			// (get) Token: 0x06001D60 RID: 7520 RVA: 0x0007835A File Offset: 0x0007655A
			public ThemeSettings.ImageSettings background
			{
				get
				{
					return this._backgroundImageSettings;
				}
			}

			// Token: 0x06001D61 RID: 7521 RVA: 0x00078364 File Offset: 0x00076564
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

			// Token: 0x06001D62 RID: 7522 RVA: 0x000783B2 File Offset: 0x000765B2
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
			// (get) Token: 0x06001D64 RID: 7524 RVA: 0x000783CF File Offset: 0x000765CF
			public Color color
			{
				get
				{
					return this._color;
				}
			}

			// (get) Token: 0x06001D65 RID: 7525 RVA: 0x000783D7 File Offset: 0x000765D7
			public Sprite sprite
			{
				get
				{
					return this._sprite;
				}
			}

			// (get) Token: 0x06001D66 RID: 7526 RVA: 0x000783DF File Offset: 0x000765DF
			public Material materal
			{
				get
				{
					return this._materal;
				}
			}

			// (get) Token: 0x06001D67 RID: 7527 RVA: 0x000783E7 File Offset: 0x000765E7
			public Image.Type type
			{
				get
				{
					return this._type;
				}
			}

			// (get) Token: 0x06001D68 RID: 7528 RVA: 0x000783EF File Offset: 0x000765EF
			public bool preserveAspect
			{
				get
				{
					return this._preserveAspect;
				}
			}

			// (get) Token: 0x06001D69 RID: 7529 RVA: 0x000783F7 File Offset: 0x000765F7
			public bool fillCenter
			{
				get
				{
					return this._fillCenter;
				}
			}

			// (get) Token: 0x06001D6A RID: 7530 RVA: 0x000783FF File Offset: 0x000765FF
			public Image.FillMethod fillMethod
			{
				get
				{
					return this._fillMethod;
				}
			}

			// (get) Token: 0x06001D6B RID: 7531 RVA: 0x00078407 File Offset: 0x00076607
			public float fillAmout
			{
				get
				{
					return this._fillAmout;
				}
			}

			// (get) Token: 0x06001D6C RID: 7532 RVA: 0x0007840F File Offset: 0x0007660F
			public bool fillClockwise
			{
				get
				{
					return this._fillClockwise;
				}
			}

			// (get) Token: 0x06001D6D RID: 7533 RVA: 0x00078417 File Offset: 0x00076617
			public int fillOrigin
			{
				get
				{
					return this._fillOrigin;
				}
			}

			// Token: 0x06001D6E RID: 7534 RVA: 0x00078420 File Offset: 0x00076620
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
			// (get) Token: 0x06001D70 RID: 7536 RVA: 0x000784C2 File Offset: 0x000766C2
			// (set) Token: 0x06001D71 RID: 7537 RVA: 0x000784CA File Offset: 0x000766CA
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

			// (get) Token: 0x06001D72 RID: 7538 RVA: 0x000784D3 File Offset: 0x000766D3
			// (set) Token: 0x06001D73 RID: 7539 RVA: 0x000784DB File Offset: 0x000766DB
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

			// (get) Token: 0x06001D74 RID: 7540 RVA: 0x000784E4 File Offset: 0x000766E4
			// (set) Token: 0x06001D75 RID: 7541 RVA: 0x000784EC File Offset: 0x000766EC
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

			// (get) Token: 0x06001D76 RID: 7542 RVA: 0x000784F5 File Offset: 0x000766F5
			// (set) Token: 0x06001D77 RID: 7543 RVA: 0x000784FD File Offset: 0x000766FD
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

			// (get) Token: 0x06001D78 RID: 7544 RVA: 0x00078506 File Offset: 0x00076706
			// (set) Token: 0x06001D79 RID: 7545 RVA: 0x0007850E File Offset: 0x0007670E
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

			// (get) Token: 0x06001D7A RID: 7546 RVA: 0x00078517 File Offset: 0x00076717
			// (set) Token: 0x06001D7B RID: 7547 RVA: 0x0007851F File Offset: 0x0007671F
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

			// (get) Token: 0x06001D7C RID: 7548 RVA: 0x00078528 File Offset: 0x00076728
			// (set) Token: 0x06001D7D RID: 7549 RVA: 0x00078530 File Offset: 0x00076730
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

			// (get) Token: 0x06001D7E RID: 7550 RVA: 0x00078539 File Offset: 0x00076739
			// (set) Token: 0x06001D7F RID: 7551 RVA: 0x00078541 File Offset: 0x00076741
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

			// Token: 0x06001D80 RID: 7552 RVA: 0x0007854C File Offset: 0x0007674C
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
			// (get) Token: 0x06001D81 RID: 7553 RVA: 0x000785BD File Offset: 0x000767BD
			// (set) Token: 0x06001D82 RID: 7554 RVA: 0x000785C5 File Offset: 0x000767C5
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

			// (get) Token: 0x06001D83 RID: 7555 RVA: 0x000785CE File Offset: 0x000767CE
			// (set) Token: 0x06001D84 RID: 7556 RVA: 0x000785D6 File Offset: 0x000767D6
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

			// (get) Token: 0x06001D85 RID: 7557 RVA: 0x000785DF File Offset: 0x000767DF
			// (set) Token: 0x06001D86 RID: 7558 RVA: 0x000785E7 File Offset: 0x000767E7
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

			// (get) Token: 0x06001D87 RID: 7559 RVA: 0x000785F0 File Offset: 0x000767F0
			// (set) Token: 0x06001D88 RID: 7560 RVA: 0x000785F8 File Offset: 0x000767F8
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

			// (get) Token: 0x06001D89 RID: 7561 RVA: 0x00078601 File Offset: 0x00076801
			// (set) Token: 0x06001D8A RID: 7562 RVA: 0x00078609 File Offset: 0x00076809
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

			// Token: 0x06001D8B RID: 7563 RVA: 0x00078614 File Offset: 0x00076814
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
			// Token: 0x06001D8C RID: 7564 RVA: 0x00078660 File Offset: 0x00076860
			public CustomAnimationTriggers()
			{
				this.m_DisabledTrigger = string.Empty;
				this.m_HighlightedTrigger = string.Empty;
				this.m_NormalTrigger = string.Empty;
				this.m_PressedTrigger = string.Empty;
				this.m_SelectedTrigger = string.Empty;
				this.m_DisabledHighlightedTrigger = string.Empty;
			}

			// (get) Token: 0x06001D8D RID: 7565 RVA: 0x000786B5 File Offset: 0x000768B5
			// (set) Token: 0x06001D8E RID: 7566 RVA: 0x000786BD File Offset: 0x000768BD
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

			// (get) Token: 0x06001D8F RID: 7567 RVA: 0x000786C6 File Offset: 0x000768C6
			// (set) Token: 0x06001D90 RID: 7568 RVA: 0x000786CE File Offset: 0x000768CE
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

			// (get) Token: 0x06001D91 RID: 7569 RVA: 0x000786D7 File Offset: 0x000768D7
			// (set) Token: 0x06001D92 RID: 7570 RVA: 0x000786DF File Offset: 0x000768DF
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

			// (get) Token: 0x06001D93 RID: 7571 RVA: 0x000786E8 File Offset: 0x000768E8
			// (set) Token: 0x06001D94 RID: 7572 RVA: 0x000786F0 File Offset: 0x000768F0
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

			// (get) Token: 0x06001D95 RID: 7573 RVA: 0x000786F9 File Offset: 0x000768F9
			// (set) Token: 0x06001D96 RID: 7574 RVA: 0x00078701 File Offset: 0x00076901
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

			// (get) Token: 0x06001D97 RID: 7575 RVA: 0x0007870A File Offset: 0x0007690A
			// (set) Token: 0x06001D98 RID: 7576 RVA: 0x00078712 File Offset: 0x00076912
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

			// Token: 0x06001D99 RID: 7577 RVA: 0x0007871C File Offset: 0x0007691C
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
			// (get) Token: 0x06001D9A RID: 7578 RVA: 0x0007876A File Offset: 0x0007696A
			public Color color
			{
				get
				{
					return this._color;
				}
			}

			// (get) Token: 0x06001D9B RID: 7579 RVA: 0x00078772 File Offset: 0x00076972
			public Font font
			{
				get
				{
					return this._font;
				}
			}

			// (get) Token: 0x06001D9C RID: 7580 RVA: 0x0007877A File Offset: 0x0007697A
			public ThemeSettings.FontStyleOverride style
			{
				get
				{
					return this._style;
				}
			}

			// (get) Token: 0x06001D9D RID: 7581 RVA: 0x00078782 File Offset: 0x00076982
			public float sizeMultiplier
			{
				get
				{
					return this._sizeMultiplier;
				}
			}

			// (get) Token: 0x06001D9E RID: 7582 RVA: 0x0007878A File Offset: 0x0007698A
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
