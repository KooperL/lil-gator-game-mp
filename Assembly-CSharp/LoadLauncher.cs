using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.UI;

// Token: 0x0200006F RID: 111
public class LoadLauncher : MonoBehaviour
{
	// Token: 0x06000164 RID: 356 RVA: 0x00003330 File Offset: 0x00001530
	private void Start()
	{
		Object.DontDestroyOnLoad(base.gameObject);
		base.StartCoroutine(this.RunLoadLauncher());
	}

	// Token: 0x06000165 RID: 357 RVA: 0x0000334A File Offset: 0x0000154A
	private IEnumerator RunLoadLauncher()
	{
		AsyncOperationHandle<SceneInstance> asyncOperationHandle = Addressables.LoadSceneAsync(this.sceneToLoad, 0, true, 100);
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

	// Token: 0x04000222 RID: 546
	public AssetReference sceneToLoad;

	// Token: 0x04000223 RID: 547
	public Image backgroundImage;

	// Token: 0x04000224 RID: 548
	public GameObject loadingIcon;
}
