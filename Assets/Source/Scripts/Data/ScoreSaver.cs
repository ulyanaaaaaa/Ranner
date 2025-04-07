using System.Collections.Generic;
using System.IO;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreSaver : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private List<PlayerScore> _playerScores = new List<PlayerScore>();
    [SerializeField] private TextMeshProUGUI _notificationText;
    private string _filePath;

    public List<PlayerScore> PlayerScores => _playerScores;
    
    private void Awake()
    {
        _filePath = Path.Combine(Application.persistentDataPath, "playerScores.json");
        LoadScores();
    }

    public void SaveScore(CointCountFail count)
    {
        string playerName = _inputField.text.Trim();
        
        if (string.IsNullOrEmpty(playerName))
        {
            ShowNotification("Empty!", Color.red);
            return;
        }
        
        bool nameExists = false;

        for (int i = 0; i < _playerScores.Count; i++)
        {
            if (_playerScores[i].playerName.Equals(playerName, System.StringComparison.OrdinalIgnoreCase))
            {
                _playerScores[i].coins = count.Score;
                nameExists = true;
                break;
            }
        }

        if (!nameExists)
        {
            _playerScores.Add(new PlayerScore(playerName, count.Score));
        }

        SaveScoresInFile();
        TextAnimation();
        ShowNotification(nameExists ? "Record update!" : "Record save!", Color.green);
    }

    private void TextAnimation()
    {
        _notificationText.gameObject.SetActive(true);
        _notificationText.transform.localPosition = new Vector3(0, 100, 0);
        _notificationText.transform.DOLocalMoveY(-250, 0.6f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            _notificationText.transform.DOLocalMoveY(100, 0.5f).SetDelay(1f).OnComplete(() =>
            {
                _notificationText.gameObject.SetActive(false);
            });
        });
    }

    private void ShowNotification(string message, Color color)
    {
        _notificationText.text = message;
        _notificationText.color = color;
        _notificationText.gameObject.SetActive(true);
        TextAnimation();
    }
    
    private void SaveScoresInFile()
    {
        string json = JsonUtility.ToJson(new PlayerScoreList { scores = _playerScores });
        File.WriteAllText(_filePath, json);
    }

    private void LoadScores()
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            PlayerScoreList playerScoreList = JsonUtility.FromJson<PlayerScoreList>(json);
            _playerScores = playerScoreList.scores;
        }
    }
}

[System.Serializable]
public class PlayerScoreList
{
    public List<PlayerScore> scores;
}