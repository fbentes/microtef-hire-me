using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StonePayments.Business.Interfaces
{
    public interface IViewOpenObservable : IViewObservable
    {
        void Show();
    }
}
