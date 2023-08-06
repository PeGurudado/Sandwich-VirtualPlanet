using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GradeEffectScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gradeText;

    [SerializeField] private Color badColor;
    [SerializeField] private Color niceColor;
    [SerializeField] private Color greatColor;
    [SerializeField] private Color perfectColor;

    private void Start()
    {
        Destroy(this.gameObject, 2);
    }

    public void Initialize(int  bonusScore ){

        if (bonusScore < 0)
        {
            gradeText.text = "Bad";
            gradeText.color = badColor;
        }
        else if(bonusScore < 5)
        {
            gradeText.text = "Nice";
            gradeText.color = niceColor;

        }
        else if(bonusScore < 10)
        {
            gradeText.text = "Great!";
            gradeText.color = greatColor;
        }
        else 
        {
            gradeText.text = "Amazing!!";
            gradeText.color = perfectColor;
        }        
    }

    public void Initialize(string text )
    {
        gradeText.text = text;        
    }


}
