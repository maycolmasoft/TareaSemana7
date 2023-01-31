using System;
using System.Collections.Generic;
using System.Text;

namespace Capremci.Modelos
{
    public class Creditos
    {
        public int id_creditos { get; set; }
        public string nombre_tipo_creditos { get; set; }
        public float monto_otorgado_creditos { get; set; }
        public int plazo_creditos { get; set; }
        public string nombre_estado_creditos { get; set; }
        public float saldo_actual_creditos { get; set; }

    }

    public class TablaAmortizacion
    {
        public int numero_pago_tabla_amortizacion { get; set; }
        public DateTime fecha_tabla_amortizacion { get; set; }
        public float capital_tabla_amortizacion { get; set; }

        public float interes_tabla_amortizacion { get; set; }
        public float mora_tabla_amortizacion { get; set; }
        public float seguros { get; set; }

        public float total_valor_tabla_amortizacion { get; set; }
        public float total_balance_tabla_amortizacion { get; set; }
        public float balance_tabla_amortizacion { get; set; }
       

    }


    public class PagosTablaAmortizacion
    {
        public int id_transacciones { get; set; }
        public DateTime fecha_transacciones { get; set; }
        public float valor_transacciones { get; set; }

        public string observacion_transacciones { get; set; }

        public int id_creditos { get; set; }
       
      

    }



    public class PagosDetalleTablaAmortizacion
    {
        public string tipo { get; set; }

        public float valor { get; set; }

    }


}
