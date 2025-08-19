using System;
using UnityEngine;

public class PersistentAudioListener : MonoBehaviour
{
	// Token: 0x060009DA RID: 2522 RVA: 0x00009799 File Offset: 0x00007999
	private void Start()
	{
		if (PersistentAudioListener.persistentInstance == null)
		{
			PersistentAudioListener.persistentInstance = this;
			this.audioListener.enabled = true;
			global::UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			return;
		}
		global::UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060009DB RID: 2523 RVA: 0x000097D1 File Offset: 0x000079D1
	private void LateUpdate()
	{
		if (this.parent == null)
		{
			this.parent = MainCamera.t;
		}
		if (this.parent != null)
		{
			base.transform.position = this.parent.position;
		}
	}

	public static PersistentAudioListener persistentInstance;

	private Transform parent;

	public AudioListener audioListener;
}
