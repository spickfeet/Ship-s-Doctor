using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseMenu : MonoBehaviour
{
    [SerializeField] private string _titleText;
    [SerializeField] private Text _recordText;

    private Stopwatch _stopwatch;

    public void Inject(Stopwatch stopwatch)
    {
        _stopwatch = stopwatch;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnDead()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0.0f;
        _stopwatch.Stop();
        _recordText.text = _titleText + ((int)_stopwatch.Seconds).ToString();
    }

    private void OnValidate()
    {
        _recordText.text = _titleText;
    }
}
