using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeapSort
{
    class IntFloat: IComparable<IntFloat>
    {
        public int Integer { get; set; }
        public float Floater { get; set; }
        public IntFloat() { }
        public IntFloat(int Integer, float Floater)
        {
            this.Integer = Integer;
            this.Floater = Floater;
        }
        public override string ToString()
        {
            return String.Format($"Int: {Integer} | Float: {Floater}");
        }

        int IComparable<IntFloat>.CompareTo(IntFloat other)
        {
            if (other == null)
            {
                return 0;
            }
            if (this.Integer != other.Integer)
            {
                return Integer.CompareTo(other.Integer);
            }
            else
            {
                return Floater.CompareTo(other.Floater);
            }
        }
    }
}
