using System;
using UnityEngine;

// Token: 0x020000B5 RID: 181
public class JiggleDialogueActor : DialogueActor
{
	// Token: 0x060003E9 RID: 1001 RVA: 0x00016FC8 File Offset: 0x000151C8
	protected override void Start()
	{
		this.perlinSeed = 1000f * Random.value;
	}

	// Token: 0x060003EA RID: 1002 RVA: 0x00016FDC File Offset: 0x000151DC
	private Vector3 GetPerlin()
	{
		float num = this.perlinSpeed * Time.time;
		return Vector3.ClampMagnitude(new Vector3(Mathf.PerlinNoise(this.perlinSeed, num), Mathf.PerlinNoise(this.perlinSeed, 10000f + num), Mathf.PerlinNoise(this.perlinSeed, 20000f + num)) * 2f - Vector3.one, 1f);
	}

	// Token: 0x060003EB RID: 1003 RVA: 0x0001704C File Offset: 0x0001524C
	protected override void UpdateMouthFlap()
	{
		float num = Mathf.SmoothStep(0f, 1f, this.mouthControl);
		this.jiggleTransform.localPosition = num * this.displacement * this.GetPerlin();
	}

	// Token: 0x060003EC RID: 1004 RVA: 0x0001708D File Offset: 0x0001528D
	public override void MouthOpen()
	{
		this.mouthOpen = true;
		base.enabled = true;
		this.mouthOpenTime = Time.time;
		PlayAudio.p.PlayVoice(base.transform.position, this.voicePitchMultiplier, this.voiceVarianceMultiplier);
	}

	// Token: 0x04000571 RID: 1393
	[Header("Jiggle Settings (Ignore everything above)")]
	public Transform jiggleTransform;

	// Token: 0x04000572 RID: 1394
	public float displacement = 0.1f;

	// Token: 0x04000573 RID: 1395
	public float perlinSpeed = 10f;

	// Token: 0x04000574 RID: 1396
	private float perlinSeed;
}
