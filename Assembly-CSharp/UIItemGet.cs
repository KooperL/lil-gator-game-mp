using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIItemGet : MonoBehaviour
{
	// Token: 0x0600123D RID: 4669 RVA: 0x0000F7D3 File Offset: 0x0000D9D3
	public IEnumerator RunSequence(Sprite itemSprite, string displayName, DialogueChunk dialogueChunk, DialogueActor[] actors = null)
	{
		Game.DialogueDepth++;
		DialogueManager.d.SetDialogueCamera(actors);
		this.Activate(itemSprite, displayName);
		if (this.waitForPause == null)
		{
			this.waitForPause = new WaitForSeconds(0.5f);
		}
		yield return this.waitForPause;
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk(dialogueChunk, actors, DialogueManager.DialogueBoxBackground.Standard, true, true, false, false));
		this.Deactivate();
		Game.DialogueDepth--;
		yield break;
	}

	// Token: 0x0600123E RID: 4670 RVA: 0x0000F7FF File Offset: 0x0000D9FF
	public IEnumerator RunSequence(Sprite itemSprite, string displayName, string dialogue, DialogueActor[] actors = null)
	{
		Game.DialogueDepth++;
		DialogueManager.d.SetDialogueCamera(actors);
		this.Activate(itemSprite, displayName);
		if (this.waitForPause == null)
		{
			this.waitForPause = new WaitForSeconds(0.5f);
		}
		yield return this.waitForPause;
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk(dialogue, actors, DialogueManager.DialogueBoxBackground.Standard, true));
		this.Deactivate();
		Game.DialogueDepth--;
		yield break;
	}

	// Token: 0x0600123F RID: 4671 RVA: 0x0005B1F8 File Offset: 0x000593F8
	public void Activate(Sprite itemSprite, string displayName = "Item")
	{
		this.itemImage.sprite = ((itemSprite != null) ? itemSprite : this.defaultItemSprite);
		this.displayText.text = displayName;
		base.gameObject.SetActive(true);
		if (!OverriddenMusic.isOverridden)
		{
			AudioClip itemGetForMusic = this.fallbackClip;
			if (this.musicSource != null)
			{
				itemGetForMusic = this.musicSource.GetItemGetForMusic();
			}
			if (itemGetForMusic == null)
			{
				itemGetForMusic = this.fallbackClip;
			}
			this.jingleSource.clip = itemGetForMusic;
			this.jingleSource.PlayDelayed(0.2f);
			MusicStateManager.m.DuckMusic(itemGetForMusic.length + 0.2f);
		}
	}

	// Token: 0x06001240 RID: 4672 RVA: 0x0000F82B File Offset: 0x0000DA2B
	public void Deactivate()
	{
		base.gameObject.SetActive(false);
		MusicStateManager.m.CancelDuck();
	}

	public Image itemImage;

	public Text displayText;

	public Sprite defaultItemSprite;

	public AudioSource jingleSource;

	public AudioClip fallbackClip;

	public RememberMusic musicSource;

	private WaitForSeconds waitForPause;

	private const float jingleDelay = 0.2f;
}
