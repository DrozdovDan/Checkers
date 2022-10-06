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
using static Checkers.WhiteTurnClass;
using static Checkers.BlackTurnClass;

namespace Checkers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<Ellipse> whiteEllipses = new List<Ellipse>();
        public static List<Ellipse> blackEllipses = new List<Ellipse>();
        public static Button[,] field = new Button[8, 8];
        public static int countOfBeatenMen = 0;
        public static bool canBeat = false;
        public static bool becomeKing = false;

        public MainWindow()
        {
            InitializeComponent();
            //Set a list of white men
            whiteEllipses.Add(WhiteEllipse1);
            whiteEllipses.Add(WhiteEllipse2);
            whiteEllipses.Add(WhiteEllipse3);
            whiteEllipses.Add(WhiteEllipse4);
            whiteEllipses.Add(WhiteEllipse5);
            whiteEllipses.Add(WhiteEllipse6);
            whiteEllipses.Add(WhiteEllipse7);
            whiteEllipses.Add(WhiteEllipse8);
            whiteEllipses.Add(WhiteEllipse9);
            whiteEllipses.Add(WhiteEllipse10);
            whiteEllipses.Add(WhiteEllipse11);
            whiteEllipses.Add(WhiteEllipse12);
            //Set a list of black men
            blackEllipses.Add(BlackEllipse1);
            blackEllipses.Add(BlackEllipse2);
            blackEllipses.Add(BlackEllipse3);
            blackEllipses.Add(BlackEllipse4);
            blackEllipses.Add(BlackEllipse5);
            blackEllipses.Add(BlackEllipse6);
            blackEllipses.Add(BlackEllipse7);
            blackEllipses.Add(BlackEllipse8);
            blackEllipses.Add(BlackEllipse9);
            blackEllipses.Add(BlackEllipse10);
            blackEllipses.Add(BlackEllipse11);
            blackEllipses.Add(BlackEllipse12);

            //Set empty buttons to white squares
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        field[i, j] = new Button();
                    }
                }
            }

            //Set an array of buttons
            field[0, 1] = Button1;
            field[0, 3] = Button2;
            field[0, 5] = Button3;
            field[0, 7] = Button4;
            field[1, 0] = Button5;
            field[1, 2] = Button6;
            field[1, 4] = Button7;
            field[1, 6] = Button8;
            field[2, 1] = Button9;
            field[2, 3] = Button10;
            field[2, 5] = Button11;
            field[2, 7] = Button12;
            field[3, 0] = Button13;
            field[3, 2] = Button14;
            field[3, 4] = Button15;
            field[3, 6] = Button16;
            field[4, 1] = Button17;
            field[4, 3] = Button18;
            field[4, 5] = Button19;
            field[4, 7] = Button20;
            field[5, 0] = Button21;
            field[5, 2] = Button22;
            field[5, 4] = Button23;
            field[5, 6] = Button24;
            field[6, 1] = Button25;
            field[6, 3] = Button26;
            field[6, 5] = Button27;
            field[6, 7] = Button28;
            field[7, 0] = Button29;
            field[7, 2] = Button30;
            field[7, 4] = Button31;
            field[7, 6] = Button32;
        }

        public static bool isTriggered = false;
        public static Button? lastTriggeredButton;
        public static bool whiteTurn = false;
        public static Ellipse? manToBeat1 = null, manToBeat2 = null, manToBeat3 = null, manToBeat4 = null;

        private void NewClick(object? sender, RoutedEventArgs e)
        {
            canBeat = false;

            var thatButton = (Button)sender!;

            Ellipse[,] whiteMans = new Ellipse[9, 9];
            //Set empty ellipses to squares that does not have men
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    whiteMans[i, j] = new Ellipse();
                }
            }

            //Set an array of white men
            foreach (var ellipse in whiteEllipses)
            {
                whiteMans[Grid.GetRow(ellipse), Grid.GetColumn(ellipse)] = ellipse;
            }

            Ellipse[,] blackMans = new Ellipse[9, 9];
            //Set empty ellipses to squares that does not have men
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    blackMans[i, j] = new Ellipse();
                }
            }

            //Set an array of black men
            foreach (var ellipse in blackEllipses)
            {
                blackMans[Grid.GetRow(ellipse), Grid.GetColumn(ellipse)] = ellipse;
            }

            if (whiteTurn)
            {
                //Check if we can beat something
                foreach (var whiteEllipse in whiteEllipses)
                {
                    if (Grid.GetColumn(whiteEllipse) < 6 && Grid.GetRow(whiteEllipse) > 1 &&
                        blackEllipses.Contains(blackMans[Grid.GetRow(whiteEllipse) - 1,
                            Grid.GetColumn(whiteEllipse) + 1]) &&
                        !whiteEllipses.Contains(whiteMans[Grid.GetRow(whiteEllipse) - 2,
                            Grid.GetColumn(whiteEllipse) + 2]) &&
                        !blackEllipses.Contains(blackMans[Grid.GetRow(whiteEllipse) - 2,
                            Grid.GetColumn(whiteEllipse) + 2]))
                    {
                        canBeat = true;
                        break;
                    }

                    if (Grid.GetColumn(whiteEllipse) > 1 && Grid.GetRow(whiteEllipse) > 1 &&
                        blackEllipses.Contains(blackMans[Grid.GetRow(whiteEllipse) - 1,
                            Grid.GetColumn(whiteEllipse) - 1]) &&
                        !whiteEllipses.Contains(whiteMans[Grid.GetRow(whiteEllipse) - 2,
                            Grid.GetColumn(whiteEllipse) - 2]) &&
                        !blackEllipses.Contains(blackMans[Grid.GetRow(whiteEllipse) - 2,
                            Grid.GetColumn(whiteEllipse) - 2]))
                    {
                        canBeat = true;
                        break;
                    }

                    if (whiteMans[Grid.GetRow(whiteEllipse), Grid.GetColumn(whiteEllipse)].Fill == Brushes.Red &&
                        Grid.GetColumn(whiteEllipse) > 1 && Grid.GetRow(whiteEllipse) < 6 &&
                        blackEllipses.Contains(blackMans[Grid.GetRow(whiteEllipse) + 1,
                            Grid.GetColumn(whiteEllipse) - 1]) &&
                        !whiteEllipses.Contains(whiteMans[Grid.GetRow(whiteEllipse) + 2,
                            Grid.GetColumn(whiteEllipse) - 2]) &&
                        !blackEllipses.Contains(blackMans[Grid.GetRow(whiteEllipse) + 2,
                            Grid.GetColumn(whiteEllipse) - 2]))
                    {
                        canBeat = true;
                        break;
                    }

                    if (whiteMans[Grid.GetRow(whiteEllipse), Grid.GetColumn(whiteEllipse)].Fill == Brushes.Red &&
                        Grid.GetColumn(whiteEllipse) < 6 && Grid.GetRow(whiteEllipse) < 6 &&
                        blackEllipses.Contains(blackMans[Grid.GetRow(whiteEllipse) + 1,
                            Grid.GetColumn(whiteEllipse) + 1]) &&
                        !whiteEllipses.Contains(whiteMans[Grid.GetRow(whiteEllipse) + 2,
                            Grid.GetColumn(whiteEllipse) + 2]) &&
                        !blackEllipses.Contains(blackMans[Grid.GetRow(whiteEllipse) + 2,
                            Grid.GetColumn(whiteEllipse) + 2]))
                    {
                        canBeat = true;
                        break;
                    }
                }

                WhiteTurn(thatButton);
            }
            else
            {
                //Check if we can beat something
                foreach (var blackEllipse in blackEllipses)
                {
                    if (Grid.GetColumn(blackEllipse) < 6 && Grid.GetRow(blackEllipse) < 6 &&
                        whiteEllipses.Contains(whiteMans[Grid.GetRow(blackEllipse) + 1,
                            Grid.GetColumn(blackEllipse) + 1]) &&
                        !whiteEllipses.Contains(whiteMans[Grid.GetRow(blackEllipse) + 2,
                            Grid.GetColumn(blackEllipse) + 2]) &&
                        !blackEllipses.Contains(blackMans[Grid.GetRow(blackEllipse) + 2,
                            Grid.GetColumn(blackEllipse) + 2]))
                    {
                        canBeat = true;
                        break;
                    }

                    if (Grid.GetColumn(blackEllipse) > 1 && Grid.GetRow(blackEllipse) < 6 &&
                        whiteEllipses.Contains(whiteMans[Grid.GetRow(blackEllipse) + 1,
                            Grid.GetColumn(blackEllipse) - 1]) &&
                        !whiteEllipses.Contains(whiteMans[Grid.GetRow(blackEllipse) + 2,
                            Grid.GetColumn(blackEllipse) - 2]) &&
                        !blackEllipses.Contains(blackMans[Grid.GetRow(blackEllipse) + 2,
                            Grid.GetColumn(blackEllipse) - 2]))
                    {
                        canBeat = true;
                        break;
                    }

                    if (blackMans[Grid.GetRow(blackEllipse), Grid.GetColumn(blackEllipse)].Fill == Brushes.Blue &&
                        Grid.GetColumn(blackEllipse) > 1 && Grid.GetRow(blackEllipse) > 1 &&
                        whiteEllipses.Contains(whiteMans[Grid.GetRow(blackEllipse) - 1,
                            Grid.GetColumn(blackEllipse) - 1]) &&
                        !whiteEllipses.Contains(whiteMans[Grid.GetRow(blackEllipse) - 2,
                            Grid.GetColumn(blackEllipse) - 2]) &&
                        !blackEllipses.Contains(blackMans[Grid.GetRow(blackEllipse) - 2,
                            Grid.GetColumn(blackEllipse) - 2]))
                    {
                        canBeat = true;
                        break;
                    }

                    if (blackMans[Grid.GetRow(blackEllipse), Grid.GetColumn(blackEllipse)].Fill == Brushes.Blue &&
                        Grid.GetColumn(blackEllipse) < 6 && Grid.GetRow(blackEllipse) > 1 &&
                        whiteEllipses.Contains(whiteMans[Grid.GetRow(blackEllipse) - 1,
                            Grid.GetColumn(blackEllipse) + 1]) &&
                        !whiteEllipses.Contains(whiteMans[Grid.GetRow(blackEllipse) - 2,
                            Grid.GetColumn(blackEllipse) + 2]) &&
                        !blackEllipses.Contains(blackMans[Grid.GetRow(blackEllipse) - 2,
                            Grid.GetColumn(blackEllipse) + 2]))
                    {
                        canBeat = true;
                        break;
                    }
                }

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
    }
}