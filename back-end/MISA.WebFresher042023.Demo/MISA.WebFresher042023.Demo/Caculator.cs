namespace MISA.WebFresher042023.Demo
{
    /// <summary>
    /// Lớp cộng trừ nhân chia
    /// </summary>
    /// Created by: tmthuc(09/06/2023)
    public class Caculator
    {
        /// <summary>
        /// Hàm cộng 2 số nguyên
        /// </summary>
        /// <param name="a">Số hạng 1</param>
        /// <param name="b">Số hạng 2</param>
        /// <returns>Tổng 2 số nguyên</returns>
        /// Created by: tmthuc(09/06/2023)
        public long Add(int a, int b)
        {
            return (long)a + b;
        }
        /// <summary>
        /// Hàm trừ 2 số nguyên
        /// </summary>
        /// <param name="a">Số hạng 1</param>
        /// <param name="b">Số hạng 2</param>
        /// <returns>Trừ 2 số nguyên</returns>
        public long Sub(int a, int b) { 
            return (long)a - b;
        }

        /// <summary>
        /// Hàm nhân 2 số nguyên
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>

        public long Mul(int a, int b) { 
           return (long)a * b;
        }
        /// <summary>
        /// Hàm chia 2 số nguyên
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <exception cref="NotImplementedException"></exception>

        public double Div(int a, int b) {
            if (b == 0)
            {

                throw new Exception("không được chia cho 0");
            }
            return (double)a / b;
        }



    }
}
