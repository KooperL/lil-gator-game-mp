using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CreditsOverlay : MonoBehaviour
{
	// Token: 0x060002DA RID: 730 RVA: 0x000111F8 File Offset: 0x0000F3F8
	[ContextMenu("Start Credits")]
	public void StartCredits()
	{
		this.currentChunk = -1;
		this.nextChunkTime = this.startDelay;
		base.enabled = true;
	}

	// Token: 0x060002DB RID: 731 RVA: 0x00011214 File Offset: 0x0000F414
	private void Update()
	{
		if (this.nextChunkTime < 0f)
		{
			if (this.currentHeader == null && this.currentText == null)
			{
				base.enabled = false;
			}
			return;
		}
		if (Game.State == GameState.Menu || Game.State == GameState.ItemMenu)
		{
			if (this.currentHeader != null)
			{
				this.currentHeader.enabled = false;
			}
			if (this.currentText != null)
			{
				this.currentText.enabled = false;
				return;
			}
		}
		else
		{
			if (this.currentHeader != null)
			{
				this.currentHeader.enabled = true;
			}
			if (this.currentText != null)
			{
				this.currentText.enabled = true;
			}
			this.nextChunkTime -= Time.deltaTime;
			if (this.nextChunkTime < 0f)
			{
				base.StartCoroutine(this.ProgressChunk());
			}
		}
	}

	// Token: 0x060002DC RID: 732 RVA: 0x000112F7 File Offset: 0x0000F4F7
	private IEnumerator ProgressChunk()
	{
		this.currentChunk++;
		if (this.creditsChunks.Length <= this.currentChunk)
		{
			base.StartCoroutine(this.FadeOut(this.currentHeader));
			base.StartCoroutine(this.FadeOut(this.currentText));
			yield return null;
		}
		else
		{
			this.nextChunkTime = this.fadeTime + this.displayTime;
			bool needsHeader = false;
			if (this.currentHeader == null)
			{
				needsHeader = true;
			}
			else if (this.currentHeader.text != this.creditsChunks[this.currentChunk].header)
			{
				base.StartCoroutine(this.FadeOut(this.currentHeader));
				needsHeader = true;
			}
			if (this.currentText != null)
			{
				base.StartCoroutine(this.FadeOut(this.currentText));
				this.nextChunkTime += this.fadeTime;
				yield return new WaitForSeconds(this.fadeTime);
			}
			if (needsHeader)
			{
				this.currentHeader = this.GetInstance(this.headerPrefab, this.headerDocument.FetchString(this.creditsChunks[this.currentChunk].header, Language.Auto));
				base.StartCoroutine(this.FadeIn(this.currentHeader));
			}
			this.currentText = this.GetInstance(this.textPrefab, this.creditsChunks[this.currentChunk].text);
			base.StartCoroutine(this.FadeIn(this.currentText));
			yield return null;
		}
		yield break;
	}

	// Token: 0x060002DD RID: 733 RVA: 0x00011306 File Offset: 0x0000F506
	private IEnumerator FadeIn(Text text)
	{
		float fade = 0f;
		Vector2 anchoredPosition = text.rectTransform.anchoredPosition;
		Color color = text.color;
		while (fade < 1f)
		{
			fade += Time.deltaTime / this.fadeTime;
			text.rectTransform.anchoredPosition = anchoredPosition + Mathf.SmoothStep(this.fadePositionOffset, 0f, fade) * Vector2.up;
			color.a = fade;
			text.color = color;
			yield return null;
		}
		yield return null;
		yield break;
	}

	// Token: 0x060002DE RID: 734 RVA: 0x0001131C File Offset: 0x0000F51C
	private IEnumerator FadeOut(Text text)
	{
		float fade = 0f;
		Vector2 anchoredPosition = text.rectTransform.anchoredPosition;
		Color color = text.color;
		while (fade < 1f)
		{
			fade += Time.deltaTime / this.fadeTime;
			text.rectTransform.anchoredPosition = anchoredPosition + Mathf.SmoothStep(0f, -this.fadePositionOffset, fade) * Vector2.up;
			color.a = 1f - fade;
			text.color = color;
			yield return null;
		}
		yield return null;
		Object.Destroy(text.gameObject);
		yield break;
	}

	// Token: 0x060002DF RID: 735 RVA: 0x00011332 File Offset: 0x0000F532
	private Text GetInstance(GameObject prefab, string text)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(prefab, base.transform);
		gameObject.SetActive(true);
		Text component = gameObject.GetComponent<Text>();
		component.text = text;
		return component;
	}

	public MultilingualTextDocument headerDocument;

	public CreditsOverlay.CreditsChunk[] creditsChunks;

	public GameObject headerPrefab;

	public GameObject textPrefab;

	private int currentChunk = -1;

	private Text currentHeader;

	private Text currentText;

	public float startDelay = 1.5f;

	public float fadeTime = 0.5f;

	public float displayTime = 7f;

	private float nextChunkTime;

	public float fadePositionOffset = 30f;

	[Serializable]
	public struct CreditsChunk
	{
		[TextLookup("headerDocument")]
		public string header;

		[TextArea]
		public string text;
	}
}
