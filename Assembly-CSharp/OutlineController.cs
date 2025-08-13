using System;
using UnityEngine;

// Token: 0x0200003B RID: 59
public class OutlineController : MonoBehaviour
{
	// Token: 0x0400012D RID: 301
	public HighlightsFX outlinePostEffect;

	// Token: 0x0400012E RID: 302
	public OutlineController.OutlineData[] outliners;

	// Token: 0x0200003C RID: 60
	[Serializable]
	public class OutlineData
	{
		// Token: 0x0400012F RID: 303
		public Color color = Color.white;

		// Token: 0x04000130 RID: 304
		public HighlightsFX.SortingType depthType;

		// Token: 0x04000131 RID: 305
		public Renderer renderer;
	}
}
