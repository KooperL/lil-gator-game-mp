using System;
using UnityEngine;

public class ItemSkateboard : ItemShield
{
	// Token: 0x060009C9 RID: 2505 RVA: 0x0002D803 File Offset: 0x0002BA03
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
