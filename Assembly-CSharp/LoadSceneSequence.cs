using System;
using System.Collections;
using Rewired;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

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

	private static Scene oldScene;

	private static bool isSceneAsset;

	private static int sceneBuildIndex;

	private static AssetReference sceneAssetReference;

	private static LoadSceneSequence.LoadType loadType;

	private static bool hasPreloadedScene;

	private static AsyncOperationHandle<SceneInstance> preloadedSceneHandle;

	private static AssetReference preloadedScene;

	public AssetReference mainMenuScene;

	public Animator animator;

	public bool isReadyToShowScene = true;

	public enum LoadType
	{
		LoadingScreen,
		Fade
	}
}
