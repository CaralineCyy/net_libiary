/// <summary>
/// ��˵����Assistant
/// �� �� �ˣ��շ�
/// ��ϵ��ʽ��361983679  
/// ������վ��http://www.sufeinet.com/thread-655-1-1.html
/// </summary>
using System;
using System.Drawing.Imaging;
using System.Collections;
using System.Drawing;
using System.IO;

namespace DotNet.Utilities
{
    /// <summary>
    /// ö��,��������ͼģʽ
    /// </summary>
    public enum ThumbnailMod : byte
    {
        /// <summary>
        /// HW
        /// </summary>
        HW,
        /// <summary>
        /// W
        /// </summary>
        W,
        /// <summary>
        /// H
        /// </summary>
        H,
        /// <summary>
        /// Cut
        /// </summary>
        Cut
    };

    /// <summary>
    /// ����ͼƬ��, ��������ͼ,���ˮӡ
    /// </summary>
    public static class PicDeal
    {
        private static Hashtable htmimes = new Hashtable();
        internal static readonly string AllowExt = ".jpe|.jpeg|.jpg|.png|.tif|.tiff|.bmp";
        #region ��������ͼ
        /// <summary>
        /// ��������ͼ
        /// </summary>
        /// <param name="originalImagePath"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static bool MakeThumbnail(string originalImagePath, int width, int height, ThumbnailMod mode)
        {
            string thumbnailPath = originalImagePath.Substring(0, originalImagePath.LastIndexOf('.')) + "s.jpg";
            Image originalImage = Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case ThumbnailMod.HW://ָ���߿����ţ����ܱ��Σ�                
                    break;
                case ThumbnailMod.W://ָ�����߰�����                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case ThumbnailMod.H://ָ���ߣ�������
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case ThumbnailMod.Cut://ָ���߿�ü��������Σ�                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            //�½�һ��bmpͼƬ
            Image bitmap = new Bitmap(towidth, toheight);

            //�½�һ������
            Graphics g = Graphics.FromImage(bitmap);

            //���ø�������ֵ��
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //���ø�����,���ٶȳ���ƽ���̶�
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //��ջ�������͸������ɫ���
            g.Clear(System.Drawing.Color.Transparent);

            //��ָ��λ�ò��Ұ�ָ����С����ԭͼƬ��ָ������
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
                new Rectangle(x, y, ow, oh),
                GraphicsUnit.Pixel);
            bool isok = false;
            try
            {
                //��jpg��ʽ��������ͼ
                bitmap.Save(thumbnailPath, ImageFormat.Jpeg);
                isok = true;
            }
            catch (Exception)
            {
                thumbnailPath = originalImagePath;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
            return isok;
        }
        #endregion

        #region ��ͼƬ������ͼƬˮӡ
        ///// <summary>
        ///// ��ͼƬ������ͼƬˮӡ
        ///// </summary>
        ///// <param name="Path">ԭ������ͼƬ·��</param>
        ///// <param name="Path_syp">���ɵĴ�ͼƬˮӡ��ͼƬ·��</param>
        ///// <param name="Path_sypf">ˮӡͼƬ·��</param>
        /// <summary>
        /// ��ͼƬ������ͼƬˮӡ
        /// </summary>
        /// <param name="Path">ԭ������ͼƬ·��</param>
        /// <param name="Path_sypf">ˮӡͼƬ·��</param>
        public static void AddWaterPic(string Path, string Path_sypf)
        {
            try
            {
                Image image = Image.FromFile(Path);
                Image copyImage = Image.FromFile(Path_sypf);
                Graphics g = Graphics.FromImage(image);
                g.DrawImage(copyImage, new System.Drawing.Rectangle(image.Width - copyImage.Width, image.Height - copyImage.Height, copyImage.Width, copyImage.Height), 0, 0, copyImage.Width, copyImage.Height, System.Drawing.GraphicsUnit.Pixel);
                g.Dispose();

                image.Save(Path + ".temp");
                image.Dispose();
                System.IO.File.Delete(Path);
                File.Move(Path + ".temp", Path);
            }
            catch
            { }
        }
        #endregion

        /// <summary>
        /// ��������
        /// </summary>
        private static void GetImgType()
        {
            htmimes[".jpe"] = "image/jpeg";
            htmimes[".jpeg"] = "image/jpeg";
            htmimes[".jpg"] = "image/jpeg";
            htmimes[".png"] = "image/png";
            htmimes[".tif"] = "image/tiff";
            htmimes[".tiff"] = "image/tiff";
            htmimes[".bmp"] = "image/bmp";
        }


        #region ������ͼƬ�ߴ�
        /// <summary>
        /// ������ͼƬ�ߴ�
        /// </summary>
        /// <param name="width">ԭʼ��</param>
        /// <param name="height">ԭʼ��</param>
        /// <param name="maxWidth">��ͼƬ����</param>
        /// <param name="maxHeight">��ͼƬ����</param>
        /// <returns></returns>
        public static Size ResizeImage(int width, int height, int maxWidth, int maxHeight)
        {
            decimal MAX_WIDTH = (decimal)maxWidth;
            decimal MAX_HEIGHT = (decimal)maxHeight;
            decimal ASPECT_RATIO = MAX_WIDTH / MAX_HEIGHT;

            int newWidth, newHeight;

            decimal originalWidth = (decimal)width;
            decimal originalHeight = (decimal)height;

            if (originalWidth > MAX_WIDTH || originalHeight > MAX_HEIGHT)
            {
                decimal factor;
                // determine the largest factor 
                if (originalWidth / originalHeight > ASPECT_RATIO)
                {
                    factor = originalWidth / MAX_WIDTH;
                    newWidth = Convert.ToInt32(originalWidth / factor);
                    newHeight = Convert.ToInt32(originalHeight / factor);
                }
                else
                {
                    factor = originalHeight / MAX_HEIGHT;
                    newWidth = Convert.ToInt32(originalWidth / factor);
                    newHeight = Convert.ToInt32(originalHeight / factor);
                }
            }
            else
            {
                newWidth = width;
                newHeight = height;
            }

            return new Size(newWidth, newHeight);

        }
        #endregion
    }
}
