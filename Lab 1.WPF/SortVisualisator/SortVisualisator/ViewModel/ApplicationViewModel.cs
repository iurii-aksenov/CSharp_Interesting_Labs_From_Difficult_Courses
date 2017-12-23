using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using SortVisualisator.Animation;
using SortVisualisator._App;

namespace SortVisualisator.ViewModel
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        AutoResetEvent _autoResetEvent = new AutoResetEvent(false);
        private static Thread _thread;
        private static int _swappingIndex = 0;

        private static UIElementCollection _buttons;
        private int[] _items;
        private static IList<(int, int)> _swapSequence;
        private static EButtonState _buttonState = EButtonState.BS_SHUFFLE;
        private static EItemsState _itemsState = EItemsState.IS_SORTED;

        private Storyboard _storyboard = new Storyboard();


        public ApplicationViewModel(UIElementCollection buttons)
        {
            _storyboard.Completed += _storyboard_Completed;

            _buttons = buttons;

            _items = new int[buttons.Count];
            for (int i = 0; i < _items.Length; i++)
            {
                _items[i] = i;
            }
        }

        private string _itemsStateText = "Shuffle";
        public string ItemsStateText
        {
            get => _itemsStateText;
            set { _itemsStateText = value; OnPropertyChanged("ItemsStateText"); }
        }

        private RelayCommand _changeItemsState;
        public RelayCommand ChangeItemsState
        {
            get
            {
                return _changeItemsState ?? (_changeItemsState = new RelayCommand(obj => RouteStates()));
            }
        }

        private void MakeSwap()
        {
            _storyboard.Children = new TimelineCollection();

            UIElement leftElement;
            UIElement rightElement;

            if (Canvas.GetLeft(_buttons[_swapSequence[_swappingIndex].Item1]) <= Canvas.GetLeft(_buttons[_swapSequence[_swappingIndex].Item2]))
            {
                leftElement = _buttons[_swapSequence[_swappingIndex].Item1];
                rightElement = _buttons[_swapSequence[_swappingIndex].Item2];
            }
            else
            {
                leftElement = _buttons[_swapSequence[_swappingIndex].Item2];
                rightElement = _buttons[_swapSequence[_swappingIndex].Item1];
            }


            var dY = ((Button)leftElement).Height + 15.0;

            var dX = Math.Abs(Canvas.GetLeft(leftElement) - Canvas.GetLeft(rightElement));

            var timeLineRight = SwapCanvasAnimation.CreateTimelineForSwappedElemnt(rightElement, -dX, -dY);
            //if(Math.Abs(dX)<= Math.Abs(Canvas.GetLeft(_buttons[0]) - Canvas.GetLeft(_buttons[1]))) dY = 0;
            //if (Math.Abs(_swapSequence[_swappingIndex].Item1 - _swapSequence[_swappingIndex].Item2)<=1) dY = 0;
            var timeLineLeft = SwapCanvasAnimation.CreateTimelineForSwappedElemnt(leftElement, dX, dY);

            foreach (var timeLine in timeLineLeft)
            {
                _storyboard.Children.Add(timeLine);
            }

            foreach (var timeLine in timeLineRight)
            {
                _storyboard.Children.Add(timeLine);
            }


            _storyboard.Begin();
            
        }

        private void Shuffle()
        {
            _swapSequence = new List<(int, int)>();
            Shuffle<int>.Run(_items, _swapSequence);

            for (int i = 0; i < _swapSequence.Count; i++)
            {
                _swappingIndex = i;
                Application.Current.Dispatcher.BeginInvoke(new Action(MakeSwap));
                _autoResetEvent.WaitOne();

            }
            ItemsStateText = "Sort";
            _itemsState = EItemsState.IS_SHUFFLED;
            _buttonState = EButtonState.BS_SORT;
        }

        private void Sort()
        {
            _swapSequence = new List<(int, int)>();
            InsertionSort<int>.Sort(_items, _swapSequence);
            for (int i = 0; i < _swapSequence.Count; i++)
            {
                _swappingIndex = i;
                Application.Current.Dispatcher.BeginInvoke(new Action(MakeSwap));
                _autoResetEvent.WaitOne();

            }
            ItemsStateText = "Shuffle";
            _itemsState = EItemsState.IS_SORTED;
            _buttonState = EButtonState.BS_SHUFFLE;

        }

        private void _storyboard_Completed(object sender, EventArgs e)
        {
            _autoResetEvent.Set();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RouteStates()
        {


            if (_buttonState == EButtonState.BS_SHUFFLE)
            {
                if (_itemsState == EItemsState.IS_SORTED)
                {
                    _thread = null;
                    _itemsState = EItemsState.IS_NONE;
                }

                if (_thread != null)
                {


                    if (ItemsStateText == "Pause")
                    {
                        _storyboard.Pause();
                        ItemsStateText = "Resume";
                    }
                    else
                    {
                        _storyboard.Resume();
                        ItemsStateText = "Pause";
                    }
                }
                else
                {
                    ItemsStateText = "Pause";
                    _thread = new Thread(Shuffle);
                    _thread.Start();
                }



            }
            else if(_buttonState == EButtonState.BS_SORT)
            {
                if (_itemsState == EItemsState.IS_SHUFFLED)
                {
                    _thread = null;
                    _itemsState = EItemsState.IS_NONE;
                }
                if (_thread != null)
                {

                    if (ItemsStateText == "Pause")
                    {
                        _storyboard.Pause();
                        ItemsStateText = "Resume";
                    }
                    else
                    {
                        _storyboard.Resume();
                        ItemsStateText = "Pause";
                    }
                }
                else
                {
                    ItemsStateText = "Pause";
                    _thread = new Thread(Sort);
                    _thread.Start();
                }
            }
        }

        public enum EButtonState
        {
            BS_SORT = 0,
            BS_SHUFFLE
        }

        public enum EItemsState
        {
            IS_SORTED=0,
            IS_SHUFFLED,
            IS_NONE
        }

     
    }
}

