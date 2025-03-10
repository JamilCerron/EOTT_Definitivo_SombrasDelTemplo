using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ChangeSceneButton : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private AudioSource fxSource;
    [SerializeField] private AudioClip clickSound;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(ChangeScene);
    }

    private void ChangeScene()
    {
        PlaySoundButton();
        SceneManager.LoadScene(sceneName);
    }

    private void PlaySoundButton()
    {
        fxSource.PlayOneShot(clickSound);
    }
}