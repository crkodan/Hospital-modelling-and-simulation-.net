using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROJECTPSIM
{
    public partial class Form1 : Form
    {
        #region variabel
        Random r = new Random();
        int iterasi = 0;
        double clock = 0.0;
        double timeLastEvent = 0.0;

        List<Scheduler> listSchedule = new List<Scheduler>();

        double totalDelay = 0.0;
        double delay = 0.0;
        int numberDelay = 0;
        double areaUnderQT = 0.0;
        double areaUnderBT = 0.0;

        double avgWaitingQueue = 0.0;
        double avgNumQueue = 0.0;
        double serverUlti = 0.0;
        DataGridView[] dataGridViewLayanan = new DataGridView[4];
        #endregion

        #region A
        Scheduler arrivalA;
        Queue<double> antrianA = new Queue<double>();
        Scheduler layananA1;
        Scheduler layananA2;
        double pasien = 0.0;
        bool pelayanA1 = false;
        bool pelayanA2 = false;
        #endregion

        #region B
        Queue<double> antrianB = new Queue<double>();
        Scheduler layananB1;
        Scheduler layananB2;
        bool pelayanB1 = false;
        bool pelayanB2 = false;
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            arrivalA = new Scheduler("arrival", RvgCalc.exponen() + clock);
            layananA1 = new Scheduler("departA1", double.PositiveInfinity);
            layananA2 = new Scheduler("departA2", double.PositiveInfinity);
            layananB1 = new Scheduler("departB1", double.PositiveInfinity);
            layananB2 = new Scheduler("departB2", double.PositiveInfinity);

            listSchedule.Add(arrivalA);
            listSchedule.Add(layananA1);
            listSchedule.Add(layananA2);
            listSchedule.Add(layananB1);
            listSchedule.Add(layananB2);

            listBoxInfo.Items.Add("Inisialisasi sistem awal: ");
            listBoxInfo.Items.Add("Clock = " + clock.ToString());
            listBoxInfo.Items.Add("Isi list Event");
            for(int i = 0; i < listSchedule.Count; i++)
            {
                listBoxInfo.Items.Add(listSchedule[i].Objective + " = " + listSchedule[i].Clock);
            }
            listBoxInfo.Items.Add("Number delay: " + numberDelay);
            listBoxInfo.Items.Add("Total delay: " + totalDelay);
            listBoxInfo.Items.Add("Server A1 sibuk: " + pelayanA1.ToString());
            listBoxInfo.Items.Add("------------------------------");
            listBoxInfo.SelectedIndex = listBoxInfo.Items.Count - 1;
        }
        private void checkup()
        {
            listSchedule = listSchedule.OrderBy(i => i.Clock).ToList();
            Scheduler currentSchedule = new Scheduler(listSchedule[0]);
            timeLastEvent = clock;
            clock = currentSchedule.Clock;
            #region Arrive
            if(currentSchedule.Objective == "arrival")
            {
                listSchedule[0].Clock = RvgCalc.exponen() + clock;
                if(pelayanA1 == true && pelayanA2 == true)
                {
                    antrianA.Enqueue(currentSchedule.Clock);
                    if (antrianA.Count < 10)
                    {
                        MessageBox.Show("Masuk Antrian!");
                    }
                }
                else
                {
                    delay = 0.0;
                    numberDelay++;
                    if(pelayanA1 == false)
                    {
                        pelayanA1 = true;
                        pasien = currentSchedule.Clock; //buat nentuin jam masuk
                        layananA1.Clock = RvgCalc.dagum_a1(); //buat nentuin lama layanan
                    }
                    else if(pelayanA2 == false)
                    {
                        pelayanA2 = true; //jadiin sibuk
                        pasien = currentSchedule.Clock; //nentukan jam masuk
                        layananA2.Clock = RvgCalc.gamma_a2(); //nentukan lama layanan
                    }
                }
            }
            #endregion
            #region Depart
            else if (layananA1.Objective == "departA1")
            {
                if(antrianA.Count == 0)
                {
                    pelayanA1 = false;
                    layananA1.Clock = double.PositiveInfinity;
                    pulangTidak(pasien);
                    pasien = 0.0;
                }
                else
                {
                    areaUnderQT += (clock - timeLastEvent) * antrianA.Count;
                    pasien = antrianA.Dequeue();
                    delay = clock - pasien;
                    totalDelay += delay;
                    numberDelay++;
                    areaUnderBT += clock - timeLastEvent;
                    layananA1.Clock = RvgCalc.dagum_a1() + clock;
                    pulangTidak(pasien);
                }
            }
            else if (layananA2.Objective == "departA2")
            {
                if (antrianA.Count == 0)
                {
                    pelayanA2 = false;
                    layananA2.Clock = double.PositiveInfinity;
                    pulangTidak(pasien);
                    pasien = 0.0;
                }
                else
                {
                    areaUnderQT += (clock - timeLastEvent) * antrianA.Count;
                    pasien = antrianA.Dequeue();
                    delay = clock - pasien;
                    totalDelay += delay;
                    numberDelay++;
                    areaUnderBT += clock - timeLastEvent;
                    layananA2.Clock = RvgCalc.dagum_a1() + clock;
                    pulangTidak(pasien);
                }
            }
            #endregion
        }
        private void pulangTidak(double Psn)
        {
            Scheduler currentSchedule = new Scheduler(listSchedule[0]);
            double prob = r.NextDouble();
            if (prob <= 0.6)
            {

            }
            else if (prob >= 0.6)
            {
                #region masukB
                if (pelayanB1==true && pelayanB2==true)
                {
                    antrianB.Enqueue(layananB2.Clock);
                    if (antrianB.Count < 10)
                    {
                        MessageBox.Show("Masuk ANTREAN!");
                    }
                }
                else
                { 
                    if (pelayanB1 == false)
                    {
                        pelayanB1 = true;
                        pasien = currentSchedule.Clock;
                        layananB1.Clock = RvgCalc.rayleigh_b1();
                    }
                    else if(pelayanB2 == false)
                    {
                        pelayanB2 = true;
                        pasien = currentSchedule.Clock;
                        layananB2.Clock = RvgCalc.burr_b2();
                    }
                }

                #endregion
            }
        }

        private void tampilData(int i, double[] layanan)
        {
            #region datagrid
            dataGridViewLayanan[i].Rows.Clear();
            int counter = 0;
            for (int j = 0; j < layanan.Length; j++)
            {
                string temp = "nganggur";
                if (layanan[j] != 0)
                {
                    temp = layanan[j] + "";
                }
                dataGridViewLayanan[i].Rows.Add("Layanan " + j, temp);

                if (temp != "nganggur")
                {
                    dataGridViewLayanan[i].Rows[j].Cells[1].Style.BackColor = Color.Green;
                    counter++;
                }
            }

            if(counter >= layanan.Length)
            {
                dataGridViewLayanan[i].DefaultCellStyle.BackColor = Color.Red;
            }
            else
            {
                dataGridViewLayanan[i].DefaultCellStyle.BackColor = Color.White;
            }
            dataGridViewLayanan[i].CurrentCell.Selected = false;
            #endregion
        }
        private void show()
        {

            listSchedule = listSchedule.OrderBy(o => o.Clock).ToList();
            listBoxInfo.Items.Add("Iterasi ke-" + iterasi.ToString());
            listBoxInfo.Items.Add("Clock = " + clock.ToString());
            listBoxInfo.Items.Add("Pasien yang dilayani A1: " + pasien.ToString());
            listBoxInfo.Items.Add("Isi list Event");
            for (int i = 0; i < listSchedule.Count; i++)
            {
                if (listSchedule[i].Clock != 999.999)
                {
                    listBoxInfo.Items.Add(listSchedule[i].Objective + " = " + listSchedule[i].Clock);
                }
            }
            listBoxInfo.Items.Add("Server A1 sibuk: " + pelayanA1.ToString());
            listBoxInfo.Items.Add("------------------------------");
            listBoxInfo.SelectedIndex = listBoxInfo.Items.Count - 1;
        }
        private void buttonSimulasi_Click(object sender, EventArgs e)
        {
            while (clock < 10)
            {
                iterasi++;
                checkup();
                show();
            }
        }
    }
}
