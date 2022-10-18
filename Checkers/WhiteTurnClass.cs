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
using static Checkers.Bot;

namespace Checkers;

public static class WhiteTurnClass
{
    public static void WhiteTurn(Button thatButton)
    {
        SetField();

        var rowOfThatButton = Grid.GetRow(thatButton);
        var columnOfThatButton = Grid.GetColumn(thatButton);

        if (whiteEllipses.Contains(whiteMans[rowOfThatButton, columnOfThatButton]))
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
            var rowOfThatEllipse = Grid.GetRow(whiteEllipse);
            var columnOfThatEllipse = Grid.GetColumn(whiteEllipse);

            if (columnOfThatEllipse < maxSizeOfField - 2 && rowOfThatEllipse > 1 &&
                blackEllipses.Contains(blackMans[rowOfThatEllipse - 1, columnOfThatEllipse + 1]) &&
                !whiteEllipses.Contains(whiteMans[rowOfThatEllipse - 2, columnOfThatEllipse + 2]) &&
                !blackEllipses.Contains(blackMans[rowOfThatEllipse - 2, columnOfThatEllipse + 2]))
            {
                canBeat = true;
            }

            if (columnOfThatEllipse > 1 && rowOfThatEllipse > 1 &&
                blackEllipses.Contains(blackMans[rowOfThatEllipse - 1, columnOfThatEllipse - 1]) &&
                !whiteEllipses.Contains(whiteMans[rowOfThatEllipse - 2, columnOfThatEllipse - 2]) &&
                !blackEllipses.Contains(blackMans[rowOfThatEllipse - 2, columnOfThatEllipse - 2]))
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
        var rowOfThatEllipse = Grid.GetRow(whiteEllipse);
        var columnOfThatEllipse = Grid.GetColumn(whiteEllipse);

        if (whiteMans[rowOfThatEllipse, columnOfThatEllipse].Fill == Brushes.Red &&
            columnOfThatEllipse > 1 && rowOfThatEllipse < maxSizeOfField - 2 &&
            blackEllipses.Contains(blackMans[rowOfThatEllipse + 1, columnOfThatEllipse - 1]) &&
            !whiteEllipses.Contains(whiteMans[rowOfThatEllipse + 2, columnOfThatEllipse - 2]) &&
            !blackEllipses.Contains(blackMans[rowOfThatEllipse + 2, columnOfThatEllipse - 2]))
        {
            canBeat = true;
        }

        if (whiteMans[rowOfThatEllipse, columnOfThatEllipse].Fill == Brushes.Red &&
            columnOfThatEllipse < maxSizeOfField - 2 && rowOfThatEllipse < maxSizeOfField - 2 &&
            blackEllipses.Contains(blackMans[rowOfThatEllipse + 1, columnOfThatEllipse + 1]) &&
            !whiteEllipses.Contains(whiteMans[rowOfThatEllipse + 2, columnOfThatEllipse + 2]) &&
            !blackEllipses.Contains(blackMans[rowOfThatEllipse + 2, columnOfThatEllipse + 2]))
        {
            canBeat = true;
        }
    }

    private static void IfCanMoveWhite(Ellipse whiteEllipse)
    {
        var rowOfThatEllipse = Grid.GetRow(whiteEllipse);
        var columnOfThatEllipse = Grid.GetColumn(whiteEllipse);

        if (columnOfThatEllipse < maxSizeOfField - 1 && rowOfThatEllipse > 0 &&
            !whiteEllipses.Contains(whiteMans[rowOfThatEllipse - 1, columnOfThatEllipse + 1]) &&
            !blackEllipses.Contains(blackMans[rowOfThatEllipse - 1, columnOfThatEllipse + 1]))
        {
            canMove = true;
        }

        if (columnOfThatEllipse > 0 && rowOfThatEllipse > 0 &&
            !whiteEllipses.Contains(whiteMans[rowOfThatEllipse - 1, columnOfThatEllipse - 1]) &&
            !blackEllipses.Contains(blackMans[rowOfThatEllipse - 1, columnOfThatEllipse - 1]))
        {
            canMove = true;
        }

        IfCanMoveWhiteKing(whiteEllipse);
    }

