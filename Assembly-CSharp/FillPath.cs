using System;
using UnityEngine;

public class FillPath : GenericPath
{
	public GameObject prefab;

	public float prefabWidth = 10f;

	public float prefabSpacing;

	public bool lockToFlat = true;

	[ReadOnly]
	public GameObject[] filledObjects;
}
