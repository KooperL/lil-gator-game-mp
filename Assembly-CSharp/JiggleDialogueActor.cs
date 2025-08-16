using System;
using UnityEngine;

public class JiggleDialogueActor : DialogueActor
{
	// Token: 0x060004C3 RID: 1219 RVA: 0x00005779 File Offset: 0x00003979
	protected override void Start()
	{
		this.perlinSeed = 1000f * global::UnityEngine.Random.value;
	}

	// Token: 0x060004C4 RID: 1220 RVA: 0x0002C36C File Offset: 0x0002A56C
	private Vector3 GetPerlin()
	{
		float num = this.perlinSpeed * Time.time;
		return Vector3.ClampMagnitude(new Vector3(Mathf.PerlinNoise(this.perlinSeed, num), Mathf.PerlinNoise(this.perlinSeed, 10000f + num), Mathf.PerlinNoise(this.perlinSeed, 20000f + num)) * 2f - Vector3.one, 1f);
	}

	// Token: 0x060004C5 RID: 1221 RVA: 0x0002C3DC File Offset: 0x0002A5DC
	protected override void UpdateMouthFlap()
	{
		float num = Mathf.SmoothStep(0f, 1f, this.mouthControl);
		this.jiggleTransform.localPosition = num * this.displacement * this.GetPerlin();
	}

	// Token: 0x060004C6 RID: 1222 RVA: 0x00005708 File Offset: 0x00003908
	public override void MouthOpen()
	{
		this.mouthOpen = true;
		base.enabled = true;
		this.mouthOpenTime = Time.time;
		PlayAudio.p.PlayVoice(base.transform.position, this.voicePitchMultiplier, this.voiceVarianceMultiplier);
	}

	[Header("Jiggle Settings (Ignore everything above)")]
	public Transform jiggleTransform;

	public float displacement = 0.1f;

	public float perlinSpeed = 10f;

	private float perlinSeed;
}
