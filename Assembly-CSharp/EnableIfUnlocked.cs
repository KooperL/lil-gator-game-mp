using System;
using UnityEngine;

public class EnableIfUnlocked : MonoBehaviour
{
	// Token: 0x060009BC RID: 2492 RVA: 0x0003B758 File Offset: 0x00039958
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

	// Token: 0x060009BD RID: 2493 RVA: 0x0000963A File Offset: 0x0000783A
	private void Start()
	{
		if (!this.profile.IsUnlocked)
		{
			this.profile.OnChange += this.OnChange;
			this.hasEvent = true;
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x060009BE RID: 2494 RVA: 0x00009673 File Offset: 0x00007873
	private void OnDestroy()
	{
		if (this.hasEvent)
		{
			this.profile.OnChange -= this.OnChange;
		}
	}

	// Token: 0x060009BF RID: 2495 RVA: 0x00009694 File Offset: 0x00007894
	public void OnChange(object sender, bool isUnlocked)
	{
		base.gameObject.SetActive(isUnlocked);
	}

	public CharacterProfile profile;

	private bool hasEvent;
}
