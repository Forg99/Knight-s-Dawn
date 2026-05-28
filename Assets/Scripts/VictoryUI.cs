using UnityEngine;
using UnityEngine.UI;

public class VictoryUI : MonoBehaviour
{
    [SerializeField] private Text coinsText;
    [SerializeField] private Text deathsText;

    void Update()
    {
        coinsText.text = "x" + GameManager.Instance.coins + "/18";
        deathsText.text = "x" + GameManager.Instance.deaths;
    }

}
