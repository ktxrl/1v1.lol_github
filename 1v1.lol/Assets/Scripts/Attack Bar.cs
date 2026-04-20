using UnityEngine;
using UnityEngine.UI;

public class AttackBar : MonoBehaviour
{
    [SerializeField] Image comboBar;
    [SerializeField] float comboWindow = 1f;
    private float currentTimer;

    // Update is called once per frame
    void Update()
    {
        if (currentTimer > 0)
        {
            currentTimer -= Time.deltaTime;
            comboBar.fillAmount = currentTimer / comboWindow;
        }
        else
        {
            comboBar.fillAmount = 0;
        }
    }
    public void ResetCombo()
    {
        currentTimer = comboWindow;
    }
}
