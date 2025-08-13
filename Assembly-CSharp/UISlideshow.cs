using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.UI;

// Token: 0x020003D1 RID: 977
public class UISlideshow : MonoBehaviour
{
	// Token: 0x060012CC RID: 4812 RVA: 0x0000FE9E File Offset: 0x0000E09E
	public void OnEnable()
	{
		Game.g.SetWorldState(this.worldState, false, true);
		this.currentSlide = -1;
		this.NextSlide();
		this.sequencer.JustStartSequence();
		this.StartLoad();
	}

	// Token: 0x060012CD RID: 4813 RVA: 0x0000FED0 File Offset: 0x0000E0D0
	private Color MoveTowardsColor(Color current, float target, float speed, int channel)
	{
		current[channel] = Mathf.MoveTowards(current[channel], target, speed * Time.deltaTime);
		return current;
	}

	// Token: 0x060012CE RID: 4814 RVA: 0x0000FEF2 File Offset: 0x0000E0F2
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

	// Token: 0x060012CF RID: 4815 RVA: 0x0005C440 File Offset: 0x0005A640
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

	// Token: 0x060012D0 RID: 4816 RVA: 0x0000FF08 File Offset: 0x0000E108
	public void StartLoad()
	{
		base.StartCoroutine(this.LoadSceneAsync());
	}

	// Token: 0x060012D1 RID: 4817 RVA: 0x0000FF17 File Offset: 0x0000E117
	public void FinishSlideshow()
	{
		if (!this.isLoadFinished)
		{
			SpeedrunData.isLoading = true;
			this.isFastLoading = true;
		}
		this.isSlideshowFinished = true;
	}

	// Token: 0x060012D2 RID: 4818 RVA: 0x0000FF35 File Offset: 0x0000E135
	public IEnumerator LoadSceneAsync()
	{
		Object.DontDestroyOnLoad(base.gameObject);
		this.loadingIcon.SetActive(true);
		FadeGameVolume.FadeOutGameVolume();
		yield return new WaitForSeconds(1f);
		AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(this.mainScene, 0, false, 100);
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

	// Token: 0x04001834 RID: 6196
	public DialogueSequencer sequencer;

	// Token: 0x04001835 RID: 6197
	public GameObject loadingIcon;

	// Token: 0x04001836 RID: 6198
	public Image background;

	// Token: 0x04001837 RID: 6199
	private Color oldBackgroundColor;

	// Token: 0x04001838 RID: 6200
	public Image slide;

	// Token: 0x04001839 RID: 6201
	public UISlideshow.Slide[] slides;

	// Token: 0x0400183A RID: 6202
	private int currentSlide = -1;

	// Token: 0x0400183B RID: 6203
	public float firstSlideFadeInSpeed = 0.5f;

	// Token: 0x0400183C RID: 6204
	public float slideFadeInSpeed = 0.5f;

	// Token: 0x0400183D RID: 6205
	public float slideFadeOutSpeed = 2f;

	// Token: 0x0400183E RID: 6206
	public AssetReference mainScene;

	// Token: 0x0400183F RID: 6207
	public bool useFastLoading;

	// Token: 0x04001840 RID: 6208
	public WorldState worldState = WorldState.Act1;

	// Token: 0x04001841 RID: 6209
	private IEnumerator currentTransition;

	// Token: 0x04001842 RID: 6210
	private bool isSlideshowFinished;

	// Token: 0x04001843 RID: 6211
	private bool isFastLoading;

	// Token: 0x04001844 RID: 6212
	private bool isLoadFinished;

	// Token: 0x020003D2 RID: 978
	[Serializable]
	public struct Slide
	{
		// Token: 0x04001845 RID: 6213
		public Color backgroundColor;

		// Token: 0x04001846 RID: 6214
		public Sprite sprite;

		// Token: 0x04001847 RID: 6215
		public UnityEvent onSlideEvent;
	}
}
