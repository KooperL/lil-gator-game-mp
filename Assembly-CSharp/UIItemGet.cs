using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIItemGet : MonoBehaviour
{
	// Token: 0x06000F05 RID: 3845 RVA: 0x00048080 File Offset: 0x00046280
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

	// Token: 0x06000F06 RID: 3846 RVA: 0x000480AC File Offset: 0x000462AC
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

	// Token: 0x06000F07 RID: 3847 RVA: 0x000480D8 File Offset: 0x000462D8
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

	// Token: 0x06000F08 RID: 3848 RVA: 0x00048184 File Offset: 0x00046384
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
