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

        KeyValuePair<DoghnutType, double>[] PriceList =
        {
            new KeyValuePair<DoghnutType, double>(DoghnutType.Sugar, 2.5),
            new KeyValuePair<DoghnutType, double>(DoghnutType.Glazed, 3),
            new KeyValuePair<DoghnutType, double>(DoghnutType.Chocolate, 4.5),
            new KeyValuePair<DoghnutType, double>(DoghnutType.Vanilla, 4),
            new KeyValuePair<DoghnutType, double>(DoghnutType.Lemon, 3.5),

        };
        DoghnutType selectedDoghnut;
        public MainWindow()
        {
            InitializeComponent();
            CommandBinding cmd1 = new CommandBinding();
            cmd1.Command = ApplicationCommands.Print;
            ApplicationCommands.Print.InputGestures.Add(new KeyGesture(Key.I, ModifierKeys.Alt));
            cmd1.Executed += new ExecutedRoutedEventHandler(CtrlP_CommandHandler);
            this.CommandBindings.Add(cmd1);

            CommandBinding cmd2 = new CommandBinding();
            cmd2.Command = CustomCommands.StopCommand.Launch;
            cmd2.Executed += new ExecutedRoutedEventHandler(CtrlS_CommandHandler);
            this.CommandBindings.Add(cmd2);
        }
        private void CtrlP_CommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("You have in stock:"+mRaisedGlazed+" Glazed," +mRaisedSugar+
            "Sugar," +mFilledLemon+ "Lemon," +mFilledChocolate+ "Chocolate," +mFilledVanilla+ "Vanilla");
        }
        private void CtrlS_CommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Ctrl+S was pressed! the doghnut machine will stop!");
            this.stopToolStripMenuItem_Click(sender, e);
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

            cmbType.ItemsSource = PriceList;
            cmbType.DisplayMemberPath = "Key";
            cmbType.SelectedValuePath = "Value";
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
        /*
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
        */
        private void exitToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void stopToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            myDoghnutMachine.Enabled = false;
            this.Title = " Virtual Doghnuts Factory";
            e.Handled = true;
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

        private void FilledItems_Click(object sender, RoutedEventArgs e)
        {
            String DoghnutFlavour;
            MenuItem SelectedItem = (MenuItem)e.OriginalSource;
            DoghnutFlavour = SelectedItem.Header.ToString();

            Enum.TryParse(DoghnutFlavour, out DoghnutType myFlavour);
            myDoghnutMachine.MakeDoghnuts(myFlavour);
        }
        private void FilledItemsShow_Click(object sender, RoutedEventArgs e)
        {
            string mesaj;

            MenuItem SelectedItem = (MenuItem)e.OriginalSource;
            mesaj = SelectedItem.Header.ToString() + " doghnuts are being cooked";
            this.Title = mesaj;
        }

        private void cmbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtPrice.Text = cmbType.SelectedValue.ToString();
            KeyValuePair<DoghnutType, double> selectedEntry = (KeyValuePair<DoghnutType, double>)cmbType.SelectedItem;
            selectedDoghnut = selectedEntry.Key;
        }

        private int ValidateQuantity(DoghnutType selectedDoghnut)
        {
            int q = int.Parse(txtQuantity.Text);
            int r = 1;

            switch (selectedDoghnut)
            {
                case DoghnutType.Glazed:
                    if (q > mRaisedGlazed)
                    {
                        r = 0;
                    }
                    break;
                case DoghnutType.Sugar:
                    if (q > mRaisedGlazed)
                    {
                        r = 0;
                    }
                    break;
                case DoghnutType.Chocolate:
                    if (q > mFilledChocolate)
                    {
                        r = 0;
                    }
                    break;
                case DoghnutType.Lemon:
                    if (q > mFilledLemon)
                    {
                        r = 0;
                    }
                    break;
                case DoghnutType.Vanilla:
                    if (q > mFilledVanilla)
                    {
                        r = 0;
                    }
                    break;
            }

            return r;
        }

        private void btnAddToSale_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateQuantity(selectedDoghnut) > 0)
            {
                lstSale.Items.Add(txtQuantity.Text + " " + selectedDoghnut.ToString() + ":" + txtPrice.Text + " " + double.Parse(txtQuantity.Text) * double.Parse(txtPrice.Text));
            }
            else
            {
                MessageBox.Show("Cantitatea introdusa nu este disponibila in stoc!");
            }
        }

        private void btnRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            lstSale.Items.Remove(lstSale.SelectedItem);
        }

        private void btnCheckOut_Click(object sender, RoutedEventArgs e)
        {
            txtTotal.Text = (double.Parse(txtTotal.Text) + double.Parse(txtQuantity.Text) * double.Parse(txtPrice.Text)).ToString();

            foreach (string s in lstSale.Items)
            {
                switch (s.Substring(s.IndexOf(" ") + 1, s.IndexOf(":") - s.IndexOf(" ") - 1))
                {
                    case "Glazed":
                        mRaisedGlazed = mRaisedGlazed - Int32.Parse(s.Substring(0, s.IndexOf(" ")));
                        txtGlazedRaised.Text = mRaisedGlazed.ToString();
                        break;
                    case "Sugar":
                        mRaisedSugar = mRaisedSugar - Int32.Parse(s.Substring(0, s.IndexOf(" ")));
                        txtSugarRaised.Text = mRaisedSugar.ToString();
                        break;
                    case "Chocolate":
                        mFilledChocolate = mFilledChocolate - Int32.Parse(s.Substring(0, s.IndexOf(" ")));
                        txtChocolateFilled.Text = mFilledChocolate.ToString();
                        break;
                    case "Vanilla":
                        mFilledVanilla = mFilledVanilla - Int32.Parse(s.Substring(0, s.IndexOf(" ")));
                        txtVanillaFilled.Text = mFilledVanilla.ToString();
                        break;
                    case "Lemon":
                        mFilledLemon = mFilledLemon - Int32.Parse(s.Substring(0, s.IndexOf(" ")));
                        txtLemonFilled.Text = mFilledLemon.ToString();
                        break;
                }

            }
        }
        //!!!!!!!!!!!!!-----------NEED TO ADD THE REST SUCH AS VANILLA CHOCOLATE ETC BUTTONS, VALUES AND FUNCTIONALITIES
        //private void frmMain_Loaded(object sender, RoutedEventArgs e)
        //{
        //  myDoghnutMachine = new DoghnutMachine();
        // }
    }
}
