using System;
using System.Collections.Generic;
using System.Text;

namespace CompassMobileModels
{
    public class BoundingCoordinates
    {
        public double Left { get; set; }
        public double Top { get; set; }
        public double Right { get; set; }
        public double Bottom { get; set; }

        public BoundingCoordinates(double left, double top, double right, double bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        /// <summary>
        /// If you're using this for Latitude Longitude in a part of the world
        /// where you may cross 180degrees or 0 degrees you'll need to override this
        /// in a derived class
        /// </summary>
        /// <param name="bc"></param>
        /// <returns></returns>
        public virtual bool Within(BoundingCoordinates bc)
        {
            if (Left >= bc.Left
                && Right <= bc.Right
                && Top <= bc.Top
                && Bottom >= bc.Bottom)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual bool Within(BoundingCoordinates bc, int precision)
        {
            double thisLeft = Math.Round(this.Left, precision);
            double thisTop = Math.Round(this.Top, precision);
            double thisRight = Math.Round(this.Right, precision);
            double thisBottom = Math.Round(this.Bottom, precision);

            double thatLeft = Math.Round(bc.Left, precision);
            double thatTop = Math.Round(bc.Top, precision);
            double thatRight = Math.Round(bc.Right, precision);
            double thatBottom = Math.Round(bc.Bottom, precision);

            if (thisLeft >= thatLeft
                && thisRight <= thatRight
                && thisTop <= thatTop
                && thisBottom >= thatBottom)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator ==(BoundingCoordinates bc1, BoundingCoordinates bc2)
        {
            if (bc1.Left == bc2.Left && bc1.Top == bc2.Top && bc1.Right == bc2.Right && bc1.Bottom == bc2.Bottom)
                return true;
            else
                return false;
        }

        public static bool operator !=(BoundingCoordinates bc1, BoundingCoordinates bc2)
        {
            return !(bc1 == bc2);
        }

        public bool Equals(BoundingCoordinates bc1, BoundingCoordinates bc2)
        {
            return (bc1 == bc2);
        }
        public override bool Equals(object obj)
        {
            if (obj is BoundingCoordinates)
            {
                return this == ((BoundingCoordinates)obj);
            }

            return false;
        }

        public override int GetHashCode()
        {
            if (object.ReferenceEquals(this, null))
                return 0;

            int leftHashCode = Left.GetHashCode();
            int topHashCode = Top.GetHashCode();
            int rightHashCode = Right.GetHashCode();
            int bottomHashCode = Bottom.GetHashCode();


            return leftHashCode ^ topHashCode ^ rightHashCode ^ bottomHashCode;
        }

        public int GetHashCode(BoundingCoordinates bc)
        {
            return bc.GetHashCode();
        }
    }
}
