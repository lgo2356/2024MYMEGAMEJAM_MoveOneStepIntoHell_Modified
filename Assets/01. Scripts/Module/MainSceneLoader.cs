using System.Collections;
using UnityEngine;

namespace TeamJustFour.MoveOneStep.Module
{
    public class MainSceneLoader : Singleton<MainSceneLoader>
    {
        private const string SCENE_NAME = "MainScene";

        public void Load()
        {
            StartCoroutine(LoadSceneAsyncCoroutine());

            //Addressables.InstantiateAsync(AssetPath.LOADING_SCREEN_PATH).Completed += (obj) =>
            //{
            //    UI_LoadingScreen loadingScreen = obj.Result.GetComponent<UI_LoadingScreen>();
            //    loadingScreen.SetLoadingImage();

            //    StartCoroutine(LoadSceneAsyncCoroutine());
            //};
        }

        public IEnumerator LoadSceneAsyncCoroutine()
        {
            AsyncOperation asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(SCENE_NAME);

            while (!asyncOperation.isDone)
            {
                yield return null;
            }
        }
    }
}
