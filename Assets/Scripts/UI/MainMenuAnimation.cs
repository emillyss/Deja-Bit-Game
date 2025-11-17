using UnityEngine;

public class MainMenuAnimation : MonoBehaviour
{
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        animator.enabled = true;
    }

}
