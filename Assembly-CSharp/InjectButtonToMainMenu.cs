using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004D2 RID: 1234
public class InjectButtonToMainMenu : MonoBehaviour
{
	// Token: 0x06001E6C RID: 7788
	private void Start()
	{
		GameObject mainMenuCanvas = GameObject.Find("Main Menu Canvas");
		if (mainMenuCanvas == null)
		{
			Debug.LogError("Main Menu Canvas not found!");
			return;
		}
		Transform buttonsParent = mainMenuCanvas.transform.Find("Title Screen/Buttons");
		if (buttonsParent == null)
		{
			Debug.LogError("Buttons container not found!");
			return;
		}
		GameObject newButtonGO = new GameObject("InjectedButton", new Type[]
		{
			typeof(RectTransform),
			typeof(CanvasRenderer),
			typeof(Image),
			typeof(Button)
		});
		newButtonGO.transform.SetParent(buttonsParent, false);
		RectTransform component = newButtonGO.GetComponent<RectTransform>();
		component.sizeDelta = new Vector2(300f, 60f);
		component.anchoredPosition = new Vector2(0f, -300f);
		component.localRotation = Quaternion.Euler(0f, 0f, -12f);
		newButtonGO.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
		GameObject gameObject = new GameObject("Text", new Type[]
		{
			typeof(RectTransform),
			typeof(CanvasRenderer),
			typeof(Text)
		});
		gameObject.transform.SetParent(newButtonGO.transform, false);
		RectTransform component2 = gameObject.GetComponent<RectTransform>();
		component2.anchorMin = Vector2.zero;
		component2.anchorMax = Vector2.one;
		component2.offsetMin = Vector2.zero;
		component2.offsetMax = Vector2.zero;
		Text uiText = gameObject.GetComponent<Text>();
		uiText.text = "My New Button";
		uiText.fontSize = 28;
		uiText.alignment = TextAnchor.MiddleCenter;
		uiText.color = Color.green;
		if (this.scribbleFont != null)
		{
			uiText.font = this.scribbleFont;
		}
		else
		{
			uiText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
		}
		newButtonGO.GetComponent<Button>().onClick.AddListener(delegate
		{
			Debug.Log("Custom Injected Button clicked");
		});
		Debug.Log("✅ Injected custom button successfully.");
	}

	// Token: 0x04002042 RID: 8258
	public Font scribbleFont;
}
