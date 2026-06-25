using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Net_Bot_App
{
    public class QuizQuestion
    {
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public string CorrectAnswer { get; set; }
        public string Explanation { get; set; }
        public bool IsTrueFalse { get; set; }
    }

    public partial class QuizGame : Window
    {
        private List<QuizQuestion> _questions;
        private int _currentIndex = 0;
        private int _score = 0;

        public QuizGame()
        {
            InitializeComponent();
            LoadQuestions();
            ShowQuestion();
        }

        private void LoadQuestions()
        {
            _questions = new List<QuizQuestion>
            {
                new QuizQuestion
                {
                    Question = "What should you do if you receive an email asking for your password?",
                    Options = new List<string> { "A) Reply with your password", "B) Delete the email", "C) Report it as phishing", "D) Ignore it" },
                    CorrectAnswer = "C",
                    Explanation = "✅ Correct! Reporting phishing emails helps protect others and stops scammers.",
                    IsTrueFalse = false
                },
                new QuizQuestion
                {
                    Question = "A strong password should be at least 12 characters long.",
                    Options = new List<string>(),
                    CorrectAnswer = "True",
                    Explanation = "✅ Correct! Longer passwords are exponentially harder to crack.",
                    IsTrueFalse = true
                },
                new QuizQuestion
                {
                    Question = "Which of these is the safest password?",
                    Options = new List<string> { "A) password123", "B) John1990", "C) $uN#8kLm!2vQ", "D) qwerty" },
                    CorrectAnswer = "C",
                    Explanation = "✅ Correct! A mix of symbols, numbers, and random letters makes it very hard to guess.",
                    IsTrueFalse = false
                },
                new QuizQuestion
                {
                    Question = "It is safe to use the same password on multiple websites.",
                    Options = new List<string>(),
                    CorrectAnswer = "False",
                    Explanation = "❌ False! If one site is breached, all your accounts become vulnerable.",
                    IsTrueFalse = true
                },
                new QuizQuestion
                {
                    Question = "What does HTTPS in a website URL indicate?",
                    Options = new List<string> { "A) The site is popular", "B) The site has encrypted connection", "C) The site is government owned", "D) The site is free" },
                    CorrectAnswer = "B",
                    Explanation = "✅ Correct! HTTPS means your connection to the site is encrypted and more secure.",
                    IsTrueFalse = false
                },
                new QuizQuestion
                {
                    Question = "Public Wi-Fi networks are always safe to use for banking.",
                    Options = new List<string>(),
                    CorrectAnswer = "False",
                    Explanation = "❌ False! Public Wi-Fi is unencrypted — attackers can intercept your data. Use a VPN.",
                    IsTrueFalse = true
                },
                new QuizQuestion
                {
                    Question = "What is social engineering in cybersecurity?",
                    Options = new List<string> { "A) Building social media apps", "B) Manipulating people to reveal confidential info", "C) Engineering social networks", "D) Creating fake profiles" },
                    CorrectAnswer = "B",
                    Explanation = "✅ Correct! Social engineering tricks people psychologically rather than hacking systems directly.",
                    IsTrueFalse = false
                },
                new QuizQuestion
                {
                    Question = "Two-factor authentication (2FA) makes your account significantly more secure.",
                    Options = new List<string>(),
                    CorrectAnswer = "True",
                    Explanation = "✅ Correct! Even if your password is stolen, 2FA blocks unauthorised access.",
                    IsTrueFalse = true
                },
                new QuizQuestion
                {
                    Question = "What is ransomware?",
                    Options = new List<string> { "A) Software that speeds up your PC", "B) A type of antivirus", "C) Malware that encrypts your files and demands payment", "D) A firewall tool" },
                    CorrectAnswer = "C",
                    Explanation = "✅ Correct! Ransomware locks your files and criminals demand payment to restore access.",
                    IsTrueFalse = false
                },
                new QuizQuestion
                {
                    Question = "You should regularly back up your data to protect against ransomware.",
                    Options = new List<string>(),
                    CorrectAnswer = "True",
                    Explanation = "✅ Correct! Regular backups mean you can restore files without paying ransoms.",
                    IsTrueFalse = true
                },
                new QuizQuestion
                {
                    Question = "Which action best protects your privacy on social media?",
                    Options = new List<string> { "A) Share your location always", "B) Use your full name and birthday publicly", "C) Review and restrict your privacy settings", "D) Accept all friend requests" },
                    CorrectAnswer = "C",
                    Explanation = "✅ Correct! Restricting privacy settings limits who can see your personal information.",
                    IsTrueFalse = false
                },
                new QuizQuestion
                {
                    Question = "Antivirus software updates are unnecessary if your PC runs fine.",
                    Options = new List<string>(),
                    CorrectAnswer = "False",
                    Explanation = "❌ False! Updates patch vulnerabilities and protect against the latest threats.",
                    IsTrueFalse = true
                }
            };
        }

        private void ShowQuestion()
        {
            if (_currentIndex >= _questions.Count)
            {
                ShowFinalScore();
                return;
            }

            var q = _questions[_currentIndex];
            QuestionText.Text = q.Question;
            QuestionCounter.Text = $"Question {_currentIndex + 1} of {_questions.Count}";
            QuizProgress.Value = _currentIndex + 1;
            FeedbackBox.Visibility = Visibility.Collapsed;
            NextButton.Visibility = Visibility.Collapsed;

            // Reset multiple-choice button colors
            foreach (var btn in new[] { BtnA, BtnB, BtnC, BtnD })
            {
                btn.Background = new SolidColorBrush(Color.FromRgb(107, 33, 168));
                btn.IsEnabled = true;
            }

            // Reset True/False button colors too (this was missing - caused T/F buttons to stay disabled after first use)
            foreach (var child in TrueFalsePanel.Children)
            {
                if (child is Button b)
                {
                    b.Background = new SolidColorBrush(Color.FromRgb(107, 33, 168));
                    b.IsEnabled = true;
                }
            }

            if (q.IsTrueFalse)
            {
                BtnA.Visibility = Visibility.Collapsed;
                BtnB.Visibility = Visibility.Collapsed;
                BtnC.Visibility = Visibility.Collapsed;
                BtnD.Visibility = Visibility.Collapsed;
                TrueFalsePanel.Visibility = Visibility.Visible;
            }
            else
            {
                TrueFalsePanel.Visibility = Visibility.Collapsed;
                BtnA.Visibility = Visibility.Visible;
                BtnB.Visibility = Visibility.Visible;
                BtnC.Visibility = Visibility.Visible;
                BtnD.Visibility = Visibility.Visible;
                BtnA.Content = q.Options[0];
                BtnB.Content = q.Options[1];
                BtnC.Content = q.Options[2];
                BtnD.Content = q.Options[3];
            }
        }

        private void Answer_Click(object sender, RoutedEventArgs e)
        {
            Button clicked = sender as Button;
            string answer = clicked.Tag.ToString();
            var q = _questions[_currentIndex];
            bool correct = answer == q.CorrectAnswer;

            if (correct)
            {
                _score++;
                ScoreDisplay.Text = $"Score: {_score}";
                clicked.Background = new SolidColorBrush(Colors.Green);
                FeedbackText.Text = q.Explanation;
                FeedbackBox.Background = new SolidColorBrush(Color.FromRgb(20, 80, 20));
            }
            else
            {
                clicked.Background = new SolidColorBrush(Colors.Red);
                FeedbackText.Text = $"❌ Incorrect! The correct answer was {q.CorrectAnswer}. {q.Explanation}";
                FeedbackBox.Background = new SolidColorBrush(Color.FromRgb(80, 20, 20));
            }

            // Disable all multiple-choice buttons after answering
            foreach (var btn in new[] { BtnA, BtnB, BtnC, BtnD })
                btn.IsEnabled = false;

            // Disable true/false buttons too
            foreach (var child in TrueFalsePanel.Children)
                if (child is Button b) b.IsEnabled = false;

            FeedbackBox.Visibility = Visibility.Visible;
            NextButton.Content = _currentIndex + 1 >= _questions.Count ? "See Final Score 🏆" : "Next Question ➤";
            NextButton.Visibility = Visibility.Visible;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            _currentIndex++;
            ShowQuestion();
        }

        private void ShowFinalScore()
        {
            QuestionText.Text = $"🏆 Quiz Complete!\n\nYou scored {_score} out of {_questions.Count}";
            BtnA.Visibility = Visibility.Collapsed;
            BtnB.Visibility = Visibility.Collapsed;
            BtnC.Visibility = Visibility.Collapsed;
            BtnD.Visibility = Visibility.Collapsed;
            TrueFalsePanel.Visibility = Visibility.Collapsed;
            FeedbackBox.Visibility = Visibility.Visible;

            string message;
            if (_score == _questions.Count)
                message = "🌟 Perfect score! You're a cybersecurity expert!";
            else if (_score >= _questions.Count * 0.8)
                message = "🎉 Excellent work! You have strong cybersecurity knowledge!";
            else if (_score >= _questions.Count * 0.6)
                message = "👍 Good effort! Keep learning to stay safe online.";
            else
                message = "📚 Keep studying! Cybersecurity knowledge protects you every day.";

            FeedbackText.Text = message;
            NextButton.Content = "Close Quiz ✖";
            NextButton.Visibility = Visibility.Visible;
            NextButton.Click -= Next_Click;
            NextButton.Click += (s, e) => this.Close();
        }
    }
}