// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("lPxqZhPfqA79+IadJ2vmp0onQYuj/FZf99jnEZ3oxyMz+geE0RxzIsNATkFxw0BLQ8NAQEH23FnQ/k25D2tWoW8RxsUYV2w/9ZcSZ3PZZ7H8LFn/Cdq8C8oddkLBkoVUOsjT3JsESvNa5P4DpSUE36o7O07dxiTcWjWX9kIawBth88vZ0s3rXtpXHPb6Elne7i1CI2uclKWM8pum4EKTfLpBlIqJ/GVPLquqe6EMXX1XheIc4BDLwMzQq2CBROr7VqPNtWTw94x8/xNZRWu/CbH299Zzrposwjx0lY5wd3cEka6Mrj0qGvzivKrZg/NXccNAY3FMR0hrxwnHtkxAQEBEQUJjVRmaJsMfQVRt7ZBr0IQtudmrBjxIQIazXIED+ENCQEFA");
        private static int[] order = new int[] { 2,7,7,9,8,6,6,10,11,11,11,12,12,13,14 };
        private static int key = 65;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
