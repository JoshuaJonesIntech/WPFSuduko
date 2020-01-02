using System;
using System.Collections.Generic;
using System.Text;

namespace WpfSuduko.MVC.Models
{
    class Square
    {
        public int StoredValue { get; set; }
        public int DisplayedValue { get; set; }

        public Boolean CheckCorrectness() {
            return this.StoredValue == DisplayedValue;
        }


    }
}
