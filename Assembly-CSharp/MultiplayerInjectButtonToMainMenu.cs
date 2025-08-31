using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerInjectButtonToMainMenu : MonoBehaviour
{
	private void Start()
	{
		this.CreateButton();
		this.StartAnimations();
	}

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
		this.buttonText = gameObject2.GetComponent<Text>();
		if (MultiplayerConfigLoader.Instance.ConfigFileFound && MultiplayerConfigLoader.Instance.DisplayNamePresent && MultiplayerConfigLoader.Instance.ServerHostPresent)
		{
			this.buttonText.text = "Config loaded!";
			this.buttonText.color = Color.green;
		}
		else
		{
			this.buttonText.text = "Config error!";
			this.buttonText.color = Color.red;
		}
		this.buttonText.fontSize = 28;
		this.buttonText.alignment = TextAnchor.MiddleCenter;
		if (this.scribbleFont != null)
		{
			this.buttonText.font = this.scribbleFont;
		}
		else
		{
			this.buttonText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
		}
		this.injectedButton.GetComponent<Button>().onClick.AddListener(delegate
		{
			Debug.Log("[LGG-MP] Custom Injected Button clicked");
			base.StartCoroutine(this.ClickAnimation());
		});
		Debug.Log("[LGG-MP] Injected custom button successfully");
	}

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

	private void Update()
	{
		if (this.buttonText.text == "Config loaded!" || this.buttonText.text == "Could not connect to server!" || this.buttonText.text == "Multiplayer ready!")
		{
			if (MultiplayerCommunicationService.Instance.connectionState == MultiplayerCommunicationService.ConnectionState.Connected)
			{
				this.buttonText.text = "Multiplayer ready!";
				this.buttonText.color = Color.green;
			}
			else
			{
				this.buttonText.text = "Could not connect to server!";
				this.buttonText.color = Color.red;
			}
		}
		if (!this.animationsEnabled || this.buttonTransform == null)
		{
			return;
		}
		this.AnimateRotation();
		this.AnimateScale();
	}

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

	private void AnimateScale()
	{
		this.scaleTimer += Time.deltaTime * this.scaleSpeed;
		float num = 1f + Mathf.Sin(this.scaleTimer) * this.scaleAmplitude;
		Vector3 vector = this.baseScale * num;
		this.buttonTransform.localScale = vector;
	}

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

	public void EnableAnimations(bool enable)
	{
		this.animationsEnabled = enable;
	}

	public void SetRotationSpeed(float speed)
	{
		this.rotationSpeed = speed;
	}

	public void SetScaleSettings(float amplitude, float speed)
	{
		this.scaleAmplitude = amplitude;
		this.scaleSpeed = speed;
	}

	private void OnDestroy()
	{
		if (this.injectedButton != null)
		{
			Object.Destroy(this.injectedButton);
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

	private Text buttonText;
}
