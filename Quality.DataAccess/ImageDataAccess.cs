using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Quality.Common;
using Quality.Common.AppSettings;
using Microsoft.AspNetCore.Http;

namespace Quality.DataAccess
{
    public class ImageDataAccess : IImageDataAccess
    {
        private readonly string _filesPath;

        public ImageDataAccess(IOptions<Config> config)
        {
            _filesPath = config.Value.FilesPath;
        }

        public async Task<Result> WriteFile(IFormFile file)
        {
            var result = new Result();
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                string fileName = Guid.NewGuid() + extension;
                var path = $"{_filesPath}\\{fileName}";

                using (var bits = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(bits);
                }

                result.Message = fileName;
                result.IsSuccess = true;
            }
            catch (Exception e)
            {
                result.IsSuccess = false;
                result.Message = e.Message;
            }
            return result;
        }

        public async Task<Result> WriteFileToPath(IFormFile file, string path)
        {
            var result = new Result();
            try
            {
                string fileName = Guid.NewGuid().ToString() + ".jpg";
                if (!Directory.Exists($"{_filesPath}\\{path}"))
                    Directory.CreateDirectory($"{_filesPath}\\{path}");
                path = $"{_filesPath}\\{path}\\{fileName}";

                using (var bits = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(bits);
                }

                if (File.Exists(path))
                {
                    ResizeImage(path);
                    result.Message = fileName;
                    result.IsSuccess = true;
                }

            }
            catch (Exception e)
            {
                result.IsSuccess = false;
                result.Message = e.Message;
            }
            return result;
        }

        /// <summary>
        /// resize the image file to 4 sizes
        /// xs,sm,md,lg
        /// </summary>
        /// <param name="path"></param>
        public void ResizeImage(string path)
        {
            if (File.Exists(path))
            {
                FileInfo fileInfo = new FileInfo(path);
                var filesCount = fileInfo.Directory?.GetFiles().Length;
                if (filesCount > 1)
                    filesCount = ((filesCount - 1) / 5) + 1;

                using (FileStream imageStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using (var img = new Bitmap(imageStream))
                    {
                        float width = img.Width;
                        float height = img.Height;
                        float aspectRatio = height / width;

                        List<int> sizes = new List<int> { 70, 120, 600, 1000 };
                        foreach (var newWidth in sizes)
                        {
                            int newHeight = Convert.ToInt32(aspectRatio * newWidth);

                            using (Bitmap newImage = new Bitmap(newWidth, newHeight))
                            {
                                using (Graphics gr = Graphics.FromImage(newImage))
                                {
                                    gr.SmoothingMode = SmoothingMode.HighQuality;
                                    gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                    gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                    gr.DrawImage(img, new Rectangle(0, 0, newWidth, newHeight));
                                    gr.Dispose();
                                }
                                string size = "xs";
                                if (newWidth == 120)
                                    size = "sm";
                                else if (newWidth == 600)
                                    size = "md";
                                else if (newWidth == 1000)
                                    size = "lg";

                                string outputFileName = $@"{fileInfo.Directory?.ToString()}\{filesCount}_{size}.jpg";
                                using (MemoryStream memory = new MemoryStream())
                                {
                                    using (FileStream fs = new FileStream(outputFileName, FileMode.Create, FileAccess.ReadWrite))
                                    {
                                        newImage.Save(memory, ImageFormat.Jpeg);
                                        byte[] bytes = memory.ToArray();
                                        fs.Write(bytes, 0, bytes.Length);
                                    }
                                }
                            }
                        }
                        img.Dispose();
                    }
                }
            }
        }
    }
}