/*
 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using SortVisualisator.Animation;
using SortVisualisator._App;

namespace SortVisualisator.ViewModel
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        AutoResetEvent _autoResetEvent = new AutoResetEvent(false);
        private static Thread _thread;
        private static (int, int) _swappingPair = (0, 0);

        private static UIElementCollection _buttons;
        private int[] _items;
        private static IList<(int, int)> _swapSequence;
        private static EButtonState _buttonState = EButtonState.BS_SHUFFLE;
        private static EItemsState _itemsState = EItemsState.IS_SORTED;

        private Storyboard _storyboard = new Storyboard();


        public ApplicationViewModel(UIElementCollection buttons)
        {
            _storyboard.Completed += _storyboard_Completed;

            _buttons = buttons;

            _items = new int[buttons.Count];
            for (int i = 0; i < _items.Length; i++)
            {
                _items[i] = i;
            }
        }

        private string _itemsStateText = "Shuffle";
        public string ItemsStateText
        {
            get => _itemsStateText;
            set { _itemsStateText = value; OnPropertyChanged("ItemsStateText"); }
        }

        private RelayCommand _changeItemsState;
        public RelayCommand ChangeItemsState
        {
            get
            {
                return _changeItemsState ?? (_changeItemsState = new RelayCommand(obj => RouteStates()));
            }
        }

        private void MakeSwap()
        {
            _storyboard.Children = new TimelineCollection();

            UIElement leftElement;
            UIElement rightElement;

            if (Canvas.GetLeft(_buttons[_swappingPair.Item1]) <= Canvas.GetLeft(_buttons[_swappingPair.Item2]))
            {
                leftElement = _buttons[_swappingPair.Item1];
                rightElement = _buttons[_swappingPair.Item2];
            }
            else
            {
                leftElement = _buttons[_swappingPair.Item2];
                rightElement = _buttons[_swappingPair.Item1];
            }


            var dY = ((Button)leftElement).Height + 15.0;

            var dX = Math.Abs(Canvas.GetLeft(leftElement) - Canvas.GetLeft(rightElement));

            var timeLineRight = SwapCanvasAnimation.CreateTimelineForSwappedElemnt(rightElement, -dX, -dY);
            if (dX <= Math.Abs(Canvas.GetLeft(_buttons[0]) - Canvas.GetLeft(_buttons[1]))) dY = 0;
            var timeLineLeft = SwapCanvasAnimation.CreateTimelineForSwappedElemnt(leftElement, dX, dY);

            foreach (var timeLine in timeLineLeft)
            {
                _storyboard.Children.Add(timeLine);
            }

            foreach (var timeLine in timeLineRight)
            {
                _storyboard.Children.Add(timeLine);
            }


            _storyboard.Begin();

        }

        private void Shuffle()
        {
            _swapSequence = new List<(int, int)>();
            Shuffle<int>.Run(_items, _swapSequence);

            for (int i = 0; i < _swapSequence.Count; i++)
            {
                _swappingPair = _swapSequence[i];
                Application.Current.Dispatcher.BeginInvoke(new Action(MakeSwap));
                _autoResetEvent.WaitOne();

            }
            ItemsStateText = "Sort";
            _itemsState = EItemsState.IS_SHUFFLED;
            _buttonState = EButtonState.BS_SORT;
        }

        private void Sort()
        {
            _swapSequence = new List<(int, int)>();
            InsertionSort<int>.Sort(_items, _swapSequence);
            for (int i = 0; i < _swapSequence.Count; i++)
            {
                _swappingPair = _swapSequence[i];
                Application.Current.Dispatcher.BeginInvoke(new Action(MakeSwap));
                _autoResetEvent.WaitOne();

            }
            ItemsStateText = "Shuffle";
            _itemsState = EItemsState.IS_SORTED;
            _buttonState = EButtonState.BS_SHUFFLE;

        }

        private void _storyboard_Completed(object sender, EventArgs e)
        {
            _autoResetEvent.Set();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RouteStates()
        {


            if (_buttonState == EButtonState.BS_SHUFFLE)
            {
                if (_itemsState == EItemsState.IS_SORTED)
                {
                    _thread = null;
                    _itemsState = EItemsState.IS_NONE;
                }

                if (_thread != null)
                {


                    if (ItemsStateText == "Pause")
                    {
                        _storyboard.Pause();
                        ItemsStateText = "Resume";
                    }
                    else
                    {
                        _storyboard.Resume();
                        ItemsStateText = "Pause";
                    }
                }
                else
                {
                    ItemsStateText = "Pause";
                    _thread = new Thread(Shuffle);
                    _thread.Start();
                }



            }
            else if (_buttonState == EButtonState.BS_SORT)
            {
                if (_itemsState == EItemsState.IS_SHUFFLED)
                {
                    _thread = null;
                    _itemsState = EItemsState.IS_NONE;
                }
                if (_thread != null)
                {

                    if (ItemsStateText == "Pause")
                    {
                        _storyboard.Pause();
                        ItemsStateText = "Resume";
                    }
                    else
                    {
                        _storyboard.Resume();
                        ItemsStateText = "Pause";
                    }
                }
                else
                {
                    ItemsStateText = "Pause";
                    _thread = new Thread(Sort);
                    _thread.Start();
                }
            }
        }

        public enum EButtonState
        {
            BS_SORT = 0,
            BS_SHUFFLE
        }

        public enum EItemsState
        {
            IS_SORTED = 0,
            IS_SHUFFLED,
            IS_NONE
        }


    }
}
*/

