using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Threading;

namespace Nechita_Andrei_Lab2
{
    class DoghnutMachine
    {
        private DoghnutType mFlavor;

        public DoghnutType Flavor
        {
            get
            {
                return mFlavor;
            }
            set
            {
                mFlavor = value;
            }
        }
        private System.Collections.ArrayList mDoghnuts = new System.Collections.ArrayList();
        public Doghnut this[int Index]
        {
            get
            {
                return (Doghnut)mDoghnuts[Index];
            }
            set
            {
                mDoghnuts[Index] = value;
            }
        }

        public delegate void DoghnutCompleteDelegate();
        public event DoghnutCompleteDelegate DoghnutComplete;

        DispatcherTimer doghnutTimer;

        private void InitializaComponent()
        {
            this.doghnutTimer = new DispatcherTimer();
            this.doghnutTimer.Tick += new System.EventHandler(this.doghnutTimer_Tick);
        }

        public DoghnutMachine()
        {
            InitializaComponent();
        }

        private void doghnutTimer_Tick(object sender, EventArgs e)
        {
            Doghnut aDoghnut = new Doghnut(this.Flavor);
            mDoghnuts.Add(aDoghnut);
            DoghnutComplete();
        }

        public bool Enabled
        {
            set
            {
                doghnutTimer.IsEnabled = value;
            }
        }
        public int Interval
        {
            set
            {
                doghnutTimer.Interval = new TimeSpan(0,0,value);
            }
        }

        public void MakeDoghnuts(DoghnutType dFlavor)
        {
            Flavor = dFlavor;
            switch (Flavor)
            {
                case DoghnutType.Glazed: Interval = 3; break;
                case DoghnutType.Sugar: Interval = 2; break;
                case DoghnutType.Lemon: Interval = 5; break;
                case DoghnutType.Chocolate: Interval = 7; break;
                case DoghnutType.Vanilla: Interval = 4; break;
            }
        }
    }
    public enum DoghnutType
    {
        Glazed,
        Sugar,
        Lemon,
        Chocolate,
        Vanilla
    }
    class Doghnut
    {
        private DoghnutType mFlavor;
        public DoghnutType Flavor{
            get{
                return mFlavor;
            }
            set
            {
                mFlavor = value;
            }
        }

        private float mPrice = .50F;
        public float Price
        {
            get
            {
                return mPrice;
            }
            set
            {
                mPrice = value;
            }
        }

        private readonly DateTime mTimeOfCreation;
        public DateTime TimeOfCreation
        {
            get
            {
                return mTimeOfCreation;
            }
        }

        public Doghnut(DoghnutType aFlavor)
        {
            mTimeOfCreation = DateTime.Now;
            mFlavor = aFlavor;
        }
    }
}
