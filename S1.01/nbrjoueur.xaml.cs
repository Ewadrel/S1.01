﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace S1._01
{
    /// <summary>
    /// Logique d'interaction pour nbrjoueur.xaml
    /// </summary>
    public partial class Nbrjoueur : Window
    {
        public Nbrjoueur()
        {
            InitializeComponent();
        }

        private void _1joueur_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).nombreJoueur = 1;
            this.DialogResult = true;
        }

        private void _2joueurs_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).nombreJoueur = 2;
            this.DialogResult = false;
        }
    }
}
