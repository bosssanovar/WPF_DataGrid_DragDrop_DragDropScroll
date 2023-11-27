using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LivetApp2.Views
{
    /* 
     * If some events were receive from ViewModel, then please use PropertyChangedWeakEventListener and CollectionChangedWeakEventListener.
     * If you want to subscribe custome events, then you can use LivetWeakEventListener.
     * When window closing and any timing, Dispose method of LivetCompositeDisposable is useful to release subscribing events.
     *
     * Those events are managed using WeakEventListener, so it is not occurred memory leak, but you should release explicitly.
     */
    public partial class MainWindow : Window
    {
        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        private DataGridScrollSynchronizer _verticalScrollSynchronizer;
        private DataGridScrollSynchronizer _horizontalScrollSynchronizer;
        private ScrollDragger _ScrollDragger;

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------
        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - internal ----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - protected ---------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            var verticals = new List<ItemsControl>();
            verticals.Add(rowHeader1);
            verticals.Add(rowHeader2);
            verticals.Add(rowHeader3);
            verticals.Add(body);
            _verticalScrollSynchronizer = new DataGridScrollSynchronizer(verticals, SynchronizeDirection.Vertical);

            var horizontals = new List<ItemsControl>();
            horizontals.Add(columnHeader1);
            horizontals.Add(columnHeader2);
            horizontals.Add(columnHeader3);
            horizontals.Add(body);
            _horizontalScrollSynchronizer = new DataGridScrollSynchronizer(horizontals, SynchronizeDirection.Horizontal);

            _ScrollDragger = new ScrollDragger(body);
        }

        private void body_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            slider.Value += (e.Delta > 0) ? -0.03 : 0.03;

            e.Handled = true;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - override ----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}
