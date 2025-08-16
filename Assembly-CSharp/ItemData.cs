using System;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
	public string id;

	public GameObject prefab;

	public ItemManager.ItemType itemType;
}
