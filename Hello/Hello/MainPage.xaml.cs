using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hello
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {

            {
                StackLayout stackLayout = new StackLayout();
                // Loop through the Color structure fields.
                foreach (FieldInfo info in typeof(Color).GetRuntimeFields())
                {
                    // Skip the obsolete (i.e. misspelled) colors.
                    if (info.GetCustomAttribute<ObsoleteAttribute>() != null)
                        continue;
                    if (info.IsPublic &&
                    info.IsStatic &&
                    info.FieldType == typeof(Color))
                    {
                        stackLayout.Children.Add(
                        CreateColorView((Color)info.GetValue(null), info.Name));
                    }
                }
                // Loop through the Color structure properties.
                foreach (PropertyInfo info in typeof(Color).GetRuntimeProperties())
                {
                    MethodInfo methodInfo = info.GetMethod;
                    if (methodInfo.IsPublic &&
                    methodInfo.IsStatic &&
                    methodInfo.ReturnType == typeof(Color))
                    {
                        stackLayout.Children.Add(
                        CreateColorView((Color)info.GetValue(null), info.Name));
                    }
                }
                Padding = new Thickness(5, Device.OnPlatform(20, 5, 5), 5, 5);
                // Put the StackLayout in a ScrollView.
                Content = new ScrollView
                {
                    Content = stackLayout
                };
            }
           
 Label CreateColorView(Color color, string name)
            {
                Color backgroundColor = Color.Default;
                if (color != Color.Default)
                {
                    // Standard luminance calculation.
                    double luminance = 0.30 * color.R +
                    0.59 * color.G +
                    0.11 * color.B;
                    backgroundColor = luminance > 0.5 ? Color.Black : Color.White;
                }
                // Create the Label.
                return new Label
                {
                    Text = name,
                    TextColor = color,
                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                    BackgroundColor = backgroundColor
                };
            }
        }



        private static View CreateColorView(Color color, string name)
            {

                return new Frame
                {
                     OutlineColor = Color.Accent,
                     Padding = new Thickness(5),
                     Content = new StackLayout
                     { 
                     Orientation = StackOrientation.Horizontal,
                     Spacing = 15,
                     Children =
                        {
                             new BoxView
                             {
                                 Color = color
                                 },
                                new Label
                                {
                                     Text = name,
                                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                                     FontAttributes = FontAttributes.Bold,
                                    VerticalOptions = LayoutOptions.Center,
                                    HorizontalOptions = LayoutOptions.StartAndExpand
                                     },
                                        new StackLayout
                                         {
                                         Children =
                                        {
                                         new Label
                                        {
                                         Text = String.Format("{0:X2}-{1:X2}-{2:X2}",
                                         (int)(255 * color.R),
                                         (int)(255 * color.G),
                                        (int)(255 * color.B)),
                                         VerticalOptions = LayoutOptions.CenterAndExpand,
                                        IsVisible = color != Color.Default
                                         },
                                        new Label
                                        {
                                             Text = String.Format("{0:F2}, {1:F2}, {2:F2}",
                                             color.Hue,
                                color.Saturation,
                                color.Luminosity),
                                 VerticalOptions = LayoutOptions.CenterAndExpand,
                                 IsVisible = color != Color.Default
                                 }
                                 },
                                HorizontalOptions = LayoutOptions.End
                                 }
                                 }
                                     }
                                 };
                                            }
                                        }

                                    }

    


