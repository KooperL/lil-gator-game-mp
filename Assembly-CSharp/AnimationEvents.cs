using System;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvents : MonoBehaviour
{
	// Token: 0x060001EF RID: 495 RVA: 0x0000A99F File Offset: 0x00008B9F
	private void Awake()
	{
		this.footsteps = base.GetComponent<Footsteps>();
		this.actor = base.GetComponent<DialogueActor>();
	}

	// Token: 0x060001F0 RID: 496 RVA: 0x0000A9BC File Offset: 0x00008BBC
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

	// Token: 0x060001F1 RID: 497 RVA: 0x0000AA58 File Offset: 0x00008C58
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

	// Token: 0x060001F2 RID: 498 RVA: 0x0000AAF8 File Offset: 0x00008CF8
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

	// Token: 0x060001F3 RID: 499 RVA: 0x0000AB60 File Offset: 0x00008D60
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

	// Token: 0x060001F4 RID: 500 RVA: 0x0000AC80 File Offset: 0x00008E80
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

	// Token: 0x060001F5 RID: 501 RVA: 0x0000AD88 File Offset: 0x00008F88
	private void ClearContinuousSound()
	{
		this.hasContinousHook = false;
		this.actor.onChangeStateOrEmote.RemoveListener(new UnityAction(this.ClearContinuousSound));
		if (this.continuousPing != null && this.continuousPing.gameObject != null)
		{
			Object.Destroy(this.continuousPing.gameObject);
		}
	}

	// Token: 0x060001F6 RID: 502 RVA: 0x0000ADE9 File Offset: 0x00008FE9
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