    //Check if King can move something
    private static void IfCanMoveWhiteKing(Ellipse whiteEllipse)
    {
        var rowOfThatEllipse = Grid.GetRow(whiteEllipse);
        var columnOfThatEllipse = Grid.GetColumn(whiteEllipse);

        if (whiteMans[rowOfThatEllipse, columnOfThatEllipse].Fill == Brushes.Red &&
            columnOfThatEllipse > 0 && rowOfThatEllipse < maxSizeOfField - 1 &&
            !whiteEllipses.Contains(whiteMans[rowOfThatEllipse + 1, columnOfThatEllipse - 1]) &&
            !blackEllipses.Contains(blackMans[rowOfThatEllipse + 1, columnOfThatEllipse - 1]))
        {
            canMove = true;
        }

        if (whiteMans[rowOfThatEllipse, columnOfThatEllipse].Fill == Brushes.Red &&
            columnOfThatEllipse < maxSizeOfField - 1 && rowOfThatEllipse < maxSizeOfField - 1 &&
            !whiteEllipses.Contains(whiteMans[rowOfThatEllipse + 1, columnOfThatEllipse + 1]) &&
            !blackEllipses.Contains(blackMans[rowOfThatEllipse + 1, columnOfThatEllipse + 1]))
        {
            canMove = true;
        }
    }

    private static void TriggerButton(Button thatButton)
    {
        lastTriggeredButton = thatButton;
        var rowOfThatButton = Grid.GetRow(thatButton);
        var columnOfThatButton = Grid.GetColumn(thatButton);

        for (int i = 0; i < maxSizeOfField; i++)
        {
            for (int j = 0; j < maxSizeOfField; j++)
            {
                if ((i + j) % 2 != 0)
                {
                    if (rowOfThatButton - i == 1 && Math.Abs(j - columnOfThatButton) == 1 &&
                        !whiteEllipses.Contains(whiteMans[i, j]) && !blackEllipses.Contains(blackMans[i, j]) &&
                        countOfBeatenMen == 0 && !canBeat && countOfBeatenMen == 0)
                    {
                        thatButton.Background = Brushes.Gold;
                        isTriggered = true;
                        field[i, j].Background = Brushes.ForestGreen;
                    }

                    if (rowOfThatButton - i == 2 && j - columnOfThatButton == 2 &&
                        blackEllipses.Contains(blackMans[i + 1, j - 1]) && !whiteEllipses.Contains(whiteMans[i, j]) &&
                        !blackEllipses.Contains(blackMans[i, j]))
                    {
                        thatButton.Background = Brushes.Gold;
                        isTriggered = true;
                        manToBeat1 = blackMans[i + 1, j - 1];
                        field[i, j].Background = Brushes.ForestGreen;
                    }

                    if (rowOfThatButton - i == 2 && columnOfThatButton - j == 2 &&
                        blackEllipses.Contains(blackMans[i + 1, j + 1]) && !whiteEllipses.Contains(whiteMans[i, j]) &&
                        !blackEllipses.Contains(blackMans[i, j]))
                    {
                        thatButton.Background = Brushes.Gold;
                        isTriggered = true;
                        manToBeat2 = blackMans[i + 1, j + 1];
                        field[i, j].Background = Brushes.ForestGreen;
                    }
                }
            }
        }

        CheckForKing(thatButton);

        //Check if we can't beat more
        if (!isTriggered) countOfBeatenMen = 0;
    }

