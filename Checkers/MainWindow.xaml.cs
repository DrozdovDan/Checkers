using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
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
using static Checkers.WhiteTurnClass;
using static Checkers.BlackTurnClass;
using static Checkers.Bot;

namespace Checkers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool blackBot = false;
        public static bool whiteBot = false;
        public static String message = "Black turn";
        public const int maxSizeOfField = 8;
        private const int numberOfRowsForMen = 3;
        public static List<Ellipse> whiteEllipses = new List<Ellipse>();
        public static List<Ellipse> blackEllipses = new List<Ellipse>();
        public static Button[,] field = new Button[maxSizeOfField, maxSizeOfField];
        public static int countOfBeatenMen = 0;
        public static bool canBeat = false;
        public static bool becomeKing = false;
        public static Ellipse[,] whiteMans = new Ellipse[maxSizeOfField, maxSizeOfField];
        public static Ellipse[,] blackMans = new Ellipse[maxSizeOfField, maxSizeOfField];
        public static bool canMove = false;
        private bool endGame = false;

        public MainWindow()
        {
            InitializeComponent();

            CreateField();

            SetMen();
        }

        public static bool isTriggered = false;
        public static Button? lastTriggeredButton;
        public static bool whiteTurn = false;
        public static Ellipse? manToBeat1 = null, manToBeat2 = null, manToBeat3 = null, manToBeat4 = null;

        private void ClickOnSquare(object? sender, RoutedEventArgs e)
        {
            if (!endGame)
            {
                canBeat = false;
                canMove = false;

                var thatButton = (Button)sender!;

                SetField();

                if (whiteTurn)
                {
                    IfCanBeatWhite();

                    WhoWon();

                    if (canMove)
                    {
                        WhiteTurn(thatButton);

                        if (blackBot)
                        {
                            blackBot = !blackBot;
                            BlackBot(BlackBotButton, e);
                        }
                    }

                    Message.Text = message;
                }
                else
                {
                    IfCanBeatBlack();

                    WhoWon();

                    if (canMove)
                    {
                        BlackTurn(thatButton);

                        if (whiteBot)
                        {
                            whiteBot = !whiteBot;
                            WhiteBot(WhiteBotButton, e);
                        }
                    }

                    Message.Text = message;
                }
                
                WhoWon();
            }
        }

        public static void SetField()
        {
            //Set empty ellipses to squares that does not have men
            for (int i = 0; i < maxSizeOfField; i++)
            {
                for (int j = 0; j < maxSizeOfField; j++)
                {
                    whiteMans[i, j] = new Ellipse();
                }
            }

            //Set an array of white men
            foreach (var ellipse in whiteEllipses)
            {
                whiteMans[Grid.GetRow(ellipse), Grid.GetColumn(ellipse)] = ellipse;
            }

            //Set empty ellipses to squares that does not have men
            for (int i = 0; i < maxSizeOfField; i++)
            {
                for (int j = 0; j < maxSizeOfField; j++)
                {
                    blackMans[i, j] = new Ellipse();
                }
            }

            //Set an array of black men
            foreach (var ellipse in blackEllipses)
            {
                blackMans[Grid.GetRow(ellipse), Grid.GetColumn(ellipse)] = ellipse;
            }
        }

        public static void SetColorBack()
        {
            if (lastTriggeredButton != null)
            {
                var columnOfButton = Grid.GetColumn(lastTriggeredButton);
                var rowOfButton = Grid.GetRow(lastTriggeredButton);
                lastTriggeredButton.Background = Brushes.Chocolate;
                for (int i = 0; i < maxSizeOfField; i++)
                {
                    for (int j = 0; j < maxSizeOfField; j++)
                    {
                        if ((i + j) % 2 != 0) field[i, j].Background = Brushes.Chocolate;
                    }
                }
            }
        }

        public void Reset(object? sender, RoutedEventArgs e)
        {
            InitializeComponent();

            endGame = false;
            countOfBeatenMen = 0;
            canBeat = false;
            becomeKing = false;
            isTriggered = false;
            manToBeat1 = null;
            manToBeat2 = null;
            manToBeat3 = null;
            manToBeat4 = null;
            whiteTurn = false;
            lastTriggeredButton = null;
            canMove = false;
            blackBot = false;
            whiteBot = false;

            CreateField();

            whiteEllipses = new List<Ellipse>();
            blackEllipses = new List<Ellipse>();
            SetMen();

            Message.Text = "Black turn";

            WB.Text = "Is inactive";
            BB.Text = "Is inactive";
        }

        private void CreateField()
        {
            MyGrid.Background = Brushes.AntiqueWhite;
            for (int i = 0; i < maxSizeOfField; i++)
            {
                for (int j = 0; j < maxSizeOfField; j++)
                {
                    if ((i + j) % 2 != 0)
                    {
                        Button button = new Button();
                        Grid.SetColumn(button, j);
                        Grid.SetRow(button, i);
                        button.Background = Brushes.Chocolate;
                        button.Click += ClickOnSquare;
                        MyGrid.Children.Add(button);
                        field[i, j] = button;
                    }

                    AddNumbers(i, j);
                }
            }
        }

        private void AddNumbers(int i, int j)
        {
            if (j == 0)
            {
                TextBlock textBlock = new TextBlock();
                Grid.SetColumn(textBlock, j);
                Grid.SetRow(textBlock, i);
                textBlock.Text = (maxSizeOfField - i).ToString();
                textBlock.HorizontalAlignment = HorizontalAlignment.Left;
                textBlock.VerticalAlignment = VerticalAlignment.Top;
                if (i % 2 != 0)
                {
                    textBlock.Foreground = Brushes.AntiqueWhite;
                }
                else
                {
                    textBlock.Foreground = Brushes.Chocolate;
                }

                textBlock.IsHitTestVisible = false;
                MyGrid.Children.Add(textBlock);
            }
            if (i == maxSizeOfField - 1)
            {
                TextBlock textBlock = new TextBlock();
                Grid.SetColumn(textBlock, j);
                Grid.SetRow(textBlock, i);
                textBlock.Text = ((char)(j + 65)).ToString();
                textBlock.HorizontalAlignment = HorizontalAlignment.Right;
                textBlock.VerticalAlignment = VerticalAlignment.Bottom;
                if (j % 2 == 0)
                {
                    textBlock.Foreground = Brushes.AntiqueWhite;
                }
                else
                {
                    textBlock.Foreground = Brushes.Chocolate;
                }

                textBlock.IsHitTestVisible = false;
                MyGrid.Children.Add(textBlock);
            }
        }

        private void SetMen()
        {
            for (int i = 0; i < maxSizeOfField; i++)
            {
                for (int j = 0; j < maxSizeOfField; j++)
                {
                    if ((i + j) % 2 != 0)
                    {
                        Ellipse ellipse = new Ellipse();
                        Grid.SetColumn(ellipse, j);
                        Grid.SetRow(ellipse, i);
                        ellipse.Width = 90;
                        ellipse.Height = 90;
                        ellipse.IsHitTestVisible = false;
                        if (i < numberOfRowsForMen)
                        {
                            ellipse.Fill = Brushes.Black;
                            blackEllipses.Add(ellipse);
                            MyGrid.Children.Add(ellipse);
                        }

                        if (i >= maxSizeOfField - numberOfRowsForMen)
                        {
                            ellipse.Fill = Brushes.White;
                            whiteEllipses.Add(ellipse);
                            MyGrid.Children.Add(ellipse);
                        }
                    }
                }
            }
        }

        public void WhoWon()
        {
            if (whiteTurn && !canMove || whiteEllipses.Count == 0)
            {
                Message.Text = "BLACK WON";
                endGame = true;
            }
            else if (!whiteTurn && !canMove || blackEllipses.Count == 0)
            {
                Message.Text = "WHITE WON";
                endGame = true;
            }
        }

        private void BlackBot(object? sender, RoutedEventArgs e)
        {
            if (!endGame && !whiteBot)
            {
                blackBot = !blackBot;
                
                if (blackBot)
                {
                    BB.Text = "Is active";
                }
                else
                {
                    BB.Text = "Is inactive";
                }

                if (blackBot && !whiteTurn)
                {
                    StartBot(blackEllipses, !whiteTurn);
                    if (whiteTurn) Message.Text = "White turn";
                    WhoWon();
                }
            }
        }

        private void WhiteBot(object? sender, RoutedEventArgs e)
        {
            if (!endGame && !blackBot)
            {
                whiteBot = !whiteBot;

                if (whiteBot)
                {
                    WB.Text = "Is active";
                }
                else
                {
                    WB.Text = "Is inactive";
                }

                if (whiteBot && whiteTurn)
                {
                    StartBot(whiteEllipses, whiteTurn);
                    if (!whiteTurn) Message.Text = "Black turn";
                    WhoWon();
                }
            }
        }
    }
}