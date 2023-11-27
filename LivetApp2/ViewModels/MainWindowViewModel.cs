using Livet;
using Livet.Commands;
using Livet.EventListeners;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.Messaging.Windows;
using LivetApp2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LivetApp2.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        /// <summary>
        /// Items
        /// </summary>
        public ObservableCollection<DetailViewModel> Items { get; } = new ObservableCollection<DetailViewModel>();

        public List<HeaderDetail> RowHeaders1 { get; set; } = new();
        public List<HeaderDetail> RowHeaders2 { get; set; } = new();
        public List<HeaderDetail> RowHeaders3 { get; set; } = new();
        public List<HeaderDetail> ColumnHeaders1 { get; set; } = new();
        public List<HeaderDetail> ColumnHeaders2 { get; set; } = new();
        public List<HeaderDetail> ColumnHeaders3 { get; set; } = new();

        // Some useful code snippets for ViewModel are defined as l*(llcom, llcomn, lvcomm, lsprop, etc...).
        public void Initialize()
        {
        }

        public MainWindowViewModel()
        {
            for(int i = 0; i < 642; i++)
            {
                RowHeaders1.Add(new(i.ToString()));
                RowHeaders2.Add(new(i.ToString()));
                ColumnHeaders1.Add(new(i.ToString()));
                ColumnHeaders2.Add(new(i.ToString()));
                if (i % 3 == 0)
                {
                    RowHeaders3.Add(new((i / 3).ToString()));
                    ColumnHeaders3.Add(new((i / 3).ToString()));
                }
                Items.Add(new());
            }
        }
    }
}
