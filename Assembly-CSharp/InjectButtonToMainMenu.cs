using System;
using UnityEngine;
using UnityEngine.UI;

public class InjectButtonToMainMenu : MonoBehaviour
{
	// Token: 0x06001E78 RID: 7800 RVA: 0x00077AFC File Offset: 0x00075CFC
	private void Start()
	{
		GameObject gameObject = GameObject.Find("Main Menu Canvas");
		if (gameObject == null)
		{
			Debug.LogError("Main Menu Canvas not found!");
			return;
		}
		Transform transform = gameObject.transform.Find("Title Screen/Buttons");
		if (transform == null)
		{
			Debug.LogError("Buttons container not found!");
			return;
		}
		GameObject gameObject2 = new GameObject("InjectedButton", new Type[]
		{
			typeof(RectTransform),
			typeof(CanvasRenderer),
			typeof(Image),
			typeof(Button)
		});
		gameObject2.transform.SetParent(transform, false);
		RectTransform component = gameObject2.GetComponent<RectTransform>();
		component.sizeDelta = new Vector2(300f, 60f);
		component.anchoredPosition = new Vector2(0f, -300f);
		component.localRotation = Quaternion.Euler(0f, 0f, -12f);
		gameObject2.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
		GameObject gameObject3 = new GameObject("Text", new Type[]
		{
			typeof(RectTransform),
			typeof(CanvasRenderer),
			typeof(Text)
		});
		gameObject3.transform.SetParent(gameObject2.transform, false);
		RectTransform component2 = gameObject3.GetComponent<RectTransform>();
		component2.anchorMin = Vector2.zero;
		component2.anchorMax = Vector2.one;
		component2.offsetMin = Vector2.zero;
		component2.offsetMax = Vector2.zero;
		Text component3 = gameObject3.GetComponent<Text>();
		component3.text = "My New Button";
		component3.fontSize = 28;
		component3.alignment = TextAnchor.MiddleCenter;
		component3.color = Color.green;
		if (this.scribbleFont != null)
		{
			component3.font = this.scribbleFont;
		}
		else
		{
			component3.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
		}
		gameObject2.GetComponent<Button>().onClick.AddListener(delegate
		{
			Debug.Log("Custom Injected Button clicked");
		});
		Debug.Log("✅ Injected custom button successfully.");
	}

	public Font scribbleFont;
}
