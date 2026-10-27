using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void PlayGame(){
        SceneManager.LoadScene("01_menu");
    }
}
