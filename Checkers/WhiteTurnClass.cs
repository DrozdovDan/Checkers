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
        SetField();

        if (whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton), Grid.GetColumn(thatButton)]))
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

    public static void IfCanBeatWhite()
    {
        //Check if we can beat something
        foreach (var whiteEllipse in whiteEllipses)
        {
            if (Grid.GetColumn(whiteEllipse) < maxSizeOfField - 2 && Grid.GetRow(whiteEllipse) > 1 &&
                blackEllipses.Contains(blackMans[Grid.GetRow(whiteEllipse) - 1, Grid.GetColumn(whiteEllipse) + 1]) &&
                !whiteEllipses.Contains(whiteMans[Grid.GetRow(whiteEllipse) - 2, Grid.GetColumn(whiteEllipse) + 2]) &&
                !blackEllipses.Contains(blackMans[Grid.GetRow(whiteEllipse) - 2, Grid.GetColumn(whiteEllipse) + 2]))
            {
                canBeat = true;
            }

            if (Grid.GetColumn(whiteEllipse) > 1 && Grid.GetRow(whiteEllipse) > 1 &&
                blackEllipses.Contains(blackMans[Grid.GetRow(whiteEllipse) - 1, Grid.GetColumn(whiteEllipse) - 1]) &&
                !whiteEllipses.Contains(whiteMans[Grid.GetRow(whiteEllipse) - 2, Grid.GetColumn(whiteEllipse) - 2]) &&
                !blackEllipses.Contains(blackMans[Grid.GetRow(whiteEllipse) - 2, Grid.GetColumn(whiteEllipse) - 2]))
            {
                canBeat = true;
            }

            IfCanMoveWhite(whiteEllipse);
            
            IfCanBeatWhiteKing(whiteEllipse);
        }
    }

    //Check if King can beat something
    private static void IfCanBeatWhiteKing(Ellipse whiteEllipse)
    {
        if (whiteMans[Grid.GetRow(whiteEllipse), Grid.GetColumn(whiteEllipse)].Fill == Brushes.Red &&
            Grid.GetColumn(whiteEllipse) > 1 && Grid.GetRow(whiteEllipse) < maxSizeOfField - 2 &&
            blackEllipses.Contains(blackMans[Grid.GetRow(whiteEllipse) + 1, Grid.GetColumn(whiteEllipse) - 1]) &&
            !whiteEllipses.Contains(whiteMans[Grid.GetRow(whiteEllipse) + 2, Grid.GetColumn(whiteEllipse) - 2]) &&
            !blackEllipses.Contains(blackMans[Grid.GetRow(whiteEllipse) + 2, Grid.GetColumn(whiteEllipse) - 2]))
        {
            canBeat = true;
        }

        if (whiteMans[Grid.GetRow(whiteEllipse), Grid.GetColumn(whiteEllipse)].Fill == Brushes.Red &&
            Grid.GetColumn(whiteEllipse) < maxSizeOfField - 2 && Grid.GetRow(whiteEllipse) < maxSizeOfField - 2 &&
            blackEllipses.Contains(blackMans[Grid.GetRow(whiteEllipse) + 1, Grid.GetColumn(whiteEllipse) + 1]) &&
            !whiteEllipses.Contains(whiteMans[Grid.GetRow(whiteEllipse) + 2, Grid.GetColumn(whiteEllipse) + 2]) &&
            !blackEllipses.Contains(blackMans[Grid.GetRow(whiteEllipse) + 2, Grid.GetColumn(whiteEllipse) + 2]))
        {
            canBeat = true;
        }
    }

    private static void IfCanMoveWhite(Ellipse whiteEllipse)
    {
        if (Grid.GetColumn(whiteEllipse) < maxSizeOfField - 1 && Grid.GetRow(whiteEllipse) > 0 &&
            !whiteEllipses.Contains(whiteMans[Grid.GetRow(whiteEllipse) - 1, Grid.GetColumn(whiteEllipse) + 1]) &&
            !blackEllipses.Contains(blackMans[Grid.GetRow(whiteEllipse) - 1, Grid.GetColumn(whiteEllipse) + 1]))
        {
            canMove = true;
        }

        if (Grid.GetColumn(whiteEllipse) > 0 && Grid.GetRow(whiteEllipse) > 0 &&
            !whiteEllipses.Contains(whiteMans[Grid.GetRow(whiteEllipse) - 1, Grid.GetColumn(whiteEllipse) - 1]) &&
            !blackEllipses.Contains(blackMans[Grid.GetRow(whiteEllipse) - 1, Grid.GetColumn(whiteEllipse) - 1]))
        {
            canMove = true;
        }
        
        IfCanMoveWhiteKing(whiteEllipse);
    }
    
    //Check if King can move something
    private static void IfCanMoveWhiteKing(Ellipse whiteEllipse)
    {
        if (whiteMans[Grid.GetRow(whiteEllipse), Grid.GetColumn(whiteEllipse)].Fill == Brushes.Red &&
            Grid.GetColumn(whiteEllipse) > 0 && Grid.GetRow(whiteEllipse) < maxSizeOfField - 1 &&
            !whiteEllipses.Contains(whiteMans[Grid.GetRow(whiteEllipse) + 1, Grid.GetColumn(whiteEllipse) - 1]) &&
            !blackEllipses.Contains(blackMans[Grid.GetRow(whiteEllipse) + 1, Grid.GetColumn(whiteEllipse) - 1]))
        {
            canMove = true;
        }

        if (whiteMans[Grid.GetRow(whiteEllipse), Grid.GetColumn(whiteEllipse)].Fill == Brushes.Red &&
            Grid.GetColumn(whiteEllipse) < maxSizeOfField - 1 && Grid.GetRow(whiteEllipse) < maxSizeOfField - 1 &&
            !whiteEllipses.Contains(whiteMans[Grid.GetRow(whiteEllipse) + 1, Grid.GetColumn(whiteEllipse) + 1]) &&
            !blackEllipses.Contains(blackMans[Grid.GetRow(whiteEllipse) + 1, Grid.GetColumn(whiteEllipse) + 1]))
        {
            canMove = true;
        }
    }
    private static void TriggerButton(Button thatButton)
    {
        lastTriggeredButton = thatButton;
        //Check right-up
        if (Grid.GetColumn(thatButton) < maxSizeOfField - 1 && Grid.GetRow(thatButton) > 0 &&
            !whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton) - 1, Grid.GetColumn(thatButton) + 1]) &&
            !blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) - 1, Grid.GetColumn(thatButton) + 1]) &&
            countOfBeatenMen == 0 && !canBeat)
        {
            thatButton.Background = Brushes.Gold;
            isTriggered = true;
            field[Grid.GetRow(thatButton) - 1, Grid.GetColumn(thatButton) + 1].Background =
                Brushes.ForestGreen;
        }

        //Check right-up to beat
        if (Grid.GetColumn(thatButton) < maxSizeOfField - 2 && Grid.GetRow(thatButton) > 1 &&
            blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) - 1, Grid.GetColumn(thatButton) + 1]) &&
            !whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton) - 2, Grid.GetColumn(thatButton) + 2]) &&
            !blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) - 2, Grid.GetColumn(thatButton) + 2]))
        {
            thatButton.Background = Brushes.Gold;
            isTriggered = true;
            manToBeat1 = blackMans[Grid.GetRow(thatButton) - 1, Grid.GetColumn(thatButton) + 1];
            field[Grid.GetRow(thatButton) - 2, Grid.GetColumn(thatButton) + 2].Background =
                Brushes.ForestGreen;
        }

        //Check left-up
        if (Grid.GetColumn(thatButton) > 0 && Grid.GetRow(thatButton) > 0 &&
            !whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton) - 1, Grid.GetColumn(thatButton) - 1]) &&
            !blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) - 1, Grid.GetColumn(thatButton) - 1]) &&
            countOfBeatenMen == 0 && !canBeat)
        {
            thatButton.Background = Brushes.Gold;
            isTriggered = true;
            field[Grid.GetRow(thatButton) - 1, Grid.GetColumn(thatButton) - 1].Background =
                Brushes.ForestGreen;
        }

        //Check left-up to beat
        if (Grid.GetColumn(thatButton) > 1 && Grid.GetRow(thatButton) > 1 &&
            blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) - 1, Grid.GetColumn(thatButton) - 1]) &&
            !whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton) - 2, Grid.GetColumn(thatButton) - 2]) &&
            !blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) - 2, Grid.GetColumn(thatButton) - 2]))
        {
            thatButton.Background = Brushes.Gold;
            isTriggered = true;
            manToBeat2 = blackMans[Grid.GetRow(thatButton) - 1, Grid.GetColumn(thatButton) - 1];
            field[Grid.GetRow(thatButton) - 2, Grid.GetColumn(thatButton) - 2].Background =
                Brushes.ForestGreen;
        }

        CheckKing(thatButton);

        //Check if we can't beat more
        if (!isTriggered) countOfBeatenMen = 0;
    }

    private static void CheckKing(Button thatButton)
    {
        //Check right-down
        if (whiteMans[Grid.GetRow(thatButton), Grid.GetColumn(thatButton)].Fill == Brushes.Red &&
            Grid.GetColumn(thatButton) < maxSizeOfField - 1 && Grid.GetRow(thatButton) < maxSizeOfField - 1 &&
            !whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) + 1]) &&
            !blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) + 1]) &&
            countOfBeatenMen == 0 && !canBeat)
        {
            thatButton.Background = Brushes.Gold;
            isTriggered = true;
            field[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) + 1].Background =
                Brushes.ForestGreen;
        }

        //Check right-down to beat
        if (whiteMans[Grid.GetRow(thatButton), Grid.GetColumn(thatButton)].Fill == Brushes.Red &&
            Grid.GetColumn(thatButton) < maxSizeOfField - 2 && Grid.GetRow(thatButton) < maxSizeOfField - 2 &&
            blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) + 1]) &&
            !whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton) + 2, Grid.GetColumn(thatButton) + 2]) &&
            !blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) + 2, Grid.GetColumn(thatButton) + 2]))
        {
            thatButton.Background = Brushes.Gold;
            isTriggered = true;
            manToBeat3 = blackMans[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) + 1];
            field[Grid.GetRow(thatButton) + 2, Grid.GetColumn(thatButton) + 2].Background =
                Brushes.ForestGreen;
        }

        //Check left-down
        if (whiteMans[Grid.GetRow(thatButton), Grid.GetColumn(thatButton)].Fill == Brushes.Red &&
            Grid.GetColumn(thatButton) > 0 && Grid.GetRow(thatButton) < maxSizeOfField - 1 &&
            !whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) - 1]) &&
            !blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) - 1]) &&
            countOfBeatenMen == 0 && !canBeat)
        {
            thatButton.Background = Brushes.Gold;
            isTriggered = true;
            field[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) - 1].Background =
                Brushes.ForestGreen;
        }

        //Check left-down to beat
        if (whiteMans[Grid.GetRow(thatButton), Grid.GetColumn(thatButton)].Fill == Brushes.Red &&
            Grid.GetColumn(thatButton) > 1 && Grid.GetRow(thatButton) < maxSizeOfField - 2 &&
            blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) - 1]) &&
            !whiteEllipses.Contains(whiteMans[Grid.GetRow(thatButton) + 2, Grid.GetColumn(thatButton) - 2]) &&
            !blackEllipses.Contains(blackMans[Grid.GetRow(thatButton) + 2, Grid.GetColumn(thatButton) - 2]))
        {
            thatButton.Background = Brushes.Gold;
            isTriggered = true;
            manToBeat4 = blackMans[Grid.GetRow(thatButton) + 1, Grid.GetColumn(thatButton) - 1];
            field[Grid.GetRow(thatButton) + 2, Grid.GetColumn(thatButton) - 2].Background =
                Brushes.ForestGreen;
        }
    }

    private static void DeleteTrigger(Button thatButton)
    {
        if (thatButton.Background.Equals(Brushes.Gold) && countOfBeatenMen == 0)
        {
            isTriggered = false;
            thatButton.Background = Brushes.Chocolate;
            manToBeat1 = null;
            manToBeat2 = null;
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
        }
    }

    private static void MoveMan(Button thatButton)
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

                SetColorBack();

                BeatMan(thatButton);
            }

            if (countOfBeatenMen == 0)
            {
                whiteTurn = false;
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
                blackEllipses.Remove(manToBeat1);
            }

            if (manToBeat3 != null)
            {
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
                manToBeat2.Visibility = Visibility.Collapsed;
                blackEllipses.Remove(manToBeat2);
            }

            if (manToBeat4 != null)
            {
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
}