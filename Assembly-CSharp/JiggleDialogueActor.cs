using System;
using UnityEngine;

// Token: 0x020000F4 RID: 244
public class JiggleDialogueActor : DialogueActor
{
	// Token: 0x0600049C RID: 1180 RVA: 0x00005546 File Offset: 0x00003746
	protected override void Start()
	{
		this.perlinSeed = 1000f * Random.value;
	}

	// Token: 0x0600049D RID: 1181 RVA: 0x0002B398 File Offset: 0x00029598
	private Vector3 GetPerlin()
	{
		float num = this.perlinSpeed * Time.time;
		return Vector3.ClampMagnitude(new Vector3(Mathf.PerlinNoise(this.perlinSeed, num), Mathf.PerlinNoise(this.perlinSeed, 10000f + num), Mathf.PerlinNoise(this.perlinSeed, 20000f + num)) * 2f - Vector3.one, 1f);
	}

	// Token: 0x0600049E RID: 1182 RVA: 0x0002B408 File Offset: 0x00029608
	protected override void UpdateMouthFlap()
	{
		float num = Mathf.SmoothStep(0f, 1f, this.mouthControl);
		this.jiggleTransform.localPosition = num * this.displacement * this.GetPerlin();
	}

	// Token: 0x0600049F RID: 1183 RVA: 0x000054D5 File Offset: 0x000036D5
	public override void MouthOpen()
	{
		this.mouthOpen = true;
		base.enabled = true;
		this.mouthOpenTime = Time.time;
		PlayAudio.p.PlayVoice(base.transform.position, this.voicePitchMultiplier, this.voiceVarianceMultiplier);
	}

	// Token: 0x04000683 RID: 1667
	[Header("Jiggle Settings (Ignore everything above)")]
	public Transform jiggleTransform;

	// Token: 0x04000684 RID: 1668
	public float displacement = 0.1f;

	// Token: 0x04000685 RID: 1669
	public float perlinSpeed = 10f;

	// Token: 0x04000686 RID: 1670
	private float perlinSeed;
}
