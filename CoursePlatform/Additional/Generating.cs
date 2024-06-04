
using CoursePlatform.Common.Entities;
using CoursePlatform.Common.Migrations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using System.Security.Claims;

namespace CoursePlatform.Common.Additional
{
    public static class Generating
    {
        public static async void GenerateCertificate(this Certificate certificate, CoursePlatformContext context)
        {
            var shablon = "./wwwroot/image/shablon.png";
            var certificatesDirectory = "./wwwroot/image/Certificates";

            // Проверяем существование директории для сертификатов
            if (!Directory.Exists(certificatesDirectory))
            {
                Directory.CreateDirectory(certificatesDirectory);
            }

            var currentCert = context.Set<Certificate>()
                .Include(c => c.CourseEnrollment).ThenInclude(ce => ce.Student).ThenInclude(s => s.Profile)
                .Include(c => c.CourseEnrollment).ThenInclude(ce => ce.Course)
                .FirstOrDefault(c => c.Id == certificate.Id);

            FontCollection collection = new();
            collection.Add("./wwwroot/fonts/TeXGyreAdventor-Bold.ttf");
            collection.Add("./wwwroot/fonts/TeXGyreAdventor-Regular.ttf");

            using var frame = SixLabors.ImageSharp.Image.Load(shablon);


            frame.Mutate(ctx => ctx.Fill(Color.FromRgb(0x79, 0x6a, 0x69), GetPath($"выдан", 500, 310, collection, 30, true, 700)));
            frame.Mutate(ctx => ctx.Fill(Color.FromRgb(0x79, 0x6a, 0x69), GetPath($"пользователю", 600, 310, collection, 30, true, 700)));
            frame.Mutate(ctx => ctx.Fill(Color.FromRgb(0x24, 0x43, 0x73), GetPath($"{currentCert.CourseEnrollment.Student.Profile.Surname}", 360 , 350, collection, 100, true, 1000)));
            frame.Mutate(ctx => ctx.Fill(Color.FromRgb(0x24, 0x43, 0x73), GetPath($"{currentCert.CourseEnrollment.Student.Profile.Name}", 360, 450, collection, 100, true, 1000)));
            frame.Mutate(ctx => ctx.Fill(Color.FromRgb(0x79, 0x6a, 0x69), GetPath($"за окончание курса «{currentCert.CourseEnrollment.Course.CourseTitle}»", 330, 585, collection, 25, true, 720)));
            
            frame.Mutate(ctx => ctx.Fill(Color.FromRgb(0x79, 0x6a, 0x69), GetPath($"{currentCert.CourseEnrollment.Student.UserName}", 340, 730, collection, 28, false, 650)));
            frame.Mutate(ctx => ctx.Fill(Color.FromRgb(0x24, 0x43, 0x73), GetPath($"имя пользователя", 325, 770, collection, 26, false, 650)));
            
            frame.Mutate(ctx => ctx.Fill(Color.FromRgb(0x79, 0x6a, 0x69), GetPath($"{currentCert.IssueDate.Value.ToShortDateString()}", 765, 730, collection, 28, true, 650)));
            frame.Mutate(ctx => ctx.Fill(Color.FromRgb(0x24, 0x43, 0x73), GetPath($"дата вручения", 745, 770, collection, 26, true, 650)));

            var outputPath = $"./wwwroot/image/Certificates/{certificate.CourseEnrollment.Student.UserName}__{certificate.CourseEnrollment.Course.CourseTitle.Replace(" ", "")}.png";
            await frame.SaveAsync(outputPath, frame.DetectEncoder(shablon));
        } 

        public static IPathCollection GetPath(string text, float x, float y, FontCollection collection, int size, bool isBold = false, int length = 400)
        {
            var fnt = isBold ? collection.Families.First() : collection.Families.Last();

            var textOptions = new TextOptions(fnt.CreateFont(size)) { WrappingLength = length };
            var pathBuilder = new PathBuilder();

            pathBuilder.SetOrigin(new PointF(x, y));
            pathBuilder.AddLine(new PointF(0, 0), new PointF(1000, 0));

            IPath path = pathBuilder.Build();

            return TextBuilder.GenerateGlyphs(text, path, textOptions);
        }
    }
}
