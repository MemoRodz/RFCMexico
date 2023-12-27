using System;
using System.Collections;
using System.Text;

namespace ObtenRFC
{
    public class obtieneRFC
    {
        static public string CalcularRFC(string nombre, string apellidoPaterno, string apellidoMaterno, string fecha)
        {
            //Cambiamos todo a mayúsculas
            nombre = nombre.ToUpper();
            apellidoPaterno = apellidoPaterno.ToUpper();
            apellidoMaterno = apellidoMaterno.ToUpper();

            //RFC que se devolverá
            string rfc = String.Empty;

            //Quitamos los espacios al principio y final del nombre y apellidos
            nombre.Trim();
            apellidoPaterno = apellidoPaterno.Trim();
            apellidoMaterno = apellidoMaterno.Trim();

            //Agregamos el primer caracter del apellido paterno
            rfc = apellidoPaterno.Substring(0, 1);

            //Buscamos y agregamos al rfc la primera vocal del primer apellido
            foreach (char c in apellidoPaterno)
            {
                if (EsVocal(c))
                {
                    rfc += c;
                    break;
                }
            }

            //Agregamos el primer caracter del apellido materno
            rfc += apellidoMaterno.Substring(0, 1);

            //Agregamos el primer caracter del primer nombre
            rfc += nombre.Substring(0, 1);

            //Eliminamos palabras prohibidas
            //string strAceptable = QuitaProhibidas(rfc);
            rfc = QuitaProhibidas(rfc); //strAceptable;

            //Agregamos la fecha yymmdd (por ejemplo: 520707, 07 de julio de 1952 )
            rfc += ObtieneFecha(fecha);

            //Le agregamos la homoclave al rfc 
            CalcularHomoclave(apellidoPaterno + " " + apellidoMaterno + " " + nombre, fecha, ref rfc);

            return rfc;
        }

