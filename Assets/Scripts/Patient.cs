using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Patient : Interactable
{

    [SerializeField] private float _minBreakTime = 5f;
    [SerializeField] private float _maxBreakTime = 30f;

    [SerializeField] private float _minWaitingTime = 10;
    [SerializeField] private float _maxWaitingTime = 45f;
    [SerializeField] private Role _role;

    [SerializeField] private int _maxNeededItems = 5;

    [SerializeField][Range(0, 0.99f)] private float _chanceSound = 0.4f;
    [SerializeField] private float _soundDelay = 5f;

    private float _soundDelayTime = 0f;

    private AudioSource _audioSource;

    private Necessity _necessity;
    private List<Item> _needItems;

    private float _breakTime = 0;
    private float _currentWaitingTime = 0;

    private State _state;

    private AudioLibrary _audioLibrary;

    public float WaitingTime { get; set; }

    public List<Item> NeedItems
    {
        get
        {
            return _needItems;
        }
        set
        {
            _needItems = value;
        }
    }

    public Role Role
    {
        get
        {
            return _role;
        }
        set
        {
            _role = value;
        }
    }

    public float CurrentWaitingTime
    {
        get
        {
            return _currentWaitingTime;
        }
        set
        {
            _currentWaitingTime = value;
        }
    }

    public float MaxBreakTime
    {
        get
        {
            return _maxBreakTime;
        }
        set
        {
            _maxBreakTime = value;
        }
    }

    public float MinBreakTime
    {
        get
        {
            return _minBreakTime;
        }
    }

    public float MaxWaitingTime
    {
        get
        {
            return _maxWaitingTime;
        }
        set
        {
            _maxWaitingTime = value;
        }
    }

    public float MinWaitingTime
    {
        get
        {
            return _minWaitingTime;
        }
    }

    public State CurrentState
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;
        }
    }

    private float GetRandomWaitingTime => UnityEngine.Random.Range(_minWaitingTime, _maxWaitingTime);

    public Action<Patient> NeedItemsChanged;

    public Action Dead;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        _necessity = new Necessity(_maxNeededItems);
    }

    private void Start()
    {
        _breakTime = UnityEngine.Random.Range(1, _minWaitingTime);
        _state = State.Breaking;

    }

    public void Inject(Player player, AudioLibrary audioLibrary)
    {
        _audioLibrary = audioLibrary;
        _player = player;
    }

    public override void Interact()
    {
        if (_state == State.Breaking || _state == State.Dead) return;

        foreach (Item item in NeedItems)
        {
            int count = item.Count;
            for (int i = 0; i < item.Count; i++)
            {
                if (_player.Inventory.TryRemoveItem(item.Type))
                {
                    count--;
                }
            }

            item.Count = count;
        }

        List<Item> buff = new List<Item>(NeedItems);

        int temp = 0;
        foreach (Item item in NeedItems)
        {
            temp += item.Count;
        }
        if (temp == 0)
        {
            _breakTime = _maxBreakTime;
            _state = State.Breaking;
            NeedItemsChanged?.Invoke(this);
            NeedItems.Clear();

            _audioSource.clip = _audioLibrary.GetAudio(AudioLibrary.Names.Spit3);
            _audioSource.Play();
            return;
        }
        NeedItemsChanged?.Invoke(this);
    }

    private void RequestItems()
    {
        NeedItems = _necessity.GetNecessities();
        NeedItemsChanged?.Invoke(this);

        WaitingTime = GetRandomWaitingTime;
        CurrentWaitingTime = WaitingTime;

        _state = State.Waiting;

        _audioSource.clip = _audioLibrary.GetAudio(AudioLibrary.Names.Cough);
    }

    public void Update()
    {
        switch (_state)
        {
            case State.Waiting:
                if (CurrentWaitingTime >= 0)
                {
                    CurrentWaitingTime -= Time.deltaTime;
                }
                else
                {
                    CurrentWaitingTime = 0;
                    _state = State.Dead;
                    _audioSource.Stop();
                    Dead?.Invoke();
                }

                if (_soundDelayTime >= 0)
                {
                    _soundDelayTime -= Time.deltaTime;
                }
                else
                {
                    if (UnityEngine.Random.Range(0f, 1f) <= _chanceSound)
                    {
                        _audioSource.pitch = UnityEngine.Random.Range(1, 1.2f);
                        _audioSource.Play();
                    }

                    _soundDelayTime = _soundDelay;
                }

                break;
            case State.Breaking:
                if (_breakTime > 0)
                {
                    CurrentWaitingTime = WaitingTime;
                    _breakTime -= Time.deltaTime;
                }
                else
                {
                    RequestItems();
                }
                break;
            default:
                return;
        }
    }

}
