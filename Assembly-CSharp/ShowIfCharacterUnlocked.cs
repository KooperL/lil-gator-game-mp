using System;
using UnityEngine;

// Token: 0x020000FD RID: 253
public class ShowIfCharacterUnlocked : MonoBehaviour
{
	// Token: 0x060004C6 RID: 1222 RVA: 0x00002229 File Offset: 0x00000429
	[ContextMenu("fix Profiles")]
	private void FixProfiles()
	{
	}

	// Token: 0x060004C7 RID: 1223 RVA: 0x0002BE54 File Offset: 0x0002A054
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

	// Token: 0x060004C8 RID: 1224 RVA: 0x000057DE File Offset: 0x000039DE
	private void OnDestroy()
	{
		if (this.hasListener)
		{
			this.RemoveListeners();
		}
	}

	// Token: 0x060004C9 RID: 1225 RVA: 0x0002BED4 File Offset: 0x0002A0D4
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

	// Token: 0x060004CA RID: 1226 RVA: 0x000057EE File Offset: 0x000039EE
	private void OnCharacterChange(object sender, bool isUnlocked)
	{
		if (this.IsUnlocked())
		{
			this.RemoveListeners();
			base.gameObject.SetActive(true);
		}
	}

	// Token: 0x060004CB RID: 1227 RVA: 0x0002BF28 File Offset: 0x0002A128
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

	// Token: 0x040006C9 RID: 1737
	public CharacterProfile characterProfile;

	// Token: 0x040006CA RID: 1738
	public CharacterProfile[] moreCharacters;

	// Token: 0x040006CB RID: 1739
	private bool hasListener;
}
