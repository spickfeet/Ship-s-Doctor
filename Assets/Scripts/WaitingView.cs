using UnityEngine;
using UnityEngine.UI;

public class WaitingView : MonoBehaviour
{
    [SerializeField] private Image _waitingTimeCurrent;
    [SerializeField] private Patient _patient;
    [SerializeField] private GameObject _waitingTimeBar;

    private void Update()
    {
        if (_patient.CurrentState == State.Waiting)
        {
            _waitingTimeBar.SetActive(true);
            _waitingTimeCurrent.fillAmount = _patient.CurrentWaitingTime / _patient.WaitingTime;
        }
        else
        {
            _waitingTimeBar.SetActive(false);
        }
    }
}
