using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string scenename;
    public Animator TransitionAnimator;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TransitionAnimator.SetTrigger("go");
            Invoke("quickDelay", 1.4f);
        }
    }

    void quickDelay()
    {
        SceneManager.LoadScene(scenename);
    }
}
