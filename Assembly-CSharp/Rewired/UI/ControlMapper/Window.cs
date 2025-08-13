using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000335 RID: 821
	[AddComponentMenu("")]
	[RequireComponent(typeof(CanvasGroup))]
	public class Window : MonoBehaviour
	{
		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x060016C9 RID: 5833 RVA: 0x0005F8CB File Offset: 0x0005DACB
		public bool hasFocus
		{
			get
			{
				return this._isFocusedCallback != null && this._isFocusedCallback(this._id);
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x060016CA RID: 5834 RVA: 0x0005F8E8 File Offset: 0x0005DAE8
		public int id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x060016CB RID: 5835 RVA: 0x0005F8F0 File Offset: 0x0005DAF0
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

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x060016CC RID: 5836 RVA: 0x0005F917 File Offset: 0x0005DB17
		public Text titleText
		{
			get
			{
				return this._titleText;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x060016CD RID: 5837 RVA: 0x0005F91F File Offset: 0x0005DB1F
		public List<Text> contentText
		{
			get
			{
				return this._contentText;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x060016CE RID: 5838 RVA: 0x0005F927 File Offset: 0x0005DB27
		// (set) Token: 0x060016CF RID: 5839 RVA: 0x0005F92F File Offset: 0x0005DB2F
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

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x060016D0 RID: 5840 RVA: 0x0005F938 File Offset: 0x0005DB38
		// (set) Token: 0x060016D1 RID: 5841 RVA: 0x0005F940 File Offset: 0x0005DB40
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

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x060016D2 RID: 5842 RVA: 0x0005F949 File Offset: 0x0005DB49
		public Window.Timer timer
		{
			get
			{
				return this._timer;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x060016D3 RID: 5843 RVA: 0x0005F951 File Offset: 0x0005DB51
		// (set) Token: 0x060016D4 RID: 5844 RVA: 0x0005F964 File Offset: 0x0005DB64
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

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x060016D5 RID: 5845 RVA: 0x0005F992 File Offset: 0x0005DB92
		// (set) Token: 0x060016D6 RID: 5846 RVA: 0x0005F9A8 File Offset: 0x0005DBA8
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

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x060016D7 RID: 5847 RVA: 0x0005F9D6 File Offset: 0x0005DBD6
		protected bool initialized
		{
			get
			{
				return this._initialized;
			}
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x0005F9DE File Offset: 0x0005DBDE
		private void OnEnable()
		{
			base.StartCoroutine("OnEnableAsync");
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x0005F9EC File Offset: 0x0005DBEC
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

		// Token: 0x060016DA RID: 5850 RVA: 0x0005FA20 File Offset: 0x0005DC20
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

		// Token: 0x060016DB RID: 5851 RVA: 0x0005FA77 File Offset: 0x0005DC77
		public void SetSize(int width, int height)
		{
			this.rectTransform.sizeDelta = new Vector2((float)width, (float)height);
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x0005FA8D File Offset: 0x0005DC8D
		public void CreateTitleText(GameObject prefab, Vector2 offset)
		{
			this.CreateText(prefab, ref this._titleText, "Title Text", UIPivot.TopCenter, UIAnchor.TopHStretch, offset);
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x0005FAAC File Offset: 0x0005DCAC
		public void CreateTitleText(GameObject prefab, Vector2 offset, string text)
		{
			this.CreateTitleText(prefab, offset);
			this.SetTitleText(text);
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x0005FAC0 File Offset: 0x0005DCC0
		public void AddContentText(GameObject prefab, UIPivot pivot, UIAnchor anchor, Vector2 offset)
		{
			Text text = null;
			this.CreateText(prefab, ref text, "Content Text", pivot, anchor, offset);
			this._contentText.Add(text);
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x0005FAED File Offset: 0x0005DCED
		public void AddContentText(GameObject prefab, UIPivot pivot, UIAnchor anchor, Vector2 offset, string text)
		{
			this.AddContentText(prefab, pivot, anchor, offset);
			this.SetContentText(text, this._contentText.Count - 1);
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x0005FB0F File Offset: 0x0005DD0F
		public void AddContentImage(GameObject prefab, UIPivot pivot, UIAnchor anchor, Vector2 offset)
		{
			this.CreateImage(prefab, "Image", pivot, anchor, offset);
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x0005FB21 File Offset: 0x0005DD21
		public void AddContentImage(GameObject prefab, UIPivot pivot, UIAnchor anchor, Vector2 offset, string text)
		{
			this.AddContentImage(prefab, pivot, anchor, offset);
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x0005FB30 File Offset: 0x0005DD30
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

		// Token: 0x060016E3 RID: 5859 RVA: 0x0005FBBE File Offset: 0x0005DDBE
		public string GetTitleText(string text)
		{
			if (this._titleText == null)
			{
				return string.Empty;
			}
			return this._titleText.text;
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x0005FBDF File Offset: 0x0005DDDF
		public void SetTitleText(string text)
		{
			if (this._titleText == null)
			{
				return;
			}
			this._titleText.text = text;
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x0005FBFC File Offset: 0x0005DDFC
		public string GetContentText(int index)
		{
			if (this._contentText == null || this._contentText.Count <= index || this._contentText[index] == null)
			{
				return string.Empty;
			}
			return this._contentText[index].text;
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x0005FC4C File Offset: 0x0005DE4C
		public float GetContentTextHeight(int index)
		{
			if (this._contentText == null || this._contentText.Count <= index || this._contentText[index] == null)
			{
				return 0f;
			}
			return this._contentText[index].rectTransform.sizeDelta.y;
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x0005FCA4 File Offset: 0x0005DEA4
		public void SetContentText(string text, int index)
		{
			if (this._contentText == null || this._contentText.Count <= index || this._contentText[index] == null)
			{
				return;
			}
			this._contentText[index].text = text;
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x0005FCE3 File Offset: 0x0005DEE3
		public void SetUpdateCallback(Action<int> callback)
		{
			this.updateCallback = callback;
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x0005FCEC File Offset: 0x0005DEEC
		public virtual void TakeInputFocus()
		{
			if (EventSystem.current == null)
			{
				return;
			}
			EventSystem.current.SetSelectedGameObject(this._defaultUIElement);
			this.Enable();
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x0005FD12 File Offset: 0x0005DF12
		public virtual void Enable()
		{
			this._canvasGroup.interactable = true;
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x0005FD20 File Offset: 0x0005DF20
		public virtual void Disable()
		{
			this._canvasGroup.interactable = false;
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x0005FD2E File Offset: 0x0005DF2E
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

		// Token: 0x060016ED RID: 5869 RVA: 0x0005FD4C File Offset: 0x0005DF4C
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

		// Token: 0x060016EE RID: 5870 RVA: 0x0005FDE0 File Offset: 0x0005DFE0
		private void CreateImage(GameObject prefab, string name, UIPivot pivot, UIAnchor anchor, Vector2 offset)
		{
			if (prefab == null || this.content == null)
			{
				return;
			}
			UITools.InstantiateGUIObject<Image>(prefab, this.content.transform, name, pivot, anchor.min, anchor.max, offset);
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x0005FE30 File Offset: 0x0005E030
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

		// Token: 0x060016F0 RID: 5872 RVA: 0x0005FEBA File Offset: 0x0005E0BA
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

		// Token: 0x060016F1 RID: 5873 RVA: 0x0005FECC File Offset: 0x0005E0CC
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

		// Token: 0x060016F2 RID: 5874 RVA: 0x0005FF18 File Offset: 0x0005E118
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

		// Token: 0x060016F3 RID: 5875 RVA: 0x0005FF57 File Offset: 0x0005E157
		private void SetUISelection(GameObject selection)
		{
			if (EventSystem.current == null)
			{
				return;
			}
			EventSystem.current.SetSelectedGameObject(selection);
		}

		// Token: 0x040018E9 RID: 6377
		public Image backgroundImage;

		// Token: 0x040018EA RID: 6378
		public GameObject content;

		// Token: 0x040018EB RID: 6379
		private bool _initialized;

		// Token: 0x040018EC RID: 6380
		private int _id = -1;

		// Token: 0x040018ED RID: 6381
		private RectTransform _rectTransform;

		// Token: 0x040018EE RID: 6382
		private Text _titleText;

		// Token: 0x040018EF RID: 6383
		private List<Text> _contentText;

		// Token: 0x040018F0 RID: 6384
		private GameObject _defaultUIElement;

		// Token: 0x040018F1 RID: 6385
		private Action<int> _updateCallback;

		// Token: 0x040018F2 RID: 6386
		private Func<int, bool> _isFocusedCallback;

		// Token: 0x040018F3 RID: 6387
		private Window.Timer _timer;

		// Token: 0x040018F4 RID: 6388
		private CanvasGroup _canvasGroup;

		// Token: 0x040018F5 RID: 6389
		public UnityAction cancelCallback;

		// Token: 0x040018F6 RID: 6390
		private GameObject lastUISelection;

		// Token: 0x0200049E RID: 1182
		public class Timer
		{
			// Token: 0x17000609 RID: 1545
			// (get) Token: 0x06001DAB RID: 7595 RVA: 0x0007884E File Offset: 0x00076A4E
			public bool started
			{
				get
				{
					return this._started;
				}
			}

			// Token: 0x1700060A RID: 1546
			// (get) Token: 0x06001DAC RID: 7596 RVA: 0x00078856 File Offset: 0x00076A56
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

			// Token: 0x1700060B RID: 1547
			// (get) Token: 0x06001DAD RID: 7597 RVA: 0x00078879 File Offset: 0x00076A79
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

			// Token: 0x06001DAF RID: 7599 RVA: 0x0007889D File Offset: 0x00076A9D
			public void Start(float length)
			{
				this.end = Time.realtimeSinceStartup + length;
				this._started = true;
			}

			// Token: 0x06001DB0 RID: 7600 RVA: 0x000788B3 File Offset: 0x00076AB3
			public void Stop()
			{
				this._started = false;
			}

			// Token: 0x04001F3A RID: 7994
			private bool _started;

			// Token: 0x04001F3B RID: 7995
			private float end;
		}
	}
}
