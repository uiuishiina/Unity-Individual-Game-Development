using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class GameManager : MonoBehaviour
{
    [Header("問題データ")]
    [SerializeField] private QuizDate quizData;

    [Header("UI")]
    [SerializeField] private Image quizImage;
    [SerializeField] private TMP_InputField answerInput;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private TMP_Text timerText;

    [Header("制限時間")]
    [SerializeField] private float timer;

    [Header("ズーム時間")]
    [SerializeField] private float zoom_time;

    private void Start()
    {
        ShowQuiz();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {

        }
        else
        {
            timerText.text = "TIME : "+ timer.ToString("F1");
        }
    }
    private void ShowQuiz()
    {
        quizImage.sprite = quizData.image_date;

        answerInput.text = "";
        resultText.text = "";

        RectTransform rect = quizImage.rectTransform;

        rect.localScale = Vector3.one * zoom_time;
        rect.DOScale(1f, zoom_time).SetEase(Ease.OutQuad);

    }

    public void CheckAnswer()
    {
        string playerAnswer = answerInput.text.Trim();

        bool found = false;

        foreach (Quiz_Answer answer in quizData.answers)
        {
            if (playerAnswer.ToLower() == answer.Answer.ToLower())
            {
                resultText.text = $"Success\nGet Score : {answer.Score}";
                timer += answer.Score;
                found = true;
                break;
            }
        }

        if (!found)
        {
            resultText.text = "Failed";
        }
    }
}