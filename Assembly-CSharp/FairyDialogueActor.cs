using System;
using UnityEngine;

public class FairyDialogueActor : DialogueActor
{
	// Token: 0x060004B3 RID: 1203 RVA: 0x0002C154 File Offset: 0x0002A354
	protected override void Start()
	{
		this.perlinSeed = 1000f * global::UnityEngine.Random.value;
		this.anchorPoint = base.transform.localPosition;
		this.position = base.transform.position;
		this.mouth = 0f;
		this.UpdateMouthFlap();
		base.Start();
		base.enabled = true;
		this.originalParent = base.transform.parent;
		base.transform.parent = base.transform.parent.parent;
	}

	// Token: 0x060004B4 RID: 1204 RVA: 0x0002C1E0 File Offset: 0x0002A3E0
	private Vector3 GetPerlinOffset()
	{
		float num = this.perlinSpeed * Time.time;
		Vector3 vector = new Vector3(Mathf.PerlinNoise(this.perlinSeed, num), Mathf.PerlinNoise(this.perlinSeed, 10000f + num), Mathf.PerlinNoise(this.perlinSeed, 20000f + num));
		vector *= 2f;
		vector -= Vector3.one;
		return this.perlinRadius * Vector3.ClampMagnitude(vector, 1f);
	}

	// Token: 0x060004B5 RID: 1205 RVA: 0x0002C260 File Offset: 0x0002A460
	public override void LateUpdate()
	{
		base.LateUpdate();
		base.enabled = true;
		Vector3 vector = this.originalParent.TransformPoint(this.anchorPoint) + this.GetPerlinOffset();
		float num = this.smoothTime;
		float num2 = this.acceleration;
		if (DialogueManager.currentlySpeakingActor == this || DialogueManager.currentlySpeakingBubbleActor == this)
		{
			vector = this.originalParent.TransformPoint(this.talkingAnchorPoint);
			num = Mathf.Min(num, 0.2f);
			this.acceleration = Mathf.Max(this.acceleration, 20f);
		}
		this.position = Vector3.MoveTowards(vector, this.position, 5f);
		this.position = MathUtils.SmoothDampAcc(this.position, vector, ref this.velocity, num, this.acceleration);
		this.velocity = Vector3.ClampMagnitude(this.velocity, 15f);
		base.transform.position = this.position;
	}

	// Token: 0x060004B6 RID: 1206 RVA: 0x0002C350 File Offset: 0x0002A550
	protected override void UpdateMouthFlap()
	{
		float num = Mathf.SmoothStep(0f, 1f, this.mouthControl);
		this.fairySprite.transform.localScale = Mathf.Lerp(this.mouthClosedSize, this.mouthOpenSize, this.mouth * num) * Vector3.one;
		this.fairySprite.color = Color.Lerp(this.mouthClosedColor, this.mouthOpenColor, this.mouth * num);
	}

	// Token: 0x060004B7 RID: 1207 RVA: 0x00005708 File Offset: 0x00003908
	public override void MouthOpen()
	{
		this.mouthOpen = true;
		base.enabled = true;
		this.mouthOpenTime = Time.time;
		PlayAudio.p.PlayVoice(base.transform.position, this.voicePitchMultiplier, this.voiceVarianceMultiplier);
	}

	[Header("Fairy Settings (Ignore everything above)")]
	public SpriteRenderer fairySprite;

	public float mouthClosedSize = 0.2f;

	public float mouthOpenSize = 0.5f;

	public Color mouthClosedColor = Color.grey;

	public Color mouthOpenColor = Color.white;

	public float perlinRadius = 0.5f;

	public float perlinSpeed = 0.25f;

	private float perlinSeed;

	public float acceleration = 5f;

	public float smoothTime = 0.2f;

	private const float maxSpeed = 15f;

	private const float maxDistance = 5f;

	private Vector3 anchorPoint;

	public Vector3 talkingAnchorPoint;

	private Vector3 position;

	private Vector3 velocity;

	private Transform originalParent;
}
