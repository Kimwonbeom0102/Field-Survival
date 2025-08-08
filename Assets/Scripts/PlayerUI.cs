using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public Slider healthSlider;
    public TextMeshProUGUI healthText; 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealth(int current, int max)
    {
         // 슬라이더 값 조정
        healthSlider.maxValue = max;
        healthSlider.value = current;

        if( healthText != null)
            healthText.text = "HP " + current + "/" + max;
    }
}
