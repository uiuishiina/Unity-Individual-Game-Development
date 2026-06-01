using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Quiz_Answer 
{
    [SerializeField,Header("‰ğ“š")]public string Answer;
    [SerializeField,Header("“¾‚ç‚ê‚é“_”")]public int Score;
}

[CreateAssetMenu(fileName = "NewQuizDate", menuName = "QuizDate")]
public class QuizDate : ScriptableObject
{
    [SerializeField, Header("o‘è‰æ‘œ")] public Sprite image_date;
    [SerializeField,Header("‰ğ“š”z—ñ")]public List<Quiz_Answer> answers;
}
