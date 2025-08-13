using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000056 RID: 86
public class LoadLauncher : MonoBehaviour
{
	// Token: 0x0600013F RID: 319 RVA: 0x00007B99 File Offset: 0x00005D99
	private void Start()
	{
		Object.DontDestroyOnLoad(base.gameObject);
		base.StartCoroutine(this.RunLoadLauncher());
	}

	// Token: 0x06000140 RID: 320 RVA: 0x00007BB3 File Offset: 0x00005DB3
	private IEnumerator RunLoadLauncher()
	{
		AsyncOperationHandle<SceneInstance> asyncOperationHandle = Addressables.LoadSceneAsync(this.sceneToLoad, LoadSceneMode.Single, true, 100);
		yield return asyncOperationHandle;
		this.loadingIcon.SetActive(false);
		float num = 1f;
		Color color = this.backgroundImage.color;
		while (num > 0f)
		{
			num -= Time.deltaTime * 2f;
			color.a = num;
			this.backgroundImage.color = color;
		}
		Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x040001BE RID: 446
	public AssetReference sceneToLoad;

	// Token: 0x040001BF RID: 447
	public Image backgroundImage;

	// Token: 0x040001C0 RID: 448
	public GameObject loadingIcon;
}
