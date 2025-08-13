using System;
using UnityEngine;

// Token: 0x0200025C RID: 604
public class ItemSkateboard : ItemShield
{
	// Token: 0x06000B72 RID: 2930 RVA: 0x0000ACA0 File Offset: 0x00008EA0
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
