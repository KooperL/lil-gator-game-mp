using System;
using UnityEngine;

// Token: 0x02000149 RID: 329
[ExecuteInEditMode]
public class RenderDepth : MonoBehaviour
{
	// Token: 0x1700009D RID: 157
	// (get) Token: 0x06000628 RID: 1576 RVA: 0x0002FE38 File Offset: 0x0002E038
	private Shader shader
	{
		get
		{
			if (!(this._shader != null))
			{
				return this._shader = Shader.Find("Custom/RenderDepth");
			}
			return this._shader;
		}
	}

	// Token: 0x1700009E RID: 158
	// (get) Token: 0x06000629 RID: 1577 RVA: 0x00006692 File Offset: 0x00004892
	private Material material
	{
		get
		{
			if (this._material == null)
			{
				this._material = new Material(this.shader);
				this._material.hideFlags = 61;
			}
			return this._material;
		}
	}

	// Token: 0x0600062A RID: 1578 RVA: 0x000066C6 File Offset: 0x000048C6
	private void Awake()
	{
		this.camera = base.GetComponent<Camera>();
	}

	// Token: 0x0600062B RID: 1579 RVA: 0x0002FE70 File Offset: 0x0002E070
	private void Start()
	{
		if (this.shader == null || !this.shader.isSupported)
		{
			base.enabled = false;
			MonoBehaviour.print("Shader " + this.shader.name + " is not supported");
			return;
		}
		this.camera.depthTextureMode = 1;
	}

	// Token: 0x0600062C RID: 1580 RVA: 0x000066D4 File Offset: 0x000048D4
	private void OnDisable()
	{
		if (this._material != null)
		{
			Object.DestroyImmediate(this._material);
		}
	}

	// Token: 0x0600062D RID: 1581 RVA: 0x000066EF File Offset: 0x000048EF
	private void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		if (this.shader != null)
		{
			this.material.SetFloat("_DepthLevel", this.depthLevel);
			Graphics.Blit(src, dest, this.material);
			return;
		}
		Graphics.Blit(src, dest);
	}

	// Token: 0x04000848 RID: 2120
	[Range(0f, 3f)]
	public float depthLevel = 0.5f;

	// Token: 0x04000849 RID: 2121
	private Shader _shader;

	// Token: 0x0400084A RID: 2122
	private Material _material;

	// Token: 0x0400084B RID: 2123
	private Camera camera;
}
