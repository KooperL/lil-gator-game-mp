using System;
using UnityEngine;

// Token: 0x020001FE RID: 510
public class EnableIfUnlocked : MonoBehaviour
{
	// Token: 0x06000976 RID: 2422 RVA: 0x00039D44 File Offset: 0x00037F44
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

	// Token: 0x06000977 RID: 2423 RVA: 0x000092DC File Offset: 0x000074DC
	private void Start()
	{
		if (!this.profile.IsUnlocked)
		{
			this.profile.OnChange += this.OnChange;
			this.hasEvent = true;
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000978 RID: 2424 RVA: 0x00009315 File Offset: 0x00007515
	private void OnDestroy()
	{
		if (this.hasEvent)
		{
			this.profile.OnChange -= this.OnChange;
		}
	}

	// Token: 0x06000979 RID: 2425 RVA: 0x00009336 File Offset: 0x00007536
	public void OnChange(object sender, bool isUnlocked)
	{
		base.gameObject.SetActive(isUnlocked);
	}

	// Token: 0x04000C24 RID: 3108
	public CharacterProfile profile;

	// Token: 0x04000C25 RID: 3109
	private bool hasEvent;
}
