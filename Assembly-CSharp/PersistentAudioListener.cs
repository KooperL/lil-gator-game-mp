using System;
using UnityEngine;

public class PersistentAudioListener : MonoBehaviour
{
	// Token: 0x06000824 RID: 2084 RVA: 0x000270A2 File Offset: 0x000252A2
	private void Start()
	{
		if (PersistentAudioListener.persistentInstance == null)
		{
			PersistentAudioListener.persistentInstance = this;
			this.audioListener.enabled = true;
			Object.DontDestroyOnLoad(base.gameObject);
			return;
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06000825 RID: 2085 RVA: 0x000270DA File Offset: 0x000252DA
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
