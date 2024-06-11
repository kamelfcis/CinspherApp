using CinspherApp.Controls;
using CinspherApp.Models;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CinspherApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeatsPage : ContentPage
    {
        public SeatsPage(Show show, string image, string title)
        {
            InitializeComponent();
            SelectedTicket = show;
            Image = image;
            Title = title;
            InitializeSeats();
            BindingContext = this;
        }

        public Show SelectedTicket { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }

        private readonly Dictionary<int, bool> seatData = new Dictionary<int, bool>();
        private readonly SKPaint availablePaint = new SKPaint { Style = SKPaintStyle.Stroke, Color = SKColor.Parse("#343352") };
        private readonly SKPaint reservedPaint = new SKPaint { Style = SKPaintStyle.StrokeAndFill, Color = SKColor.Parse("#ED1C24") };
        private readonly SKPaint yourSeatPaint = new SKPaint { Style = SKPaintStyle.StrokeAndFill, Color = SKColor.Parse("#ECCA69") };
        private readonly SKPaint textPaint = new SKPaint { TextSize = 40, Color = SKColor.Parse("#343352") };

        private void InitializeSeats()
        {
            for (int i = 0; i < SelectedTicket.Seats.Count; i++)
            {
                seatData[i] = SelectedTicket.Seats[i].IsBooked;
            }
        }

        private void SKCanvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            var info = e.Info;
            canvas.Clear(SKColors.Transparent);

            const int itemHeight = 40;
            const int itemWidth = 40;
            const int margin = 20;
            const int cornerRadius = 4;

            // Calculate total height of seat layout
            var totalRows = (seatData.Count + GetColumnCount(0) - 1) / GetColumnCount(0);
            var totalHeight = totalRows * (itemHeight + margin) - margin;

            // Calculate offset to center the layout vertically
            var startY = (info.Height - totalHeight) / 2;
            var y = startY;

            var row = -1;
            var items = 0;
            var startX = 0; // Initialize startX outside the loop

            for (int i = 0; i < seatData.Count; i++)
            {
                if (items == 0)
                {
                    row++;
                    items = GetColumnCount(row);
                    // Calculate the starting X position to center the seats in the row
                    var totalRowWidth = items * itemWidth + (items - 1) * margin;
                    startX = (int)((info.Width - totalRowWidth) / 2); // Explicit cast to int
                    y = startY + row * (itemHeight + margin);
                }

                var x = startX + (GetColumnCount(row) - items) * (itemWidth + margin);
                var seatPaint = GetSeatPaint(seatData[i]);
                canvas.DrawRoundRect(new SKRoundRect(new SKRect(x, y, x + itemWidth, y + itemHeight), cornerRadius), seatPaint);

                items--;

                if (items == 0)
                {
                    canvas.DrawText((row + 1).ToString(), 0, y + itemHeight / 2 + margin / 2, textPaint);
                }
            }
        }

        private SKPaint GetSeatPaint(bool isBooked)
        {
            return isBooked ? reservedPaint : availablePaint;
        }

        private int GetColumnCount(int row)
        {
            switch (row)
            {
                case 0:
                    return 8;
                case 1:
                case 9:
                    return 10;
                case 2:
                case 3:
                case 8:
                    return 12;
                default:
                    return 14;
            }
        }
        List<int> selectedseatss = new List<int>();
        private void SKCanvasView_Touch(object sender, SKTouchEventArgs e)
        {
            if (e.ActionType == SKTouchAction.Pressed)
            {
                var canvasView = sender as SKCanvasView;
                var info = canvasView.CanvasSize;

                const int itemHeight = 40;
                const int itemWidth = 40;
                const int margin = 20;

                var totalRows = (seatData.Count + GetColumnCount(0) - 1) / GetColumnCount(0);
                var totalHeight = totalRows * (itemHeight + margin) - margin;

                var startY = (info.Height - totalHeight) / 2;
                var y = startY;

                var row = -1;
                var items = 0;
                var startX = 0; // Initialize startX outside the loop

                for (int i = 0; i < seatData.Count; i++)
                {
                    if (items == 0)
                    {
                        row++;
                        items = GetColumnCount(row);
                        var totalRowWidth = items * itemWidth + (items - 1) * margin;
                        startX = (int)((info.Width - totalRowWidth) / 2); // Explicit cast to int
                        y = startY + row * (itemHeight + margin);
                    }

                    var x = startX + (GetColumnCount(row) - items) * (itemWidth + margin);
                    var rect = new SKRect(x, y, x + itemWidth, y + itemHeight);

                    if (rect.Contains(e.Location))
                    {
                        seatData[i] = !seatData[i];
                        selectedseatss.Add(i);
                        canvasView.InvalidateSurface();
                        break;
                    }

                    items--;
                }

                e.Handled = true;
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var customAlert = new CustomAlertView();
            await Application.Current.MainPage.Navigation.PushModalAsync(new ContentPage
            {
                Content = customAlert,
                BackgroundColor = Color.FromRgba(0, 0, 0, 0.5)
            });

        }
    }
}