    private static void CheckForKing(Button thatButton)
    {
        var rowOfThatButton = Grid.GetRow(thatButton);
        var columnOfThatButton = Grid.GetColumn(thatButton);

        for (int i = 0; i < maxSizeOfField; i++)
        {
            for (int j = 0; j < maxSizeOfField; j++)
            {
                if ((i + j) % 2 != 0)
                {
                    if (whiteMans[rowOfThatButton, columnOfThatButton].Fill == Brushes.Red &&
                        i - rowOfThatButton == 1 && Math.Abs(j - columnOfThatButton) == 1 &&
                        !whiteEllipses.Contains(whiteMans[i, j]) && !blackEllipses.Contains(blackMans[i, j]) &&
                        countOfBeatenMen == 0 && !canBeat && countOfBeatenMen == 0)
                    {
                        thatButton.Background = Brushes.Gold;
                        isTriggered = true;
                        field[i, j].Background = Brushes.ForestGreen;
                    }

                    if (whiteMans[rowOfThatButton, columnOfThatButton].Fill == Brushes.Red &&
                        i - rowOfThatButton == 2 && j - columnOfThatButton == 2 &&
                        blackEllipses.Contains(blackMans[i - 1, j - 1]) && !whiteEllipses.Contains(whiteMans[i, j]) &&
                        !blackEllipses.Contains(blackMans[i, j]))
                    {
                        thatButton.Background = Brushes.Gold;
                        isTriggered = true;
                        manToBeat1 = blackMans[i - 1, j - 1];
                        field[i, j].Background = Brushes.ForestGreen;
                    }

                    if (whiteMans[rowOfThatButton, columnOfThatButton].Fill == Brushes.Red &&
                        i - rowOfThatButton == 2 && columnOfThatButton - j == 2 &&
                        blackEllipses.Contains(blackMans[i - 1, j + 1]) && !whiteEllipses.Contains(whiteMans[i, j]) &&
                        !blackEllipses.Contains(blackMans[i, j]))
                    {
                        thatButton.Background = Brushes.Gold;
                        isTriggered = true;
                        manToBeat2 = blackMans[i - 1, j + 1];
                        field[i, j].Background = Brushes.ForestGreen;
                    }
                }
            }
        }
    }

    private static void DeleteTrigger(Button thatButton)
    {
        if (thatButton.Background.Equals(Brushes.Gold) && countOfBeatenMen == 0)
        {
            manToBeat1 = null;
            manToBeat2 = null;
            manToBeat3 = null;
            manToBeat4 = null;
            isTriggered = false;

            for (int i = 0; i < maxSizeOfField; i++)
            {
                for (int j = 0; j < maxSizeOfField; j++)
                {
                    if ((i + j) % 2 != 0)
                    {
                        field[i, j].Background = Brushes.Chocolate;
                    }
                }
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
                var rowOfThatButton = Grid.GetRow(thatButton);
                var columnOfThatButton = Grid.GetColumn(thatButton);
                var rowOfLastTriggeredButton = Grid.GetRow(lastTriggeredButton);
                var columnOfLastTriggeredButton = Grid.GetColumn(lastTriggeredButton);

                Grid.SetColumn(whiteMans[rowOfLastTriggeredButton, columnOfLastTriggeredButton], columnOfThatButton);
                Grid.SetRow(whiteMans[rowOfLastTriggeredButton, columnOfLastTriggeredButton], rowOfThatButton);

                //become king if we on the kingsrow
                if (rowOfThatButton == 0 &&
                    whiteMans[rowOfLastTriggeredButton, columnOfLastTriggeredButton].Fill == Brushes.White)
                {
                    becomeKing = true;
                    whiteMans[rowOfLastTriggeredButton, columnOfLastTriggeredButton].Fill = Brushes.Red;
                }

                SetColorBack();

                BeatMan(thatButton);
            }

            if (countOfBeatenMen == 0)
            {
                whiteTurn = false;
                message = "Black turn";
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
                
                if (whiteBot && isTriggered)
                {
                    TryMove();
                }
            }
            else
                countOfBeatenMen = 0;
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
                
                if (whiteBot && isTriggered)
                {
                    TryMove();
                }
            }
            else
                countOfBeatenMen = 0;
        }
    }
}