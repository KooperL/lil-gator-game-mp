using System;
using UnityEngine;

// Token: 0x02000249 RID: 585
[CreateAssetMenu]
public class ItemData : ScriptableObject
{
	// Token: 0x04000DED RID: 3565
	public string id;

	// Token: 0x04000DEE RID: 3566
	public GameObject prefab;

	// Token: 0x04000DEF RID: 3567
	public ItemManager.ItemType itemType;
}
