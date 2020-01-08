using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WpfSuduko.MVC.Models
{
    class Square 
    {
        public const int DefaultStoredValue = -1;

        public int StoredValue { get; set; }
        public int XGridPosition { get; set; }
        public int YGridPosition { get; set; }

        public Square() 
        {
            this.StoredValue = DefaultStoredValue;
        }
        public Square(int xPos, int yPos, int storedValue = DefaultStoredValue)
        {
            XGridPosition = xPos;
            YGridPosition = yPos;
            StoredValue = storedValue;
        }
        public bool IsSet() 
        {
            return StoredValue != DefaultStoredValue;
        }
        public string GetDisplayValue() 
        {
            return StoredValue != DefaultStoredValue ? StoredValue.ToString() : "";
        }
        public static Square CopySquare(Square square) 
        {
            Square newSquare = new Square
            {
                StoredValue = square.StoredValue,
                XGridPosition = square.XGridPosition,
                YGridPosition = square.YGridPosition
            };
            return newSquare;
        }
        public override string ToString() 
        {
            string returnVal = " [ " + this.StoredValue + " ]";
            return returnVal;
        }
        public override bool Equals(object otherObject)
        {
            bool equals = false;
            if (otherObject is Square otherAsSquare) 
            {
                equals = this.StoredValue == otherAsSquare.StoredValue;
            }
            
            return equals;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(StoredValue, XGridPosition, YGridPosition);
        }
    }
}
