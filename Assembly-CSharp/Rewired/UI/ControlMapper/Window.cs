using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000483 RID: 1155
	[AddComponentMenu("")]
	[RequireComponent(typeof(CanvasGroup))]
	public class Window : MonoBehaviour
	{
		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001C58 RID: 7256 RVA: 0x00015AC7 File Offset: 0x00013CC7
		public bool hasFocus
		{
			get
			{
				return this._isFocusedCallback != null && this._isFocusedCallback(this._id);
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001C59 RID: 7257 RVA: 0x00015AE4 File Offset: 0x00013CE4
		public int id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001C5A RID: 7258 RVA: 0x00015AEC File Offset: 0x00013CEC
		public RectTransform rectTransform
		{
			get
			{
				if (this._rectTransform == null)
				{
					this._rectTransform = base.gameObject.GetComponent<RectTransform>();
				}
				return this._rectTransform;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001C5B RID: 7259 RVA: 0x00015B13 File Offset: 0x00013D13
		public Text titleText
		{
			get
			{
				return this._titleText;
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001C5C RID: 7260 RVA: 0x00015B1B File Offset: 0x00013D1B
		public List<Text> contentText
		{
			get
			{
				return this._contentText;
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06001C5D RID: 7261 RVA: 0x00015B23 File Offset: 0x00013D23
		// (set) Token: 0x06001C5E RID: 7262 RVA: 0x00015B2B File Offset: 0x00013D2B
		public GameObject defaultUIElement
		{
			get
			{
				return this._defaultUIElement;
			}
			set
			{
				this._defaultUIElement = value;
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001C5F RID: 7263 RVA: 0x00015B34 File Offset: 0x00013D34
		// (set) Token: 0x06001C60 RID: 7264 RVA: 0x00015B3C File Offset: 0x00013D3C
		public Action<int> updateCallback
		{
			get
			{
				return this._updateCallback;
			}
			set
			{
				this._updateCallback = value;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001C61 RID: 7265 RVA: 0x00015B45 File Offset: 0x00013D45
		public Window.Timer timer
		{
			get
			{
				return this._timer;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001C62 RID: 7266 RVA: 0x00015B4D File Offset: 0x00013D4D
		// (set) Token: 0x06001C63 RID: 7267 RVA: 0x0006F3FC File Offset: 0x0006D5FC
		public int width
		{
			get
			{
				return (int)this.rectTransform.sizeDelta.x;
			}
			set
			{
				Vector2 sizeDelta = this.rectTransform.sizeDelta;
				sizeDelta.x = (float)value;
				this.rectTransform.sizeDelta = sizeDelta;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001C64 RID: 7268 RVA: 0x00015B60 File Offset: 0x00013D60
		// (set) Token: 0x06001C65 RID: 7269 RVA: 0x0006F42C File Offset: 0x0006D62C
		public int height
		{
			get
			{
				return (int)this.rectTransform.sizeDelta.y;
			}
			set
			{
				Vector2 sizeDelta = this.rectTransform.sizeDelta;
				sizeDelta.y = (float)value;
				this.rectTransform.sizeDelta = sizeDelta;
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001C66 RID: 7270 RVA: 0x00015B73 File Offset: 0x00013D73
		protected bool initialized
		{
			get
			{
				return this._initialized;
			}
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x00015B7B File Offset: 0x00013D7B
		private void OnEnable()
		{
			base.StartCoroutine("OnEnableAsync");
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x00015B89 File Offset: 0x00013D89
		protected virtual void Update()
		{
			if (!this._initialized)
			{
				return;
			}
			if (!this.hasFocus)
			{
				return;
			}
			this.CheckUISelection();
			if (this._updateCallback != null)
			{
				this._updateCallback(this._id);
			}
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x0006F45C File Offset: 0x0006D65C
		public virtual void Initialize(int id, Func<int, bool> isFocusedCallback)
		{
			if (this._initialized)
			{
				Debug.LogError("Window is already initialized!");
				return;
			}
			this._id = id;
			this._isFocusedCallback = isFocusedCallback;
			this._timer = new Window.Timer();
			this._contentText = new List<Text>();
			this._canvasGroup = base.GetComponent<CanvasGroup>();
			this._initialized = true;
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x00015BBC File Offset: 0x00013DBC
		public void SetSize(int width, int height)
		{
			this.rectTransform.sizeDelta = new Vector2((float)width, (float)height);
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x00015BD2 File Offset: 0x00013DD2
		public void CreateTitleText(GameObject prefab, Vector2 offset)
		{
			this.CreateText(prefab, ref this._titleText, "Title Text", UIPivot.TopCenter, UIAnchor.TopHStretch, offset);
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x00015BF1 File Offset: 0x00013DF1
		public void CreateTitleText(GameObject prefab, Vector2 offset, string text)
		{
			this.CreateTitleText(prefab, offset);
			this.SetTitleText(text);
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x0006F4B4 File Offset: 0x0006D6B4
		public void AddContentText(GameObject prefab, UIPivot pivot, UIAnchor anchor, Vector2 offset)
		{
			Text text = null;
			this.CreateText(prefab, ref text, "Content Text", pivot, anchor, offset);
			this._contentText.Add(text);
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x00015C02 File Offset: 0x00013E02
		public void AddContentText(GameObject prefab, UIPivot pivot, UIAnchor anchor, Vector2 offset, string text)
		{
			this.AddContentText(prefab, pivot, anchor, offset);
			this.SetContentText(text, this._contentText.Count - 1);
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x00015C24 File Offset: 0x00013E24
		public void AddContentImage(GameObject prefab, UIPivot pivot, UIAnchor anchor, Vector2 offset)
		{
			this.CreateImage(prefab, "Image", pivot, anchor, offset);
		}

		// Token: 0x06001C70 RID: 7280 RVA: 0x00015C36 File Offset: 0x00013E36
		public void AddContentImage(GameObject prefab, UIPivot pivot, UIAnchor anchor, Vector2 offset, string text)
		{
			this.AddContentImage(prefab, pivot, anchor, offset);
		}

		// Token: 0x06001C71 RID: 7281 RVA: 0x0006F4E4 File Offset: 0x0006D6E4
		public void CreateButton(GameObject prefab, UIPivot pivot, UIAnchor anchor, Vector2 offset, string buttonText, UnityAction confirmCallback, UnityAction cancelCallback, bool setDefault)
		{
			if (prefab == null)
			{
				return;
			}
			ButtonInfo buttonInfo;
			GameObject gameObject = this.CreateButton(prefab, "Button", anchor, pivot, offset, out buttonInfo);
			if (gameObject == null)
			{
				return;
			}
			Button component = gameObject.GetComponent<Button>();
			if (confirmCallback != null)
			{
				component.onClick.AddListener(confirmCallback);
			}
			CustomButton customButton = component as CustomButton;
			if (cancelCallback != null && customButton != null)
			{
				customButton.CancelEvent += cancelCallback;
			}
			if (buttonInfo.text != null)
			{
				buttonInfo.text.text = buttonText;
			}
			if (setDefault)
			{
				this._defaultUIElement = gameObject;
			}
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x00015C43 File Offset: 0x00013E43
		public string GetTitleText(string text)
		{
			if (this._titleText == null)
			{
				return string.Empty;
			}
			return this._titleText.text;
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x00015C64 File Offset: 0x00013E64
		public void SetTitleText(string text)
		{
			if (this._titleText == null)
			{
				return;
			}
			this._titleText.text = text;
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x0006F574 File Offset: 0x0006D774
		public string GetContentText(int index)
		{
			if (this._contentText == null || this._contentText.Count <= index || this._contentText[index] == null)
			{
				return string.Empty;
			}
			return this._contentText[index].text;
		}

		// Token: 0x06001C75 RID: 7285 RVA: 0x0006F5C4 File Offset: 0x0006D7C4
		public float GetContentTextHeight(int index)
		{
			if (this._contentText == null || this._contentText.Count <= index || this._contentText[index] == null)
			{
				return 0f;
			}
			return this._contentText[index].rectTransform.sizeDelta.y;
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x00015C81 File Offset: 0x00013E81
		public void SetContentText(string text, int index)
		{
			if (this._contentText == null || this._contentText.Count <= index || this._contentText[index] == null)
			{
				return;
			}
			this._contentText[index].text = text;
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x00015CC0 File Offset: 0x00013EC0
		public void SetUpdateCallback(Action<int> callback)
		{
			this.updateCallback = callback;
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x00015CC9 File Offset: 0x00013EC9
		public virtual void TakeInputFocus()
		{
			if (EventSystem.current == null)
			{
				return;
			}
			EventSystem.current.SetSelectedGameObject(this._defaultUIElement);
			this.Enable();
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x00015CEF File Offset: 0x00013EEF
		public virtual void Enable()
		{
			this._canvasGroup.interactable = true;
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x00015CFD File Offset: 0x00013EFD
		public virtual void Disable()
		{
			this._canvasGroup.interactable = false;
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x00015D0B File Offset: 0x00013F0B
		public virtual void Cancel()
		{
			if (!this.initialized)
			{
				return;
			}
			if (this.cancelCallback != null)
			{
				this.cancelCallback.Invoke();
			}
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x0006F61C File Offset: 0x0006D81C
		private void CreateText(GameObject prefab, ref Text textComponent, string name, UIPivot pivot, UIAnchor anchor, Vector2 offset)
		{
			if (prefab == null || this.content == null)
			{
				return;
			}
			if (textComponent != null)
			{
				Debug.LogError("Window already has " + name + "!");
				return;
			}
			GameObject gameObject = UITools.InstantiateGUIObject<Text>(prefab, this.content.transform, name, pivot, anchor.min, anchor.max, offset);
			if (gameObject == null)
			{
				return;
			}
			textComponent = gameObject.GetComponent<Text>();
			if (textComponent == null)
			{
				textComponent = gameObject.GetComponentInChildren<Text>();
			}
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x0006F6B0 File Offset: 0x0006D8B0
		private void CreateImage(GameObject prefab, string name, UIPivot pivot, UIAnchor anchor, Vector2 offset)
		{
			if (prefab == null || this.content == null)
			{
				return;
			}
			UITools.InstantiateGUIObject<Image>(prefab, this.content.transform, name, pivot, anchor.min, anchor.max, offset);
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x0006F700 File Offset: 0x0006D900
		private GameObject CreateButton(GameObject prefab, string name, UIAnchor anchor, UIPivot pivot, Vector2 offset, out ButtonInfo buttonInfo)
		{
			buttonInfo = null;
			if (prefab == null)
			{
				return null;
			}
			GameObject gameObject = UITools.InstantiateGUIObject<ButtonInfo>(prefab, this.content.transform, name, pivot, anchor.min, anchor.max, offset);
			if (gameObject == null)
			{
				return null;
			}
			buttonInfo = gameObject.GetComponent<ButtonInfo>();
			if (gameObject.GetComponent<Button>() == null)
			{
				Debug.Log("Button prefab is missing Button component!");
				return null;
			}
			if (buttonInfo == null)
			{
				Debug.Log("Button prefab is missing ButtonInfo component!");
				return null;
			}
			return gameObject;
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x00015D29 File Offset: 0x00013F29
		private IEnumerator OnEnableAsync()
		{
			yield return 1;
			if (EventSystem.current == null)
			{
				yield break;
			}
			if (this.defaultUIElement != null)
			{
				EventSystem.current.SetSelectedGameObject(this.defaultUIElement);
			}
			else
			{
				EventSystem.current.SetSelectedGameObject(null);
			}
			yield break;
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x0006F78C File Offset: 0x0006D98C
		private void CheckUISelection()
		{
			if (!this.hasFocus)
			{
				return;
			}
			if (EventSystem.current == null)
			{
				return;
			}
			if (EventSystem.current.currentSelectedGameObject == null)
			{
				this.RestoreDefaultOrLastUISelection();
			}
			this.lastUISelection = EventSystem.current.currentSelectedGameObject;
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x00015D38 File Offset: 0x00013F38
		private void RestoreDefaultOrLastUISelection()
		{
			if (!this.hasFocus)
			{
				return;
			}
			if (this.lastUISelection == null || !this.lastUISelection.activeInHierarchy)
			{
				this.SetUISelection(this._defaultUIElement);
				return;
			}
			this.SetUISelection(this.lastUISelection);
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x0001332D File Offset: 0x0001152D
		private void SetUISelection(GameObject selection)
		{
			if (EventSystem.current == null)
			{
				return;
			}
			EventSystem.current.SetSelectedGameObject(selection);
		}

		// Token: 0x04001E14 RID: 7700
		public Image backgroundImage;

		// Token: 0x04001E15 RID: 7701
		public GameObject content;

		// Token: 0x04001E16 RID: 7702
		private bool _initialized;

		// Token: 0x04001E17 RID: 7703
		private int _id = -1;

		// Token: 0x04001E18 RID: 7704
		private RectTransform _rectTransform;

		// Token: 0x04001E19 RID: 7705
		private Text _titleText;

		// Token: 0x04001E1A RID: 7706
		private List<Text> _contentText;

		// Token: 0x04001E1B RID: 7707
		private GameObject _defaultUIElement;

		// Token: 0x04001E1C RID: 7708
		private Action<int> _updateCallback;

		// Token: 0x04001E1D RID: 7709
		private Func<int, bool> _isFocusedCallback;

		// Token: 0x04001E1E RID: 7710
		private Window.Timer _timer;

		// Token: 0x04001E1F RID: 7711
		private CanvasGroup _canvasGroup;

		// Token: 0x04001E20 RID: 7712
		public UnityAction cancelCallback;

		// Token: 0x04001E21 RID: 7713
		private GameObject lastUISelection;

		// Token: 0x02000484 RID: 1156
		public class Timer
		{
			// Token: 0x170005F6 RID: 1526
			// (get) Token: 0x06001C84 RID: 7300 RVA: 0x00015D86 File Offset: 0x00013F86
			public bool started
			{
				get
				{
					return this._started;
				}
			}

			// Token: 0x170005F7 RID: 1527
			// (get) Token: 0x06001C85 RID: 7301 RVA: 0x00015D8E File Offset: 0x00013F8E
			public bool finished
			{
				get
				{
					if (!this.started)
					{
						return false;
					}
					if (Time.realtimeSinceStartup < this.end)
					{
						return false;
					}
					this._started = false;
					return true;
				}
			}

			// Token: 0x170005F8 RID: 1528
			// (get) Token: 0x06001C86 RID: 7302 RVA: 0x00015DB1 File Offset: 0x00013FB1
			public float remaining
			{
				get
				{
					if (!this._started)
					{
						return 0f;
					}
					return this.end - Time.realtimeSinceStartup;
				}
			}

			// Token: 0x06001C88 RID: 7304 RVA: 0x00015DCD File Offset: 0x00013FCD
			public void Start(float length)
			{
				this.end = Time.realtimeSinceStartup + length;
				this._started = true;
			}

			// Token: 0x06001C89 RID: 7305 RVA: 0x00015DE3 File Offset: 0x00013FE3
			public void Stop()
			{
				this._started = false;
			}

			// Token: 0x04001E22 RID: 7714
			private bool _started;

			// Token: 0x04001E23 RID: 7715
			private float end;
		}
	}
}
