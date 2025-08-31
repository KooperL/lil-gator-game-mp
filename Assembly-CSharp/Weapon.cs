using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour, IItemBehaviour
{
	// (get) Token: 0x060009F5 RID: 2549 RVA: 0x0002E552 File Offset: 0x0002C752
	private PlayerItemManager itemManager
	{
		get
		{
			return Player.itemManager;
		}
	}

	// Token: 0x060009F6 RID: 2550 RVA: 0x0002E559 File Offset: 0x0002C759
	private void Awake()
	{
		this.audioVariance = base.GetComponent<AudioSourceVariance>();
		this.waitForPointOne = new WaitForSeconds(0.1f);
	}

	// Token: 0x060009F7 RID: 2551 RVA: 0x0002E577 File Offset: 0x0002C777
	public void Cancel()
	{
		this.isSwinging = false;
		this.itemManager.SetItemInUse(this, false);
	}

	// Token: 0x060009F8 RID: 2552 RVA: 0x0002E58D File Offset: 0x0002C78D
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

	// Token: 0x060009F9 RID: 2553 RVA: 0x0002E5D0 File Offset: 0x0002C7D0
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

	// Token: 0x060009FA RID: 2554 RVA: 0x0002E632 File Offset: 0x0002C832
	public void PlayAudio()
	{
		this.audioVariance.Play();
	}

	public void StartSwing()
	{
		Player.itemManager.isWeaponAttacking = true;
		this.onSwing.Invoke();
		this.trail.Clear();
		this.trail.emitting = true;
		this.triggers.SetActive(true);
	}

	public void StopSwing()
	{
		Player.itemManager.isWeaponAttacking = false;
		this.trail.emitting = false;
		this.triggers.SetActive(false);
	}

	// Token: 0x060009FD RID: 2557 RVA: 0x0002E689 File Offset: 0x0002C889
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

	// Token: 0x060009FE RID: 2558 RVA: 0x0002E698 File Offset: 0x0002C898
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

	// Token: 0x060009FF RID: 2559 RVA: 0x0002E72A File Offset: 0x0002C92A
	public void OnRemove()
	{
	}

	// Token: 0x06000A00 RID: 2560 RVA: 0x0002E72C File Offset: 0x0002C92C
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

	// Token: 0x06000A01 RID: 2561 RVA: 0x0002E77F File Offset: 0x0002C97F
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

	// Token: 0x06000A02 RID: 2562 RVA: 0x0002E78E File Offset: 0x0002C98E
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
