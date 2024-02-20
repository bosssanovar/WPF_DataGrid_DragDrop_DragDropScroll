using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.IO;
using System.Windows.Media;
using System.Diagnostics;

namespace LivetApp2.Views
{
    public class ScrollDragger
    {
        private readonly ScrollViewer _scrollViewer;
        private readonly UIElement _content;
        private Point _scrollMousePoint;

        private double _vOff = 1;
        private double _hOff = 1;

        public ScrollDragger(DataGrid dataGrid)
        {
            _scrollViewer = GetScrollViewer(dataGrid);
            _content = dataGrid;

            _content.PreviewMouseLeftButtonDown += ScrollViewer_MouseLeftButtonDown;
        }

        public ScrollDragger(ScrollViewer scrollViewer)
        {
            _scrollViewer = scrollViewer;
            _content = scrollViewer;

            _content.PreviewMouseLeftButtonDown += ScrollViewer_MouseLeftButtonDown;
        }

        private void ScrollViewer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) == true
                || Keyboard.IsKeyDown(Key.RightCtrl) == true)
            {
                _content.PreviewMouseMove += ScrollViewer_PreviewMouseMove;
                _content.PreviewMouseLeftButtonUp += ScrollViewer_PreviewMouseLeftButtonUp;

                _scrollMousePoint = e.GetPosition(_scrollViewer);
                _vOff = _scrollViewer.VerticalOffset;
                _hOff = _scrollViewer.HorizontalOffset;

                e.Handled = true;

                Debug.WriteLine($"MouseLeftButtonDown");

                _content.CaptureMouse();
            }
        }

        private void ScrollViewer_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (_content.IsMouseCaptured)
            {
                var newVerticalOffset = _vOff + (_scrollMousePoint.Y - e.GetPosition(_scrollViewer).Y);
                _scrollViewer.ScrollToVerticalOffset(newVerticalOffset);

                var newHorizontalOffset = _hOff + (_scrollMousePoint.X - e.GetPosition(_scrollViewer).X);
                _scrollViewer.ScrollToHorizontalOffset(newHorizontalOffset);
            }

            e.Handled = true;

            Debug.WriteLine($"PreviewMouseMove");
        }

        private void ScrollViewer_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _content.PreviewMouseMove -= ScrollViewer_PreviewMouseMove;
            _content.PreviewMouseLeftButtonUp -= ScrollViewer_PreviewMouseLeftButtonUp;

            _content.ReleaseMouseCapture();

            e.Handled = true;

            Debug.WriteLine($"PreviewMouseLeftButtonUp");
        }
        private static ScrollViewer GetScrollViewer(FrameworkElement element)
        {
            // 引数elementのビジュアルオブジェクト数分繰り返す。
            var childrenNum = VisualTreeHelper.GetChildrenCount(element);
            for (int i = 0; i < childrenNum; ++i)
            {
                // ビジュアルオブジェクトを取得します。
                // ビジュアルオブジェクトが取得できない場合
                if (VisualTreeHelper.GetChild(element, i) is not FrameworkElement child)
                {
                    // 次を取得します。
                    continue;
                }

                // 取得したビジュアルオブジェクトがスクロールビューワーの場合
                if (child is ScrollViewer)
                {
                    // 取得したスクロールビューワーを返却します。
                    return child as ScrollViewer;
                }

                // 次のビジュアルオブジェクトを取得します。
                child = GetScrollViewer(child);
                if (child != null)
                {
                    return child as ScrollViewer;
                }
            }
            return null;
        }
    }

}