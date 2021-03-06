﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfNetCore.Views
{
    /// <summary>
    /// Interaction logic for Inicio.xaml
    /// </summary>
    public partial class Inicio : Window
    {
        public Inicio()
        {
            InitializeComponent();
        }

        private void Video_MediaEnded(object sender, RoutedEventArgs e)
        {
            Video.Position = new TimeSpan(0);
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Border)sender).Background = (Brush)new BrushConverter().ConvertFrom("#FFF774F7");
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Border)sender).Background = (Brush)new BrushConverter().ConvertFrom("#99F774F7");
        }

        private void ButtonTreinar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Treinar treinar = new Treinar();
            Hide();
            treinar.ShowDialog();
            Show();
        }

        private void ButtonCantar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Cantar cantar = new Cantar();
            Hide();
            cantar.ShowDialog();
            Show();
        }
    }
}
