using System;
using UnityEngine;

// Token: 0x020000BE RID: 190
public class ShowIfCharacterUnlocked : MonoBehaviour
{
	// Token: 0x06000412 RID: 1042 RVA: 0x00017D69 File Offset: 0x00015F69
	[ContextMenu("fix Profiles")]
	private void FixProfiles()
	{
	}

	// Token: 0x06000413 RID: 1043 RVA: 0x00017D6C File Offset: 0x00015F6C
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

	// Token: 0x06000414 RID: 1044 RVA: 0x00017DEA File Offset: 0x00015FEA
	private void OnDestroy()
	{
		if (this.hasListener)
		{
			this.RemoveListeners();
		}
	}

	// Token: 0x06000415 RID: 1045 RVA: 0x00017DFC File Offset: 0x00015FFC
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

	// Token: 0x06000416 RID: 1046 RVA: 0x00017E50 File Offset: 0x00016050
	private void OnCharacterChange(object sender, bool isUnlocked)
	{
		if (this.IsUnlocked())
		{
			this.RemoveListeners();
			base.gameObject.SetActive(true);
		}
	}

	// Token: 0x06000417 RID: 1047 RVA: 0x00017E6C File Offset: 0x0001606C
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

	// Token: 0x040005B7 RID: 1463
	public CharacterProfile characterProfile;

	// Token: 0x040005B8 RID: 1464
	public CharacterProfile[] moreCharacters;

	// Token: 0x040005B9 RID: 1465
	private bool hasListener;
}
