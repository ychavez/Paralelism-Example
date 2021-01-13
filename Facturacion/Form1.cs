using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Facturacion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private List<int> GetFacturas(int limite)
        {
            Random gen = new Random();
            var result = new List<int>();

            for (int i = 0; i < limite; i++)
            {
                result.Add(gen.Next());
            }
            return result;

        }
        /// <summary>
        /// Simulacion del servicio
        /// </summary>
        /// <param name="factura"></param>
        /// <returns></returns>
        private async Task<(int, bool)> Facturar(int factura)
        {
            Random gen = new Random();
            var boolResul = gen.Next(100) <= 70;
            await Task.Delay(1000); // simular proceso del servicio
            return (factura, boolResul);
        }




        private async void button1_Click(object sender, EventArgs e)
        {
            var watch = Stopwatch.StartNew();
            var facturas = GetFacturas(100000);

            List<Task<(int, bool)>> resultado = new List<Task<(int, bool)>>();

            foreach (var factura in facturas)
            {
                var facturaResult = Facturar(factura);
                resultado.Add(facturaResult);
            }
            var result = await Task.WhenAll<(int, bool)>(resultado);
            watch.Stop();
            MessageBox.Show($"Transcurrieron {watch.ElapsedMilliseconds} milisegundos");
        }
    }
}
