using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempSceneChange : MonoBehaviour
{
    public string scenename;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(scenename);
        }
    }
}
