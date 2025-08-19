using System;
using UnityEngine;

public class Paintsplat : MonoBehaviour
{
	// Token: 0x06000113 RID: 275 RVA: 0x00002DF0 File Offset: 0x00000FF0
	public void SetColor(Color color)
	{
		this.color = color;
		if (this.setDecal != null)
		{
			this.UpdateDecal();
		}
	}

	// Token: 0x06000114 RID: 276 RVA: 0x00002E0D File Offset: 0x0000100D
	private void Start()
	{
		if (this.isRandomized)
		{
			this.color = this.randomColor.Evaluate(global::UnityEngine.Random.value);
		}
		this.setDecal = base.GetComponent<SetDecal>();
		this.t = 0f;
		this.UpdateDecal();
	}

	// Token: 0x06000115 RID: 277 RVA: 0x00002E4A File Offset: 0x0000104A
	private void Update()
	{
		this.t += Time.deltaTime * this.curveSpeed;
		this.UpdateDecal();
		if (this.t >= 1f)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000116 RID: 278 RVA: 0x0001B724 File Offset: 0x00019924
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
