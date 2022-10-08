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
        SetField();

        if (blackEllipses.Contains(blackMans[Grid.GetRow(thatButton), Grid.GetColumn(thatButton)]))
        {
            if (!isTriggered)
            {
                TriggerButton(thatButton);
            }

            else
            {
                DeleteTrigger(thatButton);
            }
        }

        if (isTriggered)
        {
            MoveMan(thatButton);
        }
    }

    public static void IfCanBeatBlack()
    {
        //Check if we can beat something
        foreach (var blackEllipse in blackEllipses)
        {
            if (Grid.GetColumn(blackEllipse) < maxSizeOfField - 2 && Grid.GetRow(blackEllipse) < maxSizeOfField - 2 &&
                whiteEllipses.Contains(whiteMans[Grid.GetRow(blackEllipse) + 1, Grid.GetColumn(blackEllipse) + 1]) &&
                !whiteEllipses.Contains(whiteMans[Grid.GetRow(blackEllipse) + 2, Grid.GetColumn(blackEllipse) + 2]) &&
                !blackEllipses.Contains(blackMans[Grid.GetRow(blackEllipse) + 2, Grid.GetColumn(blackEllipse) + 2]))
            {
                canBeat = true;
            }

            if (Grid.GetColumn(blackEllipse) > 1 && Grid.GetRow(blackEllipse) < maxSizeOfField - 2 &&
                whiteEllipses.Contains(whiteMans[Grid.GetRow(blackEllipse) + 1, Grid.GetColumn(blackEllipse) - 1]) &&
                !whiteEllipses.Contains(whiteMans[Grid.GetRow(blackEllipse) + 2, Grid.GetColumn(blackEllipse) - 2]) &&
                !blackEllipses.Contains(blackMans[Grid.GetRow(blackEllipse) + 2, Grid.GetColumn(blackEllipse) - 2]))
            {
                canBeat = true;
            }

            IfCanBeatBlackKing(blackEllipse);
        }
    }

    //Check if King can beat something
    private static void IfCanBeatBlackKing(Ellipse blackEllipse)
    {
        if (blackMans[Grid.GetRow(blackEllipse), Grid.GetColumn(blackEllipse)].Fill == Brushes.Blue &&
            Grid.GetColumn(blackEllipse) > 1 && Grid.GetRow(blackEllipse) > 1 &&
            whiteEllipses.Contains(whiteMans[Grid.GetRow(blackEllipse) - 1, Grid.GetColumn(blackEllipse) - 1]) &&
            !whiteEllipses.Contains(whiteMans[Grid.GetRow(blackEllipse) - 2, Grid.GetColumn(blackEllipse) - 2]) &&
            !blackEllipses.Contains(blackMans[Grid.GetRow(blackEllipse) - 2, Grid.GetColumn(blackEllipse) - 2]))
        {
            canBeat = true;
        }

        if (blackMans[Grid.GetRow(blackEllipse), Grid.GetColumn(blackEllipse)].Fill == Brushes.Blue &&
            Grid.GetColumn(blackEllipse) < maxSizeOfField - 2 && Grid.GetRow(blackEllipse) > 1 &&
            whiteEllipses.Contains(whiteMans[Grid.GetRow(blackEllipse) - 1, Grid.GetColumn(blackEllipse) + 1]) &&
            !whiteEllipses.Contains(whiteMans[Grid.GetRow(blackEllipse) - 2, Grid.GetColumn(blackEllipse) + 2]) &&
            !blackEllipses.Contains(blackMans[Grid.GetRow(blackEllipse) - 2, Grid.GetColumn(blackEllipse) + 2]))
        {
            canBeat = true;
        }
    }

    private static void TriggerButton(Button thatButton)
    {
        lastTriggeredButton = thatButton;
        //Check right-up
        if (Grid.GetColumn(thatButton) < maxSizeOfField - 1 && Grid.GetRow(thatButton) < maxSizeOfField - 1 &&
            !whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) + 1]) &&
            !blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) + 1]) &&
            countOfBeatenMen == 0 && !canBeat)
        {
            thatButton.Background = Brushes.Gold;
            isTriggered = true;
            field[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) + 1].Background =
                Brushes.ForestGreen;
        }

        //Check right-up to beat
        if (Grid.GetColumn(thatButton) < maxSizeOfField - 2 && Grid.GetRow(thatButton) < maxSizeOfField - 2 &&
            whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) + 1]) &&
            !whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton) + 2, Grid.GetColumn(thatButton) + 2]) &&
            !blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) + 2, Grid.GetColumn(thatButton) + 2]))
        {
            thatButton.Background = Brushes.Gold;
            isTriggered = true;
            manToBeat1 = whiteMans[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) + 1];
            field[Grid.GetRow(thatButton) + 2, Grid.GetColumn(thatButton) + 2].Background =
                Brushes.ForestGreen;
        }

        //Check left-up
        if (Grid.GetColumn(thatButton) > 0 && Grid.GetRow(thatButton) < maxSizeOfField - 1 &&
            !whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) - 1]) &&
            !blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) - 1]) &&
            countOfBeatenMen == 0 && !canBeat)
        {
            thatButton.Background = Brushes.Gold;
            isTriggered = true;
            field[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) - 1].Background =
                Brushes.ForestGreen;
        }

        //Check left-up to beat
        if (Grid.GetColumn(thatButton) > 1 && Grid.GetRow(thatButton) < maxSizeOfField - 2 &&
            whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) - 1]) &&
            !whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton) + 2, Grid.GetColumn(thatButton) - 2]) &&
            !blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) + 2, Grid.GetColumn(thatButton) - 2]))
        {
            thatButton.Background = Brushes.Gold;
            isTriggered = true;
            manToBeat2 = whiteMans[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) - 1];
            field[Grid.GetRow(thatButton) + 2, Grid.GetColumn(thatButton) - 2].Background =
                Brushes.ForestGreen;
        }

        CheckForKing(thatButton);

        //Check if we can't beat more
        if (!isTriggered) countOfBeatenMen = 0;
    }

    private static void CheckForKing(Button thatButton)
    {
        //Check right-down
        if (blackMans[Grid.GetRow(thatButton), Grid.GetColumn(thatButton)].Fill == Brushes.Blue &&
            Grid.GetColumn(thatButton) < maxSizeOfField - 1 && Grid.GetRow(thatButton) > 0 &&
            !whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton) - 1, Grid.GetColumn(thatButton) + 1]) &&
            !blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) - 1, Grid.GetColumn(thatButton) + 1]) &&
            countOfBeatenMen == 0 && !canBeat)
        {
            thatButton.Background = Brushes.Gold;
            isTriggered = true;
            field[Grid.GetRow(thatButton) - 1, Grid.GetColumn(thatButton) + 1].Background =
                Brushes.ForestGreen;
        }

        //Check right-down to beat
        if (blackMans[Grid.GetRow(thatButton), Grid.GetColumn(thatButton)].Fill == Brushes.Blue &&
            Grid.GetColumn(thatButton) < maxSizeOfField - 2 && Grid.GetRow(thatButton) > 1 &&
            whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton) - 1, Grid.GetColumn(thatButton) + 1]) &&
            !whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton) - 2, Grid.GetColumn(thatButton) + 2]) &&
            !blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) - 2, Grid.GetColumn(thatButton) + 2]))
        {
            thatButton.Background = Brushes.Gold;
            isTriggered = true;
            manToBeat3 = whiteMans[Grid.GetRow(thatButton) - 1, Grid.GetColumn(thatButton) + 1];
            field[Grid.GetRow(thatButton) - 2, Grid.GetColumn(thatButton) + 2].Background =
                Brushes.ForestGreen;
        }

        //Check left-down
        if (blackMans[Grid.GetRow(thatButton), Grid.GetColumn(thatButton)].Fill == Brushes.Blue &&
            Grid.GetColumn(thatButton) > 0 && Grid.GetRow(thatButton) > 0 &&
            !whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton) - 1, Grid.GetColumn(thatButton) - 1]) &&
            !blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) - 1, Grid.GetColumn(thatButton) - 1]) &&
            countOfBeatenMen == 0 && !canBeat)
        {
            thatButton.Background = Brushes.Gold;
            isTriggered = true;
            field[Grid.GetRow(thatButton) - 1, Grid.GetColumn(thatButton) - 1].Background =
                Brushes.ForestGreen;
        }

        //Check left-down to beat
        if (blackMans[Grid.GetRow(thatButton), Grid.GetColumn(thatButton)].Fill == Brushes.Blue &&
            Grid.GetColumn(thatButton) > 1 && Grid.GetRow(thatButton) > 1 &&
            whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton) - 1, Grid.GetColumn(thatButton) - 1]) &&
            !whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton) - 2, Grid.GetColumn(thatButton) - 2]) &&
            !blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) - 2, Grid.GetColumn(thatButton) - 2]))
        {
            thatButton.Background = Brushes.Gold;
            isTriggered = true;
            manToBeat4 = whiteMans[Grid.GetRow(thatButton) - 1, Grid.GetColumn(thatButton) - 1];
            field[Grid.GetRow(thatButton) - 2, Grid.GetColumn(thatButton) - 2].Background =
                Brushes.ForestGreen;
        }
    }

    private static void DeleteTrigger(Button thatButton)
    {
        if (thatButton.Background.Equals(Brushes.Gold))
        {
            manToBeat1 = null;
            manToBeat2 = null;
            isTriggered = false;
            thatButton.Background = Brushes.Chocolate;
            if (Grid.GetColumn(thatButton) > 0 && Grid.GetRow(thatButton) < maxSizeOfField - 1)
            {
                field[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) - 1].Background =
                    Brushes.Chocolate;
            }

            if (Grid.GetColumn(thatButton) < maxSizeOfField - 1 && Grid.GetRow(thatButton) < maxSizeOfField - 1)
            {
                field[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) + 1].Background =
                    Brushes.Chocolate;
            }

            if (Grid.GetColumn(thatButton) > 0 && Grid.GetRow(thatButton) > 0)
            {
                field[Grid.GetRow(thatButton) - 1, Grid.GetColumn(thatButton) - 1].Background =
                    Brushes.Chocolate;
            }

            if (Grid.GetColumn(thatButton) < maxSizeOfField - 1 && Grid.GetRow(thatButton) > 0)
            {
                field[Grid.GetRow(thatButton) - 1, Grid.GetColumn(thatButton) + 1].Background =
                    Brushes.Chocolate;
            }

            if (Grid.GetColumn(thatButton) > 1 && Grid.GetRow(thatButton) < maxSizeOfField - 2)
            {
                field[Grid.GetRow(thatButton) + 2, Grid.GetColumn(thatButton) - 2].Background =
                    Brushes.Chocolate;
            }

            if (Grid.GetColumn(thatButton) < maxSizeOfField - 2 && Grid.GetRow(thatButton) < maxSizeOfField - 2)
            {
                field[Grid.GetRow(thatButton) + 2, Grid.GetColumn(thatButton) + 2].Background =
                    Brushes.Chocolate;
            }

            if (Grid.GetColumn(thatButton) > 1 && Grid.GetRow(thatButton) > 1)
            {
                field[Grid.GetRow(thatButton) - 2, Grid.GetColumn(thatButton) - 2].Background =
                    Brushes.Chocolate;
            }

            if (Grid.GetColumn(thatButton) < maxSizeOfField - 2 && Grid.GetRow(thatButton) > 1)
            {
                field[Grid.GetRow(thatButton) - 2, Grid.GetColumn(thatButton) + 2].Background =
                    Brushes.Chocolate;
            }
        }
    }

    private static void MoveMan(Button thatButton)
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
                //become king if we on the kingsrow
                if (Grid.GetRow(thatButton) == 7 &&
                    blackMans[Grid.GetRow(lastTriggeredButton), Grid.GetColumn(lastTriggeredButton)].Fill ==
                    Brushes.Black)
                {
                    becomeKing = true;
                    blackMans[Grid.GetRow(lastTriggeredButton), Grid.GetColumn(lastTriggeredButton)].Fill =
                        Brushes.Blue;
                }

                SetColorBack();

                BeatMan(thatButton);
            }

            if (countOfBeatenMen == 0)
            {
                whiteTurn = true;
                manToBeat1 = null;
                manToBeat2 = null;
                manToBeat3 = null;
                manToBeat4 = null;
            }

            becomeKing = false;
        }
    }

    private static void BeatMan(Button thatButton)
    {
        if (Grid.GetColumn(thatButton) == Grid.GetColumn(lastTriggeredButton) + 2)
        {
            if (manToBeat1 != null)
            {
                manToBeat1.Visibility = Visibility.Collapsed;
                whiteEllipses.Remove(manToBeat1);
            }

            if (manToBeat3 != null)
            {
                manToBeat3.Visibility = Visibility.Collapsed;
                whiteEllipses.Remove(manToBeat3);
            }

            countOfBeatenMen += 1;

            //Check if it becomes king
            if (!becomeKing)
            {
                //new turn if we can beat more
                BlackTurn(thatButton);
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
                manToBeat2.Visibility = Visibility.Collapsed;
                whiteEllipses.Remove(manToBeat2);
            }

            if (manToBeat4 != null)
            {
                manToBeat4.Visibility = Visibility.Collapsed;
                whiteEllipses.Remove(manToBeat4);
            }

            countOfBeatenMen += 1;

            //Check if it becomes king
            if (!becomeKing)
            {
                //new turn if we can beat more
                BlackTurn(thatButton);
            }
            else
            {
                countOfBeatenMen = 0;
            }
        }
    }
}