using System;
using UnityEngine;

// Token: 0x02000031 RID: 49
public class OutlineController : MonoBehaviour
{
	// Token: 0x04000106 RID: 262
	public HighlightsFX outlinePostEffect;

	// Token: 0x04000107 RID: 263
	public OutlineController.OutlineData[] outliners;

	// Token: 0x02000359 RID: 857
	[Serializable]
	public class OutlineData
	{
		// Token: 0x040019F9 RID: 6649
		public Color color = Color.white;

		// Token: 0x040019FA RID: 6650
		public HighlightsFX.SortingType depthType;

		// Token: 0x040019FB RID: 6651
		public Renderer renderer;
	}
}
