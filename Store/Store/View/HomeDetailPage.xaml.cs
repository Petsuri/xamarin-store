﻿
using Store.Model;
using Store.ViewModel;
using Microsoft.Practices.Unity;
using System.Collections.Specialized;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections;
using Store.Domain;
using System;

namespace Store.Ui.View
{
    
    public partial class HomeDetailPage : ContentPage
    {
        private HomeDetailViewModel m_viewModel;
        
        public HomeDetailPage()
        {
            InitializeComponent();
            
            m_viewModel = App.Container.Resolve<HomeDetailViewModel>();
            m_viewModel.addMangaList(App.Container.Resolve<BookPreviewItemListViewModel>(BookCategory.Category.Manga.ToString()));
            m_viewModel.addRecommendationList(App.Container.Resolve<BookPreviewItemListViewModel>(BookCategory.Category.Recommendation.ToString()));
            m_viewModel.loadItems();
            
            BindingContext = m_viewModel;

            IMessageQueue messaging = App.Container.Resolve<IMessageQueue>();
            messaging.Subscribe<BookPreviewItemViewModel, BookPreviewItem>(this, BookPreviewItemViewModel.ShowItemMessage, async (sender, selectedItem) =>
            {

                var books = App.Container.Resolve<IBookRepository>(selectedItem.Category.SelectedCategory.ToString());
                var allPreviewIds = await books.loadAllPreviewItemIds();
                await Navigation.PushAsync(new BookCarouselPage(selectedItem.Id, allPreviewIds, selectedItem.Category.SelectedCategory));
                
            });

            messaging.Subscribe<BookPreviewItemViewModel, BookPreviewItem>(this, BookPreviewItemViewModel.ShowOptionsMessage, async (sender, selectedItem) =>
            {
                
                string[] buttons = { "Katso", "Ei kiinnosta" };
                var selectedAction = await DisplayActionSheet("Toiminnot", null, null, buttons);
                
            });
            
        }
       

        private void BookRecommendationsScrolled(object sender, ScrolledEventArgs e)
        {            
            if (isScrollectToRightEnd((ScrollView)sender))
            {
                m_viewModel.Recommendation.loadNextBooks();
            }
        }

        private void BookMangasScrolled(object sender, ScrolledEventArgs e)
        {
            if (isScrollectToRightEnd((ScrollView)sender))
            {
                m_viewModel.Manga.loadNextBooks();
            }
        }


        private bool isScrollectToRightEnd(ScrollView sv)
        {
            return (sv.ScrollX >= (sv.ContentSize.Width - sv.Width));
        }

    }
    
}
