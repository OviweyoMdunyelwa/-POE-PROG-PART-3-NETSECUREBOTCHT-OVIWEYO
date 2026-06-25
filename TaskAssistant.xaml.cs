using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Net_Bot_App
{
    public partial class TaskAssistant : Window
    {
        private readonly TaskManager _taskManager;

        public TaskAssistant()
        {
            InitializeComponent();
            _taskManager = new TaskManager();
            RefreshTaskList();
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleInput.Text.Trim();
            string description = DescriptionInput.Text.Trim();
            string reminder = ReminderInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Please enter a title for the task.");
                return;
            }

            _taskManager.AddTask(title, description, reminder);

            TitleInput.Text = "";
            DescriptionInput.Text = "";
            ReminderInput.Text = "";

            RefreshTaskList();
        }

        private void RefreshTaskList()
        {
            TaskListContainer.Items.Clear();

            var tasks = _taskManager.GetAllTasks();

            foreach (var task in tasks)
            {
                var border = new Border
                {
                    Background = new SolidColorBrush(Color.FromRgb(107, 33, 168)),
                    CornerRadius = new CornerRadius(10),
                    Padding = new Thickness(12),
                    Margin = new Thickness(0, 0, 0, 8)
                };

                var stack = new StackPanel();

                var titleBlock = new TextBlock
                {
                    Text = (task.IsComplete ? "✅ " : "🔲 ") + task.Title,
                    Foreground = Brushes.White,
                    FontSize = 16,
                    FontWeight = FontWeights.Bold,
                    TextDecorations = task.IsComplete ? TextDecorations.Strikethrough : null
                };
                stack.Children.Add(titleBlock);

                if (!string.IsNullOrWhiteSpace(task.Description))
                {
                    stack.Children.Add(new TextBlock
                    {
                        Text = task.Description,
                        Foreground = Brushes.White,
                        TextWrapping = TextWrapping.Wrap,
                        Margin = new Thickness(0, 4, 0, 0)
                    });
                }

                if (!string.IsNullOrWhiteSpace(task.Reminder))
                {
                    stack.Children.Add(new TextBlock
                    {
                        Text = "⏰ " + task.Reminder,
                        Foreground = new SolidColorBrush(Color.FromRgb(229, 213, 255)),
                        FontSize = 13,
                        Margin = new Thickness(0, 4, 0, 0)
                    });
                }

                var buttonRow = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 8, 0, 0) };

                if (!task.IsComplete)
                {
                    var completeBtn = new Button
                    {
                        Content = "Mark Complete",
                        Tag = task.Id,
                        Padding = new Thickness(10, 4, 10, 4),
                        Margin = new Thickness(0, 0, 8, 0),
                        Background = Brushes.Green,
                        Foreground = Brushes.White,
                        BorderThickness = new Thickness(0)
                    };
                    completeBtn.Click += (s, e) =>
                    {
                        _taskManager.MarkAsComplete(task.Id);
                        RefreshTaskList();
                    };
                    buttonRow.Children.Add(completeBtn);
                }

                var deleteBtn = new Button
                {
                    Content = "Delete",
                    Tag = task.Id,
                    Padding = new Thickness(10, 4, 10, 4),
                    Background = Brushes.DarkRed,
                    Foreground = Brushes.White,
                    BorderThickness = new Thickness(0)
                };
                deleteBtn.Click += (s, e) =>
                {
                    _taskManager.DeleteTask(task.Id);
                    RefreshTaskList();
                };
                buttonRow.Children.Add(deleteBtn);

                stack.Children.Add(buttonRow);
                border.Child = stack;
                TaskListContainer.Items.Add(border);
            }
        }
    }
}
