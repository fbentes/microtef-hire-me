using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StonePayments.Client.Util
{
    public interface IViewOpenObservable : IViewObservable
    {
        void Show();
    }
}
