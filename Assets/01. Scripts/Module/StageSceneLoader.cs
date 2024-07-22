using System.Collections;
using UnityEngine;

namespace TeamJustFour.MoveOneStep.Module
{
    public class StageSceneLoader : Singleton<StageSceneLoader>
    {
        private const string SCENE_NAME = "StageScene";

        public void Load()
        {
            StartCoroutine(LoadSceneAsyncCoroutine());
        }

        public IEnumerator LoadSceneAsyncCoroutine()
        {
            AsyncOperation asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(SCENE_NAME);

            while (!asyncOperation.isDone)
            {
                Destroy(gameObject);

                yield return null;
            }
        }
    }
}
