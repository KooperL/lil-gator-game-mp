using System;
using UnityEngine;

// Token: 0x020001C6 RID: 454
[CreateAssetMenu]
public class ItemData : ScriptableObject
{
	// Token: 0x04000BC7 RID: 3015
	public string id;

	// Token: 0x04000BC8 RID: 3016
	public GameObject prefab;

	// Token: 0x04000BC9 RID: 3017
	public ItemManager.ItemType itemType;
}
