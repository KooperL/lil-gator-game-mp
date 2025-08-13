using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020001DC RID: 476
public class Weapon : MonoBehaviour, IItemBehaviour
{
	// Token: 0x17000083 RID: 131
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

	// Token: 0x060009FB RID: 2555 RVA: 0x0002E63F File Offset: 0x0002C83F
	public void StartSwing()
	{
		this.onSwing.Invoke();
		this.trail.Clear();
		this.trail.emitting = true;
		this.triggers.SetActive(true);
	}

	// Token: 0x060009FC RID: 2556 RVA: 0x0002E66F File Offset: 0x0002C86F
	public void StopSwing()
	{
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

	// Token: 0x04000C49 RID: 3145
	public static float lastWeaponSwingTime = -1f;

	// Token: 0x04000C4A RID: 3146
	public static float lastWeaponHitTime = -1f;

	// Token: 0x04000C4B RID: 3147
	public string id;

	// Token: 0x04000C4C RID: 3148
	public TrailRenderer trail;

	// Token: 0x04000C4D RID: 3149
	public Collider hitbox;

	// Token: 0x04000C4E RID: 3150
	public GameObject triggers;

	// Token: 0x04000C4F RID: 3151
	private AudioSourceVariance audioVariance;

	// Token: 0x04000C50 RID: 3152
	private YieldInstruction waitForPointOne;

	// Token: 0x04000C51 RID: 3153
	private bool isSwinging;

	// Token: 0x04000C52 RID: 3154
	public UnityEvent onSwing;

	// Token: 0x04000C53 RID: 3155
	public UnityEvent onHit;

	// Token: 0x04000C54 RID: 3156
	public UnityEvent onEquip;

	// Token: 0x04000C55 RID: 3157
	public UnityEvent onUnequip;

	// Token: 0x04000C56 RID: 3158
	private IEnumerator useWeaponCoroutine;

	// Token: 0x04000C57 RID: 3159
	private static float lastHitPause = 0f;

	// Token: 0x04000C58 RID: 3160
	private const float minHitPauseDelay = 0.5f;

	// Token: 0x04000C59 RID: 3161
	private WaitForSeconds waitHitPause;
}
