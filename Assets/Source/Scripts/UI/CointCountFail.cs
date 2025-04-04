using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CointCountFail : MonoBehaviour
{
    public int Score { get; private set; }
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void DisplayCount(int count)
    {
        Score = count;
        _text.text = "Your score:  <sprite name=\"coin\">" + count;
    }
}
