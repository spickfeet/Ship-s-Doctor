using System.Linq;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GlobalUIView _globalUIView;

    [Header("Audio")]
    [SerializeField] private AudioLibrary _audioLibrary;
    [SerializeField] private AudioSource _musicSource;

    [Header("Utilities")]
    [SerializeField] private Diagnostics _diagnostics;
    [SerializeField] private LoseMenu _loseMenu;
    [SerializeField] private Stopwatch _stopwatch;

    private Patient[] _patients;
    private BarrelSupplies[] _barrelSupplies;
    private HoldLadder[] _holdLadders;

    private void Awake()
    {
        Inventory inventory = new Inventory();

        _patients = FindObjectsByType<Patient>(FindObjectsSortMode.None);
        _barrelSupplies = FindObjectsByType<BarrelSupplies>(FindObjectsSortMode.None);
        _holdLadders = FindObjectsByType<HoldLadder>(FindObjectsSortMode.None);

        _globalUIView.Inject(inventory, _patients);
        _player.Inject(inventory);
        _loseMenu.Inject(_stopwatch);

        foreach (var holdLadder in _holdLadders)
        {
            holdLadder.Inject(_player);
        }

        foreach (var patient in _patients)
        {
            patient.Inject(_player, _audioLibrary);
            patient.Dead += _loseMenu.OnDead;
        }

        foreach (var barrelSupplies in _barrelSupplies)
        {
            barrelSupplies.Inject(_player);
        }

        _diagnostics.Inject(_patients.ToList());
    }

    private void Start()
    {
        Time.timeScale = 1f;

        PlayGlobalMusic(AudioLibrary.Names.Sea, 0.5f);
        PlayGlobalMusic(AudioLibrary.Names.Seagull, 1f);
    }

    public void PlayGlobalMusic(string musicName, float volume)
    {
        AudioSource audioSource = Instantiate(_musicSource);
        audioSource.clip = _audioLibrary.GetAudio(musicName);
        audioSource.volume = volume;
        audioSource.Play();
    }
}
