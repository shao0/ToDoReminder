using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ToDoReminder.Client.Common.Extensions
{
    public static class ImageExtension
    {
        /// <summary>
        /// 根据图像路径转换为图像二进制byte
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static byte[] FilePathToBytes(this string path)
        {
            byte[] array;
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                array = new byte[fileStream.Length];
                fileStream.Read(array, 0, array.Length);
            }
            return array;
        }
        /// <summary>
        /// 根据图像二进制byte数据转换为图像位图
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static BitmapImage BytesToBitmapImage(this byte[] bytes)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.CreateOptions = BitmapCreateOptions.DelayCreation;
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new MemoryStream(bytes);
            bitmapImage.EndInit();
            bitmapImage.Freeze();
            return bitmapImage;
        }
        /// <summary>
        /// 根据图像二进制byte数据转换为图像位图
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static BitmapImage UriToBitmapImage(this string uri)
        {
            return uri.UriToBytes().BytesToBitmapImage();
        }
        public static byte[] UriToBytes(this string uri)
        {
            byte[] result;
            using (var stream = Application.GetResourceStream(new Uri(uri, UriKind.Absolute)).Stream)
            {
                result = new byte[stream.Length];
                stream.Read(result, 0, result.Length);
            }
            return result;
        }
        /// <summary>
        /// 根据图像路径转换为图像位图
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static BitmapImage FilePathToBitmapImage(this string path)
        {
            return path.FilePathToBytes().BytesToBitmapImage();
        }
        /// <summary>
        /// 图像转Base64字符串
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ImagePathToBase64String(this string path)
        {
            return Convert.ToBase64String(path.FilePathToBytes());
        }
        /// <summary>
        /// Base64转图像
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static BitmapImage Base64StringToBitmapImage(this string base64)
        {
            return Convert.FromBase64String(base64).BytesToBitmapImage();
        }
    }
}
