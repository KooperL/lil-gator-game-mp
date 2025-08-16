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
	// Token: 0x0600017A RID: 378 RVA: 0x0000344B File Offset: 0x0000164B
	public static void LoadScene(int buildIndex, LoadSceneSequence.LoadType loadType = LoadSceneSequence.LoadType.LoadingScreen)
	{
		LoadSceneSequence.isSceneAsset = false;
		LoadSceneSequence.sceneBuildIndex = buildIndex;
		LoadSceneSequence.LoadScene(loadType);
	}

	// Token: 0x0600017B RID: 379 RVA: 0x0000345F File Offset: 0x0000165F
	public static void LoadScene(AssetReference sceneAsset, LoadSceneSequence.LoadType loadType = LoadSceneSequence.LoadType.LoadingScreen)
	{
		LoadSceneSequence.isSceneAsset = true;
		LoadSceneSequence.sceneAssetReference = sceneAsset;
		LoadSceneSequence.LoadScene(loadType);
	}

	// Token: 0x0600017C RID: 380 RVA: 0x00003473 File Offset: 0x00001673
	public static void StartPreloadScene(AssetReference sceneAsset)
	{
		LoadSceneSequence.hasPreloadedScene = true;
		LoadSceneSequence.preloadedSceneHandle = Addressables.LoadSceneAsync(sceneAsset, LoadSceneMode.Single, false, 100);
		LoadSceneSequence.preloadedScene = sceneAsset;
	}

	// Token: 0x0600017D RID: 381 RVA: 0x00003490 File Offset: 0x00001690
	private static void LoadScene(LoadSceneSequence.LoadType loadType)
	{
		LoadSceneSequence.loadType = loadType;
		LoadSceneSequence.oldScene = SceneManager.GetActiveScene();
		if (loadType == LoadSceneSequence.LoadType.Fade)
		{
			global::UnityEngine.Object.Instantiate<GameObject>(Prefabs.p.loadingSequenceFade);
			return;
		}
		global::UnityEngine.Object.Instantiate<GameObject>(Prefabs.p.loadingSequence);
	}

	// Token: 0x0600017E RID: 382 RVA: 0x000034C7 File Offset: 0x000016C7
	private void Start()
	{
		base.StartCoroutine(this.LoadSceneAsync());
	}

	// Token: 0x0600017F RID: 383 RVA: 0x000034D6 File Offset: 0x000016D6
	public IEnumerator LoadSceneAsync()
	{
		global::UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
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
		global::UnityEngine.Object.Destroy(base.gameObject);
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
