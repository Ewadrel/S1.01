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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace S1._01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int compteur=0;
        private int tourDuJoueur = 1;
        Point position = new Point(0, 0);
        private readonly double[] COORDONNEX = {10, 134, 258, 382, 506, 630, 754, 878, 1002};
        private readonly double[] COORDONNEY = {3, 124, 245, 366, 487, 608, 729, 850 };
        private int[,] grille = new int[9,9];
        private bool testeligne=false;
        private bool testecolonne = false;
        private bool testedigonale = false;


        //private string CHOIXJETON = ChoixDuJeton(1);
        private ImageBrush jeton1 = new ImageBrush();
        private ImageBrush jeton2 = new ImageBrush();
        private ImageBrush fond = new ImageBrush();
        private ImageBrush fm = new ImageBrush();
        private bool TourJoueur1 = true, joueur;
        private int VALBONUS = 3;
        private string A, B, C;
        
        //variable pour le tour du joueur
        public MainWindow()
        {
            InitializeComponent();
            
            Jouer jouer = new Jouer();
            bool resultatjouer=(bool)jouer.ShowDialog();
            if (resultatjouer == true)
            {
                Nbrjoueur nbrjoueur1 = new Nbrjoueur();
                joueur = (bool)nbrjoueur1.ShowDialog();
                if (joueur == true || joueur == false)
                {
                    Window1 window1 = new Window1();
                    bool couleur = (bool)window1.ShowDialog();
                }
            }
                     
            
            fond.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/puissance4x9x8.png"));
            jeton1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/violet.png"));
            jeton2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/vert.png"));
            fm.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/fm.jpg"));
            main.Background = fm;

            plateau.Fill = fond;
            Canvas.SetZIndex(plateau, 1);

            
        }
        
        
        public int[] PointBonus( int[,] tab)
        {
            int[] bonus = { 0, 0 };
            for (int i=0; i < tab.GetLength(0)-2; i++)
            {
                for (int j = 1; j < tab.GetLength(1)-1; j++)
                { 
                    if (tab[i,j]==1 && tab[i+1,j-1]==1 && tab[i + 1, j + 1]==1 && tab[i + 2, j ] == 1)
                    {
                        bonus[0] += VALBONUS;
                    }
                    if (tab[i, j] == 2 && tab[i + 1, j - 1] == 2 && tab[i + 1, j + 1] == 2 && tab[i + 2, j] == 2)
                    {
                        bonus[1] += VALBONUS;
                    }
                }
            }
            
            return bonus;
        }
        
        public bool Colonne(int[,] tab, int[]point)
        {
            int compte = 0;
            for (int i = 0; i < tab.GetLength(0) - 5; i++)
            {
                if(tab[i , point[1]]!=0)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (tab[i + j, point[1]] == tab[i, point[1]])
                        {
                            compte++;
                        }
                        if (compte == 6)
                        {
                            return true;
                        }
                    }
                  
                }
                compte = 0;
            }
            return false;  
        
        }
        public bool Diag(int[,] tab, int[] point)
        {
            int compte = 0;
            if (point[0] < 6 || point[1]>tab.GetLength(1)-5)
            {
                return false;
            }
            
            while (point[0]>0|| point[1]<0)
            {
                int i = 0;
                if (tab[point[0] - i, point[1] - i] == tab[point[0], point[1]])
                {
                    compte++;
                }
                if (compte == 6)
                {
                    return true;
                }
            }
            return false;

        }

        public bool LIGNE(int[,] tab, int[]point)
        {

            int indice = point[0] + 1;
            int compte = 0;
            for (int i = 0; i < tab.GetLength(1) - 5; i++)
            {
                if (tab[indice, i ] != 0)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (tab[indice, i + j ] == tab[indice, i])
                        {
                            compte++;
                        }
                        if (compte == 5)
                        {
                            return true;
                        }
                    }

                }
                compte = 0;
            }
            return false;



        }
        
        public int colonneoccupe(int[,] tab,int indicej)
        {
            int indice = 0;
            for (int i = 0; i < tab.GetLength(0); i++)
            {
                if (tab[i, indicej] == 0)
                {
                    indice += 1;
                }

            }
            if (indice==0)
            {
                indice += 1;
            }
            indice -= 1;
            return indice;

        }


        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            // trouver l'indice des colonnes de grille 
            position = e.GetPosition(plateau);
            double x = position.X;
            int indice = 0;
            for (int i = 0; i < COORDONNEX.Length; i++)
            {
                if ( COORDONNEX[i]  < x)
                {
                    indice = i;
                    
                }

            }
            //dernière ligne du tableau occupé
            if (grille[1,indice]!=0)
            { 
                MessageBox.Show("coup impossible"); 
            }
            else
            {
               //création du jeton
                Rectangle jeton = new Rectangle();
                jeton.Width = 110;
                jeton.Height = 110;
                if (compteur % 2 == 0)
                {
                    jeton.Fill = jeton1;
                    tourDuJoueur = 1;
                }
                else
                {
                    jeton.Fill = jeton2;
                    tourDuJoueur = 2;
                }
                //jeton derriere la plateau
                Canvas.SetZIndex(jeton, 0);
                //ajoue du pion dans le tableau
                grille[colonneoccupe(grille, indice), indice] = tourDuJoueur;
                //ajoue du pion dans le canvas
                Canvas.SetTop(jeton, COORDONNEY[colonneoccupe(grille, indice)]);
                Canvas.SetLeft(jeton, COORDONNEX[indice]);
                main.Children.Add(jeton);
                compteur += 1;
                //detection coup gagnant
                int[]point = new int[] { colonneoccupe(grille, indice), indice};
                
                if (LIGNE(grille,point) ==true|| Colonne(grille, point) == true)
                {
                    Victoire victoire = new Victoire();
                    bool victory = (bool)victoire.ShowDialog();

                    if (victory == true)
                    {
                        Nbrjoueur nbrjoueur1 = new Nbrjoueur();
                        joueur = (bool)nbrjoueur1.ShowDialog();
                        if (joueur == true || joueur == false)
                        {
                            Window1 window1 = new Window1();
                            bool couleur = (bool)window1.ShowDialog();
                        }
                    }
                    this.Close();
                    if (tourDuJoueur % 2 == 0)
                    {
                        victoire.gagne.Text = "Joueur 2";
                    }
                    else if (tourDuJoueur % 2 == 1)
                    {
                        victoire.gagne.Text = "Joueur 1";
                    }
                    else { victoire.gagne.Text = "IA"; }
                    victoire.ShowDialog();
                }
            }
            

            //affiche la grille
            for (int i=0; i < grille.GetLength(0); i++) 
            {
                for( int j=0; j < grille.GetLength(1); j++)
                {
                    Console.Write(grille[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine(colonneoccupe(grille, indice)) ;
            Console.WriteLine(indice);




            Console.WriteLine(x) ;
            Console.WriteLine(COORDONNEY[colonneoccupe(grille,indice)]) ;




        }
    }   
}

