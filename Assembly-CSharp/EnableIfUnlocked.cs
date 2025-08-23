using System;
using UnityEngine;

public class EnableIfUnlocked : MonoBehaviour
{
	// Token: 0x060009BD RID: 2493 RVA: 0x0003BA20 File Offset: 0x00039C20
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

	// Token: 0x060009BE RID: 2494 RVA: 0x00009644 File Offset: 0x00007844
	private void Start()
	{
		if (!this.profile.IsUnlocked)
		{
			this.profile.OnChange += this.OnChange;
			this.hasEvent = true;
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x060009BF RID: 2495 RVA: 0x0000967D File Offset: 0x0000787D
	private void OnDestroy()
	{
		if (this.hasEvent)
		{
			this.profile.OnChange -= this.OnChange;
		}
	}

	// Token: 0x060009C0 RID: 2496 RVA: 0x0000969E File Offset: 0x0000789E
	public void OnChange(object sender, bool isUnlocked)
	{
		base.gameObject.SetActive(isUnlocked);
	}

	public CharacterProfile profile;

	private bool hasEvent;
}
