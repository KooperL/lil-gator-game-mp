using System;
using UnityEngine;

public class EnableIfUnlocked : MonoBehaviour
{
	// Token: 0x0600080C RID: 2060 RVA: 0x00026CD8 File Offset: 0x00024ED8
	private void OnValidate()
	{
		if (this.profile == null)
		{
			DialogueActor component = base.GetComponent<DialogueActor>();
			if (component != null)
			{
				this.profile = component.profile;
			}
		}
	}

	// Token: 0x0600080D RID: 2061 RVA: 0x00026D0F File Offset: 0x00024F0F
	private void Start()
	{
		if (!this.profile.IsUnlocked)
		{
			this.profile.OnChange += this.OnChange;
			this.hasEvent = true;
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600080E RID: 2062 RVA: 0x00026D48 File Offset: 0x00024F48
	private void OnDestroy()
	{
		if (this.hasEvent)
		{
			this.profile.OnChange -= this.OnChange;
		}
	}

	// Token: 0x0600080F RID: 2063 RVA: 0x00026D69 File Offset: 0x00024F69
	public void OnChange(object sender, bool isUnlocked)
	{
		base.gameObject.SetActive(isUnlocked);
	}

	public CharacterProfile profile;

	private bool hasEvent;
}
