using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003AE RID: 942
public class UIItemGet : MonoBehaviour
{
	// Token: 0x060011DD RID: 4573 RVA: 0x0000F3E0 File Offset: 0x0000D5E0
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

	// Token: 0x060011DE RID: 4574 RVA: 0x0000F40C File Offset: 0x0000D60C
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

	// Token: 0x060011DF RID: 4575 RVA: 0x00059258 File Offset: 0x00057458
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

	// Token: 0x060011E0 RID: 4576 RVA: 0x0000F438 File Offset: 0x0000D638
	public void Deactivate()
	{
		base.gameObject.SetActive(false);
		MusicStateManager.m.CancelDuck();
	}

	// Token: 0x04001713 RID: 5907
	public Image itemImage;

	// Token: 0x04001714 RID: 5908
	public Text displayText;

	// Token: 0x04001715 RID: 5909
	public Sprite defaultItemSprite;

	// Token: 0x04001716 RID: 5910
	public AudioSource jingleSource;

	// Token: 0x04001717 RID: 5911
	public AudioClip fallbackClip;

	// Token: 0x04001718 RID: 5912
	public RememberMusic musicSource;

	// Token: 0x04001719 RID: 5913
	private WaitForSeconds waitForPause;

	// Token: 0x0400171A RID: 5914
	private const float jingleDelay = 0.2f;
}
