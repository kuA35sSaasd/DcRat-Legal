﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace DcRat.Helper.StreamLibrary.src
{
    public class JpgCompression
    {
        private EncoderParameter parameter;
        private ImageCodecInfo encoderInfo;
        private EncoderParameters encoderParams;

        public JpgCompression(int Quality)
        {
            parameter = new EncoderParameter(Encoder.Quality, Quality);
            encoderInfo = GetEncoderInfo("image/jpeg");
            encoderParams = new EncoderParameters(2);
            encoderParams.Param[0] = parameter;
            encoderParams.Param[1] = new EncoderParameter(Encoder.Compression, (long)2);
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (encoderParams != null)
                {
                    encoderParams.Dispose();
                }
            }
        }

        public byte[] Compress(Bitmap bmp)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bmp.Save(stream, encoderInfo, encoderParams);
                return stream.ToArray();
            }
        }
        public void Compress(Bitmap bmp, ref Stream TargetStream)
        {
            bmp.Save(TargetStream, encoderInfo, encoderParams);
        }

        private ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
            int num2 = imageEncoders.Length - 1;
            for (int i = 0; i <= num2; i++)
            {
                if (imageEncoders[i].MimeType == mimeType)
                {
                    return imageEncoders[i];
                }
            }
            return null;
        }
    }
}
