using System;
using UnityEngine;

// Token: 0x020000F6 RID: 246
[ExecuteInEditMode]
public class RenderDepth : MonoBehaviour
{
	// Token: 0x17000043 RID: 67
	// (get) Token: 0x06000516 RID: 1302 RVA: 0x0001B528 File Offset: 0x00019728
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

	// Token: 0x17000044 RID: 68
	// (get) Token: 0x06000517 RID: 1303 RVA: 0x0001B55D File Offset: 0x0001975D
	private Material material
	{
		get
		{
			if (this._material == null)
			{
				this._material = new Material(this.shader);
				this._material.hideFlags = HideFlags.HideAndDontSave;
			}
			return this._material;
		}
	}

	// Token: 0x06000518 RID: 1304 RVA: 0x0001B591 File Offset: 0x00019791
	private void Awake()
	{
		this.camera = base.GetComponent<Camera>();
	}

	// Token: 0x06000519 RID: 1305 RVA: 0x0001B5A0 File Offset: 0x000197A0
	private void Start()
	{
		if (this.shader == null || !this.shader.isSupported)
		{
			base.enabled = false;
			MonoBehaviour.print("Shader " + this.shader.name + " is not supported");
			return;
		}
		this.camera.depthTextureMode = DepthTextureMode.Depth;
	}

	// Token: 0x0600051A RID: 1306 RVA: 0x0001B5FB File Offset: 0x000197FB
	private void OnDisable()
	{
		if (this._material != null)
		{
			Object.DestroyImmediate(this._material);
		}
	}

	// Token: 0x0600051B RID: 1307 RVA: 0x0001B616 File Offset: 0x00019816
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

	// Token: 0x04000701 RID: 1793
	[Range(0f, 3f)]
	public float depthLevel = 0.5f;

	// Token: 0x04000702 RID: 1794
	private Shader _shader;

	// Token: 0x04000703 RID: 1795
	private Material _material;

	// Token: 0x04000704 RID: 1796
	private Camera camera;
}
