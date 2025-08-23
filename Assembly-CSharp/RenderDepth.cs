using System;
using UnityEngine;

[ExecuteInEditMode]
public class RenderDepth : MonoBehaviour
{
	// (get) Token: 0x06000662 RID: 1634 RVA: 0x00031548 File Offset: 0x0002F748
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

	// (get) Token: 0x06000663 RID: 1635 RVA: 0x00006958 File Offset: 0x00004B58
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

	// Token: 0x06000664 RID: 1636 RVA: 0x0000698C File Offset: 0x00004B8C
	private void Awake()
	{
		this.camera = base.GetComponent<Camera>();
	}

	// Token: 0x06000665 RID: 1637 RVA: 0x00031580 File Offset: 0x0002F780
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

	// Token: 0x06000666 RID: 1638 RVA: 0x0000699A File Offset: 0x00004B9A
	private void OnDisable()
	{
		if (this._material != null)
		{
			global::UnityEngine.Object.DestroyImmediate(this._material);
		}
	}

	// Token: 0x06000667 RID: 1639 RVA: 0x000069B5 File Offset: 0x00004BB5
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

	[Range(0f, 3f)]
	public float depthLevel = 0.5f;

	private Shader _shader;

	private Material _material;

	private Camera camera;
}
