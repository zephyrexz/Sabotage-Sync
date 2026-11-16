
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
public class MathQuestion
{
    public string question;

    public string[] answers = new string[4];

    public int correctAnswerIndex;
}

public class QuizManager : MonoBehaviour
{
    [Header("Quiz Settings")]
    public List<MathQuestion> questionBank;

    public int minQuestions = 3;
    public int maxQuestions = 5;

    [Header("UI")]
    public GameObject quizPanel;
    public TMP_Text questionText;
    public TMP_Text progressText;

    public Button[] answerButtons;
    public TMP_Text[] answerLabels;

    public GameObject resultPanel;
    public TMP_Text resultText;

    [Header("Manager")]
    public DataManager dataManager;

    private List<MathQuestion> selectedQuestions;

    private int currentQuestion;
    private int currentScore;
    private int correctIndex;

    private PlayerInteraction currentPlayer;
    private PCInteractable currentPC;

    void Awake()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i;

            answerButtons[i].onClick.AddListener(
                () => AnswerQuestion(index)
            );
        }
    }

    void Start()
    {
        quizPanel.SetActive(false);
        resultPanel.SetActive(false);
    }

    public void StartQuiz(
        PCInteractable pc,
        PlayerInteraction player)
    {
        if (pc.isCompleted || questionBank.Count == 0)
            return;

        currentPC = pc;
        currentPlayer = player;

        currentPlayer.SetControl(false);

        currentScore = 0;
        currentQuestion = 0;

        selectedQuestions = new List<MathQuestion>(
            questionBank
        );

        ShuffleQuestions();

        int amount = Random.Range(
            minQuestions,
            maxQuestions + 1
        );

        amount = Mathf.Min(
            amount,
            selectedQuestions.Count
        );

        selectedQuestions =
            selectedQuestions.GetRange(0, amount);

        quizPanel.SetActive(true);
        resultPanel.SetActive(false);

        ShowQuestion();
    }

    void ShuffleQuestions()
    {
        for (int i = 0; i < selectedQuestions.Count; i++)
        {
            int random = Random.Range(
                i,
                selectedQuestions.Count
            );

            MathQuestion temp = selectedQuestions[i];

            selectedQuestions[i] =
                selectedQuestions[random];

            selectedQuestions[random] = temp;
        }
    }


    void ShowQuestion()
    {
        MathQuestion q = selectedQuestions[currentQuestion];

        // Pastikan soal memiliki 4 jawaban
        if (q.answers == null || q.answers.Length != 4)
        {
            Debug.LogError("Soal harus memiliki 4 pilihan jawaban!");
            return;
        }

        // Pastikan UI memiliki 4 tombol dan label
        if (answerButtons.Length != 4 ||
            answerLabels.Length != 4)
        {
            Debug.LogError("AnswerButtons dan AnswerLabels harus berjumlah 4!");
            return;
        }

        // Pastikan index jawaban benar valid
        if (q.correctAnswerIndex < 0 ||
            q.correctAnswerIndex >= 4)
        {
            Debug.LogError("Correct Answer Index tidak valid!");
            return;
        }

        questionText.text = q.question;

        progressText.text =
            "QUESTION " + (currentQuestion + 1) +
            " / " + selectedQuestions.Count;

        // Acak pilihan jawaban
        List<int> order = new List<int>
        {
            0, 1, 2, 3
        };

        for (int i = 0; i < order.Count; i++)
        {
            int random = Random.Range(i, order.Count);

            int temp = order[i];
            order[i] = order[random];
            order[random] = temp;
        }

        for (int i = 0; i < 4; i++)
        {
            int answerIndex = order[i];

            answerLabels[i].text = q.answers[answerIndex];

            if (answerIndex == q.correctAnswerIndex)
            {
                correctIndex = i;
            }
        }
    }


    public void AnswerQuestion(int answer)
    {
        if (answer == correctIndex)
        {
            currentScore++;
        }

        currentQuestion++;

        if (currentQuestion < selectedQuestions.Count)
        {
            ShowQuestion();
        }
        else
        {
            FinishQuiz();
        }
    }

    void FinishQuiz()
    {
        quizPanel.SetActive(false);
        resultPanel.SetActive(true);

        resultText.text =
            "QUIZ COMPLETED!\n\n" +
            "Correct Answers: " + currentScore +
            " / " + selectedQuestions.Count +
            "\n\nData Fragment: +" + currentScore;

        dataManager.AddDataFragments(currentScore);

        currentPC.CompletePC();

        currentPlayer.SetControl(true);

        currentPlayer = null;
        currentPC = null;
    }
}
