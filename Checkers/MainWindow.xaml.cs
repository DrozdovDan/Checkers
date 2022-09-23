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

namespace Checkers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        bool isTriggered = false;
        Button lastTriggeredButton;
        private bool whiteTurn = true;
        private void NewClick(object sender, RoutedEventArgs e)
        {
            var thatButton = (Button)sender;
            Ellipse[,] whiteMans = new Ellipse[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    whiteMans[i, j] = new Ellipse();
                }
            }

            List<Ellipse> whiteEllipses = new List<Ellipse>{WhiteEllipse1, WhiteEllipse2, WhiteEllipse3, WhiteEllipse4, 
                WhiteEllipse5, WhiteEllipse6, WhiteEllipse7, WhiteEllipse8, WhiteEllipse9, WhiteEllipse10, WhiteEllipse11, WhiteEllipse12};
            foreach (var ellipse in whiteEllipses)
            {
                whiteMans[Grid.GetRow(ellipse), Grid.GetColumn(ellipse)] = ellipse;
            }
            
            Ellipse[,] blackMans = new Ellipse[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    blackMans[i, j] = new Ellipse();
                }
            }
            List<Ellipse> blackEllipses = new List<Ellipse>{BlackEllipse1, BlackEllipse2, BlackEllipse3, BlackEllipse4, 
                BlackEllipse5, BlackEllipse6, BlackEllipse7, BlackEllipse8, BlackEllipse9, BlackEllipse10, BlackEllipse11, BlackEllipse12};
            foreach (var ellipse in blackEllipses)
            {
                blackMans[Grid.GetRow(ellipse), Grid.GetColumn(ellipse)] = ellipse;
            }
            Button[,] field = new Button[8, 8];
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
            if (whiteTurn)
            {
                if (whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton), Grid.GetColumn(thatButton)]))
                {
                    if (!isTriggered)
                    {
                        lastTriggeredButton = thatButton;
                        //Check right-up
                        if (Grid.GetColumn(thatButton) < 7 && Grid.GetRow(thatButton) > 0 &&
                            !whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton) - 1, Grid.GetColumn(thatButton) + 1]) &&
                            !blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) - 1, Grid.GetColumn(thatButton) + 1]))
                        {
                            thatButton.Background = Brushes.Gold;
                            isTriggered = true;
                            field[Grid.GetRow(thatButton) - 1, Grid.GetColumn(thatButton) + 1].Background = Brushes.ForestGreen;
                        }

                        //Check left-up
                        if (Grid.GetColumn(thatButton) > 0 && Grid.GetRow(thatButton) > 0 &&
                            !whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton) - 1, Grid.GetColumn(thatButton) - 1]) &&
                            !blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) - 1, Grid.GetColumn(thatButton) - 1]))
                        {
                            thatButton.Background = Brushes.Gold;
                            isTriggered = true;
                            field[Grid.GetRow(thatButton) - 1, Grid.GetColumn(thatButton) - 1].Background = Brushes.ForestGreen;
                        }
                    }

                    else
                    {
                        //Delete trigger
                        if (thatButton.Background.Equals(Brushes.Gold))
                        {
                            isTriggered = false;
                            thatButton.Background = Brushes.Maroon;
                            if (Grid.GetColumn(thatButton) > 0 && Grid.GetRow(thatButton) > 0)
                            {
                                field[Grid.GetRow(thatButton) - 1, Grid.GetColumn(thatButton) - 1].Background = Brushes.Maroon;
                            }
                            if (Grid.GetColumn(thatButton) < 7 && Grid.GetRow(thatButton) > 0)
                            {
                                field[Grid.GetRow(thatButton) - 1, Grid.GetColumn(thatButton) + 1].Background = Brushes.Maroon;
                            }
                        }
                    }
                }
                //move it
                if (thatButton.Background.Equals(Brushes.ForestGreen)) 
                {
                    isTriggered = false;
                    Grid.SetColumn(whiteMans[Grid.GetRow(lastTriggeredButton), Grid.GetColumn(lastTriggeredButton)], Grid.GetColumn(thatButton));
                    Grid.SetRow(whiteMans[Grid.GetRow(lastTriggeredButton), Grid.GetColumn(lastTriggeredButton)], Grid.GetRow(thatButton));
                    lastTriggeredButton.Background = Brushes.Maroon;
                    if (Grid.GetColumn(lastTriggeredButton) > 0 && Grid.GetRow(lastTriggeredButton) > 0)
                    {
                        field[Grid.GetRow(lastTriggeredButton) - 1, Grid.GetColumn(lastTriggeredButton) - 1].Background = Brushes.Maroon;
                    }
                    if (Grid.GetColumn(lastTriggeredButton) < 7 && Grid.GetRow(lastTriggeredButton) > 0)
                    {
                        field[Grid.GetRow(lastTriggeredButton) - 1, Grid.GetColumn(lastTriggeredButton) + 1].Background = Brushes.Maroon;
                    }

                    whiteTurn = false;
                }
            }
            else
            {
                if (blackEllipses.Contains(blackMans[Grid.GetRow(thatButton), Grid.GetColumn(thatButton)]))
                {
                    if (!isTriggered)
                    {
                        lastTriggeredButton = thatButton;
                        //Check right-up
                        if (Grid.GetColumn(thatButton) < 7 && Grid.GetRow(thatButton) < 7 &&
                            !whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) + 1]) &&
                            !blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) + 1]))
                        {
                            thatButton.Background = Brushes.Gold;
                            isTriggered = true;
                            field[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) + 1].Background = Brushes.ForestGreen;
                        }

                        //Check left-up
                        if (Grid.GetColumn(thatButton) > 0 && Grid.GetRow(thatButton) < 7 &&
                            !whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) - 1]) &&
                            !blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) - 1]))
                        {
                            thatButton.Background = Brushes.Gold;
                            isTriggered = true;
                            field[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) - 1].Background = Brushes.ForestGreen;
                        }
                    }

                    else
                    {
                        //Delete trigger
                        if (thatButton.Background.Equals(Brushes.Gold))
                        {
                            isTriggered = false;
                            thatButton.Background = Brushes.Maroon;
                            if (Grid.GetColumn(thatButton) > 0 && Grid.GetRow(thatButton) < 7)
                            {
                                field[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) - 1].Background = Brushes.Maroon;
                            }
                            if (Grid.GetColumn(thatButton) < 7 && Grid.GetRow(thatButton) < 7)
                            {
                                field[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) + 1].Background = Brushes.Maroon;
                            }
                        }
                    }
                }
                //move it
                if (thatButton.Background.Equals(Brushes.ForestGreen)) 
                {
                    isTriggered = false;
                    Grid.SetColumn(blackMans[Grid.GetRow(lastTriggeredButton), Grid.GetColumn(lastTriggeredButton)], Grid.GetColumn(thatButton));
                    Grid.SetRow(blackMans[Grid.GetRow(lastTriggeredButton), Grid.GetColumn(lastTriggeredButton)], Grid.GetRow(thatButton));
                    lastTriggeredButton.Background = Brushes.Maroon;
                    if (Grid.GetColumn(lastTriggeredButton) > 0 && Grid.GetRow(lastTriggeredButton) < 7)
                    {
                        field[Grid.GetRow(lastTriggeredButton) + 1, Grid.GetColumn(lastTriggeredButton) - 1].Background = Brushes.Maroon;
                    }
                    if (Grid.GetColumn(lastTriggeredButton) < 7 && Grid.GetRow(lastTriggeredButton) < 7)
                    {
                        field[Grid.GetRow(lastTriggeredButton) + 1, Grid.GetColumn(lastTriggeredButton) + 1].Background = Brushes.Maroon;
                    }

                    whiteTurn = true;
                }
            }
        }
    }
}
