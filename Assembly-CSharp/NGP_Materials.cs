using System;
using UnityEngine;

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

	public Material[] materials;

	public NGP_Materials.MaterialAltTextures[] materialAltTextures;

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

		public string name;

		public Material material;

		public Texture2D defaultTexture;

		public Texture2D altTexture;
	}
}
