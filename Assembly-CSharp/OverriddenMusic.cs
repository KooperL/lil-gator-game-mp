using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

// Token: 0x02000033 RID: 51
public class OverriddenMusic : MonoBehaviour
{
	// Token: 0x060000B0 RID: 176 RVA: 0x000199D0 File Offset: 0x00017BD0
	private void OnEnable()
	{
		OverriddenMusic.isOverridden = true;
		this.timer = 0f;
		base.transform.parent = null;
		Object.DontDestroyOnLoad(base.gameObject);
		this.nativeScene = SceneManager.GetActiveScene();
		SceneManager.sceneUnloaded += new UnityAction<Scene>(this.OnSceneUnload);
	}

	// Token: 0x060000B1 RID: 177 RVA: 0x000029CC File Offset: 0x00000BCC
	private void OnDisable()
	{
		OverriddenMusic.isOverridden = false;
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x000029D4 File Offset: 0x00000BD4
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

	// Token: 0x060000B3 RID: 179 RVA: 0x00002A03 File Offset: 0x00000C03
	public void StopMusic()
	{
		this.isStopped = true;
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x00019A24 File Offset: 0x00017C24
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
				SceneManager.sceneUnloaded -= new UnityAction<Scene>(this.OnSceneUnload);
				base.gameObject.SetActive(false);
				return;
			}
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04000100 RID: 256
	public static bool isOverridden;

	// Token: 0x04000101 RID: 257
	public AudioSource audioSource;

	// Token: 0x04000102 RID: 258
	private float timer = -1f;

	// Token: 0x04000103 RID: 259
	public float endDelay = 3f;

	// Token: 0x04000104 RID: 260
	public float fadeSpeed = 0.3f;

	// Token: 0x04000105 RID: 261
	private bool isUnloading;

	// Token: 0x04000106 RID: 262
	private Scene nativeScene;

	// Token: 0x04000107 RID: 263
	private bool isStopped;
}
