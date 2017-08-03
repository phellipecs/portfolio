using UnityEngine;
using System.Collections;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {

    public static T instance;

    protected void Awake()
    {
        if(instance == null)
        {
            instance = (T)FindObjectOfType<T>();
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }
}
