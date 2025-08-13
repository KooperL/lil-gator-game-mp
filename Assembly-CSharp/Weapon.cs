using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000263 RID: 611
public class Weapon : MonoBehaviour, IItemBehaviour
{
	// Token: 0x17000111 RID: 273
	// (get) Token: 0x06000B9E RID: 2974 RVA: 0x0000AF02 File Offset: 0x00009102
	private PlayerItemManager itemManager
	{
		get
		{
			return Player.itemManager;
		}
	}

	// Token: 0x06000B9F RID: 2975 RVA: 0x0000AF09 File Offset: 0x00009109
	private void Awake()
	{
		this.audioVariance = base.GetComponent<AudioSourceVariance>();
		this.waitForPointOne = new WaitForSeconds(0.1f);
	}

	// Token: 0x06000BA0 RID: 2976 RVA: 0x0000AF27 File Offset: 0x00009127
	public void Cancel()
	{
		this.isSwinging = false;
		this.itemManager.SetItemInUse(this, false);
	}

	// Token: 0x06000BA1 RID: 2977 RVA: 0x0000AF3D File Offset: 0x0000913D
	private void OnDisable()
	{
		if (this.isSwinging)
		{
			this.isSwinging = false;
			this.itemManager.SetItemInUse(this, false);
			this.StopSwing();
		}
		if (Player.animator != null)
		{
			Player.animator.speed = 1f;
		}
	}

	// Token: 0x06000BA2 RID: 2978 RVA: 0x000401C0 File Offset: 0x0003E3C0
	public void Input(bool isDown, bool isHeld)
	{
		if (isDown && !this.isSwinging)
		{
			this.itemManager.SetEquippedState(PlayerItemManager.EquippedState.SwordAndShield, false);
			Weapon.lastWeaponSwingTime = Time.time;
			if (this.useWeaponCoroutine != null)
			{
				base.StopCoroutine(this.useWeaponCoroutine);
				this.StopSwing();
			}
			this.useWeaponCoroutine = this.UseWeapon();
			base.StartCoroutine(this.useWeaponCoroutine);
		}
	}

	// Token: 0x06000BA3 RID: 2979 RVA: 0x0000AF7D File Offset: 0x0000917D
	public void PlayAudio()
	{
		this.audioVariance.Play();
	}

	// Token: 0x06000BA4 RID: 2980 RVA: 0x0000AF8A File Offset: 0x0000918A
	public void StartSwing()
	{
		this.onSwing.Invoke();
		this.trail.Clear();
		this.trail.emitting = true;
		this.triggers.SetActive(true);
	}

	// Token: 0x06000BA5 RID: 2981 RVA: 0x0000AFBA File Offset: 0x000091BA
	public void StopSwing()
	{
		this.trail.emitting = false;
		this.triggers.SetActive(false);
	}

	// Token: 0x06000BA6 RID: 2982 RVA: 0x0000AFD4 File Offset: 0x000091D4
	private IEnumerator UseWeapon()
	{
		this.isSwinging = true;
		this.itemManager.itemInUse = this;
		this.itemManager.animator.SetTrigger("Attack");
		this.PlayAudio();
		yield return this.waitForPointOne;
		this.StartSwing();
		this.itemManager.CutGrass();
		yield return this.waitForPointOne;
		this.isSwinging = false;
		yield return this.waitForPointOne;
		this.StopSwing();
		this.itemManager.SetItemInUse(this, false);
		this.useWeaponCoroutine = null;
		yield break;
	}

	// Token: 0x06000BA7 RID: 2983 RVA: 0x00040224 File Offset: 0x0003E424
	public void SetEquipped(bool isEquipped)
	{
		Transform transform = (isEquipped ? this.itemManager.leftHandAnchor : this.itemManager.swordUnequippedAnchor);
		if (base.transform.parent != transform)
		{
			base.transform.parent = transform;
			base.transform.localPosition = Vector3.zero;
			base.transform.localRotation = Quaternion.identity;
			base.transform.localScale = Vector3.one;
		}
		if (isEquipped)
		{
			this.onEquip.Invoke();
			return;
		}
		this.onUnequip.Invoke();
	}

	// Token: 0x06000BA8 RID: 2984 RVA: 0x00002229 File Offset: 0x00000429
	public void OnRemove()
	{
	}

	// Token: 0x06000BA9 RID: 2985 RVA: 0x000402B8 File Offset: 0x0003E4B8
	public void OnHit()
	{
		Weapon.lastWeaponHitTime = Time.time;
		if (this.onHit != null)
		{
			this.onHit.Invoke();
		}
		if (Time.time - Weapon.lastHitPause > 0.5f)
		{
			Weapon.lastHitPause = Time.time;
			base.StartCoroutine(this.RunHitPause());
		}
	}

	// Token: 0x06000BAA RID: 2986 RVA: 0x0000AFE3 File Offset: 0x000091E3
	private IEnumerator RunHitPause()
	{
		Player.animator.speed = 0f;
		if (this.waitHitPause == null)
		{
			this.waitHitPause = new WaitForSeconds(0.06f);
		}
		yield return this.waitHitPause;
		Player.animator.speed = 1f;
		yield break;
	}

	// Token: 0x06000BAB RID: 2987 RVA: 0x00002229 File Offset: 0x00000429
	public void SetIndex(int index)
	{
	}

	// Token: 0x04000E81 RID: 3713
	public static float lastWeaponSwingTime = -1f;

	// Token: 0x04000E82 RID: 3714
	public static float lastWeaponHitTime = -1f;

	// Token: 0x04000E83 RID: 3715
	public string id;

	// Token: 0x04000E84 RID: 3716
	public TrailRenderer trail;

	// Token: 0x04000E85 RID: 3717
	public Collider hitbox;

	// Token: 0x04000E86 RID: 3718
	public GameObject triggers;

	// Token: 0x04000E87 RID: 3719
	private AudioSourceVariance audioVariance;

	// Token: 0x04000E88 RID: 3720
	private YieldInstruction waitForPointOne;

	// Token: 0x04000E89 RID: 3721
	private bool isSwinging;

	// Token: 0x04000E8A RID: 3722
	public UnityEvent onSwing;

	// Token: 0x04000E8B RID: 3723
	public UnityEvent onHit;

	// Token: 0x04000E8C RID: 3724
	public UnityEvent onEquip;

	// Token: 0x04000E8D RID: 3725
	public UnityEvent onUnequip;

	// Token: 0x04000E8E RID: 3726
	private IEnumerator useWeaponCoroutine;

	// Token: 0x04000E8F RID: 3727
	private static float lastHitPause = 0f;

	// Token: 0x04000E90 RID: 3728
	private const float minHitPauseDelay = 0.5f;

	// Token: 0x04000E91 RID: 3729
	private WaitForSeconds waitHitPause;
}
