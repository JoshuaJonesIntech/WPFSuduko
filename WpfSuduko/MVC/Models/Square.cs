using System;
using System.Collections.Generic;
using System.Text;

namespace WpfSuduko.MVC.Models
{
    class Square
    {
        public const int DefaultValue = -1;
        public int StoredValue { get; set; }
        public int DisplayedValue { get; set; }
        public int XGridPosition { get; set; }
        public int YGridPosition { get; set; }

        public Square() 
        {
            StoredValue = DefaultValue;
        }

        public bool CheckCorrectness() 
        {
            return this.StoredValue == DisplayedValue;
        }
        public bool IsSet() 
        {
            return StoredValue != DefaultValue;
        }


    }
}
