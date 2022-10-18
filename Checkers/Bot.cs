using System;
using System.Collections.Generic;
using System.Linq;
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
using static Checkers.MainWindow;
using static Checkers.WhiteTurnClass;
using static Checkers.BlackTurnClass;

namespace Checkers;

public class Bot
{
    private static List<Ellipse> myMen = new List<Ellipse>();

    public static void StartBot(List<Ellipse> men, bool turn)
    {
        if (turn)
        {
            SetField();

            //create copy og list of men
            foreach (var man in men) myMen.Add(man);

            TryClick();
        }
    }

    private static void TryClick()
    {
        Ellipse man = new Ellipse();
        if (myMen.Count > 0)
        {
            man = myMen[new Random().Next(myMen.Count)];
            
            var thatButton = field[Grid.GetRow(man), Grid.GetColumn(man)];
            canBeat = false;
            canMove = false;

            //Try to trigger button
            if (whiteTurn)
            {
                IfCanBeatWhite();

                if (canMove) WhiteTurn(thatButton);
            }
            else
            {
                IfCanBeatBlack();

                if (canMove) BlackTurn(thatButton);
            }

            while (!isTriggered && myMen.Count > 1)
            {
                myMen.Remove(man);

                man = myMen[new Random().Next(myMen.Count)];
                thatButton = field[Grid.GetRow(man), Grid.GetColumn(man)];

                //Try to trigger button
                if (whiteTurn) WhiteTurn(thatButton);
                else BlackTurn(thatButton);
            }

            TryMove();
        }
    }

    public static void TryMove()
    {
        List<Button> myButtons = new List<Button>();

        for (int i = 0; i < maxSizeOfField; i++)
        {
            for (int j = 0; j < maxSizeOfField; j++)
            {
                if ((i + j) % 2 != 0)
                {
                    if (field[i, j].Background == Brushes.ForestGreen) myButtons.Add(field[i, j]);
                }
            }
        }

        if (myButtons.Count > 0)
        {
            Button thatButton = myButtons[new Random().Next(myButtons.Count)];

            if (whiteTurn) WhiteTurn(thatButton);
            else BlackTurn(thatButton);
        }
    }
}