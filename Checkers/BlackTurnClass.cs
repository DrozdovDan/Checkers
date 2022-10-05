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

public class BlackTurnClass
{
    public static void BlackTurn(Button thatButton)
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

        if (blackEllipses.Contains(blackMans[Grid.GetRow(thatButton), Grid.GetColumn(thatButton)]))
        {
            if (!isTriggered)
            {
                lastTriggeredButton = thatButton;
                //Check right-up
                if (Grid.GetColumn(thatButton) < 7 && Grid.GetRow(thatButton) < 7 &&
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

                //Check right-up to beat
                if (Grid.GetColumn(thatButton) < 6 && Grid.GetRow(thatButton) < 6 &&
                    whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton) + 1,
                        Grid.GetColumn(thatButton) + 1]) &&
                    !whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton) + 2,
                        Grid.GetColumn(thatButton) + 2]) &&
                    !blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) + 2,
                        Grid.GetColumn(thatButton) + 2]))
                {
                    thatButton.Background = Brushes.Gold;
                    isTriggered = true;
                    manToBeat1 = whiteMans[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) + 1];
                    field[Grid.GetRow(thatButton) + 2, Grid.GetColumn(thatButton) + 2].Background =
                        Brushes.ForestGreen;
                }

                //Check left-up
                if (Grid.GetColumn(thatButton) > 0 && Grid.GetRow(thatButton) < 7 &&
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

                //Check left-up to beat
                if (Grid.GetColumn(thatButton) > 1 && Grid.GetRow(thatButton) < 6 &&
                    whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton) + 1,
                        Grid.GetColumn(thatButton) - 1]) &&
                    !whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton) + 2,
                        Grid.GetColumn(thatButton) - 2]) &&
                    !blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) + 2,
                        Grid.GetColumn(thatButton) - 2]))
                {
                    thatButton.Background = Brushes.Gold;
                    isTriggered = true;
                    manToBeat2 = whiteMans[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) - 1];
                    field[Grid.GetRow(thatButton) + 2, Grid.GetColumn(thatButton) - 2].Background =
                        Brushes.ForestGreen;
                }

                //Check if we can't beat more
                if (!isTriggered) countOfBeatenMen = 0;
            }

            else
            {
                //Delete trigger
                if (thatButton.Background.Equals(Brushes.Gold))
                {
                    manToBeat1 = null;
                    manToBeat2 = null;
                    isTriggered = false;
                    thatButton.Background = Brushes.Maroon;
                    if (Grid.GetColumn(thatButton) > 0 && Grid.GetRow(thatButton) < 7)
                    {
                        field[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) - 1].Background =
                            Brushes.Maroon;
                    }

                    if (Grid.GetColumn(thatButton) < 7 && Grid.GetRow(thatButton) < 7)
                    {
                        field[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) + 1].Background =
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
                    Grid.SetColumn(blackMans[Grid.GetRow(lastTriggeredButton), Grid.GetColumn(lastTriggeredButton)],
                        Grid.GetColumn(thatButton));
                    Grid.SetRow(blackMans[Grid.GetRow(lastTriggeredButton), Grid.GetColumn(lastTriggeredButton)],
                        Grid.GetRow(thatButton));
                    lastTriggeredButton.Background = Brushes.Maroon;
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

                    if (Grid.GetColumn(lastTriggeredButton) > 1 && Grid.GetRow(lastTriggeredButton) < 6)
                    {
                        field[Grid.GetRow(lastTriggeredButton) + 2, Grid.GetColumn(lastTriggeredButton) - 2]
                            .Background = Brushes.Maroon;
                    }

                    if (Grid.GetColumn(lastTriggeredButton) < 6 && Grid.GetRow(lastTriggeredButton) < 6)
                    {
                        field[Grid.GetRow(lastTriggeredButton) + 2, Grid.GetColumn(lastTriggeredButton) + 2]
                            .Background = Brushes.Maroon;
                    }

                    //Beat a man
                    if (Grid.GetColumn(thatButton) == Grid.GetColumn(lastTriggeredButton) + 2)
                    {
                        if (manToBeat1 != null)
                        {
                            Grid.SetRow(manToBeat1, 8);
                            manToBeat1.Visibility = Visibility.Collapsed;
                            whiteEllipses.Remove(manToBeat1);
                            countOfBeatenMen += 1;
                        }

                        BlackTurn(thatButton);
                    }

                    if (Grid.GetColumn(thatButton) == Grid.GetColumn(lastTriggeredButton) - 2)
                    {
                        if (manToBeat2 != null)
                        {
                            Grid.SetRow(manToBeat2, 8);
                            manToBeat2.Visibility = Visibility.Collapsed;
                            whiteEllipses.Remove(manToBeat2);
                            countOfBeatenMen += 1;
                        }

                        BlackTurn(thatButton);
                    }
                }

                if (countOfBeatenMen == 0) whiteTurn = true;
                if (countOfBeatenMen == 0) manToBeat1 = null;
                if (countOfBeatenMen == 0) manToBeat2 = null;
            }
        }
    }
}