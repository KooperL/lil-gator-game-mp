using System;
using UnityEngine;

// Token: 0x0200003A RID: 58
public class SetDecal : MonoBehaviour
{
	// Token: 0x060000EB RID: 235 RVA: 0x00006528 File Offset: 0x00004728
	public void OnValidate()
	{
		this.UpdatePropertyBlock();
	}

	// Token: 0x060000EC RID: 236 RVA: 0x00006530 File Offset: 0x00004730
	public void SetColor(Color color)
	{
		this.color = color;
		this.UpdatePropertyBlock();
	}

	// Token: 0x060000ED RID: 237 RVA: 0x00006540 File Offset: 0x00004740
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

	// Token: 0x04000142 RID: 322
	public Color color;

	// Token: 0x04000143 RID: 323
	private int colorID;

	// Token: 0x04000144 RID: 324
	private MaterialPropertyBlock properties;

	// Token: 0x04000145 RID: 325
	public Renderer renderer;
}
