using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverriddenMusic : MonoBehaviour
{
	// Token: 0x060000A4 RID: 164 RVA: 0x00005178 File Offset: 0x00003378
	private void OnEnable()
	{
		OverriddenMusic.isOverridden = true;
		this.timer = 0f;
		base.transform.parent = null;
		Object.DontDestroyOnLoad(base.gameObject);
		this.nativeScene = SceneManager.GetActiveScene();
		SceneManager.sceneUnloaded += this.OnSceneUnload;
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x000051C9 File Offset: 0x000033C9
	private void OnDisable()
	{
		OverriddenMusic.isOverridden = false;
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x000051D1 File Offset: 0x000033D1
	private void OnSceneUnload(Scene scene)
	{
		if (scene == this.nativeScene)
		{
			this.isUnloading = true;
			if (!base.gameObject.activeSelf)
			{
				Object.Destroy(base.gameObject);
			}
		}
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x00005200 File Offset: 0x00003400
	public void StopMusic()
	{
		this.isStopped = true;
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x0000520C File Offset: 0x0000340C
	private void Update()
	{
		if (this.isUnloading || this.isStopped)
		{
			this.audioSource.volume = Mathf.MoveTowards(this.audioSource.volume, 0f, Time.unscaledDeltaTime * this.fadeSpeed);
			if (this.audioSource.volume == 0f)
			{
				this.audioSource.Stop();
			}
		}
		if (!this.audioSource.isPlaying)
		{
			this.timer += Time.unscaledDeltaTime;
		}
		if (this.timer > this.endDelay)
		{
			if (!this.isUnloading)
			{
				SceneManager.MoveGameObjectToScene(base.gameObject, SceneManager.GetActiveScene());
				SceneManager.sceneUnloaded -= this.OnSceneUnload;
				base.gameObject.SetActive(false);
				return;
			}
			Object.Destroy(base.gameObject);
		}
	}

	public static bool isOverridden;

	public AudioSource audioSource;

	private float timer = -1f;

	public float endDelay = 3f;

	public float fadeSpeed = 0.3f;

	private bool isUnloading;

	private Scene nativeScene;

	private bool isStopped;
}
