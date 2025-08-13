using System;
using UnityEngine;

// Token: 0x0200004C RID: 76
public class SetDecal : MonoBehaviour
{
	// Token: 0x06000110 RID: 272 RVA: 0x00002E39 File Offset: 0x00001039
	public void OnValidate()
	{
		this.UpdatePropertyBlock();
	}

	// Token: 0x06000111 RID: 273 RVA: 0x00002E41 File Offset: 0x00001041
	public void SetColor(Color color)
	{
		this.color = color;
		this.UpdatePropertyBlock();
	}

	// Token: 0x06000112 RID: 274 RVA: 0x0001AF3C File Offset: 0x0001913C
	public void UpdatePropertyBlock()
	{
		if (this.properties == null)
		{
			this.properties = new MaterialPropertyBlock();
			this.colorID = Shader.PropertyToID("_Color");
			if (this.renderer == null)
			{
				this.renderer = base.GetComponent<Renderer>();
			}
		}
		this.properties.SetColor(this.colorID, this.color);
		this.renderer.SetPropertyBlock(this.properties);
	}

	// Token: 0x04000189 RID: 393
	public Color color;

	// Token: 0x0400018A RID: 394
	private int colorID;

	// Token: 0x0400018B RID: 395
	private MaterialPropertyBlock properties;

	// Token: 0x0400018C RID: 396
	public Renderer renderer;
}
