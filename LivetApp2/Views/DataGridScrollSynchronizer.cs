using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LivetApp2.Views
{
    /**
     * @brief スクロールの同期する方向
     */
    [Flags]
    public enum SynchronizeDirection
    {
        //! 水平方向
        Horizontal = 0x01,

        //! 垂直方向
        Vertical = 0x02,

        //! 両方
        Both = 0x03,
    }

    /**
     * @brief データグリッドスクロール同期クラス
     */
    public class DataGridScrollSynchronizer
    {
        //! スクロールビューワーリスト
        private readonly List<ScrollViewer> _scrollViewerList;

        private int _index = 0;

        private int NextIndex
        {
            get
            {
                int ret = _index;
                _index++;
                return ret;
            }
        }

        //! スクロール方向
        private SynchronizeDirection _direction { get; set; }

        /**
         * @brier コンストラクタ
         * 
         * @param [in] dataGridList 同期するデータグリッドリスト
         * @param [in] direction 同期するスクロール方向
         */
        public DataGridScrollSynchronizer(SynchronizeDirection direction = SynchronizeDirection.Both)
        {
            _scrollViewerList = new List<ScrollViewer>();

            // スクロール方向を保存します。
            _direction = direction;
        }

        public void AddScrollableElement(ItemsControl itemsControl)
        {
            // データグリッドのスクロールビューワーを取得します。
            var scrollViewer = GetScrollViewer(itemsControl);

            // スクロールビューワーにイベントハンドラを設定します。
            scrollViewer.ScrollChanged += ScrollChanged;

            // スクロールビューワーを識別するためタグを設定します。
            scrollViewer.Tag = NextIndex;

            // スクロールビューワーリストに保存します。
            _scrollViewerList.Add(scrollViewer);
        }

        public void AddScrollableElement(ScrollViewer scrollViewer)
        {
            // スクロールビューワーにイベントハンドラを設定します。
            scrollViewer.ScrollChanged += ScrollChanged;

            // スクロールビューワーを識別するためタグを設定します。
            scrollViewer.Tag = NextIndex;

            // スクロールビューワーリストに保存します。
            _scrollViewerList.Add(scrollViewer);
        }

        /**
         * @brief スクロールビューワーを取得します。
         * 
         * @param [in] element エレメント
         * @return スクロールビューワー
         *         取得できない場合、nullを返却します。
         */
        private ScrollViewer GetScrollViewer(FrameworkElement element)
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

        /**
         * @brief スクロールされた時に呼び出されるます。
         * 
         * @param [in] sender スクロールビューワー
         * @param [in] e スクロールチェンジイベント
         */
        private void ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var srcScrollViewer = sender as ScrollViewer;

            // 同期するスクロール方向が水平方向の場合
            if (_direction.HasFlag(SynchronizeDirection.Horizontal))
            {
                // スクロールするオフセットを取得します。
                var offset = srcScrollViewer.HorizontalOffset;

                // スクロールビューワー数分繰り返します。
                foreach (var dstScrollVierwer in _scrollViewerList)
                {
                    // スクロールしたスクロールビューワーは無視します。
                    if (dstScrollVierwer.Tag == srcScrollViewer.Tag)
                    {
                        continue;
                    }

                    if (offset == dstScrollVierwer.HorizontalOffset)
                    {
                        continue;
                    }

                    // 同期するスクロールビューワーをスクロールします。
                    dstScrollVierwer.ScrollToHorizontalOffset(offset);
                }
            }

            // 同期するスクロール方向が垂直方向の場合
            if (_direction.HasFlag(SynchronizeDirection.Vertical))
            {
                // スクロールするオフセットを取得します。
                var offset = srcScrollViewer.VerticalOffset;

                // スクロールビューワー数分繰り返します。
                foreach (var dstScrollVierwer in _scrollViewerList)
                {
                    // スクロールしたスクロールビューワーは無視します。
                    if (dstScrollVierwer.Tag == srcScrollViewer.Tag)
                    {
                        continue;
                    }

                    if (offset == dstScrollVierwer.VerticalOffset)
                    {
                        continue;
                    }

                    // 同期するスクロールビューワーをスクロールします。
                    dstScrollVierwer.ScrollToVerticalOffset(GetOffset(srcScrollViewer, dstScrollVierwer, offset));
                }
            }
        }

        private double GetOffset(ScrollViewer src, ScrollViewer dst, double offset)
        {
            if (!src.CanContentScroll && dst.CanContentScroll)
            {
                return offset / 20.0;
            }
            else if (src.CanContentScroll && !dst.CanContentScroll)
            {
                return offset * 20.0;
            }
            else
            {
                return offset;
            }
        }
    }
}