/*
 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using SortVisualisator.Animation;
using SortVisualisator._App;

namespace SortVisualisator.ViewModel
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        AutoResetEvent _autoResetEvent = new AutoResetEvent(false);
        private static Thread _thread;
        private static int _i = 0;

        private static UIElementCollection _buttons;
        private int[] _items;
        private static IList<(int, int)> _swapSequence;
        private static EButtonState _buttonState = EButtonState.BS_SHUFFLE;
        private static EItemsState _itemsState = EItemsState.IS_SORTED;

        private Storyboard _storyboard = new Storyboard();


        public ApplicationViewModel(UIElementCollection buttons)
        {
            _storyboard.Completed += _storyboard_Completed;

            _buttons = buttons;

            _items = new int[buttons.Count];
            for (int i = 0; i < _items.Length; i++)
            {
                _items[i] = i;
            }
        }

        private string _itemsStateText = "Shuffle";
        public string ItemsStateText
        {
            get => _itemsStateText;
            set { _itemsStateText = value; OnPropertyChanged("ItemsStateText"); }
        }

        private RelayCommand _changeItemsState;
        public RelayCommand ChangeItemsState
        {
            get
            {
                return _changeItemsState ?? (_changeItemsState = new RelayCommand(obj => RouteStates()));
            }
        }

        private void MakeSwap()
        {
            _storyboard.Children = new TimelineCollection();

            UIElement leftElement;
            UIElement rightElement;

            if (Canvas.GetLeft(_buttons[_swapSequence[_i].Item1]) <= Canvas.GetLeft(_buttons[_swapSequence[_i].Item2]))
            {
                leftElement = _buttons[_swapSequence[_i].Item1];
                rightElement = _buttons[_swapSequence[_i].Item2];
            }
            else
            {
                leftElement = _buttons[_swapSequence[_i].Item2];
                rightElement = _buttons[_swapSequence[_i].Item1];
            }


            var dY = ((Button)leftElement).Height + 15.0;

            var dX = Math.Abs(Canvas.GetLeft(leftElement) - Canvas.GetLeft(rightElement));

            var timeLineRight = SwapCanvasAnimation.CreateTimelineForSwappedElemnt(rightElement, -dX, -dY);
            if(dX<= Math.Abs(Canvas.GetLeft(_buttons[0]) - Canvas.GetLeft(_buttons[1]))) dY = 0;
            var timeLineLeft = SwapCanvasAnimation.CreateTimelineForSwappedElemnt(leftElement, dX, dY);

            foreach (var timeLine in timeLineLeft)
            {
                _storyboard.Children.Add(timeLine);
            }

            foreach (var timeLine in timeLineRight)
            {
                _storyboard.Children.Add(timeLine);
            }


            _storyboard.Begin();
            
        }

        private void Shuffle()
        {
            _swapSequence = new List<(int, int)>();
            Shuffle<int>.Run(_items, _swapSequence);

            for (int i = 0; i < _swapSequence.Count; i++)
            {
                _i = i;
                Application.Current.Dispatcher.BeginInvoke(new Action(MakeSwap));
                _autoResetEvent.WaitOne();

            }
            ItemsStateText = "Sort";
            _itemsState = EItemsState.IS_SHUFFLED;
            _buttonState = EButtonState.BS_SORT;
        }

        private void Sort()
        {
            _swapSequence = new List<(int, int)>();
            InsertionSort<int>.Sort(_items, _swapSequence);
            for (int i = 0; i < _swapSequence.Count; i++)
            {
                _i = i;
                Application.Current.Dispatcher.BeginInvoke(new Action(MakeSwap));
                _autoResetEvent.WaitOne();

            }
            ItemsStateText = "Shuffle";
            _itemsState = EItemsState.IS_SORTED;
            _buttonState = EButtonState.BS_SHUFFLE;

        }

        private void _storyboard_Completed(object sender, EventArgs e)
        {
            _autoResetEvent.Set();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RouteStates()
        {


            if (_buttonState == EButtonState.BS_SHUFFLE)
            {
                if (_itemsState == EItemsState.IS_SORTED)
                {
                    _thread = null;
                    _itemsState = EItemsState.IS_NONE;
                }

                if (_thread != null)
                {


                    if (ItemsStateText == "Pause")
                    {
                        _storyboard.Pause();
                        ItemsStateText = "Resume";
                    }
                    else
                    {
                        _storyboard.Resume();
                        ItemsStateText = "Pause";
                    }
                }
                else
                {
                    ItemsStateText = "Pause";
                    _thread = new Thread(Shuffle);
                    _thread.Start();
                }



            }
            else if(_buttonState == EButtonState.BS_SORT)
            {
                if (_itemsState == EItemsState.IS_SHUFFLED)
                {
                    _thread = null;
                    _itemsState = EItemsState.IS_NONE;
                }
                if (_thread != null)
                {

                    if (ItemsStateText == "Pause")
                    {
                        _storyboard.Pause();
                        ItemsStateText = "Resume";
                    }
                    else
                    {
                        _storyboard.Resume();
                        ItemsStateText = "Pause";
                    }
                }
                else
                {
                    ItemsStateText = "Pause";
                    _thread = new Thread(Sort);
                    _thread.Start();
                }
            }
        }

        public enum EButtonState
        {
            BS_SORT = 0,
            BS_SHUFFLE
        }

        public enum EItemsState
        {
            IS_SORTED=0,
            IS_SHUFFLED,
            IS_NONE
        }

        //public bool IsState()
        //{
        //    int state = 0;
        //    if (state != 0)
        //    {
        //        if (state == -1)
        //        {
        //            _storyboardLeft.Pause(Canvas);
        //            _storyBoardRight.Pause(Canvas);
        //            state = -2;
        //        }
        //        else
        //        {
        //            _storyboardLeft.Resume(Canvas);
        //            _storyBoardRight.Resume(Canvas);
        //            state = -1;
        //        }


        //        return true;
        //    }
        //    return false;
        //}
    }
}

*/
