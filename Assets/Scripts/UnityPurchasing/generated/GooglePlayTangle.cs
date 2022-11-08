// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("HHTi7ptXIIZ1cA4Vr+NuL8KvyQMG+P//jBkmBCa1opJ0ajQiUQt734fj3innmU5NkN/kt30fmu/7Ue859Heb0c3jN4E5fn9e+yYSpEq0/B1omENIRFgj6AnMYnPeK0U97Hh/BBOMwnvSbHaLLa2MVyKzs8ZVTqxU0r0ffsqSSJPpe0NRWkVj1lLflH4yyRwCAXTtx6YjIvMphNX13w1qlCt03td/UG+ZFWBPq7tyjwxZlPuqcprRVmalyqvjFBwtBHoTLmjKG/Tr3ZESrkuXydzlZRjjWAylMVEjjvlLyOv5xM/A40+BTz7EyMjIzMnKdKTRd4FSNINClf7KSRoN3LJAW1RLyMbJ+UvIw8tLyMjJflTRWHbFMbTAyA471AmLcMvKyMnI");
        private static int[] order = new int[] { 2,12,9,3,4,6,6,11,11,10,13,13,13,13,14 };
        private static int key = 201;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
