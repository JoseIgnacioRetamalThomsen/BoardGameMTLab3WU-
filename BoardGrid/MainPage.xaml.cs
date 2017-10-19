using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409
//test new brach git hub
namespace BoardGrid
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region Global Variables _rows is essential, don't delete it
        int _Rows;
        const int _iHeight = 100;
        const int _iWidth = 100;
        int _mouse = 1;
        int _cats;
        Grid myGrid;
        #endregion
        #region Constructor and set up code
        public MainPage()
        {
            this.InitializeComponent();
        }
        #endregion
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton current = (RadioButton)sender;
            _Rows = Convert.ToInt32(current.Tag);
            userStatus.Text = "some string " + _Rows;
            createGrid();
            setupThePieces();
            //comit this
            Grid findIt = new Grid();
            findIt = (Grid)FindName("ChessBoard");

        }

        Boolean mouseMove = true;
        Boolean someOneIsMoving = false;

        Ellipse mouse;
        Ellipse move1;
        Ellipse move2;
        private void setupThePieces()
        {
            _cats = (int)_Rows / 2;

            //place the mouse in the board
            mouse = new Ellipse();
            int xMouse, yMouse;
            xMouse = 3;
            yMouse = 0;
            mouse.SetValue(Grid.ColumnProperty, (double)xMouse);
            mouse.SetValue(Grid.RowProperty, (double)yMouse);
            mouse.Fill = new SolidColorBrush(Colors.Green);
            mouse.Name = "TheMouse";
            myGrid.Children.Add(mouse);

            mouse.Tapped += Mouse_Taped;

            //create and add cats
            int x = 0;
            for (int i = 0; i < _cats; i++)
            {

                Ellipse cat = new Ellipse();
                cat.Width = 50;
                cat.Height = 50;
                cat.Fill = new SolidColorBrush(Colors.Red);
                if (_Rows % 2 == 0)
                {
                    if (i == 0)
                    {
                        x = i;
                    }

                }
                else
                {
                    if (i == 0)
                    {
                        x = 1;
                    }
                }


                cat.SetValue(Grid.ColumnProperty, (double)(x));
                x += 2;
                cat.SetValue(Grid.RowProperty, ((double)_Rows - 1));
                //cat.Name = "TheCat" + i;
                myGrid.Children.Add(cat);
                cat.Tapped += Cat_Taped;
            }

        }

        Ellipse cat;
        Ellipse catMove1;
        Ellipse catMove2;
        private void Cat_Taped(object sender, TappedRoutedEventArgs e)
        {
            if (!someOneIsMoving)
            {

                if (!mouseMove)
                {

                    Debug.WriteLine(((Ellipse)sender).Name + " taped");
                    int x = (int)((Ellipse)sender).GetValue(Grid.ColumnProperty);
                    int y = (int)((Ellipse)sender).GetValue(Grid.RowProperty);
                    cat = ((Ellipse)sender);
                    Debug.WriteLine(((Ellipse)sender).GetValue(Grid.RowProperty));

                    //move 1
                    if ((x - 1) < 0)
                    { }
                    else if (y != 0)
                    {
                        //check if the move is valid
                        Boolean invalidMove = false;
                        int xMove = x - 1;
                        int yMove = (y - 1);
                        int xOther, yOther;
                        foreach (UIElement uiElement in myGrid.Children)
                        {
                            if (uiElement.GetType() == typeof(Ellipse))
                            {
                                xOther = (int)uiElement.GetValue(Grid.ColumnProperty);
                                yOther = (int)uiElement.GetValue(Grid.RowProperty);
                                if (xMove == xOther && yMove == yOther)
                                {
                                    invalidMove = true;
                                }
                            }
                        }

                        if (invalidMove)
                        { }
                        else
                        {
                            catMove1 = new Ellipse();
                            catMove1.Fill = new SolidColorBrush(Colors.Gray);
                            catMove1.Height = 50;
                            catMove1.Width = 50;
                            catMove1.SetValue(Grid.ColumnProperty, xMove);
                            catMove1.SetValue(Grid.RowProperty, yMove);
                            catMove1.Tapped += CAT_Move1_Tapped;
                            myGrid.Children.Add(catMove1);
                            someOneIsMoving = true;
                        }
                    }//end else if is insede
                    //move 2
                    if ((x + 1) >= _Rows)
                    { }
                    else if (y != 0)
                    {
                        Boolean invalidMove = false;

                        int xMove = x + 1;
                        int yMove = (y - 1);
                        int xOther, yOther;

                        foreach (UIElement uiElement in myGrid.Children)
                        {
                            // Debug.WriteLine(elli.GetValue(Grid.ColumnProperty));
                            if (uiElement.GetType() == typeof(Ellipse))
                            {
                                xOther = (int)uiElement.GetValue(Grid.ColumnProperty);
                                yOther = (int)uiElement.GetValue(Grid.RowProperty);
                                if (xMove == xOther && yMove == yOther)
                                {
                                    invalidMove = true;
                                }
                            }
                        }



                        if (invalidMove)
                        { }
                        else
                        {


                            //second move
                            catMove2 = new Ellipse();
                            catMove2.Fill = new SolidColorBrush(Colors.Gray);
                            catMove2.Height = 50;
                            catMove2.Width = 50;
                            catMove2.SetValue(Grid.ColumnProperty, xMove);
                            catMove2.SetValue(Grid.RowProperty, yMove);
                            myGrid.Children.Add(catMove2);
                            catMove2.Tapped += CAT_Move1_Tapped;
                            someOneIsMoving = true;
                        }
                    }
                } //!mouse move

            }
            else
            {
            }
        }//cat tapped

        private void CAT_Move1_Tapped(object sender, TappedRoutedEventArgs e)
        {

            cat.SetValue(Grid.ColumnProperty, ((Ellipse)sender).GetValue(Grid.ColumnProperty));
            cat.SetValue(Grid.RowProperty, ((Ellipse)sender).GetValue(Grid.RowProperty));
            myGrid.Children.Remove(catMove1);
            myGrid.Children.Remove(catMove2);
            mouseMove = true;
            someOneIsMoving = false;
        }

        private void Mouse_Taped(object sender, TappedRoutedEventArgs e)
        {
            if (!someOneIsMoving)
            {

                if (mouseMove)
                {
                    Debug.WriteLine("mouse taped");
                    int x = (int)((Ellipse)sender).GetValue(Grid.ColumnProperty);
                    int y = (int)((Ellipse)sender).GetValue(Grid.RowProperty);


                    // first move
                    if ((x - 1) < 0)
                    { }
                    else
                    {
                        Boolean invalidMove = false;
                        Debug.WriteLine(x);
                        Debug.WriteLine(y);
                        int xMove = x - 1;
                        int yMove = (y + 1);
                        int xOther, yOther;
                        foreach (UIElement uiElement in myGrid.Children)
                        {
                            if (uiElement.GetType() == typeof(Ellipse))
                            {
                                xOther = (int)uiElement.GetValue(Grid.ColumnProperty);
                                yOther = (int)uiElement.GetValue(Grid.RowProperty);
                                if (xMove == xOther && yMove == yOther)
                                {
                                    invalidMove = true;
                                }

                            }
                        }
                        if (invalidMove)
                        { }
                        else
                        {
                            move1 = new Ellipse();
                            move1.Fill = new SolidColorBrush(Colors.Gray);
                            move1.Height = 50;
                            move1.Width = 50;
                            move1.SetValue(Grid.ColumnProperty, (x - 1));
                            move1.SetValue(Grid.RowProperty, (y + 1));
                            move1.Tapped += Move1_Tapped;

                            myGrid.Children.Add(move1);
                            someOneIsMoving = true;
                        }
                    }
                    if ((x + 1) >= _Rows)
                    { }
                    else
                    {
                        Boolean invalidMove = false;

                        int xMove = x + 1;
                        int yMove = (y + 1);
                        int xOther, yOther;
                        foreach (UIElement uiElement in myGrid.Children)
                        {
                            if (uiElement.GetType() == typeof(Ellipse))
                            {
                                xOther = (int)uiElement.GetValue(Grid.ColumnProperty);
                                yOther = (int)uiElement.GetValue(Grid.RowProperty);
                                if (xMove == xOther && yMove == yOther)
                                {
                                    invalidMove = true;
                                }
                            }
                        }

                        if (invalidMove)
                        { }
                        else
                        {
                            //second move
                            move2 = new Ellipse();
                            move2.Fill = new SolidColorBrush(Colors.Gray);
                            move2.Height = 50;
                            move2.Width = 50;
                            move2.SetValue(Grid.ColumnProperty, (x + 1));
                            move2.SetValue(Grid.RowProperty, (y + 1));
                            myGrid.Children.Add(move2);
                            move2.Tapped += Move1_Tapped;
                            someOneIsMoving = true;
                        }
                    }


                }//mouse move

            }
        }//mouse tapped
        private void Move1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // Debug.WriteLine("move 1 tapped");
            //((Ellipse)sender).SetValue(Grid.ColumnProperty);
            mouse.SetValue(Grid.ColumnProperty, ((Ellipse)sender).GetValue(Grid.ColumnProperty));
            mouse.SetValue(Grid.RowProperty, ((Ellipse)sender).GetValue(Grid.RowProperty));
            myGrid.Children.Remove(move1);
            myGrid.Children.Remove(move2);
            mouseMove = false;

            someOneIsMoving = false;

        }


        private void createGrid()
        {

            //create the row definitios nad col definitios
            //grid (Thaht ii just create and put on the root grid)
            //need two loops- one for rows, one for cols
            //start outer loop for rows
            //then run the inner loop set each column elemnt
            paneForGrid.Children.Remove(myGrid);
            myGrid = new Grid();

            Border sqr;

            myGrid.HorizontalAlignment = HorizontalAlignment.Left;
            myGrid.VerticalAlignment = VerticalAlignment.Top;
            myGrid.Name = "ChessBoard";

            myGrid.Height = _iHeight * _Rows;
            myGrid.Width = _iWidth * _Rows;
            myGrid.Background = new SolidColorBrush(Colors.Gray);
            myGrid.Margin = new Thickness(5);
            myGrid.SetValue(Grid.RowProperty, 1);

            // Define the Columns
            for (int i = 0; i < _Rows; i++)
            {
                ColumnDefinition colDef1 = new ColumnDefinition();
                RowDefinition rowDef1 = new RowDefinition();
                myGrid.ColumnDefinitions.Add(colDef1);
                myGrid.RowDefinitions.Add(rowDef1);
            }

            for (int i = 0; i < _Rows; i++)
                for (int j = 0; j < _Rows; j++)
                {
                    {
                        #region Create one border element and add to the grid
                        sqr = new Border();

                        sqr.Height = 98;
                        sqr.Width = 98;
                        sqr.HorizontalAlignment = HorizontalAlignment.Center;
                        sqr.VerticalAlignment = VerticalAlignment.Center;
                        sqr.SetValue(Grid.ColumnProperty, (double)i);
                        sqr.SetValue(Grid.RowProperty, (double)j);

                        if ((i + j) % 2 == 0)
                        {

                            sqr.Background = new SolidColorBrush(Colors.Black);
                        }
                        else
                        {

                            sqr.Background = new SolidColorBrush(Colors.White);
                        }

                        myGrid.Children.Add(sqr);
                        #endregion
                    }

                }

            paneForGrid.Children.Add(myGrid);
        }

        private void GetGridElements()
        {
            Grid findIt = new Grid();
            findIt = (Grid)FindName("ChessBoard");
            /*
            foreach(UIElement el in findIt.Children)
            {
                if((int)el.GetValue(Grid.ColumnProperty))
            }
            */

        }
    }
}
