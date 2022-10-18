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

public class BlackTurnClass
{
    public static void BlackTurn(Button thatButton)
    {
        SetField();

        var rowOfThatButton = Grid.GetRow(thatButton);
        var columnOfThatButton = Grid.GetColumn(thatButton);

        if (blackEllipses.Contains(blackMans[rowOfThatButton, columnOfThatButton]))
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
            var rowOfThatEllipse = Grid.GetRow(blackEllipse);
            var columnOfThatEllipse = Grid.GetColumn(blackEllipse);

            if (columnOfThatEllipse < maxSizeOfField - 2 && rowOfThatEllipse < maxSizeOfField - 2 &&
                whiteEllipses.Contains(whiteMans[rowOfThatEllipse + 1, columnOfThatEllipse + 1]) &&
                !whiteEllipses.Contains(whiteMans[rowOfThatEllipse + 2, columnOfThatEllipse + 2]) &&
                !blackEllipses.Contains(blackMans[rowOfThatEllipse + 2, columnOfThatEllipse + 2]))
            {
                canBeat = true;
            }

            if (columnOfThatEllipse > 1 && rowOfThatEllipse < maxSizeOfField - 2 &&
                whiteEllipses.Contains(whiteMans[rowOfThatEllipse + 1, columnOfThatEllipse - 1]) &&
                !whiteEllipses.Contains(whiteMans[rowOfThatEllipse + 2, columnOfThatEllipse - 2]) &&
                !blackEllipses.Contains(blackMans[rowOfThatEllipse + 2, columnOfThatEllipse - 2]))
            {
                canBeat = true;
            }

            IfCanBeatBlackKing(blackEllipse);

            IfCanMoveBlack(blackEllipse);
        }
    }

    //Check if King can beat something
    private static void IfCanBeatBlackKing(Ellipse blackEllipse)
    {
        var rowOfThatEllipse = Grid.GetRow(blackEllipse);
        var columnOfThatEllipse = Grid.GetColumn(blackEllipse);

        if (blackMans[rowOfThatEllipse, columnOfThatEllipse].Fill == Brushes.Blue &&
            columnOfThatEllipse > 1 && rowOfThatEllipse > 1 &&
            whiteEllipses.Contains(whiteMans[rowOfThatEllipse - 1, columnOfThatEllipse - 1]) &&
            !whiteEllipses.Contains(whiteMans[rowOfThatEllipse - 2, columnOfThatEllipse - 2]) &&
            !blackEllipses.Contains(blackMans[rowOfThatEllipse - 2, columnOfThatEllipse - 2]))
        {
            canBeat = true;
        }

        if (blackMans[rowOfThatEllipse, columnOfThatEllipse].Fill == Brushes.Blue &&
            columnOfThatEllipse < maxSizeOfField - 2 && rowOfThatEllipse > 1 &&
            whiteEllipses.Contains(whiteMans[rowOfThatEllipse - 1, columnOfThatEllipse + 1]) &&
            !whiteEllipses.Contains(whiteMans[rowOfThatEllipse - 2, columnOfThatEllipse + 2]) &&
            !blackEllipses.Contains(blackMans[rowOfThatEllipse - 2, columnOfThatEllipse + 2]))
        {
            canBeat = true;
        }
    }

    private static void IfCanMoveBlack(Ellipse blackEllipse)
    {
        var rowOfThatEllipse = Grid.GetRow(blackEllipse);
        var columnOfThatEllipse = Grid.GetColumn(blackEllipse);

        if (columnOfThatEllipse < maxSizeOfField - 1 && rowOfThatEllipse < maxSizeOfField - 1 &&
            !whiteEllipses.Contains(whiteMans[rowOfThatEllipse + 1, columnOfThatEllipse + 1]) &&
            !blackEllipses.Contains(blackMans[rowOfThatEllipse + 1, columnOfThatEllipse + 1]))
        {
            canMove = true;
        }

        if (columnOfThatEllipse > 0 && rowOfThatEllipse < maxSizeOfField - 1 &&
            !whiteEllipses.Contains(whiteMans[rowOfThatEllipse + 1, columnOfThatEllipse - 1]) &&
            !blackEllipses.Contains(blackMans[rowOfThatEllipse + 1, columnOfThatEllipse - 1]))
        {
            canMove = true;
        }

        IfCanMoveBlackKing(blackEllipse);
    }

    //Check if King can move
    private static void IfCanMoveBlackKing(Ellipse blackEllipse)
    {
        var rowOfThatEllipse = Grid.GetRow(blackEllipse);
        var columnOfThatEllipse = Grid.GetColumn(blackEllipse);

        if (blackMans[rowOfThatEllipse, columnOfThatEllipse].Fill == Brushes.Blue &&
            columnOfThatEllipse > 0 && rowOfThatEllipse > 0 &&
            !whiteEllipses.Contains(whiteMans[rowOfThatEllipse - 1, columnOfThatEllipse - 1]) &&
            !blackEllipses.Contains(blackMans[rowOfThatEllipse - 1, columnOfThatEllipse - 1]))
        {
            canMove = true;
        }

        if (blackMans[rowOfThatEllipse, columnOfThatEllipse].Fill == Brushes.Blue &&
            columnOfThatEllipse < maxSizeOfField - 1 && rowOfThatEllipse > 0 &&
            !whiteEllipses.Contains(whiteMans[rowOfThatEllipse - 1, columnOfThatEllipse + 1]) &&
            !blackEllipses.Contains(blackMans[rowOfThatEllipse - 1, columnOfThatEllipse + 1]))
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
                    if (i - rowOfThatButton == 1 && Math.Abs(j - columnOfThatButton) == 1 &&
                        !whiteEllipses.Contains(whiteMans[i, j]) && !blackEllipses.Contains(blackMans[i, j]) &&
                        countOfBeatenMen == 0 && !canBeat && countOfBeatenMen == 0)
                    {
                        thatButton.Background = Brushes.Gold;
                        isTriggered = true;
                        field[i, j].Background = Brushes.ForestGreen;
                    }

                    if (i - rowOfThatButton == 2 && j - columnOfThatButton == 2 &&
                        whiteEllipses.Contains(whiteMans[i - 1, j - 1]) && !whiteEllipses.Contains(whiteMans[i, j]) &&
                        !blackEllipses.Contains(blackMans[i, j]))
                    {
                        thatButton.Background = Brushes.Gold;
                        isTriggered = true;
                        manToBeat1 = whiteMans[i - 1, j - 1];
                        field[i, j].Background = Brushes.ForestGreen;
                    }

                    if (i - rowOfThatButton == 2 && columnOfThatButton - j == 2 &&
                        whiteEllipses.Contains(whiteMans[i - 1, j + 1]) && !whiteEllipses.Contains(whiteMans[i, j]) &&
                        !blackEllipses.Contains(blackMans[i, j]))
                    {
                        thatButton.Background = Brushes.Gold;
                        isTriggered = true;
                        manToBeat2 = whiteMans[i - 1, j + 1];
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
                    if (blackMans[rowOfThatButton, columnOfThatButton].Fill == Brushes.Blue &&
                        rowOfThatButton - i == 1 && Math.Abs(j - columnOfThatButton) == 1 &&
                        !whiteEllipses.Contains(whiteMans[i, j]) && !blackEllipses.Contains(blackMans[i, j]) &&
                        countOfBeatenMen == 0 && !canBeat  && countOfBeatenMen == 0)
                    {
                        thatButton.Background = Brushes.Gold;
                        isTriggered = true;
                        field[i, j].Background = Brushes.ForestGreen;
                    }

                    if (blackMans[rowOfThatButton, columnOfThatButton].Fill == Brushes.Blue &&
                        rowOfThatButton - i == 2 && j - columnOfThatButton == 2 &&
                        whiteEllipses.Contains(whiteMans[i + 1, j - 1]) && !whiteEllipses.Contains(whiteMans[i, j]) &&
                        !blackEllipses.Contains(blackMans[i, j]))
                    {
                        thatButton.Background = Brushes.Gold;
                        isTriggered = true;
                        manToBeat3 = whiteMans[i + 1, j - 1];
                        field[i, j].Background = Brushes.ForestGreen;
                    }

                    if (blackMans[rowOfThatButton, columnOfThatButton].Fill == Brushes.Blue &&
                        rowOfThatButton - i == 2 && columnOfThatButton - j == 2 &&
                        whiteEllipses.Contains(whiteMans[i + 1, j + 1]) && !whiteEllipses.Contains(whiteMans[i, j]) &&
                        !blackEllipses.Contains(blackMans[i, j]))
                    {
                        thatButton.Background = Brushes.Gold;
                        isTriggered = true;
                        manToBeat4 = whiteMans[i + 1, j + 1];
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

                Grid.SetColumn(blackMans[rowOfLastTriggeredButton, columnOfLastTriggeredButton], columnOfThatButton);
                Grid.SetRow(blackMans[rowOfLastTriggeredButton, columnOfLastTriggeredButton], rowOfThatButton);

                //become king if we on the kingsrow
                if (rowOfThatButton == 7 && blackMans[rowOfLastTriggeredButton, columnOfLastTriggeredButton].Fill ==
                    Brushes.Black)
                {
                    becomeKing = true;
                    blackMans[rowOfLastTriggeredButton, columnOfLastTriggeredButton].Fill = Brushes.Blue;
                }

                SetColorBack();

                BeatMan(thatButton);
            }

            if (countOfBeatenMen == 0)
            {
                whiteTurn = true;
                message = "White turn";
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
                
                if (blackBot && isTriggered)
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
                
                if (blackBot && isTriggered)
                {
                    TryMove();
                }
            }
            else
                countOfBeatenMen = 0;
        }
    }
}