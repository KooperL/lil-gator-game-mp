using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UISlideshow : MonoBehaviour
{
	// Token: 0x0600132D RID: 4909 RVA: 0x000102A5 File Offset: 0x0000E4A5
	public void OnEnable()
	{
		Game.g.SetWorldState(this.worldState, false, true);
		this.currentSlide = -1;
		this.NextSlide();
		this.sequencer.JustStartSequence();
		this.StartLoad();
	}

	// Token: 0x0600132E RID: 4910 RVA: 0x000102D7 File Offset: 0x0000E4D7
	private Color MoveTowardsColor(Color current, float target, float speed, int channel)
	{
		current[channel] = Mathf.MoveTowards(current[channel], target, speed * Time.deltaTime);
		return current;
	}

	// Token: 0x0600132F RID: 4911 RVA: 0x000102F9 File Offset: 0x0000E4F9
	private IEnumerator RunSlideTransition(int currentSlide)
	{
		Color backgroundColor = this.slides[currentSlide].backgroundColor;
		Sprite slideSprite;
		if (currentSlide >= this.slides.Length)
		{
			slideSprite = null;
		}
		else
		{
			slideSprite = this.slides[currentSlide].sprite;
		}
		while (this.background.color.a != backgroundColor.a || (this.slide.color.g > 0f && this.slide.sprite != slideSprite) || (this.slide.color.a > 0f && currentSlide == this.slides.Length - 1) || (this.slide.color.a < 1f && currentSlide < this.slides.Length - 1))
		{
			if (this.background.color.a != backgroundColor.a)
			{
				this.background.color = this.MoveTowardsColor(this.background.color, backgroundColor.a, (this.background.color.a < backgroundColor.a) ? this.firstSlideFadeInSpeed : this.slideFadeOutSpeed, 3);
			}
			if (this.slide.sprite != slideSprite && this.slide.color.g > 0f && currentSlide != this.slides.Length - 1)
			{
				this.slide.color = this.MoveTowardsColor(this.slide.color, 0f, this.slideFadeOutSpeed, 1);
			}
			if (currentSlide == this.slides.Length - 1)
			{
				if (this.slide.color.a > 0f)
				{
					this.slide.color = this.MoveTowardsColor(this.slide.color, 0f, this.slideFadeOutSpeed, 3);
				}
			}
			else if (this.slide.color.a < 1f)
			{
				this.slide.color = this.MoveTowardsColor(this.slide.color, 1f, this.firstSlideFadeInSpeed, 3);
			}
			if (this.slide.color.r < 1f)
			{
				this.slide.color = this.MoveTowardsColor(this.slide.color, 1f, this.slideFadeInSpeed, 0);
			}
			yield return null;
		}
		if (this.slide.sprite != slideSprite && currentSlide != this.slides.Length - 1)
		{
			this.slide.sprite = slideSprite;
			this.slide.color = new Color(0f, 1f, 1f, 1f);
		}
		while (this.slide.color.r < 1f && this.slide.sprite != null)
		{
			if (this.slide.color.r < 1f)
			{
				this.slide.color = this.MoveTowardsColor(this.slide.color, 1f, this.slideFadeInSpeed, 0);
			}
			yield return null;
		}
		this.currentTransition = null;
		if (currentSlide == this.slides.Length - 1)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
		if (currentSlide == this.slides.Length - 2)
		{
			this.FinishSlideshow();
		}
		yield break;
	}

	// Token: 0x06001330 RID: 4912 RVA: 0x0005E730 File Offset: 0x0005C930
	public void NextSlide()
	{
		this.oldBackgroundColor = this.background.color;
		this.currentSlide++;
		this.slides[this.currentSlide].onSlideEvent.Invoke();
		if (this.currentTransition != null)
		{
			base.StopCoroutine(this.currentTransition);
		}
		this.currentTransition = this.RunSlideTransition(this.currentSlide);
		base.StartCoroutine(this.currentTransition);
	}

	// Token: 0x06001331 RID: 4913 RVA: 0x0001030F File Offset: 0x0000E50F
	public void StartLoad()
	{
		base.StartCoroutine(this.LoadSceneAsync());
	}

	// Token: 0x06001332 RID: 4914 RVA: 0x0001031E File Offset: 0x0000E51E
	public void FinishSlideshow()
	{
		if (!this.isLoadFinished)
		{
			SpeedrunData.isLoading = true;
			this.isFastLoading = true;
		}
		this.isSlideshowFinished = true;
	}

	// Token: 0x06001333 RID: 4915 RVA: 0x0001033C File Offset: 0x0000E53C
	public IEnumerator LoadSceneAsync()
	{
		global::UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		this.loadingIcon.SetActive(true);
		FadeGameVolume.FadeOutGameVolume();
		yield return new WaitForSeconds(1f);
		AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(this.mainScene, LoadSceneMode.Single, false, 100);
		yield return handle;
		this.isLoadFinished = true;
		this.loadingIcon.SetActive(false);
		while (!this.isSlideshowFinished)
		{
			yield return null;
		}
		yield return handle.Result.ActivateAsync();
		bool flag = this.isFastLoading;
		FadeGameVolume.FadeInGameVolume();
		SpeedrunData.isLoading = false;
		yield return new WaitForSeconds(0.25f);
		this.NextSlide();
		yield return new WaitForSeconds(4f);
		global::UnityEngine.Object.Destroy(base.gameObject);
		yield break;
	}

	public DialogueSequencer sequencer;

	public GameObject loadingIcon;

	public Image background;

	private Color oldBackgroundColor;

	public Image slide;

	public UISlideshow.Slide[] slides;

	private int currentSlide = -1;

	public float firstSlideFadeInSpeed = 0.5f;

	public float slideFadeInSpeed = 0.5f;

	public float slideFadeOutSpeed = 2f;

	public AssetReference mainScene;

	public bool useFastLoading;

	public WorldState worldState = WorldState.Act1;

	private IEnumerator currentTransition;

	private bool isSlideshowFinished;

	private bool isFastLoading;

	private bool isLoadFinished;

	[Serializable]
	public struct Slide
	{
		public Color backgroundColor;

		public Sprite sprite;

		public UnityEvent onSlideEvent;
	}
}
