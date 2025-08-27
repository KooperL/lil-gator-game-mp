using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour, IItemBehaviour
{
	// (get) Token: 0x06000BEB RID: 3051
	private PlayerItemManager itemManager
	{
		get
		{
			return Player.itemManager;
		}
	}

	private void Awake()
	{
		this.audioVariance = base.GetComponent<AudioSourceVariance>();
		this.waitForPointOne = new WaitForSeconds(0.1f);
	}

	public void Cancel()
	{
		this.isSwinging = false;
		this.itemManager.SetItemInUse(this, false);
	}

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

	public void OnRemove()
	{
	}

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

	public bool isSwinging;

	public UnityEvent onSwing;

	public UnityEvent onHit;

	public UnityEvent onEquip;

	public UnityEvent onUnequip;

	private IEnumerator useWeaponCoroutine;

	private static float lastHitPause = 0f;

	private const float minHitPauseDelay = 0.5f;

	private WaitForSeconds waitHitPause;
}
