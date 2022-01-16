using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PI_Project
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

    }

    public class Guest 
    {
        public static void Booking ()
        {
            string name = "";
            string info = "";

            Console.WriteLine("Get you numb");
            int numb = Convert.ToInt32(Console.ReadLine());
        }
    }

    public class Admin : Guest
    {
        public static void Recruiting()
        {

        }
        public static void Shelving()
        {

        }
        public static void MakingSchedule()
        {

        }
        public static void AcceptingPayment() //Можно платить наличными и картой, также может предоставлять скидку в праздничные дни. в соответствии с оплатой он заселяет
        {

        }
        public static void Reporting()
        {

        }
        public static void Evicting()
        {

        }
    }

    public class Clerk
    {
        public static void Reporting()
        {

        }
    }

}
