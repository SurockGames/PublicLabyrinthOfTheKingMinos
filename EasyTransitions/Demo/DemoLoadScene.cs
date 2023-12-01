using UnityEngine;
using UnityEngine.SceneManagement;

namespace EasyTransition
{

    public class DemoLoadScene : MonoBehaviour
    {
        public TransitionSettings transition;
        public float startDelay;

        
        public void LoadScene(string _sceneName)
        {
            TransitionManager.Instance().Transition(_sceneName, transition, startDelay);
        }

        public void LoadSceneByIndex(int index)
        {
            TransitionManager.Instance().Transition(SceneManager.GetSceneByBuildIndex(index).name, transition, startDelay);
        }

        public void LoadNextScene()
        {
            TransitionManager.Instance().Transition(SceneManager.GetActiveScene().buildIndex + 1, transition, startDelay);
        }
    }

}


