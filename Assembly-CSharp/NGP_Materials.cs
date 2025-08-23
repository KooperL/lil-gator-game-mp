using System;
using UnityEngine;

public class NGP_Materials : MonoBehaviour
{
	// Token: 0x0600099A RID: 2458 RVA: 0x0003B2B8 File Offset: 0x000394B8
	private void Start()
	{
		foreach (NGP_Materials.MaterialAltTextures materialAltTextures in this.materialAltTextures)
		{
			materialAltTextures.material.mainTexture = (Game.IsNewGamePlus ? materialAltTextures.altTexture : materialAltTextures.defaultTexture);
		}
	}

	public Material[] materials;

	public NGP_Materials.MaterialAltTextures[] materialAltTextures;

	[Serializable]
	public struct MaterialAltTextures
	{
		// Token: 0x0600099C RID: 2460 RVA: 0x000094DE File Offset: 0x000076DE
		public MaterialAltTextures(Material material)
		{
			this.name = material.name;
			this.material = material;
			this.defaultTexture = material.mainTexture as Texture2D;
			this.altTexture = null;
		}

		public string name;

		public Material material;

		public Texture2D defaultTexture;

		public Texture2D altTexture;
	}
}
