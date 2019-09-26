using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxy.Taurus.AuthorizationAPI.VerificationCode
{
    /// <summary>
    /// 图片验证码
    /// </summary>
    public class VerificationCodeImage
    {
        /// <summary>
        /// 颜色
        /// </summary>
        private static Color[] colorArray = { Color.Black, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };

        /// <summary>
        /// 字体
        /// </summary>
        private static string[] fonts = { "lnk Free", "Segoe Print", "Comic Sans MS", "MV Boli", "华文行楷" };

        /// <summary>  
        /// 随机字符
        /// </summary>  
        /// <param name="number">字符数量</param>  
        /// <returns>随机字符</returns>  
        private static string RandomCode(int number)
        {
            var str = "3456789ABCDEFGHJKLMNPQRSTUVWXY";
            char[] str_char_arrary = str.ToArray();
            Random rand = new Random();
            HashSet<string> hs = new HashSet<string>();
            bool randomBool = true;
            while (randomBool)
            {
                if (hs.Count == number)
                    break;
                int rand_number = rand.Next(str_char_arrary.Length);
                hs.Add(str_char_arrary[rand_number].ToString());
            }
            string code = string.Join("", hs);
            return code;
        }

        /// <summary>
        /// 画干扰线
        /// </summary>
        private static void DrawInterferenceLines(Graphics g, int lineCount, int height = 80, int width = 200)
        {
            for (int i = 0; i < lineCount; i++)
            {
                Random random = new Random();
                Pen p = new Pen(colorArray[random.Next(colorArray.Length)]);
                g.DrawLine(p, random.Next(width), random.Next(height), random.Next(width), random.Next(height));
            }
        }

        /// <summary>
        /// 画干扰点
        /// </summary>
        private static void DrawInterferencePoins(Bitmap bitmap, int pointCount, int height = 80, int width = 200)
        {
            for (int i = 0; i < pointCount; i++)
            {
                Random random = new Random();
                int x = random.Next(1, width);
                int y = random.Next(1, height);
                Color color = colorArray[random.Next(1, colorArray.Length)];
                bitmap.SetPixel(x, y, color);
            }
        }

        /// <summary>
        /// 获取尺寸
        /// </summary>
        private static SizeF GetCodeSize(Graphics g, Font font, string code)
        {
            StringFormat sf = new StringFormat();
            sf.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
            return g.MeasureString(code, font, 1000, StringFormat.GenericTypographic);
        }

        /// <summary>
        /// 画字符
        /// </summary>
        private static string DrawString(Graphics g, int numbers = 4, int height = 80, int width = 200)
        {
            Random random = new Random();
            string code = RandomCode(numbers);
            int codeLength = code.Length;

            for (int i = 0; i < codeLength; i++)
            {
                Font font = new Font(fonts[random.Next(fonts.Length)], 35, FontStyle.Bold);
                Brush brush = new SolidBrush(colorArray[random.Next(colorArray.Length)]);
                string word = code.Substring(i, 1);
                SizeF codeSize = GetCodeSize(g, font, word);
                int codeHeight = Convert.ToInt32(codeSize.Height);
                int codeWidth = Convert.ToInt32(codeSize.Width);

                int y = 0;
                if (height - codeSize.Height > 0)
                {
                    int maxOffsetY = height - codeHeight;

                    if (maxOffsetY > 0)
                    {
                        y = random.Next(0, maxOffsetY);
                    }
                }

                int unitX = width / codeLength;
                int x = i * unitX;

                if (i == 0)
                {
                    int maxOffsetX = (unitX - codeWidth) + 20;

                    if (maxOffsetX > 0)
                    {
                        x = random.Next(0, maxOffsetX);
                    }
                }
                else if (i == codeLength - 1)
                {
                    int maxOffsetX = unitX - codeWidth;

                    if (maxOffsetX > 0)
                    {
                        x = x + random.Next(0, maxOffsetX);
                    }
                }
                else
                {
                    int maxOffsetX = unitX - codeWidth + 20;

                    if (maxOffsetX > 0)
                    {
                        x = x + random.Next(0, maxOffsetX);
                    }
                }

                g.DrawString(word, font, brush, x, y);
            }

            return code;
        }

        /// <summary>  
        /// 创建字符验证码图片
        /// </summary>  
        /// <param name="numbers">生成位数（默认4位）</param>  
        /// <param name="height">图片高度</param>  
        /// <param name="width">图片宽度</param>  
        public static Task<VerificationCodeModel> CreateCode(int numbers = 4, int height = 80, int width = 200)
        {
            Bitmap bitmapImg = new Bitmap(width, height);
            //画干扰点
            DrawInterferencePoins(bitmapImg, width * 5, height, width);
            //生成画布
            Graphics g = Graphics.FromImage(bitmapImg);
            //画背景色
            g.FillRectangle(Brushes.LightGray, 0, 0, width, height);
            //画字符
            string code = DrawString(g);
            //画干扰线
            DrawInterferenceLines(g, 12, 80, 200);

            MemoryStream ms = new MemoryStream();
            bitmapImg.Save(ms, ImageFormat.Jpeg);
            g.Dispose();
            bitmapImg.Dispose();
            ms.Dispose();
            VerificationCodeModel imageModel = new VerificationCodeModel();
            imageModel.ImageBase64Str = "data:image/jpg;base64," + Convert.ToBase64String(ms.GetBuffer());
            imageModel.Code = code;
            return Task.FromResult(imageModel);
        }
    }

    /// <summary>
    /// 验证码数据模型
    /// </summary>
    public class VerificationCodeModel
    {
        /// <summary>
        /// 图片
        /// </summary>
        public string ImageBase64Str { get; set; } = "";

        /// <summary>
        /// 位置信息
        /// </summary>
        public string Code { get; set; }
    }
}
