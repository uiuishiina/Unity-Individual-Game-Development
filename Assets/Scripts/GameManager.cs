using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class GameManager : MonoBehaviour
{
    [Header("問題データ")]
    [SerializeField] private QuizDate[] quizData;

    [Header("QuizUI")]
    [SerializeField] private GameObject quizPanel;
    [SerializeField] private Image quizImage;
    [SerializeField] private TMP_InputField answerInput;
    [SerializeField] private TMP_Text answerText;
    [SerializeField] private TMP_Text timerText;

    [Header("ResultUI")]
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private TMP_Text resultSucoreText;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private Button retryButton;

    [Header("制限時間")]
    [SerializeField] private float timer;
    [SerializeField] private float gameTime;

    [Header("ズーム時間")]
    [SerializeField] private float zoom_time;
    RectTransform rect;

    [Header("ループ管理")]
    [SerializeField] private int quizIndex = 0;
    private UniTaskCompletionSource _methodExecuteSource;
    [SerializeField] int gameCount = 0;

    [Header("スコアなどの管理")]
    [SerializeField] private int gameScore = 0;
    [SerializeField] private int highScore = 0;
    private bool is_timeup = false;

    private void Start()
    {
        QuizGame(gameCount);
    }

    async UniTaskVoid QuizGame(int quizCount)
    {
        //  初期化
        gameScore = 0;
        quizPanel.SetActive(true);
        resultPanel.SetActive(false);
        timer = gameTime;
        is_timeup = false;

        //  出題ループ
        for (int i = 0;i < quizCount; i++) {

            //  待機イベント作成
            _methodExecuteSource = new UniTaskCompletionSource();

            //  出題
            quizIndex = Random.Range(0, quizData.Length);
            ShowQuiz(quizIndex);

            //  回答許可
            answerInput.interactable = true;

            //  イベント待機
            await _methodExecuteSource.Task;

            //  タイムアップなら終了
            if (is_timeup) {
                break;
            }
        }

        //  Result処理
        ShowResult(gameScore);
    }

    /// <summary>
    /// 時間管理
    /// </summary>
    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0) {
            is_timeup = true;
            if (_methodExecuteSource != null) {
                _methodExecuteSource.TrySetResult();
            }
        }
        else {
            timerText.text = "TIME : "+ timer.ToString("F1");
        }
    }

    /// <summary>
    /// クイズ出力関数
    /// </summary>
    /// <param name="index">クイズ配列インデックス</param>
    private void ShowQuiz(int index)
    {
        quizImage.sprite = quizData[index].image_date;

        answerInput.text = "";

        rect?.DOKill();

        rect = quizImage.rectTransform;
        rect.localScale = Vector3.one * zoom_time;
        rect.DOScale(1f, zoom_time).SetEase(Ease.OutQuad);

        FocusInput().Forget();
    }

    private async UniTask FocusInput()
    {
        await UniTask.Yield();

        answerInput.Select();
        answerInput.ActivateInputField();
    }

    /// <summary>
    /// 回答チェック関数
    /// </summary>
    public void CheckAnswer()
    {
        string playerAnswer = answerInput.text.Trim();
        answerInput.interactable = false;

        bool found = false;

        foreach (Quiz_Answer answer in quizData[quizIndex].answers) {
            if (playerAnswer.ToLower() == answer.Answer.ToLower()) {
                answerText.text = $"Success\nGet Score : {answer.Score}";
                gameScore += answer.Score;
                found = true;
                break;
            }
        }

        if (!found) {
            answerText.text = "Failed";
        }

        //  待機処理終了を通知
        if (_methodExecuteSource != null) {
            _methodExecuteSource.TrySetResult();
        }
    }

    /// <summary>
    /// リザルト表示関数
    /// </summary>
    void ShowResult(int score)
    {
        resultPanel.SetActive(true);
        resultSucoreText.text = "ResultScore : " + score.ToString();
        if (highScore < score) {
            highScore = score;
        }
        highScoreText.text = "HighScore : " + highScore.ToString();
    }

    public void RetryGame() {
        QuizGame(gameCount);
    }
}