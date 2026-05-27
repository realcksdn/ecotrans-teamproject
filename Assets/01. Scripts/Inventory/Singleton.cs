using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : SingleTone<T>
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<T>();
                }
            }
            return instance;
        }
    }
}
// ΩÃ±€≈Ê¿Ã æ»¥Ô