using Livet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivetApp2.ViewModels
{
    public class DetailViewModel : ViewModel
    {
        private bool _B1 = false;
        public bool B1
        {
            get
            {
                return _B1;
            }
            set
            {
                if(_B1 == value)
                {
                    return;
                }

                _B1 = value;

                RaisePropertyChanged();
            }
        }
    }



    public record HeaderDetail(string Text)
    {
    }
}
