using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing.Imaging;





/*
 * "Form1" est le cœur du jeu Snake. Il contient la logique du jeu, le rendu graphique et gère les entrées utilisateur.
 * 
 * Propriétés principales :
 * 1. Snake: Une liste de "Circle" représentant le serpent.
 * 2. food: Un objet "Circle" représentant la nourriture.
 * 3. Les autres variables gèrent des aspects tels que les dimensions du jeu, le score, et les directions de déplacement.
 *
 * Fonctions principales :
 * - KeyIsDown/KeyIsUp: Ces méthodes gèrent les événements de touche enfoncée/relâchée pour contrôler la direction du serpent.
 * - StartGame: Initialise et démarre une nouvelle partie.
 * - TakeSnapShot: Prend une capture d'écran du jeu.
 * - GameTimerEvent: Gère la logique du mouvement du serpent, le déplacement, la collision, manger de la nourriture, et le game over.
 * - UpdatePictureBoxGraphics: Dessine le serpent et la nourriture sur l'interface graphique.
 * - comboBox1_SelectedIndexChanged: Ajuste la vitesse du jeu en fonction de la difficulté sélectionnée par l'utilisateur.
 * - RestartGame: Réinitialise le jeu à ses valeurs par défaut pour une nouvelle partie.
 * - EatFood: Augmente la taille du serpent et actualise le score lorsque le serpent mange la nourriture.
 * - GameOver: Gère les actions à effectuer lorsque le joueur perd.
 */







namespace Classic_Snakes_Game_Tutorial___MOO_ICT
{
    public partial class Form1 : Form
    {
        // Déclaration des variables

        private List<Circle> Snake = new List<Circle>();
        private Circle food = new Circle();

        int maxWidth;
        int maxHeight;

        int score;
        int highScore;

        Random rand = new Random();

        bool goLeft, goRight, goDown, goUp;


        public Form1()
        {
            InitializeComponent();

            // Ajout des éléments à la ComboBox
            comboBox1.Items.Add("Facile");
            comboBox1.Items.Add("Moyen");
            comboBox1.Items.Add("Difficile");

            // Sélectionne le premier élément par défaut
            comboBox1.SelectedIndex = 0;

            new Settings();
        }

