using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x0200046A RID: 1130
	[Serializable]
	public class ThemeSettings : ScriptableObject
	{
		// Token: 0x06001BC8 RID: 7112 RVA: 0x0006E170 File Offset: 0x0006C370
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

		// Token: 0x06001BC9 RID: 7113 RVA: 0x0006E1AC File Offset: 0x0006C3AC
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

		// Token: 0x06001BCA RID: 7114 RVA: 0x0006E22C File Offset: 0x0006C42C
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

		// Token: 0x06001BCB RID: 7115 RVA: 0x0006E2E0 File Offset: 0x0006C4E0
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

		// Token: 0x06001BCC RID: 7116 RVA: 0x0006E578 File Offset: 0x0006C778
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

		// Token: 0x06001BCD RID: 7117 RVA: 0x000155AC File Offset: 0x000137AC
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

		// Token: 0x06001BCE RID: 7118 RVA: 0x000155DB File Offset: 0x000137DB
		private static FontStyle GetFontStyle(ThemeSettings.FontStyleOverride style)
		{
			return style - ThemeSettings.FontStyleOverride.Normal;
		}

		// Token: 0x04001DB3 RID: 7603
		[SerializeField]
		private ThemeSettings.ImageSettings _mainWindowBackground;

		// Token: 0x04001DB4 RID: 7604
		[SerializeField]
		private ThemeSettings.ImageSettings _popupWindowBackground;

		// Token: 0x04001DB5 RID: 7605
		[SerializeField]
		private ThemeSettings.ImageSettings _areaBackground;

		// Token: 0x04001DB6 RID: 7606
		[SerializeField]
		private ThemeSettings.SelectableSettings _selectableSettings;

		// Token: 0x04001DB7 RID: 7607
		[SerializeField]
		private ThemeSettings.SelectableSettings _buttonSettings;

		// Token: 0x04001DB8 RID: 7608
		[SerializeField]
		private ThemeSettings.SelectableSettings _inputGridFieldSettings;

		// Token: 0x04001DB9 RID: 7609
		[SerializeField]
		private ThemeSettings.ScrollbarSettings _scrollbarSettings;

		// Token: 0x04001DBA RID: 7610
		[SerializeField]
		private ThemeSettings.SliderSettings _sliderSettings;

		// Token: 0x04001DBB RID: 7611
		[SerializeField]
		private ThemeSettings.ImageSettings _invertToggle;

		// Token: 0x04001DBC RID: 7612
		[SerializeField]
		private Color _invertToggleDisabledColor;

		// Token: 0x04001DBD RID: 7613
		[SerializeField]
		private ThemeSettings.ImageSettings _calibrationBackground;

		// Token: 0x04001DBE RID: 7614
		[SerializeField]
		private ThemeSettings.ImageSettings _calibrationValueMarker;

		// Token: 0x04001DBF RID: 7615
		[SerializeField]
		private ThemeSettings.ImageSettings _calibrationRawValueMarker;

		// Token: 0x04001DC0 RID: 7616
		[SerializeField]
		private ThemeSettings.ImageSettings _calibrationZeroMarker;

		// Token: 0x04001DC1 RID: 7617
		[SerializeField]
		private ThemeSettings.ImageSettings _calibrationCalibratedZeroMarker;

		// Token: 0x04001DC2 RID: 7618
		[SerializeField]
		private ThemeSettings.ImageSettings _calibrationDeadzone;

		// Token: 0x04001DC3 RID: 7619
		[SerializeField]
		private ThemeSettings.TextSettings _textSettings;

		// Token: 0x04001DC4 RID: 7620
		[SerializeField]
		private ThemeSettings.TextSettings _buttonTextSettings;

		// Token: 0x04001DC5 RID: 7621
		[SerializeField]
		private ThemeSettings.TextSettings _inputGridFieldTextSettings;

		// Token: 0x0200046B RID: 1131
		[Serializable]
		private abstract class SelectableSettings_Base
		{
			// Token: 0x170005B6 RID: 1462
			// (get) Token: 0x06001BD0 RID: 7120 RVA: 0x000155E0 File Offset: 0x000137E0
			public Selectable.Transition transition
			{
				get
				{
					return this._transition;
				}
			}

			// Token: 0x170005B7 RID: 1463
			// (get) Token: 0x06001BD1 RID: 7121 RVA: 0x000155E8 File Offset: 0x000137E8
			public ThemeSettings.CustomColorBlock selectableColors
			{
				get
				{
					return this._colors;
				}
			}

			// Token: 0x170005B8 RID: 1464
			// (get) Token: 0x06001BD2 RID: 7122 RVA: 0x000155F0 File Offset: 0x000137F0
			public ThemeSettings.CustomSpriteState spriteState
			{
				get
				{
					return this._spriteState;
				}
			}

			// Token: 0x170005B9 RID: 1465
			// (get) Token: 0x06001BD3 RID: 7123 RVA: 0x000155F8 File Offset: 0x000137F8
			public ThemeSettings.CustomAnimationTriggers animationTriggers
			{
				get
				{
					return this._animationTriggers;
				}
			}

			// Token: 0x06001BD4 RID: 7124 RVA: 0x0006E660 File Offset: 0x0006C860
			public virtual void Apply(Selectable item)
			{
				Selectable.Transition transition = this._transition;
				bool flag = item.transition != transition;
				item.transition = transition;
				ICustomSelectable customSelectable = item as ICustomSelectable;
				if (transition == 1)
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
				else if (transition == 2)
				{
					item.spriteState = this._spriteState;
					if (customSelectable != null)
					{
						customSelectable.disabledHighlightedSprite = this._spriteState.disabledHighlightedSprite;
					}
				}
				else if (transition == 3)
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

			// Token: 0x04001DC6 RID: 7622
			[SerializeField]
			protected Selectable.Transition _transition;

			// Token: 0x04001DC7 RID: 7623
			[SerializeField]
			protected ThemeSettings.CustomColorBlock _colors;

			// Token: 0x04001DC8 RID: 7624
			[SerializeField]
			protected ThemeSettings.CustomSpriteState _spriteState;

			// Token: 0x04001DC9 RID: 7625
			[SerializeField]
			protected ThemeSettings.CustomAnimationTriggers _animationTriggers;
		}

		// Token: 0x0200046C RID: 1132
		[Serializable]
		private class SelectableSettings : ThemeSettings.SelectableSettings_Base
		{
			// Token: 0x170005BA RID: 1466
			// (get) Token: 0x06001BD6 RID: 7126 RVA: 0x00015600 File Offset: 0x00013800
			public ThemeSettings.ImageSettings imageSettings
			{
				get
				{
					return this._imageSettings;
				}
			}

			// Token: 0x06001BD7 RID: 7127 RVA: 0x00015608 File Offset: 0x00013808
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

			// Token: 0x04001DCA RID: 7626
			[SerializeField]
			private ThemeSettings.ImageSettings _imageSettings;
		}

		// Token: 0x0200046D RID: 1133
		[Serializable]
		private class SliderSettings : ThemeSettings.SelectableSettings_Base
		{
			// Token: 0x170005BB RID: 1467
			// (get) Token: 0x06001BD9 RID: 7129 RVA: 0x00015641 File Offset: 0x00013841
			public ThemeSettings.ImageSettings handleImageSettings
			{
				get
				{
					return this._handleImageSettings;
				}
			}

			// Token: 0x170005BC RID: 1468
			// (get) Token: 0x06001BDA RID: 7130 RVA: 0x00015649 File Offset: 0x00013849
			public ThemeSettings.ImageSettings fillImageSettings
			{
				get
				{
					return this._fillImageSettings;
				}
			}

			// Token: 0x170005BD RID: 1469
			// (get) Token: 0x06001BDB RID: 7131 RVA: 0x00015651 File Offset: 0x00013851
			public ThemeSettings.ImageSettings backgroundImageSettings
			{
				get
				{
					return this._backgroundImageSettings;
				}
			}

			// Token: 0x06001BDC RID: 7132 RVA: 0x0006E7A4 File Offset: 0x0006C9A4
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

			// Token: 0x06001BDD RID: 7133 RVA: 0x00015659 File Offset: 0x00013859
			public override void Apply(Selectable item)
			{
				base.Apply(item);
				this.Apply(item as Slider);
			}

			// Token: 0x04001DCB RID: 7627
			[SerializeField]
			private ThemeSettings.ImageSettings _handleImageSettings;

			// Token: 0x04001DCC RID: 7628
			[SerializeField]
			private ThemeSettings.ImageSettings _fillImageSettings;

			// Token: 0x04001DCD RID: 7629
			[SerializeField]
			private ThemeSettings.ImageSettings _backgroundImageSettings;
		}

		// Token: 0x0200046E RID: 1134
		[Serializable]
		private class ScrollbarSettings : ThemeSettings.SelectableSettings_Base
		{
			// Token: 0x170005BE RID: 1470
			// (get) Token: 0x06001BDF RID: 7135 RVA: 0x0001566E File Offset: 0x0001386E
			public ThemeSettings.ImageSettings handle
			{
				get
				{
					return this._handleImageSettings;
				}
			}

			// Token: 0x170005BF RID: 1471
			// (get) Token: 0x06001BE0 RID: 7136 RVA: 0x00015676 File Offset: 0x00013876
			public ThemeSettings.ImageSettings background
			{
				get
				{
					return this._backgroundImageSettings;
				}
			}

			// Token: 0x06001BE1 RID: 7137 RVA: 0x0006E838 File Offset: 0x0006CA38
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

			// Token: 0x06001BE2 RID: 7138 RVA: 0x0001567E File Offset: 0x0001387E
			public override void Apply(Selectable item)
			{
				base.Apply(item);
				this.Apply(item as Scrollbar);
			}

			// Token: 0x04001DCE RID: 7630
			[SerializeField]
			private ThemeSettings.ImageSettings _handleImageSettings;

			// Token: 0x04001DCF RID: 7631
			[SerializeField]
			private ThemeSettings.ImageSettings _backgroundImageSettings;
		}

		// Token: 0x0200046F RID: 1135
		[Serializable]
		private class ImageSettings
		{
			// Token: 0x170005C0 RID: 1472
			// (get) Token: 0x06001BE4 RID: 7140 RVA: 0x00015693 File Offset: 0x00013893
			public Color color
			{
				get
				{
					return this._color;
				}
			}

			// Token: 0x170005C1 RID: 1473
			// (get) Token: 0x06001BE5 RID: 7141 RVA: 0x0001569B File Offset: 0x0001389B
			public Sprite sprite
			{
				get
				{
					return this._sprite;
				}
			}

			// Token: 0x170005C2 RID: 1474
			// (get) Token: 0x06001BE6 RID: 7142 RVA: 0x000156A3 File Offset: 0x000138A3
			public Material materal
			{
				get
				{
					return this._materal;
				}
			}

			// Token: 0x170005C3 RID: 1475
			// (get) Token: 0x06001BE7 RID: 7143 RVA: 0x000156AB File Offset: 0x000138AB
			public Image.Type type
			{
				get
				{
					return this._type;
				}
			}

			// Token: 0x170005C4 RID: 1476
			// (get) Token: 0x06001BE8 RID: 7144 RVA: 0x000156B3 File Offset: 0x000138B3
			public bool preserveAspect
			{
				get
				{
					return this._preserveAspect;
				}
			}

			// Token: 0x170005C5 RID: 1477
			// (get) Token: 0x06001BE9 RID: 7145 RVA: 0x000156BB File Offset: 0x000138BB
			public bool fillCenter
			{
				get
				{
					return this._fillCenter;
				}
			}

			// Token: 0x170005C6 RID: 1478
			// (get) Token: 0x06001BEA RID: 7146 RVA: 0x000156C3 File Offset: 0x000138C3
			public Image.FillMethod fillMethod
			{
				get
				{
					return this._fillMethod;
				}
			}

			// Token: 0x170005C7 RID: 1479
			// (get) Token: 0x06001BEB RID: 7147 RVA: 0x000156CB File Offset: 0x000138CB
			public float fillAmout
			{
				get
				{
					return this._fillAmout;
				}
			}

			// Token: 0x170005C8 RID: 1480
			// (get) Token: 0x06001BEC RID: 7148 RVA: 0x000156D3 File Offset: 0x000138D3
			public bool fillClockwise
			{
				get
				{
					return this._fillClockwise;
				}
			}

			// Token: 0x170005C9 RID: 1481
			// (get) Token: 0x06001BED RID: 7149 RVA: 0x000156DB File Offset: 0x000138DB
			public int fillOrigin
			{
				get
				{
					return this._fillOrigin;
				}
			}

			// Token: 0x06001BEE RID: 7150 RVA: 0x0006E888 File Offset: 0x0006CA88
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

			// Token: 0x04001DD0 RID: 7632
			[SerializeField]
			private Color _color = Color.white;

			// Token: 0x04001DD1 RID: 7633
			[SerializeField]
			private Sprite _sprite;

			// Token: 0x04001DD2 RID: 7634
			[SerializeField]
			private Material _materal;

			// Token: 0x04001DD3 RID: 7635
			[SerializeField]
			private Image.Type _type;

			// Token: 0x04001DD4 RID: 7636
			[SerializeField]
			private bool _preserveAspect;

			// Token: 0x04001DD5 RID: 7637
			[SerializeField]
			private bool _fillCenter;

			// Token: 0x04001DD6 RID: 7638
			[SerializeField]
			private Image.FillMethod _fillMethod;

			// Token: 0x04001DD7 RID: 7639
			[SerializeField]
			private float _fillAmout;

			// Token: 0x04001DD8 RID: 7640
			[SerializeField]
			private bool _fillClockwise;

			// Token: 0x04001DD9 RID: 7641
			[SerializeField]
			private int _fillOrigin;
		}

		// Token: 0x02000470 RID: 1136
		[Serializable]
		private struct CustomColorBlock
		{
			// Token: 0x170005CA RID: 1482
			// (get) Token: 0x06001BF0 RID: 7152 RVA: 0x000156F6 File Offset: 0x000138F6
			// (set) Token: 0x06001BF1 RID: 7153 RVA: 0x000156FE File Offset: 0x000138FE
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

			// Token: 0x170005CB RID: 1483
			// (get) Token: 0x06001BF2 RID: 7154 RVA: 0x00015707 File Offset: 0x00013907
			// (set) Token: 0x06001BF3 RID: 7155 RVA: 0x0001570F File Offset: 0x0001390F
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

			// Token: 0x170005CC RID: 1484
			// (get) Token: 0x06001BF4 RID: 7156 RVA: 0x00015718 File Offset: 0x00013918
			// (set) Token: 0x06001BF5 RID: 7157 RVA: 0x00015720 File Offset: 0x00013920
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

			// Token: 0x170005CD RID: 1485
			// (get) Token: 0x06001BF6 RID: 7158 RVA: 0x00015729 File Offset: 0x00013929
			// (set) Token: 0x06001BF7 RID: 7159 RVA: 0x00015731 File Offset: 0x00013931
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

			// Token: 0x170005CE RID: 1486
			// (get) Token: 0x06001BF8 RID: 7160 RVA: 0x0001573A File Offset: 0x0001393A
			// (set) Token: 0x06001BF9 RID: 7161 RVA: 0x00015742 File Offset: 0x00013942
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

			// Token: 0x170005CF RID: 1487
			// (get) Token: 0x06001BFA RID: 7162 RVA: 0x0001574B File Offset: 0x0001394B
			// (set) Token: 0x06001BFB RID: 7163 RVA: 0x00015753 File Offset: 0x00013953
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

			// Token: 0x170005D0 RID: 1488
			// (get) Token: 0x06001BFC RID: 7164 RVA: 0x0001575C File Offset: 0x0001395C
			// (set) Token: 0x06001BFD RID: 7165 RVA: 0x00015764 File Offset: 0x00013964
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

			// Token: 0x170005D1 RID: 1489
			// (get) Token: 0x06001BFE RID: 7166 RVA: 0x0001576D File Offset: 0x0001396D
			// (set) Token: 0x06001BFF RID: 7167 RVA: 0x00015775 File Offset: 0x00013975
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

			// Token: 0x06001C00 RID: 7168 RVA: 0x0006E918 File Offset: 0x0006CB18
			public static implicit operator ColorBlock(ThemeSettings.CustomColorBlock item)
			{
				ColorBlock colorBlock = default(ColorBlock);
				colorBlock.selectedColor = item.m_SelectedColor;
				colorBlock.colorMultiplier = item.m_ColorMultiplier;
				colorBlock.disabledColor = item.m_DisabledColor;
				colorBlock.fadeDuration = item.m_FadeDuration;
				colorBlock.highlightedColor = item.m_HighlightedColor;
				colorBlock.normalColor = item.m_NormalColor;
				colorBlock.pressedColor = item.m_PressedColor;
				return colorBlock;
			}

			// Token: 0x04001DDA RID: 7642
			[SerializeField]
			private float m_ColorMultiplier;

			// Token: 0x04001DDB RID: 7643
			[SerializeField]
			private Color m_DisabledColor;

			// Token: 0x04001DDC RID: 7644
			[SerializeField]
			private float m_FadeDuration;

			// Token: 0x04001DDD RID: 7645
			[SerializeField]
			private Color m_HighlightedColor;

			// Token: 0x04001DDE RID: 7646
			[SerializeField]
			private Color m_NormalColor;

			// Token: 0x04001DDF RID: 7647
			[SerializeField]
			private Color m_PressedColor;

			// Token: 0x04001DE0 RID: 7648
			[SerializeField]
			private Color m_SelectedColor;

			// Token: 0x04001DE1 RID: 7649
			[SerializeField]
			private Color m_DisabledHighlightedColor;
		}

		// Token: 0x02000471 RID: 1137
		[Serializable]
		private struct CustomSpriteState
		{
			// Token: 0x170005D2 RID: 1490
			// (get) Token: 0x06001C01 RID: 7169 RVA: 0x0001577E File Offset: 0x0001397E
			// (set) Token: 0x06001C02 RID: 7170 RVA: 0x00015786 File Offset: 0x00013986
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

			// Token: 0x170005D3 RID: 1491
			// (get) Token: 0x06001C03 RID: 7171 RVA: 0x0001578F File Offset: 0x0001398F
			// (set) Token: 0x06001C04 RID: 7172 RVA: 0x00015797 File Offset: 0x00013997
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

			// Token: 0x170005D4 RID: 1492
			// (get) Token: 0x06001C05 RID: 7173 RVA: 0x000157A0 File Offset: 0x000139A0
			// (set) Token: 0x06001C06 RID: 7174 RVA: 0x000157A8 File Offset: 0x000139A8
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

			// Token: 0x170005D5 RID: 1493
			// (get) Token: 0x06001C07 RID: 7175 RVA: 0x000157B1 File Offset: 0x000139B1
			// (set) Token: 0x06001C08 RID: 7176 RVA: 0x000157B9 File Offset: 0x000139B9
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

			// Token: 0x170005D6 RID: 1494
			// (get) Token: 0x06001C09 RID: 7177 RVA: 0x000157C2 File Offset: 0x000139C2
			// (set) Token: 0x06001C0A RID: 7178 RVA: 0x000157CA File Offset: 0x000139CA
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

			// Token: 0x06001C0B RID: 7179 RVA: 0x0006E98C File Offset: 0x0006CB8C
			public static implicit operator SpriteState(ThemeSettings.CustomSpriteState item)
			{
				SpriteState spriteState = default(SpriteState);
				spriteState.selectedSprite = item.m_SelectedSprite;
				spriteState.disabledSprite = item.m_DisabledSprite;
				spriteState.highlightedSprite = item.m_HighlightedSprite;
				spriteState.pressedSprite = item.m_PressedSprite;
				return spriteState;
			}

			// Token: 0x04001DE2 RID: 7650
			[SerializeField]
			private Sprite m_DisabledSprite;

			// Token: 0x04001DE3 RID: 7651
			[SerializeField]
			private Sprite m_HighlightedSprite;

			// Token: 0x04001DE4 RID: 7652
			[SerializeField]
			private Sprite m_PressedSprite;

			// Token: 0x04001DE5 RID: 7653
			[SerializeField]
			private Sprite m_SelectedSprite;

			// Token: 0x04001DE6 RID: 7654
			[SerializeField]
			private Sprite m_DisabledHighlightedSprite;
		}

		// Token: 0x02000472 RID: 1138
		[Serializable]
		private class CustomAnimationTriggers
		{
			// Token: 0x06001C0C RID: 7180 RVA: 0x0006E9D8 File Offset: 0x0006CBD8
			public CustomAnimationTriggers()
			{
				this.m_DisabledTrigger = string.Empty;
				this.m_HighlightedTrigger = string.Empty;
				this.m_NormalTrigger = string.Empty;
				this.m_PressedTrigger = string.Empty;
				this.m_SelectedTrigger = string.Empty;
				this.m_DisabledHighlightedTrigger = string.Empty;
			}

			// Token: 0x170005D7 RID: 1495
			// (get) Token: 0x06001C0D RID: 7181 RVA: 0x000157D3 File Offset: 0x000139D3
			// (set) Token: 0x06001C0E RID: 7182 RVA: 0x000157DB File Offset: 0x000139DB
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

			// Token: 0x170005D8 RID: 1496
			// (get) Token: 0x06001C0F RID: 7183 RVA: 0x000157E4 File Offset: 0x000139E4
			// (set) Token: 0x06001C10 RID: 7184 RVA: 0x000157EC File Offset: 0x000139EC
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

			// Token: 0x170005D9 RID: 1497
			// (get) Token: 0x06001C11 RID: 7185 RVA: 0x000157F5 File Offset: 0x000139F5
			// (set) Token: 0x06001C12 RID: 7186 RVA: 0x000157FD File Offset: 0x000139FD
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

			// Token: 0x170005DA RID: 1498
			// (get) Token: 0x06001C13 RID: 7187 RVA: 0x00015806 File Offset: 0x00013A06
			// (set) Token: 0x06001C14 RID: 7188 RVA: 0x0001580E File Offset: 0x00013A0E
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

			// Token: 0x170005DB RID: 1499
			// (get) Token: 0x06001C15 RID: 7189 RVA: 0x00015817 File Offset: 0x00013A17
			// (set) Token: 0x06001C16 RID: 7190 RVA: 0x0001581F File Offset: 0x00013A1F
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

			// Token: 0x170005DC RID: 1500
			// (get) Token: 0x06001C17 RID: 7191 RVA: 0x00015828 File Offset: 0x00013A28
			// (set) Token: 0x06001C18 RID: 7192 RVA: 0x00015830 File Offset: 0x00013A30
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

			// Token: 0x06001C19 RID: 7193 RVA: 0x0006EA30 File Offset: 0x0006CC30
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

			// Token: 0x04001DE7 RID: 7655
			[SerializeField]
			private string m_DisabledTrigger;

			// Token: 0x04001DE8 RID: 7656
			[SerializeField]
			private string m_HighlightedTrigger;

			// Token: 0x04001DE9 RID: 7657
			[SerializeField]
			private string m_NormalTrigger;

			// Token: 0x04001DEA RID: 7658
			[SerializeField]
			private string m_PressedTrigger;

			// Token: 0x04001DEB RID: 7659
			[SerializeField]
			private string m_SelectedTrigger;

			// Token: 0x04001DEC RID: 7660
			[SerializeField]
			private string m_DisabledHighlightedTrigger;
		}

		// Token: 0x02000473 RID: 1139
		[Serializable]
		private class TextSettings
		{
			// Token: 0x170005DD RID: 1501
			// (get) Token: 0x06001C1A RID: 7194 RVA: 0x00015839 File Offset: 0x00013A39
			public Color color
			{
				get
				{
					return this._color;
				}
			}

			// Token: 0x170005DE RID: 1502
			// (get) Token: 0x06001C1B RID: 7195 RVA: 0x00015841 File Offset: 0x00013A41
			public Font font
			{
				get
				{
					return this._font;
				}
			}

			// Token: 0x170005DF RID: 1503
			// (get) Token: 0x06001C1C RID: 7196 RVA: 0x00015849 File Offset: 0x00013A49
			public ThemeSettings.FontStyleOverride style
			{
				get
				{
					return this._style;
				}
			}

			// Token: 0x170005E0 RID: 1504
			// (get) Token: 0x06001C1D RID: 7197 RVA: 0x00015851 File Offset: 0x00013A51
			public float sizeMultiplier
			{
				get
				{
					return this._sizeMultiplier;
				}
			}

			// Token: 0x170005E1 RID: 1505
			// (get) Token: 0x06001C1E RID: 7198 RVA: 0x00015859 File Offset: 0x00013A59
			public float lineSpacing
			{
				get
				{
					return this._lineSpacing;
				}
			}

			// Token: 0x04001DED RID: 7661
			[SerializeField]
			private Color _color = Color.white;

			// Token: 0x04001DEE RID: 7662
			[SerializeField]
			private Font _font;

			// Token: 0x04001DEF RID: 7663
			[SerializeField]
			private ThemeSettings.FontStyleOverride _style;

			// Token: 0x04001DF0 RID: 7664
			[SerializeField]
			private float _sizeMultiplier = 1f;

			// Token: 0x04001DF1 RID: 7665
			[SerializeField]
			private float _lineSpacing = 1f;
		}

		// Token: 0x02000474 RID: 1140
		private enum FontStyleOverride
		{
			// Token: 0x04001DF3 RID: 7667
			Default,
			// Token: 0x04001DF4 RID: 7668
			Normal,
			// Token: 0x04001DF5 RID: 7669
			Bold,
			// Token: 0x04001DF6 RID: 7670
			Italic,
			// Token: 0x04001DF7 RID: 7671
			BoldAndItalic
		}
	}
}
