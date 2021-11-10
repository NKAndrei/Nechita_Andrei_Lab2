using System;
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

namespace Nechita_Andrei_Lab2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DoghnutMachine myDoghnutMachine;

        private int mRaisedGlazed;
        private int mRaisedSugar;
        private int mFilledLemon;
        private int mFilledChocolate;
        private int mFilledVanilla;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnButon1_Click(object sender, RoutedEventArgs e)
        {
            //RoutedEventArgs base class that handles all events, all other event handling classes are child classes of this one
            // sender is the button that actually calls this method
        }

        private void frmMain_Loaded(object sender, RoutedEventArgs e)
        {
            myDoghnutMachine = new DoghnutMachine();
            myDoghnutMachine.DoghnutComplete += new DoghnutMachine.DoghnutCompleteDelegate(DoghnutCompleteHandler);
        }

        private void glazedToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            glazedToolStripMenuItem.IsChecked = true;
            sugarToolStripMenuItem.IsChecked = false;
            myDoghnutMachine.MakeDoghnuts(DoghnutType.Glazed);
        }

        private void sugarToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            glazedToolStripMenuItem.IsChecked = false;
            sugarToolStripMenuItem.IsChecked = true;
            myDoghnutMachine.MakeDoghnuts(DoghnutType.Sugar);
        }
        private void lemonFilledMenuItem_Click(object sender, RoutedEventArgs e)
        {
            lemonFilledMenuItem.IsChecked = true;
            chocolateFilledMenuItem.IsChecked = false;
            vanillaFilledMenuItem.IsChecked = false;
            myDoghnutMachine.MakeDoghnuts(DoghnutType.Lemon);
        }

        private void chocolateFilledMenuItem_Click(object sender, RoutedEventArgs e)
        {
            lemonFilledMenuItem.IsChecked = true;
            chocolateFilledMenuItem.IsChecked = false;
            vanillaFilledMenuItem.IsChecked = false;
            myDoghnutMachine.MakeDoghnuts(DoghnutType.Chocolate);
        }

        private void vanillaFilledMenuItem_Click(object sender, RoutedEventArgs e)
        {
            lemonFilledMenuItem.IsChecked = true;
            chocolateFilledMenuItem.IsChecked = false;
            vanillaFilledMenuItem.IsChecked = false;
            myDoghnutMachine.MakeDoghnuts(DoghnutType.Vanilla);
        }
        private void exitToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void stopToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            myDoghnutMachine.Enabled = false;
        }

        private void DoghnutCompleteHandler()
        {
            switch (myDoghnutMachine.Flavor)
            {
                case DoghnutType.Glazed:
                    mRaisedGlazed++;
                    txtGlazedRaised.Text = mRaisedGlazed.ToString();
                    break;

                case DoghnutType.Sugar:
                    mRaisedSugar++;
                    txtSugarRaised.Text = mRaisedSugar.ToString();
                    break;
                case DoghnutType.Lemon:
                    mFilledLemon++;
                    txtLemonFilled.Text = mFilledLemon.ToString();
                    break;
                case DoghnutType.Chocolate:
                    mFilledChocolate++;
                    txtChocolateFilled.Text = mFilledChocolate.ToString();
                    break;
                case DoghnutType.Vanilla:
                    mFilledVanilla++;
                    txtVanillaFilled.Text = mFilledVanilla.ToString();
                    break;
            }
        }

        private void txtQuantity_KeyPress(object sender, KeyEventArgs e)
        {
            if (!(e.Key>=Key.D0 && e.Key<=Key.D9))
            {
                MessageBox.Show("Numaicifre se pot introduce!", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        
        //!!!!!!!!!!!!!-----------NEED TO ADD THE REST SUCH AS VANILLA CHOCOLATE ETC BUTTONS, VALUES AND FUNCTIONALITIES
        //private void frmMain_Loaded(object sender, RoutedEventArgs e)
        //{
        //  myDoghnutMachine = new DoghnutMachine();
        // }
    }
}
