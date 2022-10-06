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
using static Checkers.MainWindow;

namespace Checkers;

public static class WhiteTurnClass
{
    public static void WhiteTurn(Button thatButton)
    {
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

        if (whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton), Grid.GetColumn(thatButton)]))
        {
            if (!isTriggered)
            {
                lastTriggeredButton = thatButton;
                //Check right-up
                if (Grid.GetColumn(thatButton) < 7 && Grid.GetRow(thatButton) > 0 &&
                    !whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton) - 1,
                        Grid.GetColumn(thatButton) + 1]) &&
                    !blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) - 1,
                        Grid.GetColumn(thatButton) + 1]) && countOfBeatenMen == 0 && !canBeat)
                {
                    thatButton.Background = Brushes.Gold;
                    isTriggered = true;
                    field[Grid.GetRow(thatButton) - 1, Grid.GetColumn(thatButton) + 1].Background =
                        Brushes.ForestGreen;
                }

                //Check right-up to beat
                if (Grid.GetColumn(thatButton) < 6 && Grid.GetRow(thatButton) > 1 &&
                    blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) - 1,
                        Grid.GetColumn(thatButton) + 1]) &&
                    !whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton) - 2,
                        Grid.GetColumn(thatButton) + 2]) &&
                    !blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) - 2,
                        Grid.GetColumn(thatButton) + 2]))
                {
                    thatButton.Background = Brushes.Gold;
                    isTriggered = true;
                    manToBeat1 = blackMans[Grid.GetRow(thatButton) - 1, Grid.GetColumn(thatButton) + 1];
                    field[Grid.GetRow(thatButton) - 2, Grid.GetColumn(thatButton) + 2].Background =
                        Brushes.ForestGreen;
                }

                //Check right-down
                if (whiteMans[Grid.GetRow(thatButton), Grid.GetColumn(thatButton)].Fill == Brushes.Red &&
                    Grid.GetColumn(thatButton) < 7 && Grid.GetRow(thatButton) < 7 &&
                    !whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton) + 1,
                        Grid.GetColumn(thatButton) + 1]) &&
                    !blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) + 1,
                        Grid.GetColumn(thatButton) + 1]) && countOfBeatenMen == 0 && !canBeat)
                {
                    thatButton.Background = Brushes.Gold;
                    isTriggered = true;
                    field[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) + 1].Background =
                        Brushes.ForestGreen;
                }

                //Check right-down to beat
                if (whiteMans[Grid.GetRow(thatButton), Grid.GetColumn(thatButton)].Fill == Brushes.Red &&
                    Grid.GetColumn(thatButton) < 6 && Grid.GetRow(thatButton) < 6 &&
                    blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) + 1,
                        Grid.GetColumn(thatButton) + 1]) &&
                    !whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton) + 2,
                        Grid.GetColumn(thatButton) + 2]) &&
                    !blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) + 2,
                        Grid.GetColumn(thatButton) + 2]))
                {
                    thatButton.Background = Brushes.Gold;
                    isTriggered = true;
                    manToBeat3 = blackMans[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) + 1];
                    field[Grid.GetRow(thatButton) + 2, Grid.GetColumn(thatButton) + 2].Background =
                        Brushes.ForestGreen;
                }

                //Check left-up
                if (Grid.GetColumn(thatButton) > 0 && Grid.GetRow(thatButton) > 0 &&
                    !whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton) - 1,
                        Grid.GetColumn(thatButton) - 1]) &&
                    !blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) - 1,
                        Grid.GetColumn(thatButton) - 1]) && countOfBeatenMen == 0 && !canBeat)
                {
                    thatButton.Background = Brushes.Gold;
                    isTriggered = true;
                    field[Grid.GetRow(thatButton) - 1, Grid.GetColumn(thatButton) - 1].Background =
                        Brushes.ForestGreen;
                }

                //Check left-up to beat
                if (Grid.GetColumn(thatButton) > 1 && Grid.GetRow(thatButton) > 1 &&
                    blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) - 1,
                        Grid.GetColumn(thatButton) - 1]) &&
                    !whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton) - 2,
                        Grid.GetColumn(thatButton) - 2]) &&
                    !blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) - 2,
                        Grid.GetColumn(thatButton) - 2]))
                {
                    thatButton.Background = Brushes.Gold;
                    isTriggered = true;
                    manToBeat2 = blackMans[Grid.GetRow(thatButton) - 1, Grid.GetColumn(thatButton) - 1];
                    field[Grid.GetRow(thatButton) - 2, Grid.GetColumn(thatButton) - 2].Background =
                        Brushes.ForestGreen;
                }

                //Check left-down
                if (whiteMans[Grid.GetRow(thatButton), Grid.GetColumn(thatButton)].Fill == Brushes.Red &&
                    Grid.GetColumn(thatButton) > 0 && Grid.GetRow(thatButton) < 7 &&
                    !whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton) + 1,
                        Grid.GetColumn(thatButton) - 1]) &&
                    !blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) + 1,
                        Grid.GetColumn(thatButton) - 1]) && countOfBeatenMen == 0 && !canBeat)
                {
                    thatButton.Background = Brushes.Gold;
                    isTriggered = true;
                    field[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) - 1].Background =
                        Brushes.ForestGreen;
                }

                //Check left-down to beat
                if (whiteMans[Grid.GetRow(thatButton), Grid.GetColumn(thatButton)].Fill == Brushes.Red &&
                    Grid.GetColumn(thatButton) > 1 && Grid.GetRow(thatButton) < 6 &&
                    blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) + 1,
                        Grid.GetColumn(thatButton) - 1]) &&
                    !whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton) + 2,
                        Grid.GetColumn(thatButton) - 2]) &&
                    !blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) + 2,
                        Grid.GetColumn(thatButton) - 2]))
                {
                    thatButton.Background = Brushes.Gold;
                    isTriggered = true;
                    manToBeat4 = blackMans[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) - 1];
                    field[Grid.GetRow(thatButton) + 2, Grid.GetColumn(thatButton) - 2].Background =
                        Brushes.ForestGreen;
                }

                //Check if we can't beat more
                if (!isTriggered) countOfBeatenMen = 0;
            }

            else
            {
                //Delete trigger
                if (thatButton.Background.Equals(Brushes.Gold) && countOfBeatenMen == 0)
                {
                    isTriggered = false;
                    thatButton.Background = Brushes.Maroon;
                    manToBeat1 = null;
                    manToBeat2 = null;
                    if (Grid.GetColumn(thatButton) > 0 && Grid.GetRow(thatButton) > 0)
                    {
                        field[Grid.GetRow(thatButton) - 1, Grid.GetColumn(thatButton) - 1].Background =
                            Brushes.Maroon;
                    }

                    if (Grid.GetColumn(thatButton) < 7 && Grid.GetRow(thatButton) > 0)
                    {
                        field[Grid.GetRow(thatButton) - 1, Grid.GetColumn(thatButton) + 1].Background =
                            Brushes.Maroon;
                    }

                    if (Grid.GetColumn(lastTriggeredButton) > 0 && Grid.GetRow(lastTriggeredButton) < 7)
                    {
                        field[Grid.GetRow(lastTriggeredButton) + 1, Grid.GetColumn(lastTriggeredButton) - 1]
                            .Background = Brushes.Maroon;
                    }

                    if (Grid.GetColumn(lastTriggeredButton) < 7 && Grid.GetRow(lastTriggeredButton) < 7)
                    {
                        field[Grid.GetRow(lastTriggeredButton) + 1, Grid.GetColumn(lastTriggeredButton) + 1]
                            .Background = Brushes.Maroon;
                    }

                    if (Grid.GetColumn(thatButton) > 1 && Grid.GetRow(thatButton) > 1)
                    {
                        field[Grid.GetRow(thatButton) - 2, Grid.GetColumn(thatButton) - 2].Background =
                            Brushes.Maroon;
                    }

                    if (Grid.GetColumn(thatButton) < 6 && Grid.GetRow(thatButton) > 1)
                    {
                        field[Grid.GetRow(thatButton) - 2, Grid.GetColumn(thatButton) + 2].Background =
                            Brushes.Maroon;
                    }

                    if (Grid.GetColumn(thatButton) > 1 && Grid.GetRow(thatButton) < 6)
                    {
                        field[Grid.GetRow(thatButton) + 2, Grid.GetColumn(thatButton) - 2].Background =
                            Brushes.Maroon;
                    }

                    if (Grid.GetColumn(thatButton) < 6 && Grid.GetRow(thatButton) < 6)
                    {
                        field[Grid.GetRow(thatButton) + 2, Grid.GetColumn(thatButton) + 2].Background =
                            Brushes.Maroon;
                    }
                }
            }
        }

        //Move a man
        if (isTriggered)
        {
            if (thatButton.Background.Equals(Brushes.ForestGreen))
            {
                isTriggered = false;
                if (lastTriggeredButton != null)
                {
                    Grid.SetColumn(whiteMans[Grid.GetRow(lastTriggeredButton), Grid.GetColumn(lastTriggeredButton)],
                        Grid.GetColumn(thatButton));
                    Grid.SetRow(whiteMans[Grid.GetRow(lastTriggeredButton), Grid.GetColumn(lastTriggeredButton)],
                        Grid.GetRow(thatButton));
                    //become king if we on the kingsrow
                    if (Grid.GetRow(thatButton) == 0 &&
                        whiteMans[Grid.GetRow(lastTriggeredButton), Grid.GetColumn(lastTriggeredButton)].Fill ==
                        Brushes.White)
                    {
                        becomeKing = true;
                        whiteMans[Grid.GetRow(lastTriggeredButton), Grid.GetColumn(lastTriggeredButton)].Fill =
                            Brushes.Red;
                    }

                    lastTriggeredButton.Background = Brushes.Maroon;
                    if (Grid.GetColumn(lastTriggeredButton) > 0 && Grid.GetRow(lastTriggeredButton) > 0)
                    {
                        field[Grid.GetRow(lastTriggeredButton) - 1, Grid.GetColumn(lastTriggeredButton) - 1]
                            .Background = Brushes.Maroon;
                    }

                    if (Grid.GetColumn(lastTriggeredButton) < 7 && Grid.GetRow(lastTriggeredButton) > 0)
                    {
                        field[Grid.GetRow(lastTriggeredButton) - 1, Grid.GetColumn(lastTriggeredButton) + 1]
                            .Background = Brushes.Maroon;
                    }

                    if (Grid.GetColumn(lastTriggeredButton) > 0 && Grid.GetRow(lastTriggeredButton) < 7)
                    {
                        field[Grid.GetRow(lastTriggeredButton) + 1, Grid.GetColumn(lastTriggeredButton) - 1]
                            .Background = Brushes.Maroon;
                    }

                    if (Grid.GetColumn(lastTriggeredButton) < 7 && Grid.GetRow(lastTriggeredButton) < 7)
                    {
                        field[Grid.GetRow(lastTriggeredButton) + 1, Grid.GetColumn(lastTriggeredButton) + 1]
                            .Background = Brushes.Maroon;
                    }

                    if (Grid.GetColumn(lastTriggeredButton) > 1 && Grid.GetRow(lastTriggeredButton) > 1)
                    {
                        field[Grid.GetRow(lastTriggeredButton) - 2, Grid.GetColumn(lastTriggeredButton) - 2]
                            .Background = Brushes.Maroon;
                    }

                    if (Grid.GetColumn(lastTriggeredButton) < 6 && Grid.GetRow(lastTriggeredButton) > 1)
                    {
                        field[Grid.GetRow(lastTriggeredButton) - 2, Grid.GetColumn(lastTriggeredButton) + 2]
                            .Background = Brushes.Maroon;
                    }

                    if (Grid.GetColumn(lastTriggeredButton) > 1 && Grid.GetRow(lastTriggeredButton) < 6)
                    {
                        field[Grid.GetRow(lastTriggeredButton) + 2, Grid.GetColumn(lastTriggeredButton) - 2]
                                .Background =
                            Brushes.Maroon;
                    }

                    if (Grid.GetColumn(lastTriggeredButton) < 6 && Grid.GetRow(lastTriggeredButton) < 6)
                    {
                        field[Grid.GetRow(lastTriggeredButton) + 2, Grid.GetColumn(lastTriggeredButton) + 2]
                                .Background =
                            Brushes.Maroon;
                    }

                    //Beat a man
                    if (Grid.GetColumn(thatButton) == Grid.GetColumn(lastTriggeredButton) + 2)
                    {
                        if (manToBeat1 != null)
                        {
                            Grid.SetRow(manToBeat1, 8);
                            manToBeat1.Visibility = Visibility.Collapsed;
                            blackEllipses.Remove(manToBeat1);
                        }

                        if (manToBeat3 != null)
                        {
                            Grid.SetRow(manToBeat3, 8);
                            manToBeat3.Visibility = Visibility.Collapsed;
                            blackEllipses.Remove(manToBeat3);
                        }

                        countOfBeatenMen += 1;
                        //Check if it becomes king
                        if (!becomeKing)
                        {
                            //new turn if we can beat more
                            WhiteTurn(thatButton);
                        }
                        else
                        {
                            countOfBeatenMen = 0;
                        }
                    }

                    if (Grid.GetColumn(thatButton) == Grid.GetColumn(lastTriggeredButton) - 2)
                    {
                        if (manToBeat2 != null)
                        {
                            Grid.SetRow(manToBeat2, 8);
                            manToBeat2.Visibility = Visibility.Collapsed;
                            blackEllipses.Remove(manToBeat2);
                        }

                        if (manToBeat4 != null)
                        {
                            Grid.SetRow(manToBeat4, 8);
                            manToBeat4.Visibility = Visibility.Collapsed;
                            blackEllipses.Remove(manToBeat4);
                        }

                        countOfBeatenMen += 1;
                        //Check if it becomes king
                        if (!becomeKing)
                        {
                            //new turn if we can beat more
                            WhiteTurn(thatButton);
                        }
                        else
                        {
                            countOfBeatenMen = 0;
                        }
                    }
                }

                if (countOfBeatenMen == 0) whiteTurn = false;
                if (countOfBeatenMen == 0) manToBeat1 = null;
                if (countOfBeatenMen == 0) manToBeat2 = null;
                if (countOfBeatenMen == 0) manToBeat3 = null;
                if (countOfBeatenMen == 0) manToBeat4 = null;
                becomeKing = false;
            }
        }
    }
}