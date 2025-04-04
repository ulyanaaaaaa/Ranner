using System.Linq;
using UnityEngine;

public class ScorePanel : MonoBehaviour
{
    private PlayerScorePanel _playerScorePanelPrefab; 
    [SerializeField] private Transform _content; 
    [SerializeField] private ScoreSaver _scoreSaver;

    private void Awake()
    {
        _playerScorePanelPrefab = Resources.Load<PlayerScorePanel>("PlayerCard");
    }

    private void OnEnable()
    {
        ReloadTable();
    }

    private void ReloadTable()
    {
        foreach (Transform child in _content)
        {
            Destroy(child.gameObject);
        }
        
        var sortedScores = _scoreSaver.PlayerScores
            .OrderByDescending(score => score.coins)
            .ToList();
        
        foreach (var playerScore in sortedScores)
        {
            Instantiate(_playerScorePanelPrefab, _content)
                .Show(playerScore.playerName, playerScore.coins.ToString());
        }
    }
}