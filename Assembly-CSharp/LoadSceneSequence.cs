using System;
using System.Collections;
using Rewired;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

// Token: 0x02000059 RID: 89
public class LoadSceneSequence : MonoBehaviour
{
	// Token: 0x06000147 RID: 327 RVA: 0x00007C90 File Offset: 0x00005E90
	public static void LoadScene(int buildIndex, LoadSceneSequence.LoadType loadType = LoadSceneSequence.LoadType.LoadingScreen)
	{
		LoadSceneSequence.isSceneAsset = false;
		LoadSceneSequence.sceneBuildIndex = buildIndex;
		LoadSceneSequence.LoadScene(loadType);
	}

	// Token: 0x06000148 RID: 328 RVA: 0x00007CA4 File Offset: 0x00005EA4
	public static void LoadScene(AssetReference sceneAsset, LoadSceneSequence.LoadType loadType = LoadSceneSequence.LoadType.LoadingScreen)
	{
		LoadSceneSequence.isSceneAsset = true;
		LoadSceneSequence.sceneAssetReference = sceneAsset;
		LoadSceneSequence.LoadScene(loadType);
	}

	// Token: 0x06000149 RID: 329 RVA: 0x00007CB8 File Offset: 0x00005EB8
	public static void StartPreloadScene(AssetReference sceneAsset)
	{
		LoadSceneSequence.hasPreloadedScene = true;
		LoadSceneSequence.preloadedSceneHandle = Addressables.LoadSceneAsync(sceneAsset, LoadSceneMode.Single, false, 100);
		LoadSceneSequence.preloadedScene = sceneAsset;
	}

	// Token: 0x0600014A RID: 330 RVA: 0x00007CD5 File Offset: 0x00005ED5
	private static void LoadScene(LoadSceneSequence.LoadType loadType)
	{
		LoadSceneSequence.loadType = loadType;
		LoadSceneSequence.oldScene = SceneManager.GetActiveScene();
		if (loadType == LoadSceneSequence.LoadType.Fade)
		{
			Object.Instantiate<GameObject>(Prefabs.p.loadingSequenceFade);
			return;
		}
		Object.Instantiate<GameObject>(Prefabs.p.loadingSequence);
	}

	// Token: 0x0600014B RID: 331 RVA: 0x00007D0C File Offset: 0x00005F0C
	private void Start()
	{
		base.StartCoroutine(this.LoadSceneAsync());
	}

	// Token: 0x0600014C RID: 332 RVA: 0x00007D1B File Offset: 0x00005F1B
	public IEnumerator LoadSceneAsync()
	{
		Object.DontDestroyOnLoad(base.gameObject);
		FadeGameVolume.FadeOutGameVolume();
		ReInput.players.GetPlayer(0).controllers.maps.SetAllMapsEnabled(false);
		yield return new WaitForSeconds(1f);
		SpeedrunData.isLoading = true;
		if (LoadSceneSequence.hasPreloadedScene)
		{
			if (LoadSceneSequence.isSceneAsset && LoadSceneSequence.preloadedScene.SubObjectName == LoadSceneSequence.sceneAssetReference.SubObjectName)
			{
				Debug.Log("Using preloaded scene");
			}
			else
			{
				yield return LoadSceneSequence.preloadedSceneHandle;
				yield return LoadSceneSequence.preloadedSceneHandle.Result.ActivateAsync();
				LoadSceneSequence.hasPreloadedScene = false;
			}
		}
		if (LoadSceneSequence.isSceneAsset)
		{
			AsyncOperationHandle<SceneInstance> handle;
			if (LoadSceneSequence.hasPreloadedScene)
			{
				handle = LoadSceneSequence.preloadedSceneHandle;
			}
			else
			{
				handle = Addressables.LoadSceneAsync(LoadSceneSequence.sceneAssetReference, LoadSceneMode.Single, false, 100);
			}
			yield return handle;
			yield return handle.Result.ActivateAsync();
			handle = default(AsyncOperationHandle<SceneInstance>);
		}
		else
		{
			AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(LoadSceneSequence.sceneBuildIndex, LoadSceneMode.Single);
			yield return asyncOperation;
		}
		SpeedrunData.isLoading = false;
		LoadSceneSequence.hasPreloadedScene = false;
		if (this.animator != null)
		{
			this.animator.SetTrigger("LoadFinished");
		}
		FadeGameVolume.FadeInGameVolume();
		yield return new WaitForSeconds(0.5f);
		ReInput.players.GetPlayer(0).controllers.maps.SetAllMapsEnabled(true);
		Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x040001CB RID: 459
	private static Scene oldScene;

	// Token: 0x040001CC RID: 460
	private static bool isSceneAsset;

	// Token: 0x040001CD RID: 461
	private static int sceneBuildIndex;

	// Token: 0x040001CE RID: 462
	private static AssetReference sceneAssetReference;

	// Token: 0x040001CF RID: 463
	private static LoadSceneSequence.LoadType loadType;

	// Token: 0x040001D0 RID: 464
	private static bool hasPreloadedScene;

	// Token: 0x040001D1 RID: 465
	private static AsyncOperationHandle<SceneInstance> preloadedSceneHandle;

	// Token: 0x040001D2 RID: 466
	private static AssetReference preloadedScene;

	// Token: 0x040001D3 RID: 467
	public AssetReference mainMenuScene;

	// Token: 0x040001D4 RID: 468
	public Animator animator;

	// Token: 0x040001D5 RID: 469
	public bool isReadyToShowScene = true;

	// Token: 0x02000369 RID: 873
	public enum LoadType
	{
		// Token: 0x04001A3A RID: 6714
		LoadingScreen,
		// Token: 0x04001A3B RID: 6715
		Fade
	}
}
