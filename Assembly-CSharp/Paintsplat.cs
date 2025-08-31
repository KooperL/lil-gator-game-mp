using System;
using UnityEngine;

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

	public bool isRandomized = true;

	public Gradient randomColor;

	public Color color;

	private SetDecal setDecal;

	public AnimationCurve alphaCurve;

	public float curveSpeed = 0.25f;

	private float t;

	public ParticleSystem particleSystem;
}
