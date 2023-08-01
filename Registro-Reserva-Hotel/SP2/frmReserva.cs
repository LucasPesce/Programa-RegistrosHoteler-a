using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SP2
{
    public partial class frmReserva : Form
    {
        //VARIABLES
        private const float TIPO_A = 20;        //Importe por la Cabaña tipo A
        public const float TIPO_B = 34;         //Importe por la Cabaña tipo B
        const float COCINA = 1f;                //Importe extra por Cocina
        const float HELADERA = 1.5f;            //Importe extra por Heladera
        const float TELEVISOR = 2f;             //Importe extra por TV
        const float POR_PERSONA = 1f;           //Importe extra por Persona

        public frmReserva()
        {
            InitializeComponent();
        }
        //EVENTO LOAD: Se inicia una unica vez al inicio del programa.
        //Inicializa las propiedades del formulario
        private void frmReserva_Load(object sender, EventArgs e)
        {
            comboTipo.Items.Clear();            // Limpia la lista desplegable
            comboTipo.Items.Add("Tipo A");      // Agregega a la lista el item "TIPO A"
            comboTipo.Items.Add("Tipo B");      // Agregega a la lista el item "TIPO B"
            comboTipo.SelectedIndex = 0;        // Establece como PREDETERMINADO el item "TIPO A"

            textDias.Text = "1";                // Establece como PREDETERMINADO el 1

            checkCocina.Checked = false;        // Establece como PREDETERMINADO el item DESMARCADO
            checkHeladera.Checked = false;      // Establece como PREDETERMINADO el item DESMARCADO
            checkTelevisión.Checked = false;    // Establece como PREDETERMINADO el item DESMARCADO

            radioEfectivo.Checked = true;       // Establece como PREDETERMINADO la opcion MARCADA

            comboTarjeta.Items.Clear();                     // Limpia la lista de Items
            comboTarjeta.Items.Add("Tarjeta Naranja");      // Agrega el elemento a la lista
            comboTarjeta.Items.Add("Tarjeta Visa");         // Agrega el elemento a la lista
            comboTarjeta.Items.Add("Tarjeta Mastercard");   // Agrega el elemento a la lista

            textNombre.Text = "";               // Limpia el campo de texto de Nombre
            textTelefono.Text = "";             // Limpia el campo de texto de Nombre

            buttonAceptar.Enabled = false;      // Deshabilita el boton aceptar
        }

        //EVENTP DE LA LISTA DE TIPO DE CABAÑA.
        //Activacion cuando se selecciona otro tipo de cabaña
        private void comboTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int PERSONAS = 0;      //Contador de CANTIDAD DE PERSONAS.Segun tipo de cabaña, se actualiza el contador
            
            comboPersonas.Items.Clear();    // Limpia el campo de Personas

            if(comboTipo.SelectedIndex == 0)    // Si se escoje TIPO A: Cabañas de hasta 4 personas
            {
                for(PERSONAS = 1; PERSONAS <= 4; PERSONAS++) // Se ejecuta un bucle de 4 repeticiones, para agregar las opciones
                {
                    comboPersonas.Items.Add(PERSONAS);       // Por cada ejecucion se agrega un numero del 1-4.
                }
            }
            else                               // Si no, se escogio TIPO B: Cabañas de hasta 8 personas
            {
                for (PERSONAS = 1; PERSONAS <= 8; PERSONAS++) // Se ejecuta un bucle de 8 repeticiones, para agregar las opciones
                {
                    comboPersonas.Items.Add(PERSONAS);       // Por cada ejecucion se agrega un numero del 1-8.
                }
            }
            comboPersonas.SelectedIndex = 0;    // Establece como PREDETERMINADO el primer item (1 Persona)
        }

        //EVENTO DE LA SECCION DIAS.
        private void textDias_TextChanged(object sender, EventArgs e)
        {
            //Condiciones para HABILITAR EL BOTON ACEPTAR. (Debe estar en las 3 secciones.)
            if(textDias.Text != "" && textNombre.Text != "" && textTelefono.Text != "")
            {
                buttonAceptar.Enabled = true;
            }
            else
            {
                buttonAceptar.Enabled = false;
            }
        }

        //EVENTO DE LA OPCION EFECTIVO.
        private void radioEfectivo_CheckedChanged(object sender, EventArgs e)
        {
            comboTarjeta.Enabled = false;       // Cuando se selecciona EFECTIVO. Se deshabilita las opciones de Tarjetas.
            comboTarjeta.SelectedIndex = -1;    // Permite ocultar las opciones.
        }

        //EVENTO DE LA OPCION TARJETA
        private void radioTarjeta_CheckedChanged(object sender, EventArgs e)
        {
            comboTarjeta.Enabled = true;        // Cuando se selecciona Tarjeta. Se habilita las opciones de Tarjetas.
            comboTarjeta.SelectedIndex = 0;     // Establece como PREDETERMINADO la primera opcion (Tarjeta Naranja)
        }

        //EVENTO DE LA SECCION NOMBRE.
        private void textNombre_TextChanged(object sender, EventArgs e)
        {
            //Condiciones para HABILITAR EL BOTON ACEPTAR
            if (textDias.Text != "" && textNombre.Text != "" && textTelefono.Text != "")
            {
                buttonAceptar.Enabled = true;
            }
            else
            {
                buttonAceptar.Enabled = false;
            }
        }

        //EVENTO DE LA SECCION TELEFONO.
        private void textTelefono_TextChanged(object sender, EventArgs e)
        {
            //Condiciones para HABILITAR EL BOTON ACEPTAR
            if (textDias.Text != "" && textNombre.Text != "" && textTelefono.Text != "")
            {
                buttonAceptar.Enabled = true;
            }
            else
            {
                buttonAceptar.Enabled = false;
            }
        }
        // EVENTO DEL BOTON ACEPTAR
        private void buttonAceptar_Click(object sender, EventArgs e)
        {   
            // Variables.
            float precioBase;
            float opcionales;
            float recargo;
            int dias;
            float total;

            //DIAS
            dias = int.Parse(textDias.Text);    // Obtiene lo ingresado en Días y lo convierte en ENTERO

            if(dias < 1 || dias > 8)            // Si se selecciona por fuera de nuestras restricciones
            {
                MessageBox.Show("Error. Seleccione una cantidad de dias entre el 1 al 8");
            }
            else         // Asignacion de las variables a las opciones escogidas 
            {
                //TIPO DE CABAÑA
                if(comboTipo.SelectedIndex == 0)    //Variable segun el TIPO DE CABAÑA 
                {
                    precioBase = TIPO_A;
                }
                else
                {
                    precioBase = TIPO_B;
                }

                // CANTIDAD DE PERSONAS
                precioBase += (POR_PERSONA * int.Parse(comboPersonas.Text)); //Se suma el precio base a la cantidad de PERSONAS

                //OPCIONALES
                opcionales = 0; // Si no se escoge ninguna opcion, no se suma ningun valor
                if(checkCocina.Checked == true) // Si se agrego la Cocina, se le suma el valor de esta
                {
                    opcionales += COCINA;
                }
                if (checkHeladera.Checked == true) // Si se agrego la Heladera, se le suma el valor de esta
                {
                    opcionales += HELADERA;
                }
                if (checkTelevisión.Checked == true) // Si se agrego la TV, se le suma el valor de esta
                {
                    opcionales += TELEVISOR;
                }

                // SUMATORIA DEL TOTAL
                total = (precioBase + opcionales) * dias;   // Se calcula la cantidad por el total de dias

                //METODO DE PAGO. SUMAR RECARGO
                if(radioTarjeta.Checked == true)            // Si se selecciono tarjeta
                {
                    if(comboTarjeta.SelectedIndex == 0)     // Si se escoge Naranja
                    {
                        recargo = total * 0.1f;             // Se recarga el 10% al total
                    }
                    else                                    // Para las otras dos tarjetas
                    {
                        recargo = total * 0.2f;             // Se recarga el 20%
                    }

                    total += recargo;
                }

                // MUESTRA EL RESULTADO FINAL
                MessageBox.Show("Total = US$ " + total.ToString(), "Importe de la reserva",
                    MessageBoxButtons.OK);

                // INICIALIZA LOS CONTROLES DE INTERFAZ PARA VOLVER A LOS VALORES INICIALES
                comboTipo.SelectedIndex = 0;
                comboPersonas.SelectedIndex = 0;
                textDias.Text = "";

                checkCocina.Checked = false;
                checkHeladera.Checked = false;
                checkTelevisión.Checked = false;

                radioEfectivo.Enabled = true;

                textNombre.Text = "";
                textTelefono.Text = "";
            }
        }
    }
}
