using System;
using UnityEngine;

public class ItemSkateboard : ItemShield
{
	// Token: 0x06000BBE RID: 3006 RVA: 0x0000AF93 File Offset: 0x00009193
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
