using System;
using UnityEngine;

// Token: 0x02000039 RID: 57
public class Paintsplat : MonoBehaviour
{
	// Token: 0x060000E6 RID: 230 RVA: 0x00006425 File Offset: 0x00004625
	public void SetColor(Color color)
	{
		this.color = color;
		if (this.setDecal != null)
		{
			this.UpdateDecal();
		}
	}

	// Token: 0x060000E7 RID: 231 RVA: 0x00006442 File Offset: 0x00004642
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

	// Token: 0x060000E8 RID: 232 RVA: 0x0000647F File Offset: 0x0000467F
	private void Update()
	{
		this.t += Time.deltaTime * this.curveSpeed;
		this.UpdateDecal();
		if (this.t >= 1f)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060000E9 RID: 233 RVA: 0x000064B8 File Offset: 0x000046B8
	private void UpdateDecal()
	{
		Color color = this.color;
		color.a = this.alphaCurve.Evaluate(this.t);
		this.setDecal.SetColor(color);
		this.particleSystem.main.startColor = this.color;
	}

	// Token: 0x0400013A RID: 314
	public bool isRandomized = true;

	// Token: 0x0400013B RID: 315
	public Gradient randomColor;

	// Token: 0x0400013C RID: 316
	public Color color;

	// Token: 0x0400013D RID: 317
	private SetDecal setDecal;

	// Token: 0x0400013E RID: 318
	public AnimationCurve alphaCurve;

	// Token: 0x0400013F RID: 319
	public float curveSpeed = 0.25f;

	// Token: 0x04000140 RID: 320
	private float t;

	// Token: 0x04000141 RID: 321
	public ParticleSystem particleSystem;
}