        /// <summary>
        /// Calcula la homoclave
        /// </summary>
        /// <param name="nombreCompleto">El nombre completo de la persona en el formato "ApellidoPaterno ApellidoMaterno Nombre(s)"</param>
        /// <param name="fecha">fecha en el formato "dd/MM/yy"</param>
        /// <param name="rfc">rfc sin homoclave, esta se pasa con ref y después de la función tendrá la homoclave</param>
        static private void CalcularHomoclave(string nombreCompleto, string fecha, ref string rfc)
        {
            //Guardara el nombre en su correspondiente numérico
            StringBuilder nombreEnNumero = new StringBuilder(); ;
            //La suma de la secuencia de números de nombreEnNumero
            long valorSuma = 0;

            #region Tablas para calcular la homoclave
            //Estas tablas realmente no se porque son como son
            //solo las copie de lo que encontré en internet

            #region TablaRFC 1
            Hashtable tablaRFC1 = new Hashtable();
            tablaRFC1.Add("&", 10);
            tablaRFC1.Add("Ñ", 10);
            tablaRFC1.Add("A", 11);
            tablaRFC1.Add("B", 12);
            tablaRFC1.Add("C", 13);
            tablaRFC1.Add("D", 14);
            tablaRFC1.Add("E", 15);
            tablaRFC1.Add("F", 16);
            tablaRFC1.Add("G", 17);
            tablaRFC1.Add("H", 18);
            tablaRFC1.Add("I", 19);
            tablaRFC1.Add("J", 21);
            tablaRFC1.Add("K", 22);
            tablaRFC1.Add("L", 23);
            tablaRFC1.Add("M", 24);
            tablaRFC1.Add("N", 25);
            tablaRFC1.Add("O", 26);
            tablaRFC1.Add("P", 27);
            tablaRFC1.Add("Q", 28);
            tablaRFC1.Add("R", 29);
            tablaRFC1.Add("S", 32);
            tablaRFC1.Add("T", 33);
            tablaRFC1.Add("U", 34);
            tablaRFC1.Add("V", 35);
            tablaRFC1.Add("W", 36);
            tablaRFC1.Add("X", 37);
            tablaRFC1.Add("Y", 38);
            tablaRFC1.Add("Z", 39);
            tablaRFC1.Add("0", 0);
            tablaRFC1.Add("1", 1);
            tablaRFC1.Add("2", 2);
            tablaRFC1.Add("3", 3);
            tablaRFC1.Add("4", 4);
            tablaRFC1.Add("5", 5);
            tablaRFC1.Add("6", 6);
            tablaRFC1.Add("7", 7);
            tablaRFC1.Add("8", 8);
            tablaRFC1.Add("9", 9);
            #endregion

            #region TablaRFC 2
            Hashtable tablaRFC2 = new Hashtable();
            tablaRFC2.Add(0, "1");
            tablaRFC2.Add(1, "2");
            tablaRFC2.Add(2, "3");
            tablaRFC2.Add(3, "4");
            tablaRFC2.Add(4, "5");
            tablaRFC2.Add(5, "6");
            tablaRFC2.Add(6, "7");
            tablaRFC2.Add(7, "8");
            tablaRFC2.Add(8, "9");
            tablaRFC2.Add(9, "A");
            tablaRFC2.Add(10, "B");
            tablaRFC2.Add(11, "C");
            tablaRFC2.Add(12, "D");
            tablaRFC2.Add(13, "E");
            tablaRFC2.Add(14, "F");
            tablaRFC2.Add(15, "G");
            tablaRFC2.Add(16, "H");
            tablaRFC2.Add(17, "I");
            tablaRFC2.Add(18, "J");
            tablaRFC2.Add(19, "K");
            tablaRFC2.Add(20, "L");
            tablaRFC2.Add(21, "M");
            tablaRFC2.Add(22, "N");
            tablaRFC2.Add(23, "P");
            tablaRFC2.Add(24, "Q");
            tablaRFC2.Add(25, "R");
            tablaRFC2.Add(26, "S");
            tablaRFC2.Add(27, "T");
            tablaRFC2.Add(28, "U");
            tablaRFC2.Add(29, "V");
            tablaRFC2.Add(30, "W");
            tablaRFC2.Add(31, "X");
            tablaRFC2.Add(32, "Y");
            #endregion

            #region TablaRFC 3
            Hashtable tablaRFC3 = new Hashtable();
            tablaRFC3.Add("A", 10);
            tablaRFC3.Add("B", 11);
            tablaRFC3.Add("C", 12);
            tablaRFC3.Add("D", 13);
            tablaRFC3.Add("E", 14);
            tablaRFC3.Add("F", 15);
            tablaRFC3.Add("G", 16);
            tablaRFC3.Add("H", 17);
            tablaRFC3.Add("I", 18);
            tablaRFC3.Add("J", 19);
            tablaRFC3.Add("K", 20);
            tablaRFC3.Add("L", 21);
            tablaRFC3.Add("M", 22);
            tablaRFC3.Add("N", 23);
            tablaRFC3.Add("O", 25);
            tablaRFC3.Add("P", 26);
            tablaRFC3.Add("Q", 27);
            tablaRFC3.Add("R", 28);
            tablaRFC3.Add("S", 29);
            tablaRFC3.Add("T", 30);
            tablaRFC3.Add("U", 31);
            tablaRFC3.Add("V", 32);
            tablaRFC3.Add("W", 33);
            tablaRFC3.Add("X", 34);
            tablaRFC3.Add("Y", 35);
            tablaRFC3.Add("Z", 36);
            tablaRFC3.Add("0", 0);
            tablaRFC3.Add("1", 1);
            tablaRFC3.Add("2", 2);
            tablaRFC3.Add("3", 3);
            tablaRFC3.Add("4", 4);
            tablaRFC3.Add("5", 5);
            tablaRFC3.Add("6", 6);
            tablaRFC3.Add("7", 7);
            tablaRFC3.Add("8", 8);
            tablaRFC3.Add("9", 9);
            tablaRFC3.Add("", 24);
            tablaRFC3.Add(" ", 37);
            #endregion

            #endregion

            //agregamos un cero al inicio de la representación númerica del nombre
            nombreEnNumero.Append("0");

            //Recorremos el nombre y vamos convirtiendo las letras en 
            //su valor numérico
            foreach (char c in nombreCompleto)
            {
                if (tablaRFC1.ContainsKey(c.ToString()))
                    nombreEnNumero.Append(tablaRFC1[c.ToString()].ToString());
                else
                    nombreEnNumero.Append("00");
            }

            //Calculamos la suma de la secuencia de números 
            //calculados anteriormente
            //la formula es:
            //( (el caracter actual multiplicado por diez)
            //mas el valor del caracter siguiente )
            //(y lo anterior multiplicado por el valor del caracter siguiente)
            for (int i = 0; i < nombreEnNumero.Length - 1; i++)
            {
                valorSuma += ((Convert.ToInt32(nombreEnNumero[i].ToString()) * 10) + Convert.ToInt32(nombreEnNumero[i + 1].ToString())) * Convert.ToInt32(nombreEnNumero[i + 1].ToString());
            }

            //Lo siguiente no se porque se calcula así, es parte del algoritmo.
            //Los magic numbers que aparecen por ahí deben tener algún origen matemático
            //relacionado con el algoritmo al igual que el proceso mismo de calcular el 
            //digito verificador.
            //Por esto no puedo añadir comentarios a lo que sigue, lo hice por acto de fe.

            int div = 0, mod = 0;
            div = Convert.ToInt32(valorSuma) % 1000;
            mod = div % 34;
            div = (div - mod) / 34;

            int indice = 0;
            string hc = String.Empty;  //los dos primeros caracteres de la homoclave
            while (indice <= 1)
            {
                if (tablaRFC2.ContainsKey((indice == 0) ? div : mod))
                    hc += tablaRFC2[(indice == 0) ? div : mod];
                else
                    hc += "Z";
                indice++;
            }

            //Agregamos al RFC los dos primeros caracteres de la homoclave
            rfc += hc;

            //Aqui empieza el calculo del digito verificador basado en lo que tenemos del RFC
            //En esta parte tampoco conozco el origen matemático del algoritmo como para dar 
            //una explicación del proceso, así que ¡tengamos fe hermanos!.
            int rfcAnumeroSuma = 0, sumaParcial = 0;
            for (int i = 0; i < rfc.Length; i++)
            {
                if (tablaRFC3.ContainsKey(rfc[i].ToString()))
                {
                    rfcAnumeroSuma = Convert.ToInt32(tablaRFC3[rfc[i].ToString()]);
                    sumaParcial += (rfcAnumeroSuma * (14 - (i + 1)));
                }
            }

            int moduloVerificador = sumaParcial % 11;
            if (moduloVerificador == 0)
                rfc += "0";
            else
            {
                sumaParcial = 11 - moduloVerificador;
                if (sumaParcial == 10)
                    rfc += "A";
                else
                    rfc += sumaParcial.ToString();
            }

            //en este punto la variable rfc pasada ya debe tener la homoclave
            //recuerda que la variable rfc se paso como "ref string" lo cual
            //hace que se modifique la original.
        }

