using AppGloboXamarin.Helpers;
using AppGloboXamarin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppGloboXamarin.Pages.MenuApp
{
    public class MenuPage : ContentPage
    {
        public ListView Menu { get; set; }
        public Xamarin.Forms.MasterDetailPage md;


        public MenuPage()
        {         

            var searchBar = new Xamarin.Forms.SearchBar();
            searchBar.TextChanged += (sender, e) =>
            {                
                //search                                 
            };

            Icon = UtilityResources.icon_Test;
            Title = "menu"; // The Title property must be set.
            BackgroundColor = Color.FromHex("FFFFFF");
            Menu = new ListView
            {
                RowHeight = 80,
                Footer = new BoxView { BackgroundColor = Color.Transparent, HeightRequest = 95, MinimumHeightRequest = 95 },
                ItemTemplate = new DataTemplate(() =>
                {
                    Color textColor = Color.Black;

                    Label nameLabel = new Label();
                    nameLabel.TextColor = textColor;
                    nameLabel.SetBinding(Label.TextProperty, "Description");                    

                    var disclosure = new Image()
                    {
                        BackgroundColor = Color.Transparent,
                        Aspect = Aspect.AspectFit,
                        Source = UtilityResources.freccia_dx
                    };

                    int dimPpoint = 30;
                    int dimDisclosure = 12;

                    RelativeLayout cell = new RelativeLayout();
                    cell.Children.Add(nameLabel,
                            Constraint.RelativeToParent((parent) =>
                            {
                                return dimPpoint;
                            }),
                            Constraint.RelativeToParent((parent) =>
                            {
                                return parent.Height / 3;
                            }),
                            Constraint.RelativeToParent((parent) =>
                            {
                                return parent.Width - dimPpoint - 20;
                            }),
                            Constraint.RelativeToParent((parent) =>
                            {
                                return parent.Height / 3;
                            })
                        );                                      

                    cell.Children.Add(disclosure,
                            Constraint.RelativeToParent((parent) =>
                            {
                                return parent.Width - 20;
                            }),
                            Constraint.RelativeToParent((parent) =>
                            {
                                return (parent.Height - 20) / 2;
                            }),
                            Constraint.RelativeToParent((parent) =>
                            {
                                return dimDisclosure;
                            }),
                            Constraint.RelativeToParent((parent) =>
                            {
                                return 20;
                            })
                        );

                    return new ViewCell
                    {
                        View = cell

                    };
                })
            };

            var backLabel = new BoxView
            {
                BackgroundColor = Color.FromHex("EDEBE6")
            };

            //Titolo
            var menuLabel = new ContentView
            {
                Padding = new Thickness(0, 0, 0, 0),
                Content = new Label
                {
                    TextColor = Color.FromHex("005DAB"),
                    Text = "Globo",
                    FontSize = 21,
                    BackgroundColor = Color.Transparent,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.End
                }
            };


            #region AGGIONRAMENTO
            RelativeLayout rlAggiornamento = new RelativeLayout();

            var imgAgg = new Image()
            {
                BackgroundColor = Color.Transparent,
                Aspect = Aspect.AspectFit,
                Source = UtilityResources.icon_Test
            };

            var nameLabelAgg = new Label()
            {
                FontSize = 15,
                TextColor = Color.Black, //Color.FromHex ("005DAB"),
                BackgroundColor = Color.Transparent,
                Text = UtilityStringResources.nameLabelAgg,
                VerticalTextAlignment = TextAlignment.Center
            };

            var lineAgg = new BoxView()
            {
                BackgroundColor = Color.Gray
            };

            var btnAggiornamento = new Button()
            {
                BackgroundColor = Color.Transparent
            };
            btnAggiornamento.Clicked += BtnUpdate_Clicked;

            rlAggiornamento.Children.Add(imgAgg,
                Constraint.RelativeToParent((parent) => {
                    return parent.Height / 4;
                }),
                Constraint.RelativeToParent((parent) => {
                    return parent.Height / 4;
                }),
                Constraint.RelativeToParent((parent) => {
                    return parent.Width / 6;
                }),
                Constraint.RelativeToParent((parent) => {
                    return parent.Height / 2;
                })
            );
            rlAggiornamento.Children.Add(nameLabelAgg,
                Constraint.RelativeToParent((parent) => {
                    return parent.Width / 6 + 20;
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
            rlAggiornamento.Children.Add(lineAgg,
                Constraint.RelativeToParent((parent) => {
                    return parent.Height / 4;
                }),
                Constraint.RelativeToParent((parent) => {
                    return 0;
                }),
                Constraint.RelativeToParent((parent) => {
                    return parent.Width;
                }),
                Constraint.RelativeToParent((parent) => {
                    return 0.5;
                })
            );

            rlAggiornamento.Children.Add(btnAggiornamento,
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

            var aggiornamentoLabel = new ContentView
            {
                Padding = new Thickness(0, 0, 0, 0),
                Content = rlAggiornamento
            };
            #endregion

            StackLayout st = new StackLayout();
            st.Children.Add(searchBar);
            st.Children.Add(Menu);

            RelativeLayout rLmenu = new RelativeLayout();

            float wTestata = 375;
            float hTestata = 64;
            float pTestata = (App.ScreenWidth * hTestata) / wTestata;

            if(Device.Idiom == TargetIdiom.Phone)
            {              

                rLmenu.Children.Add(backLabel,
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
                        return pTestata;
                    })
                );
                rLmenu.Children.Add(menuLabel,
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
                        return pTestata - 5;
                    })
                );

                rLmenu.Children.Add(st,
                    Constraint.RelativeToParent((parent) => {
                        return 0;
                    }),
                    Constraint.RelativeToParent((parent) => {
                        return hTestata;
                    }),
                    Constraint.RelativeToParent((parent) => {
                        return parent.Width;
                    }),
                    Constraint.RelativeToParent((parent) => {
                        return parent.Height - pTestata - 100;
                    })
                );

                rLmenu.Children.Add(aggiornamentoLabel,
                    Constraint.RelativeToParent((parent) => {
                        return 0;
                    }),
                    Constraint.RelativeToParent((parent) => {
                        return parent.Height - 50;
                    }),
                    Constraint.RelativeToParent((parent) => {
                        return parent.Width;
                    }),
                    Constraint.RelativeToParent((parent) => {
                        return 50;
                    })
                );
            }
            else
            {
                rLmenu.Children.Add(backLabel,
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
                      return parent.Height * 10 / 100;
                  })
               );

                rLmenu.Children.Add(menuLabel,
                    Constraint.RelativeToParent((parent) => {
                        return 0;
                    }),
                    Constraint.RelativeToParent((parent) => {
                        return 15;
                    }),
                    Constraint.RelativeToParent((parent) => {
                        return parent.Width;
                    }),
                    Constraint.RelativeToParent((parent) => {
                        return backLabel.Height - 15;
                    })
                );

                rLmenu.Children.Add(st,
                    Constraint.RelativeToParent((parent) =>
                    {
                        return 0;
                    }),
                    Constraint.RelativeToParent((parent) =>
                    {
                        return backLabel.Height + 20;
                    }),
                    Constraint.RelativeToParent((parent) =>
                    {
                        return parent.Width;
                    }),
                    Constraint.RelativeToParent((parent) =>
                    {
                        return parent.Height - 100;
                    })
                );

                rLmenu.Children.Add(aggiornamentoLabel,
                    Constraint.RelativeToParent((parent) =>
                    {
                        return 0;
                    }),
                    Constraint.RelativeToParent((parent) =>
                    {
                        return parent.Height - 50;
                    }),
                    Constraint.RelativeToParent((parent) =>
                    {
                        return parent.Width;
                    }),
                    Constraint.RelativeToParent((parent) =>
                    {
                        return 50;
                    })
                );
            }
        

            Content = rLmenu;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();         

        }

        private void BtnUpdate_Clicked(object sender, EventArgs e)
        {

            DisplayAlert("ALERT","Pulsante Aggiornamento premuto","OK");
            //Type actionType = Type.GetType("AppIncaricate.Pages.Impostazioni.ImpostazioniPage");

            //if (actionType == null)
            //    return;

            //Page displayPage = (Page)Activator.CreateInstance(actionType, -1);

            //((AppGloboXamarin.Pages.Commons.GloboContentPage)displayPage).md = this.md;

            //((AppGloboXamarin.Pages.Commons.GloboContentPage)displayPage).IsRoot(false);

            //md.Detail.Navigation.PushAsync(displayPage);


            //md.IsPresented = false;
        }
    }
}
