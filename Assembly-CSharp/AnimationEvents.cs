using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000099 RID: 153
public class AnimationEvents : MonoBehaviour
{
	// Token: 0x06000226 RID: 550 RVA: 0x00003C6D File Offset: 0x00001E6D
	private void Awake()
	{
		this.footsteps = base.GetComponent<Footsteps>();
		this.actor = base.GetComponent<DialogueActor>();
	}

	// Token: 0x06000227 RID: 551 RVA: 0x0001E61C File Offset: 0x0001C81C
	public void LeftStep()
	{
		if (this.lastStep > 0f && Time.time - this.lastStep < 0.5f)
		{
			return;
		}
		this.lastStep = Time.time;
		if (this.footsteps != null)
		{
			this.footsteps.DoStep(true);
			return;
		}
		SurfaceMaterial surfaceMaterial = MaterialManager.m.SampleSurfaceMaterial(base.transform.position, Vector3.down);
		if (surfaceMaterial != null && surfaceMaterial.HasFootstep)
		{
			surfaceMaterial.PlayFootstep(base.transform.position, 0.5f, 0.95f);
		}
	}

	// Token: 0x06000228 RID: 552 RVA: 0x0001E6B8 File Offset: 0x0001C8B8
	public void RightStep()
	{
		if (this.lastStep < 0f && Time.time - -this.lastStep < 0.5f)
		{
			return;
		}
		this.lastStep = -Time.time;
		if (this.footsteps != null)
		{
			this.footsteps.DoStep(false);
			return;
		}
		SurfaceMaterial surfaceMaterial = MaterialManager.m.SampleSurfaceMaterial(base.transform.position, Vector3.down);
		if (surfaceMaterial != null && surfaceMaterial.HasFootstep)
		{
			surfaceMaterial.PlayFootstep(base.transform.position, 0.5f, 1f);
		}
	}

	// Token: 0x06000229 RID: 553 RVA: 0x0001E758 File Offset: 0x0001C958
	public void PlaySound(AnimationEvent animEvent)
	{
		if (animEvent.objectReferenceParameter == null)
		{
			return;
		}
		AudioClip audioClip = animEvent.objectReferenceParameter as AudioClip;
		if (audioClip == null)
		{
			return;
		}
		float num = animEvent.floatParameter;
		if (num == 0f)
		{
			num = 1f;
		}
		PlayAudio.p.PlayAtPoint(audioClip, base.transform.position, num, 1f, 128);
	}

	// Token: 0x0600022A RID: 554 RVA: 0x0001E7C0 File Offset: 0x0001C9C0
	public void PlayEffect(AnimationEvent animEvent)
	{
		int intParameter = animEvent.intParameter;
		string text = animEvent.stringParameter.ToLower();
		float floatParameter = animEvent.floatParameter;
		Vector3 vector = base.transform.TransformPoint(floatParameter * Vector3.up);
		if (text != null)
		{
			if (text == "dust")
			{
				EffectsManager.e.Dust(vector, intParameter);
				return;
			}
			if (text == "floordust" || text == "floor dust")
			{
				EffectsManager.e.FloorDust(vector, intParameter, Vector3.up);
				return;
			}
			if (text == "splash")
			{
				EffectsManager.e.Splash(vector, 0.8f);
				return;
			}
			if (text == "dig")
			{
				EffectsManager.e.Dig(vector);
				return;
			}
			if (!(text == "whomp"))
			{
				return;
			}
			Vector3 vector2 = base.transform.position + -0.25f * base.transform.forward;
			EffectsManager.e.FloorDust(vector2, 5, Vector3.up);
			EffectsManager.e.TryToPlayHitSound(vector2, Vector3.down, 1f);
		}
	}

	// Token: 0x0600022B RID: 555 RVA: 0x0001E8E0 File Offset: 0x0001CAE0
	public void PlayContinuousSound(AnimationEvent animEvent)
	{
		if (Vector3.Distance(base.transform.position, MainCamera.t.position) > 30f)
		{
			return;
		}
		if (this.continuousSound == null)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(Prefabs.p.continuousSound, base.transform);
			this.continuousSound = gameObject.GetComponent<AudioSource>();
			this.continuousSound.clip = animEvent.objectReferenceParameter as AudioClip;
			this.continuousSound.Play();
			this.continuousPing = gameObject.GetComponent<DestroyUnlessPinged>();
			this.continuousPing.Ping();
		}
		else
		{
			this.continuousSound.clip = animEvent.objectReferenceParameter as AudioClip;
			this.continuousPing.Ping();
		}
		float floatParameter = animEvent.floatParameter;
		if (floatParameter != 0f)
		{
			this.continuousSound.volume = floatParameter;
		}
		if (!this.hasContinousHook && this.actor != null)
		{
			this.actor.onChangeStateOrEmote.AddListener(new UnityAction(this.ClearContinuousSound));
		}
	}

	// Token: 0x0600022C RID: 556 RVA: 0x0001E9E8 File Offset: 0x0001CBE8
	private void ClearContinuousSound()
	{
		this.hasContinousHook = false;
		this.actor.onChangeStateOrEmote.RemoveListener(new UnityAction(this.ClearContinuousSound));
		if (this.continuousPing != null && this.continuousPing.gameObject != null)
		{
			Object.Destroy(this.continuousPing.gameObject);
		}
	}

	// Token: 0x0600022D RID: 557 RVA: 0x00003C87 File Offset: 0x00001E87
	public void StartTransition(float duration)
	{
		if (this.transitionReciever != null)
		{
			this.transitionReciever.StartTransition(duration);
		}
	}

	// Token: 0x04000319 RID: 793
	private DialogueActor actor;

	// Token: 0x0400031A RID: 794
	private Footsteps footsteps;

	// Token: 0x0400031B RID: 795
	private float lastStep;

	// Token: 0x0400031C RID: 796
	private bool isPlayingContinuous;

	// Token: 0x0400031D RID: 797
	private const float continuousTimeout = 0.5f;

	// Token: 0x0400031E RID: 798
	private const float maxContinuousDistance = 30f;

	// Token: 0x0400031F RID: 799
	private AudioSource continuousSound;

	// Token: 0x04000320 RID: 800
	private DestroyUnlessPinged continuousPing;

	// Token: 0x04000321 RID: 801
	private bool hasContinousHook;

	// Token: 0x04000322 RID: 802
	public ITransitionReciever transitionReciever;
}
