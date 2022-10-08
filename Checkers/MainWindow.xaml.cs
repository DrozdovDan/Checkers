using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using static Checkers.WhiteTurnClass;
using static Checkers.BlackTurnClass;

namespace Checkers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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

        public MainWindow()
        {
            InitializeComponent();
            //Set field
            for (int i = 0; i < maxSizeOfField; i++)
            {
                for (int j = 0; j < maxSizeOfField; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        field[i, j] = new Button();
                        Border border = new Border();
                        Grid.SetColumn(border, j);
                        Grid.SetRow(border, i);
                        border.Background = Brushes.AntiqueWhite;
                        MyGrid.Children.Add(border);
                    }
                    else
                    {
                        Button button = new Button();
                        Grid.SetColumn(button, j);
                        Grid.SetRow(button, i);
                        button.Background = Brushes.Chocolate;
                        button.Click += NewClick;
                        MyGrid.Children.Add(button);
                        field[i, j] = button;
                    }
                }
            }

            //Set Men
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

        public static bool isTriggered = false;
        public static Button? lastTriggeredButton;
        public static bool whiteTurn = false;
        public static Ellipse? manToBeat1 = null, manToBeat2 = null, manToBeat3 = null, manToBeat4 = null;

        private void NewClick(object? sender, RoutedEventArgs e)
        {
            canBeat = false;

            var thatButton = (Button)sender!;

            SetField();

            if (whiteTurn)
            {
                IfCanBeatWhite();

                WhiteTurn(thatButton);
            }
            else
            {
                IfCanBeatBlack();

                BlackTurn(thatButton);
            }

            //Who won?
            if (whiteEllipses.Count == 0)
            {
                BlackWon.Visibility = Visibility.Visible;
            }

            if (blackEllipses.Count == 0)
            {
                WhiteWon.Visibility = Visibility.Visible;
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
            lastTriggeredButton.Background = Brushes.Chocolate;
            if (Grid.GetColumn(lastTriggeredButton) > 0 && Grid.GetRow(lastTriggeredButton) < maxSizeOfField - 1)
            {
                field[Grid.GetRow(lastTriggeredButton) + 1, Grid.GetColumn(lastTriggeredButton) - 1]
                    .Background = Brushes.Chocolate;
            }

            if (Grid.GetColumn(lastTriggeredButton) < maxSizeOfField - 1 &&
                Grid.GetRow(lastTriggeredButton) < maxSizeOfField - 1)
            {
                field[Grid.GetRow(lastTriggeredButton) + 1, Grid.GetColumn(lastTriggeredButton) + 1]
                    .Background = Brushes.Chocolate;
            }

            if (Grid.GetColumn(lastTriggeredButton) > 0 && Grid.GetRow(lastTriggeredButton) > 0)
            {
                field[Grid.GetRow(lastTriggeredButton) - 1, Grid.GetColumn(lastTriggeredButton) - 1]
                    .Background = Brushes.Chocolate;
            }

            if (Grid.GetColumn(lastTriggeredButton) < maxSizeOfField - 1 && Grid.GetRow(lastTriggeredButton) > 0)
            {
                field[Grid.GetRow(lastTriggeredButton) - 1, Grid.GetColumn(lastTriggeredButton) + 1]
                    .Background = Brushes.Chocolate;
            }

            if (Grid.GetColumn(lastTriggeredButton) > 1 && Grid.GetRow(lastTriggeredButton) < maxSizeOfField - 2)
            {
                field[Grid.GetRow(lastTriggeredButton) + 2, Grid.GetColumn(lastTriggeredButton) - 2]
                    .Background = Brushes.Chocolate;
            }

            if (Grid.GetColumn(lastTriggeredButton) < maxSizeOfField - 2 &&
                Grid.GetRow(lastTriggeredButton) < maxSizeOfField - 2)
            {
                field[Grid.GetRow(lastTriggeredButton) + 2, Grid.GetColumn(lastTriggeredButton) + 2]
                    .Background = Brushes.Chocolate;
            }

            if (Grid.GetColumn(lastTriggeredButton) > 1 && Grid.GetRow(lastTriggeredButton) > 1)
            {
                field[Grid.GetRow(lastTriggeredButton) - 2, Grid.GetColumn(lastTriggeredButton) - 2]
                    .Background = Brushes.Chocolate;
            }

            if (Grid.GetColumn(lastTriggeredButton) < maxSizeOfField - 2 && Grid.GetRow(lastTriggeredButton) > 1)
            {
                field[Grid.GetRow(lastTriggeredButton) - 2, Grid.GetColumn(lastTriggeredButton) + 2].Background =
                    Brushes.Chocolate;
            }
        }
    }
}