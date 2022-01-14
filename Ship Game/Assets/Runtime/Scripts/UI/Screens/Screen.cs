using UnityEngine;

public abstract class Screen : MonoBehaviour
{
    public virtual void EnableScreen() => gameObject.SetActive(true);
    public virtual void DisableScreen() => gameObject.SetActive(false);
}