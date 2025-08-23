using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerInjectButtonToMainMenu : MonoBehaviour
{
	// Token: 0x06001E75 RID: 7797 RVA: 0x000174BD File Offset: 0x000156BD
	private void Start()
	{
		this.CreateButton();
		this.StartAnimations();
	}

	// Token: 0x06001E76 RID: 7798 RVA: 0x000789D4 File Offset: 0x00076BD4
	private void CreateButton()
	{
		GameObject gameObject = GameObject.Find("Main Menu Canvas");
		if (gameObject == null)
		{
			Debug.LogError("[LGG-MP] Main Menu Canvas not found!");
			return;
		}
		Transform transform = gameObject.transform.Find("Title Screen/Buttons");
		if (transform == null)
		{
			Debug.LogError("[LGG-MP] Buttons container not found!");
			return;
		}
		this.injectedButton = new GameObject("InjectedButton", new Type[]
		{
			typeof(RectTransform),
			typeof(CanvasRenderer),
			typeof(Image),
			typeof(Button)
		});
		this.injectedButton.transform.SetParent(transform, false);
		this.buttonTransform = this.injectedButton.GetComponent<RectTransform>();
		this.buttonTransform.sizeDelta = new Vector2(300f, 60f);
		this.buttonTransform.anchoredPosition = new Vector2(0f, -300f);
		this.baseScale = this.buttonTransform.localScale;
		this.injectedButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
		GameObject gameObject2 = new GameObject("Text", new Type[]
		{
			typeof(RectTransform),
			typeof(CanvasRenderer),
			typeof(Text)
		});
		gameObject2.transform.SetParent(this.injectedButton.transform, false);
		RectTransform component = gameObject2.GetComponent<RectTransform>();
		component.anchorMin = Vector2.zero;
		component.anchorMax = Vector2.one;
		component.offsetMin = Vector2.zero;
		component.offsetMax = Vector2.zero;
		Text component2 = gameObject2.GetComponent<Text>();
		if (MultiplayerConfigLoader.Instance.ConfigFileFound && MultiplayerConfigLoader.Instance.DisplayNamePresent && MultiplayerConfigLoader.Instance.ServerHostPresent)
		{
			component2.text = "Multiplayer loaded!";
			component2.color = Color.green;
		}
		else
		{
			component2.text = "Error loading multiplayer";
			component2.color = Color.red;
		}
		component2.fontSize = 28;
		component2.alignment = TextAnchor.MiddleCenter;
		if (this.scribbleFont != null)
		{
			component2.font = this.scribbleFont;
		}
		else
		{
			component2.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
		}
		this.injectedButton.GetComponent<Button>().onClick.AddListener(delegate
		{
			Debug.Log("[LGG-MP] Custom Injected Button clicked");
			base.StartCoroutine(this.ClickAnimation());
		});
		Debug.Log("[LGG-MP] Injected custom button successfully");
	}

	// Token: 0x06001E77 RID: 7799 RVA: 0x000174CB File Offset: 0x000156CB
	private void StartAnimations()
	{
		if (this.buttonTransform == null)
		{
			Debug.LogWarning("[LGG-MP] Button transform not found, animations disabled");
			this.animationsEnabled = false;
			return;
		}
		this.animationsEnabled = true;
	}

	// Token: 0x06001E78 RID: 7800 RVA: 0x000174F4 File Offset: 0x000156F4
	private void Update()
	{
		if (!this.animationsEnabled || this.buttonTransform == null)
		{
			return;
		}
		this.AnimateRotation();
		this.AnimateScale();
	}

	// Token: 0x06001E79 RID: 7801 RVA: 0x00078C40 File Offset: 0x00076E40
	private void AnimateRotation()
	{
		this.rotationTimer += Time.deltaTime;
		if (this.rotationTimer >= this.rotationChangeInterval)
		{
			this.currentRotationDirection *= -1f;
			this.rotationTimer = 0f;
		}
		float num = this.rotationSpeed * this.currentRotationDirection * Time.deltaTime;
		this.buttonTransform.Rotate(0f, 0f, num);
	}

	// Token: 0x06001E7A RID: 7802 RVA: 0x00078CB4 File Offset: 0x00076EB4
	private void AnimateScale()
	{
		this.scaleTimer += Time.deltaTime * this.scaleSpeed;
		float num = 1f + Mathf.Sin(this.scaleTimer) * this.scaleAmplitude;
		Vector3 vector = this.baseScale * num;
		this.buttonTransform.localScale = vector;
	}

	// Token: 0x06001E7B RID: 7803 RVA: 0x00017519 File Offset: 0x00015719
	private IEnumerator ClickAnimation()
	{
		Vector3 originalScale = this.buttonTransform.localScale;
		Vector3 clickScale = originalScale * 1.1f;
		float duration = 0.1f;
		float elapsed = 0f;
		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;
			float num = elapsed / duration;
			this.buttonTransform.localScale = Vector3.Lerp(originalScale, clickScale, num);
			yield return null;
		}
		elapsed = 0f;
		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;
			float num2 = elapsed / duration;
			this.buttonTransform.localScale = Vector3.Lerp(clickScale, originalScale, num2);
			yield return null;
		}
		this.buttonTransform.localScale = originalScale;
		yield break;
	}

	// Token: 0x06001E7C RID: 7804 RVA: 0x00017528 File Offset: 0x00015728
	public void EnableAnimations(bool enable)
	{
		this.animationsEnabled = enable;
	}

	// Token: 0x06001E7D RID: 7805 RVA: 0x00017531 File Offset: 0x00015731
	public void SetRotationSpeed(float speed)
	{
		this.rotationSpeed = speed;
	}

	// Token: 0x06001E7E RID: 7806 RVA: 0x0001753A File Offset: 0x0001573A
	public void SetScaleSettings(float amplitude, float speed)
	{
		this.scaleAmplitude = amplitude;
		this.scaleSpeed = speed;
	}

	// Token: 0x06001E7F RID: 7807 RVA: 0x0001754A File Offset: 0x0001574A
	private void OnDestroy()
	{
		if (this.injectedButton != null)
		{
			global::UnityEngine.Object.Destroy(this.injectedButton);
		}
	}

	public Font scribbleFont;

	[Header("Animation Settings")]
	[SerializeField]
	private float rotationSpeed;

	[SerializeField]
	private float scaleAmplitude = 0.15f;

	[SerializeField]
	private float scaleSpeed = 2f;

	[SerializeField]
	private float rotationChangeInterval = 1f;

	private GameObject injectedButton;

	private RectTransform buttonTransform;

	private Vector3 baseScale = Vector3.one;

	private float currentRotationDirection = -1f;

	private float rotationTimer;

	private float scaleTimer;

	private bool animationsEnabled = true;
}
