﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using ExpertsBlog.Entities;
using ExpertsBlog.Mobile.Pages;
using Xamarin.Forms;
using System.Net.Http;
using System;
using Newtonsoft.Json;
using System.Diagnostics;
using ExpertsBlog.Mobile.Services;

namespace ExpertsBlog.Mobile.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IExpertsBlogApiService apiService = new ExpertsBlogApiService();

        private ObservableCollection<BlogPost> blogPosts = new ObservableCollection<BlogPost>();
        public ObservableCollection<BlogPost> BlogPosts
        {
            get => blogPosts;
            set => SetProperty(ref blogPosts, value);
        }

        
        public MainViewModel()
        {
            GetData();
           
        }

        private async void GetData()
        {
            BlogPosts = new ObservableCollection<BlogPost>(await apiService.GetBlogPosts());

        }
        //private async void GetData()
        //{
        //    using (HttpClient httpClient = new HttpClient()
        //    {
        //        BaseAddress = new Uri("https://expertsblogapi.azurewebsites.net/")
        //    })
        //    {
        //        var json = await httpClient.GetStringAsync("BlogPosts");
        //        Debug.WriteLine("***********************************" + json);
        //        var x = JsonConvert.DeserializeObject<IEnumerable<BlogPost>>(json);
        //        foreach (var blogPost in x)
        //        {
        //            BlogPosts.Add(blogPost);
        //        }
        //    }
        //}


        //public event PropertyChangedEventHandler PropertyChanged;
        //private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //protected void SetProperty<T>(ref T storage, T value, /*Action afteraction = null,*/ [CallerMemberName] string propertyName = null)
        //{
        //    if (EqualityComparer<T>.Default.Equals(storage, value))
        //    {
        //        return;
        //    }
        //    storage = value;
        //    OnPropertyChanged(propertyName);
        //    //afteraction?.Invoke();
        //    //return true;
        //}

        public ICommand DetailsCommand => new Command<BlogPost>(async bp =>
       {
           await Shell.Current.GoToAsync($"{nameof(DetailsPage)}?{nameof(DetailsViewModel.Id)}={bp.Id}");
       });
    }
}
