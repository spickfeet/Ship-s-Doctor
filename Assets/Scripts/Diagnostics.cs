using System.Collections.Generic;
using UnityEngine;

public class Diagnostics : MonoBehaviour
{
    [SerializeField] private Stopwatch _stopwatch;

    [SerializeField] private float _breakOffset = 2.5f;
    [SerializeField] private float _waitingOffset = 5f;

    private List<Patient> _patients;

    private void Start()
    {
        _stopwatch.Run();

        _stopwatch.Elapsed += OnElapsed;
    }

    public void Inject(List<Patient> patients)
    {
        _patients = patients;
    }

    private void OnElapsed(int interval)
    {
        foreach (Patient patient in _patients)
        {
            if (patient.MaxWaitingTime - _waitingOffset >= patient.MinWaitingTime)
                patient.MaxWaitingTime -= _waitingOffset;
            if (patient.MaxBreakTime - _breakOffset >= patient.MinBreakTime)
                patient.MaxBreakTime -= _breakOffset;
        }
    }
}
