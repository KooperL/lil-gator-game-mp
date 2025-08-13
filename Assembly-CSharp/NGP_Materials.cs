using System;
using UnityEngine;

// Token: 0x02000183 RID: 387
public class NGP_Materials : MonoBehaviour
{
	// Token: 0x060007F6 RID: 2038 RVA: 0x000267E0 File Offset: 0x000249E0
	private void Start()
	{
		foreach (NGP_Materials.MaterialAltTextures materialAltTextures in this.materialAltTextures)
		{
			materialAltTextures.material.mainTexture = (Game.IsNewGamePlus ? materialAltTextures.altTexture : materialAltTextures.defaultTexture);
		}
	}

	// Token: 0x04000A27 RID: 2599
	public Material[] materials;

	// Token: 0x04000A28 RID: 2600
	public NGP_Materials.MaterialAltTextures[] materialAltTextures;

	// Token: 0x020003CD RID: 973
	[Serializable]
	public struct MaterialAltTextures
	{
		// Token: 0x06001996 RID: 6550 RVA: 0x0006D820 File Offset: 0x0006BA20
		public MaterialAltTextures(Material material)
		{
			this.name = material.name;
			this.material = material;
			this.defaultTexture = material.mainTexture as Texture2D;
			this.altTexture = null;
		}

		// Token: 0x04001BF9 RID: 7161
		public string name;

		// Token: 0x04001BFA RID: 7162
		public Material material;

		// Token: 0x04001BFB RID: 7163
		public Texture2D defaultTexture;

		// Token: 0x04001BFC RID: 7164
		public Texture2D altTexture;
	}
}
