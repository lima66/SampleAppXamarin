using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppGloboXamarin.Pages.Utilities
{
    public class ToolbarButton : ContentView
    {
        public Button btn;
        public Image img;

        public ToolbarButton(string imageSource)
        {
            RelativeLayout rL = new RelativeLayout();

            img = new Image
            {
                Source = imageSource,
                BackgroundColor = Color.Transparent
            };

            rL.Children.Add(img,
                Constraint.RelativeToParent((parent) => {
                    return 0;
                }),
                Constraint.RelativeToParent((parent) => {
                    return 0;
                }),
                Constraint.RelativeToParent((parent) => {
                    return parent.Width;
                }),
                Constraint.RelativeToParent((parent) => {
                    return parent.Height;
                })
            );


            btn = new Button
            {
                BackgroundColor = Color.Transparent,
                BorderRadius = 0
            };
            rL.Children.Add(btn,
                Constraint.RelativeToParent((parent) => {
                    return 0;
                }),
                Constraint.RelativeToParent((parent) => {
                    return 0;
                }),
                Constraint.RelativeToParent((parent) => {
                    return parent.Width;
                }),
                Constraint.RelativeToParent((parent) => {
                    return parent.Height;
                })
            );

            this.Content = rL;
        }

        public void setImage(string path)
        {
            img.Source = path;
        }
    }
}
