using System;
using UnityEngine;

public class SetDecal : MonoBehaviour
{
	// Token: 0x06000118 RID: 280 RVA: 0x00002E9D File Offset: 0x0000109D
	public void OnValidate()
	{
		this.UpdatePropertyBlock();
	}

	// Token: 0x06000119 RID: 281 RVA: 0x00002EA5 File Offset: 0x000010A5
	public void SetColor(Color color)
	{
		this.color = color;
		this.UpdatePropertyBlock();
	}

	// Token: 0x0600011A RID: 282 RVA: 0x0001B77C File Offset: 0x0001997C
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

	public Color color;

	private int colorID;

	private MaterialPropertyBlock properties;

	public Renderer renderer;
}
