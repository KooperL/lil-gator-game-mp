using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLauncher : MonoBehaviour
{
	// Token: 0x0600016C RID: 364 RVA: 0x000033D3 File Offset: 0x000015D3
	private void Start()
	{
		global::UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		base.StartCoroutine(this.RunLoadLauncher());
	}

	// Token: 0x0600016D RID: 365 RVA: 0x000033ED File Offset: 0x000015ED
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
		global::UnityEngine.Object.Destroy(base.gameObject);
		yield break;
	}

	public AssetReference sceneToLoad;

	public Image backgroundImage;

	public GameObject loadingIcon;
}
