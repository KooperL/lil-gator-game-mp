using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverriddenMusic : MonoBehaviour
{
	// Token: 0x060000B8 RID: 184 RVA: 0x0001A0B4 File Offset: 0x000182B4
	private void OnEnable()
	{
		OverriddenMusic.isOverridden = true;
		this.timer = 0f;
		base.transform.parent = null;
		global::UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		this.nativeScene = SceneManager.GetActiveScene();
		SceneManager.sceneUnloaded += this.OnSceneUnload;
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x00002A30 File Offset: 0x00000C30
	private void OnDisable()
	{
		OverriddenMusic.isOverridden = false;
	}

	// Token: 0x060000BA RID: 186 RVA: 0x00002A38 File Offset: 0x00000C38
	private void OnSceneUnload(Scene scene)
	{
		if (scene == this.nativeScene)
		{
			this.isUnloading = true;
			if (!base.gameObject.activeSelf)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	// Token: 0x060000BB RID: 187 RVA: 0x00002A67 File Offset: 0x00000C67
	public void StopMusic()
	{
		this.isStopped = true;
	}

	// Token: 0x060000BC RID: 188 RVA: 0x0001A108 File Offset: 0x00018308
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
			global::UnityEngine.Object.Destroy(base.gameObject);
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
