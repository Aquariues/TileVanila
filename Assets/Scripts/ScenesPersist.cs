using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenesPersist : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        int numSensesPersists = FindObjectsOfType<ScenesPersist>().Length;
        if (numSensesPersists > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public void ResetScenesPersist()
    {
        Destroy(gameObject);
    }
}
