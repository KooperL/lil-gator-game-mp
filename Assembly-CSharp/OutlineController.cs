using System;
using UnityEngine;

public class OutlineController : MonoBehaviour
{
	public HighlightsFX outlinePostEffect;

	public OutlineController.OutlineData[] outliners;

	[Serializable]
	public class OutlineData
	{
		public Color color = Color.white;

		public HighlightsFX.SortingType depthType;

		public Renderer renderer;
	}
}
