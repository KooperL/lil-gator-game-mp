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
	// Token: 0x06000FAC RID: 4012 RVA: 0x0004B116 File Offset: 0x00049316
	public void OnEnable()
	{
		Game.g.SetWorldState(this.worldState, false, true);
		this.currentSlide = -1;
		this.NextSlide();
		this.sequencer.JustStartSequence();
		this.StartLoad();
	}

	// Token: 0x06000FAD RID: 4013 RVA: 0x0004B148 File Offset: 0x00049348
	private Color MoveTowardsColor(Color current, float target, float speed, int channel)
	{
		current[channel] = Mathf.MoveTowards(current[channel], target, speed * Time.deltaTime);
		return current;
	}

	// Token: 0x06000FAE RID: 4014 RVA: 0x0004B16A File Offset: 0x0004936A
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
			Object.Destroy(base.gameObject);
		}
		if (currentSlide == this.slides.Length - 2)
		{
			this.FinishSlideshow();
		}
		yield break;
	}

	// Token: 0x06000FAF RID: 4015 RVA: 0x0004B180 File Offset: 0x00049380
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

	// Token: 0x06000FB0 RID: 4016 RVA: 0x0004B1FA File Offset: 0x000493FA
	public void StartLoad()
	{
		base.StartCoroutine(this.LoadSceneAsync());
	}

	// Token: 0x06000FB1 RID: 4017 RVA: 0x0004B209 File Offset: 0x00049409
	public void FinishSlideshow()
	{
		if (!this.isLoadFinished)
		{
			SpeedrunData.isLoading = true;
			this.isFastLoading = true;
		}
		this.isSlideshowFinished = true;
	}

	// Token: 0x06000FB2 RID: 4018 RVA: 0x0004B227 File Offset: 0x00049427
	public IEnumerator LoadSceneAsync()
	{
		Object.DontDestroyOnLoad(base.gameObject);
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
		Object.Destroy(base.gameObject);
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
