using System;
using UnityEngine;

public class ShowIfCharacterUnlocked : MonoBehaviour
{
	// Token: 0x060004EC RID: 1260 RVA: 0x00002229 File Offset: 0x00000429
	[ContextMenu("fix Profiles")]
	private void FixProfiles()
	{
	}

	// Token: 0x060004ED RID: 1261 RVA: 0x0002CF90 File Offset: 0x0002B190
	private void Start()
	{
		if (!this.IsUnlocked())
		{
			if (this.characterProfile != null)
			{
				this.characterProfile.OnChange += this.OnCharacterChange;
			}
			if (this.moreCharacters != null)
			{
				CharacterProfile[] array = this.moreCharacters;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].OnChange += this.OnCharacterChange;
				}
			}
			this.hasListener = true;
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x060004EE RID: 1262 RVA: 0x00005A04 File Offset: 0x00003C04
	private void OnDestroy()
	{
		if (this.hasListener)
		{
			this.RemoveListeners();
		}
	}

	// Token: 0x060004EF RID: 1263 RVA: 0x0002D010 File Offset: 0x0002B210
	private bool IsUnlocked()
	{
		if (this.characterProfile != null && !this.characterProfile.IsUnlocked)
		{
			return false;
		}
		if (this.moreCharacters != null)
		{
			CharacterProfile[] array = this.moreCharacters;
			for (int i = 0; i < array.Length; i++)
			{
				if (!array[i].IsUnlocked)
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x060004F0 RID: 1264 RVA: 0x00005A14 File Offset: 0x00003C14
	private void OnCharacterChange(object sender, bool isUnlocked)
	{
		if (this.IsUnlocked())
		{
			this.RemoveListeners();
			base.gameObject.SetActive(true);
		}
	}

	// Token: 0x060004F1 RID: 1265 RVA: 0x0002D064 File Offset: 0x0002B264
	private void RemoveListeners()
	{
		if (this.characterProfile != null)
		{
			this.characterProfile.OnChange -= this.OnCharacterChange;
		}
		if (this.moreCharacters != null)
		{
			CharacterProfile[] array = this.moreCharacters;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].OnChange -= this.OnCharacterChange;
			}
		}
		this.hasListener = false;
	}

	public CharacterProfile characterProfile;

	public CharacterProfile[] moreCharacters;

	private bool hasListener;
}
