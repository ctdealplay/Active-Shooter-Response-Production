using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{

    // Static function to execute a method with a delay
    public static void ExecuteAfterDelay(float delay, System.Action action)
    {
        // Start the coroutine using a MonoBehaviour instance
        CoroutineRunner.Instance.StartCoroutine(ExecuteAfterDelayCoroutine(delay, action));
    }

    // Coroutine to handle the delay
    private static IEnumerator ExecuteAfterDelayCoroutine(float delay, System.Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }

    // Internal MonoBehaviour class to run coroutines
    private class CoroutineRunner : MonoBehaviour
    {
        private static CoroutineRunner _instance;

        public static CoroutineRunner Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject obj = new GameObject("CoroutineRunner");
                    _instance = obj.AddComponent<CoroutineRunner>();
                    Object.DontDestroyOnLoad(obj); // Optional: Keeps the object alive between scenes
                }
                return _instance;
            }
        }
    }
}
