using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	[AddComponentMenu("")]
	[RequireComponent(typeof(CanvasGroup))]
	public class Window : MonoBehaviour
	{
		// (get) Token: 0x06001CB8 RID: 7352 RVA: 0x00015EFD File Offset: 0x000140FD
		public bool hasFocus
		{
			get
			{
				return this._isFocusedCallback != null && this._isFocusedCallback(this._id);
			}
		}

		// (get) Token: 0x06001CB9 RID: 7353 RVA: 0x00015F1A File Offset: 0x0001411A
		public int id
		{
			get
			{
				return this._id;
			}
		}

		// (get) Token: 0x06001CBA RID: 7354 RVA: 0x00015F22 File Offset: 0x00014122
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

		// (get) Token: 0x06001CBB RID: 7355 RVA: 0x00015F49 File Offset: 0x00014149
		public Text titleText
		{
			get
			{
				return this._titleText;
			}
		}

		// (get) Token: 0x06001CBC RID: 7356 RVA: 0x00015F51 File Offset: 0x00014151
		public List<Text> contentText
		{
			get
			{
				return this._contentText;
			}
		}

		// (get) Token: 0x06001CBD RID: 7357 RVA: 0x00015F59 File Offset: 0x00014159
		// (set) Token: 0x06001CBE RID: 7358 RVA: 0x00015F61 File Offset: 0x00014161
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

		// (get) Token: 0x06001CBF RID: 7359 RVA: 0x00015F6A File Offset: 0x0001416A
		// (set) Token: 0x06001CC0 RID: 7360 RVA: 0x00015F72 File Offset: 0x00014172
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

		// (get) Token: 0x06001CC1 RID: 7361 RVA: 0x00015F7B File Offset: 0x0001417B
		public Window.Timer timer
		{
			get
			{
				return this._timer;
			}
		}

		// (get) Token: 0x06001CC2 RID: 7362 RVA: 0x00015F83 File Offset: 0x00014183
		// (set) Token: 0x06001CC3 RID: 7363 RVA: 0x000713A8 File Offset: 0x0006F5A8
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

		// (get) Token: 0x06001CC4 RID: 7364 RVA: 0x00015F96 File Offset: 0x00014196
		// (set) Token: 0x06001CC5 RID: 7365 RVA: 0x000713D8 File Offset: 0x0006F5D8
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

		// (get) Token: 0x06001CC6 RID: 7366 RVA: 0x00015FA9 File Offset: 0x000141A9
		protected bool initialized
		{
			get
			{
				return this._initialized;
			}
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x00015FB1 File Offset: 0x000141B1
		private void OnEnable()
		{
			base.StartCoroutine("OnEnableAsync");
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x00015FBF File Offset: 0x000141BF
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

		// Token: 0x06001CC9 RID: 7369 RVA: 0x00071408 File Offset: 0x0006F608
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

		// Token: 0x06001CCA RID: 7370 RVA: 0x00015FF2 File Offset: 0x000141F2
		public void SetSize(int width, int height)
		{
			this.rectTransform.sizeDelta = new Vector2((float)width, (float)height);
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x00016008 File Offset: 0x00014208
		public void CreateTitleText(GameObject prefab, Vector2 offset)
		{
			this.CreateText(prefab, ref this._titleText, "Title Text", UIPivot.TopCenter, UIAnchor.TopHStretch, offset);
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x00016027 File Offset: 0x00014227
		public void CreateTitleText(GameObject prefab, Vector2 offset, string text)
		{
			this.CreateTitleText(prefab, offset);
			this.SetTitleText(text);
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x00071460 File Offset: 0x0006F660
		public void AddContentText(GameObject prefab, UIPivot pivot, UIAnchor anchor, Vector2 offset)
		{
			Text text = null;
			this.CreateText(prefab, ref text, "Content Text", pivot, anchor, offset);
			this._contentText.Add(text);
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x00016038 File Offset: 0x00014238
		public void AddContentText(GameObject prefab, UIPivot pivot, UIAnchor anchor, Vector2 offset, string text)
		{
			this.AddContentText(prefab, pivot, anchor, offset);
			this.SetContentText(text, this._contentText.Count - 1);
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x0001605A File Offset: 0x0001425A
		public void AddContentImage(GameObject prefab, UIPivot pivot, UIAnchor anchor, Vector2 offset)
		{
			this.CreateImage(prefab, "Image", pivot, anchor, offset);
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x0001606C File Offset: 0x0001426C
		public void AddContentImage(GameObject prefab, UIPivot pivot, UIAnchor anchor, Vector2 offset, string text)
		{
			this.AddContentImage(prefab, pivot, anchor, offset);
		}

		// Token: 0x06001CD1 RID: 7377 RVA: 0x00071490 File Offset: 0x0006F690
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

		// Token: 0x06001CD2 RID: 7378 RVA: 0x00016079 File Offset: 0x00014279
		public string GetTitleText(string text)
		{
			if (this._titleText == null)
			{
				return string.Empty;
			}
			return this._titleText.text;
		}

		// Token: 0x06001CD3 RID: 7379 RVA: 0x0001609A File Offset: 0x0001429A
		public void SetTitleText(string text)
		{
			if (this._titleText == null)
			{
				return;
			}
			this._titleText.text = text;
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x00071520 File Offset: 0x0006F720
		public string GetContentText(int index)
		{
			if (this._contentText == null || this._contentText.Count <= index || this._contentText[index] == null)
			{
				return string.Empty;
			}
			return this._contentText[index].text;
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x00071570 File Offset: 0x0006F770
		public float GetContentTextHeight(int index)
		{
			if (this._contentText == null || this._contentText.Count <= index || this._contentText[index] == null)
			{
				return 0f;
			}
			return this._contentText[index].rectTransform.sizeDelta.y;
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x000160B7 File Offset: 0x000142B7
		public void SetContentText(string text, int index)
		{
			if (this._contentText == null || this._contentText.Count <= index || this._contentText[index] == null)
			{
				return;
			}
			this._contentText[index].text = text;
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x000160F6 File Offset: 0x000142F6
		public void SetUpdateCallback(Action<int> callback)
		{
			this.updateCallback = callback;
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x000160FF File Offset: 0x000142FF
		public virtual void TakeInputFocus()
		{
			if (EventSystem.current == null)
			{
				return;
			}
			EventSystem.current.SetSelectedGameObject(this._defaultUIElement);
			this.Enable();
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x00016125 File Offset: 0x00014325
		public virtual void Enable()
		{
			this._canvasGroup.interactable = true;
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x00016133 File Offset: 0x00014333
		public virtual void Disable()
		{
			this._canvasGroup.interactable = false;
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x00016141 File Offset: 0x00014341
		public virtual void Cancel()
		{
			if (!this.initialized)
			{
				return;
			}
			if (this.cancelCallback != null)
			{
				this.cancelCallback();
			}
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x000715C8 File Offset: 0x0006F7C8
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

		// Token: 0x06001CDD RID: 7389 RVA: 0x0007165C File Offset: 0x0006F85C
		private void CreateImage(GameObject prefab, string name, UIPivot pivot, UIAnchor anchor, Vector2 offset)
		{
			if (prefab == null || this.content == null)
			{
				return;
			}
			UITools.InstantiateGUIObject<Image>(prefab, this.content.transform, name, pivot, anchor.min, anchor.max, offset);
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x000716AC File Offset: 0x0006F8AC
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

		// Token: 0x06001CDF RID: 7391 RVA: 0x0001615F File Offset: 0x0001435F
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

		// Token: 0x06001CE0 RID: 7392 RVA: 0x00071738 File Offset: 0x0006F938
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

		// Token: 0x06001CE1 RID: 7393 RVA: 0x0001616E File Offset: 0x0001436E
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

		// Token: 0x06001CE2 RID: 7394 RVA: 0x0001372A File Offset: 0x0001192A
		private void SetUISelection(GameObject selection)
		{
			if (EventSystem.current == null)
			{
				return;
			}
			EventSystem.current.SetSelectedGameObject(selection);
		}

		public Image backgroundImage;

		public GameObject content;

		private bool _initialized;

		private int _id = -1;

		private RectTransform _rectTransform;

		private Text _titleText;

		private List<Text> _contentText;

		private GameObject _defaultUIElement;

		private Action<int> _updateCallback;

		private Func<int, bool> _isFocusedCallback;

		private Window.Timer _timer;

		private CanvasGroup _canvasGroup;

		public UnityAction cancelCallback;

		private GameObject lastUISelection;

		public class Timer
		{
			// (get) Token: 0x06001CE4 RID: 7396 RVA: 0x000161BC File Offset: 0x000143BC
			public bool started
			{
				get
				{
					return this._started;
				}
			}

			// (get) Token: 0x06001CE5 RID: 7397 RVA: 0x000161C4 File Offset: 0x000143C4
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

			// (get) Token: 0x06001CE6 RID: 7398 RVA: 0x000161E7 File Offset: 0x000143E7
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

			// Token: 0x06001CE8 RID: 7400 RVA: 0x00016203 File Offset: 0x00014403
			public void Start(float length)
			{
				this.end = Time.realtimeSinceStartup + length;
				this._started = true;
			}

			// Token: 0x06001CE9 RID: 7401 RVA: 0x00016219 File Offset: 0x00014419
			public void Stop()
			{
				this._started = false;
			}

			private bool _started;

			private float end;
		}
	}
}
