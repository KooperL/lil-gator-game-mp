using System;
using UnityEngine;

public class ItemSkateboard : ItemShield
{
	// Token: 0x06000BBF RID: 3007 RVA: 0x0000AFB2 File Offset: 0x000091B2
	public override Transform GetParent(bool isEquipped, bool isSledding)
	{
		if (isEquipped && isSledding)
		{
			return Player.itemManager.shieldSkateAnchor;
		}
		if (isEquipped && !isSledding)
		{
			return Player.itemManager.shieldUnderArmAnchor;
		}
		return base.GetParent(isEquipped, isSledding);
	}
}
