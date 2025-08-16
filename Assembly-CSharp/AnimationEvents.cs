using System;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvents : MonoBehaviour
{
	// Token: 0x06000233 RID: 563 RVA: 0x00003D59 File Offset: 0x00001F59
	private void Awake()
	{
		this.footsteps = base.GetComponent<Footsteps>();
		this.actor = base.GetComponent<DialogueActor>();
	}

	// Token: 0x06000234 RID: 564 RVA: 0x0001EEE4 File Offset: 0x0001D0E4
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

	// Token: 0x06000235 RID: 565 RVA: 0x0001EF80 File Offset: 0x0001D180
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

	// Token: 0x06000236 RID: 566 RVA: 0x0001F020 File Offset: 0x0001D220
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

	// Token: 0x06000237 RID: 567 RVA: 0x0001F088 File Offset: 0x0001D288
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

	// Token: 0x06000238 RID: 568 RVA: 0x0001F1A8 File Offset: 0x0001D3A8
	public void PlayContinuousSound(AnimationEvent animEvent)
	{
		if (Vector3.Distance(base.transform.position, MainCamera.t.position) > 30f)
		{
			return;
		}
		if (this.continuousSound == null)
		{
			GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(Prefabs.p.continuousSound, base.transform);
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

	// Token: 0x06000239 RID: 569 RVA: 0x0001F2B0 File Offset: 0x0001D4B0
	private void ClearContinuousSound()
	{
		this.hasContinousHook = false;
		this.actor.onChangeStateOrEmote.RemoveListener(new UnityAction(this.ClearContinuousSound));
		if (this.continuousPing != null && this.continuousPing.gameObject != null)
		{
			global::UnityEngine.Object.Destroy(this.continuousPing.gameObject);
		}
	}

	// Token: 0x0600023A RID: 570 RVA: 0x00003D73 File Offset: 0x00001F73
	public void StartTransition(float duration)
	{
		if (this.transitionReciever != null)
		{
			this.transitionReciever.StartTransition(duration);
		}
	}

	private DialogueActor actor;

	private Footsteps footsteps;

	private float lastStep;

	private bool isPlayingContinuous;

	private const float continuousTimeout = 0.5f;

	private const float maxContinuousDistance = 30f;

	private AudioSource continuousSound;

	private DestroyUnlessPinged continuousPing;

	private bool hasContinousHook;

	public ITransitionReciever transitionReciever;
}