        /// <summary>
        /// Verifica si el caracter pasado es una vocal
        /// </summary>
        /// <param name="letra">Caracter a comprobar</param>
        /// <returns>Regresa true si es vocal, de lo contrario false</returns>
        static private bool EsVocal(char letra)
        {
            //Aunque para el caso del RFC cambié todas las letras a mayúsculas
            //igual agregé las minúsculas.
            if (letra == 'A' || letra == 'E' || letra == 'I' || letra == 'O' || letra == 'U' ||
                letra == 'a' || letra == 'e' || letra == 'i' || letra == 'o' || letra == 'u')
                return true;
            else
                return false;
        }

        /// <summary>
        /// Remplaza los artículos comúnes en los apellidos en México con caracter vacío (String.Empty).
        /// </summary>
        /// <param name="palabra">Palabra que se le quitaran los artículos</param>
        /// <returns>Regresa la palabra sin los artículos</returns>
        static private string QuitarArticulos(string palabra)
        {
            return palabra.Replace("DEL ", String.Empty).Replace("LAS ", String.Empty).Replace("DE ", String.Empty).Replace("LA ", String.Empty).Replace("Y ", String.Empty).Replace("A ", String.Empty);
        }

        static private string QuitaProhibidas(string palabra)
        {
            string[] strPalabras = { "BUEI", "BUEY", "CACA", "CACO", "CAGA", "CAGO", "CAKA", "CAKO", "COGE", "COJA",
                "KOGE","KOJO","KAKA","KULO","MAME","MAMO","MEAR","MEAS","MEON","MION","COJE","COJI","COJO","CULO",
                "FETO","GUEY","JOTO","KACA","KACO","KAGA","KAGO","MOCO","MULA","PEDA","PEDO","PENE","PITO","PUTA","PUTO",
                "QULO","RATA","RUIN" };
            string Aceptable = string.Empty;
            foreach (string Prohibida in strPalabras)
            {
                if (palabra == Prohibida)
                {
                    char[] charpalabra = palabra.ToCharArray();
                    charpalabra[3] = 'X';
                    Aceptable = new string(charpalabra);
                }
            }
            return Aceptable;

        }

        /// <summary>
        /// Obtiene el formato de fecha para RFC (yyMMdd).
        /// </summary>
        /// <param name="fNac">Fecha en formatos: dd/MM/yy o dd/MM/yyyy.</param>
        /// <returns>Cadena fecha formato yyMMdd o vacía.</returns>
        static private string ObtieneFecha(string fNac)
        {
            //Establece cultura para México
            //CultureInfo cultura = new CultureInfo("es-MX");
            //por ejemplo: 25/08/68 largo 8; 25/08/1968 largo 10; default 25/08/1968 12:00:00 a.m.
            string Nace = string.Empty;
            //DateTime Fecha = Convert.ToDateTime(fNac);
            //string day = Fecha.Day.ToString();
            //string month = Fecha.Month.ToString();
            //string year = Fecha.Year.ToString();
            Nace = fNac; //.ToString(cultura);
            //Nace = day + "/" + month + "/" + year;
            switch (Nace.Length)
            {
                case 8:
                    Nace = fNac.Substring(6, 2) +
                        fNac.Substring(3, 2) +
                        fNac.Substring(0, 2);
                    break;
                case 10:
                    Nace = fNac.Substring(8, 2) +
                        fNac.Substring(3, 2) +
                        fNac.Substring(0, 2);
                    break;
                default:
                    Nace = fNac.Substring(8, 2) +
                        fNac.Substring(3, 2) +
                        fNac.Substring(0, 2);
                    break;
            }

            //fecha.Substring(6, 2) +
            //fecha.Substring(3, 2) +
            //fecha.Substring(0, 2);
            return Nace;
        }
    }
}
