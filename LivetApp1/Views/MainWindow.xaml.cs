using System;
using System.Collections.Generic;
using System.Linq;
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

namespace LivetApp1.Views
{
    public partial class MainWindow : Window
    {
        public List<HeaderDetail> RowHeaders1 { get; set; } = new();
        public List<HeaderDetail> RowHeaders2 { get; set; } = new();
        public List<HeaderDetail> RowHeaders3 { get; set; } = new();
        public List<HeaderDetail> ColumnHeaders1 { get; set; } = new();
        public List<HeaderDetail> ColumnHeaders2 { get; set; } = new();
        public List<HeaderDetail> ColumnHeaders3 { get; set; } = new();

        public List<Detail> Items { get; set; } = new();

        public MainWindow()
        {
            for (int i = 0; i < 600; i++)
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

            InitializeComponent();
        }
    }

    public record HeaderDetail(string Text)
    {
    }

    public class Detail
    {
        public bool B1 { get; set; }
    }
}
