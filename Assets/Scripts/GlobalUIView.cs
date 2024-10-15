using UnityEngine;
using UnityEngine.UI;

public class GlobalUIView : MonoBehaviour
{
    [SerializeField] private Text _orangeCount;
    [SerializeField] private Text _bandageCount;
    [SerializeField] private Text _painkillerCount;

    [SerializeField] private GameObject[] _pirateCaptainNecessity;
    [SerializeField] private GameObject[] _pirateCleanerNecessity;
    [SerializeField] private GameObject[] _pirateDefaultNecessity;

    [SerializeField] private Text[] _pirateCaptainNecessityCount;
    [SerializeField] private Text[] _pirateCleanerNecessityCount;
    [SerializeField] private Text[] _pirateDefaultNecessityCount;

    private Inventory _inventory;
    private Patient[] _patients;



    [SerializeField] private Image[] _waitingTimeCurrent;

    private void Update()
    {
        for (int i = 0; i < 3; i++)
        {
            _waitingTimeCurrent[(int)_patients[i].Role].fillAmount = _patients[i].CurrentWaitingTime / _patients[i].WaitingTime;
        }
    }


    public void Inject(Inventory inventory, Patient[] patients)
    {
        _inventory = inventory;
        _patients = patients;
        for (int i = 0; i < _patients.Length; i++)
        {
            _patients[i].NeedItemsChanged += ChangeNecessity;
        }
        _inventory.ItemCountChanged += ChangeItemCount;
    }

    public void ChangeItemCount(Item item)
    {
        switch (item.Type)
        {
            case ItemType.Bandage:
                _bandageCount.text = item.Count.ToString();
                break;
            case ItemType.Oranges:
                _orangeCount.text = item.Count.ToString();
                break;
            case ItemType.Painkillers:
                _painkillerCount.text = item.Count.ToString();
                break;
        }
    }

    public void ChangeNecessity(Patient patient)
    {
        switch (patient.Role)
        {
            case Role.Captain:
                foreach (Item needItem in patient.NeedItems)
                {
                    switch (needItem.Type)
                    {
                        case ItemType.Bandage:
                            if (needItem.Count == 0)
                            {
                                _pirateCaptainNecessity[0].SetActive(false);
                                continue;
                            }
                            _pirateCaptainNecessity[0].SetActive(true);
                            _pirateCaptainNecessityCount[0].text = needItem.Count.ToString();
                            break;
                        case ItemType.Oranges:
                            if (needItem.Count == 0)
                            {
                                _pirateCaptainNecessity[1].SetActive(false);
                                continue;
                            }
                            _pirateCaptainNecessity[1].SetActive(true);
                            _pirateCaptainNecessityCount[1].text = needItem.Count.ToString();
                            break;
                        case ItemType.Painkillers:
                            if (needItem.Count == 0)
                            {
                                _pirateCaptainNecessity[2].SetActive(false);
                                continue;
                            }
                            _pirateCaptainNecessity[2].SetActive(true);
                            _pirateCaptainNecessityCount[2].text = needItem.Count.ToString();
                            break;
                    }
                }
                break;
            case Role.Cleaner:
                foreach (Item needItem in patient.NeedItems)
                {
                    switch (needItem.Type)
                    {
                        case ItemType.Bandage:
                            if (needItem.Count == 0)
                            {
                                _pirateCleanerNecessity[0].SetActive(false);
                                continue;
                            }
                            _pirateCleanerNecessity[0].SetActive(true);
                            _pirateCleanerNecessityCount[0].text = needItem.Count.ToString();
                            break;
                        case ItemType.Oranges:
                            if (needItem.Count == 0)
                            {
                                _pirateCleanerNecessity[1].SetActive(false);
                                continue;
                            }
                            _pirateCleanerNecessity[1].SetActive(true);
                            _pirateCleanerNecessityCount[1].text = needItem.Count.ToString();
                            break;
                        case ItemType.Painkillers:
                            if (needItem.Count == 0)
                            {
                                _pirateCleanerNecessity[2].SetActive(false);
                                continue;
                            }
                            _pirateCleanerNecessity[2].SetActive(true);
                            _pirateCleanerNecessityCount[2].text = needItem.Count.ToString();
                            break;
                    }
                }
                break;

            case Role.Default:
                foreach (Item needItem in patient.NeedItems)
                {
                    switch (needItem.Type)
                    {
                        case ItemType.Bandage:
                            if (needItem.Count == 0)
                            {
                                _pirateDefaultNecessity[0].SetActive(false);
                                continue;
                            }
                            _pirateDefaultNecessity[0].SetActive(true);
                            _pirateDefaultNecessityCount[0].text = needItem.Count.ToString();
                            break;
                        case ItemType.Oranges:
                            if (needItem.Count == 0)
                            {
                                _pirateDefaultNecessity[1].SetActive(false);
                                continue;
                            }
                            _pirateDefaultNecessity[1].SetActive(true);
                            _pirateDefaultNecessityCount[1].text = needItem.Count.ToString();
                            break;
                        case ItemType.Painkillers:
                            if (needItem.Count == 0)
                            {
                                _pirateDefaultNecessity[2].SetActive(false);
                                continue;
                            }
                            _pirateDefaultNecessity[2].SetActive(true);
                            _pirateDefaultNecessityCount[2].text = needItem.Count.ToString();
                            break;
                    }
                }
                break;

        }
    }
}
