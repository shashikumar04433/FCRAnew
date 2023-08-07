using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.ViewModels
{
    public class ModelResultViewModel<T>
    {
        public bool Result { get; set; }
        public T? Model { get; set; }
    }
}
