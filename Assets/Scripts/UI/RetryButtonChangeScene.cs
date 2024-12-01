using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RetryButtonChangeScene : MonoBehaviour
{
    public static string SCENE_NAME = string.Empty;
    [SerializeField] private AudioSource fxSource;
    [SerializeField] private AudioClip clickSound;

    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(ChangeScene);
    }

    void ChangeScene()
    {
        PlaySoundButton();
        if (SCENE_NAME == string.Empty)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SCENE_NAME);
        }
    }

    private void PlaySoundButton()
    {
        fxSource.PlayOneShot(clickSound);
    }
}