        // Méthode pour gérer l'événement lorsqu'une touche est enfoncée
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            // Logique pour déterminer la direction du serpent en fonction de la touche pressée
            if (e.KeyCode == Keys.Left && Settings.directions != "right")
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right && Settings.directions != "left")
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Up && Settings.directions != "down")
            {
                goUp = true;
            }
            if (e.KeyCode == Keys.Down && Settings.directions != "up")
            {
                goDown = true;
            }



        }

        // Méthode pour gérer l'événement lorsqu'une touche est relâchée
        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            // Logique pour stopper le mouvement du serpent
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
        }

        private void StartGame(object sender, EventArgs e)
        {
            RestartGame();
        }

        private void TakeSnapShot(object sender, EventArgs e)
        {
            Label caption = new Label();
            caption.Text = "I scored: " + score + " and my Highscore is " + highScore + " on the Snake Game from MOO ICT";
            caption.Font = new Font("Ariel", 12, FontStyle.Bold);
            caption.ForeColor = Color.Purple;
            caption.AutoSize = false;
            caption.Width = picCanvas.Width;
            caption.Height = 30;
            caption.TextAlign = ContentAlignment.MiddleCenter;
            picCanvas.Controls.Add(caption);

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = "Snake Game SnapShot MOO ICT";
            dialog.DefaultExt = "jpg";
            dialog.Filter = "JPG Image File | *.jpg";
            dialog.ValidateNames = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                int width = Convert.ToInt32(picCanvas.Width);
                int height = Convert.ToInt32(picCanvas.Height);
                Bitmap bmp = new Bitmap(width, height);
                picCanvas.DrawToBitmap(bmp, new Rectangle(0,0, width, height));
                bmp.Save(dialog.FileName, ImageFormat.Jpeg);
                picCanvas.Controls.Remove(caption);
            }





        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            // réglage de la direction

            if (goLeft)
            {
                Settings.directions = "left";
            }
            if (goRight)
            {
                Settings.directions = "right";
            }
            if (goDown)
            {
                Settings.directions = "down";
            }
            if (goUp)
            {
                Settings.directions = "up";
            }
            // fin de la direction

            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {

                    switch (Settings.directions)
                    {
                        case "left":
                            Snake[i].X--;
                            break;
                        case "right":
                            Snake[i].X++;
                            break;
                        case "down":
                            Snake[i].Y++;
                            break;
                        case "up":
                            Snake[i].Y--;
                            break;
                    }

                    if (Snake[i].X < 0)
                    {
                        Snake[i].X = maxWidth;
                    }
                    if (Snake[i].X > maxWidth)
                    {
                        Snake[i].X = 0;
                    }
                    if (Snake[i].Y < 0)
                    {
                        Snake[i].Y = maxHeight;
                    }
                    if (Snake[i].Y > maxHeight)
                    {
                        Snake[i].Y = 0;
                    }


                    if (Snake[i].X == food.X && Snake[i].Y == food.Y)
                    {
                        EatFood();
                    }

                    for (int j = 1; j < Snake.Count; j++)
                    {

                        if (Snake[i].X == Snake[j].X && Snake[i].Y == Snake[j].Y)
                        {
                            GameOver();
                        }

                    }


                }
                else
                {
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }


            picCanvas.Invalidate();

        }

        private void UpdatePictureBoxGraphics(object sender, PaintEventArgs e)
        {

            // Logique pour dessiner le serpent et la nourriture
            Graphics canvas = e.Graphics;

            Brush snakeColour;

            for (int i = 0; i < Snake.Count; i++)
            {
                if (i == 0)
                {
                    snakeColour = Brushes.Black;
                }
                else
                {
                    snakeColour = Brushes.DarkGreen;
                }

                canvas.FillEllipse(snakeColour, new Rectangle
                    (
                    Snake[i].X * Settings.Width,
                    Snake[i].Y * Settings.Height,
                    Settings.Width, Settings.Height
                    ));
            }


            canvas.FillEllipse(Brushes.DarkRed, new Rectangle
            (
            food.X * Settings.Width,
            food.Y * Settings.Height,
            Settings.Width, Settings.Height
            ));
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDifficulty = comboBox1.SelectedItem.ToString();

            switch (selectedDifficulty)
            {
                case "Facile":
                    gameTimer.Interval = 75;
                    break;
                case "Moyen":
                    gameTimer.Interval = 50;
                    break;
                case "Difficile":
                    gameTimer.Interval = 25;
                    break;
                default:
                    gameTimer.Interval = 200; // Valeur par défaut
                    break;
            }
        }



        private void RestartGame()
        {
            maxWidth = picCanvas.Width / Settings.Width - 1;
            maxHeight = picCanvas.Height / Settings.Height - 1;

            Snake.Clear();

            startButton.Enabled = false;
            snapButton.Enabled = false;
            comboBox1.Enabled = false;
            score = 0;
            txtScore.Text = "Score: " + score;

            Circle head = new Circle { X = 10, Y = 5 };
            Snake.Add(head); 

            for (int i = 0; i < 10; i++)
            {
                Circle body = new Circle();
                Snake.Add(body);
            }

            food = new Circle { X = rand.Next(2, maxWidth), Y = rand.Next(2, maxHeight)};

            gameTimer.Start();

        }

        private void EatFood()
        {
            score += 1;

            txtScore.Text = "Score: " + score;

            Circle body = new Circle
            {
                X = Snake[Snake.Count - 1].X,
                Y = Snake[Snake.Count - 1].Y
            };

            Snake.Add(body);

            food = new Circle { X = rand.Next(2, maxWidth), Y = rand.Next(2, maxHeight) };


        }

        private void GameOver()
        {
            gameTimer.Stop();
            startButton.Enabled = true;
            snapButton.Enabled = true;
            comboBox1.Enabled = true;

            if (score > highScore)
            {
                highScore = score;

                txtHighScore.Text = "High Score: " + Environment.NewLine + highScore;
                txtHighScore.ForeColor = Color.Maroon;
                txtHighScore.TextAlign = ContentAlignment.MiddleCenter;
            }
        }


    }
}
