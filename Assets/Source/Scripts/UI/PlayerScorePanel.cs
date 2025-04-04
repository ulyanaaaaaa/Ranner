using TMPro;
using UnityEngine;

public class PlayerScorePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _score;

    public void Show(string name, string score)
    {
        _name.text = name;
        _score.text = score;
    }
}
