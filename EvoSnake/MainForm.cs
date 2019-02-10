using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace EvoSnake
{
    public partial class MainForm : Form
    {
        Timer timer = new Timer();

        Snake best;
        SnakeManager snakeManager;

        bool play = false;

        float
            w = 720.0f / Settings.fieldWidth,
            h = 720.0f / Settings.fieldHeight;

        Graphics g; 

        SolidBrush food = new SolidBrush(Color.FromArgb(255, 152, 0));
        SolidBrush tail = new SolidBrush(Color.FromArgb(139, 195, 74));
        SolidBrush head = new SolidBrush(Color.FromArgb(76, 175, 80));

        public MainForm()
        {
            InitializeComponent();

            g = DrawingPanel.CreateGraphics();

            if (File.Exists("Snakes.txt"))
            {
                using (var sr = new StreamReader(File.Open("Snakes.txt", FileMode.Open), Encoding.UTF8))
                {
                    var str = sr.ReadToEnd();
                    sr.Close();
                    snakeManager = JsonConvert.DeserializeObject<SnakeManager>(str);
                }
            }
            else
            {
                snakeManager = new SnakeManager();
            }

            timer.Interval = 1000;

            InitNewGenerationAsync();

            timer.Tick += (s, e) =>
            {
                if (best != null && best.Alive)
                {
                    DrawSnake();
                    best.Step();
                }
                else
                {                  
                    InitNewGenerationAsync();
                }
            };
        }

        async void InitNewGenerationAsync()
        {
            timer.Stop();

            MessageLabel.Text = "Initializing new generation...";

            snakeManager.Update();

            best = new Snake(snakeManager.SelectBest());

            snakeManager.NaturalSelection();

            GenLabel.Text = snakeManager.Generation.ToString();
            FitLabel.Text = snakeManager.Fitness.ToString();

            MessageLabel.Text = string.Empty;

            if (play)
                timer.Start();
        }

        void DrawSnake()
        {     
            g.Clear(Color.FromArgb(55, 71, 79));          

            g.FillRectangle(food, w * best.Food.X, h * best.Food.Y, w, h);
            foreach (var segment in best.Tail)
                g.FillRectangle(tail, w * segment.X, h * segment.Y, w, h);
            g.FillRectangle(head, w * best.Head.X, h * best.Head.Y, w, h);
        }

        private void PlayButton_Click(object sender, System.EventArgs e)
        {
            if (play = !play)
            {
                PlayButton.Text = "Stop";
                timer.Start();
            }
            else
            {
                PlayButton.Text = "Start";
                timer.Stop();
            }
        }

        private void SpeedControl_ValueChanged(object sender, System.EventArgs e)
        {
            timer.Interval = SpeedControl.Value * 10;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (var sw = new StreamWriter(File.Open("Snakes.txt", FileMode.Create), Encoding.UTF8))
            {
                sw.Write(JsonConvert.SerializeObject(snakeManager));
                sw.Flush();
                sw.Close();
            }
        }
    }
}
