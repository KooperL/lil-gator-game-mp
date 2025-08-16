using System;
using UnityEngine;

public class BillboardLodSetup : MonoBehaviour
{
	public RenderTexture renderTexture;

	public GameObject[] sourcePrefabs;

	public GameObject[] sourcePrefabs_h;

	public bool useHighPrefabs;

	public GameObject[] placedPrefabs;

	public Mesh[] billboardMeshes;

	public GameObject[] lodPrefabs;

	public GameObject[] treePrefabs;

	public Material billboardMaterial;

	public GameObject billboardPrefab;

	public int rows = 2;

	public float size = 50f;

	public float buffer = 0.5f;

	public int[] rowIndices;

	public float[] rowWidths;
}
