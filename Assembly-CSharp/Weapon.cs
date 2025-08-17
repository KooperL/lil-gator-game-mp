using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour, IItemBehaviour
{
	// (get) Token: 0x06000BEA RID: 3050 RVA: 0x0000B20A File Offset: 0x0000940A
	private PlayerItemManager itemManager
	{
		get
		{
			return Player.itemManager;
		}
	}

	// Token: 0x06000BEB RID: 3051 RVA: 0x0000B211 File Offset: 0x00009411
	private void Awake()
	{
		this.audioVariance = base.GetComponent<AudioSourceVariance>();
		this.waitForPointOne = new WaitForSeconds(0.1f);
	}

	// Token: 0x06000BEC RID: 3052 RVA: 0x0000B22F File Offset: 0x0000942F
	public void Cancel()
	{
		this.isSwinging = false;
		this.itemManager.SetItemInUse(this, false);
	}

	// Token: 0x06000BED RID: 3053 RVA: 0x0000B245 File Offset: 0x00009445
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

	// Token: 0x06000BEE RID: 3054 RVA: 0x00041D48 File Offset: 0x0003FF48
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

	// Token: 0x06000BEF RID: 3055 RVA: 0x0000B285 File Offset: 0x00009485
	public void PlayAudio()
	{
		this.audioVariance.Play();
	}

	// Token: 0x06000BF0 RID: 3056 RVA: 0x0000B292 File Offset: 0x00009492
	public void StartSwing()
	{
		this.onSwing.Invoke();
		this.trail.Clear();
		this.trail.emitting = true;
		this.triggers.SetActive(true);
	}

	// Token: 0x06000BF1 RID: 3057 RVA: 0x0000B2C2 File Offset: 0x000094C2
	public void StopSwing()
	{
		this.trail.emitting = false;
		this.triggers.SetActive(false);
	}

	// Token: 0x06000BF2 RID: 3058 RVA: 0x0000B2DC File Offset: 0x000094DC
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

	// Token: 0x06000BF3 RID: 3059 RVA: 0x00041DAC File Offset: 0x0003FFAC
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

	// Token: 0x06000BF4 RID: 3060 RVA: 0x00002229 File Offset: 0x00000429
	public void OnRemove()
	{
	}

	// Token: 0x06000BF5 RID: 3061 RVA: 0x00041E40 File Offset: 0x00040040
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

	// Token: 0x06000BF6 RID: 3062 RVA: 0x0000B2EB File Offset: 0x000094EB
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

	// Token: 0x06000BF7 RID: 3063 RVA: 0x00002229 File Offset: 0x00000429
	public void SetIndex(int index)
	{
	}

	public static float lastWeaponSwingTime = -1f;

	public static float lastWeaponHitTime = -1f;

	public string id;

	public TrailRenderer trail;

	public Collider hitbox;

	public GameObject triggers;

	private AudioSourceVariance audioVariance;

	private YieldInstruction waitForPointOne;

	private bool isSwinging;

	public UnityEvent onSwing;

	public UnityEvent onHit;

	public UnityEvent onEquip;

	public UnityEvent onUnequip;

	private IEnumerator useWeaponCoroutine;

	private static float lastHitPause = 0f;

	private const float minHitPauseDelay = 0.5f;

	private WaitForSeconds waitHitPause;
}
