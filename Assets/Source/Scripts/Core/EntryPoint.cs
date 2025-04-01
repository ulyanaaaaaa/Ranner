using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private GameObject failWindow;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private CoinsCount coinsCount;

    private PlayerTrigger _playerPrefab;
    private RoadSpawner _roadSpawnerPrefab;
    private RoadSpawner _roadSpawner;
    private PlayerTrigger _player;

    private void OnEnable()
    {
        mainMenu.GetComponentInChildren<StartButton>().OnStart += StartGame;
        failWindow.GetComponentInChildren<StartButton>().OnStart += StartGame;
        failWindow.GetComponentInChildren<MenuButton>().OnClickButton += () => OpenMeinMenu();
    }

    private void Start()
    {
        mainMenu.gameObject.SetActive(true);
        failWindow.gameObject.SetActive(false);
    }

    private void StartGame()
    {
        InitializeGame();
    }

    private void OpenMeinMenu()
    {
        failWindow.SetActive(false);
        mainMenu.SetActive(true);
    }

    private void InitializeGame()
    {
        ClearPreviousRoads();

        mainMenu.SetActive(false);
        failWindow.SetActive(false);
        coinsCount.gameObject.SetActive(true);

        _playerPrefab = Resources.Load<PlayerTrigger>("Player");
        _roadSpawnerPrefab = Resources.Load<RoadSpawner>("RoadSpawner");

        _roadSpawner = Instantiate(_roadSpawnerPrefab);
        _player = Instantiate(_playerPrefab).Setup(_roadSpawner);
        
        coinsCount.Setup(_player.GetComponent<PlayerWallet>());
        _player.OnDie += StopGame;
    }

    private void ClearPreviousRoads()
    {
        Road[] existingRoads = FindObjectsOfType<Road>();
        foreach (var road in existingRoads)
        {
            Destroy(road.gameObject);
        }
    }

    private void StopGame()
    {
        failWindow.SetActive(true);
        failWindow.GetComponentInChildren<CointCountFail>().
            DisplayCount(_player.GetComponent<PlayerWallet>().CurrentCoins);
        coinsCount.gameObject.SetActive(false);
        
        if (_player != null)
        {
            _player.OnDie -= StopGame;
            Destroy(_player.gameObject);
        }
        
        if (_roadSpawner != null)
            Destroy(_roadSpawner.gameObject);
    }
}