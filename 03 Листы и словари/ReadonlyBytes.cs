using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using static hashes.ReadonlyBytesTests;

namespace hashes
{
    // TODO: Создайте класс ReadonlyBytes
    public class ReadonlyBytes : IEnumerable<byte>
    {
        readonly byte[] mas;
        const int FNV_prime = 16777619;
        readonly int hashCode;
        public int Length { get; }
        public byte this[int index]
        {
            get => mas[index];
        }

        public override bool Equals(object obj)
        {
            if (obj is not ReadonlyBytes || obj.GetType() != typeof(ReadonlyBytes)) return false;
           
            var readonlyBytes = obj as ReadonlyBytes;

            if (Length != readonlyBytes.Length) return false;

            for (int i = 0; i < Length; i++)
                if (mas[i] != readonlyBytes[i])
                    return false;

            return true;
        }

        public override int GetHashCode()
        {
            return hashCode;
        }
        
        public ReadonlyBytes(params byte[] array)
        {
            if (array is null)
            {
                throw new ArgumentNullException();
            }

            Length = array.Length;

            this.mas = new byte[array.Length];
           
            array.CopyTo(mas, 0);

            unchecked
            {
                foreach (var vr in mas)
                {
                    hashCode *= FNV_prime;
                    hashCode ^= vr;
                }
            }
        }

        public override string ToString()
        {
            string str = "[";
            if(mas.Length == 1)
            {
                str += mas[0];
            }
            if(mas.Length > 1)
            {
                for (int i = 0; i < mas.Length; i++)
                {
                    str += mas[i];
                    if (i != mas.Length - 1)
                        str += ", ";
                }
            }

            str += "]";
            return str;
        }

        public IEnumerator<byte> GetEnumerator()
        {
            for(int i = 0; i < mas.Length; i++)
                yield return mas[i];
        }
      
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
