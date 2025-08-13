using System;
using UnityEngine;

// Token: 0x0200004B RID: 75
public class Paintsplat : MonoBehaviour
{
	// Token: 0x0600010B RID: 267 RVA: 0x00002D8C File Offset: 0x00000F8C
	public void SetColor(Color color)
	{
		this.color = color;
		if (this.setDecal != null)
		{
			this.UpdateDecal();
		}
	}

	// Token: 0x0600010C RID: 268 RVA: 0x00002DA9 File Offset: 0x00000FA9
	private void Start()
	{
		if (this.isRandomized)
		{
			this.color = this.randomColor.Evaluate(Random.value);
		}
		this.setDecal = base.GetComponent<SetDecal>();
		this.t = 0f;
		this.UpdateDecal();
	}

	// Token: 0x0600010D RID: 269 RVA: 0x00002DE6 File Offset: 0x00000FE6
	private void Update()
	{
		this.t += Time.deltaTime * this.curveSpeed;
		this.UpdateDecal();
		if (this.t >= 1f)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0600010E RID: 270 RVA: 0x0001AEE4 File Offset: 0x000190E4
	private void UpdateDecal()
	{
		Color color = this.color;
		color.a = this.alphaCurve.Evaluate(this.t);
		this.setDecal.SetColor(color);
		this.particleSystem.main.startColor = this.color;
	}

	// Token: 0x04000181 RID: 385
	public bool isRandomized = true;

	// Token: 0x04000182 RID: 386
	public Gradient randomColor;

	// Token: 0x04000183 RID: 387
	public Color color;

	// Token: 0x04000184 RID: 388
	private SetDecal setDecal;

	// Token: 0x04000185 RID: 389
	public AnimationCurve alphaCurve;

	// Token: 0x04000186 RID: 390
	public float curveSpeed = 0.25f;

	// Token: 0x04000187 RID: 391
	private float t;

	// Token: 0x04000188 RID: 392
	public ParticleSystem particleSystem;
}
